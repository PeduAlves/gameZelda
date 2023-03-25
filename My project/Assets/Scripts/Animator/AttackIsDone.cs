using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIsDone : StateMachineBehaviour
{
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

        animator.gameObject.SendMessage("AttackIsDone", SendMessageOptions.DontRequireReceiver);
    }
       
}

  
