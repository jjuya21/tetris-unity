using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankText : MonoBehaviour
{
    // ��ŷ�� ǥ���ϴ� Text UI ���
    public static Text rankText;
    public static List<int> allScores;
    public static List<string> allUsers;

    // Awake �Լ��� ��ũ��Ʈ�� Ȱ��ȭ�� �� ȣ��Ǵ� �Լ�
    void Awake()
    {
        // Text UI ��Ҹ� �����ͼ� rankText ������ �Ҵ�
        rankText = GetComponent<Text>();
    }

    // ��ŷ ������ ������Ʈ�ϰ� ȭ�鿡 ǥ���ϴ� �Լ�
    public static void RankScoreText()
    {
        // ȭ�� �ʱ�ȭ
        rankText.text = "";

        // ��ȯ�� ����Ʈ�� ����Ͽ� ���� 10���� ��ŷ ������ ȭ�鿡 ǥ��
        Server.Instance().GetScoreList( (message) =>
        {
            for (int i = 0; i < message.scores.Count; i++)
            {
                // 0�� ������ ������ ������ �� �̻� ǥ������ ����
                if (message.scores[i] == 0)
                    break;

                // ��ŷ ������ ȭ�鿡 �߰�
                rankText.text += message.rank[i] + "# : " + message.users[i] + "  " + message.scores[i].ToString("D8") + "\n";
            }
        });
    }
}
