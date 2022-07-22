using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanGun : MonoBehaviour , IFirearm
{

    [SerializeField]
    [Range(0.05f, 3.0f)]
    private float fireRate = 0.2f;

    private int damage = 5;

    [SerializeField] private Transform debugTransform;

    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private LayerMask damageColliderLayerMask = new LayerMask();
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private Transform firePosition;



    private int magazineSize = 10;
    private int currentMag;

    private bool canFire = true;

    private FunctionTimer ROFTimer;
    private FunctionTimer reloadTimer;

    private float reloadDuration = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        ROFTimer = FunctionTimer.Create(OnROFTimerTimeout, fireRate, false);
        reloadTimer = FunctionTimer.Create(OnreloadTimerTimeout, reloadDuration, false);
        currentMag = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
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

    public void FireGun()
    {
        if (canFire)
        {
            if (currentMag == 0)
            {
                Reload();
                return;
            }

            Vector3 endPoint = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, damageColliderLayerMask))
            {
                endPoint = raycastHit.point;
                if (raycastHit.collider.TryGetComponent(out IDamageable hurtbox))
                {
                    hurtbox.health -= damage;
                }
            }
            else
            {
                // Distance it should check if there is no collision
                Vector3 point = ray.GetPoint(999f);
                endPoint = point;
            }
            GameObject b = Instantiate(bulletTrail);
            b.GetComponent<LineRenderer>().SetPosition(0, firePosition.position);
            b.GetComponent<LineRenderer>().SetPosition(1, endPoint);
            currentMag -= 1;
            canFire = false;
            ROFTimer.Start();
        }
    }

    public void Reload()
    {
        canFire = false;
        reloadTimer.Start();
    }

    private void OnROFTimerTimeout()
    {
        canFire = true;
    }

    private void OnreloadTimerTimeout()
    {
        currentMag = magazineSize;
        canFire = true;
    }
}
