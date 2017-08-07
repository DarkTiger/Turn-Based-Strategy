using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationScript : MonoBehaviour 
{
    public Animator stateAnimator;
    Animator hitAnimator;
    int attackerRoleIndex;
    bool abilityIsPersistent = false;
    UnitScript unitScript;
    Animator anim = null;


	void Start () 
    {
        stateAnimator = transform.GetChild(8).gameObject.GetComponent<Animator>();
        hitAnimator = transform.GetChild(9).gameObject.GetComponent<Animator>();
        attackerRoleIndex = GetComponent<UnitScript>().roleIndex;
        unitScript = GetComponent<UnitScript>();
    }
	

	void Update()
    {      
        if (anim != stateAnimator)
        {
            CheckAnimationsState();
        }
    }


    public void PlayDeathAnimation()
    {
        hitAnimator.SetBool("OnDead", true);
    }



    public IEnumerator PlayAttackAnimation(int attackerRoleIndex, bool isAbility, bool persistent)
    {
        if (persistent)
        {
            anim = stateAnimator;
        }
        else
        {
            anim = hitAnimator;
        }

        anim.SetInteger("AttackerRoleIndex", attackerRoleIndex);

        if (isAbility)
        {
            anim.SetBool("Ability", true);
            anim.SetBool("Attack", false);
        }
        else
        {
            anim.SetBool("Attack", true);
            anim.SetBool("Ability", false);
        }

        abilityIsPersistent = persistent;

        yield return new WaitForSeconds(0.5f);
    }


    void CheckAnimationsState()
    {
        AnimatorStateInfo state = hitAnimator.GetCurrentAnimatorStateInfo(0);

        if (!abilityIsPersistent || (!unitScript.isReadyToCounterAttack && !unitScript.isStunned && !unitScript.isInvulnerable && !unitScript.isCrippled))
        {
            if (state.IsName("PorcupineAttack") || state.IsName("RabbitAttack") || state.IsName("SkunkAttack") || state.IsName("KingAttack") || state.IsName("MoleAttack") || state.IsName("TigerAttack") || state.IsName("BearAttack"))
            {
                if (state.normalizedTime >= state.length)
                {
                    hitAnimator.SetBool("Attack", false);
                    hitAnimator.SetInteger("AttackerRoleIndex", -1);

                    /*if (!state.IsName("KingAttack"))
                    {
                        //hitAnimator.SetBool("Ability", false);
                        //hitAnimator.SetInteger("AttackerRoleIndex", -1);
                    }*/
                }
            }
            else if (/*state.IsName("PorcupineAbility") ||*/ state.IsName("RabbitAbility") /*|| state.IsName("SkunkAbility") || state.IsName("TigerAbility") || state.IsName("BearAbility")*/ || state.IsName("MoleAbility"))
            {
                if (state.normalizedTime >= state.length)
                {
                    //if (!state.IsName("PorcupineAbility"))
                    //{
                        hitAnimator.SetBool("Ability", false);
                        hitAnimator.SetInteger("AttackerRoleIndex", -1);

                        //hitAnimator.SetBool("Attack", false);
                        //hitAnimator.SetInteger("AttackerRoleIndex", -1);
                    //}
                }
            } 
        }

        if (state.IsName("Death"))
        {
            if (state.normalizedTime >= state.length)
            {
                hitAnimator.SetBool("OnDead", false);
            }
        }
    }


    public void DisableStateAnimation()
    {
        //hitAnimator.SetBool("Ability", false);

        stateAnimator.SetBool("Ability", false);
        //stateAnimator.SetInteger("AttackerRoleIndex", -1);
    } 
}
