using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(PlayerActions))]
[RequireComponent(typeof(PathFinderManager))]
public class AIController : MonoBehaviour {

    private PlayerActions playerActions;
    private float movement;
    private bool jump;
    private bool shoot;

    private PathFinderManager pathManager;

    public Transform target;
    public float updateRate = 2f;
    public List<Vector3> path;

    [HideInInspector]
    public bool pathEnded = false;

    public float WPDistance = 2;

    private Seeker seeker;
    private Rigidbody2D rb;

    private int currentWP = 0;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        playerActions = GetComponent<PlayerActions>();
        pathManager = GetComponent<PathFinderManager>();

        path = pathManager.FindPath(transform.position, target.position);

        //StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if (target == null) yield return false;

        path = pathManager.FindPath(transform.position, target.position);
        currentWP = 0;

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    //public void OnPathComplete(Path p)
    //{
    //    if (!p.error)
    //    {
    //        path = p;
    //        currentWP = 0;
    //    }
    //}

    void FixedUpdate()
    {
        if (target == null) return;
        if (path == null) return;
        if (pathEnded) return;

        if (currentWP >= path.Count)
        {
            if (pathEnded) return;

            Debug.Log("Path Ended");
            pathEnded = true;
            return;
        }
        pathEnded = false;

        Vector3 dir = (path[currentWP] - transform.position);
        //dir *= 300 * Time.fixedDeltaTime;
        
        var move = 0;
        if (dir.x < -.825f) move = -1;
        if (dir.x > .825f) move =  1;

        var jump = false;
        if (dir.y > 1) jump = true;

        playerActions.Move(move, jump, false);
        //rb.AddForce(dir, fMode);
        //rb.velocity = dir;

        var posX = new Vector2(transform.position.x, 0);
        var pathX = new Vector2(path[currentWP].x, 0);
        float dist = Vector2.Distance(posX, pathX);
        if ((dist < WPDistance) && (Mathf.Abs(transform.position.y - path[currentWP].y) < .1f))
        {
            Debug.Log(currentWP++);
            //currentWP++;
        }
    }
}
