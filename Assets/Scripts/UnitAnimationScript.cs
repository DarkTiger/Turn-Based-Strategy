using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationScript : MonoBehaviour 
{
    public Animator animator;
    int attackerRoleIndex;



	void Start () 
    {
	    animator = transform.GetChild(6).gameObject.GetComponent<Animator>();
        attackerRoleIndex = GetComponent<UnitScript>().roleIndex;
    }
	

	void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            PlayAttackAnimation(2, false);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            PlayAttackAnimation(4, true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            PlayAttackAnimation(4, false);
        }


        CheckAnimationsState();
    }


    /// <summary>
    /// PLAYA L'ANIMAZIONE DELL'ATTACCO
    /// </summary>
    /// <param name="attackerRoleIndex">INDICE CLASSE ATTACCANTE</param>
    /// <param name="isAbility">If set to <c>true</c> E' UN'ABILITA' (true, false)</param>
    public void PlayAttackAnimation(int attackerRoleIndex, bool isAbility)
    {
        animator.SetInteger("AttackerRoleIndex", attackerRoleIndex);

        if (isAbility)
        {
            animator.SetBool("Ability", true);
            /*switch (attackerRoleIndex)
            {
                case 0: animator.SetBool("Ability", true); break;
                case 1: animator.SetBool("Ability", true); break;
                case 2: animator.SetBool("Ability", true); break;
                case 3: animator.SetBool("Ability", true); break;
                case 4: animator.SetBool("Ability", true); break;
                case 5: animator.SetBool("Ability", true); break;
            }*/
        }
        else
        {
            animator.SetBool("Attack", true);
        }
    }


    void CheckAnimationsState()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("PorcupineAttack") || state.IsName("RabbitAttack") || state.IsName("SkunkAttack"))
        {
            if (state.normalizedTime >= state.length)
            {
                animator.SetBool("Attack", false);
                animator.SetInteger("AttackerRoleIndex", -1);
            }
        }
        else if (state.IsName("PorcupineAbility") || state.IsName("SkunkAbility"))
        {
            if (state.normalizedTime >= state.length)
            {
                animator.SetBool("Ability", false);
                animator.SetInteger("AttackerRoleIndex", -1);
            }
        }
    }
}
