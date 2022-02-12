using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private string currentAnimationState;

    [SerializeField] private Animator animator;
   public void ChangeAnimationState(string newState){
        if(currentAnimationState==newState){
            return;
        }
        else{
            currentAnimationState=newState;
            animator.Play(currentAnimationState);
        }
    }    
}
