using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private int _health;
    public int Health
    {
        set
        {
            _health = value;
            print(_health);
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
        get { return _health; }
    }
    private void Start()
    {
        Health = 100;
    }
    private void OnTriggerEnter(Collider other)
    {
        IPortalClose p = other.GetComponent<IPortalClose>();
        p.PortalRangeEntered(this);

    }

    private void OnTriggerExit(Collider other)
    {
        IPortalClose p = other.GetComponent<IPortalClose>();
        p.PortalRangeExited();
    }
}
