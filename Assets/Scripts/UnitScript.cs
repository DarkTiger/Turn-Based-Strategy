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
    public UnitAnimationScript unitAnimationScript;

    public int ownerIndex = 1;                      // Indica a quale player appartiene l'unità
    public int roleIndex = 0;                       // Indica la classe dell'unità
    public bool isSelected = false;                 // Segnala l'unità selezionata
    public bool hasAttacked = false;                // Indica se l'unità ha già attaccato nel proprio turno
    public bool hasMoved = false;                   // Indica se l'unità si è già mossa nel proprio turno
    public bool isKing = false;                     // Segnala l'unità re
    public bool isDead = false;
    public bool isAbilityUsed = false;
    public bool isAbilityInCooldown = false;

    public bool isStunned = false;
    public bool isInvulnerable = false;
    public bool isReadyToCounterAttack = false;                    // Contrattacco
    public bool isCrippled = false;

    public int bonusAttack = 0;                     // Gestione dei bonus forniti dalle tiles ambientali
    public int bonusDefense = 0;

    public SpriteRenderer spriteRenderer;           // Gestione delle sprite associate alle unità
    // public Color selectionColorP1;
    // public Color selectionColorP2;
    public Color selectionColor;
    public Color kingColor;
    public Sprite[] spritesP1;
    public Sprite[] spritesP2;

    public int currentMoveCount;                    // Gestione del movimento
    public Vector3 movementDestination;
    public bool unitIsMoving = false;
    Vector3 positionInPixels;

    GameObject circleColliderGameobject;            // Gestione dei collider circolari
    public Outline outlineScript;

    public int tempTurn = 0;
    public int tempTurnCounterAbility = 0;
    public int tempTurnStunAbility = 0;
    public int tempTurnCrippleAbility = 0;
    public int tempTurnInvisibilityAbility = 0;
    Image cooldownImage;

    AudioSource soundsAudioSource;
    public AudioClip[] abilitySoundEffects;         // Audio abilità
    AudioClip abilitySoundEffect;
    public AudioClip[] attackSoundEffects;          // Audio attacchi
    AudioClip attackSoundEffect;
    public AudioClip movementSoundEffect;                  // Audio movimento
    bool isMovementAudioPlaying = false;



    void Start()
    {
        stats = GetComponent<Stats>();
        role = GetComponent<Role>();
        unitAnimationScript = GetComponent<UnitAnimationScript>();
        spriteRenderer = transform.GetChild(8).GetComponent<SpriteRenderer>();
        movementDestination = transform.position;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        circleColliderGameobject = transform.GetChild(0).gameObject;
        outlineScript = transform.GetChild(8).GetComponent<Outline>();
        cooldownImage = transform.GetChild(5).GetComponent<Image>();
        soundsAudioSource = GameObject.Find("SoundsAudioSource").GetComponent<AudioSource>();

        try {outlineScript.color = ownerIndex - 1;}
        catch (System.Exception) {};

        UpdateUnitStat();
    }


    void Update()
    {
        Selection();
        Movement();
        CheckCooldown();

        positionInPixels = cam.WorldToScreenPoint(transform.position);

        CheckIfUnitCanAttack();
        SetOrderInLayer();
        //PlayMovementAudio();
    }


    void Selection()
    {
        if (!isDead && !isStunned)
        {
            if (Input.GetMouseButtonUp(0) /*&& Input.GetKey(KeyCode.LeftShift)*/)
            {
                if (!unitIsMoving)
                {
                    isSelected = false;
                    spriteRenderer.color = Color.white;
                    outlineScript.color = ownerIndex -1;
                }

                float mouseX = Input.mousePosition.x;
                float mouseY = Input.mousePosition.y;

                if ((mouseX > positionInPixels.x - 24) && (mouseX < positionInPixels.x + 24) && (mouseY > positionInPixels.y - 40) && (mouseY < positionInPixels.y + 8))
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
                                    spriteRenderer.color = selectionColor;
                                    gameManagerScript.currentSelectedUnit = this;
                                }
                                else
                                {
                                    spriteRenderer.color = Color.white;
                                    gameManagerScript.currentSelectedUnit = null;
                                }
                            }

                            circleColliderGameobject.SetActive(false);
                            circleColliderGameobject.SetActive(true);
                        }
                    }
                }
            }
        }

        try
        {
            if (gameManagerScript.playerIndex == ownerIndex && !hasAttacked && !isAbilityUsed)
            {
                outlineScript.enabled = true;
                //outlineScript.color = ownerIndex - 1;
            }
            else if (outlineScript.color != 2)
            {
                outlineScript.enabled = false;
            }
        }
        catch (System.Exception) { }
    }


    void PlayMovementAudio()
    {
        if (unitIsMoving && !soundsAudioSource.isPlaying)
        {
            soundsAudioSource.clip = movementSoundEffect;
            soundsAudioSource.Play();            
        }
    }

    
    public void CheckIfUnitCanAttack()
    {
        if (!hasAttacked && !isAbilityUsed && isSelected)
        {
            foreach (UnitScript unit in gameManagerScript.unitScriptList)
            {
                if (unit != this && unit.ownerIndex != ownerIndex)
                {
                    if (!hasAttacked && Vector2.Distance(transform.position, unit.transform.position) <= stats.attackRange)
                    {
                        unit.spriteRenderer.color = Color.red;
                    }

                    if (!isKing && !isAbilityUsed && !isAbilityInCooldown && Vector2.Distance(transform.position, unit.transform.position) <= stats.attackRange)
                    {
                        if (roleIndex != 0 && roleIndex != 1 && roleIndex != 3)
                        {
                            unit.outlineScript.color = 2;
                            unit.outlineScript.enabled = true;
                        }
                    }
                }
                else if (unit != this && unit.ownerIndex == ownerIndex)
                {
                    if (roleIndex == 3 && unit.stats.health < unit.stats.maxHealth || roleIndex == 5)
                    {
                        if (!isAbilityUsed && !isAbilityInCooldown && (Vector2.Distance(transform.position, unit.transform.position) <= stats.attackRange))
                        {
                            unit.outlineScript.color = 2;
                            unit.outlineScript.enabled = true;
                        }
                    }
                }
            }

            if (!isKing && !isAbilityUsed && !isAbilityInCooldown)
            {
                if (roleIndex == 0 || roleIndex == 1 || roleIndex == 3)
                {
                    if (!isReadyToCounterAttack && !isInvulnerable && roleIndex != 3 || (roleIndex == 3 && stats.health < stats.maxHealth))
                    {
                        outlineScript.color = 2;
                        outlineScript.enabled = true;
                    }
                }
            }
        }
    }


    public void UpdateUnitStat()
    {
        try
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
        catch (System.Exception) {}
    }


    void Movement()
    {
        if (transform.position != movementDestination)
        {
            float distance = Vector3.Distance(transform.position, movementDestination);
            transform.position = Vector3.Lerp(transform.position, movementDestination, 5 / distance * Time.deltaTime);
            circleColliderGameobject.SetActive(false);
            unitIsMoving = true;
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
        float attackDistance = Vector2.Distance(transform.position, attacker.gameObject.transform.position);

        /*if (attacker.roleIndex == 2)
        {*/
        Debug.Log(Vector2.Distance(transform.position, attacker.gameObject.transform.position).ToString());
        /*}*/

        if (!isInvulnerable)
        {
            if (attackDistance <= attacker.stats.attackRange)
            {
                int tempDamage = attacker.stats.damage + attacker.bonusAttack;

                if (tempDamage > bonusDefense)
                {
                    unitAnimationScript.PlayAttackAnimation(attacker.roleIndex, false, false);             // Gestisce l'animazione di attacco semplice in base alla classe dell'attacker

                    if (attacker.roleIndex == 0)
                    {
                        soundsAudioSource.clip = attackSoundEffects[0];
                    }
                    else if (attacker.roleIndex == 1)
                    {
                        soundsAudioSource.clip = attackSoundEffects[1];
                    }
                    else if (attacker.roleIndex == 2)
                    {
                        soundsAudioSource.clip = attackSoundEffects[2];
                    }
                    else if (attacker.roleIndex == 3)
                    {
                        soundsAudioSource.clip = attackSoundEffects[3];
                    }
                    else if (attacker.roleIndex == 4)
                    {
                        soundsAudioSource.clip = attackSoundEffects[4];
                    }
                    else if (attacker.roleIndex == 5)
                    {
                        soundsAudioSource.clip = attackSoundEffects[5];
                    }
                    else if (attacker.roleIndex == 6)
                    {
                        soundsAudioSource.clip = attackSoundEffects[6];
                    }
                    soundsAudioSource.Play();

                    tempDamage -= bonusDefense;
                    stats.health -= tempDamage;

                    if (isReadyToCounterAttack)
                    {
                        attacker.stats.health -= 3;

                        if (attacker.stats.health <= 0)
                        {
                            unitAnimationScript.PlayDeathAnimation();
                            attacker.Death();
                        }
                    }

                    if (stats.health <= 0)
                    {
                        unitAnimationScript.PlayDeathAnimation();
                        Death();
                    }

                }
                attacker.hasAttacked = true;
                attacker.currentMoveCount = 0;

                DeselectUnitsAfterAttack();
            }
        }
    }


    void DeselectUnitsAfterAttack()
    {
        foreach (UnitScript unit in gameManagerScript.unitScriptList)
        {
            unit.spriteRenderer.color = Color.white;
            unit.outlineScript.color = ownerIndex-1;
            unit.outlineScript.enabled = false;
            unit.isSelected = false;
        }
    }


    void Death() // Gestione morte eroi
    {
        isDead = true;
        enabled = false;
        spriteRenderer.enabled = false;
        gameObject.GetComponent<Canvas>().enabled = false;
        circleColliderGameobject.SetActive(false);

        if (isKing) // Gestione morte re e fine del gioco
        {
            if (ownerIndex == 1)
            {
                gameManagerScript.EndGamePlayer2();
            }
            else
            {
                gameManagerScript.EndGamePlayer1();
            }
        }

        unitAnimationScript.DisableStateAnimation();
    }


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
        float attackDistance = Vector2.Distance(transform.position, attacker.gameObject.transform.position);     

        if (attackDistance <= 1.1f)
        {
            if (stats.health != stats.maxHealth)
            {
                unitAnimationScript.PlayAttackAnimation(attacker.roleIndex, true, false);             // Gestisce l'animazione dell'abilità
                soundsAudioSource.clip = abilitySoundEffects[3];
                soundsAudioSource.Play();

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

                DeselectUnitsAfterAttack();
            }
        }
    }

    // Abilità Specialist
    public void AbilityStun(UnitScript attacker)
    {
        float attackDistance = Vector2.Distance(transform.position, attacker.gameObject.transform.position);

        if (attackDistance <= 1.1f)
        {
            unitAnimationScript.PlayAttackAnimation(attacker.roleIndex, true, true);             // Gestisce l'animazione dell'abilità
            soundsAudioSource.clip = abilitySoundEffects[4];
            soundsAudioSource.Play();

            isStunned = true;
            attacker.isAbilityUsed = true;
            attacker.isAbilityInCooldown = true;
            attacker.currentMoveCount = 0;
            attacker.tempTurn = gameManagerScript.turnIndex;
            tempTurnStunAbility = gameManagerScript.turnIndex;

            DeselectUnitsAfterAttack();
        }
    }

    // Abilità Assassin
    public void AbilityInvisibility(UnitScript attacker)
    {
        unitAnimationScript.PlayAttackAnimation(attacker.roleIndex, true, true);             // Gestisce l'animazione dell'abilità
        soundsAudioSource.clip = abilitySoundEffects[1];
        soundsAudioSource.Play();

        attacker.isInvulnerable = true;
        attacker.isAbilityUsed = true;
        attacker.isAbilityInCooldown = true;
        attacker.currentMoveCount = 0;
        attacker.tempTurn = gameManagerScript.turnIndex;
        tempTurnInvisibilityAbility = gameManagerScript.turnIndex;

        DeselectUnitsAfterAttack();
    }

    // Abilità Specialist 2
    public void AbilitySwap(UnitScript attacker, TileScript targetTile, TileScript attackerTile)
    {
        float attackDistance = Vector2.Distance(transform.position, attacker.gameObject.transform.position);
                
        if (attackDistance <= 1.1f)
        {            
            unitAnimationScript.PlayAttackAnimation(attacker.roleIndex, true, false);             // Gestisce l'animazione dell'abilità
            soundsAudioSource.clip = abilitySoundEffects[5];
            soundsAudioSource.Play();

            Vector3 temp = movementDestination;
            movementDestination = attacker.movementDestination;
            attacker.movementDestination = temp;

            targetTile.currentUnit = attackerTile.currentUnit;
            attackerTile.currentUnit = this;

            attacker.isAbilityUsed = true;
            attacker.isAbilityInCooldown = true;
            attacker.currentMoveCount = 0;
            attacker.tempTurn = gameManagerScript.turnIndex;

            DeselectUnitsAfterAttack();
        }
    }
    
    // Abilità Tank
    public void AbilityRetaliation(UnitScript attacker)
    {
        unitAnimationScript.PlayAttackAnimation(attacker.roleIndex, true, true);             // Gestisce l'animazione dell'abilità
        soundsAudioSource.clip = abilitySoundEffects[0];
        soundsAudioSource.Play();

        attacker.isReadyToCounterAttack = true;
        attacker.isAbilityUsed = true;
        attacker.isAbilityInCooldown = true;
        attacker.currentMoveCount = 0;
        attacker.tempTurn = gameManagerScript.turnIndex;
        tempTurnCounterAbility = gameManagerScript.turnIndex;

        DeselectUnitsAfterAttack();
    }

    // Abilità Ranged
    public void AbilityCripple(UnitScript attacker)
    {
        float attackDistance = Vector2.Distance(transform.position, attacker.gameObject.transform.position);

        if (attackDistance <= 3.1f)
        {
            unitAnimationScript.PlayAttackAnimation(attacker.roleIndex, true, false);             // Gestisce l'animazione dell'abilità
            soundsAudioSource.clip = abilitySoundEffects[2];
            soundsAudioSource.Play();

            isCrippled = true;
            attacker.isAbilityUsed = true;
            attacker.isAbilityInCooldown = true;
            attacker.currentMoveCount = 0;
            attacker.tempTurn = gameManagerScript.turnIndex;
            tempTurnCrippleAbility = gameManagerScript.turnIndex;

            DeselectUnitsAfterAttack();
        }
    }


    public void CheckCooldown()
    {
        if (gameManagerScript.turnIndex == tempTurn + 3)
        {
            isAbilityInCooldown = false;
            tempTurn = 0;

            if (roleIndex == 0 || roleIndex == 1)
            {
                unitAnimationScript.DisableStateAnimation();
            }
        }


        if ((gameManagerScript.turnIndex == tempTurnCounterAbility + 3) || (gameManagerScript.turnIndex == tempTurnCrippleAbility + 3) ||
         (gameManagerScript.turnIndex == tempTurnInvisibilityAbility + 3) || (gameManagerScript.turnIndex == tempTurnStunAbility + 3))
        {
            unitAnimationScript.DisableStateAnimation();
            tempTurnCounterAbility = 0;
            tempTurnCrippleAbility = 0;
            tempTurnInvisibilityAbility = 0;
            tempTurnStunAbility = 0;
        }

        cooldownImage.enabled = isAbilityInCooldown;
    }

    void SetOrderInLayer()
    {
        if (isSelected)
        {
            spriteRenderer.sortingOrder = 0;
        }
        else
        {
            if (transform.position.y >= 0 && transform.position.y <= 0.625)
            {
                spriteRenderer.sortingOrder = -1;
            }

            if (transform.position.y >= 0.626 && transform.position.y <= 1.25)
            {
                spriteRenderer.sortingOrder = -2;
            }

            if (transform.position.y >= 1.26 && transform.position.y <= 1.875)
            {
                spriteRenderer.sortingOrder = -3;
            }

            if (transform.position.y >= 1.876 && transform.position.y <= 2.5)
            {
                spriteRenderer.sortingOrder = -4;
            }

            if (transform.position.y >= 2.6 && transform.position.y <= 3.125)
            {
                spriteRenderer.sortingOrder = -5;
            }

            if (transform.position.y >= 3.126 && transform.position.y <= 3.75)
            {
                spriteRenderer.sortingOrder = -6;
            }

            if (transform.position.y >= 3.76 && transform.position.y <= 4.375)
            {
                spriteRenderer.sortingOrder = -7;
            }

            if (transform.position.y >= 4.376 && transform.position.y <= 5)
            {
                spriteRenderer.sortingOrder = -8;
            }

            if (transform.position.y >= 5.1 && transform.position.y <= 5.625)
            {
                spriteRenderer.sortingOrder = -9;
            }

            if (transform.position.y >= 5.626 && transform.position.y <= 6.25)
            {
                spriteRenderer.sortingOrder = -10;
            }

            if (transform.position.y >= 6.26 && transform.position.y <= 6.875)
            {
                spriteRenderer.sortingOrder = -11;
            }

            if (transform.position.y >= 6.876 && transform.position.y <= 7.5)
            {
                spriteRenderer.sortingOrder = -12;
            }
        }
    }
}
