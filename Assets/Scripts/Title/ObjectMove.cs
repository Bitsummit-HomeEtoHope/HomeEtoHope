using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Item Number")]
    public int item;

    private Vector3[] waypoints;

    private int currentWaypointIndex;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = new Vector3[7];
        Vector3 tmp = GameObject.Find("folder01").transform.position;
        waypoints[0] = tmp;
        tmp = GameObject.Find("good01").transform.position;
        waypoints[1] = tmp;
        tmp = GameObject.Find("good02").transform.position;
        waypoints[2] = tmp;
        tmp = GameObject.Find("good03").transform.position;
        waypoints[3] = tmp;
        tmp = GameObject.Find("good04").transform.position;
        waypoints[4] = tmp;
        tmp = GameObject.Find("good05").transform.position;
        waypoints[5] = tmp;
        tmp = GameObject.Find("folder02").transform.position;
        waypoints[6] = tmp;
        currentWaypointIndex = item+1;
    }

    // Update is called once per frame
    void Update()
    {
        // 目標ポイントに向かって移動
        MoveTowardsWaypoint();
    }

    private void MoveTowardsWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        //transform.position = targetPosition;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (currentWaypointIndex == 6)
            {
                transform.position = waypoints[0];
                currentWaypointIndex = 0;
            }
            else
            {
                currentWaypointIndex++;
            }
        }
    }
}

