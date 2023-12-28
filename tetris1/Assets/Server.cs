//�Ʒ� �� ���� �׳� �⺻�̶�� �����ϸ� ����.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WebSocketSharp;//�� ���� ���̺귯���� ����Ѵ�

[System.Serializable]
public class ServerMessage
{
    public string type;
    public int highScore;
    public int bestScore;
    public bool result;
    public List<int> scores;
    public List<string> users;
    public List<int> rank;
    public int currentHighScore;
    public string user;
}

[System.Serializable]
public class User
{
    public string UserID;
    public string UserPW;
    public string type;
    public int Score;
    public float Playtime;
}

public class Server : MonoBehaviour
{
    
    public static ServerMessage serverMessage;
    public static string userID;
    private WebSocket ws;//���� ����

    private static Server instance;
    Action<ServerMessage> callback;
    ServerMessage message;
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
            ws = new WebSocket("ws://192.168.50.206:1337");// 127.0.0.1�� ������ ������ �ּ��̴�. 1337��Ʈ�� �����Ѵٴ� �ǹ��̴�.
            ws.OnMessage += ws_OnMessage; //�������� ����Ƽ ������ �޼����� �� ��� ������ �Լ��� ����Ѵ�.
            ws.OnOpen += ws_OnOpen;//������ ����� ��� ������ �Լ��� ����Ѵ�
            ws.OnClose += ws_OnClose;//������ ���� ��� ������ �Լ��� ����Ѵ�.
            ws.Connect();//������ �����Ѵ�.
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    private void Update()
    {
        if (this.message != null)
        {
            if (this.message.type == "CurrentHighScore")
            {
                string user = this.message.user;
                int _score = this.message.currentHighScore;
                this.message = null;
            }
            else
            {
                this.callback.Invoke(this.message);
                this.message = null;
            }
        }
    }
    public void SaveScore(int score, float playtime)
    {
        // SaveScore �޽����� �ش��ϴ� JSON ������ ����
        User saveScoreData = new User
        {
            type = "SaveScore",
            UserID = userID,
            Score = score,
            Playtime = playtime
        };
        // JSON�� ���ڿ��� ��ȯ�Ͽ� ������ ����
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

    public void GetBestScore(Action<ServerMessage> callback)
    {
        User getBestScore = new User
        {
            UserID = userID,
            type = "GetBestScore"
        };

        this.callback = callback;
        ws.Send(JsonUtility.ToJson(getBestScore));
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
    public void SignUp(string ID, string PW, Action<ServerMessage> callback)
    {
        User signUpData = new User
        {
            type = "SignUp",
            UserID = ID,
            UserPW = PW
        };

        this.callback = callback;
        ws.Send(JsonUtility.ToJson(signUpData));
    }
    void ws_OnMessage(object sender, MessageEventArgs e)
    {
        serverMessage = JsonUtility.FromJson<ServerMessage>(e.Data);
        this.message = serverMessage;
    }
    void ws_OnOpen(object sender, System.EventArgs e)
    {
        Debug.Log("open"); //����� �ֿܼ� "open"�̶�� ��´�.
    }
    void ws_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("close"); //����� �ֿܼ� "close"�̶�� ��´�.
    }
}
