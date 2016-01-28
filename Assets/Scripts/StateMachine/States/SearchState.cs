using System.Collections.Generic;
using UnityEngine;

public class SearchState : IState
{
    private PathFinderManager pathManager;
    private PlayerActions playerActions;
	private List<Vector3> path;
	public bool pathEnded = false;

    private AIController aiController;

    public List<GameObject> players;
    private Vector3 target;
	private Vector3 lastPositionTarget;
    
	private int currentWP = 0;

    public SearchState(AIController aiController)
    {
        this.aiController = aiController;
        playerActions = aiController.GetComponent<PlayerActions>();
        pathManager = aiController.GetComponent<PathFinderManager>();
        players = new List<GameObject>();

        foreach (var gObj in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (gObj.name == aiController.gameObject.name) continue;
            players.Add(gObj);
        }

		target = ChooseNewTarget();
		lastPositionTarget = target;
    }

	void UpdatePath()
	{
		if ((target == null) || (pathEnded))
		{
			target = ChooseNewTarget();
			pathEnded = false;
		}
		if ((path == null) || (lastPositionTarget == null) || (pathManager.GetDistance(target, lastPositionTarget) > 3f))
		{
			lastPositionTarget = target;
			path = pathManager.FindPath(aiController.transform.position, target);
			currentWP = 0;
		}

        if (currentWP >= path.Count)
        {
            Debug.Log("Path Ended");
            pathEnded = true;
            return;
        }

        var distX = Mathf.Abs(aiController.transform.position.x - path[currentWP].x);
		var distY = aiController.transform.position.y - path[currentWP].y;
		if (distX < aiController.WPDistanceX && distY > aiController.WPDistanceYMin && distY < aiController.WPDistanceYMax)
		{
			currentWP++;
		}

        if (currentWP >= path.Count)
        {
            Debug.Log("Path Ended");
            pathEnded = true;
            return;
        }
    }

    public void Update()
    {
		UpdatePath();
        if (pathEnded) return;

        Vector3 dir = (path[currentWP] - aiController.transform.position);
		
		var move = 0;
		if (dir.x < -.825f) move = -1;
		if (dir.x > .825f) move =  1;
		
		var jump = false;
		if (dir.y > 1) jump = true;
		
		playerActions.Move(move, jump);
    }

    public StatesEnum StateTransition(StatesEnum nextState)
    {
		return nextState;
    }

    private Vector3 ChooseNewTarget()
    {
        var total = players.Count;
		var random = new System.Random();

		var value = random.Next(total);

		return players[value].transform.position;
    }
}
