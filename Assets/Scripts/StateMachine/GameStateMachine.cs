using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateMachine : BaseStateMachine<GameStatesEnum>
{
	public List<IGameState> states;

	public IGameState State
	{
		get { return states[(int)currentState]; }
	}

	public GameStateMachine(GameManager gm) : base()
	{
		states = new List<IGameState>
		{
			new MenuState(gm),
			new PreArenaState(gm),
			new ArenaState(gm),
			new PosArenaState(gm)
		};
		currentState = GameStatesEnum.Menu;

		transitions = new Dictionary<StateTransition, GameStatesEnum>
		{
			{ new StateTransition(GameStatesEnum.Menu, GameStatesEnum.PreArena), GameStatesEnum.PreArena },
			{ new StateTransition(GameStatesEnum.PreArena, GameStatesEnum.Arena), GameStatesEnum.Arena },
			{ new StateTransition(GameStatesEnum.Arena, GameStatesEnum.PosArena), GameStatesEnum.PosArena},
			{ new StateTransition(GameStatesEnum.PosArena, GameStatesEnum.PreArena), GameStatesEnum.PreArena},
			{ new StateTransition(GameStatesEnum.PosArena, GameStatesEnum.Menu), GameStatesEnum.Menu}
		};
	}
}
