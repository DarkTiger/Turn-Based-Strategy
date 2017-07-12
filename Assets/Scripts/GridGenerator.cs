using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


public class GridGenerator : MonoBehaviour
{
    public int worldSizeX = 0;
    public int worldSizeY = 0;
    GameObject tileType1; //grass
    GameObject tileType2; //desert
    GameObject tileType3; //forest
    GameObject tileType4; //lake
    GameObject tileType5; //mountain
    List<GameObject> worldTiles;
    GameObject pg;



    void Start()
    {
        tileType1 = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TileGrass.prefab", typeof(GameObject));
        tileType2 = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TileDesert.prefab", typeof(GameObject));
        tileType3 = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TileForest.prefab", typeof(GameObject));
        tileType4 = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TileLake.prefab", typeof(GameObject));
        tileType5 = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TileMountain.prefab", typeof(GameObject));
        pg = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Pg.prefab", typeof(GameObject));
        worldTiles = new List<GameObject>();

        WorldGenerator();
        AddSomeUnits();
    }


    void WorldGenerator()
    {
        for (int y = 0; y < worldSizeY; y++)
        {
            for (int x = 0; x < worldSizeX; x++)
            {
                GameObject tileTemp = tileType1;

                switch (Random.Range(0, 5))
                {
                    case 1: tileTemp = tileType2; break;
                    case 2: tileTemp = tileType3; break;
                    case 3: tileTemp = tileType4; break;
                    case 4: tileTemp = tileType5; break;
                }
                
                float xTemp = x;
                if ((y % 2) == 0)
                {
                    xTemp += 0.5f;
                }

                GameObject newTile = Instantiate(tileTemp, new Vector3(xTemp, y / 1.5f, 1), tileTemp.transform.rotation);
                worldTiles.Add(newTile);
            }
        }
    }

    
    void AddSomeUnits()
    {   
        foreach (GameObject tile in worldTiles)
        {
            Vector3 newPos = tile.transform.position;
            newPos.y += 0.3f;
            newPos.z = -1;

            if (Random.Range(1, 20) == 5)
            {
                GameObject newPG = Instantiate(pg, newPos, Quaternion.identity);
                newPG.transform.Rotate(new Vector3(0, 0, 7));
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            EditorSceneManager.LoadScene("Main");
        }
    }
}
