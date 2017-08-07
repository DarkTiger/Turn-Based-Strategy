using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Stats stats;             
    public UnitScript currentUnit = null;
    GameManagerScript gameManager;
    MapGenerator mapGenerator;

    bool isSelected = false;            // Selezione tile
    public bool isTileTaken = false;    // Indica se la tile è occupata
    public bool isInRange = false;      // Indica se la tile è in range
    public int typeIndex = 0;           // Tipo di tile (foresta, montagna...)
    
    public Sprite[] sprites;            // Gestione sprite
    public Sprite[] movementPreviewSprites; // Sprite movimento
    public SpriteRenderer spriteRenderer;
    public Color selectionColor;
    public Color p1Color;
    public Color p2Color;
    public int activationTurnIndex = -1;
    //public SpriteRenderer bannerSpriteRenderer;

    AudioSource soundsAudioSource;
    public AudioClip movementSoundEffect;
    


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[typeIndex];
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        soundsAudioSource = GameObject.Find("SoundsAudioSource").GetComponent<AudioSource>();
        //bannerSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Selection();
        SetTileTaken();        
        OnInRange();
        SetTileBonus();
        SetGameOver();
    }


    void SetTileTaken()
    {
        foreach (UnitScript unit in gameManager.unitScriptList)
        {
            if (!unit.isDead)
            {
                Vector3 unitPos = unit.transform.position;
                unitPos.y -= 0.3f;
                unitPos.z = 1;

                if (unitPos == transform.position || (currentUnit != null && currentUnit.unitIsMoving))
                {
                    isInRange = false;
                    isTileTaken = true;
                    currentUnit = unit;
                    spriteRenderer.color = Color.white;
                }
            }
        }
        
        if (currentUnit != null && currentUnit.isDead)
        {
            isTileTaken = false;
            currentUnit = null;
        }
    }


    void Selection()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            isInRange = false;
            currentUnit = null;

            if (activationTurnIndex == -1)
            {
                isTileTaken = false;
            }
        }
    }


    void OnInRange()
    {
        if (!gameManager.isGameOver)
        {
            if (isInRange)
            {
                //spriteRenderer.sprite = movementPreviewSprites[typeIndex];
                spriteRenderer.color = selectionColor;
            }
            else if (!isTileTaken)
            {
                //spriteRenderer.sprite = sprites[typeIndex]; 
                /*if (gameObject.tag == "P1BaseTile")
                {
                    spriteRenderer.color = p1Color;
                }
                else if (gameObject.tag == "P2BaseTile")
                {
                    spriteRenderer.color = p2Color;
                }
                else*/
                //{
                    spriteRenderer.color = Color.white;
                //}
            }
            else if (isTileTaken)
            {
                if (currentUnit != null && currentUnit.isKing)
                {
                    if (currentUnit.ownerIndex == 1)
                    {
                        spriteRenderer.color = p1Color;
                    }
                    else
                    {
                        spriteRenderer.color = p2Color;
                    }
                }
                else
                {
                    spriteRenderer.color = Color.white;
                }
            }
        }
    }


    void OnMouseOver()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !gameManager.isGameOver)
        {
            isSelected = true;
            
            foreach (UnitScript unitScript in gameManager.unitScriptList)
            {
                if (unitScript.isSelected && !unitScript.hasAttacked && !unitScript.isAbilityUsed && currentUnit != null && isTileTaken)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(currentUnit.transform.position, unitScript.gameObject.transform.position);

                    foreach (RaycastHit2D hit in hits)
                    {
                        TileScript tileScriptHit = null;

                        if (hit.collider.tag == "Tile" || hit.collider.tag == "P1BaseTile" || hit.collider.tag == "P2BaseTile")
                        {
                            tileScriptHit = hit.collider.gameObject.GetComponent<TileScript>();

                            if (tileScriptHit.currentUnit != null) // Controllo sull'unità
                            {
                                if (unitScript.gameObject != currentUnit.gameObject)
                                {
                                    if ((Input.GetMouseButtonDown(0) /*&& Input.GetKey(KeyCode.A)*/))
                                    {
                                        if (tileScriptHit.currentUnit.ownerIndex != unitScript.ownerIndex)
                                        {
                                            StartCoroutine(tileScriptHit.currentUnit.GetDamage(unitScript, tileScriptHit));
                                            break;
                                        }
                                    }
                                    else if (Input.GetMouseButtonDown(1))
                                    {
                                        if (unitScript.roleIndex == 5 && !unitScript.isAbilityInCooldown)
                                        {
                                            StartCoroutine(tileScriptHit.currentUnit.AbilitySwap(unitScript, tileScriptHit, GetComponent<TileScript>()));
                                        }
                                        else if (tileScriptHit.currentUnit.ownerIndex != unitScript.ownerIndex) // Avversario
                                        {
                                            if (unitScript.roleIndex == 4 && !unitScript.isAbilityInCooldown)
                                            {
                                                StartCoroutine(tileScriptHit.currentUnit.AbilityStun(unitScript));
                                            }
                                            else if (unitScript.roleIndex == 2 && !unitScript.isAbilityInCooldown)
                                            {
                                                StartCoroutine(tileScriptHit.currentUnit.AbilityCripple(unitScript));
                                            }
                                        }
                                        else if (tileScriptHit.currentUnit.ownerIndex == unitScript.ownerIndex) // Alleato
                                        {                                            
                                            if (unitScript.roleIndex == 3 && !unitScript.isAbilityInCooldown)
                                            {
                                                StartCoroutine(tileScriptHit.currentUnit.AbilityCure(unitScript));
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (Input.GetMouseButtonDown(1))
                                    {                                        
                                        if (unitScript.roleIndex == 1 && !unitScript.isAbilityInCooldown)
                                        {                                            
                                            StartCoroutine(currentUnit.AbilityInvisibility(unitScript));
                                        }
                                        else if (unitScript.roleIndex == 0 && !unitScript.isAbilityInCooldown)
                                        {
                                            StartCoroutine(currentUnit.AbilityRetaliation(unitScript));
                                        }
                                        else if (unitScript.roleIndex == 3 && !unitScript.isAbilityInCooldown)
                                        {
                                            StartCoroutine(currentUnit.AbilityCure(unitScript));
                                        }
                                    }
                                }
                            }                           
                        }
                    }
                }
                else if (unitScript.isSelected && unitScript.currentMoveCount > 0 && isInRange && !Input.GetMouseButtonDown(1))
                {
                    foreach (TileScript tile in gameManager.tileScriptList)
                    {
                        tile.isInRange = false;
                    }

                    if (!soundsAudioSource.isPlaying)
                    {
                        soundsAudioSource.clip = movementSoundEffect;
                        soundsAudioSource.Play();
                    }

                    Vector3 movementDestinationTemp = transform.position;
                    movementDestinationTemp.y += 0.3f;
                    movementDestinationTemp.z = -1;

                    unitScript.movementDestination = movementDestinationTemp;
                    unitScript.currentMoveCount -= 1;

                    isTileTaken = false;
                    currentUnit = null;
                    spriteRenderer.color = Color.white;

                    break;
                }
                /*else if (!isInRange)
                {
                    unitScript.isSelected = false;
                }*/
            }
        } 
    }

    
    void SetTileBonus()
    {
        if (currentUnit != null)
        {           
            if (typeIndex == 0 && currentUnit.roleIndex == 0) // Tank
            {
                currentUnit.bonusAttack = 1;
                currentUnit.bonusDefense = 2;
                
            }
            else if (typeIndex == 1 && currentUnit.roleIndex == 1) // Assassin
            {
                currentUnit.bonusAttack = 1;
                currentUnit.bonusDefense = 2;
            }
            else if (typeIndex == 2 && currentUnit.roleIndex == 2) // Ranged
            {
                currentUnit.bonusAttack = 2;
                currentUnit.bonusDefense = 1;
            }
            else if (typeIndex == 3 && currentUnit.roleIndex == 3) // Healer
            {
                currentUnit.bonusDefense = 3;
            }
            else if (typeIndex == 4 && currentUnit.roleIndex == 4) // Specialist
            {
                currentUnit.bonusAttack = 2;
                currentUnit.bonusDefense = 1;
            }
            else if (typeIndex == 4 && currentUnit.roleIndex == 5) // Specialist
            {
                currentUnit.bonusAttack = 2;
                currentUnit.bonusDefense = 1;
            }
            else
            {
                currentUnit.bonusAttack = 0;
                currentUnit.bonusDefense = 0;
            }
        }
    }


    void SetGameOver()
    {
        if (currentUnit != null && !gameManager.isGameOver)
        {
            if (currentUnit.isKing)
            {
                if ((tag == "P1BaseTile" && currentUnit.ownerIndex == 2) || (tag == "P2BaseTile" && currentUnit.ownerIndex == 1))
                {
                    gameManager.EndGame();
                }
            }
        }
    }
}
