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
	private int m_TimeTillDestroy = 5;				// The time in seconds the bullet will stay alive untill it is destroyed
	private float m_Timer = 0;						// The timer
    private GameObject m_BulletObject;				// The model for the bullet

	/// <summary>
	/// Called every frame,
	/// Updates the bullet movement
	/// </summary>
	private void LateUpdate()
	{
		m_BulletObject.transform.position += m_BulletObject.transform.forward * Time.deltaTime * m_Speed;
		m_Timer += Time.deltaTime;
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
	private void Spawn(Vector3 position, Vector3 forward, int damage, bool deflect, bool explode, bool bleeding)
	{
		m_Timer = 0;
		m_BulletObject.transform.position = position;
		m_BulletObject.transform.forward = forward;
		m_Damage = damage;
		m_Deflect = deflect;
		m_ExplodeOnImpact = explode;
		m_BleedingDamage = bleeding;

		m_BulletObject.SetActive(true);
	}
}
