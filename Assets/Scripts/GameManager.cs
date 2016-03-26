using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

public class GameManager : MonoBehaviour {

	private static GameManager manager = null;
	public static GameManager Manager
	{
		get { return manager; }
	}

	private GameStateMachine gameStateMachine;

	public int currentArenaNumber;
	public float startGameDelay = 5f;

	public List<GameObject> spawnPointsL;
	public List<GameObject> spawnPointsR;
	public List<ArenaPlayer> players;

	public int maxRounds = 5;
	[HideInInspector]public int currentRound;

	void Awake()
	{
		if (manager != null && manager != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			manager = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start()
	{
		gameStateMachine = new GameStateMachine(this);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		gameStateMachine.State.Update();
	}

	public void ChangeState(GameStatesEnum state)
	{
		gameStateMachine.MoveNext(state);
		gameStateMachine.State.Start();
	}

	public void GameManagerFunctionCall(string info)
	{
		string func = null;
		object[] parameters = GetParameters(info, out func);
		Type t = gameStateMachine.State.GetType();
		MethodInfo mi = t.GetMethod(func);
		if (mi == null) return;
		mi.Invoke(gameStateMachine.State, parameters);
	}

	private object[] GetParameters(string strParam, out string funcName) 
	{
		string[] values = strParam.Split(';');
		object[] retObj = new object[values.Length - 1];
		funcName = values[0];
		for (var i = 1; i < values.Length; i++)
		{
			string[] par = values[i].Split(':');
			Type t = Type.GetType(par[1]);
			retObj[i - 1] = Convert.ChangeType(par[0], t);
		}

		return retObj;
	}
}

public class ArenaPlayer {
	public GameObject playerObject;
	public bool human;
	public int score;
	public GameObject indicator;
}