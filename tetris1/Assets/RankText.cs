using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankText : MonoBehaviour
{
    // 랭킹을 표시하는 Text UI 요소
    public static Text rankText;

    // Awake 함수는 스크립트가 활성화될 때 호출되는 함수
    void Awake()
    {
        // Text UI 요소를 가져와서 rankText 변수에 할당
        rankText = GetComponent<Text>();
    }

    // 랭킹 정보를 업데이트하고 화면에 표시하는 함수
    public static void RankScoreText()
    {
        // 화면 초기화
        rankText.text = "";

        // rankText가 null이 아닌지 확인
        if (rankText != null)
        {
            // DB에서 모든 점수와 사용자 목록을 가져옴
            List<int> allScores = DB.GetAllScores();
            List<string> allUsers = DB.GetAllUsers();

            // 반환된 리스트를 사용하여 상위 10명의 랭킹 정보를 화면에 표시
            for (int i = 0; i < 10 && i < allScores.Count; i++)
            {
                // 0점 이하의 점수가 나오면 더 이상 표시하지 않음
                if (allScores[i] == 0)
                    break;

                // 랭킹 정보를 화면에 추가
                rankText.text += (i + 1) + "# : " + allUsers[i] + "  " + allScores[i].ToString("D8") + "\n";
            }
        }
    }
}
