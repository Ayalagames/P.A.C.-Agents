using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aime)
    {
        aime.anim.SetBool("Aiming", true);
    }


    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) aim.SwitchState(aim.Hip);
    }
}