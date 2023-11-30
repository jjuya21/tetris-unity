using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    // ������ ǥ���ϴ� Text UI ���
    public static Text scoreText;
    public static int highscore;

    // ���� �� ȣ��Ǵ� �Լ�
    void Start()
    {
        // Text UI ��Ҹ� �����ͼ� scoreText ������ �Ҵ�
        scoreText = GetComponent<Text>();
        UpdateScoreText();
    }

    // ������ ������Ʈ�ϰ� ȭ�鿡 ǥ���ϴ� �Լ�
    public static void UpdateScoreText()
    {
        Server.Instance().GetHighScore( (message) =>
        {
            highscore = message.highScore;
            int score = Playfield.GetScore();
            scoreText.text = "Score: " + score.ToString() + "\nHighScore : " + highscore.ToString();
        });
    }
}
