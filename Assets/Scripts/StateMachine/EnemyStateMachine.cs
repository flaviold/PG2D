using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyStateMachine : BaseStateMachine<EnemyStatesEnum>
{
    public List<IState> states;

    public IState State
    {
        get { return states[(int)currentState]; }
    }

    public EnemyStateMachine(AIController aiController) : base()
    {
        states = new List<IState>
        {
            new SearchState(aiController),
            new AttackState(aiController),
            new DefendState()
        };
        currentState = EnemyStatesEnum.Search;

        transitions = new Dictionary<StateTransition, EnemyStatesEnum>
        {
            { new StateTransition(EnemyStatesEnum.Search, EnemyStatesEnum.Attack), EnemyStatesEnum.Attack },
            { new StateTransition(EnemyStatesEnum.Search, EnemyStatesEnum.Defend), EnemyStatesEnum.Defend },
            { new StateTransition(EnemyStatesEnum.Attack, EnemyStatesEnum.Search), EnemyStatesEnum.Search },
            { new StateTransition(EnemyStatesEnum.Attack, EnemyStatesEnum.Defend), EnemyStatesEnum.Defend },
            { new StateTransition(EnemyStatesEnum.Defend, EnemyStatesEnum.Search), EnemyStatesEnum.Search },
			{ new StateTransition(EnemyStatesEnum.Defend, EnemyStatesEnum.Attack), EnemyStatesEnum.Attack },
        };
    }
}
