using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{

    public Vector3[] points;
    private Vector3 point;

    private NavMeshAgent nav;

    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private LayerMask obstacleLayer;

    private float playerViewRange = 25f;

    private Transform player;

    private int frameNum = 0;

    enum BEHAVIOUR { IDLE, CHASE };
    private BEHAVIOUR behaviour = BEHAVIOUR.IDLE;
    //private Vector3 Velocity = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        setPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (frameNum == 6)
        {
            bool playerInRange = Physics.CheckSphere(transform.position, playerViewRange, playerLayer);
            bool playerInSight = !Physics.Linecast(transform.position, player.position, out RaycastHit hitInfo, obstacleLayer);
            if (!playerInSight)
            {
                print(hitInfo.collider.name);

            }
            if (playerInRange && playerInSight)
            {
                behaviour = BEHAVIOUR.CHASE;
            }
            else
            {
                if (behaviour == BEHAVIOUR.CHASE)
                {
                    setPoints();
                }
                behaviour = BEHAVIOUR.IDLE;
            }
            frameNum = 0;
        }

        if (behaviour == BEHAVIOUR.IDLE)
        {
            if (point != null)
            {
                nav.SetDestination(point);
                float distance = (transform.position - point).magnitude;
                if (distance < 5)
                {
                    if (point == points[0])
                    {
                        point = points[1];
                    }
                    else
                    {
                        point = points[0];
                    }
                }

            }
        }
        else if (behaviour == BEHAVIOUR.CHASE)
        {
            nav.SetDestination(player.position);
        }
        frameNum++;
    }

    private void setPoints()
    {
        points = new Vector3[2] {
            new Vector3(gameObject.transform.position.x - 10, 6.5f, gameObject.transform.position.z),
            new Vector3(gameObject.transform.position.x + 10, 6.5f, gameObject.transform.position.z)
        };
        point = points[0];
    }
}
