﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;


public class UnitScript : MonoBehaviour
{
    public Stats stats;
    public Role role;
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

    public bool isStunned = false;
    public bool isInvulnerable = false;
    
    public int bonusAttack = 0;                 // Gestione dei bonus forniti dalle tiles ambientali
    public int bonusDefense = 0;

    SpriteRenderer spriteRenderer;          // Gestione delle sprite associate alle unità
    public Color selectionColor;
    public Color kingColor;
    public Sprite[] spritesP1;
    public Sprite[] spritesP2;

    public int currentMoveCount;            // Gestione del movimento
    public Vector3 movementDestination;
    public bool unitIsMoving = false;
    Vector3 positionInPixels;

    GameObject circleColliderGameobject; // Gestione dei collider circolari
    Outline outlineScript;



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

        positionInPixels = cam.WorldToScreenPoint(transform.position);
    }


    void Selection()
    {
        if (Input.GetMouseButtonDown(0) && !isDead && !isStunned)
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

        if (isSelected & !gameManagerScript.isGameOver)
        {            
            if (isKing)
            {
                spriteRenderer.color = kingColor;
            }
            else
            {
                spriteRenderer.color = selectionColor;
            }
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

        if (!attacker.isInvulnerable)
        {
            if (attackDistance <= attacker.stats.attackRange)
            {
                int tempDamage = attacker.stats.damage + bonusAttack;

                if (tempDamage > bonusDefense)
                {
                    tempDamage -= bonusDefense;
                    stats.health -= tempDamage;

                    if (stats.health <= 0)
                    {
                        Death(tile);
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


    void Death(TileScript tile) // Gestione morte eroi
    {
        isDead = true;
        spriteRenderer.enabled = false;
        enabled = false;
        tile.isTileTaken = false;
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

            attacker.hasAttacked = true;
            attacker.currentMoveCount = 0;
        }
    }

    // Abilità Specialist
    public void AbilityStun(UnitScript attacker)
    {
        float attackDistance = Mathf.Ceil(Vector2.Distance(transform.position, attacker.gameObject.transform.position));

        if (attackDistance <= 1)
        {
            isStunned = true;
        }
        attacker.hasAttacked = true;
        attacker.currentMoveCount = 0;
    }

    // Abilità Assassin
    public void AbilityInvisibility(UnitScript attacker)
    {
        attacker.isInvulnerable = true;

        attacker.hasAttacked = true;
        attacker.currentMoveCount = 0;
    }
}
