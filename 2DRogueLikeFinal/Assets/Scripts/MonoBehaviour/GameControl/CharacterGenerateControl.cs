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

    public GameObject lootBox;

    public GameObject door;

    public GameObject playerGenerateEffect;

    public GameObject enemyGenerateEffect;

    public Transform enemyHolder;

    public MapGenerator mapGenerator;

    private List<Coord> characterTiles = new List<Coord>();

    private void Start()
    {
        totalEnemyWave = enemyWaveRange.Random;
        mapGenerator.StartGenerateMap(transform);
        characterTiles = mapGenerator.GetCoordList(0);
        PoolManager.instance.CreatePool(player, 1);
        PoolManager.instance.CreatePool(door, 1);
        PoolManager.instance.CreatePool(lootBox, 1);
        PoolManager.instance.CreatePool(playerGenerateEffect, 1);
        PoolManager.instance.CreatePool(enemyGenerateEffect, enemyRange.m_Max);
        StartCoroutine(GeneratePlayer(GetCanGeneratePosition()));
        GenerateNextWave();
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
        PoolManager.instance.ReuseObject(lootBox, GetCanGeneratePosition(), Quaternion.identity);
        PoolManager.instance.ReuseObject(door, GetCanGeneratePosition(), Quaternion.identity);
    }

    private void GenerateNextWave()
    {
        int number = enemyRange.Random;

        for (int i = 0; i < number ; i++)
        {
            int index = Random.Range(0, enemyGameobjectList.Count);

            StartCoroutine(GenerateSingleEnemy(enemyGameobjectList[index], GetCanGeneratePosition()));            
        }
    }

    private Vector3 GetCanGeneratePosition()
    {
        Coord index = characterTiles[Random.Range(0, characterTiles.Count)];
        Vector3 result = new Vector3(-mapGenerator.width / 4 + 0.5f * index.tileX, -mapGenerator.height / 4 + index.tileY * 0.5f, 0);
        return result;
    }

    private IEnumerator GeneratePlayer(Vector3 position)
    {
        PoolManager.instance.ReuseObject(playerGenerateEffect, position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        PoolManager.instance.ReuseObject(player, position,Quaternion.identity);
    }

    private IEnumerator GenerateSingleEnemy(GameObject character,Vector3 position)
    {
        PoolManager.instance.ReuseObject(enemyGenerateEffect, position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(character, position, Quaternion.identity, enemyHolder);
    }

    public void OnLevelStart()
    {
        for (int i = 0; i<enemyHolder.childCount;i++)
        {
            Destroy(enemyHolder.GetChild(i).gameObject);
        }
        mapGenerator.StartGenerateMap(transform);
        characterTiles = mapGenerator.GetCoordList(0);
        totalEnemyWave = enemyWaveRange.Random;
        waveIndex = 1;
        StartCoroutine(GeneratePlayer(GetCanGeneratePosition()));
        GenerateNextWave();
    }
}
