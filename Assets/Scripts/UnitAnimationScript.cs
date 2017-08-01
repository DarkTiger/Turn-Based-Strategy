using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationScript : MonoBehaviour 
{
    public Animator animator;
    int attackerRoleIndex;
    bool abilityIsPersistent = false;
    UnitScript unitScript;



	void Start () 
    {
	    animator = transform.GetChild(6).gameObject.GetComponent<Animator>();
        attackerRoleIndex = GetComponent<UnitScript>().roleIndex;
        unitScript = GetComponent<UnitScript>();
    }
	

	void Update()
    {      
        CheckAnimationsState();
    }


    /// <summary>
    /// PLAYA L'ANIMAZIONE DELL'ATTACCO
    /// </summary>
    /// <param name="attackerRoleIndex">INDICE CLASSE ATTACCANTE</param>
    /// <param name="isAbility">If set to <c>true</c> E' UN'ABILITA' (true, false)</param>
    public void PlayAttackAnimation(int attackerRoleIndex, bool isAbility, bool persistent)
    {
        animator.SetInteger("AttackerRoleIndex", attackerRoleIndex);

        if (isAbility)
        {
            animator.SetBool("Ability", true);
            animator.SetBool("Attack", false);
        }
        else
        {
            animator.SetBool("Attack", true);
            animator.SetBool("Ability", false);
        }

        abilityIsPersistent = persistent;
    }


    void CheckAnimationsState()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (!abilityIsPersistent || (!unitScript.isReady && !unitScript.isStunned && !unitScript.isInvulnerable))
        {
            if (state.IsName("PorcupineAttack") || state.IsName("RabbitAttack") || state.IsName("SkunkAttack"))
            {
                if (state.normalizedTime >= state.length)
                {
                    animator.SetBool("Attack", false);
                    animator.SetBool("Ability", false);
                    animator.SetInteger("AttackerRoleIndex", -1);
                }
            }
            else if (state.IsName("PorcupineAbility") || state.IsName("RabbitAbility") || state.IsName("SkunkAbility"))
            {
                if (state.normalizedTime >= state.length)
                {
                    animator.SetBool("Ability", false);
                    animator.SetBool("Attack", false);
                    animator.SetInteger("AttackerRoleIndex", -1);
                }
            }
        }
    }
}
