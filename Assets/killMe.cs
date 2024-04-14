using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killMe : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    	//Debug.Break();
       animator.gameObject.SetActive(false);

    }
}
