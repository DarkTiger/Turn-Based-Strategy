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
    GameManagerScript gameManager;



    void Start()
    {
        worldTiles = new List<GameObject>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();

        WorldGenerator();
        
        
        gameManager.mapCreated = true;
    }


    //nascondiamo le miserie
    void WorldGenerator()
    {
        for (int y = 0; y < worldSizeY; y++)
        {
            for (int x = 0; x < worldSizeX; x++)
            {
                float offset = 0;

                float xTemp = x+offset;

                if (y == 0)
                {
                    xTemp -= 0.603f;
                }

                if (y == 1)
                {
                    xTemp = x + offset;
                    xTemp += 0.0f;
                }

                if (y == 2)
                {
                    xTemp = x + offset;
                    xTemp -= 0.403f;
                }

                if (y == 3)
                {
                    xTemp = x + offset;
                    xTemp += 0.203f;
                }

                if (y == 4)
                {
                    xTemp = x + offset;
                    xTemp -= 0.2f;
                }

                if (y == 5)
                {
                    xTemp = x + offset;
                    xTemp += 0.4f;
                }

                if (y == 6)
                {

                }

                if (y == 7)
                {
                    xTemp = x + offset;
                    xTemp += 0.6f;
                }

                if (y == 8)
                {
                    xTemp = x + offset;
                    xTemp += 0.2f;
                }

                if (y == 9)
                {
                    xTemp = x + offset;
                    xTemp += 0.8f;
                }

                if (y == 10)
                {
                    xTemp = x + offset;
                    xTemp += 0.4f;
                }

                if (y == 11)
                {
                    xTemp = x + offset;
                    xTemp += 1f;
                }

                if (y == 12)
                {
                    xTemp = x + offset;
                    xTemp += 0.6f;
                }

                if (y == 13)
                {
                    xTemp = x + offset;
                    xTemp += 1.2f;
                }

                GameObject newTile = Instantiate(tile, new Vector3(xTemp, y / 1.6f, 1), tile.transform.rotation);
                newTile.GetComponent<TileScript>().typeIndex = Random.Range(0, 6);
                newTile.transform.parent = GameObject.Find("Tiles").transform;

                worldTiles.Add(newTile);
            }
        }

        AddUnits(1);
        AddUnits(2);
    }

    
    void AddUnits(int playerIndex)
    {
        bool unitCreated = false;

        foreach (GameObject tile in worldTiles)
        {
            Vector3 newPos = tile.transform.position;
            newPos.y += 0.3f;
            newPos.z = -1;

            if (Random.Range(1, 30) == 5)
            {
                if (!unitCreated)
                {
                    GameObject newUnit = Instantiate(unit, newPos, Quaternion.identity);
                    newUnit.transform.Rotate(new Vector3(0, 0, 0));
                    newUnit.transform.tag = "UnitsP" + playerIndex.ToString();
                    newUnit.transform.parent = GameObject.Find("UnitsP" + playerIndex.ToString()).transform;

                    UnitScript newUnitScript = newUnit.GetComponent<UnitScript>();
                    newUnitScript.ownerIndex = playerIndex;
                    newUnitScript.currentMoveCount = 20; //provvisorio per test
                                

                    if (playerIndex == 1)
                    {
                        newUnit.GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<UnitScript>().spritesP1[Random.Range(0, 5)];
                    }
                    else
                    {
                        newUnit.GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<UnitScript>().spritesP2[Random.Range(0, 5)];
                    }

                    unitCreated = true;
                }
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
