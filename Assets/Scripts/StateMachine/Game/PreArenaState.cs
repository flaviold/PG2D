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

		gameManager.players.Add(ConfigureArenaPlayer(true, humanSP));
		gameManager.players.Add(ConfigureArenaPlayer(false, computerSP));
	}

	private ArenaPlayer ConfigureArenaPlayer(bool human, GameObject spawn)
	{
		var indicator = "";
		var model = "";
		if (human)
		{
			indicator = "IndicatorJ";
			model = "Player";
		}
		else
		{
			indicator += "IndicatorC";
			model = "AIPlayer";
		}
			
		var scale = Vector2.one;

		if (spawn.transform.position.x != 0) 
		{
			scale.x = -(spawn.transform.position.x / Mathf.Abs(spawn.transform.position.x));
		}

		var player = new ArenaPlayer 
		{
			human = human,
			score = 0,
			playerObject = (GameObject)GameObject.Instantiate(Resources.Load(model), 
															  spawn.transform.position, 
															  spawn.transform.rotation),
		};
		player.playerObject.transform.localScale = scale;

		player.indicator = (GameObject)GameObject.Instantiate(Resources.Load(indicator),
															  Camera.main.WorldToScreenPoint(player.playerObject.transform.position),
															  player.playerObject.transform.rotation);
		player.indicator.transform.SetParent(GameObject.Find("Canvas").transform);
		player.indicator.GetComponent<Animator>().SetBool("Active", true);

		return player;
	}
}
