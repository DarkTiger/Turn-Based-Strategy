using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class GridGenerator : MonoBehaviour
{
    public int worldSizeX = 0;
    public int worldSizeY = 0;
    GameObject tile;


    void Start()
    {
        tile = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TestTile.prefab", typeof(GameObject));

        Generator();
    }


    void Generator()
    {
        for (int y = 0; y < worldSizeY; y++)
        {
            for (int x = 0; x < worldSizeX; x++)
            {
                if ((y % 2) != 0)
                {
                    Instantiate(tile, new Vector3(x, y-0.670f, 1), Quaternion.identity);
                }
                else
                {
                    Instantiate(tile, new Vector3(x+0.5f, y-0.670f, 1), Quaternion.identity);
                } 
            }
        }
    }
}
