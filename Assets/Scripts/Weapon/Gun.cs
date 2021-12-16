using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	// Bullet stuff
	private Vector3 m_BulletExitPosition;           // The exit position of the bullet from the gun
	[SerializeField] private bool m_FirePressed = false;				// If the fire button is currently pressed
	public bool FirePressed { get { return m_FirePressed; } set { m_FirePressed = value; } }

	[SerializeField] private float m_BulletDamage = 1;
	[SerializeField] private float m_RateOfFire = 1.0f;              // The amount of times per second the gun can fire
	private float m_Timer = 0.0f;                   // The timer managing the rate of fire
	[SerializeField] private float m_BulletSpeed = 1.0f;			// Speed of the bullet
	[SerializeField] private bool m_BulletDeflect = false;           // If the bullet will deflect or not
	[SerializeField] private bool m_BulletExplode = false;           // If the bullet will explode on impact
	[SerializeField] private bool m_BulletBleed = false;				// If the bullet will inflict damage over time

	private void Update()
	{
		m_Timer += Time.deltaTime;
		if (m_FirePressed)
		{
			Fire();
		}

	}

	public void Fire()
	{
		if(m_Timer >= 1 / m_RateOfFire)
		{
			GameObject bullet;
			bullet = PoolManager.Instance.SpawnFromPool("Bullet", this.transform.position, Player.Instance.transform.rotation);
			bullet.GetComponent<Bullet>().Spawn(this.transform.position, Player.Instance.transform.forward, m_BulletDamage, m_BulletSpeed, m_BulletDeflect, m_BulletExplode, m_BulletBleed);
			m_Timer = 0;
		}

	}
}
