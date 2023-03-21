using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject go_Panel = null;

    [SerializeField] TMPro.TMP_Text[] txtCount = null;
    [SerializeField] TMPro.TMP_Text txtMaxCombo = null;
    [SerializeField] TMPro.TMP_Text txtScore = null;
    [SerializeField] TMPro.TMP_Text txtExp = null;

    ScoreManager theScore;
    TimingManager theTimingManager;
    NoteManager theNoteManager;

    const int SCORE = 1, COMBO = 2;
    bool isResult = false;

    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theTimingManager = FindObjectOfType<TimingManager>();
        theNoteManager = FindObjectOfType<NoteManager>();
    }
    private void Update()
    {
        if(isResult)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                BtnMainMenu();
            }
        }
    }

    public void ShowResult()
    {
        AudioManager.inst.StopBGM();
        isResult = true;

        theNoteManager.RemoveNote(); // 노트 전부 없애주기.

        go_Panel.SetActive(true);

        for(int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = "0";
        }

        txtMaxCombo.text = "0";
        txtScore.text = "0";
        txtExp.text = "0";

        int[] judgement = theTimingManager.GetJudgementRecord();
        int currentScore = theScore.GetScoreAndCombo(SCORE);
        int maxCombo = theScore.GetScoreAndCombo(COMBO);
        int experience = currentScore / 50;

        for (int i = 0; i < txtCount.Length; i++)
        {
            txtCount[i].text = string.Format("{0:#,##0}", judgement[i]);
        }

        txtScore.text = string.Format("{0:#,##0}", currentScore);
        txtMaxCombo.text = string.Format("{0:#,##0}", maxCombo);
        txtExp.text = string.Format("{0:#,##0}", experience);
    }

    public void BtnMainMenu()
    {
        go_Panel.SetActive(false);
        isResult = false;
        GameManager.inst.MainMenu();
        theScore.ResetCombo();
    }

}
