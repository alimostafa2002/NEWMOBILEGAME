using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setfloatbehaviour : StateMachineBehaviour
{
    public string parameterName;

    public float valueOnEnter = 0f;
    public float valueOnExit = 0f;
    public float valueOnUpdate = 0f;

    public bool setOnEnter = true;
    public bool setOnUpdate = false;
    public bool setOnExit = false;

    public bool setOnStateMachineEnter = false;
    public bool setOnStateMachineExit = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (setOnEnter)
        {
            animator.SetFloat(parameterName, valueOnEnter);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (setOnUpdate)
        {
            animator.SetFloat(parameterName, valueOnUpdate);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (setOnExit)
        {
            animator.SetFloat(parameterName, valueOnExit);
        }
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (setOnStateMachineEnter)
        {
            animator.SetFloat(parameterName, valueOnEnter);
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (setOnStateMachineExit)
        {
            animator.SetFloat(parameterName, valueOnExit);
        }
    }
}
