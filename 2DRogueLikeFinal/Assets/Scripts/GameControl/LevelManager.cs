using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{

	public List<EnemyHealth> enemies;

	public GameObject enemy;

	private void Start()
	{
	  //  GenerateEnemies();
	}

	private void GenerateEnemies()
	{
		Vector3 position = new Vector3(0, 0, 0);
		for (int i = 0; i<5;i++)
		{
			var spawnEnemey = Instantiate(enemy, position, Quaternion.identity) as GameObject;
			position += new Vector3(0.1f, 0, 0);
			var enemyHealth = spawnEnemey.GetComponent<EnemyHealth>();
			enemies.Add(enemyHealth);
			enemyHealth.OnEnemyDestroyed += OnEnemyDestroyed;
		}
	}

	private void OnEnemyDestroyed(object sender, EventArgs e)
	{
		enemies.Remove(sender as EnemyHealth);
		if (enemies.FindAll(eh=>!eh.healthPoints.Equals(0)).Count == 0)
		{
			EnemyClear();
		}
	}

	private void EnemyClear()
	{
		Debug.Log("Enemy Clear");
	}
}
