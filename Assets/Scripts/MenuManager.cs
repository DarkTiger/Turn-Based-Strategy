using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameObject panelMenuButtons;
    GameObject startText;
    GameObject helpPanelMenu;

    bool helpModeMenuEnabled = false;



    void Start()
    {
        startText = GameObject.Find("PressStart");
        panelMenuButtons = GameObject.Find("PanelMenuButtons");
        helpPanelMenu = GameObject.Find("HelpPanelMenu");
        panelMenuButtons.SetActive(false);
        helpPanelMenu.SetActive(false);
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

    public void HelpScreenMenu()
    {
        helpPanelMenu.SetActive(!helpModeMenuEnabled);

        helpModeMenuEnabled = !helpModeMenuEnabled;
    }
}
