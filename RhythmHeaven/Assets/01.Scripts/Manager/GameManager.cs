using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    [SerializeField] GameObject go_StageTitle = null;

    public bool isStartGame = false;

    [SerializeField] ScoreManager theScore;
    [SerializeField] TimingManager theTiming;
    [SerializeField] StatusManager theStatus;
    [SerializeField] PlayerController thePlayer;
    [SerializeField] StageManager theStage;


    // Start is called before the first frame update
    private void Awake()
    {
        inst = this;
    }

    public void GameStart()
    {
        // 게임리셋.
        theStage.RemoveStage();
        theStage.SettingStage();
        theScore.Initialized();
        theScore.ResetCombo();
        theTiming.Initialized();
        theStatus.Initialized();
        thePlayer.Initialized();

        isStartGame = true;
    }

    public void MainMenu()
    {
        isStartGame = false;

        go_StageTitle.SetActive(true);
    }
}
