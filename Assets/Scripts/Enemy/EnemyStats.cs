using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour //, IDamageable
{
    [SerializeField] private float m_Health = 100;
    [SerializeField] private float m_CurrentHealth;
    [SerializeField] private float m_Damage = 1;
	private EnemyController EC = null;
    public bool m_Bleed = false;

	//[SerializeField] private AttackRadius m_AttackRadius;
	//[SerializeField] private Animator m_Animator;
	//private Coroutine m_LookCoroutine;

	private void Awake()
	{
        m_CurrentHealth = m_Health;
		EC = gameObject.GetComponent<EnemyController>();
		//m_AttackRadius.OnAttack += OnAttack;
	}

	/// <summary>
	/// Called once per frame,
	/// Updates everything
	/// </summary>
	void Update()
    {
		Material mat = EC.GFX.gameObject.GetComponent<Renderer>().material;
		if (m_CurrentHealth > 0.8f * m_Health)
		{
			mat.color = Color.blue;
		}

		else if (m_CurrentHealth > 0.6f * m_Health)
		{
			mat.color = Color.green;
		}

		else if (m_CurrentHealth > 0.4f * m_Health)
		{
			mat.color = Color.yellow;
		}

		else if (m_CurrentHealth > 0.2f * m_Health)
		{
			mat.color = Color.red;
		}

		else if (m_CurrentHealth <= 0)
		{
			Die();
		}
	}

	//public void OnAttack(IDamageable Target)
	//{
	//	PoolManager.Instance.SpawnFromPool("Explosion", transform.position, transform.rotation);
	//	if (m_LookCoroutine != null)
	//	{
	//		StopCoroutine(m_LookCoroutine);
	//	}

	//	m_LookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
	//}

	//private IEnumerator LookAt(Transform target)
	//{
	//	Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
	//	float time = 0;

	//	while (time < 1)
	//	{
	//		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
	//		time += Time.deltaTime * 2;
	//		yield return null;
	//	}

	//	transform.rotation = lookRotation;
	//}

	public void TakeDamage(float damage, bool bleed)
    {
        m_Bleed = bleed;
        m_CurrentHealth -= damage;
    }


    /// <summary>
    /// Called when the enemy dies,
    /// Plays animations, drops loot etc
    /// </summary>
    private void Die()
    {
        gameObject.SetActive(false);
        EC.EnemySpawner.GetComponent<Spawner>().RemoveEnemy(gameObject);
    }
    public void Spawn()
	{
        m_CurrentHealth = m_Health;
	}

	//public Transform GetTransform()
	//{
	//	return transform;
	//}

	//public void TakeDamage(float damage)
	//{
	//	m_Health -= damage;
	//}
}
