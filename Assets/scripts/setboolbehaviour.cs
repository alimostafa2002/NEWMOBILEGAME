using UnityEngine;

public class setboolbehaviour : StateMachineBehaviour
{
    public string parameterName;
    public bool valueOnEnter = true;
    public bool valueOnExit = false;
    public bool updateOnState = true;
    // REMOVE these two:
    // public bool updateOnStateMachine = true;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(parameterName, valueOnEnter);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetBool(parameterName, valueOnExit);
        }
    }
}
