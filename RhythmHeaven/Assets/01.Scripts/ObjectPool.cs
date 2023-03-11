using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject go_Prefab; // Ǯ�� ������Ʈ.
    public int count; // queue�� �Ҵ��� ����.
    public Transform tfPoolParent; // �θ� ������Ʈ
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool inst;

    [SerializeField] ObjectInfo[] objectInfos = null;

    public Queue<GameObject> noteQueue = new Queue<GameObject>();

    private void Awake()
    {
        inst = this;
        noteQueue = InsertQueue(objectInfos[0]);
    }

    // Ǯ���� ������Ʈ ���� �̸� �����ϴ� �Լ�.
    private Queue<GameObject> InsertQueue(ObjectInfo _objectInfo)
    {
        Queue<GameObject> queue = new Queue<GameObject>();
        for(int i = 0; i < _objectInfo.count; i++)
        {
            GameObject clone = Instantiate(_objectInfo.go_Prefab, transform.position, Quaternion.identity);
            clone.SetActive(false);
            if (_objectInfo.tfPoolParent != null)
                clone.transform.SetParent(_objectInfo.tfPoolParent);
            else
                clone.transform.SetParent(this.transform);

            queue.Enqueue(clone); // for�� ���鼭 �����ص� count ������ŭ ��ť.
        }

        return queue;
    }
}
