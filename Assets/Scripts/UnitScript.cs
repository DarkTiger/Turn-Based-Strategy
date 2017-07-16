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
    public bool unitIsMoving = false;
    PolygonCollider2D circleCollider;
    GameObject circleColliderGameobject;
    Camera cam;
    Vector3 positionInPixels;
    GameManagerScript gameManagerScript;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        circleColliderGameobject = transform.GetChild(0).gameObject;
        movementDestination = transform.position;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
    }


    void Update()
    {
        Selection();
        Movement();

        positionInPixels = cam.WorldToScreenPoint(transform.position);

    }


    void Selection()
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

                circleColliderGameobject.SetActive(false);
                circleColliderGameobject.SetActive(true);
            }
        }

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


    void Movement()
    {
        if (transform.position != movementDestination)
        {
            float distance = Vector3.Distance(transform.position, movementDestination);
            transform.position = Vector3.Lerp(transform.position, movementDestination, 5 / distance * Time.deltaTime);
            unitIsMoving = true;
        }
        else
        {
            unitIsMoving = false;
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


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Tile")
        {
            if (isSelected)
            {
                if (currentMoveCount > 0)
                {
                    if (!unitIsMoving)
                    {
                        other.gameObject.GetComponent<TileScript>().isInRange = true;
                    }
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


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Tile")
        {
            other.gameObject.GetComponent<TileScript>().isInRange = false;
        }
    }
}
