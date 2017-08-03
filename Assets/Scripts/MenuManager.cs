using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    GameObject panelMenuButtons;
    GameObject startText;
    GameObject helpPanelMenu;

    bool helpModeMenuEnabled = false;

    public Sprite[] menuTutorialImages;
    Image menuTutorialScreen;



    void Start()
    {
        startText = GameObject.Find("PressStart");
        panelMenuButtons = GameObject.Find("PanelMenuButtons");
        helpPanelMenu = GameObject.Find("HelpPanelMenu");
        menuTutorialScreen = GameObject.Find("MenuTutorialScreen").GetComponent<Image>();

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
        menuTutorialScreen.sprite = menuTutorialImages[0];
    }

    public void SetMenuLegendImage()
    {
        menuTutorialScreen.sprite = menuTutorialImages[0];
    }

    public void SetMenuRulesImage()
    {
        menuTutorialScreen.sprite = menuTutorialImages[1];
    }

    public void SetMenuCommandsImage()
    {

    }

    public void SetMenuHudImage()
    {

    }

    public void ExitMenuHelpScreen()
    {
        helpPanelMenu.SetActive(false);
        helpModeMenuEnabled = false;
    }
}
