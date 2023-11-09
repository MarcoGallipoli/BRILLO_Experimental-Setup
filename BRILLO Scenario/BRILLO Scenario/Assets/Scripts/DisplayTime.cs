using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    public GameObject theDisplay;
    public int hours;
    public int minutes;
    public int seconds;
    public string minute;
    public string second;
    public string hour;
    void Update()
    {
        // Take the time from the system
        hours = System.DateTime.Now.Hour;
        minutes = System.DateTime.Now.Minute;
        seconds = System.DateTime.Now.Second;
        
        if (minutes < 10)
            minute = "0" + minutes;
        else
            minute = "" + minutes;
        if (seconds < 10)
            second = "0" + seconds;
        else
            second = "" + seconds;
        if (hours < 10)
            hour = "0" + hours;
        else
            hour = "" + hours;
        // the variable display shows the time
        theDisplay.GetComponent<TMP_Text>().text = hour + ":" + minute + ":" + second;
   
    }
}

