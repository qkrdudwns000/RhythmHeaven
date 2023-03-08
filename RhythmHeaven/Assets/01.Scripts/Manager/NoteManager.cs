using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;
    Vector3 noteScale = new Vector3(1, 1, 1);

    [SerializeField] Transform tfNoteAppear = null; // ��Ʈ ������ ��ġ.
    [SerializeField] GameObject goNote = null; // ��Ʈ ������.


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            t_note.transform.localScale = noteScale;
            currentTime -= 60d / bpm; // 0���ȵ�. ��Ȯ�����ʰ� double�̱⶧����, ���Ѹ�ŭ �״�� ����� ��Ȯ�� ����.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Note"))
        {
            Destroy(collision.gameObject);
        }
    }
}
