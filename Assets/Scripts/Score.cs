using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    public float score;
    public int displayScore;

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
        //heldTime += Time.deltaTime;
        //if (heldTime >= 1)
        //{
        //    score += (int)heldTime;
        //    heldTime -= (int)heldTime;
        //}
        //score += pointIncreasePerSecond * Time.deltaTime;
        //scoreUI.text = score.ToString();
        pointIncreasePerSecond += Time.deltaTime;
        if (pointIncreasePerSecond >= 1)
        {
            score += (int)pointIncreasePerSecond;
            pointIncreasePerSecond -= (int)pointIncreasePerSecond;
            scoreUI.text = "SCORE: " + score.ToString();
        }
    }

    //private IEnumerator ScoreUpdater()
    //{
    //    while (true)
    //    {
    //        //if (displayScore < score)
    //        //{
    //            displayScore++;
    //            scoreUI.text = displayScore.ToString();
    //        //}
    //        yield return new WaitForSeconds(1.0f); 
    //    }
    //}
}
