using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankText_TMP : MonoBehaviour
{
    // 랭킹을 표시하는 TextMeshProUGUI 요소
    public static TextMeshProUGUI rankText;

    // Awake 함수는 스크립트가 활성화될 때 호출되는 함수
    void Awake()
    {
        // TextMeshProUGUI 요소를 가져와서 rankText 변수에 할당
        rankText = GetComponent<TextMeshProUGUI>();
    }

    // 랭킹 정보를 업데이트하고 화면에 표시하는 함수
    public static void RankScoreText()
    {
        // 화면 초기화
        rankText.text = "";

        // 반환된 리스트를 사용하여 상위 10명의 랭킹 정보를 화면에 표시
        Server.Instance().GetScoreList((message) =>
        {
            for (int i = 0; i < message.scores.Count; i++)
            {
                // 0점 이하의 점수가 나오면 더 이상 표시하지 않음
                if (message.scores[i] == 0)
                    break;

                // 랭킹 정보를 화면에 추가
                rankText.text += message.rank[i] + "# : " + message.users[i] + "  " + message.scores[i].ToString("D8") + "\n";
            }
        });
    }
}
