using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class MenuSet : MonoBehaviour
{
    // ���� ������Ʈ���� Inspector���� �Ҵ��ϱ� ���� ������
    public GameObject rankMenuSet;
    public GameObject mainMenuSet;
    public GameObject subMenuSet;
    public GameObject userMenuSet;

    // �α��� �Է� �ʵ�� ��ư
    public InputField inputField_ID;
    public InputField inputField_PW;
    public Button Button_Login;

    // �α��� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void SignInButtonClick()
    {
        // DB���� ID�� PW�� Ȯ���Ͽ� �α��� ���� �� �޴��� ��ȯ
        Server.Instance().SignIn(inputField_ID.text, inputField_PW.text, (message) =>
        {
            if (message.result == true)
            {
                userMenuSet.SetActive(false);
                mainMenuSet.SetActive(true);
            }
        });
    }

    // ȸ������ ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void SignUpButtonClick()
    {
        // DB�� ȸ������ ���� ����
        Server.Instance().SignUp(inputField_ID.text, inputField_PW.text);
    }

    public void LogoutButtonClick()
    {
        userMenuSet.SetActive(true);
        mainMenuSet.SetActive(false);
    }

    // �ʱ�ȭ �Լ�
    void Start()
    {
        // ���� �Ͻ� ����
        Time.timeScale = 0f;
    }

    // ������Ʈ �Լ�
    void Update()
    {
        // ������ ���� ���̸鼭 ��� Ű�� ������ �� ���� �޴��� ǥ��
        if (Time.timeScale > 0f)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                subMenuSet.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    // ����ϱ� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void Continue()
    {
        subMenuSet.SetActive(false);
        Time.timeScale = 1f;
    }

    // ���� �޴� ���� �Լ�
    public void SetMainMenu()
    {
        mainMenuSet.SetActive(true);
        subMenuSet.SetActive(false);
        Playfield.GameOver();
    }

    // ���� �޴� ���� �Լ�
    public void OffMainMenu()
    {
        mainMenuSet.SetActive(false);
    }

    // ���� ���� �Լ�
    public void StartGame()
    {
        Time.timeScale = 1f;
        mainMenuSet.SetActive(false);
    }

    // ��ũ �޴� ���� �Լ�
    public void SetRankMenu()
    {
        rankMenuSet.SetActive(true);
        RankText_TMP.RankScoreText();
    }
}
