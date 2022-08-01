using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour , IFirearm
{
    [SerializeField]
    [Range(0.2f, 3.0f)]
    private float fireRate = 0.5f;

    // [SerializeField]
    // [Range(1,10)]

    private int magazineSize = 10;
    private int currentMag;

    [SerializeField]
    private Transform firePoint;
    private float timer;

    [SerializeField] private Transform bulletProjectile;

    [SerializeField] private Transform debugTransform;

    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();

    private Vector3 mouseWorldPosition;

    //Reference to the timer created from FunctionTimer script.
    private FunctionTimer rofTimer;
    private FunctionTimer reloadTimer;


    private bool canFire = true;
    private float reloadDuration = 1.5f;

    void Start()
    {
        rofTimer = FunctionTimer.Create(OnrofTimerTimeout, fireRate, false);
        reloadTimer = FunctionTimer.Create(OnreloadTimerTimeout, reloadDuration, false);
        currentMag = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        drawLaserSight();
        
    }
    public void FireGun()
    {
        if (!canFire)
        {
            return;
        }
        if (currentMag ==0)
        {
            Reload();
            return;
        }
        Vector3 aimDir = (mouseWorldPosition - firePoint.position).normalized;
        Instantiate(bulletProjectile, firePoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        canFire = false;
        rofTimer.Start();
        currentMag -= 1;
    }

    public void Reload()
    {
        canFire = false;
        reloadTimer.Start();
    }

    private void drawLaserSight()
    {
        mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        else
        {
            // Distance it should check if there is no collision
            Vector3 point = ray.GetPoint(999f);
            debugTransform.position = point;
            mouseWorldPosition = point;
        }
    }

    private void OnrofTimerTimeout()
    {
        canFire = true;
    }
    private void OnreloadTimerTimeout()
    {
        currentMag = magazineSize;
        canFire = true;
    }
}
