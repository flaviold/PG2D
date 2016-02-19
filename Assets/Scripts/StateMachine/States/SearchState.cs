﻿using System.Collections.Generic;
using UnityEngine;

public class SearchState : IState
{
    private PathFinderManager pathManager;
    private PlayerActions playerActions;
	private List<Vector3> path;
	public bool pathEnded = false;

    private AIController aiController;

    private Vector3 target;
	private Vector3 lastPositionTarget;

    private float nxtUpdate = 0;
    
	private int currentWP = 0;

    public SearchState(AIController aiController)
    {
        this.aiController = aiController;
        playerActions = aiController.GetComponent<PlayerActions>();
        pathManager = aiController.GetComponent<PathFinderManager>();

		target = ChooseNewTarget();
		lastPositionTarget = target;
        nxtUpdate = 0;
        
    }

	void UpdatePath()
	{
        if ((target == null) || (pathEnded))
		{
			target = ChooseNewTarget();
			pathEnded = false;
		}
		if ((path == null) || (lastPositionTarget == null) || (pathManager.GetDistance(target, lastPositionTarget) > 3f) || (nxtUpdate >= Time.fixedDeltaTime))
		{
			lastPositionTarget = target;
			path = pathManager.FindPath(aiController.transform.position, target);
            nxtUpdate += 1f / aiController.updateRate;
			currentWP = 0;
		}

        if (currentWP >= path.Count)
        {
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
            pathEnded = true;
            return;
        }
    }

    public void Update()
    {

        Debug.Log("Search Update!");
        UpdatePath();
        if (pathEnded) return;

        Vector3 dir = (path[currentWP] - aiController.transform.position);
		
		var move = 0;
		if (dir.x < -.825f) move = -1;
		if (dir.x > .825f) move =  1;
		
		if (dir.y > 1) playerActions.Jump();

        playerActions.Move(move);

        CheckEnemies();
        CheckBullets();
    }

    private void CheckBullets()
    {
        return;
    }

    private void CheckEnemies()
    {
        foreach(var enemy in aiController.players)
        {
            var enemyPosition = enemy.transform.position;
            var enemyHeight = enemy.GetComponent<BoxCollider2D>().size.y;
            var myPosition = aiController.transform.position;
            var shootPosition = playerActions.shootSpawn.transform.position;

            //Check position y between the player and the enemy
            if ((shootPosition.y > enemyPosition.y - enemyHeight/4) && (shootPosition.y < enemyPosition.y + enemyHeight / 4))
            {
                var dir = (enemyPosition.x - myPosition.x > 0) ? Vector2.right : Vector2.left;
                playerActions.Rotate(dir.x);
                var hit = Physics2D.Raycast(shootPosition, dir);
                if (hit != null)
                {
                    if (hit.collider.tag != "Player") return;
                    aiController.attackTarget = hit.collider.gameObject;
                    aiController.ChangeState(StatesEnum.Attack);
                }
            }
        }
    }

    public StatesEnum StateTransition(StatesEnum nextState)
    {
		return nextState;
    }

    private Vector3 ChooseNewTarget()
    {
        var total = aiController.players.Count;
		var random = new System.Random();

		var value = random.Next(total);

		return aiController.players[value].transform.position;
    }
}
