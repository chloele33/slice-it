using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
	public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
		int score = PlayerPrefs.GetInt("Score");
		scoreText.text = "SCORE: " + score;
    }
}
