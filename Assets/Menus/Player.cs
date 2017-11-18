using UnityEngine;

public class LobbyPlayer : MonoBehaviour
{
    public string name;
    public int faction;
    public int playerNumber;
    
    public GameObject player;

    // keep this object alive always
    void Awake()
    {
        player = GameObject.Find("PLAYER"+playerNumber);
        if (player == null)
        {
            player = this.gameObject;
            player.name = "PLAYER"+playerNumber;
            DontDestroyOnLoad(player);
        } else 
        {
            if (this.gameObject.name != "PLAYER"+playerNumber)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
