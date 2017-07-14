using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public int ownerIndex;
    Stats stats;
    public bool isSelected = false;
    bool hasAttacked = false;
    bool hasMoved = false;
    bool isKing = false;
    bool isStunned = false;
    bool isDead = false;
    SpriteRenderer spriteRenderer;
    public Color selectionColor;
    public Sprite[] spritesP1;
    public Sprite[] spritesP2;
    public int currentMoveCount;
    public Vector3 movementDestination;
    PolygonCollider2D circleCollider;


    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        movementDestination = transform.position;
    }


    void Update()
    {
        Selection();
        Move();
    }


    void Selection()
    {m
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
        if (transform.position != movementDestination)
        {
            transform.position = Vector3.Lerp(transform.position, movementDestination, 5 * Time.deltaTime);
        }
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
                if (unit != gameObject)
                {
                    unit.GetComponent<UnitScript>().isSelected = false;
                }
            }
            isSelected = !isSelected;

            circleCollider.enabled = false;
            circleCollider.enabled = true;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tile")
        {
            if (isSelected)
            {
                other.gameObject.GetComponent<TileScript>().isInRange = true;

                
            }
            else
            {
                other.gameObject.GetComponent<TileScript>().isInRange = false;
            }
        }
    }
}
