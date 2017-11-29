using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour
{
    public int height;
    public int width;

    public float tileSize = 0.5f;
    
	public List<GameObject> enemiesCanGenerate;
    public GameObject enemyGenerateEffect;
      
    public List<GameObject> groundList;
    public GameObject groundBoundary;

    public Transform enemyHolder;
    public Transform itemHolder;

    public myItemDropTable itemDropTable;

    public IntRange totalEnemyWaveRange;
    public IntRange totalEnemyNumberPerWaveRange;

    public GameObject door;


    //Control the player Generate
    public GameObject playerGenerateEffect;
    private GameObject player;
    private Vector3 generatePoint = new Vector3();

    //感谢猪猪猪mei送的辣条----------Memeda~~

    private List<EnemyHealth> enemies = new List<EnemyHealth>();//enemies Generate From this

    private Boundary tileBoundary;

    private int totalWaveNumber;
    private int waveIndex = 1;

    //Gui level COmplete
    public GameObject LevelCompletePanel;

    private void Start()
	{
        LevelCompletePanel.SetActive(false);

        player = Instantiate(Resources.Load("Prefabs/Player") as GameObject);

        totalWaveNumber = totalEnemyWaveRange.Random;

        tileBoundary = new Boundary(0, width * tileSize, 0, height * tileSize);

        GenerateBoard();

        StartCoroutine(GeneratePlayer());  //Generate player and enemy;      

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
            var go = Instantiate(groundBoundary, position, Quaternion.identity,transform);
            var sr = go.GetComponentInChildren<SpriteRenderer>();
            sr.sortingOrder = 1;sr.sortingLayerName = "Effects";
            position = new Vector3(i * tileSize, height*tileSize, 0);
            go = Instantiate(groundBoundary, position, Quaternion.identity,transform);
            go.GetComponentInChildren<SpriteRenderer>().sortingOrder = -2000;
        }
        for (int j = 0;j<height;j++)
        {
            Vector3 position = new Vector3(-tileSize, j*tileSize, 0);
            var go = Instantiate(groundBoundary, position, Quaternion.identity,transform);
            go.GetComponentInChildren<SpriteRenderer>().sortingOrder = -j;
            position = new Vector3( tileSize*width, j*tileSize, 0);
            go = Instantiate(groundBoundary, position, Quaternion.identity,transform);
            go.GetComponentInChildren<SpriteRenderer>().sortingOrder = -j;

        }
    }

	private IEnumerator GenerateEnemies()
	{
        List<Vector3> enemiesPoints = new List<Vector3>();
        for (int i = 0; i<totalEnemyNumberPerWaveRange.Random;i++)
        {       
            FloatRange x = new FloatRange(1, tileBoundary.maxX-1);
            FloatRange y = new FloatRange(1, tileBoundary.maxY-1);
            var position = new Vector3(x.Random,y.Random,0);
            Instantiate(enemyGenerateEffect, position, Quaternion.identity,enemyHolder);
            enemiesPoints.Add(position);
		}

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < enemiesPoints.Count; i++)
        {
            var position = enemiesPoints[i];
            position += new Vector3(0.1f, 0, 0);
            var spawnEnemey = Instantiate(GenerateFromGameObjectList(enemiesCanGenerate), position, Quaternion.identity,enemyHolder) as GameObject;
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
        length = UnityEngine.Random.Range(0, length);
        return gameObjects[length];
    }

	private void EnemyClear()
	{
        //Check if Enemy all destroyed if no generate next wave, yes open the door
        waveIndex++;
        if (waveIndex > totalWaveNumber)
        {

            //this levele Clear
            Debug.Log("Enemy Clear");

            var player = GameObject.FindGameObjectWithTag("Player").transform;

            Instantiate(itemDropTable.generateItem(), player.position, Quaternion.identity,enemyHolder);

            GenerateDoor();
        }
        else
            StartCoroutine(GenerateEnemies());
    }

    private void GenerateDoor()
    {
        FloatRange boundaryWidth = new FloatRange(1, tileBoundary.maxX-tileSize);
        FloatRange boundaryHeight = new FloatRange(1, tileBoundary.maxY-tileSize);

        var point = new Vector3(boundaryWidth.Random, boundaryHeight.Random, 0);

        GameManager.instance.generatePoint = point;  //GameManager Get the next level player generate Point;

        var go = Instantiate(door, point, Quaternion.identity);
        go.GetComponent<TriggerTheDoor>().levelComplete += LevelComplete;

        go.GetComponent<BoxCollider2D>().enabled = true;
    }

    private IEnumerator GeneratePlayer()
    {

        generatePoint = GameManager.instance.generatePoint;

        Instantiate(playerGenerateEffect, generatePoint, Quaternion.identity,enemyHolder);

        yield return new WaitForSeconds(0.5f);        

        player.SetActive(true);

        player.transform.position = generatePoint;

        StartCoroutine(GenerateEnemies());
    }

    public void NextLevel()
    {
        GenerateBoard();
        StartCoroutine(GeneratePlayer());
    }

    private void LevelComplete(object sender, EventArgs e)
    {
        player.SetActive(false);

        LevelCompletePanel.SetActive(true);

        waveIndex = 1;

        totalWaveNumber = totalEnemyWaveRange.Random;

        Debug.Log(totalWaveNumber);

        for (int i = 0; i<enemyHolder.childCount;i++)
        {
            Destroy(enemyHolder.GetChild(i).gameObject);
        }
        
    }    
}


