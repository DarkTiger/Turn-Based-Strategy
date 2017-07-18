using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public Stats stats;
    public Role role;
    //public UnitStats myStats;
    public int ownerIndex;
    public int roleIndex;
    public bool isSelected = false;
    public bool hasAttacked = false;
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
    TileScript tileScript;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        circleColliderGameobject = transform.GetChild(0).gameObject;
        movementDestination = transform.position;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        stats = GetComponent<Stats>();
        role =  GetComponent<Role>();

        UpdateUnitStat();
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

            if ((mouseX > positionInPixels.x - 24) && (mouseX < positionInPixels.x + 24) && (mouseY > positionInPixels.y - 24) && (mouseY < positionInPixels.y + 24))
            {
                GameObject[] units = GameObject.FindGameObjectsWithTag("UnitsP" + ownerIndex.ToString());

                foreach (GameObject unit in units)
                {
                    if (gameManagerScript.playerIndex == ownerIndex)
                    {
                        if (unit != gameObject)
                        {
                            unit.GetComponent<UnitScript>().isSelected = false;
                        }

                        if (currentMoveCount > 0)
                        {
                            isSelected = !isSelected;
                        }

                        circleColliderGameobject.SetActive(false);
                        circleColliderGameobject.SetActive(true);
                    }
                }
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
        Stats newStats;
        newStats = role.GetUnitRole(roleIndex);

        stats.attackRange = newStats.attackRange;
        stats.damage = newStats.damage;
        stats.movementRange = newStats.movementRange;
        stats.health = newStats.health;

        currentMoveCount = stats.movementRange;
    }


    void Movement()
    {
        if (transform.position != movementDestination)
        {
            float distance = Vector3.Distance(transform.position, movementDestination);
            transform.position = Vector3.Lerp(transform.position, movementDestination, 5 / distance * Time.deltaTime);
            unitIsMoving = true;
            circleColliderGameobject.SetActive(false);
        }
        else
        {
            unitIsMoving = false;        
            circleColliderGameobject.SetActive(true);
        }
    }


    public void Attack()
    {
        hasAttacked = true;
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


    void OnTriggerEnter2D(Collider2D other)
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
