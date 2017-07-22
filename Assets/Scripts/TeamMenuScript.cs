﻿using System.Collections;
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

    Text btnTextSlotP2_1;
    Text btnTextSlotP2_2;
    Text btnTextSlotP2_3;
    Text btnTextSlotP2_4;
    Text btnTextSlotP2_5;
    Text btnTextSlotP2_6;

    Button btnStartGame;
    Text btnStartGameText;
    public Text currentPlayerText;

    public Button[] roleTypesButtons;
    public Button[] p1SlotButtons;
    public Button[] p2SlotButtons;
    Text[] p1SlotsText;
    Text[] p2SlotsText;


    void Start ()
    {
        btnTextSlotP1_1 = p1SlotButtons[0].transform.GetChild(0).GetComponent<Text>();
        btnTextSlotP1_2 = p1SlotButtons[1].transform.GetChild(0).GetComponent<Text>();
        btnTextSlotP1_3 = p1SlotButtons[2].transform.GetChild(0).GetComponent<Text>();
        btnTextSlotP1_4 = p1SlotButtons[3].transform.GetChild(0).GetComponent<Text>();
        btnTextSlotP1_5 = p1SlotButtons[4].transform.GetChild(0).GetComponent<Text>();
        btnTextSlotP1_6 = p1SlotButtons[5].transform.GetChild(0).GetComponent<Text>();

        btnTextSlotP2_1 = p2SlotButtons[0].transform.GetChild(0).GetComponent<Text>();
        btnTextSlotP2_2 = p2SlotButtons[1].transform.GetChild(0).GetComponent<Text>();
        btnTextSlotP2_3 = p2SlotButtons[2].transform.GetChild(0).GetComponent<Text>();
        btnTextSlotP2_4 = p2SlotButtons[3].transform.GetChild(0).GetComponent<Text>();
        btnTextSlotP2_5 = p2SlotButtons[4].transform.GetChild(0).GetComponent<Text>();
        btnTextSlotP2_6 = p2SlotButtons[5].transform.GetChild(0).GetComponent<Text>();

        btnStartGame = GameObject.Find("StartGameButton").GetComponent<Button>();
        btnStartGameText = btnStartGame.transform.GetChild(0).GetComponent<Text>();
        btnStartGameText.text = "PLAYER 1 READY";

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
                    SetSlotRole(btnTextSlotP1_1, teamRolesP1[i]);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_1, teamRolesP2[i]);
                }   
            }
            else if (i == 1)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_2, teamRolesP1[i]);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_2, teamRolesP2[i]);
                }
            }
            else if (i == 2)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_3, teamRolesP1[i]);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_3, teamRolesP2[i]);
                }
            }
            else if (i == 3)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_4, teamRolesP1[i]);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_4, teamRolesP2[i]);
                }
            }
            else if (i == 4)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_5, teamRolesP1[i]);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_5, teamRolesP2[i]);
                }
            }
            else if (i == 5)
            {
                if (currentPlayer == 1)
                {
                    SetSlotRole(btnTextSlotP1_6, teamRolesP1[i]);
                }
                else
                {
                    SetSlotRole(btnTextSlotP2_6, teamRolesP2[i]);
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


    void SetSlotRole(Text slotText, int roleIndex)
    {
        switch (roleIndex)
        {
            case 0: slotText.text = "TANK"; slotText.color = Color.green; break;
            case 1: slotText.text = "ASSASSIN"; slotText.color = Color.green; break;
            case 2: slotText.text = "RANGED"; slotText.color = Color.green; break;
            case 3: slotText.text = "HEALER"; slotText.color = Color.green; break;
            case 4: slotText.text = "SPECIALIST 1"; slotText.color = Color.green; break;
            case 5: slotText.text = "SPECIALIST 2"; slotText.color = Color.green; break;
            case -1: slotText.text = "NOT ASSIGNED"; slotText.color = Color.red; break;
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
        btnTextSlotP2_1.color = Color.red;
        btnTextSlotP2_2.text = "NOT ASSIGNED";
        btnTextSlotP2_2.color = Color.red;
        btnTextSlotP2_3.text = "NOT ASSIGNED";
        btnTextSlotP2_3.color = Color.red;
        btnTextSlotP2_4.text = "NOT ASSIGNED";
        btnTextSlotP2_4.color = Color.red;
        btnTextSlotP2_5.text = "NOT ASSIGNED";
        btnTextSlotP2_5.color = Color.red;
        btnTextSlotP2_6.text = "NOT ASSIGNED";
        btnTextSlotP2_6.color = Color.red;
    }


    public void OnStartGameClick()
    {
        if (currentPlayer == 1)
        {
            currentPlayer = 2;
            btnStartGameText.text = "START GAME!";
            btnStartGame.interactable = false;

            currentPlayerText.text = "CHOOSE YOUR TEAM:\n(Player 2)";

            for (int i = 0; i < roleTypesButtons.Length; i++)
            {
                roleTypesButtons[i].interactable = true;
            }
        }
        else
        {
            GameObject.Find("Map").GetComponent<MapGenerator>().CreateMap(teamRolesP1, teamRolesP2);
            gameObject.SetActive(false);
        }
    }
}