using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{ 
    public int playerIndex;
    public int turnDuration;
    public int gameTime;
    public List<GameObject> unitsList;
    public bool mapCreated = false;
    bool unitsLoadedInList = false;



    void Start()
    {
        List<GameObject> unitsList = new List<GameObject>();
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

    }


    void OnTurnChanged()
    {

    }


    void UpdateTime()
    {

    }
}
