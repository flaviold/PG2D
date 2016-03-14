using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreArenaState : IGameState
{
	private GameManager gameManager;

	private float startBattleTime;

	public PreArenaState(GameManager gm)
	{
		gameManager = gm;
	}

	public void Start()
	{
		if (SceneManager.GetActiveScene().name != "Arena" + gameManager.currentArenaNumber)
		{
			SceneManager.LoadScene("Arena" + gameManager.currentArenaNumber);
		}
	}

	public void Update()
	{
		if (SceneManager.GetActiveScene().name != "Arena" + gameManager.currentArenaNumber) return;
		if (!SceneManager.GetActiveScene().isLoaded) return;

		if (gameManager.currentRound == null || gameManager.currentRound == 0)
		{
			startBattleTime = Time.fixedTime + gameManager.startGameDelay;
			FetchSpawnPoints();
			AddPlayers();
			gameManager.currentRound = 1;
		}

		if (Time.fixedTime > startBattleTime)
		{
			gameManager.ChangeState(GameStatesEnum.Arena);
		}
	}

	private void FetchSpawnPoints()
	{
		var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		gameManager.spawnPointsL = new List<GameObject>();
		gameManager.spawnPointsR = new List<GameObject>();

		foreach (var sp in spawnPoints)
		{
			if (sp.name == "L")
			{
				gameManager.spawnPointsL.Add(sp);	
			} 
			else if (sp.name == "R")
			{
				gameManager.spawnPointsR.Add(sp);
			}
		}
	}

	void AddPlayers()
	{
		var lSpawn = gameManager.spawnPointsL[Random.Range(0, gameManager.spawnPointsL.Count)];
		var rSpawn = gameManager.spawnPointsR[Random.Range(0, gameManager.spawnPointsR.Count)];

		GameObject humanSP;
		GameObject computerSP;

		gameManager.players = new List<ArenaPlayer>();
		if (Random.Range(0, 1) == 1)
		{
			humanSP = lSpawn;
			computerSP = rSpawn;
		}
		else
		{
			humanSP = lSpawn;
			computerSP = rSpawn;
		}

		var human = new ArenaPlayer 
		{
			human = true,
			score = 0,
			playerObject = (GameObject)GameObject.Instantiate(Resources.Load("Player1"), 
				lSpawn.transform.position, 
				lSpawn.transform.rotation)
		};
		human.playerObject.transform.SetParent(humanSP.transform);
		human.playerObject.transform.localScale = humanSP.transform.localScale;

		var computer = new ArenaPlayer 
		{
			human = false,
			score = 0,
			playerObject = (GameObject) GameObject.Instantiate(Resources.Load("Player1"), 
				rSpawn.transform.position, 
				rSpawn.transform.rotation)
		};
		computer.playerObject.transform.SetParent(computerSP.transform);
		computer.playerObject.transform.localScale = humanSP.transform.localScale;

		gameManager.players.Add(human);
		gameManager.players.Add(computer);
	}
}
