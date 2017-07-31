using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationScript : MonoBehaviour 
{
    public Animator animator;



	void Start () 
    {
	    animator = transform.GetChild(6).gameObject.GetComponent<Animator>();	
	}
	

	void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Attack", true);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Ability", true);
        }

        //Animation anim = animator.an animation;

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("PorcupineAttack"))
        {
            if (state.normalizedTime >= state.length)
            {
                animator.SetBool("Attack", false);
            }
        }
        else if (state.IsName("PorcupineAbility"))
        {
            if (state.normalizedTime >= state.length)
            {
                animator.SetBool("Ability", false);
            }
        }


        Debug.Log(state.IsName("PorcupineAbility").ToString() + "  " + state.normalizedTime.ToString() + "  " + state.length.ToString());

        /*if (!anim.isPlaying)
        {
            animator.SetBool("Attack", false);
        }*/
    }

}
