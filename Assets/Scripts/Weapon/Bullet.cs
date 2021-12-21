using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Rigidbody m_RigidBody = null;
    private float m_Speed = 1;						// The initial speed of the bullets
    private float m_Damage = 1;						// The damage of the bullet
    private bool m_Deflect = false;                 // If the bullet would deflect or not
	private float m_BouncesLeft = 2;
	private bool m_ExplodeOnImpact = false;			// Does the bullet explode on impact
	private float m_ExplosionDamagePercent = 0.4f;	// The percentage of the base damage the explosion will do
	private bool m_BleedingDamage = false;			// Does the bullet inflict bleeding damge
	private float m_BleedDamagePercent = 0.05f;		// The percentage of gun damage the bleeding damage will do
	private float m_BulletBleedDuration = 1;		// The time the bleed effect will last for 
	private float m_TimeBetweenBleed = 0.30f;		// The time between each tick of bleed
	private int m_BounceAmount = 3;					// The amount times the bullet will bouce before destroying
	private int m_TimeTillDestroy = 10;             // The time in seconds the bullet will stay alive untill it is destroyed
	private bool m_Piercing = false;                // If the bullet will go through enemies or not
	private int m_PiercingAmount = 1;               // The amount of times a bullet will go through an enemy 
	private float m_PiercingLeft = 0;
	private float m_Timer = 0;                      // The timer
	[SerializeField] private LayerMask m_Wall;

	private void Awake()
	{
		m_RigidBody = gameObject.GetComponent<Rigidbody>();	
	}

	/// <summary>
	/// Called every frame,
	/// Updates the bullet movement
	/// </summary>
	private void LateUpdate()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 0.3f, m_Wall))
		{
			if(m_Deflect && m_BouncesLeft > 0)
			{
				transform.forward = Vector3.Reflect(transform.forward, hit.normal);
				m_BouncesLeft--;
			}
			else
			{
				OnFinalHit(hit.transform.gameObject);
			}
		}

		m_RigidBody.velocity = m_RigidBody.transform.forward * ((m_Speed * 10) * Time.deltaTime);
		m_Timer += Time.deltaTime;
		
		// Deactivates bullet
		if(m_Timer >= m_TimeTillDestroy)
		{
			gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// Called when spawning a bullet from a bullet pool,
	/// Spawns a new bullet by reactivating the bullet in a pool
	/// </summary>
	/// <param name="position">The position of the end of the barrel</param>
	/// <param name="forward">The Forward direction of the player</param>
	/// <param name="damage">The amount of damage the bullet will do</param>
	/// <param name="deflect">If the bullet deflects on surfaces</param>
	/// <param name="explode">If the bullet explodes on inpact</param>
	/// <param name="bleeding">If the bullet gives bleed damage over time</param>
	public void Spawn(Vector3 position, Vector3 forward, float damage, float speed, bool deflect, int deflectCount, bool explode, bool bleeding, bool piercing, int piercingAmount)
	{
		m_Timer = 0;
		m_Speed = speed;
		gameObject.transform.position = position;
		gameObject.transform.forward = forward;
		m_Damage = damage;
		m_Deflect = deflect;
		m_BounceAmount = deflectCount;
		m_ExplodeOnImpact = explode;
		m_BleedingDamage = bleeding;
		m_Piercing = piercing;
		m_PiercingAmount = piercingAmount;
		m_BouncesLeft = m_BounceAmount;
		m_PiercingLeft = m_PiercingAmount;
		gameObject.SetActive(true);
	}

	private void OnFinalHit(GameObject obj)
	{
		gameObject.SetActive(false);
		if (m_ExplodeOnImpact)
		{
			GameObject newObj = PoolManager.Instance.SpawnFromPool("Explosion", transform.position, transform.rotation);
			newObj.GetComponent<Explode>().Spawn(transform.position, obj.transform);
		}
		if (obj.tag == "Enemy")
		{
			obj.GetComponent<EnemyController>().EnemyStats.TakeDamage(m_Damage, m_BleedingDamage);
		}
	}
	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
			gameObject.SetActive(false);
			Debug.Log("Collided with Player");
		}

		if (other.gameObject.tag == "Bullet")
		{
			Debug.Log("Collided with Bullet");
		}
		
		if(other.transform.gameObject.tag == "Enemy")
		{
			Debug.Log("Enemy Hit!");
			//Do damage
			if(m_Piercing && m_PiercingLeft > 0)
			{
				m_PiercingLeft--;
				other.gameObject.GetComponent<EnemyController>().EnemyStats.TakeDamage(m_Damage, m_BleedingDamage);
			}

			else
			{
				OnFinalHit(other.gameObject);
			}
		}
	}
}
