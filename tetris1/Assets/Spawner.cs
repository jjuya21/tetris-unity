using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Spawner : MonoBehaviour
{
    // �̸� ���ǵ� ��� �׷���� �迭
    public GameObject[] groups;

    // ���� ��� �׷��� �����ϴ� �Լ�
    public void spawnNext()
    {
        // groups �迭���� �����ϰ� �ϳ��� ����
        int i = Random.Range(0, groups.Length);

        // ���õ� ��� �׷��� ���� ������ ��ġ�� ����
        Instantiate(groups[i], transform.position, Quaternion.identity);
    }
}
