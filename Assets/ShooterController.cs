using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;



public class ShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;

    private Gun gun;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        GameObject other = GameObject.Find("Gun");
        gun = (Gun) other.GetComponent(typeof(Gun));
        print(gun);
    }


    // Update is called once per frame
    void Update()
    {
        
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);

            // Vector3 worldAimTarget = mouseWorldPosition;
            // worldAimTarget.y = transform.position.y;
            // Vector3 aimDirection = (worldAimTarget - transform.position).normalized; 

            // transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime*20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);

        }

        if (starterAssetsInputs.shoot)
        {
            // Call current gun, fire()
            print(gun);
            gun.FireGun();
        }

    }
}
