using System;

public class DefendState : IState
{
    public void Update()
    {
        throw new NotImplementedException();
	}

	public EnemyStatesEnum StateTransition(EnemyStatesEnum nextState)
    {
        return nextState;
    }
}