using System.Collections.Generic;
using UnityEngine;

public class ArenaState : IGameState
{
	private GameManager gameManager;

	public ArenaState(GameManager gm)
	{
		gameManager = gm;
	}

	public void Start()
	{
		EnablePlayersInput();
	}
    
	public void Update()
	{

    }

	private void EnablePlayersInput()
	{
		gameManager.players.ForEach(delegate(ArenaPlayer ap) 
		{
			if (ap.human){
				ap.playerObject.AddComponent<PlayerController>();
			}
			else
			{
				ap.playerObject.AddComponent<AIController>();
			}
		});
	}
}
