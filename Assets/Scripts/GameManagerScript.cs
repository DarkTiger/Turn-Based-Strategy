using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{ 
    public int playerIndex;
    public int turnDuration;
    public int gameTime;
    public List<GameObject> unitsList;
    public bool mapCreated = false;
    bool unitsLoadedInList = false;
    public Button turnButton;



    void Start()
    {
        List<GameObject> unitsList = new List<GameObject>();

        //Gestione del button che controlla il cambio turno
        Button btn = turnButton.GetComponent<Button>();
        btn.onClick.AddListener(OnTurnButtonClick);

        StartGame();

    }


    void Update()
    {
        if (mapCreated)
        {
            //Carico tutte le unità di gioco in una lista una volta per averle disponibili sempre
            CheckIfUnitsAreLoaded();
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

            unitsLoadedInList = true;
        }
    }


    void StartGame()
    {
        playerIndex = 1; //Setta il giocatore di partenza
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
        if (playerIndex == 1)
        {
            playerIndex = 2;
        }
        else
        {
            playerIndex = 1;
        }
    }
}
