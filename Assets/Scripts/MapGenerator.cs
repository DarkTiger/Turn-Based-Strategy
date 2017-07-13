using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapGenerator : MonoBehaviour
{
    public int worldSizeX = 0;
    public int worldSizeY = 0;
    public GameObject tileType1; //grass
    public GameObject tileType2; //desert
    public GameObject tileType3; //forest
    public GameObject tileType4; //lake
    public GameObject tileType5; //mountain
    public GameObject unit;
    List<GameObject> worldTiles;




    void Start()
    {
        worldTiles = new List<GameObject>();

        WorldGenerator();
    }


    void WorldGenerator()
    {
        for (int y = 0; y < worldSizeY; y++)
        {
            for (int x = 0; x < worldSizeX; x++)
            {
                GameObject tileTemp = tileType1;

                switch (Random.Range(0, 7))
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
                GameObject newPG = Instantiate(unit, newPos, Quaternion.identity);
                newPG.transform.Rotate(new Vector3(0, 0, 7));
            }
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
