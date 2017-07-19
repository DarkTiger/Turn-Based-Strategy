using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{
    public string title;
    public string desc;
    List<Ability> abilities;


    public Stats GetUnitRole(int roleIndex)
    {
        Stats newStats = new Stats();

        if (roleIndex == 0) // Tank
        {           
            newStats.attackRange = 1;
            newStats.damage = 5;
            newStats.movementRange = 2;
            newStats.health = 10;

            return newStats;
        }
        else if (roleIndex == 1) // Assassin
        {
            newStats.attackRange = 1;
            newStats.damage = 7;
            newStats.movementRange = 3;
            newStats.health = 4;

            return newStats;
        }
        else if (roleIndex == 2) // Ranged
        {
            newStats.attackRange = 3;
            newStats.damage = 3;
            newStats.movementRange = 3;
            newStats.health = 5;

            return newStats;
        }
        else if (roleIndex == 3) // Healer
        {
            newStats.attackRange = 1;
            newStats.damage = 2;
            newStats.movementRange = 2;
            newStats.health = 5;

            return newStats;
        }
        else if (roleIndex == 4) // Specialist
        {
            newStats.attackRange = 1;
            newStats.damage = 4;
            newStats.movementRange = 2;
            newStats.health = 6;

            return newStats;
        }

        return null;
    }
}
