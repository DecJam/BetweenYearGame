using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private float m_Frequency = 5;
	[SerializeField] private float m_Radius = 1;
	[SerializeField] private int m_SpawnTotal = 0;
	private int m_SpawnCount = 0;
	private float timer = 0;
	private List<GameObject> m_SpawnedEnemies;

	private void Awake()
	{
		m_SpawnedEnemies = new List<GameObject>();
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= m_Frequency && m_SpawnCount < m_SpawnTotal)
		{
			timer = 0;
			float r = m_Radius * Mathf.Sqrt(Random.Range(0, 1));
			float theta = Random.Range(0, 1) * 2 * Mathf.PI;
			float x = gameObject.transform.position.x + r * Mathf.Cos(theta);
			float y = gameObject.transform.position.y + r * Mathf.Sin(theta);

			Vector3 pos = new Vector3(x, y, gameObject.transform.position.z);
			GameObject enemy = PoolManager.Instance.SpawnFromPool("Enemy", pos, Quaternion.identity);
			EnemyController EC = enemy.GetComponent<EnemyController>();
			EC.EnemySpawner = this.gameObject;
			EC.EnemyStats.Spawn();
			m_SpawnCount++;
			m_SpawnedEnemies.Add(enemy);
		}
	}

	public void RemoveEnemy(GameObject enemy)
	{
		m_SpawnedEnemies.Remove(enemy);
		m_SpawnCount--;
	}
}
