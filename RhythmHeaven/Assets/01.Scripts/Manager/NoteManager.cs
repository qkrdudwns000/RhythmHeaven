using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;

    [SerializeField] Transform tfNoteAppear = null; // ��Ʈ ������ ��ġ.

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
                theTimingManager.circleNoteList.Add(note); // ��Ʈ ��������Ʈ�� �߰�.
                currentTime -= 60d / bpm; // 0���ȵ�. ��Ȯ�����ʰ� double�̱⶧����, ���Ѹ�ŭ �״�� ����� ��Ȯ�� ����.
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ��Ʈ��(colider) ��ܷ� ���� Miss ����
        if(collision.CompareTag("Note"))
        {
            // ��Ʈ�� ��ġ�� ������ ��� (imageȭ��ȭ��ä��)
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
