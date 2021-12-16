using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float m_Speed = 1;						// The initial speed of the bullets
    private float m_Damage = 1;						// The damage of the bullet
    private bool m_Deflect = false;					// If the bullet would deflect or not
	private bool m_ExplodeOnImpact = false;			// Does the bullet explode on impact
	private float m_ExplosionDamagePercent = 0.4f;	// The percentage of the base damage the explosion will do
	private bool m_BleedingDamage = false;			// Does the bullet inflict bleeding damge
	private float m_BleedDamagePercent = 0.05f;		// The percentage of gun damage the bleeding damage will do
	private float m_BulletBleedDuration = 1;		// The time the bleed effect will last for 
	private float m_TimeBetweenBleed = 0.30f;		// The time between each tick of bleed
	private int m_bounceAmount = 1;					// The amount times the bullet will bouce before destroying
	private int m_TimeTillDestroy = 10;				// The time in seconds the bullet will stay alive untill it is destroyed
	private float m_Timer = 0;                      // The timer

	/// <summary>
	/// Called every frame,
	/// Updates the bullet movement
	/// </summary>
	private void LateUpdate()
	{
		gameObject.transform.position += gameObject.transform.forward * m_Speed * Time.deltaTime;
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
	public void Spawn(Vector3 position, Vector3 forward, float damage, float speed, bool deflect, bool explode, bool bleeding)
	{
		m_Timer = 0;
		m_Speed = speed;
		gameObject.transform.position = position;
		gameObject.transform.forward = forward;
		m_Damage = damage;
		m_Deflect = deflect;
		m_ExplodeOnImpact = explode;
		m_BleedingDamage = bleeding;

		gameObject.SetActive(true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Wall")
		{
			if (m_Deflect)
			{
				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.forward, out hit))
				{
					float perp = 2.0f * Vector3.Dot(gameObject.transform.forward, hit.normal);
					gameObject.transform.forward = gameObject.transform.forward - (perp * hit.normal);
				}
			}
			else
			{
				gameObject.SetActive(false);
			}

			Debug.Log("Collided with wall");
		}

		if (other.gameObject.tag == "Player")
		{
			gameObject.SetActive(false);
			Debug.Log("Collided with wall");
		}

		if (other.gameObject.tag == "Bullet")
		{
			Debug.Log("Collided with Bullet");
		}
	}
}
