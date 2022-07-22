using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour , IDamageable
{
    private int _health;
    public int health {
        set
        {
            _health = value;
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
        get { return _health; }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
