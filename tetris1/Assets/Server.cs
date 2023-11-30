//아래 세 개는 그냥 기본이라고 생각하면 좋다.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

using WebSocketSharp;//웹 소켓 라이브러리를 사용한다

[System.Serializable]
public class ServerMessage
{
    public string type;
    public int highScore;
    public bool result;
    public List<int> scores;
    public List<string> users;
    public List<int> rank;
}

[System.Serializable]
public class User
{
    public string UserID;
    public string UserPW;
    public string type;
    public int Score;
}

public class Server : MonoBehaviour
{
    public static ServerMessage serverMessage;
    public static string userID;
    private WebSocket ws;//소켓 선언

    private static Server instance;
    Action<ServerMessage> callback;
    ServerMessage messaage;
    public static Server Instance()
    {
        if( instance == null)
        {
            GameObject serverObject = new GameObject("ServerObject");
            instance = serverObject.AddComponent<Server>();
            GameObject.DontDestroyOnLoad(serverObject);
        }

        return instance;
    }

    void Awake()
    {
        try
        {
            ws = new WebSocket("ws://127.0.0.1:1337");// 127.0.0.1은 본인의 아이피 주소이다. 1337포트로 연결한다는 의미이다.
            ws.OnMessage += ws_OnMessage; //서버에서 유니티 쪽으로 메세지가 올 경우 실행할 함수를 등록한다.
            ws.OnOpen += ws_OnOpen;//서버가 연결된 경우 실행할 함수를 등록한다
            ws.OnClose += ws_OnClose;//서버가 닫힌 경우 실행할 함수를 등록한다.
            ws.Connect();//서버에 연결한다.
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    private void Update()
    {
        if (this.messaage != null)
        {
            this.callback.Invoke(this.messaage);
            this.messaage = null;
        }
    }
    public void SaveScore(int score)
    {
        // SaveScore 메시지에 해당하는 JSON 데이터 생성
        User saveScoreData = new User
        {
            type = "SaveScore",
            UserID = userID,
            Score = score
        };
        // JSON을 문자열로 변환하여 서버로 보냄
        ws.Send(JsonUtility.ToJson(saveScoreData));
    }
    public void GetScoreList(Action<ServerMessage> callback)
    {
        User getScoreList = new User
        {
            type = "GetScoreList"
        };
        this.callback = callback;
        ws.Send(JsonUtility.ToJson(getScoreList));
    }
    public void GetHighScore(Action<ServerMessage> callback)
    {
        User getHighScore = new User
        {
            type = "GetHighScore"
        };

        this.callback = callback;
        ws.Send(JsonUtility.ToJson(getHighScore));
    }

    public void SignIn(string ID, string PW, Action<ServerMessage> callback)
    {
        userID = ID;

        User signInData = new User
        {
            type = "SignIn",
            UserID = ID,
            UserPW = PW
        };

        this.callback = callback;
        ws.Send(JsonUtility.ToJson(signInData));
    }
    public void SignUp(string ID, string PW)
    {
        User signUpData = new User
        {
            type = "SignUp",
            UserID = ID,
            UserPW = PW
        };
        ws.Send(JsonUtility.ToJson(signUpData));
    }
    void ws_OnMessage(object sender, MessageEventArgs e)
    {
        serverMessage = JsonUtility.FromJson<ServerMessage>(e.Data);
        this.messaage = serverMessage;
    }
    void ws_OnOpen(object sender, System.EventArgs e)
    {
        Debug.Log("open"); //디버그 콘솔에 "open"이라고 찍는다.
    }
    void ws_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("close"); //디버그 콘솔에 "close"이라고 찍는다.
    }
}

