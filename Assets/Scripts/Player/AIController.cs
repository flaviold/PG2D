using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerActions))]
[RequireComponent(typeof(PathFinderManager))]
public class AIController : MonoBehaviour
{
    private EnemyStateMachine enemyStateMachine;

    [HideInInspector]
    public GameObject attackTarget;
    [HideInInspector]
    public List<GameObject> players;

    public float updateRate = 2f;

    public float WPDistanceX = 2;
    public float WPDistanceYMax = 1f;
    public float WPDistanceYMin = -.1f;

    void Start()
    {
        updatePlayersList();
        enemyStateMachine = new EnemyStateMachine(this);
    }

    void FixedUpdate()
    {
        enemyStateMachine.State.Update();
        updatePlayersList();
    }

    private void updatePlayersList()
    {
        var mapPlayers = GameObject.FindGameObjectsWithTag("Player");

        if (players == null || players.Count != (mapPlayers.Length - 1))
        {
            players = new List<GameObject>();

            foreach (var gObj in mapPlayers)
            {
                if (gObj == gameObject) continue;
                players.Add(gObj);
            }
        }
    }

    public void ChangeState(EnemyStatesEnum state)
    {
        enemyStateMachine.MoveNext(state);
    }
}
