using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerateControl : MonoBehaviour
{
    public int totalEnemyWave = 1;

    public int waveIndex = 1;

    public EnemyHealthRunTimeSet enemyHealthSet;

    public GameObject door;

    private List<Coord> characterTiles = new List<Coord>();
    public MapGenerator mapGenerator;

    private void Start()
    {
        mapGenerator.StartGenerateMap(transform);
        characterTiles = mapGenerator.GetCoordList(0);
        PoolManager.instance.CreatePool(door, 1);
    }

    public void OnEnemyDead()
    {
        if (enemyHealthSet.Count > 0)
            return;

        waveIndex++;
        Debug.Log(waveIndex);
        if (isLevelClear())
        {
            GenerateExit();
        }
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
        Debug.Log("Generate Enemies");
    }

    private Vector3 GetCanGeneratePosition()
    {
        Coord index = characterTiles[Random.Range(0, characterTiles.Count)];
        Vector3 result = new Vector3(-mapGenerator.width / 4 + 0.5f * index.tileX, -mapGenerator.height / 4 + index.tileY * 0.5f, 0);
        Debug.Log(index.tileX + "   " + index.tileY);
        return result;
    }
}
