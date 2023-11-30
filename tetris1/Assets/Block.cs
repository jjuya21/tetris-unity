using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Block : MonoBehaviour
{
    private float timer = 0f;
    private float interval = 0.5f; // 블록이 한 칸 아래로 이동하는 간격 (속도 조절)
    private float moveInterval = 1f;
    private float heldTimer = 0f;
    private float heldInterval = 0.5f;

    // 현재 그룹의 위치가 유효한지 확인하는 함수
    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            // 현재 위치가 게임 영역 안에 있는지 확인
            if (!Playfield.insideBorder(v))
                return false;

            // 현재 위치에 다른 블록이 있는지 확인
            if (Playfield.grid[(int)v.x, (int)v.y] != null && Playfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    // 그리드 업데이트 함수
    void updateGrid()
    {
        // 그리드에서 현재 그룹의 블록을 제거
        for (int y = 0; y < Playfield.h; ++y)
            for (int x = 0; x < Playfield.w; ++x)
                if (Playfield.grid[x, y] != null)
                    if (Playfield.grid[x, y].parent == transform)
                        Playfield.grid[x, y] = null;

        // 현재 그룹의 블록을 그리드에 추가
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }

    // 시작 함수
    void Start()
    {
        // 만약 그룹의 초기 위치가 유효하지 않다면 메뉴를 호출
        if (!isValidGridPos())
        {
            FindObjectOfType<MenuSet>().SetMainMenu();
        }
    }


    // 업데이트 함수
    void Update()
    {
        // 게임이 진행 중인 경우
        if (Time.timeScale >= 1)
        {
            // 키 입력에 따라 블록을 이동하고 회전
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

                // 0.2초 간격으로 오른쪽으로 이동
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
                heldTimer = 0f; // 키가 떨어지면 타이머 초기화
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Rotate(0, 0, -90);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.Rotate(0, 0, 90);
            }
            
            // 아래 화살표 키를 누르거나 시간이 경과하면 블록을 한 칸 아래로 이동
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.DownArrow) || timer >= interval)
            {
                transform.position += new Vector3(0, -1, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                {
                    // 블록이 이동할 수 없다면 행을 삭제하고 다음 블록을 생성
                    transform.position += new Vector3(0, 1, 0);
                    Playfield.deleteFullRows();
                    FindObjectOfType<Spawner>().spawnNext();
                    enabled = false; // 현재 스크립트 비활성화
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
                        // 블록이 이동할 수 없다면 행을 삭제하고 다음 블록을 생성
                        transform.position += new Vector3(0, 1, 0);
                        Playfield.deleteFullRows();
                        FindObjectOfType<Spawner>().spawnNext();
                        enabled = false; // 현재 스크립트 비활성화
                        break; // 루프 종료
                    }
                }
            }
        }
    }
}
