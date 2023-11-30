using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankText_TMP : MonoBehaviour
{
    // ��ŷ�� ǥ���ϴ� TextMeshProUGUI ���
    public static TextMeshProUGUI rankText;

    // Awake �Լ��� ��ũ��Ʈ�� Ȱ��ȭ�� �� ȣ��Ǵ� �Լ�
    void Awake()
    {
        // TextMeshProUGUI ��Ҹ� �����ͼ� rankText ������ �Ҵ�
        rankText = GetComponent<TextMeshProUGUI>();
    }

    // ��ŷ ������ ������Ʈ�ϰ� ȭ�鿡 ǥ���ϴ� �Լ�
    public static void RankScoreText()
    {
        // ȭ�� �ʱ�ȭ
        rankText.text = "";

        // ��ȯ�� ����Ʈ�� ����Ͽ� ���� 10���� ��ŷ ������ ȭ�鿡 ǥ��
        Server.Instance().GetScoreList((message) =>
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
