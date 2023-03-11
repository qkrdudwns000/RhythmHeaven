using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject go_Prefab; // 풀링 오브젝트.
    public int count; // queue에 할당할 갯수.
    public Transform tfPoolParent; // 부모 오브젝트
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

    // 풀링할 오브젝트 갯수 미리 설정하는 함수.
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

            queue.Enqueue(clone); // for문 돌면서 셋팅해둔 count 갯수만큼 인큐.
        }

        return queue;
    }
}
