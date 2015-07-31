using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


public class Server : MonoBehaviour {

public void JoinServer(string serverIp, int port)
	{
		curStatus = "connecting to " + serverIp+":"+port;
		Network.Connect(serverIp, port);
	}
public void JoinServer(string serverIp, int port, string password)
	{
		curStatus = "connecting to " + serverIp+":"+port;
		Network.Connect(serverIp, port, password);
	}
public void StartServer(int serverPort, int maxPlayers)
	{
		curStatus = "Starting server";
		Network.incomingPassword = "pass";
		Network.InitializeServer(MaxPlayers, port, !Network.HavePublicAddress());
	}
	public void StartServer(int serverPort, int maxPlayers, string password)
	{
		curStatus = "Starting server";
		Network.incomingPassword = password;
		Network.InitializeServer(MaxPlayers, port, !Network.HavePublicAddress());
	}
public void AntiHack(string name)
	{
		AddServerLog(name + " has been banned for using mods.")
		Network.CloseConnection(NetPlayers[PlayerNames.IndexOf(name)], true);
	}
	public void KickPlayer(string name)
	{
		AddServerLog(name + " has been kicked by a moderator.");
		Network.CloseConnection(NetPlayers[PlayerNames.IndexOf(name)], true);
	}
void OnPlayerConnected( NetworkPlayer player)
	{
		networkView.RPC("UpdateServerState", RPCMode.AllBuffered, (int)curState);
		
		if(peerType == NetworkPeerType.Client)
		{
			PlayerNames.Clear();
		}
	}
	void OnDisconnectedFromServer( NetworkDisconnection error)
	{
		curStatus = "Lost connection: "+ error;
		camera.enabled = true;
		gameObject.GetComponent<AudioListener>().enabled = true;
		CleanUp();
	}
//Add the whole file later?


}
