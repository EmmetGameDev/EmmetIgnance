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
        PathfindingMovement();
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

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.fixedDeltaTime;

        rb.velocity = force;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) currentWaypoint++;

        Vector3 diff = path.vectorPath[currentWaypoint] - transform.position;

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg -90f;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
}
