using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public static Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateScoreText();
    }

    public static void UpdateScoreText()
    {
        int score = Playfield.GetScore();
        int highscore = DB.GetHighScore();

        scoreText.text = "Score: " + score.ToString() + "\nHighScore : " + highscore.ToString();
    }
}
