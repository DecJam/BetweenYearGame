using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Scriptable Object that holds the BASE STATS of the enemy,
/// </summary>
[CreateAssetMenu(fileName = "Enemy Configuration", menuName ="Scriptableobject/Enemy Configuration")]
public class EnemyScriptableObject : ScriptableObject
{
	public float health = 100.0f;
	public float AttackDelay = 1f;
	public float Damage = 5;
	public float AttackRadius = 1.5f;

	public float AIUpdateInterval = 0.1f;

	public float Acceleration = 0;
	public float AngularSpeed = 120;

	public int AreaMask = -1;
	public int AvoidancePriority = 50;
	public float BaseOffset = 0;
	public float Height = 2f;
	public ObstacleAvoidanceType ObstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
	public float Radius = 0.5f;
	public float Speed = 3f;
	public float StoppingDistance = 0.5f;
}
