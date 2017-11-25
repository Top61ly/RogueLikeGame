using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{

	public List<EnemyHealth> enemies;

	public GameObject enemy;

    public IntRange enemyIndex = new IntRange(1,4);

    public GameObject Door;

    public List<GameObject> groundList = new List<GameObject>();

    public myItemDropTable itemDropTable;

	private void Start()
	{
        GenerateBoard();
        GenerateEnemies();
    }


    private void GenerateBoard()
    {
        for (int i = 0;i<100;i++)
            for (int j = 0; j<100;j++)
            {
                Vector3 position = new Vector3(i * 0.5f, j * 0.5f, 0);
                Instantiate(GenerateFromGameObjectList(groundList), position, Quaternion.identity,transform);
            }
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
		if (enemies.Count == 0)
		{
			EnemyClear();
		}
	}
    
    private GameObject GenerateFromGameObjectList(List<GameObject> gameObjects)
    {
        int length = gameObjects.Count;
        length = UnityEngine.Random.Range(0, length - 1);
        return gameObjects[length];
    }

	private void EnemyClear()
	{
        //Check if Enemy all destroyed if no generate next wave, yes open the door
		Debug.Log("Enemy Clear");

        var player = GameObject.FindGameObjectWithTag("Player").transform;

        Instantiate(itemDropTable.generateItem(),player.position,Quaternion.identity);
	}
}
