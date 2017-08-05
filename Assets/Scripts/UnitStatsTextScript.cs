﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatsTextScript : MonoBehaviour
{
    Text attackText;
    Text defenseText;
    UnitScript unitScript;

    
    void Start()
    {
        unitScript = GetComponent<UnitScript>();
        attackText = transform.GetChild(3).GetComponent<Text>();
        defenseText = transform.GetChild(4).GetComponent<Text>();
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

        if (bonusDefense > 0)
        {
            defenseText.text = "(" + bonusDefense + ")\n" + baseHealth;
        }
        else
        {
            defenseText.text = baseHealth.ToString();
        }

        if (unitScript.stats.health < unitScript.stats.maxHealth)
        {
            defenseText.color = Color.red;
        }
        else
        {
            defenseText.color = Color.white;
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
}
