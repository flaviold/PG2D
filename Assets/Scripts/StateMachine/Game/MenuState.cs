using System.Collections.Generic;
using UnityEngine;

public class MenuState : IGameState
{
	private GameManager gameManager;

	public MenuState(GameManager gm) 
	{
		gameManager = gm;
	}

	public void Start()
	{
		
	}

    public void Update()
	{

    }

	public void CallArena(int number)
	{
		gameManager.currentArenaNumber = number;
		gameManager.ChangeState(GameStatesEnum.PreArena);
	}
}
