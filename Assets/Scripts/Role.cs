using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{
    public string title;
    public string desc;
    List<Ability> abilities;
    public UnitStats myStats;

    


    void Start()
    {
        
    }


    public Stats GetUnitRole(int roleIndex)
    {
        Stats newStats = new Stats();

        if (roleIndex == 0)
        {           
            newStats.attackRange = 1;
            newStats.damage = 34;
            newStats.attackRange = 1;
            newStats.damage = 34;

            return newStats;
        }
        else if (roleIndex == 1)
        {
            newStats.attackRange = 2;
            newStats.damage = 50;

            return newStats;
        }

        return null;
    }
}
