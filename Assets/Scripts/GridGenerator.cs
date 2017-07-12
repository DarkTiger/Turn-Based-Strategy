using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class GridGenerator : MonoBehaviour
{
    public int worldSizeX = 0;
    public int worldSizeY = 0;
    GameObject tileType1; //green
    GameObject tileType2; //yellow


    void Start()
    {
        tileType1 = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TestTile.prefab", typeof(GameObject));
        tileType2 = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TestTileYellow.prefab", typeof(GameObject));
        Generator();
    }


    void Generator()
    {
        for (int y = 0; y < worldSizeY; y++)
        {
            for (int x = 0; x < worldSizeX; x++)
            {
                GameObject tileTemp = tileType1;
                float xTemp = x;
                if ((y % 2) == 0)
                {
                    xTemp += 0.5f;
                    tileTemp = tileType2;
                }

                GameObject newTile = Instantiate(tileTemp, new Vector3(xTemp, y / 1.5f, 1), Quaternion.identity);
                Color tileColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 1);
                newTile.GetComponent<SpriteRenderer>().color = tileColor;
            }
        }
    }
}
