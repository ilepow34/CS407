using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class LobbyManager : NetworkLobbyManager
{
    public override void OnLobbyClientConnect(NetworkConnection conn) {
        // send the server your player name
//        GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
//        conn.Send(1002, new StringMessage(gameStaticData.PlayerName));
        Debug.Log("Sending test msg");
        conn.Send(1002, new StringMessage("Test"));
    }

    public override void OnLobbyServerConnect(NetworkConnection conn) {
        Debug.Log("Registering handler");
        conn.RegisterHandler(1002, PlayerNameHandler);
    }

    public void PlayerNameHandler(NetworkMessage netMsg) {
        StringMessage msg = netMsg.ReadMessage<StringMessage>();
        Debug.Log(msg.value);
    }


    public void OnClientConnect(NetworkConnection conn) {
        Debug.Log("4334");
    }

    public void OnServerConnect(NetworkConnection conn) {
        Debug.Log("fdkfdkjfdkjfd");
    }


    void OnLobbyClientEnter() {
        Debug.Log("1");
    }

    public void OnLobbyStartClient(NetworkClient lobbyClient) {
        Debug.Log("2");
    }

    public void OnLobbyStartHost() {
        Debug.Log("3");
    }



}
