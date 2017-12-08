using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomEventTimer : MonoBehaviour
{
    public string seconds;
    GameObject gomgr;
    Randevent re;
    public Text tText;
    // Use this for initialization
    void Start()
    {
        gomgr = GameObject.Find("mgrGame");
        re = gomgr.GetComponent<Randevent>();
    }

    // Update is called once per frame
    void Update()
    {
        seconds = re.tstring;
        tText.text = re.tstring;
    }
}