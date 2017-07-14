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


        } 
    }


    void OnMouseExit()
    {
        isSelected = false;
    }
}
