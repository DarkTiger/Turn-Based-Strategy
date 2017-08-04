using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{
    public string title;
    public string desc;


    public Stats GetUnitRole(int roleIndex)
    {
        Stats newStats = new Stats();

        if (roleIndex == 0) // Tank
        {
            newStats.attackRange = 1.1f;
            newStats.damage = 4;
            newStats.movementRange = 2;
            newStats.health = 8;
            newStats.maxHealth = 8;

            return newStats;
        }
        else if (roleIndex == 1) // Assassin
        {
            newStats.attackRange = 1.1f;
            newStats.damage = 6;
            newStats.movementRange = 3;
            newStats.health = 4;
            newStats.maxHealth = 4;

            return newStats;
        }
        else if (roleIndex == 2) // Ranged
        {
            newStats.attackRange = 3.01f;
            newStats.damage = 3;
            newStats.movementRange = 2;
            newStats.health = 5;
            newStats.maxHealth = 5;

            return newStats;
        }
        else if (roleIndex == 3) // Healer
        {
            newStats.attackRange = 1.1f;
            newStats.damage = 2;
            newStats.movementRange = 3;
            newStats.health = 5;
            newStats.maxHealth = 5;

            return newStats;
        }
        else if (roleIndex == 4) // Specialist
        {
            newStats.attackRange = 1.1f;
            newStats.damage = 4;
            newStats.movementRange = 2;
            newStats.health = 6;
            newStats.maxHealth = 6;

            return newStats;
        }
        else if (roleIndex == 5) // Specialist 2
        {
            newStats.attackRange = 1.1f;
            newStats.damage = 3;
            newStats.movementRange = 3;
            newStats.health = 7;
            newStats.maxHealth = 7;

            return newStats;
        }
        if (roleIndex == 6) // KING
        {
            newStats.attackRange = 1.1f;
            newStats.damage = 5;
            newStats.movementRange = 2;
            newStats.health = 13;
            newStats.maxHealth = 13;

            return newStats;
        }
        else
        {
            return null;
        }
    }


    public Ability GetUnitAbility(int roleIndex)
    {
        Ability ability = new Ability();

        if (roleIndex == 0) // Tank
        {
            ability.classIndex = roleIndex;
            ability.title = "COUNTER";
            ability.desc = "";

            return ability;
        }
        else if (roleIndex == 1) // Assassin
        {
            ability.classIndex = roleIndex;
            ability.title = "STEALTH";
            ability.desc = "";

            return ability;
        }
        else if (roleIndex == 2) // Ranged
        {
            ability.classIndex = roleIndex;
            ability.title = "CRIPPLE";
            ability.desc = "";

            return ability;
        }
        else if (roleIndex == 3) // Healer
        {
            ability.classIndex = roleIndex;
            ability.title = "HEAL";
            ability.desc = "";

            return ability;
        }
        else if (roleIndex == 4) // Specialist
        {
            ability.classIndex = roleIndex;
            ability.title = "STUN";
            ability.desc = "";

            return ability;
        }
        else if (roleIndex == 5) // Specialist 2
        {
            ability.classIndex = roleIndex;
            ability.title = "SWAP";
            ability.desc = "";

            return ability;
        }
        else
        {
            return null;
        }
    }
}
