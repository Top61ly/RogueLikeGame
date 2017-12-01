using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "myCreate/Tile/BoardCollection")]
public class BoardCollection : ScriptableObject
{
    public GameObject boundary;
    public List<GameObject> boards;

    public float tileSize;

    public void GenerateBoard(int[,] map,Transform parent)
    {
        for (int i = 0; i < map.GetLength(0); i++)
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 0)
                {
                    Vector3 position = new Vector3(i * tileSize, j * tileSize, 0);
                    Instantiate(boards[Random.Range(0, boards.Count)],position,Quaternion.identity,parent);
                    for (int m = i-1;m<=i+1;m++)
                        for (int n = j-1;n<=j+1;n++)
                        {
                            if (map[m, n] == 1)
                                map[m, n] = 2;
                        }
                }
            }
    }  
    
    public void GenerateBoundary(int[,] map, Transform parent)
    {
        for (int i = 0; i < map.GetLength(0); i++)
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 2)
                {
                    Vector3 position = new Vector3(i * tileSize, j * tileSize, 0);
                    Instantiate(boundary,position,Quaternion.identity,parent);
                }
            }
    }
}
