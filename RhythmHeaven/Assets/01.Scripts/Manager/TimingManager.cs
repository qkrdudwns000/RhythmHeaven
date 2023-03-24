using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> circleNoteList = new List<GameObject>(); // 생성된 노트들

    int[] judgementRecord = new int[5];

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null; // x = 최소값, y = 최대값 으로 활용할 것임. 
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
        // 타이밍 박스 설정(최소, 최대 범위)
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2); // 판정범위 최대 최소 범위 지정.
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
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y) // 최대/최소 범위안에 판정되었는지.
                {
                    // 노트 제거
                    circleNoteList[i].GetComponent<Note>().HideNote();
                    circleNoteList.RemoveAt(i);
                    // 이펙트 연출
                    if (j < timingBoxs.Length - 1) // bad판정 제외를 위한 if문.
                    {
                        theEffectManager.NoteHitEffect();
                        theStatusManager.IncreaseHp(2);
                    }
                    theEffectManager.JudgementEffect(j);
                    
                    if (CheckCanNextPlate())
                    {
                        // 점수 증가
                        theScoreManager.IncreaseScore(j);
                        // 발판 생성
                        theStageManager.ShowNextPlate();
                        // 판정 기록
                        judgementRecord[j]++;
                    }
                    theAudioManager.PlaySFX("Clap");

                    return true;
                }
            }
        }
        // Miss 판정.
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
