using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    // ������ ǥ���ϴ� Text UI ���
    public static Text scoreText;
    public static int highscore;
    public static int bestscore;
    // ���� �� ȣ��Ǵ� �Լ�
    void Start()
    {
        // Text UI ��Ҹ� �����ͼ� scoreText ������ �Ҵ�
        scoreText = GetComponent<Text>();
    }

    // ������ ������Ʈ�ϰ� ȭ�鿡 ǥ���ϴ� �Լ�
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
