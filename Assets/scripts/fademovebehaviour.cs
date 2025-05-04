using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fademovebehaviour : StateMachineBehaviour
{
    public float fadetime = 0.5f;
    private float timeelapsed = 0f;
    SpriteRenderer spriteRenderer;
    GameObject objectmove;
    Color startcolor;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeelapsed = 0f;
        spriteRenderer=animator.GetComponent<SpriteRenderer>();
        startcolor=spriteRenderer.color;
        objectmove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeelapsed += Time.deltaTime;
        float newalpha =startcolor.a* (1 - timeelapsed / fadetime);

        spriteRenderer.color = new Color(startcolor.r, startcolor.g, startcolor.b, newalpha);

        if (timeelapsed > fadetime) { 

            Destroy(objectmove);
        
        }
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
