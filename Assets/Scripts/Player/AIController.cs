using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerActions))]
[RequireComponent(typeof(PathFinderManager))]
public class AIController : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;

    public Transform target;
    public float updateRate = 2f;

    public List<Transform> players;

    public float WPDistanceX = 2;
    public float WPDistanceYMax = 1f;
    public float WPDistanceYMin = -.1f;
    
    

    void Start()
    {
        enemyStateMachine = new EnemyStateMachine(this);

        path = pathManager.FindPath(transform.position, target.position);

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if (target == null) yield return false;
        if (pathManager.GetDistance(target.position, lastPositionTarget) > 3f)
        {
            lastPositionTarget = target.position;
            path = pathManager.FindPath(transform.position, target.position);
            currentWP = 0;
        }
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
        enemyStateMachine.State.Update();
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

        var distX = Mathf.Abs(transform.position.x - path[currentWP].x);
        var distY = transform.position.y - path[currentWP].y;
        if (distX < WPDistanceX && distY > WPDistanceYMin && distY < WPDistanceYMax)
        {
            Debug.Log(currentWP++);
            //currentWP++;
        }
    }
}
