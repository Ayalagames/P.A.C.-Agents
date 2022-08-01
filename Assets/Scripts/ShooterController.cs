using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class ShooterController : MonoBehaviour, IPortalClose
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;

    private HitscanGun gun;
    private List<IFirearm> guns;
    [SerializeField]
    private Transform gunContainer;

    private IFirearm currentWeapon;
    private int currentWeaponIndex = 0;

    private Portal portal;
    private int portalFrameNum;

    [SerializeField]
    private TMP_Text portalCloseText;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        portalCloseText.gameObject.SetActive(false);

        guns = new List<IFirearm> { };
        foreach (Transform gun in gunContainer)
        {
            guns.Add(gun.gameObject.GetComponent<IFirearm>());
        }
        currentWeapon = guns[currentWeaponIndex];
    }

    private void Awake() {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        //GameObject other = GameObject.Find("HitScanGun");
        //gun = (HitscanGun) other.GetComponent(typeof(HitscanGun));
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
            currentWeapon.FireGun();
        }
        else
        {
        }

        if (starterAssetsInputs.switchWeapons)
        {
            switchWeapons();
            starterAssetsInputs.switchWeapons = false;        
        }

        if (starterAssetsInputs.closePortal)
        {
            portalFrameNum++;
            if (portalFrameNum == 20)
            {

                if (portal != null)
                {
                    portal.Health--;
                    portalCloseText.text = $"Hold F to close the Portal {100-portal.Health}%";

                }
                portalFrameNum = 0;
            }
        }

    }

    private void switchWeapons()
    {
        print("switching");
        currentWeaponIndex += 1;
        if (currentWeaponIndex > guns.Count - 1)
        {
            currentWeaponIndex = 0;
        }
        currentWeapon = guns[currentWeaponIndex];
    }

    public void PortalRangeEntered(Portal p )
    {
        portal = p;
        portalCloseText.gameObject.SetActive(true);
        portalCloseText.text = $"Hold F to close the Portal {100 - portal.Health}%";
    }

    public void PortalRangeExited()
    {
        portal = null;
        portalCloseText.gameObject.SetActive(false);

    }
}
