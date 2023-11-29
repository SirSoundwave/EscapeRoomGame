using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameEvent loseEvent;
    private float currentTime;
    public float startingTime = 90f;
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {  
        if(currentTime <= 0 && !gameOver)
        {
            currentTime = 0;
            loseEvent.Raise(this, null);
            gameOver = true;
        }

        else if(currentTime > 0 && !gameOver) {
            currentTime -= Time.deltaTime;
            displayTime(currentTime);
        }
    }

    void displayTime(float currentTime)
    {
        int minutes = (int)(currentTime / 60);
        float seconds = ((currentTime / 60) - minutes) * 60;

        if(minutes == 0 && seconds <= 30)
        {
            countdownText.color = Color.red;
        }

        if(seconds < 10)
        {
            countdownText.text = minutes + ":" + "0" + (int)seconds;
        }

        else
        {
            countdownText.text = minutes + ":" + (int)seconds;
        }
    }

    public void StopTimer()
    {
        gameOver = true;
    }

}
