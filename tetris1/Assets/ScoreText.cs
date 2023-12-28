using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    // 점수를 표시하는 Text UI 요소
    public static Text scoreText;
    public static int highscore;
    public static int bestscore;
    // 시작 시 호출되는 함수
    void Start()
    {
        // Text UI 요소를 가져와서 scoreText 변수에 할당
        scoreText = GetComponent<Text>();
    }

    // 점수를 업데이트하고 화면에 표시하는 함수
    public static void UpdateScoreText()
    {
        Server.Instance().GetHighScore( (message) =>
        {
            highscore = message.highScore;
            Server.Instance().GetBestScore((message) =>
            {
                bestscore = message.bestScore;
                int score = Playfield.GetScore();
                if (score > highscore)
                    highscore = score;
                if (score > bestscore)
                    bestscore = score;
                scoreText.text = "Score: " + score.ToString() + "\nYourBestScore : " + bestscore.ToString() + "\nHighScore : " + highscore.ToString();
            });
        });
    }
}
