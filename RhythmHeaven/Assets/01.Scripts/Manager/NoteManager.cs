using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;

    [SerializeField] Transform tfNoteAppear = null; // 노트 나오는 위치.

    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ScoreManager theScoreManager;

    private void Start()
    {
        theTimingManager = GetComponent<TimingManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.inst.isStartGame)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 60d / bpm)
            {
                GameObject note = ObjectPool.inst.noteQueue.Dequeue();
                note.transform.position = tfNoteAppear.position;
                note.SetActive(true);
                theTimingManager.circleNoteList.Add(note); // 노트 판정리스트에 추가.
                currentTime -= 60d / bpm; // 0은안됨. 정확하지않고 double이기때문에, 더한만큼 그대로 빼줘야 정확히 상쇄됨.
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 노트가(colider) 장외로 나가 Miss 판정
        if(collision.CompareTag("Note"))
        {
            // 노트를 못치고 놓쳤을 경우 (image화성화된채로)
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                theTimingManager.MissRecord();
                theEffectManager.JudgementEffect(4);
                theScoreManager.ResetCombo();
            }

            theTimingManager.circleNoteList.Remove(collision.gameObject);

            ObjectPool.inst.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    public void RemoveNote()
    {
        GameManager.inst.isStartGame = false;

        for (int i = 0; i < theTimingManager.circleNoteList.Count; i++)
        {
            theTimingManager.circleNoteList[i].SetActive(false);
            ObjectPool.inst.noteQueue.Enqueue(theTimingManager.circleNoteList[i]);
        }

        theTimingManager.circleNoteList.Clear();
    }
}
