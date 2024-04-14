using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class runSceneBhv : StateMachineBehaviour
{
    public string scene; 

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("here");
        //SceneManager.LoadScene(scene);
        SceneManager.LoadSceneAsync(scene);

    }


}
