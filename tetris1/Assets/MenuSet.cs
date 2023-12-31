using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSet : MonoBehaviour
{
    // 게임 오브젝트들을 Inspector에서 할당하기 위한 변수들
    public GameObject rankMenuSet;
    public GameObject mainMenuSet;
    public GameObject subMenuSet;
    public GameObject userMenuSet;
    public GameObject errorMenuSet;
    public GameObject noticationMenuSet;

    // 로그인 입력 필드와 버튼
    public InputField inputField_ID;
    public InputField inputField_PW;
    public Button Button_Login;

    public GameObject wrongID_Error;
    public GameObject nullID_Error;
    public GameObject duplicationID_Error;


    
    public void NewHighScore()
    {
        noticationMenuSet.SetActive(true);
    }

    // 로그인 버튼 클릭 시 호출되는 함수
    public void SignInButtonClick()
    {
        // DB에서 ID와 PW를 확인하여 로그인 성공 시 메뉴를 전환
        Server.Instance().SignIn(inputField_ID.text, inputField_PW.text, (message) =>
        {
            if (message.result)
            {
                userMenuSet.SetActive(false);
                mainMenuSet.SetActive(true);
            }
            else
            {
                errorMenuSet.SetActive(true);
                wrongID_Error.SetActive(true);
            }
        });
    }

    // 회원가입 버튼 클릭 시 호출되는 함수
    public void SignUpButtonClick()
    {
        if (inputField_ID.text.Replace(" ", "") == "")
        {
            errorMenuSet.SetActive(true);
            nullID_Error.SetActive(true);
        }
        else
            Server.Instance().SignUp(inputField_ID.text, inputField_PW.text, (message) =>
            {
                if (message.result == false)
                {
                    duplicationID_Error.SetActive(true);
                    errorMenuSet.SetActive(true);
                }
            });
    }

    public void LogoutButtonClick()
    {
        userMenuSet.SetActive(true);
        mainMenuSet.SetActive(false);
    }

    // 초기화 함수
    void Start()
    {
        // 게임 일시 정지
        Time.timeScale = 0f;

    }

    // 업데이트 함수
    void Update()
    {
        // 게임이 진행 중이면서 취소 키가 눌렸을 때 서브 메뉴를 표시
        if (Time.timeScale > 0f)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                subMenuSet.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    // 계속하기 버튼 클릭 시 호출되는 함수
    public void Continue()
    {
        subMenuSet.SetActive(false);
        Time.timeScale = 1f;
    }

    // 메인 메뉴 설정 함수
    public void SetMainMenu()
    {
        mainMenuSet.SetActive(true);
        subMenuSet.SetActive(false);
        Playfield.GameOver();
    }

    // 메인 메뉴 해제 함수
    public void OffMainMenu()
    {
        mainMenuSet.SetActive(false);
    }

    // 게임 시작 함수
    public void StartGame()
    {
        ScoreText.UpdateScoreText();
        Time.timeScale = 1f;
        mainMenuSet.SetActive(false);
    }

    // 랭크 메뉴 설정 함수
    public void SetRankMenu()
    {
        rankMenuSet.SetActive(true);
        RankText_TMP.RankScoreText();
    }
}
