using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playfield : MonoBehaviour
{
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];
    public static int score = 0;
    static int count = 0;

    public static void RemoveObjectsWithScript<T>() where T : MonoBehaviour
    {
        T[] objectsWithScript = GameObject.FindObjectsOfType<T>();

        foreach (T obj in objectsWithScript)
        {
            Destroy(obj.gameObject);
        }
    }

    public static void GameOver()
    {
        DB.SaveScore(score);
        RemoveObjectsWithScript<Group>();
        score = 0;
        Time.timeScale = 0f;
    }

    public static int GetScore()
    {
        return score;
    }
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x),
            Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
            (int)pos.x < w &&
            (int)pos.y >= 0);
    }

    public static void deleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
        score += (200 * count);
        ScoreText.UpdateScoreText();
    }

    public static void decreaseRow(int y) 
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
            return true;
    }

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
