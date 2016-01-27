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
}
