using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum EnemyTypes
{
	Small,
	Medium,
	Large
}

public class BasicEnemySpawner : EditorWindow
{
	public EnemyTypes m_EnemyType;
	float m_SpawnRadius;
	GameObject m_SpawnerPrefab;

	List<GameObject> m_SpawnedSpawners;

	private void Awake()
	{
		m_SpawnedSpawners = new List<GameObject>();
	}

	[MenuItem("Custom Tools/Basic Enemy Spanwer (Test)")]
	public static void ShowWindow()
	{
		GetWindow(typeof(BasicEnemySpawner));
	}

	private void OnGUI()
	{
		GUILayout.Label("Create new spawner", EditorStyles.boldLabel);
		m_EnemyType = (EnemyTypes)EditorGUILayout.EnumPopup("EnemyType", m_EnemyType);
		m_SpawnRadius = EditorGUILayout.Slider("Spawn Radius", m_SpawnRadius, 0.5f, 10.0f);
		m_SpawnerPrefab = EditorGUILayout.ObjectField("Spawner Prefab", m_SpawnerPrefab, typeof(GameObject),false)as GameObject;
		
		if(GUILayout.Button("Create spawner"))
		{
			SpawnSpawner();
		}
	}

	private void SpawnSpawner()
	{
		GameObject obj = Instantiate(m_SpawnerPrefab, Vector3.zero, Quaternion.identity);
		obj.name = "Spawner";
		m_SpawnedSpawners.Add(obj);
	}
}
