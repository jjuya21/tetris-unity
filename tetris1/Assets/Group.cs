using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Group : MonoBehaviour
{
    private float timer = 0f;
    private float interval = 0.5f; //속도 조절

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            if (!Playfield.insideBorder(v))
                return false;
            if (Playfield.grid[(int)v.x, (int)v.y] != null && Playfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    void updateGrid()
    {
        for (int y = 0; y < Playfield.h; ++y)
            for (int x = 0; x < Playfield.w; ++x)
                if (Playfield.grid[x, y] != null)
                    if (Playfield.grid[x, y].parent == transform)
                        Playfield.grid[x, y] = null;
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }
    void Start()
    {
        if (!isValidGridPos())
        {
            Playfield.GameOver();
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Time.timeScale >= 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(-1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Rotate(0, 0, -90);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.Rotate(0, 0, 90);
            }

            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.DownArrow) || timer >= interval)
            {
                transform.position += new Vector3(0, -1, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                {
                    transform.position += new Vector3(0, 1, 0);

                    Playfield.deleteFullRows();

                    FindObjectOfType<Spawner>().spawnNext();

                    enabled = false;
                }
                timer = 0f;
            }
        }
    }
}
