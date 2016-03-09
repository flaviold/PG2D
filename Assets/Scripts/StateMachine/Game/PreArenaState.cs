using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreArenaState : IGameState
{
	private GameManager gameManager;
	private bool startUp = true;

	private float startBattleTime;

	public PreArenaState(GameManager gm)
	{
		gameManager = gm;
	}

	public void Start()
	{
		startUp = (gameManager.currentRound == null || gameManager.currentRound == 0);
		if (SceneManager.GetActiveScene().ToString() != "Arena" + gameManager.currentArenaNumber)
		{
			SceneManager.LoadScene("Arena" + gameManager.currentArenaNumber);
		}
	}

	public void Update(){
		if (SceneManager.GetActiveScene().ToString() != "Arena" + gameManager.currentArenaNumber) return;
		if (!SceneManager.GetActiveScene().isLoaded) return;

		if (startUp)
		{
			startBattleTime = Time.fixedTime + gameManager.startGameDelay;
			FetchSpawnPoints();
			AddPlayers();
			startUp = false;
		}

		if (Time.fixedTime > startBattleTime)
		{
			gameManager.ChangeState(GameStatesEnum.Arena);
		}
	}

	private void FetchSpawnPoints()
	{
		var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		gameManager.spawnPoints = new List<GameObject>();

		foreach (var sp in spawnPoints)
		{
			gameManager.spawnPoints.Add();
		}
	}

	void AddPlayers()
	{
		
	}
}
