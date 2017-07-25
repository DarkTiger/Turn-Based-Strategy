using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerScript : MonoBehaviour
{ 
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

    Text turnIndexText;
    Text winnerText;
    Text movementCountText;
    GameObject winnerPanel;
    


    void Start()
    {
        unitsList = new List<GameObject>();
        tilesList = new List<GameObject>();
        unitScriptList = new List<UnitScript>();
        tileScriptList = new List<TileScript>();
                
        winnerPanel = GameObject.Find("WinnerPanel");
        winnerText = GameObject.Find("WinnerText").GetComponent<Text>();

        turnIndexText = GameObject.Find("TurnIndexText").GetComponent<Text>();
        movementCountText = GameObject.Find("CurrentMovementText").GetComponent<Text>();

        winnerPanel.SetActive(false);
        StartGame();
    }


    void Update()
    {
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
        }
        else
        {
            movementCountText.text = "MOVES: 0";
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
        playerIndex = Random.Range(1,3); //Setta il giocatore di partenza randomico
    }


    void OnTurnChanged()
    {

    }


    void UpdateTime()
    {

    }

    //Gestione turni: switch dell'indice del player attivo
    public void OnTurnButtonClick()
    {
        foreach (UnitScript unit in unitScriptList)
        {
            if (unit.ownerIndex == playerIndex)
            {
                unit.isStunned = false;
                unit.isInvulnerable = false;
            }
        }

        /*foreach (TileScript tile in tileScriptList)
        {
            if (tile.activationTurnIndex != -1)
            {
                if (tile.activationTurnIndex <= turnIndex + 2)
                {
                    tile.isTileTaken = false;
                    tile.activationTurnIndex = -1;
                }
            }
        }*/

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
       

        foreach (GameObject unit in unitsList)
        {
            UnitScript unitScript = unit.GetComponent<UnitScript>();

            unitScript.currentMoveCount = unitScript.stats.movementRange;
            unitScript.hasAttacked = false;
            unitScript.isSelected = false;
        }
    }


    public void EndGame()
    {
        isGameOver = true;
        Debug.Log("The winner is " + playerIndex);

        winnerText.text = "PLAYER " + playerIndex + " WINS!";
        winnerPanel.SetActive(true);
    }


    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
}
