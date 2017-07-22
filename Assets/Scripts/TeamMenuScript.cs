using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TeamMenuScript : MonoBehaviour
{
    List<int> teamRolesP1 = new List<int> {-1, -1, -1, -1, -1, -1, -1};
    List<int> teamRolesP2 = new List<int> { -1, -1, -1, -1, -1, -1, -1 };

    Button btnRoleType1;
    Button btnRoleType2;
    Button btnRoleType3;
    Button btnRoleType4;
    Button btnRoleType5;
    Button btnRoleType6;

    Button btnSlotP1_1;
    Button btnSlotP1_2;
    Button btnSlotP1_3;
    Button btnSlotP1_4;
    Button btnSlotP1_5;
    Button btnSlotP1_6;

    Text btnTextSlotP1_1;
    Text btnTextSlotP1_2;
    Text btnTextSlotP1_3;
    Text btnTextSlotP1_4;
    Text btnTextSlotP1_5;
    Text btnTextSlotP1_6;

    Button btnSlotP2_1;
    Button btnSlotP2_2;
    Button btnSlotP2_3;
    Button btnSlotP2_4;
    Button btnSlotP2_5;
    Button btnSlotP2_6;

    Text btnTextSlotP2_1;
    Text btnTextSlotP2_2;
    Text btnTextSlotP2_3;
    Text btnTextSlotP2_4;
    Text btnTextSlotP2_5;
    Text btnTextSlotP2_6;



    void Start ()
    {
        btnRoleType1 = GameObject.Find("BtnRoleType1").GetComponent<Button>();
        btnRoleType2 = GameObject.Find("BtnRoleType2").GetComponent<Button>();
        btnRoleType3 = GameObject.Find("BtnRoleType3").GetComponent<Button>();
        btnRoleType4 = GameObject.Find("BtnRoleType4").GetComponent<Button>();
        btnRoleType5 = GameObject.Find("BtnRoleType5").GetComponent<Button>();
        btnRoleType6 = GameObject.Find("BtnRoleType6").GetComponent<Button>();

        btnSlotP1_1 = GameObject.Find("BtnSlotP1_1").GetComponent<Button>();
        btnSlotP1_2 = GameObject.Find("BtnSlotP1_2").GetComponent<Button>();
        btnSlotP1_3 = GameObject.Find("BtnSlotP1_3").GetComponent<Button>();
        btnSlotP1_4 = GameObject.Find("BtnSlotP1_4").GetComponent<Button>();
        btnSlotP1_5 = GameObject.Find("BtnSlotP1_5").GetComponent<Button>();
        btnSlotP1_6 = GameObject.Find("BtnSlotP1_6").GetComponent<Button>();

        Text btnTextSlotP1_1 = GameObject.Find("BtnSlotP1_1").transform.GetChild(0).GetComponent<Text>();
        Text btnTextSlotP1_2 = GameObject.Find("BtnSlotP1_2").transform.GetChild(0).GetComponent<Text>();
        Text btnTextSlotP1_3 = GameObject.Find("BtnSlotP1_3").transform.GetChild(0).GetComponent<Text>();
        Text btnTextSlotP1_4 = GameObject.Find("BtnSlotP1_4").transform.GetChild(0).GetComponent<Text>();
        Text btnTextSlotP1_5 = GameObject.Find("BtnSlotP1_5").transform.GetChild(0).GetComponent<Text>();
        Text btnTextSlotP1_6 = GameObject.Find("BtnSlotP1_6").transform.GetChild(0).GetComponent<Text>();
        
        btnSlotP2_1 = GameObject.Find("BtnSlotP2_1").GetComponent<Button>();
        btnSlotP2_2 = GameObject.Find("BtnSlotP2_2").GetComponent<Button>();
        btnSlotP2_3 = GameObject.Find("BtnSlotP2_3").GetComponent<Button>();
        btnSlotP2_4 = GameObject.Find("BtnSlotP2_4").GetComponent<Button>();
        btnSlotP2_5 = GameObject.Find("BtnSlotP2_5").GetComponent<Button>();
        btnSlotP2_6 = GameObject.Find("BtnSlotP2_6").GetComponent<Button>();

        Text btnTextSlotP2_1 = GameObject.Find("BtnSlotP2_1").transform.GetChild(0).GetComponent<Text>();
        Text btnTextSlotP2_2 = GameObject.Find("BtnSlotP2_2").transform.GetChild(0).GetComponent<Text>();
        Text btnTextSlotP2_3 = GameObject.Find("BtnSlotP2_3").transform.GetChild(0).GetComponent<Text>();
        Text btnTextSlotP2_4 = GameObject.Find("BtnSlotP2_4").transform.GetChild(0).GetComponent<Text>();
        Text btnTextSlotP2_5 = GameObject.Find("BtnSlotP2_5").transform.GetChild(0).GetComponent<Text>();
        Text btnTextSlotP2_6 = GameObject.Find("BtnSlotP2_6").transform.GetChild(0).GetComponent<Text>();
    }


    public void OnRoleButtonClicked(int roleIndex)
    {
        for (int i = 0; i < teamRolesP1.Count; i++)
        {
            if (teamRolesP1[i] == -1)
            {
                teamRolesP1[i] = roleIndex;
                break;
            }
        }

        OnListsChange();
    }


    void OnListsChange()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == 0)
            {
                SetSlotRole(btnTextSlotP1_1, teamRolesP1[i]);
            }
            else if (i == 1)
            {
                SetSlotRole(btnTextSlotP1_2, teamRolesP1[i]);
            }
            else if (i == 2)
            {
                SetSlotRole(btnTextSlotP1_3, teamRolesP1[i]);
            }
            else if (i == 3)
            {
                SetSlotRole(btnTextSlotP1_4, teamRolesP1[i]);
            }
            else if (i == 4)
            {
                SetSlotRole(btnTextSlotP1_5, teamRolesP1[i]);
            }
            else if (i == 5)
            {
                SetSlotRole(btnTextSlotP1_6, teamRolesP1[i]);
            }
            /*else if (i == 6)
            {
                SetSlotRole(btnTextSlotP1_7, teamRolesP1[i]);
            }*/
        }
    }

    void SetSlotRole(Text slotText, int roleIndex)
    {
        switch (roleIndex)
        {
            case 0: slotText.text = "TANK"; break;
            case 1: slotText.text = "ASSASSIN"; break;
            case 2: slotText.text = "RANGED"; break;
            case 3: slotText.text = "HEALER"; break;
            case 4: slotText.text = "SPECIALIST 1"; break;
            case 5: slotText.text = "SPECIALIST 2"; break;
            case -1: slotText.text = "NOT ASSIGNED"; break;
        }
    }
}
