using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManager aime)
    {
        aime.anim.SetBool("Aiming", false);

    }


    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
    }
}
