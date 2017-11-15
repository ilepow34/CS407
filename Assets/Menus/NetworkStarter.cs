using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkStarter : MonoBehaviour
{
    void Start()
    {
        GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
        GameObject lobbyManagerObj = GameObject.Find("LobbyManager");
        LobbyManager lobbyManager = lobbyManagerObj.GetComponent<LobbyManager>();
        NetworkData networkData = Toolbox.RegisterComponent<NetworkData>();
        if (gameStaticData.isHost) {
            Debug.Log("IsHost");
            networkData.client = lobbyManager.StartHost();    
        } else {
            Debug.Log("Isnt Host");
            networkData.client = lobbyManager.StartClient();
            
        }
    }

}
