using System;

public class DefendState : IState
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