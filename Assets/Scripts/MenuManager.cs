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

    AudioSource soundsAudioSource;
    public AudioClip[] menuSoundEffects;
    AudioClip menuSoundEffect;



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
            soundsAudioSource.clip = menuSoundEffects[0];
            soundsAudioSource.Play();

            startText.SetActive(false);
            panelMenuButtons.SetActive(true);
        }
	}


    public void StartGame()
    {
        soundsAudioSource.clip = menuSoundEffects[0];
        soundsAudioSource.Play();

        //if (!soundsAudioSource.isPlaying)
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void QuitApplication()
    {
        soundsAudioSource.clip = menuSoundEffects[1];
        soundsAudioSource.Play();
        Application.Quit();
    }

    public void HelpScreenMenu()
    {
        soundsAudioSource.clip = menuSoundEffects[0];
        soundsAudioSource.Play();

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

    public void SetMenuControlsImage()
    {

    }

    public void SetMenuHudImage()
    {

    }

    public void ExitMenuHelpScreen()
    {
        soundsAudioSource.clip = menuSoundEffects[1];
        soundsAudioSource.Play();
        helpPanelMenu.SetActive(false);
        helpModeMenuEnabled = false;
    }
}
