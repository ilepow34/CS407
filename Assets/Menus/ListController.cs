using System;
using System.Collections;
using UnityEngine;

public class ListController : MonoBehaviour
{

    public GameObject ContentPanel;
    public GameObject ListItemPrefab;


    ArrayList GameListings;
    
    void Start()
    {
        GameListings = new ArrayList()
        {
            new GameListing("Open1", "1"),
            new GameListing("Open2", "2"),
            new GameListing("Open3", "3"),
            new GameListing("Open4", "4"),
            new GameListing("Open5", "5"),
            new GameListing("Open6", "6"),
            new GameListing("Open7", "7"),
            new GameListing("Open8", "8"),
            new GameListing("Open9", "9"),
            new GameListing("Open10", "10"),
            new GameListing("Open11", "11"),
            new GameListing("Open12", "12"),
            new GameListing("Open13", "13"),
            new GameListing("Open14", "14"),
            new GameListing("Open15", "15"),
            new GameListing("Open16", "16"),
            new GameListing("Open17", "17"),
            new GameListing("Open18", "18"),
            new GameListing("Open19", "19"),
            new GameListing("Open20", "20"),
            new GameListing("Open21", "21"),
            new GameListing("Open22", "22"),
            new GameListing("Open23", "23"),
            new GameListing("Open24", "24"),
            new GameListing("Open25", "25"),
            new GameListing("Open26", "26"),
            new GameListing("Open27", "27"),
            new GameListing("Open28", "28"),
            new GameListing("Open29", "29"),
            new GameListing("Open30", "30")
        };

        int i = 0;
        foreach (GameListing game in GameListings)
        {
            GameObject newGame = Instantiate(ListItemPrefab) as GameObject;
            ListItemController controller = newGame.GetComponent<ListItemController>();
            controller.name.text = game.name;
            controller.numPlayers.text = game.numPlayers;

            newGame.transform.SetParent(ContentPanel.transform, false);

            newGame.transform.localScale = Vector3.one;
            i++;
        }

    }

}
