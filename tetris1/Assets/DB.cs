using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DB : MonoBehaviour
{
    // ���� �α����� ������� ID
    public static string ID;

    // MySQL ���� ���ڿ�
    public static string conStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};",
            "127.0.0.1", "user_data", "root", "jy122385@");

    // ������ �����ϴ� �Լ�
    public static void SaveScore(int score)
    {
        using (MySqlConnection conn = new MySqlConnection(conStr))
        {
            conn.Open();

            string query = "INSERT INTO rank (Score, ID) VALUES (@score, @id)";

            using (MySqlCommand command = new MySqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@score", score);
                command.Parameters.AddWithValue("@id", ID);
                command.ExecuteNonQuery();
            }
        }
    }

    // �ְ� ������ �������� �Լ�
    public static int GetHighScore()
    {
        using (MySqlConnection conn = new MySqlConnection(conStr))
        {
            conn.Open();

            string query = "SELECT MAX(Score) AS HighScore FROM rank";

            using (MySqlCommand command = new MySqlCommand(query, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader.IsDBNull(reader.GetOrdinal("HighScore")))
                        {
                            return 0;
                        }
                        int highscore = reader.GetInt32("HighScore");
                        return highscore;
                    }
                }
            }
        }
        return 0;
    }

    // ��� ������ �������� �Լ�
    public static List<int> GetAllScores()
    {
        List<int> scores = new List<int>();

        using (MySqlConnection conn = new MySqlConnection(conStr))
        {
            conn.Open();

            string query = "SELECT Score FROM rank ORDER BY Score desc";

            using (MySqlCommand command = new MySqlCommand(query, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Score")))
                        {
                            int score = reader.GetInt32("Score");
                            scores.Add(score);
                        }
                    }
                }
            }
        }
        return scores;
    }

    // ��� ����� ����� �������� �Լ�
    public static List<string> GetAllUsers()
    {
        List<string> users = new List<string>();

        using (MySqlConnection conn = new MySqlConnection(conStr))
        {
            conn.Open();

            string query = "SELECT ID FROM rank ORDER BY Score desc";

            using (MySqlCommand command = new MySqlCommand(query, conn))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("ID")))
                        {
                            string user = reader.GetString("ID");
                            users.Add(user);
                        }
                    }
                }
            }
        }
        return users;
    }

    // ID�� PW�� Ȯ���Ͽ� �α��� ���θ� ��ȯ�ϴ� �Լ�
    public static bool IdCheck(string userID, string userPW)
    {
        using (MySqlConnection conn = new MySqlConnection(conStr))
        {
            conn.Open();

            string query = "SELECT PW FROM login_data WHERE ID = @id";

            using (MySqlCommand command = new MySqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@id", userID);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string PW = reader.GetString("PW");

                        if (userPW == PW)
                        {
                            ID = userID;
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    // ȸ�� ���� �Լ�
    public static void SignUp(string userID, string userPW)
    {
        using (MySqlConnection conn = new MySqlConnection(conStr))
        {
            conn.Open();

            string query = "SELECT COUNT(*) FROM login_data WHERE ID = @id";

            using (MySqlCommand checkCommand = new MySqlCommand(query, conn))
            {
                checkCommand.Parameters.AddWithValue("@id", userID);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                // ID�� �ߺ����� ������ ȸ�� ���� ����
                if (count == 0)
                {
                    query = "INSERT INTO login_data (ID, PW) VALUES (@id, @pw)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@id", userID);
                        command.Parameters.AddWithValue("@pw", userPW);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
