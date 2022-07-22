using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    private float speed = 30f;

    private float time = 5f;
    private float current_time;

    private int damage = 5;


    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = transform.forward*speed;
    }
    private void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        current_time += Time.deltaTime;
        if (current_time > time)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.tag == "Player"))
        {
            if (other.TryGetComponent(out IDamageable hurtbox))
            {
                hurtbox.health -= damage;
            }
        Destroy(gameObject);
    }
    }
}
