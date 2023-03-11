using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> circleNoteList = new List<GameObject>(); // ������ ��Ʈ��

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null; // x = �ּҰ�, y = �ִ밪 ���� Ȱ���� ����. 

    EffectManager theEffectManager;
    ScoreManager theScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();

        timingBoxs = new Vector2[timingRect.Length];
        // Ÿ�̹� �ڽ� ����(�ּ�, �ִ� ����)
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2); // �������� �ִ� �ּ� ���� ����.
        }
    }

    public void CheckTiming()
    {
        for(int i = 0; i < circleNoteList.Count; i++)
        {
            float t_notePosX = circleNoteList[i].transform.localPosition.x;

            for(int j = 0; j < timingBoxs.Length; j++)
            {
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y) // �ִ�/�ּ� �����ȿ� �����Ǿ�����.
                {
                    // ��Ʈ ����
                    circleNoteList[i].GetComponent<Note>().HideNote();
                    circleNoteList.RemoveAt(i);
                    // ����Ʈ ����
                    if(j < timingBoxs.Length - 1) // bad���� ���ܸ� ���� if��.
                       theEffectManager.NoteHitEffect();
                    theEffectManager.JudgementEffect(j);
                    // ���� ����
                    theScoreManager.IncreaseScore(j);
                    return;
                }
            }
        }
        // Miss ����.
        theScoreManager.ResetCombo();
        theEffectManager.JudgementEffect(timingBoxs.Length); 
    }
}
