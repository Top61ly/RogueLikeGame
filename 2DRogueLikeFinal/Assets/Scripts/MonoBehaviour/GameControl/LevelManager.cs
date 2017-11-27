using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public int height;
    public int width;

    public float tileSize = 0.5f;

	public List<EnemyHealth> enemies;

	public GameObject enemy;
    public GameObject enemyGenerateEffect;

    public IntRange enemyIndex = new IntRange(1,4);
      
    public List<GameObject> groundList = new List<GameObject>();
    public GameObject groundBoundary;

    public myItemDropTable itemDropTable;
        
	private void Start()
	{
        GenerateBoard();
        GenerateEnemies();
    }


    private void GenerateBoard()
    {
        for (int i = 0;i<width;i++)
            for (int j = 0; j<height;j++)
            {
                Vector3 position = new Vector3(i * tileSize, j * tileSize, 0);
                Instantiate(GenerateFromGameObjectList(groundList), position, Quaternion.identity,transform);
            }
        for (int i = -1;i<width+1;i++)
        {
            Vector3 position = new Vector3(i * tileSize, -tileSize, 0);
            var go = Instantiate(groundBoundary, position, Quaternion.identity);
            go.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
            position = new Vector3(i * tileSize, height*tileSize, 0);
            go = Instantiate(groundBoundary, position, Quaternion.identity);
            go.GetComponentInChildren<SpriteRenderer>().sortingOrder = -height;
        }
        for (int j = 0;j<height;j++)
        {
            Vector3 position = new Vector3(-tileSize, j*tileSize, 0);
            var go = Instantiate(groundBoundary, position, Quaternion.identity);
            go.GetComponentInChildren<SpriteRenderer>().sortingOrder = -j;
            position = new Vector3( tileSize*width, j*tileSize, 0);
            go = Instantiate(groundBoundary, position, Quaternion.identity);
            go.GetComponentInChildren<SpriteRenderer>().sortingOrder = -j;

        }
    }

	private void GenerateEnemies()
	{   
		for (int i = 0; i<30;i++)
		{
            FloatRange x = new FloatRange(0, width * tileSize);
            FloatRange y = new FloatRange(0, height * tileSize);
            var position = new Vector3(x.Random,y.Random,0);
            Instantiate(enemyGenerateEffect, position, Quaternion.identity);
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
