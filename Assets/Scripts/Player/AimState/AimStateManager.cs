using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public HipFireState Hip = new HipFireState();
    public AimState Aim = new AimState();
    
    
    [SerializeField] float mouseSense = 1;
    float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;


    [HideInInspector] public Animator anim;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        SwitchState(Hip);
    }


    private void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);


        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    

}
