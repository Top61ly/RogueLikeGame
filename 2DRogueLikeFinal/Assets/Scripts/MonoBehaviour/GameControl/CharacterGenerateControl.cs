using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerateControl : MonoBehaviour
{
    public int totalEnemyWave = 1;

    public int waveIndex = 1;

    public IntRange enemyRange;
    public IntRange enemyWaveRange;

    public EnemyHealthRunTimeSet enemyHealthSet;

    public List<GameObject> enemyGameobjectList;

    public GameObject player;

    public GameObject door;

    public GameObject playerGenerateEffect;

    public GameObject enemyGenerateEffect;  

    public MapGenerator mapGenerator;

    private List<Coord> characterTiles = new List<Coord>();

    private void Start()
    {
        mapGenerator.StartGenerateMap(transform);
        characterTiles = mapGenerator.GetCoordList(0);
       // PoolManager.instance.CreatePool(player, 1);
        PoolManager.instance.CreatePool(door, 1);
    }

    public void OnEnemyDead()
    {
        if (enemyHealthSet.Count > 0)
            return;

        waveIndex++;

        if (isLevelClear())
            GenerateExit();        
        else
        {
            GenerateNextWave();
        }
    }

    private bool isLevelClear()
    {
        if (waveIndex > totalEnemyWave)
            return true;
        return false;
    }

    private void GenerateExit()
    {
        PoolManager.instance.ReuseObject(door, GetCanGeneratePosition(), Quaternion.identity);
    }

    private void GenerateNextWave()
    {
        int number = enemyRange.Random;

        for (int i = 0; i < number ; i++)
        {
            int index = Random.Range(0, enemyGameobjectList.Count);

            Instantiate(enemyGameobjectList[index], GetCanGeneratePosition(), Quaternion.identity);            
        }
    }

    private Vector3 GetCanGeneratePosition()
    {
        Coord index = characterTiles[Random.Range(0, characterTiles.Count)];
        Vector3 result = new Vector3(-mapGenerator.width / 4 + 0.5f * index.tileX, -mapGenerator.height / 4 + index.tileY * 0.5f, 0);
        return result;
    }

    //private IEnumerator GenerateSingleEnemy(int index)
    //{

    //}
}
