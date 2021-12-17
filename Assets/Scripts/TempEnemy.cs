using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    [SerializeField] float m_Health = 1000;
	[SerializeField] bool m_Bleed = false;
    public void TakeDamage(float damage, bool bleed)
	{
		m_Bleed = bleed;
		m_Health -= damage;
	}

	private void Update()
	{
		if(m_Health <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
