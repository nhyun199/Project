using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timeText;
    public Text gameOverTimeText;
    public Canvas NewGameCanvas, GoTitleCanvas, GameOverCanvas;

    private float elapsedTime = 0f;
    private float gameOverTime = 0f;
    private bool isPaused = false;

    void Update()
    {
        if (GameOverCanvas.enabled ||
            NewGameCanvas.enabled ||
            GoTitleCanvas.enabled)
        {
            isPaused = true;
        }

        else if (isPaused)
        {
            isPaused = false;
        }


        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimeText(timeText, elapsedTime);
        }

        if (GameOverCanvas.enabled && gameOverTime == 0f)
        {
            gameOverTime = elapsedTime;
            UpdateTimeText(gameOverTimeText, gameOverTime);
        }
    }

    private void UpdateTimeText(Text text, float time)
    {
        TimeSpan timespan = TimeSpan.FromSeconds(time);
        text.text = string.Format("{0:D2}:{1:D2}", timespan.Minutes, timespan.Seconds);
    }
}
