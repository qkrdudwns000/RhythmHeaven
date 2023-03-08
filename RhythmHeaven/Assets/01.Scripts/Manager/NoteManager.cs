using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;
    Vector3 noteScale = new Vector3(1, 1, 1);

    [SerializeField] Transform tfNoteAppear = null; // 노트 나오는 위치.
    [SerializeField] GameObject goNote = null; // 노트 프리팹.


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            t_note.transform.localScale = noteScale;
            currentTime -= 60d / bpm; // 0은안됨. 정확하지않고 double이기때문에, 더한만큼 그대로 빼줘야 정확히 상쇄됨.
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
