using System;

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
        var dir = aiController.attackTarget.transform.position.x - aiController.transform.position.x;
        playerActions.Rotate(dir);
        playerActions.Shoot(true);
    }

    public StatesEnum StateTransition(StatesEnum nextState)
    {
        return nextState;
    }
}