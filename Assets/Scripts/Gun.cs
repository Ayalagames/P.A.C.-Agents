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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                timer = 0f;
                FireGun();
            }
        }
        
    }
    private void FireGun()
    {
        Debug.DrawRay(firePoint.position, firePoint.forward*100, Color.red, 2f);
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            Destroy(hitInfo.collider.gameObject);
            Debug.Log(hitInfo);
        }

    }
}
