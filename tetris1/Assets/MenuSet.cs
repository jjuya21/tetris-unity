using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSet : MonoBehaviour
{
    public GameObject rankMenuSet;
    public GameObject mainMenuSet;
    public GameObject subMenuSet;
    public GameObject userMenuSet;

    public InputField inputField_ID;
    public InputField inputField_PW;
    public Button Button_Login;


    public void SignInButtonClick()
    {
        if (DB.IdCheck(inputField_ID.text, inputField_PW.text))
        {
            userMenuSet.SetActive(false);
            mainMenuSet.SetActive(true);
        }
    }

    public void SignUpButtonClick()
    {
        DB.SignUp(inputField_ID.text, inputField_PW.text);
    }

    void Start()
    {
        Time.timeScale = 0f;
    }
    void Update()
    {
        if (Time.timeScale > 0f) 
        {
            if (Input.GetButtonDown("Cancel"))
            {
                subMenuSet.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void Continue()
    {
        subMenuSet.SetActive(false);
        Time.timeScale = 1f;
    }
    public void SetMainMenu()
    {
        mainMenuSet.SetActive(true);
        subMenuSet.SetActive(false);
        Playfield.GameOver();
    }
    public void OffMainMenu()
    {
        mainMenuSet.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        mainMenuSet.SetActive(false);
    }
    public void SetRankMenu()
    {
        rankMenuSet.SetActive(true);
        RankText.RankScoreText();
    }
}
