using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Block : MonoBehaviour
{
    private float timer = 0f;
    private float interval = 0.5f; // ����� �� ĭ �Ʒ��� �̵��ϴ� ���� (�ӵ� ����)
    private float moveInterval = 1f;
    private float heldTimer = 0f;
    private float heldInterval = 0.5f;

    // ���� �׷��� ��ġ�� ��ȿ���� Ȯ���ϴ� �Լ�
    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            // ���� ��ġ�� ���� ���� �ȿ� �ִ��� Ȯ��
            if (!Playfield.insideBorder(v))
                return false;

            // ���� ��ġ�� �ٸ� ����� �ִ��� Ȯ��
            if (Playfield.grid[(int)v.x, (int)v.y] != null && Playfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    // �׸��� ������Ʈ �Լ�
    void updateGrid()
    {
        // �׸��忡�� ���� �׷��� ����� ����
        for (int y = 0; y < Playfield.h; ++y)
            for (int x = 0; x < Playfield.w; ++x)
                if (Playfield.grid[x, y] != null)
                    if (Playfield.grid[x, y].parent == transform)
                        Playfield.grid[x, y] = null;

        // ���� �׷��� ����� �׸��忡 �߰�
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

    // ���� �Լ�
    void Start()
    {
        // ���� �׷��� �ʱ� ��ġ�� ��ȿ���� �ʴٸ� �޴��� ȣ��
        if (!isValidGridPos())
        {
            FindObjectOfType<MenuSet>().SetMainMenu();
        }
    }


    // ������Ʈ �Լ�
    void Update()
    {
        // ������ ���� ���� ���
        if (Time.timeScale >= 1)
        {
            // Ű �Է¿� ���� ����� �̵��ϰ� ȸ��
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(1, 0, 0);
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                heldTimer += Time.deltaTime;

                // 0.2�� �������� ���������� �̵�
                if (heldTimer >= heldInterval && Time.time % moveInterval < 0.5f)
                {
                    transform.position += new Vector3(1, 0, 0);

                    if (isValidGridPos())
                        updateGrid();
                    else
                        transform.position += new Vector3(-1, 0, 0);
                }
            }
            else
            {
                heldTimer = 0f; // Ű�� �������� Ÿ�̸� �ʱ�ȭ
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Rotate(0, 0, -90);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.Rotate(0, 0, 90);
            }
            
            // �Ʒ� ȭ��ǥ Ű�� �����ų� �ð��� ����ϸ� ����� �� ĭ �Ʒ��� �̵�
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.DownArrow) || timer >= interval)
            {
                transform.position += new Vector3(0, -1, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                {
                    // ����� �̵��� �� ���ٸ� ���� �����ϰ� ���� ����� ����
                    transform.position += new Vector3(0, 1, 0);
                    Playfield.deleteFullRows();
                    FindObjectOfType<Spawner>().spawnNext();
                    enabled = false; // ���� ��ũ��Ʈ ��Ȱ��ȭ
                }
                timer = 0f;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                while (isValidGridPos())
                {
                    transform.position += new Vector3(0, -1, 0);
                    if (isValidGridPos())
                        updateGrid();
                    else
                    {
                        // ����� �̵��� �� ���ٸ� ���� �����ϰ� ���� ����� ����
                        transform.position += new Vector3(0, 1, 0);
                        Playfield.deleteFullRows();
                        FindObjectOfType<Spawner>().spawnNext();
                        enabled = false; // ���� ��ũ��Ʈ ��Ȱ��ȭ
                        break; // ���� ����
                    }
                }
            }
        }
    }
}
