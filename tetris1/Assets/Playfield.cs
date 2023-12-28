using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playfield : MonoBehaviour
{
    public GameObject GameObject;

    public static float playtime = 0f;
    // 게임 영역의 너비와 높이
    public static int w = 10;
    public static int h = 20;

    // 그리드 배열로 게임 영역 표현, 각 셀에는 블록의 Transform이 저장됨
    public static Transform[,] grid = new Transform[w, h];

    // 현재 게임의 점수
    public static int score = 0;

    // 행이 삭제된 횟수
    static int count = 0;

    // 특정 타입(T)의 스크립트를 가진 객체들을 제거하는 함수
    public static void RemoveObjectsWithScript<T>() where T : MonoBehaviour
    {
        T[] objectsWithScript = GameObject.FindObjectsOfType<T>();

        foreach (T obj in objectsWithScript)
        {
            Destroy(obj.gameObject);
        }
    }

    void FixedUpdate()
    {
        playtime += Time.fixedDeltaTime;
    }

    public void ResetTime()
    {
        playtime = 0f;
    }

    public static void GameOver()
    {
        if (score != 0)
        {
            Server.Instance().SaveScore(score, playtime);
        }
        RemoveObjectsWithScript<Block>();
        score = 0;
        Time.timeScale = 0f; // 게임 일시 정지
    }

    // 현재 점수를 반환하는 함수
    public static int GetScore()
    {
        return score;
    }

    // Vector2를 가장 가까운 정수 벡터로 반올림하는 함수
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // 주어진 위치가 게임 영역 내부에 있는지 확인하는 함수
    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0 && (int)pos.y < 18);
    }

    // 특정 행을 삭제하는 함수
    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
        // 행이 삭제될 때마다 점수를 증가하고 화면에 반영
        score += (200 * count);
        ScoreText.UpdateScoreText();
    }

    // 특정 행 위의 모든 행을 한 칸씩 내리는 함수
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

    // 특정 행 위의 모든 행들을 내리는 함수
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    // 특정 행이 모두 채워져 있는지 확인하는 함수
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    // 모든 행 중에서 채워진 행을 삭제하는 함수
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
