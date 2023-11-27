using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playfield : MonoBehaviour
{
    // ���� ������ �ʺ�� ����
    public static int w = 10;
    public static int h = 20;

    // �׸��� �迭�� ���� ���� ǥ��, �� ������ ����� Transform�� �����
    public static Transform[,] grid = new Transform[w, h];

    // ���� ������ ����
    public static int score = 0;

    // ���� ������ Ƚ��
    static int count = 0;

    // Ư�� Ÿ��(T)�� ��ũ��Ʈ�� ���� ��ü���� �����ϴ� �Լ�
    public static void RemoveObjectsWithScript<T>() where T : MonoBehaviour
    {
        T[] objectsWithScript = GameObject.FindObjectsOfType<T>();

        foreach (T obj in objectsWithScript)
        {
            Destroy(obj.gameObject);
        }
    }

    // ���� ���� ó�� �Լ�
    public static void GameOver()
    {
        // ���� ������ �����ϰ� ���� ������Ʈ���� ����
        DB.SaveScore(score);
        RemoveObjectsWithScript<Group>();
        score = 0;
        Time.timeScale = 0f; // ���� �Ͻ� ����
    }

    // ���� ������ ��ȯ�ϴ� �Լ�
    public static int GetScore()
    {
        return score;
    }

    // Vector2�� ���� ����� ���� ���ͷ� �ݿø��ϴ� �Լ�
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // �־��� ��ġ�� ���� ���� ���ο� �ִ��� Ȯ���ϴ� �Լ�
    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
    }

    // Ư�� ���� �����ϴ� �Լ�
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
        // ���� ������ ������ ������ �����ϰ� ȭ�鿡 �ݿ�
        score += (200 * count);
        ScoreText.UpdateScoreText();
    }

    // Ư�� �� ���� ��� ���� �� ĭ�� ������ �Լ�
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // Ư�� �� ���� ��� ����� ������ �Լ�
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    // Ư�� ���� ��� ä���� �ִ��� Ȯ���ϴ� �Լ�
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    // ��� �� �߿��� ä���� ���� �����ϴ� �Լ�
    public static void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            count = 0;
            if (isRowFull(y))
            {
                count++;
                deleteRow(y);

                decreaseRowsAbove(y + 1);
                --y;
            }
        }
    }
}
