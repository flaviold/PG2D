using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class AstarTest : MonoBehaviour {

    public Transform target;

    public float updateRate = 2f;

    public Path path;

    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathEnded = false;

    public float WPDistance = 3;

    private Seeker seeker;
    private Rigidbody2D rb;

    private int currentWP = 0;


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if (target == null) yield return false;

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWP = 0;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;
        if (path == null) return;

        if (currentWP >= path.vectorPath.Count)
        {
            if (pathEnded) return;

            Debug.Log("Path Ended");
            pathEnded = true;
            return;
        }
        pathEnded = false;

        Vector3 dir = (path.vectorPath[currentWP] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        Debug.Log(dir);
        //rb.AddForce(dir, fMode);
        rb.velocity = dir;

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWP]);
        if (dist < WPDistance)
        {
            currentWP++;
        }
    }
}
