using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float m_Health = 100;
    [SerializeField] private float m_Damage = 1;

    /// <summary>
    /// Called once per frame,
    /// Updates everything
    /// </summary>
    void Update()
    {
        if(m_Health <= 0)
		{
            Die();
		}
    }

    /// <summary>
    /// Called when the enemy dies,
    /// Plays animations, drops loot etc
    /// </summary>
    private void Die()
	{
        gameObject.SetActive(false);
	}
}
