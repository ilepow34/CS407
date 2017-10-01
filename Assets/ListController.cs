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
                        new GameListing("Open2", "2"),
            new GameListing("Open2fvfffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),
            new GameListing("Open2", "2"),

            new GameListing("Open3", "3")
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
