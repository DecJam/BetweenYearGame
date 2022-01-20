using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public EnemyMovement Movement;
	public NavMeshAgent Agent;
	public EnemyScriptableObject enemyScriptableObject;
	public AttackRadius m_AttackRadius;
	public float Health = 100.0f;

	private void OnEnable()
	{
		SetUpAgentFromConfiguration();
	}

	public virtual void SetUpAgentFromConfiguration()
	{
		Agent.acceleration = enemyScriptableObject.Acceleration;
		Agent.angularSpeed = enemyScriptableObject.AngularSpeed;
		Agent.areaMask = enemyScriptableObject.AreaMask;
		Agent.avoidancePriority = enemyScriptableObject.AvoidancePriority;
		Agent.baseOffset = enemyScriptableObject.BaseOffset;
		Agent.height = enemyScriptableObject.Height;
		Agent.obstacleAvoidanceType = enemyScriptableObject.ObstacleAvoidanceType;
		Agent.radius = enemyScriptableObject.Radius;
		Agent.speed = enemyScriptableObject.Speed;
		Agent.stoppingDistance = enemyScriptableObject.StoppingDistance;

		//Movement.UpdateRate = enemyScriptableObject.AIUpdateInterval;
		Health = enemyScriptableObject.health;
		m_AttackRadius.collider.radius = enemyScriptableObject.AttackRadius;
		m_AttackRadius.AttackDelay = enemyScriptableObject.AttackDelay;
		m_AttackRadius.Damage = enemyScriptableObject.Damage;
	}
}
