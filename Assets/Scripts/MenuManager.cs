using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void QuitApplication()
    {
        Application.Quit();
    }
}
