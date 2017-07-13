using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapGenerator : MonoBehaviour
{
    public int worldSizeX = 0;
    public int worldSizeY = 0;
    public GameObject tile;
    public GameObject unit;
    List<GameObject> worldTiles;
    



    void Start()
    {
        worldTiles = new List<GameObject>();
        WorldGenerator();
        AddUnits(1);
        AddUnits(2);
    }


    void WorldGenerator()
    {
        for (int y = 0; y < worldSizeY; y++)
        {
            for (int x = 0; x < worldSizeX; x++)
            {
                float xTemp = x;
                if ((y % 2) == 0)
                {
                    xTemp += 0.5f;
                }

                GameObject newTile = Instantiate(tile, new Vector3(xTemp, y / 1.5f, 1), tile.transform.rotation);

                newTile.GetComponent<TileScript>().typeIndex = Random.Range(0, 5);
                worldTiles.Add(newTile);
            }
        }
    }

    
    void AddUnits(int playerIndex)
    {   
        foreach (GameObject tile in worldTiles)
        {
            Vector3 newPos = tile.transform.position;
            newPos.y += 0.3f;
            newPos.z = -1;

            if (Random.Range(1, 30) == 5)
            {
                GameObject newUnit = Instantiate(unit, newPos, Quaternion.identity);
                newUnit.transform.Rotate(new Vector3(0, 0, 7));

                newUnit.transform.tag = "UnitsP" + playerIndex.ToString();
                newUnit.GetComponent<UnitScript>().ownerIndex = playerIndex;
                newUnit.GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<UnitScript>().sprites[playerIndex - 1];
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
