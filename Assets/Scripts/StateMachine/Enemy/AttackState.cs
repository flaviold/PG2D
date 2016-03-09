using System;
using UnityEngine;

public class AttackState : IState
{
    private AIController aiController;
    private PlayerActions playerActions;

    public AttackState(AIController aiController)
    {
        this.aiController = aiController;
        playerActions = aiController.GetComponent<PlayerActions>();
    }

    public void Update()
    {
        Debug.Log("Attack Update!");
        var dir = aiController.attackTarget.transform.position.x - aiController.transform.position.x;
        playerActions.Shoot();
		aiController.ChangeState(EnemyStatesEnum.Search);
    }

	public EnemyStatesEnum StateTransition(EnemyStatesEnum nextState)
    {
        return nextState;
    }
}