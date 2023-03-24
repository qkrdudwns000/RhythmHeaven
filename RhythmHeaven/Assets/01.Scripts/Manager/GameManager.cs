using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    [SerializeField] GameObject go_StageTitle = null;

    public bool isStartGame = false;
    public bool isOption = false;

    [SerializeField] ScoreManager theScore;
    [SerializeField] TimingManager theTiming;
    [SerializeField] StatusManager theStatus;
    [SerializeField] PlayerController thePlayer;
    [SerializeField] StageManager theStage;
    [SerializeField] StageMenu theStageMenu;
    [SerializeField] NoteManager theNote;
    [SerializeField] CircleFrame theMusic;


    // Start is called before the first frame update
    private void Awake()
    {
        inst = this;
    }

    public void GameStart(int _currentSong, int _bpm)
    {
        // 게임리셋.
        theMusic.bgmName = "BGM" + _currentSong;
        theNote.bpm = _bpm;
        theStage.RemoveStage();
        theStage.SettingStage(_currentSong);
        theScore.Initialized();
        theScore.ResetCombo();
        theTiming.Initialized();
        theStatus.Initialized();
        thePlayer.Initialized();

        AudioManager.inst.StopBGM();

        isStartGame = true;
    }

    public void MainMenu()
    {
        isStartGame = false;
        go_StageTitle.SetActive(true);
        theStageMenu.SettingSong();
    }
}
