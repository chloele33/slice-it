using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    public static float score;
    public float displayScore;

    public Text scoreUI;
    private float pointIncreasePerSecond;

    // Start is called before the first frame update
    void Start()
    {
        score = 0f;
        displayScore = 0;
        //StartCoroutine(ScoreUpdater());
        pointIncreasePerSecond = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (System.Math.Abs(displayScore - score) > 1)
        {
            displayScore = score;
            scoreUI.text = "SCORE: " + displayScore.ToString();
        }

        pointIncreasePerSecond += Time.deltaTime;
        if (pointIncreasePerSecond >= 1)
        {
            score += (int)pointIncreasePerSecond;
            displayScore = score;
            pointIncreasePerSecond -= (int)pointIncreasePerSecond;
            scoreUI.text = "SCORE: " + displayScore.ToString();
        }
    }
}
