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
        Selection();
        SetTileTaken();        
        OnInRange();
        SetTileBonus();
        SetGameOver();
    }


    void SetTileTaken()
    {
        foreach (GameObject unit in gameManager.unitsList)
        {
            if (!unit.GetComponent<UnitScript>().isDead)
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
                    TileScript tileScriptHit = hit.transform.gameObject.GetComponent<TileScript>();

                    if (hit.collider.tag == "Tile" || hit.collider.tag == "P1BaseTile" || hit.collider.tag == "P2BaseTile")
                    {
                        if (unitScript.gameObject != currentUnit.gameObject)
                        {
                            if (tileScriptHit.currentUnit.ownerIndex != unitScript.ownerIndex)
                            {
                                tileScriptHit.currentUnit.GetDamage(unitScript, tileScriptHit);
                            }
                        }
                    }
                }
                else if (unitScript.isSelected && unitScript.currentMoveCount > 0 && isInRange)
                {
                    foreach (GameObject tile in gameManager.tilesList)
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
            if (typeIndex == 0 && currentUnit.roleIndex == 0) // Tank
            {
                currentUnit.bonusDefense = 3;
            }
            else if (typeIndex == 1 && currentUnit.roleIndex == 1) // Assassin
            {
                currentUnit.bonusAttack = 1;
            }
            else if (typeIndex == 2 && currentUnit.roleIndex == 2) // Ranged
            {
                currentUnit.bonusAttack = 3;
            }
            else if (typeIndex == 3 && currentUnit.roleIndex == 3) // Healer
            {
                currentUnit.bonusDefense = 3;
            }
            else if (typeIndex == 4 && currentUnit.roleIndex == 4) // Specialist
            {
                currentUnit.bonusAttack = 2;
                currentUnit.bonusDefense = 1;
            }
            else
            {
                currentUnit.bonusAttack = 0;
                currentUnit.bonusDefense = 0;
            }
        }
    }

    void SetGameOver()
    {
        if (currentUnit != null && !gameManager.isGameOver)
        {
            if (currentUnit.isKing)
            {
                if (tag == "P1BaseTile" || tag == "P2BaseTile")
                {
                    gameManager.EndGame();
                }
            }
        }
    }
}
