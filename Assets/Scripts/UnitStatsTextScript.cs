using System.Collections;
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

        attackText.text = "(" + bonusAttack + ")\n" + baseDamage;
        defenseText.text = "(" + bonusDefense + ")\n" + baseHealth;
    }
}
