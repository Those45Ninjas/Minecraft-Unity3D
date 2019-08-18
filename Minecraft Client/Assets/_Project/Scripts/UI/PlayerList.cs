﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
	public GameObject PlayerListPanel;
	public KeyCode PlayerListKey = KeyCode.Tab;

	public PlayerEntry PlayerEntryPrefab;

	// Start is called before the first frame update
	void Start()
	{
		PlayerListPanel.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(PlayerListKey))
		{
			PlayerListPanel.SetActive(true);
		}
		else if (Input.GetKeyUp(PlayerListKey))
		{
			PlayerListPanel.SetActive(false);
		}
	}

	/// <summary>
	/// Updates the in game player list (tab menu) with a new collection of players
	/// </summary>
	/// <param name="players"></param>
	public void UpdatePlayerList(Player[] players)
	{
		Debug.Log($"adding {players.Length} players");
	}
}
