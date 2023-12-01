using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class Spawner : MonoBehaviour
{
    // �̸� ���ǵ� ��� �׷���� �迭
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
    // ���� ��� �׷��� �����ϴ� �Լ�
    public void spawnNext()
    {
        index = Random.Range(0, groups.Length);
        angle = Random.Range(0, 3) * 90;
        blocks.Add(index);
        
        Instantiate(groups[blocks[0]], transform.position, Quaternion.Euler(0, 0, angle));
        if (nextblock != null)
        {
            Destroy(nextblock);
            nextblock = null; // �߰�: Destroy ���Ŀ� null�� ����
        }
        nextblock = Instantiate(groups[blocks[1]], new Vector3(18, 7, 0), Quaternion.Euler(0, 0, angle));

        nextblock.GetComponent<Block>().enabled = false;
        // ù ��° ��� �׷� ����
        blocks.RemoveAt(0);
    }
}
