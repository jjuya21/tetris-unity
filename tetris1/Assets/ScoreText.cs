using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    // 점수를 표시하는 Text UI 요소
    public static Text scoreText;

    // 시작 시 호출되는 함수
    void Start()
    {
        // Text UI 요소를 가져와서 scoreText 변수에 할당
        scoreText = GetComponent<Text>();

        // 초기화 후 현재 점수를 화면에 표시
        UpdateScoreText();
    }

    // 점수를 업데이트하고 화면에 표시하는 함수
    public static void UpdateScoreText()
    {
        // 현재 게임 점수 및 최고 점수를 가져옴
        int score = Playfield.GetScore();
        int highscore = DB.GetHighScore();

        // 화면에 현재 점수와 최고 점수를 표시
        scoreText.text = "Score: " + score.ToString() + "\nHighScore : " + highscore.ToString();
    }
}
