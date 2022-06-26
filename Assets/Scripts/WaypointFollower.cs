using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    // for waypoint pathing
    [SerializeField] private GameObject[] waypoints;
    private int currWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        // paths game object between set waypoints
        if (Vector2.Distance(waypoints[currWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            ++currWaypointIndex;
            if (currWaypointIndex >= waypoints.Length)
            {
                currWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
