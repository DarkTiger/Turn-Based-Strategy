﻿using System.Collections;
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

    AudioSource soundsAudioSource;
    // public AudioClip[] menuSoundEffects;
    public AudioClip menuSoundEffect;



    void Start()
    {
        startText = GameObject.Find("PressStart");
        panelMenuButtons = GameObject.Find("PanelMenuButtons");
        helpPanelMenu = GameObject.Find("HelpPanelMenu");
        menuTutorialScreen = GameObject.Find("MenuTutorialScreen").GetComponent<Image>();
        soundsAudioSource = GameObject.Find("SoundsAudioSource").GetComponent<AudioSource>();

        panelMenuButtons.SetActive(false);
        helpPanelMenu.SetActive(false);

    }



    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            soundsAudioSource.clip = menuSoundEffect;
            soundsAudioSource.Play();

            startText.SetActive(false);
            panelMenuButtons.SetActive(true);
        }
	}


    public void StartGame()
    {
        soundsAudioSource.clip = menuSoundEffect;
        soundsAudioSource.Play();

        //if (!soundsAudioSource.isPlaying)
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void QuitApplication()
    {
        soundsAudioSource.clip = menuSoundEffect;
        soundsAudioSource.Play();
        Application.Quit();
    }

    public void HelpScreenMenu()
    {
        soundsAudioSource.clip = menuSoundEffect;
        soundsAudioSource.Play();

        helpPanelMenu.SetActive(!helpModeMenuEnabled);
        helpModeMenuEnabled = !helpModeMenuEnabled;
        menuTutorialScreen.sprite = menuTutorialImages[0];
    }

    public void SetMenuLegendImage()
    {
        soundsAudioSource.clip = menuSoundEffect;
        soundsAudioSource.Play();
        menuTutorialScreen.sprite = menuTutorialImages[0];
    }

    public void SetMenuRulesImage()
    {
        soundsAudioSource.clip = menuSoundEffect;
        soundsAudioSource.Play();
        menuTutorialScreen.sprite = menuTutorialImages[1];
    }

    public void SetMenuControlsImage()
    {
        soundsAudioSource.clip = menuSoundEffect;
        soundsAudioSource.Play();
    }

    public void SetMenuHudImage()
    {
        soundsAudioSource.clip = menuSoundEffect;
        soundsAudioSource.Play();
    }

    public void ExitMenuHelpScreen()
    {
        soundsAudioSource.clip = menuSoundEffect;
        soundsAudioSource.Play();
        helpPanelMenu.SetActive(false);
        helpModeMenuEnabled = false;
    }
}
