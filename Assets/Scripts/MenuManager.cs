using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameObject panelMenuButtons;
    GameObject startText;



    void Start()
    {
        startText = GameObject.Find("PressStart");
        panelMenuButtons = GameObject.Find("PanelMenuButtons");
        panelMenuButtons.SetActive(false);
    }



    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            startText.SetActive(false);
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
