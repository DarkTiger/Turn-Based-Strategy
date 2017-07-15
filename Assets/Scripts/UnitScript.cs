using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    Stats stats;
    public int ownerIndex;
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
    GameObject circleColliderGameobject;
    Camera cam;
    Vector3 positionInPixels;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        circleColliderGameobject = transform.GetChild(0).gameObject;
        movementDestination = transform.position;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }


    void Update()
    {
        Selection();
        Move();

        positionInPixels = cam.WorldToScreenPoint(transform.position);
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
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;

            if ((mouseX > positionInPixels.x - 16) && (mouseX < positionInPixels.x + 32) && (mouseY > positionInPixels.y - 16) && (mouseY < positionInPixels.y + 64))
            {
                GameObject[] units = GameObject.FindGameObjectsWithTag("UnitsP" + ownerIndex.ToString());

                foreach (GameObject unit in units)
                {
                    if (unit != gameObject)
                    {
                        unit.GetComponent<UnitScript>().isSelected = false;
                    }
                }

                if (currentMoveCount > 0)
                {
                    isSelected = !isSelected;
                }

                //circleCollider.enabled = false;
                //circleCollider.enabled = true;
                circleColliderGameobject.SetActive(false);
                circleColliderGameobject.SetActive(true);
            }
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Tile")
        {
            if (isSelected)
            {
                if (currentMoveCount > 0)
                {
                    other.gameObject.GetComponent<TileScript>().isInRange = true;
                }
                else
                {
                    isSelected = false;
                }
            }
            else
            {
                other.gameObject.GetComponent<TileScript>().isInRange = false;
            }
        }
    }
}
