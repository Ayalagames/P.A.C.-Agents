using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private int _health;
    private MeshRenderer rend;
    public int health
    {
        set
        {
            _health = value;
            rend.material.color = Color.red;
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
            Invoke("ResetColor", 0.1f);
        }
        get { return _health; }
    }
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        health = 20;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ResetColor()
    {
        rend.material.color = Color.white;
    }
}
