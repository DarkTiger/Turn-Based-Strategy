using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/stats")]
public class UnitStats : ScriptableObject {

    public int roleIndex;
    public int maxHealth;
    public int movementRange;
    public int damage;
    public int attackRange;
}
