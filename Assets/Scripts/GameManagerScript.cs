using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{ 
    public int playerIndex;                 // Indica a quale giocatore appartiene l'unità

    public int turnDuration;                // Gestione del tempo
    public int gameTime;

    public List<GameObject> unitsList;      // Variabili di gestione unità
    public List<GameObject> tilesList;
    public bool mapCreated = false;
    bool unitsLoadedInList = false;
    bool tilesLoadedInList = false;

    public bool isGameOver = false;         // Gestione del game over

    public Button turnButton;               // UI
    Text turnIndexText;
    Text winnerText;
    GameObject winnerPanel;



    void Start()
    {
        List<GameObject> unitsList = new List<GameObject>();
        List<GameObject> tilesList = new List<GameObject>();
        winnerPanel = GameObject.Find("WinnerPanel");
        winnerText = GameObject.Find("WinnerText").GetComponent<Text>();


        //Gestione del button che controlla il cambio turno
        Button btn = turnButton.GetComponent<Button>();
        btn.onClick.AddListener(OnTurnButtonClick);
        turnIndexText = GameObject.Find("TurnIndexText").GetComponent<Text>();

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
            Color blueColor = new Color();
            blueColor.r = 0;
            blueColor.g = 0.75f;
            blueColor.b = 1;
            blueColor.a = 1;
            turnIndexText.color = blueColor;
        }
        else
        {
            turnIndexText.color = Color.red;
        }

        turnIndexText.text = "TURNO DEL GIOCATORE: " + playerIndex;
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

            unitsLoadedInList = true;
        }
    }


    void StartGame()
    {
        playerIndex = Random.Range(1,2); //Setta il giocatore di partenza
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
        if (!isGameOver)
        {
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
}
