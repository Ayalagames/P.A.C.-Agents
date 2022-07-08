using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    [Range(0.2f, 1.0f)]
    private float fireRate = 1;

    // [SerializeField]
    // [Range(1,10)]
    // private int damage = 1;

    [SerializeField]
    private Transform firePoint;
    private float timer;

    [SerializeField] private Transform bulletProjectile;

    [SerializeField] private Transform debugTransform;

    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();




    private bool canFire;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
           canFire = true;
        }
        
    }
    public void FireGun()
    {

        if (!canFire)
        {
            return;
        }
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width/ 2f, Screen.height/2f);
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

        Vector3 aimDir = (mouseWorldPosition - firePoint.position).normalized;
        Instantiate(bulletProjectile, firePoint.position, Quaternion.LookRotation(aimDir, Vector3.up));

    }
}
