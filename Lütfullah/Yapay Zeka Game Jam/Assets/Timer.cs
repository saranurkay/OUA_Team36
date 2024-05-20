using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    void Update()
    {
        if( remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        if (remainingTime < 60)
        {
            timerText.color = Color.red;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }
        
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
