using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankText : MonoBehaviour
{
    public static Text rankText;
    void Awake()
    {
        rankText = GetComponent<Text>();
    }

    public static void RankScoreText()
    {
        if (rankText != null)
        {
            List<int> allScores = DB.GetAllScores();
            List<string> allUsers = DB.GetAllUsers(); 
            // ��ȯ�� ����Ʈ�� ���
            for (int i = 0; i < 10 && i < allScores.Count; i++)
            {
                if (allScores[i] == 0)
                    break;
                rankText.text += (i + 1) + "# : " + allUsers[i] + "  " + allScores[i].ToString("D8") + "\n";
            }
        }
    }
}
