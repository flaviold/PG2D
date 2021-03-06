﻿using System.Collections.Generic;
using UnityEngine;

public class MovingState : IState
{
	private PathFinderManager pathManager;
	private PlayerActions playerActions;
	private List<Vector3> path;
	public bool pathEnded = false;

	private AIController aiController;

	private Vector3 target = new Vector3();
	private Vector3 lastPositionTarget;

	private float nxtUpdate = 0;

	private int currentWP = 0;

	public MovingState(AIController aiController)
	{
		this.aiController = aiController;
		playerActions = aiController.GetComponent<PlayerActions>();
		pathManager = aiController.GetComponent<PathFinderManager>();

		ChooseNewTarget();
		lastPositionTarget = target;
		nxtUpdate = 0;

	}

	void UpdatePath()
	{
		if ((target == null) || (pathEnded))
		{
			ChooseNewTarget();
			pathEnded = false;
		}
		if ((path == null) || (lastPositionTarget == null) || (pathManager.GetDistance(target, lastPositionTarget) > 3f) || Stuck())
		{
			lastPositionTarget = target;
			ChooseNewTarget();
			path = pathManager.FindPath(aiController.transform.position, target);
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

		Debug.Log("Moving Update!");
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
					aiController.ChangeState(EnemyStatesEnum.Attack);
				}
			}
		}
	}

	public EnemyStatesEnum StateTransition(EnemyStatesEnum nextState)
	{
		return nextState;
	}

	private void ChooseNewTarget()
	{
		var total = aiController.players.Count;
		var random = new System.Random();

		var value = random.Next(total);

		target.x = Random.Range(-39, 40);
		target.y = Random.Range(6, 55);
	}

	private bool Stuck()
	{
		if (currentWP >= path.Count) return true;
		Vector3 dir = (path[currentWP] - aiController.transform.position);
		return (dir.y > 1 && aiController.GetComponent<Rigidbody2D>().velocity.y < 0);
	}
}
