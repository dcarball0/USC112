using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
public class RutaBus : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float moveSpeed = 10f,
                    rotationSpeed = 10f;
    int waypointIndex = 0;
 
    Transform initialPosition;
 
    // Start is called before the first frame update
    void Start()
    {
       transform.position = wayPoints[0].transform.position;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (waypointIndex < wayPoints.Count)
        {
            if (wayPoints[waypointIndex] != null)
            {
                var targetPosition = wayPoints[waypointIndex].transform.position;
                var movementThisFrame = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);

                // Determine which direction to rotate towards
                Vector3 targetDirection = wayPoints[waypointIndex].transform.position - transform.position;

                // The step size is equal to speed times frame time.
                float singleStep = rotationSpeed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Draw a ray pointing at our target in
                Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        
            if (waypointIndex < wayPoints.Count && Vector3.Distance(transform.position, wayPoints[waypointIndex].transform.position) < 1)
                waypointIndex += 1;
            
            if (waypointIndex >= wayPoints.Count)
                waypointIndex = 0;

        }
    }

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < wayPoints.Count; i++)
        {
            Gizmos.color = Color.blue;
            
            // Connect current waypoint to the next one
            Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[(i + 1) % wayPoints.Count].transform.position);
        }
    }

}