﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerScript : MonoBehaviour
{
    TeamMenuScript teamMenuScript;

    public int playerIndex;                 // Indica a quale giocatore appartiene l'unità
    public int turnIndex = 1;

    public int turnDuration;                // Gestione del tempo
    public int gameTime;

    [HideInInspector]
    public List<GameObject> unitsList;      // Variabili di gestione unità
    [HideInInspector]
    public List<GameObject> tilesList;
    [HideInInspector]
    public List<UnitScript> unitScriptList;
    [HideInInspector]
    public List<TileScript> tileScriptList;
    [HideInInspector]
    public UnitScript currentSelectedUnit;
    [HideInInspector]
    public bool mapCreated = false;
    [HideInInspector]
    public bool isGameOver = false;         // Gestione del game over

    bool unitsLoadedInList = false;
    bool tilesLoadedInList = false;
    bool tacticalModeEnabled = false;
    bool helpModeEnabled = false;

    Text turnIndexText;
    Text winnerText;
    Text movementCountText;
    Text abilityText;
    Text attackText;

    public Sprite[] hideImages;
    Image hideButtonImage;

    public Sprite[] tutorialImages;
    Image tutorialScreen;

    GameObject winnerPanel;
    GameObject helpPanel;
    GameObject rematchPanel;



    void Start()
    {
        unitsList = new List<GameObject>();
        tilesList = new List<GameObject>();
        unitScriptList = new List<UnitScript>();
        tileScriptList = new List<TileScript>();

        teamMenuScript = GameObject.Find("TeamMenuPanel").GetComponent<TeamMenuScript>();

        winnerPanel = GameObject.Find("WinnerPanel");
        winnerText = GameObject.Find("WinnerText").GetComponent<Text>();
        helpPanel = GameObject.Find("HelpPanel");
        rematchPanel = GameObject.Find("RematchPanel");

        turnIndexText = GameObject.Find("TurnIndexText").GetComponent<Text>();
        movementCountText = GameObject.Find("CurrentMovementText").GetComponent<Text>();
        abilityText = GameObject.Find("CurrentAbilityText").GetComponent<Text>();
        attackText = GameObject.Find("CurrentAttackText").GetComponent<Text>();
        hideButtonImage = GameObject.Find("HideButtonImage").GetComponent<Image>();
        tutorialScreen = GameObject.Find("TutorialScreen").GetComponent<Image>();

        helpPanel.SetActive(false);
        rematchPanel.SetActive(false);
        winnerPanel.SetActive(false);

        StartGame();
    }


    void Update()
    {
        AudioChange();

        if (mapCreated)
        {
            //Carico tutte le unità di gioco in una lista una volta per averle disponibili sempre
            CheckIfUnitsAreLoaded();
            CheckIfTilesAreLoaded();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnTurnButtonClick();
        }

        if (playerIndex == 1)
        {
            Color blueColor = new Color(0, 0.75f, 1, 1);
            turnIndexText.color = blueColor;
        }
        else
        {
            turnIndexText.color = Color.red;
        }

        turnIndexText.text = "TURN PLAYER: " + playerIndex;


        if (currentSelectedUnit != null)
        {
            movementCountText.text = "MOVES: " + currentSelectedUnit.currentMoveCount.ToString();

            if (currentSelectedUnit.isAbilityInCooldown)
            {
                abilityText.color = Color.yellow;
            }
            else
            {
                abilityText.color = Color.green;
            }

            if (currentSelectedUnit.hasAttacked)
            {
                attackText.color = Color.yellow;
            }
            else
            {
                attackText.color = Color.green;
            }

            attackText.text = "ATTACKED: " + currentSelectedUnit.hasAttacked.ToString();

            if (currentSelectedUnit.ability != null)
            {
                abilityText.text = "ABILITY: " + currentSelectedUnit.ability.title;
            }
            else
            {
                abilityText.color = Color.white;
                abilityText.text = "ABILITY: (NOPE)";
            }
        }
        else
        {
            movementCountText.color = Color.white;
            movementCountText.text = "MOVES: 0";
            abilityText.color = Color.white;
            abilityText.text = "ABILITY: (NOPE)";
            attackText.color = Color.white;
            attackText.text = "ATTACKED: false";
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            TacticalMode();
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            HelpScreen();
        }
    }


    void CheckIfUnitsAreLoaded()
    {
        if (!unitsLoadedInList)
        {
            GameObject[] unitsP1 = GameObject.FindGameObjectsWithTag("UnitsP1");
            GameObject[] unitsP2 = GameObject.FindGameObjectsWithTag("UnitsP2");

            for (int i = 0; i < unitsP1.Length; i++)
            {
                unitsList.Add(unitsP1[i]);
            }

            for (int i = 0; i < unitsP2.Length; i++)
            {
                unitsList.Add(unitsP2[i]);
            }

            foreach (GameObject unit in unitsList)
            {
                unitScriptList.Add(unit.GetComponent<UnitScript>());
            }

            unitsLoadedInList = true;
        }
    }


    void CheckIfTilesAreLoaded()
    {
        if (!tilesLoadedInList)
        {
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
            GameObject[] p1BaseTiles = GameObject.FindGameObjectsWithTag("P1BaseTile");
            GameObject[] p2BaseTiles = GameObject.FindGameObjectsWithTag("P2BaseTile");

            for (int i = 0; i < tiles.Length; i++)
            {
                tilesList.Add(tiles[i]);
            }

            for (int i = 0; i < p1BaseTiles.Length; i++)
            {
                tilesList.Add(p1BaseTiles[i]);
            }

            for (int i = 0; i < p2BaseTiles.Length; i++)
            {
                tilesList.Add(p2BaseTiles[i]);
            }

            foreach (GameObject tile in tilesList)
            {
                tileScriptList.Add(tile.GetComponent<TileScript>());
            }

            tilesLoadedInList = true;
        }
    }


    void StartGame()
    {
        playerIndex = Random.Range(1, 3); //Setta il giocatore di partenza randomico
    }


    //Gestione turni: switch dell'indice del player attivo
    public void OnTurnButtonClick()
    {
        foreach (UnitScript unit in unitScriptList)
        {
            unit.currentMoveCount = unit.stats.movementRange;

            if (unit.ownerIndex == playerIndex)
            {
                unit.isStunned = false;
            }
            else
            {
                unit.isInvulnerable = false;
                unit.isReadyToCounterAttack = false;

                if (unit.isCrippled)
                {
                    unit.currentMoveCount = 1;
                }

                unit.isCrippled = false;
            }

            unit.hasAttacked = false;
            unit.isAbilityUsed = false;
            unit.isSelected = false;
            unit.spriteRenderer.color = Color.white;
            unit.outlineScript.color = unit.ownerIndex - 1;
        }

        foreach (TileScript tile in tileScriptList)
        {
            tile.isInRange = false;
            tile.isTileTaken = false;
        }

        if (!isGameOver)
        {
            turnIndex++;

            if (playerIndex == 1)
            {
                playerIndex = 2;
            }
            else
            {
                playerIndex = 1;
            }
        }

        currentSelectedUnit = null;
    }


    public void TacticalMode()
    {
        tacticalModeEnabled = !tacticalModeEnabled;

        if (tacticalModeEnabled)
        {
            hideButtonImage.sprite = hideImages[1];
        }
        else
        {
            hideButtonImage.sprite = hideImages[0];
        }

        foreach (UnitScript unit in unitScriptList)
        {
            if (!unit.isDead)
            {
                unit.transform.GetChild(7).GetComponent<SpriteRenderer>().enabled = !tacticalModeEnabled;
            }
        }
    }

    // Gestione tutorial
    public void HelpScreen()
    {
            helpPanel.SetActive(!helpModeEnabled);

            helpModeEnabled = !helpModeEnabled;
            tutorialScreen.sprite = tutorialImages[0];
    }

    public void SetLegendImage()
    {
        tutorialScreen.sprite = tutorialImages[0];
    }

    public void SetRulesImage()
    {
        tutorialScreen.sprite = tutorialImages[1];
    }

    public void SetControlsImage()
    {

    }

    public void SetHudImage()
    {

    }

    public void ExitHelpScreen()
    {
        helpPanel.SetActive(false);
        helpModeEnabled = false;
    }

    // Finale che si attiva quando un re raggiunge la fine della scacchiera
    public void EndGame()
    {
        isGameOver = true;

        winnerText.text = "PLAYER "  + playerIndex.ToString() + " WINS!";
        winnerPanel.SetActive(true);
    }

    // Finale che si attiva quando muore il re del player 2
    public void EndGamePlayer1()
    {
        isGameOver = true;

        winnerText.text = "PLAYER 1 WINS!";
        winnerPanel.SetActive(true);
    }

    // Finale che si attiva quando muore il re del player 1
    public void EndGamePlayer2()
    {
        isGameOver = true;

        winnerText.text = "PLAYER 2 WINS!";
        winnerPanel.SetActive(true);
    }

    /*public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }*/

    public void OnMenuButtonClicked()
    {
        rematchPanel.SetActive(true);
    }

    public void OnRematchConfirmed()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnRematchRejected()
    {
        rematchPanel.SetActive(false);
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Gestione audio
    public void AudioChange()
    {
        try
        {
            if (teamMenuScript.isTeamPanelDeactivated)
            {
                AudioMenuScript.instance.menuMusic.Stop();
            }    
        }
        catch (System.Exception ex)
        {
            Debug.Log("DEVI PARTIRE DAL MAIN MENU!!!");   
        }
    }
}
