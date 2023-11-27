using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankText : MonoBehaviour
{
    // ��ŷ�� ǥ���ϴ� Text UI ���
    public static Text rankText;

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

        // rankText�� null�� �ƴ��� Ȯ��
        if (rankText != null)
        {
            // DB���� ��� ������ ����� ����� ������
            List<int> allScores = DB.GetAllScores();
            List<string> allUsers = DB.GetAllUsers();

            // ��ȯ�� ����Ʈ�� ����Ͽ� ���� 10���� ��ŷ ������ ȭ�鿡 ǥ��
            for (int i = 0; i < 10 && i < allScores.Count; i++)
            {
                // 0�� ������ ������ ������ �� �̻� ǥ������ ����
                if (allScores[i] == 0)
                    break;

                // ��ŷ ������ ȭ�鿡 �߰�
                rankText.text += (i + 1) + "# : " + allUsers[i] + "  " + allScores[i].ToString("D8") + "\n";
            }
        }
    }
}
