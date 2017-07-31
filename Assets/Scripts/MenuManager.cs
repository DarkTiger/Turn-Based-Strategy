using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameObject panelMenuButtons;




    void Start()
    {
        panelMenuButtons = GameObject.Find("PanelMenuButtons");
        panelMenuButtons.SetActive(false);
    }



    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            panelMenuButtons.SetActive(true);
        }
	}


    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
