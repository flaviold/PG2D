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

    public float updateRate = 2f;

    public float WPDistanceX = 2;
    public float WPDistanceYMax = 1f;
    public float WPDistanceYMin = -.1f;

    void Start()
    {
        enemyStateMachine = new EnemyStateMachine(this);
    }

    void FixedUpdate()
    {
        enemyStateMachine.State.Update();
    }

    public void ChangeState(StatesEnum state, GameObject gameObject)
    {
        switch (state)
        {
            case StatesEnum.Attack:
                if (enemyStateMachine.currentState == StatesEnum.Attack) return;
                attackTarget = gameObject;
                break;
        }
        enemyStateMachine.MoveNext(state);
    }
}
