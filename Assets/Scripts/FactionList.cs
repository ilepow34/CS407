using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionList : MonoBehaviour
{
    public static ArrayList CurrentUnitsB;
    public static ArrayList CurrentUnitsR;
    // Use this for initialization
    void Start()
    {
        CurrentUnitsB = new ArrayList();
        CurrentUnitsR = new ArrayList();
        //InvokeRepeating("Update", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //yield return new WaitForSeconds(15.0f);
        // Thread.Sleep(15000);

        // System.Threading.Thread.Sleep(
        //(int)System.TimeSpan.FromSeconds(15).TotalMilliseconds);
        Debug.Log("Checking??");
        if (CurrentUnitsB.Count <= 0)
        {
            // Time.timeScale = 0.1f;
            //faction Blue wins
            Debug.Log("Red Wins!!!");

        }
        else if (CurrentUnitsR.Count <= 0)
        {
            //Time.timeScale = 0.1f;
            //Faction red wins 
            Debug.Log("Blue Wins!!!");
        }
    }
    public void addUnit(Unit newspawn)
    {
        if (newspawn.faction)
        {
            CurrentUnitsR.Add(newspawn);
        }
        else CurrentUnitsB.Add(newspawn);
    }
    public void removeUnit(Unit unt)
    {
        if (unt.faction)
        {
            CurrentUnitsR.Remove(unt);
        }
        else CurrentUnitsB.Remove(unt);
    }
}

