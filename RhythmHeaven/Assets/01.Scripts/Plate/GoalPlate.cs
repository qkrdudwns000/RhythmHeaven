using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : Plate
{
    AudioSource theAudio;
    NoteManager theNoteManager;
    Result theResult;

    // Start is called before the first frame update
    void Start()
    {
        theResult = FindObjectOfType<Result>();
        theAudio = GetComponent<AudioSource>();
        theNoteManager = FindObjectOfType<NoteManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            theAudio.Play();
            PlayerController.isCanPressKey = false;
            theNoteManager.RemoveNote(); // 노트 전부 없애주기.
            theResult.ShowResult();
        }
    }

}
