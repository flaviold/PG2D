using System;

public class AttackState : IState
{
    public void Update()
    {
        throw new NotImplementedException();
    }

    public StatesEnum StateTransition(StatesEnum nextState)
    {
        return nextState;
    }
}