using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    // ������ ǥ���ϴ� Text UI ���
    public static Text scoreText;

    // ���� �� ȣ��Ǵ� �Լ�
    void Start()
    {
        // Text UI ��Ҹ� �����ͼ� scoreText ������ �Ҵ�
        scoreText = GetComponent<Text>();

        // �ʱ�ȭ �� ���� ������ ȭ�鿡 ǥ��
        UpdateScoreText();
    }

    // ������ ������Ʈ�ϰ� ȭ�鿡 ǥ���ϴ� �Լ�
    public static void UpdateScoreText()
    {
        // ���� ���� ���� �� �ְ� ������ ������
        int score = Playfield.GetScore();
        int highscore = DB.GetHighScore();

        // ȭ�鿡 ���� ������ �ְ� ������ ǥ��
        scoreText.text = "Score: " + score.ToString() + "\nHighScore : " + highscore.ToString();
    }
}
