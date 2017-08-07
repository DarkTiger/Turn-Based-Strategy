using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TeamMenuScript : MonoBehaviour
{
    List<int> teamRolesP1 = new List<int> {-1, -1, -1, -1, -1, -1, -1};
    List<int> teamRolesP2 = new List<int> { -1, -1, -1, -1, -1, -1, -1 };

    int roleDuplicationLimit = 2;
    int currentPlayer = 1;
    
    Text btnTextSlotP1_1;
    Text btnTextSlotP1_2;
    Text btnTextSlotP1_3;
    Text btnTextSlotP1_4;
    Text btnTextSlotP1_5;
    Text btnTextSlotP1_6;

    Image btnSpriteSlotP1_1;
    Image btnSpriteSlotP1_2;
    Image btnSpriteSlotP1_3;
    Image btnSpriteSlotP1_4;
    Image btnSpriteSlotP1_5;
    Image btnSpriteSlotP1_6;

    Text btnTextSlotP2_1;
    Text btnTextSlotP2_2;
    Text btnTextSlotP2_3;
    Text btnTextSlotP2_4;
    Text btnTextSlotP2_5;
    Text btnTextSlotP2_6;

    Image btnSpriteSlotP2_1;
    Image btnSpriteSlotP2_2;
    Image btnSpriteSlotP2_3;
    Image btnSpriteSlotP2_4;
    Image btnSpriteSlotP2_5;
    Image btnSpriteSlotP2_6;

    Button btnStartGame;
    Text btnStartGameText;
    public Text currentPlayerText;

    public Sprite[] roleSpritesP1;
    public Sprite[] roleSpritesP2;

    public Button[] roleTypesButtons;
    public Button[] p1SlotButtons;
    public Button[] p2SlotButtons;

    public bool isTeamPanelDeactivated = false;

    AudioSource soundsAudioSource;
    public AudioClip startSoundEffect;
    public AudioClip battleMusic;

    void Start ()
    {
        btnTextSlotP1_1 = p1SlotButtons[0].transform.GetChild(1).GetComponent<Text>();
        btnTextSlotP1_2 = p1SlotButtons[1].transform.GetChild(1).GetComponent<Text>();
        btnTextSlotP1_3 = p1SlotButtons[2].transform.GetChild(1).GetComponent<Text>();
        btnTextSlotP1_4 = p1SlotButtons[3].transform.GetChild(1).GetComponent<Text>();
        btnTextSlotP1_5 = p1SlotButtons[4].transform.GetChild(1).GetComponent<Text>();
        btnTextSlotP1_6 = p1SlotButtons[5].transform.GetChild(1).GetComponent<Text>();

        btnSpriteSlotP1_1 = p1SlotButtons[0].transform.GetChild(0).GetComponent<Image>();
        btnSpriteSlotP1_2 = p1SlotButtons[1].transform.GetChild(0).GetComponent<Image>();
        btnSpriteSlotP1_3 = p1SlotButtons[2].transform.GetChild(0).GetComponent<Image>();
        btnSpriteSlotP1_4 = p1SlotButtons[3].transform.GetChild(0).GetComponent<Image>();
        btnSpriteSlotP1_5 = p1SlotButtons[4].transform.GetChild(0).GetComponent<Image>();
        btnSpriteSlotP1_6 = p1SlotButtons[5].transform.GetChild(0).GetComponent<Image>();
                
        btnTextSlotP2_1 = p2SlotButtons[0].transform.GetChild(1).GetComponent<Text>();
        btnTextSlotP2_2 = p2SlotButtons[1].transform.GetChild(1).GetComponent<Text>();
        btnTextSlotP2_3 = p2SlotButtons[2].transform.GetChild(1).GetComponent<Text>();
        btnTextSlotP2_4 = p2SlotButtons[3].transform.GetChild(1).GetComponent<Text>();
        btnTextSlotP2_5 = p2SlotButtons[4].transform.GetChild(1).GetComponent<Text>();
        btnTextSlotP2_6 = p2SlotButtons[5].transform.GetChild(1).GetComponent<Text>();

        btnSpriteSlotP2_1 = p2SlotButtons[0].transform.GetChild(0).GetComponent<Image>();
        btnSpriteSlotP2_2 = p2SlotButtons[1].transform.GetChild(0).GetComponent<Image>();
        btnSpriteSlotP2_3 = p2SlotButtons[2].transform.GetChild(0).GetComponent<Image>();
        btnSpriteSlotP2_4 = p2SlotButtons[3].transform.GetChild(0).GetComponent<Image>();
        btnSpriteSlotP2_5 = p2SlotButtons[4].transform.GetChild(0).GetComponent<Image>();
        btnSpriteSlotP2_6 = p2SlotButtons[5].transform.GetChild(0).GetComponent<Image>();

        soundsAudioSource = GameObject.Find("SoundsAudioSource").GetComponent<AudioSource>();
        btnStartGame = GameObject.Find("StartGameButton").GetComponent<Button>();
        btnStartGameText = btnStartGame.transform.GetChild(0).GetComponent<Text>();
        btnStartGameText.text = "PLAYER 1 READY";

        isTeamPanelDeactivated = false;
        ResetP2SlotAtStart();
        OnListsChange();
    }


    void Update()
    {
        for (int i = 0; i < p1SlotButtons.Length; i++)
        {
            if (currentPlayer == 1)
            {
                p1SlotButtons[i].interactable = true;
                p2SlotButtons[i].interactable = false;
            }
            else
            {
                p2SlotButtons[i].interactable = true;
                p1SlotButtons[i].interactable = false;
            }
        }
    }


    public void OnRoleButtonClicked(int roleIndex)
    {
        for (int i = 0; i < teamRolesP1.Count; i++)
        {
            if (currentPlayer == 1)
            {
                if (teamRolesP1[i] == -1)
                {
                    teamRolesP1[i] = roleIndex;
                    break;
                }
            }
            else
            {
                if (teamRolesP2[i] == -1)
                {
                    teamRolesP2[i] = roleIndex;
                    break;
                }
            }
        }

        OnListsChange();
        CheckRoleDuplications();
    }


    public void OnRemoveFromRoleList(int slotNumber)
    {
        if (currentPlayer == 1)
        {
            teamRolesP1[slotNumber - 1] = -1;
        }
        else
        {
            teamRolesP2[slotNumber - 1] = -1; 
        }

        OnListsChange();
        CheckRoleDuplications();
    }


    void OnListsChange()
    {
        bool missingRoleP1 = false;
        bool missingRoleP2 = false;

        
        for (int i = 0; i < 6; i++)
        {
            if (teamRolesP1[i] == -1)
            {
                missingRoleP1 = true;
            }

            if (teamRolesP2[i] == -1)
            {
                missingRoleP2 = true;
            }

            if (i == 0)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_1, teamRolesP1[i], btnSpriteSlotP1_1, 1);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_1, teamRolesP2[i], btnSpriteSlotP2_1, 2);
                }   
            }
            else if (i == 1)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_2, teamRolesP1[i], btnSpriteSlotP1_2, 1);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_2, teamRolesP2[i], btnSpriteSlotP2_2, 2);
                }
            }
            else if (i == 2)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_3, teamRolesP1[i], btnSpriteSlotP1_3, 1);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_3, teamRolesP2[i], btnSpriteSlotP2_3, 2);
                }
            }
            else if (i == 3)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_4, teamRolesP1[i], btnSpriteSlotP1_4, 1);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_4, teamRolesP2[i], btnSpriteSlotP2_4, 2);
                }
            }
            else if (i == 4)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_5, teamRolesP1[i], btnSpriteSlotP1_5, 1);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_5, teamRolesP2[i], btnSpriteSlotP2_5, 2);
                }
            }
            else if (i == 5)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_6, teamRolesP1[i], btnSpriteSlotP1_6, 1);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_6, teamRolesP2[i], btnSpriteSlotP2_6, 2);
                }
            }
        }

        if (currentPlayer == 1)
        {
            if (missingRoleP1)
            {
                btnStartGame.interactable = false;
            }
            else
            {
                btnStartGame.interactable = true;
            }
        }
        else
        {
            if (missingRoleP2)
            {
                btnStartGame.interactable = false;
            }
            else
            {
                btnStartGame.interactable = true;
            }
        }
    }


    void SetSlotRole(Text slotText, int roleIndex, Image slotSprite, int playerIndex)
    {
        int spriteIndex = roleIndex;

        Sprite[] roleSprites = roleSpritesP1;

        if (playerIndex == 2)
        {
            roleSprites = roleSpritesP2;
        }

        if (roleIndex == -1)
        {
            spriteIndex = 6;
        }

        switch (roleIndex)
        {
            case 0: slotText.text = ""; slotText.color = Color.green; slotSprite.sprite = roleSprites[0]; break;
            case 1: slotText.text = ""; slotText.color = Color.green; slotSprite.sprite = roleSprites[1]; break;
            case 2: slotText.text = ""; slotText.color = Color.green; slotSprite.sprite = roleSprites[2]; break;
            case 3: slotText.text = ""; slotText.color = Color.green; slotSprite.sprite = roleSprites[3]; break;
            case 4: slotText.text = ""; slotText.color = Color.green; slotSprite.sprite = roleSprites[4]; break;
            case 5: slotText.text = ""; slotText.color = Color.green; slotSprite.sprite = roleSprites[5]; break;
            case -1: slotText.text = "NOT ASSIGNED"; slotText.color = Color.white; slotSprite.sprite = roleSprites[6]; break;
        }
    }


    void CheckRoleDuplications()
    {
        int type1Count = 0;
        int type2Count = 0;
        int type3Count = 0;
        int type4Count = 0;
        int type5Count = 0;
        int type6Count = 0;
        List<int> teamRoles;

        if (currentPlayer == 1)
        {
            teamRoles = teamRolesP1;
        }
        else
        {
            teamRoles = teamRolesP2;
        }

        for (int i = 0; i < 7; i++)
        {
            switch (teamRoles[i])
            {
                case 0: type1Count++; break;
                case 1: type2Count++; break;
                case 2: type3Count++; break;
                case 3: type4Count++; break;
                case 4: type5Count++; break;
                case 5: type6Count++; break;
            }
        }

        roleTypesButtons[0].interactable = true;
        roleTypesButtons[1].interactable = true;
        roleTypesButtons[2].interactable = true;
        roleTypesButtons[3].interactable = true;
        roleTypesButtons[4].interactable = true;
        roleTypesButtons[5].interactable = true;

        if (type1Count >= roleDuplicationLimit)
        {
            roleTypesButtons[0].interactable = false;
        }

        if (type2Count >= roleDuplicationLimit)
        {
            roleTypesButtons[1].interactable = false;
        }

        if (type3Count >= roleDuplicationLimit)
        {
            roleTypesButtons[2].interactable = false;
        }

        if (type4Count >= roleDuplicationLimit)
        {
            roleTypesButtons[3].interactable = false;
        }

        if (type5Count >= roleDuplicationLimit)
        {
            roleTypesButtons[4].interactable = false;
        }

        if (type6Count >= roleDuplicationLimit)
        {
            roleTypesButtons[5].interactable = false;
        }
    }


    void ResetP2SlotAtStart()
    {
        btnTextSlotP2_1.text = "NOT ASSIGNED";
        btnTextSlotP2_1.color = Color.white;
        btnTextSlotP2_2.text = "NOT ASSIGNED";
        btnTextSlotP2_2.color = Color.white;
        btnTextSlotP2_3.text = "NOT ASSIGNED";
        btnTextSlotP2_3.color = Color.white;
        btnTextSlotP2_4.text = "NOT ASSIGNED";
        btnTextSlotP2_4.color = Color.white;
        btnTextSlotP2_5.text = "NOT ASSIGNED";
        btnTextSlotP2_5.color = Color.white;
        btnTextSlotP2_6.text = "NOT ASSIGNED";
        btnTextSlotP2_6.color = Color.white;
    }


    public void OnStartGameClick()
    {
        soundsAudioSource.clip = startSoundEffect;
        soundsAudioSource.Play();

        if (currentPlayer == 1)
        {
            currentPlayer = 2;
            btnStartGameText.text = "START GAME!";
            btnStartGame.interactable = false;

            currentPlayerText.text = "CHOOSE YOUR TEAM\nPlayer 2";

            for (int i = 0; i < roleTypesButtons.Length; i++)
            {
                roleTypesButtons[i].interactable = true;
            }
        }
        else
        {
            List<int> teamRolesP1Inverted = new List<int>();
            List<int> teamRolesP2Inverted = new List<int>();

            for (int i = 0; i < teamRolesP1.Count; i++)
            {
                teamRolesP1Inverted.Add(teamRolesP1[6 - i]);
                teamRolesP2Inverted.Add(teamRolesP2[6 - i]);
            }
            

            GameObject.Find("Map").GetComponent<MapGenerator>().CreateMap(teamRolesP1Inverted, teamRolesP2Inverted);
            gameObject.SetActive(false);

            try
            {
                AudioMenuScript.instance.menuMusic.Stop();
                AudioMenuScript.instance.menuMusic.clip = battleMusic;
                AudioMenuScript.instance.menuMusic.Play();
            }
            catch (System.Exception) {};
                        
            isTeamPanelDeactivated = true;
        }
    }
}
