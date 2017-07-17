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
    public List<GameObject> worldTiles;
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
        List<Vector3> posSpawnP1 = new List<Vector3>();
        List<Vector3> posSpawnP2 = new List<Vector3>();
        List<int> orderRemains = new List<int>() {0,1,2,3,4};
        List<int> roleOrderP1 = new List<int>();
        List<int> roleOrderP2 = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            int indexRandom;
            do {indexRandom = Random.Range(0, orderRemains.Count);}
            while (orderRemains[indexRandom] == -1);
            roleOrderP1.Add(orderRemains[indexRandom]);
            orderRemains[indexRandom] = -1;
        }

        for (int i = 4; i >= 0; i--)
        {
            roleOrderP2.Add(roleOrderP1[i]);
        }

        //float xTempEven = 0;//-0.603f;
        //float xTempOdd = 0;

        for (int y = 0; y < worldSizeY; y++)
        {
            //bool yTempCalculated = false;

            for (int x = 0; x < worldSizeX; x++)
            {
                float offset = 0;
                float xTemp = x+offset;


                /*GameObject newTile;
                if (!yTempCalculated)
                {                           
                    if (y % 2 == 0)
                    {
                        if (y != 0)
                        {
                            xTempEven += 0.2f;
                            Debug.Log(xTempEven);
                            xTemp += xTempEven;
                        }
                        else if (y == 0)
                        {
                            xTempEven = -0.603f;
                            Debug.Log(xTempEven);
                            xTemp += xTempEven;
                        }
                    }
                    else
                    {
                        if (y != 1)
                        {
                            xTempOdd += 0.2f;
                            Debug.Log(xTempOdd);
                            xTemp += xTempOdd;
                        }
                        else if (y == 1)
                        {
                            xTempOdd += 0f;
                            Debug.Log(xTempOdd);
                            xTemp += xTempOdd;
                        } 
                    }
                }
                yTempCalculated = true;
                newTile = Instantiate(tile, new Vector3(xTemp, y / 1.6f, 1), tile.transform.rotation);*/

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
                    xTemp -= 0.203f;
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

                if (y == 14)
                {
                    xTemp = x + offset;
                    xTemp += 0.8f;
                }
               
                GameObject newTile = Instantiate(tile, new Vector3(xTemp, y / 1.6f, 1), tile.transform.rotation);
                newTile.GetComponent<TileScript>().typeIndex = Random.Range(0, 7);
                newTile.transform.parent = GameObject.Find("Tiles").transform;

                worldTiles.Add(newTile);
                
                if (y == 3)
                {
                    if (x == 1)
                    {
                        posSpawnP1.Add(newTile.transform.position);
                    }

                    if (x == 15)
                    {
                        posSpawnP2.Add(newTile.transform.position);
                    }
                }

                if (y == 5)
                {
                    if (x == 1)
                    {
                        posSpawnP1.Add(newTile.transform.position);
                    }

                    if (x == 15)
                    {
                        posSpawnP2.Add(newTile.transform.position);
                    }
                }

                if (y == 7)
                {
                    if (x == 1)
                    {
                        posSpawnP1.Add(newTile.transform.position);
                    }

                    if (x == 15)
                    {
                        posSpawnP2.Add(newTile.transform.position);
                    }
                }

                if (y == 9)
                {
                    if (x == 1)
                    {
                        posSpawnP1.Add(newTile.transform.position);
                    }

                    if (x == 15)
                    {
                        posSpawnP2.Add(newTile.transform.position);
                    }
                }

                if (y == 11)
                {
                    if (x == 1)
                    {
                        posSpawnP1.Add(newTile.transform.position);
                    }

                    if (x == 15)
                    {
                        posSpawnP2.Add(newTile.transform.position);
                    }
                }
            }
        }

        AddUnits(1, posSpawnP1, roleOrderP1);
        AddUnits(2, posSpawnP2, roleOrderP2);
    }

    
    void AddUnits(int playerIndex, List<Vector3> positions, List<int> unitsOrder)
    {
        int unitIndex = 0;

        for (int i = 0; i < positions.Count; i++)
        {
            int roleIndex = unitsOrder[unitIndex];

            Vector3 newPos = positions[i];
            newPos.y += 0.3f;
            newPos.z = -1;

            GameObject newUnit = Instantiate(unit, newPos, Quaternion.identity);
            newUnit.transform.Rotate(new Vector3(0, 0, 0));
            newUnit.transform.tag = "UnitsP" + playerIndex.ToString();
            newUnit.transform.parent = GameObject.Find("UnitsP" + playerIndex.ToString()).transform;

            UnitScript newUnitScript = newUnit.GetComponent<UnitScript>();
            newUnitScript.ownerIndex = playerIndex;
            newUnitScript.roleIndex = roleIndex;
            newUnitScript.currentMoveCount = 20; //provvisorio per test

            Debug.Log("create");

            if (playerIndex == 1)
            {
                newUnit.GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<UnitScript>().spritesP1[roleIndex];
            }
            else
            {
                newUnit.GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<UnitScript>().spritesP2[roleIndex];
            }

            unitIndex += 1;
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
