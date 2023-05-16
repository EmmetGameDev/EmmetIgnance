using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker), typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    Vector2 previousTargetPosition;

    public float speed;
    public float nextWaypointDistance;

    public float updatePathTime = .5f;

    [Range(0,1)]public float rotationSmoothing = .75f;

    public Vector2[] patrolPoints;
    public float nextPatrolPointDistance;
    int currentPatrolPoint = 0;

    public float gizmosSize = .2f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEnd = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, updatePathTime);
        seeker.StartPath(rb.position, target.position, OnCalculatedPath);
    }

    void UpdatePath()
    {
        if (previousTargetPosition != (Vector2)target.position && seeker.IsDone()) seeker.StartPath(rb.position, target.position, OnCalculatedPath);
        previousTargetPosition = target.position;
    }

    void OnCalculatedPath(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        PatrolingMovement();
        //PathfindingMovement();
    }

    void PathfindingMovement()
    {
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else reachedEnd = false;

        MoveTowards(path.vectorPath[currentWaypoint]);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) currentWaypoint++;
    }

    void PatrolingMovement()
    {
        if (patrolPoints == null) return;
        if (currentPatrolPoint >= patrolPoints.Length) currentPatrolPoint = 0;

        MoveTowards(patrolPoints[currentPatrolPoint]);

        float distance = Vector2.Distance(rb.position, patrolPoints[currentPatrolPoint]);
        if (distance < nextPatrolPointDistance) currentPatrolPoint++;
    }

    void MoveTowards(Vector3 p)
    {
        //v is the normalized direction to the point
        Vector2 v = ((Vector2)p - rb.position).normalized;
        rb.velocity = v * speed * Time.fixedDeltaTime;

        //code for rotating the enemy to the point
        Vector2 lookDir = (Vector2)p - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = Mathf.LerpAngle(angle, rb.rotation, rotationSmoothing);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (Vector2 p in patrolPoints)
        {
            Gizmos.DrawSphere(p, gizmosSize);
        }
    }
}
