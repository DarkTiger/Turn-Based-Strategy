using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


public class GridGenerator : MonoBehaviour
{
    public int worldSizeX = 0;
    public int worldSizeY = 0;
    GameObject tileType1; //green
    GameObject tileType2; //yellow
    List<GameObject> worldTiles;
    GameObject pg;



    void Start()
    {
        tileType1 = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TestTile.prefab", typeof(GameObject));
        tileType2 = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/TestTileYellow.prefab", typeof(GameObject));
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
                float xTemp = x;
                if ((y % 2) == 0)
                {
                    xTemp += 0.5f;
                    tileTemp = tileType2;
                }

                GameObject newTile = Instantiate(tileTemp, new Vector3(xTemp, y / 1.5f, 1), tileTemp.transform.rotation);
                Color tileColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 1);
                newTile.GetComponent<SpriteRenderer>().color = tileColor;

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
