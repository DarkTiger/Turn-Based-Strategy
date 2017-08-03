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


    public void PlayDeathAnimation()
    {
        animator.SetBool("OnDead", true);
    }


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

        if (!abilityIsPersistent || (!unitScript.isReadyToCounterAttack && !unitScript.isStunned && !unitScript.isInvulnerable))
        {
            if (state.IsName("PorcupineAttack") || state.IsName("RabbitAttack") || state.IsName("SkunkAttack") || state.IsName("KingAttack"))
            {
                if (state.normalizedTime >= state.length)
                {
                    animator.SetBool("Attack", false);

                    if (!state.IsName("KingAttack"))
                    {
                        animator.SetBool("Ability", false);
                        animator.SetInteger("AttackerRoleIndex", -1);
                    }
                }
            }
            else if (state.IsName("PorcupineAbility") || state.IsName("RabbitAbility") || state.IsName("SkunkAbility") || state.IsName("TigerAbility") || state.IsName("BearAbility"))
            {
                if (state.normalizedTime >= state.length)
                {
                    if (!state.IsName("PorcupineAbility"))
                    {
                        animator.SetBool("Ability", false);
                        animator.SetBool("Attack", false);
                        animator.SetInteger("AttackerRoleIndex", -1);
                    }
                }
            } 
        }

        if (state.IsName("Death"))
        {
            if (state.normalizedTime >= state.length)
            {
                animator.SetBool("OnDead", false);
            }
        }
    }
}
