using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject go_Panel = null;
    [SerializeField] CanvasGroup inGameUI = null;

    int currentSong = 0; public void SetCurrentSont(int _songNum) { currentSong = _songNum; }

    [SerializeField] TMPro.TMP_Text[] txtCount = null;
    [SerializeField] TMPro.TMP_Text txtMaxCombo = null;
    [SerializeField] TMPro.TMP_Text txtScore = null;
    [SerializeField] TMPro.TMP_Text txtExp = null;

    ScoreManager theScore;
    TimingManager theTimingManager;
    NoteManager theNoteManager;
    DataBaseManager theDataBaseManager;

    const int SCORE = 1, COMBO = 2;
    bool isResult = false;

    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theTimingManager = FindObjectOfType<TimingManager>();
        theNoteManager = FindObjectOfType<NoteManager>();
        theDataBaseManager = FindObjectOfType<DataBaseManager>();
    }
    private void Update()
    {
        if(isResult)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                BtnMainMenu();
            }
            if(Input.GetKeyDown(KeyCode.F5))
            {
                BtnRetry();
            }
        }
    }

    public void ShowResult()
    {
        // ��Ʈ ���� �����ֱ�.
        theNoteManager.RemoveNote();

        AudioManager.inst.StopBGM();
        isResult = true;

        FindObjectOfType<CircleFrame>().ResetMusic();

        go_Panel.SetActive(true);

        for (int i = 0; i < txtCount.Length; i++)
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

        // �ְ����� �޼� ���� ��쿡�� ���ھ� ����.
        if (currentScore > theDataBaseManager.scores[currentSong])
        {
            theDataBaseManager.scores[currentSong] = currentScore;
            theDataBaseManager.SaveScore();
        }
            
    }

    public void BtnMainMenu()
    {
        go_Panel.SetActive(false);
        isResult = false;
        GameManager.inst.MainMenu();

    }
    public void BtnRetry()
    {
        GameManager.inst.GameStart();
        go_Panel.SetActive(false);
        isResult = false;
    }

    public void HideUI()
    {
        inGameUI.alpha = 0;
    }
    public void ShowUI()
    {
        inGameUI.alpha = 1;
        ShowResult();
    }

}
