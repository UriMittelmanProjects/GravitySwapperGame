using UnityEngine;

public class MovingLavaBlock : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector2[] waypoints = new Vector2[4];

    private int currentWaypointIndex = 0;
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;

        // Define rectangular path waypoints (relative to start position)
        waypoints[0] = startPosition; // Bottom-left
        waypoints[1] = startPosition + new Vector2(10, 0); // Bottom-right  
        waypoints[2] = startPosition + new Vector2(10, 1); // Top-right
        waypoints[3] = startPosition + new Vector2(0, 1); // Top-left

        // Start at the first waypoint
        transform.position = waypoints[0];
    }

    void Update()
    {
        MoveTowardsCurrentWaypoint();
    }

    void MoveTowardsCurrentWaypoint()
    {
        Vector2 targetPosition = waypoints[currentWaypointIndex];
        Vector2 currentPosition = transform.position;

        // Move towards the target waypoint
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

        // Check if we've reached the current waypoint
        if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
        {
            // Move to next waypoint (loop back to 0 after reaching the last one)
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    // Optional: Draw the path in the Scene view for debugging
    void OnDrawGizmos()
    {
        if (waypoints.Length > 0)
        {
            Gizmos.color = Color.yellow;

            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.DrawWireSphere(waypoints[i], 0.3f);

                // Draw lines between waypoints
                int nextIndex = (i + 1) % waypoints.Length;
                Gizmos.DrawLine(waypoints[i], waypoints[nextIndex]);
            }
        }
    }
}