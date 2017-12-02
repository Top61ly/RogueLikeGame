using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerateControl : MonoBehaviour
{
    public int totalEnemyWave = 3;

    public int waveIndex = 1;

    public EnemyHealthRunTimeSet enemyHealthSet;

    public GameEvent levelClear;

    public void OnEnemyDead()
    {
        if (enemyHealthSet.Count >= 0)
            return;

        waveIndex++;
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

    }

    private void GenerateNextWave()
    {
        Debug.Log("Generate Enemies");

    }
}
