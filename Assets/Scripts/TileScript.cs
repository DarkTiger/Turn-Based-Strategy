using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    bool isSelected = false;
    public bool isTileTaken = false;
    public bool isInRange = false;
    public int typeIndex = 0;
    public Stats stats;
    public UnitScript currentUnit = null;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    public Color selectionColor;
    GameManagerScript gameManager;
    MapGenerator mapGenerator;
    Camera cam;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[typeIndex];
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        //mapGenerator = GameObject.Find("Map").GetComponent<MapGenerator>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }


    void Update()
    {
        SetTileTaken();
        Selection();
        OnInRange();
    }


    void SetTileTaken()
    {
        foreach (GameObject unit in gameManager.unitsList)
        {
            Vector3 unitPos = unit.transform.position;
            unitPos.y -= 0.3f;
            unitPos.z = 1;

            if (unitPos == transform.position)
            {
                isInRange = false;
                isTileTaken = true;

                currentUnit = unit.GetComponent<UnitScript>();
            }
            /*else
            {
                isTileTaken = false;
                currentUnit = null;
            }*/
        }   
        
            /*if (unitsList.transform.position.x == transform.position.x) //&& unitsList.transform.position.y + 0.3f == transform.position.y)
            {
                //Debug.Log("X occupata");
                if (unitsList.transform.position.y -0.3f == transform.position.y)
                {
                    Debug.Log("Y occupata");
                }
            }*/
               //Debug.Log(unitsList.transform.position.x);  
            
              //if (unitsList.transform.position.x == transform.position.x &&
                //    (unitsList.transform.position.y + 0.3f) == transform.position.y)
                //{
                  //  Debug.Log(unitsList.transform.position.x);
                   // Debug.Log(transform.position.x);
                
                    //isTileTaken = true;
              //}
       //}
    }


    void Selection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isInRange = false;
            currentUnit = null;

            foreach (GameObject unit in gameManager.unitsList)
            {
                UnitScript unitScript = unit.GetComponent<UnitScript>();

                if (unitScript.isSelected && !unitScript.hasAttacked && isTileTaken)
                {
                    RaycastHit2D hit = Physics2D.Raycast(unit.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));//transform.position);

                    if (hit.collider.tag == "Tile")
                    {
                        if (hit.collider.gameObject != gameObject)
                        {
                            TileScript TileScriptHit = new TileScript();
                            TileScriptHit = hit.collider.gameObject.GetComponent<TileScript>();

                            if (TileScriptHit.currentUnit != null)
                            {
                                if (TileScriptHit.currentUnit.ownerIndex != unitScript.ownerIndex)
                                {
                                    Debug.Log("HERE 2");
                                }
                            }
                        }
                    }
                }
            }

            isTileTaken = false;
        }

        if (isTileTaken)//(isSelected)
        {
            //spriteRenderer.color = selectionColor;
        }
        /*else
        {
            spriteRenderer.color = Color.white;
        }*/
    }


    void OnInRange()
    {
        if (isInRange)
        {
            spriteRenderer.color = selectionColor;
        }
        else if (!isTileTaken)
        {
            spriteRenderer.color = Color.white;
        }
    }


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSelected = true;
            
            foreach (GameObject unit in gameManager.unitsList)
            {
                //Debug.Log("No");
                UnitScript unitScript = unit.GetComponent<UnitScript>();

                if (unitScript.isSelected && !unitScript.hasAttacked && isTileTaken)
                {
                    /*Debug.Log("HERE 1");
                    RaycastHit2D hit = Physics2D.Raycast(unit.transform.position, transform.position);
                    TileScript TileScriptHit = hit.transform.gameObject.GetComponent<TileScript>();

                    if (TileScriptHit.currentUnit.ownerIndex != unitScript.ownerIndex)
                    {
                        Debug.Log("HERE 2");
                    }*/
                }
                else if (unitScript.isSelected && unitScript.currentMoveCount > 0 && isInRange)
                {
                    GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
                    foreach (GameObject tile in tiles)
                    {
                        tile.GetComponent<TileScript>().isInRange = false;
                    }

                    Vector3 movementDestinationTemp = transform.position;
                    movementDestinationTemp.y += 0.3f;
                    movementDestinationTemp.z = -1;

                    unitScript.movementDestination = movementDestinationTemp;
                    unitScript.currentMoveCount -= 1;

                    //isTileTaken = false;

                    break;
                }
                else if (!isInRange/* || isTileTaken*/)
                {
                    //isTileTaken = false;
                    unitScript.isSelected = false;
                }
            }
        } 
    }
}
