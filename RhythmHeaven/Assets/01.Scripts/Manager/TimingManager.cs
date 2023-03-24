using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> circleNoteList = new List<GameObject>(); // ������ ��Ʈ��

    int[] judgementRecord = new int[5];

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null; // x = �ּҰ�, y = �ִ밪 ���� Ȱ���� ����. 
    Vector3 tempPos;

    EffectManager theEffectManager;
    ScoreManager theScoreManager;
    StageManager theStageManager;
    StatusManager theStatusManager;
    PlayerController thePlayer;
    AudioManager theAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        theAudioManager = AudioManager.inst;
        theEffectManager = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theStageManager = FindObjectOfType<StageManager>();
        theStatusManager = FindObjectOfType<StatusManager>();
        thePlayer = FindObjectOfType<PlayerController>();

        timingBoxs = new Vector2[timingRect.Length];
        // Ÿ�̹� �ڽ� ����(�ּ�, �ִ� ����)
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2); // �������� �ִ� �ּ� ���� ����.
        }
    }
    public void Initialized()
    {
        for (int i = 0; i < judgementRecord.Length; i++)
        {
            judgementRecord[i] = 0;
        }
    }

    public bool CheckTiming()
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
                    if (j < timingBoxs.Length - 1) // bad���� ���ܸ� ���� if��.
                    {
                        theEffectManager.NoteHitEffect();
                        theStatusManager.IncreaseHp(2);
                    }
                    theEffectManager.JudgementEffect(j);
                    
                    if (CheckCanNextPlate())
                    {
                        // ���� ����
                        theScoreManager.IncreaseScore(j);
                        // ���� ����
                        theStageManager.ShowNextPlate();
                        // ���� ���
                        judgementRecord[j]++;
                    }
                    theAudioManager.PlaySFX("Clap");

                    return true;
                }
            }
        }
        // Miss ����.
        theScoreManager.ResetCombo();
        theEffectManager.JudgementEffect(timingBoxs.Length);
        MissRecord();
        return false;
    }

    bool CheckCanNextPlate()
    {
        tempPos = new Vector3(thePlayer.destPos.x, 0.3f, thePlayer.destPos.z);
        if (Physics.Raycast(tempPos, Vector3.down, out RaycastHit hitInfo, 1.1f))
        {
            if (hitInfo.transform.CompareTag("GrassPlate"))
            {
                Plate plate = hitInfo.transform.GetComponent<Plate>();
                if (plate.flag)
                {
                    plate.flag = false;
                    return true;
                }
            }
        }
        return false;
    }

    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }
    public void MissRecord()
    {
        judgementRecord[4]++;
    }
    
}
