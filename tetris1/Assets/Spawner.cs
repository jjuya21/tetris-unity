using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Spawner : MonoBehaviour
{
    // 미리 정의된 블록 그룹들의 배열
    public GameObject[] groups;

    // 다음 블록 그룹을 생성하는 함수
    public void spawnNext()
    {
        // groups 배열에서 랜덤하게 하나를 선택
        int i = Random.Range(0, groups.Length);

        // 선택된 블록 그룹을 현재 스포너 위치에 생성
        Instantiate(groups[i], transform.position, Quaternion.identity);
    }
}
