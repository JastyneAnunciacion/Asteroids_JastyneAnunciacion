using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {
    [SerializeField] private Animator animator;

    public void TriggerParameter(string triggerName) {
        animator.SetTrigger(triggerName);
    }
}
