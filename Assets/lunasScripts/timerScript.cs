using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timerScript : MonoBehaviour
{   [SerializeField]
    TextMeshProUGUI secText, minText;
    int min;
    float sec;
    void Start()
    {

    }

    void Update()
    {
        sec += Time.deltaTime;
        if (sec > 60)
        {
            min++;
            sec = 0;
        }
        if (sec >= 10)
            secText.text = ((int)sec).ToString();
        else
            secText.text = "0" + ((int)sec).ToString();
        if (min >= 10)
            minText.text = min.ToString() + ":";
        else
            minText.text = "0" + min.ToString() + ":";
    }
}
