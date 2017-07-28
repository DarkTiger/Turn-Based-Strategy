using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Outline = cakeslice.Outline;
using UnityEngine.UI;


public class UnitScript : MonoBehaviour
{
    public Stats stats;
    public Role role;
    public Ability ability;
    Camera cam;
    GameManagerScript gameManagerScript;
    TileScript tileScript;

    public int ownerIndex = 1;                  // Indica a quale player appartiene l'unità
    public int roleIndex = 0;                   // Indica la classe dell'unità
    public bool isSelected = false;         // Segnala l'unità selezionata
    public bool hasAttacked = false;        // Indica se l'unità ha già attaccato nel proprio turno
    public bool hasMoved = false;                  // Indica se l'unità si è già mossa nel proprio turno
    public bool isKing = false;                    // Segnala l'unità re
    public bool isDead = false;
    public bool isAbilityUsed = false;
    public bool isAbilityInCooldown = false;

    public bool isStunned = false;
    public bool isInvulnerable = false;
    public bool isReady = false;                // Contrattacco
    public bool isCrippled = false;
    
    public int bonusAttack = 0;                 // Gestione dei bonus forniti dalle tiles ambientali
    public int bonusDefense = 0;

    SpriteRenderer spriteRenderer;          // Gestione delle sprite associate alle unità
    public Color selectionColorP1;
    public Color selectionColorP2;
    public Color kingColor;
    public Sprite[] spritesP1;
    public Sprite[] spritesP2;

    public int currentMoveCount;            // Gestione del movimento
    public Vector3 movementDestination;
    public bool unitIsMoving = false;
    Vector3 positionInPixels;

    GameObject circleColliderGameobject; // Gestione dei collider circolari
    Outline outlineScript;

    public int tempTurn = 0;
    Image cooldownImage;



    void Start()
    {
        stats = GetComponent<Stats>();
        role = GetComponent<Role>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        movementDestination = transform.position;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        circleColliderGameobject = transform.GetChild(0).gameObject;
        outlineScript = GetComponent<Outline>();
        cooldownImage = transform.GetChild(5).GetComponent<Image>();

        try
        {
            outlineScript.color = ownerIndex - 1;
        }
        catch (System.Exception) { };

        KingSelection();
        UpdateUnitStat();
    }


    void Update()
    {
        Selection();
        Movement();
        CheckCooldown();

        positionInPixels = cam.WorldToScreenPoint(transform.position);
    }


    void Selection()
    {
        if ((Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift)) && !isDead && !isStunned)
        {
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;

            if ((mouseX > positionInPixels.x - 24) && (mouseX < positionInPixels.x + 24) && (mouseY > positionInPixels.y - 40) && (mouseY < positionInPixels.y + 12))
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

                        if (currentMoveCount > 0 || !hasAttacked)
                        {
                            isSelected = !isSelected;

                            if (isSelected)
                            {
                                gameManagerScript.currentSelectedUnit = GetComponent<UnitScript>();
                            }
                            else
                            {
                                gameManagerScript.currentSelectedUnit = null;
                            }
                        }

                        circleColliderGameobject.SetActive(false);
                        circleColliderGameobject.SetActive(true);
                    }
                }
            }
        }

        if (isSelected && !gameManagerScript.isGameOver && currentMoveCount > 0)
        {            
            /*if (isKing)
            {
                spriteRenderer.color = kingColor;
            }
            else
            {*/
                if (ownerIndex == 1)
                {
                    spriteRenderer.color = selectionColorP1;
                }
                else
                {
                    spriteRenderer.color = selectionColorP2;
                }  
            //}
        }
        else
        {

            spriteRenderer.color = Color.white;
        }


        try
        {
            if (gameManagerScript.playerIndex == ownerIndex)
            {
                outlineScript.enabled = true;
            }
            else
            {
                outlineScript.enabled = false;
            }
        }
        catch (System.Exception) { }
    }

    
    public void KingSelection()
    {
        //ASSEGNATO IN MAP GENERATOR/
    }


    public void UpdateUnitStat()
    {
        try  //RISOLTA QUESTA PALLA
        {
            Stats newStats;
            newStats = role.GetUnitRole(roleIndex);

            stats.attackRange = newStats.attackRange;
            stats.damage = newStats.damage;
            stats.movementRange = newStats.movementRange;
            stats.health = newStats.health;
            stats.maxHealth = newStats.maxHealth;
            currentMoveCount = stats.movementRange;

            ability = role.GetUnitAbility(roleIndex);
        }
        catch (System.Exception) { }
    }


    void Movement()
    {
        if (transform.position != movementDestination)
        {
            float distance = Vector3.Distance(transform.position, movementDestination);
            transform.position = Vector3.Lerp(transform.position, movementDestination, 5 / distance * Time.deltaTime);
            unitIsMoving = true;
            circleColliderGameobject.SetActive(false);
            ForceUnitNull();
        }
        else
        {
            unitIsMoving = false;        
            circleColliderGameobject.SetActive(true);
        }
    }


    public void GetDamage(UnitScript attacker, TileScript tile) // Gestione dell'attacco
    {
        float attackDistance = Mathf.Ceil(Vector2.Distance(transform.position, attacker.gameObject.transform.position));

        if (!isInvulnerable)
        {
            if (attackDistance <= attacker.stats.attackRange)
            {
                int tempDamage = attacker.stats.damage + attacker.bonusAttack;

                if (tempDamage > bonusDefense)
                {
                    tempDamage -= bonusDefense;
                    stats.health -= tempDamage;

                    if (isReady)
                    {
                        attacker.stats.health -= 3;

                        if (attacker.stats.health <= 0)
                        {
                            attacker.Death();
                        }
                    }

                    if (stats.health <= 0)
                    {
                        Death();
                    }

                }
                attacker.hasAttacked = true;
                attacker.currentMoveCount = 0;
            }
        }
    }


    void UseAbility(Ability ability)
    {

    }


    void Death() // Gestione morte eroi
    {
        isDead = true;
        spriteRenderer.enabled = false;
        enabled = false;
        gameObject.GetComponent<Canvas>().enabled = false;
        circleColliderGameobject.SetActive(false);

        if (isKing) // Gestione morte re e fine del gioco
        {
            gameManagerScript.EndGame();
        }
    }


    //void Respawn()
    //{

    //}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tile" || other.tag == "P1BaseTile" || other.tag == "P2BaseTile")
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
                else if (hasAttacked)
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
        if (other.tag == "Tile" || other.tag == "P1BaseTile" || other.tag == "P2BaseTile")
        {
            other.gameObject.GetComponent<TileScript>().isInRange = false;
        }
    }

    void ForceUnitNull()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            foreach (TileScript tile in gameManagerScript.tileScriptList)
            {
                tile.currentUnit = null;
            }
        }
    }

    // Abilità Healer
    public void AbilityCure(UnitScript attacker)
    {
        float attackDistance = Mathf.Ceil(Vector2.Distance(transform.position, attacker.gameObject.transform.position));
        

        if (attackDistance <= 1)
        {
            int tempHealth = stats.health + 5;

            if (tempHealth > stats.maxHealth)
            {
                stats.health = stats.maxHealth;
            }
            else
            {
                stats.health += 5;
            }
            
            attacker.isAbilityUsed = true;
            attacker.isAbilityInCooldown = true;
            attacker.currentMoveCount = 0;
            attacker.tempTurn = gameManagerScript.turnIndex;
        }
    }

    // Abilità Specialist
    public void AbilityStun(UnitScript attacker)
    {
        float attackDistance = Mathf.Ceil(Vector2.Distance(transform.position, attacker.gameObject.transform.position));

        if (attackDistance <= 1)
        {
            isStunned = true;
            attacker.isAbilityUsed = true;
            attacker.isAbilityInCooldown = true;
            attacker.currentMoveCount = 0;
            attacker.tempTurn = gameManagerScript.turnIndex;
        }
    }

    // Abilità Assassin
    public void AbilityInvisibility(UnitScript attacker)
    {
        attacker.isInvulnerable = true;

        attacker.isAbilityUsed = true;
        attacker.isAbilityInCooldown = true;
        attacker.currentMoveCount = 0;
        attacker.tempTurn = gameManagerScript.turnIndex;
    }

    // Abilità Specialist 2
    public void AbilitySwap(UnitScript attacker, TileScript targetTile, TileScript attackerTile)
    {
        float attackDistance = Mathf.Ceil(Vector2.Distance(transform.position, attacker.gameObject.transform.position));
                
        if (attackDistance <= 1)
        {            
            Vector3 temp = movementDestination;
            movementDestination = attacker.movementDestination;
            attacker.movementDestination = temp;

            targetTile.currentUnit = attackerTile.currentUnit;
            attackerTile.currentUnit = GetComponent<UnitScript>();

            attacker.isAbilityUsed = true;
            attacker.isAbilityInCooldown = true;
            attacker.currentMoveCount = 0;
            attacker.tempTurn = gameManagerScript.turnIndex;
        }
    }
    
    // Abilità Tank
    public void AbilityRetaliation(UnitScript attacker)
    {
        attacker.isReady = true;

        attacker.isAbilityUsed = true;
        attacker.isAbilityInCooldown = true;
        attacker.currentMoveCount = 0;
        attacker.tempTurn = gameManagerScript.turnIndex;
    }

    // Abilità Ranged
    public void AbilityCripple(UnitScript attacker)
    {
        float attackDistance = Mathf.Ceil(Vector2.Distance(transform.position, attacker.gameObject.transform.position));

        if (attackDistance <= 3)
        {
            isCrippled = true;
            // currentMoveCount = 1;

            attacker.isAbilityUsed = true;
            attacker.isAbilityInCooldown = true;
            attacker.currentMoveCount = 0;
            attacker.tempTurn = gameManagerScript.turnIndex;
        }
    }

    public void CheckCooldown()
    {
        if (gameManagerScript.turnIndex == tempTurn + 4)
        {
            isAbilityInCooldown = false;
            tempTurn = 0;
        }

        cooldownImage.enabled = isAbilityInCooldown;
    }
}
