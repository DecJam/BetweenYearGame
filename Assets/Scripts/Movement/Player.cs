using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Dead,
    Alive
}

/// <summary>
/// A class that manages and references all scripts relating to the player
/// </summary>
public class Player : MonoBehaviour , IDamageable
{
    [SerializeField] public CharacterController characterController = null;  // The player's rigidbody

    [SerializeField] public GameObject m_Weapon = null;

    [Header("Attached Scripts")]
	[SerializeField] public PlayerMovement Movement = null;                 // The player's movement script
    [SerializeField] public PlayerInputManager Input = null;                // The player's input script

    public PlayerState State = PlayerState.Alive;

    [SerializeField] private AttackRadius m_AttackRadius;
    [SerializeField] private Animator m_Animator;
    private Coroutine m_LookCoroutine = null;

    [SerializeField] private float m_Health;


    private static Player m_Instance;                                       // The current instance of the player
    public static Player Instance                                           // The public current instance of MenuController
    {
        get { return m_Instance; }
    }

    void Awake()
    {
        // Initialize Singleton
        if (m_Instance != null && m_Instance != this)
            Destroy(this.gameObject);
        else
            m_Instance = this;

        //m_AttackRadius.OnAttack += OnAttack;
    }

    public void OnAttack(IDamageable Target)
	{
        PoolManager.Instance.SpawnFromPool("Explosion", transform.position, transform.rotation);
        if (m_LookCoroutine != null)
		{
            StopCoroutine(m_LookCoroutine);
		}

        m_LookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
	}

    private IEnumerator LookAt(Transform target)
	{
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        float time = 0;

        while(time < 1)
		{
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * 2;
            yield return null;
		}

        transform.rotation = lookRotation;
	}

    public void TakeDamage(int Damage)
	{
        m_Health -= Damage;
        if (m_Health <= 0)
		{
            gameObject.SetActive(false);
            Debug.Log("You Died");
		}
	}


    public Transform GetTransform()
	{
        return transform;
	}

    public GameObject GetPlayer()
	{
        return gameObject;
	}

	public void TakeDamage(float damage)
	{
		m_Health -= damage;
	}
}