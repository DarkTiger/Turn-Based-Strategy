using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UnitStatsTextScript : MonoBehaviour
{
    Text attackText;
    Text healthText;
    Text defenseText;
    Image defensePanel;
    UnitScript unitScript;

    
    void Start()
    {
        unitScript = GetComponent<UnitScript>();
        attackText = transform.GetChild(4).GetComponent<Text>();
        healthText = transform.GetChild(5).GetComponent<Text>();
        defenseText = transform.GetChild(6).GetComponent<Text>();
        defensePanel = transform.GetChild(3).GetComponent<Image>();
    }


    void Update()
    {
        int bonusAttack = unitScript.bonusAttack;
        int baseDamage = unitScript.stats.damage;
        int bonusDefense = unitScript.bonusDefense;
        int baseHealth = unitScript.stats.health;

        if (bonusAttack > 0)
        {
            attackText.text = /*"(" + bonusAttack + ")\n" +*/ (bonusAttack + baseDamage).ToString();
        }
        else
        {
            attackText.text = baseDamage.ToString();
        }

        healthText.text = baseHealth.ToString();


        if (bonusDefense > 0)
        {
            defenseText.text = bonusDefense.ToString();
            defensePanel.enabled = true;
        }
        else
        {
            defenseText.text = "";
            defensePanel.enabled = false;
        }

        if (unitScript.stats.health < unitScript.stats.maxHealth)
        {
            healthText.color = Color.red;
        }
        else
        {
            healthText.color = Color.white;
        }

        if (unitScript.bonusAttack > 0)
        {
            attackText.color = Color.green;
        }
        else
        {
            attackText.color = Color.white;
        }
    }


    public void DoShakeDefenseBonus()
    {
        defensePanel.gameObject.transform.DOShakePosition(0.5f, 0.25f, 10, 90f, false, true);   
        defenseText.gameObject.transform.DOShakePosition(0.5f, 0.25f, 10, 90f, false, true);   
    }
}
