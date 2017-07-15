using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    bool isSelected = false;
    public bool isInRange = false;
    public int typeIndex = 0; //0 = grass; 1 = desert; 2 = forest; 3 = lake; 4 = mountain
    public Stats stats;
    public UnitScript currentUnit;
    public Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    public Color selectionColor;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[typeIndex];
    }


    void Update()
    {
        Selection();
        OnInRange();
    }


    void Selection()
    {
        /*if (isSelected)
        {
            spriteRenderer.color = selectionColor;
        }
        else
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
        else
        {
            spriteRenderer.color = Color.white;
        }
    }


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSelected = true;
            
            GameObject[] unitsP1 = GameObject.FindGameObjectsWithTag("UnitsP1");
            GameObject[] unitsP2 = GameObject.FindGameObjectsWithTag("UnitsP2");

            List<GameObject> unitsList = new List<GameObject>();

            for (int i = 0; i < unitsP1.Length; i++)
            {
                unitsList.Add(unitsP1[i]);
            }

            for (int i = 0; i < unitsP2.Length; i++)
            {
                unitsList.Add(unitsP2[i]);
            }

            foreach (GameObject unit in unitsList)
            {
                UnitScript unitScript = unit.GetComponent<UnitScript>();

                if (unitScript.isSelected && unitScript.currentMoveCount > 0 && isInRange )
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
            }
        } 
    }


    void OnMouseExit()
    {
        isSelected = false;
    }
}
