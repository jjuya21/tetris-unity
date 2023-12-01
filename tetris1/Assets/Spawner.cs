using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class Spawner : MonoBehaviour
{
    // 미리 정의된 블록 그룹들의 배열
    public GameObject[] groups;
    public List<int> blocks;
    public int index;
    public int angle;
    private static GameObject nextblock;
    void Start()
    {
        index = Random.Range(0, groups.Length);
        blocks.Add(index);
    }
    // 다음 블록 그룹을 생성하는 함수
    public void spawnNext()
    {
        index = Random.Range(0, groups.Length);
        angle = Random.Range(0, 3) * 90;
        blocks.Add(index);
        
        Instantiate(groups[blocks[0]], transform.position, Quaternion.Euler(0, 0, angle));
        if (nextblock != null)
        {
            Destroy(nextblock);
            nextblock = null; // 추가: Destroy 이후에 null로 설정
        }
        nextblock = Instantiate(groups[blocks[1]], new Vector3(18, 7, 0), Quaternion.Euler(0, 0, angle));

        nextblock.GetComponent<Block>().enabled = false;
        // 첫 번째 블록 그룹 제거
        blocks.RemoveAt(0);
    }
}
