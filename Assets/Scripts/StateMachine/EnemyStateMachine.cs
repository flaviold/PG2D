using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyStateMachine : BaseStateMachine<StatesEnum>
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
            new AttackState(),
            new DefendState()
        };
        currentState = StatesEnum.Search;

        transitions = new Dictionary<StateTransition, StatesEnum>
        {
            { new StateTransition(StatesEnum.Search, StatesEnum.Attack), states[(int)StatesEnum.Search].StateTransition(StatesEnum.Attack) },
            { new StateTransition(StatesEnum.Search, StatesEnum.Defend), states[(int)StatesEnum.Search].StateTransition(StatesEnum.Defend) },
            { new StateTransition(StatesEnum.Attack, StatesEnum.Search), states[(int)StatesEnum.Attack].StateTransition(StatesEnum.Search) },
            { new StateTransition(StatesEnum.Attack, StatesEnum.Defend), states[(int)StatesEnum.Attack].StateTransition(StatesEnum.Defend) },
            { new StateTransition(StatesEnum.Defend, StatesEnum.Search), states[(int)StatesEnum.Defend].StateTransition(StatesEnum.Search) },
            { new StateTransition(StatesEnum.Defend, StatesEnum.Attack), states[(int)StatesEnum.Defend].StateTransition(StatesEnum.Attack) },
        };
    }
}
