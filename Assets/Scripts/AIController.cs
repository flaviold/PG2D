using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(PlayerActions))]
public class AIController : MonoBehaviour {

    private PlayerActions playerActions;
    private float movement;
    private bool jump;
    private bool shoot;

    public PathFinderManager pathManager;

    public Transform target;
    public float updateRate = 2f;
    public Path path;

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
        playerActions = GetComponent<PlayerActions>();

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        var p = pathManager.FindPath(transform.position, target.position);
        foreach (var i in p)
        {
            Debug.Log(i);
        }

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
        if (!p.error)
        {
            path = p;
            currentWP = 0;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;
        if (path == null) return;
        if (pathEnded) return;

        if (currentWP >= path.vectorPath.Count)
        {
            if (pathEnded) return;

            Debug.Log("Path Ended");
            pathEnded = true;
            return;
        }
        pathEnded = false;

        Vector3 dir = (path.vectorPath[currentWP] - transform.position);
        //dir *= 300 * Time.fixedDeltaTime;
        
        var move = 0;
        if (dir.x < 0) move = -1;
        if (dir.x > 0) move =  1;

        var jump = false;
        if (dir.y > 1) jump = true;

        playerActions.Move(move, jump, false);
        move = 0;
        jump = false;
        //rb.AddForce(dir, fMode);
        //rb.velocity = dir;

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWP]);
        if (dist < WPDistance)
        {
            currentWP++;
        }
    }
}
