using System;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : IState
{
    private PathFinderManager pathManager;
    private PlayerActions playerActions;
    private List<Vector3> path;

    private AIController aiController;

    public List<GameObject> players;
    public Vector3 target;

    private Vector3 lastPositionTarget;
    private int currentWP = 0;
    private float movement;

    public SearchState(AIController aiController)
    {
        playerActions = aiController.GetComponent<PlayerActions>();
        pathManager = aiController.GetComponent<PathFinderManager>();
        
        foreach (var gObj in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (gObj.name == aiController.gameObject.name) continue;
            players.Add(gObj);
        }
    }

    public void Update()
    {
        if (path == null)
        {
            path = pathManager.FindPath(aiController.transform.position, aiController.transform.);
        }
        throw new NotImplementedException();
    }

    public StatesEnum StateTransition(StatesEnum nextState)
    {
        throw new NotImplementedException();
    }

    private Vector3 ChooseNewTarget()
    {
        var total = players.Count;

    }
}
