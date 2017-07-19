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



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[typeIndex];
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
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
        }   
    }


    void Selection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isInRange = false;
            currentUnit = null;
            isTileTaken = false;
        }
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
                UnitScript unitScript = unit.GetComponent<UnitScript>();

                if (unitScript.isSelected && !unitScript.hasAttacked && isTileTaken && currentUnit != null)
                {
                    RaycastHit2D hit = Physics2D.Raycast(currentUnit.transform.position, unitScript.gameObject.transform.position);
                    TileScript TileScriptHit = hit.transform.gameObject.GetComponent<TileScript>();

                    if (hit.collider.tag == "Tile")
                    {
                        if (unitScript.gameObject != currentUnit.gameObject)
                        {
                            if (TileScriptHit.currentUnit.ownerIndex != unitScript.ownerIndex)
                            {
                                TileScriptHit.currentUnit.GetDamage(unitScript);
                                unitScript.hasAttacked = true;
                            }
                        }
                    }
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

                    break;
                }
                else if (!isInRange)
                {
                    unitScript.isSelected = false;
                }
            }
        } 
    }
    
    void SetTileBonus()
    {
        if (currentUnit != null)
        {
            if (typeIndex == 0 && currentUnit.roleIndex == 0)
            {
                currentUnit.bonusDefense = 3;
            }
            else if (typeIndex == 1 && currentUnit.roleIndex == 0)
            {
                currentUnit.bonusDefense = 3;
            }


        }
    }
}
