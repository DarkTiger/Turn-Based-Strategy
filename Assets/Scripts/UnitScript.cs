using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public int ownerIndex;
    Stats stats;
    bool isSelected = false;
    bool hasAttacked = false;
    bool hasMoved = false;
    bool isKing = false;
    bool isStunned = false;
    bool isDead = false;
    SpriteRenderer spriteRenderer;
    public Color selectionColor;
    public Sprite[] sprites;
    public Vector3 movementDestination;




    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Selection();
    }


    void Selection()
    {
        if (isSelected)
        {
            spriteRenderer.color = selectionColor;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
            

    public void UpdateUnitStat()
    {

    }


    void Move()
    {

    }


    void Attack()
    {

    }


    void UseAbility(Ability ability)
    {

    }


    void Death()
    {

    }


    void Respawn()
    {

    }


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject[] units = GameObject.FindGameObjectsWithTag("UnitsP" + ownerIndex.ToString());

            foreach (GameObject unit in units)
            {

                unit.GetComponent<UnitScript>().isSelected = false;
            }

            isSelected = !isSelected;
        }
    }
}
