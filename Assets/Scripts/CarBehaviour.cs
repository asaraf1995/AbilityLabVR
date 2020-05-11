using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : StateMachineBehaviour
{
    Vector3 temppos;
    Quaternion temprot;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("enter " + animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        //Debug.Log(animator.gameObject.name);
        var newmovementCode = animator.GetInteger("newmovementCode");
        animator.SetInteger("movementCode", newmovementCode);
        animator.SetInteger("newmovementCode", 0);
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("exit");
        temppos = animator.gameObject.transform.position;
        temprot = animator.gameObject.transform.rotation;
        animator.gameObject.transform.position = Vector3.zero;
        animator.gameObject.transform.rotation = Quaternion.identity;
        animator.gameObject.transform.parent.position = temppos;
        animator.gameObject.transform.parent.rotation = temprot;
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{

    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{

    //}

}
