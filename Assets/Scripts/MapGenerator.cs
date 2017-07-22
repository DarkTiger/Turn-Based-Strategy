using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapGenerator : MonoBehaviour
{
    GameManagerScript gameManager;

    public int worldSizeX = 0;
    public int worldSizeY = 0;
    public GameObject tile;
    public GameObject unit;
    public List<GameObject> worldTiles;
  

    void Start()
    {
        worldTiles = new List<GameObject>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();

        //List<int> unitsRolesP1 = new List<int>() { 1, 1, 0, 3, 4, 5, 6 };
        //List<int> unitsRolesP2 = new List<int>() { 1, 1, 2, 3, 4, 5, 6 };

        //WorldGenerator(unitsRolesP1, unitsRolesP2); 
    }


    public void CreateMap(List<int> unitsRolesP1, List<int> unitsRolesP2)
    {
        List<Vector3> posSpawnP1 = new List<Vector3>();
        List<Vector3> posSpawnP2 = new List<Vector3>();

        unitsRolesP1[6] = -1;
        unitsRolesP2[6] = -1;

        /// SISTEMA RANDOM INCROCIATO:
        /// 
        //List<int> orderRemains = new List<int>() {0, 1, 2, 3, 4, 5, 6};
        /*for (int i = 0; i < 5; i++)
        {
            int indexRandom;
            do {indexRandom = Random.Range(0, orderRemains.Count);}
            while (orderRemains[indexRandom] == -1);
            roleOrderP1.Add(orderRemains[indexRandom]);
            orderRemains[indexRandom] = -1;

            Debug.Log("PLAYER1: " + orderRemains[indexRandom].ToString());
        }

        for (int i = 4; i >= 0; i--)
        {
            roleOrderP2.Add(roleOrderP1[i]);
            Debug.Log("PLAYER2: " + roleOrderP1[i].ToString());
        }*/
        /////////////////////////////////

        float tileXDiff = 0.2f;
        float yEvenStartPos = -0.6f;
        float yOddStartPos = 0f;
        int currentY = -1;
        float xTemp;

        for (int y = 0; y < worldSizeY; y++)
        {
            for (int x = 0; x < worldSizeX; x++)
            {
                xTemp = x;

                /*if (y % 2 == 0)
                {
                    if (y != 0)
                    {
                        
                    xTemp += yEvenStartPos;
                    yEvenStartPos = -0.6f;
                }
                    else
                    {
                        xTemp -= 0.6f;
                    }
                }
                else
                {
                    if (y != 1)
                    {
                        if (y != currentY)
                        {
                            
                        xTemp += yOddStartPos;
                        yOddStartPos = 0f;
                    }
                    }
                }

                yEvenStartPos += tileXDiff;
                yOddStartPos += tileXDiff;*/


                if (y == 0)
                {
                    xTemp -= 0.6f;
                }

                if (y == 1)
                {
                    xTemp = x;
                    xTemp += 0.0f;
                }

                if (y == 2)
                {
                    xTemp = x;
                    xTemp -= 0.4f;
                }

                if (y == 3)
                {
                    xTemp = x;
                    xTemp += 0.2f;
                }

                if (y == 4)
                {
                    xTemp = x;
                    xTemp -= 0.2f;
                }

                if (y == 5)
                {
                    xTemp = x;
                    xTemp += 0.4f;
                }

                if (y == 6)
                {

                }

                if (y == 7)
                {
                    xTemp = x;
                    xTemp += 0.6f;
                }

                if (y == 8)
                {
                    xTemp = x;
                    xTemp += 0.2f;
                }

                if (y == 9)
                {
                    xTemp = x;
                    xTemp += 0.8f;
                }

                if (y == 10)
                {
                    xTemp = x;
                    xTemp += 0.4f;
                }

                if (y == 11)
                {
                    xTemp = x;
                    xTemp += 1f;
                }

                if (y == 12)
                {
                    xTemp = x;
                    xTemp += 0.6f;
                }

                if (y == 13)
                {
                    xTemp = x;
                    xTemp += 1.2f;
                }

                if (y == 14)
                {
                    xTemp = x;
                    xTemp += 0.8f;
                }

                if (y == 15)
                {
                    xTemp = x;
                    xTemp += 1.4f;
                }


                //Debug.Log("(" + x.ToString() + ") (" + y.ToString() + ")  " + xTemp.ToString());
                GameObject newTile = Instantiate(tile, new Vector3(xTemp, y / 1.6f, 1), tile.transform.rotation);
                newTile.GetComponent<TileScript>().typeIndex = Random.Range(0, 6);
                newTile.transform.parent = GameObject.Find("Tiles").transform;
                worldTiles.Add(newTile);

                if (y % 2 != 0)
                {
                    if (x == worldSizeX - 1)
                    {
                        newTile.tag = "P2BaseTile";
                    }

                    if (x == 0)
                    {
                        posSpawnP1.Add(newTile.transform.position);
                    }
                }
                else
                {
                    if (x == 0)
                    {
                        newTile.tag = "P1BaseTile";
                    }

                    if (x == worldSizeX - 1)
                    {
                        posSpawnP2.Add(newTile.transform.position);
                    }
                }
            }
        }      

        AddUnits(1, posSpawnP1, unitsRolesP1);
        AddUnits(2, posSpawnP2, unitsRolesP2);

        gameManager.mapCreated = true;
    }

   
    void AddUnits(int playerIndex, List<Vector3> positions, List<int> unitsRoles)
    {
        int unitIndex = 0;

        for (int i = 0; i < positions.Count; i++)
        {
            int roleIndex = unitsRoles[unitIndex];

            if (roleIndex == -1)
            {
                roleIndex = 0;
            }
            else if (roleIndex > 4)
            {
                roleIndex = 4;
            }

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

            if (playerIndex == 1)
            {
                newUnit.GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<UnitScript>().spritesP1[roleIndex];
            }
            else
            {
                newUnit.GetComponent<SpriteRenderer>().sprite = newUnit.GetComponent<UnitScript>().spritesP2[roleIndex];
            }

            if (i == positions.Count - 1)
            {
                newUnitScript.isKing = true;
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
