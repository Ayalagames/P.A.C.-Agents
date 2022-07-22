using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{

    private FunctionTimer timeout;
    // Start is called before the first frame update
    void Start()
    {
        timeout = FunctionTimer.Create(OnTimerTimeout, 1.5f, true);
        timeout.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTimerTimeout()
    {
        Destroy(gameObject);
    }
}
