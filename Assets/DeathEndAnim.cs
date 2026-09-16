using UnityEngine;

public class DeathAnimationEnd : StateMachineBehaviour
{
    bool triggered = false;

    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        triggered = false;
    }

    public override void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        if (!triggered && stateInfo.normalizedTime >= 1f)
        {
            triggered = true;

            BlackScreen blackScreen = FindObjectOfType<BlackScreen>();

            if (blackScreen != null)
                blackScreen.StartDeathTransition();
        }
    }
}