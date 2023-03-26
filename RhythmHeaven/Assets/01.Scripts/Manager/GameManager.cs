using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    [SerializeField] GameObject go_StageTitle = null;

    int retrySong = 0;
    int retryBpm = 0;

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
    [SerializeField] Result theResult;

    [SerializeField] Transform cameraTR;

    [SerializeField] Animator splashAnim = null;
    string splash = "Splash";


    // Start is called before the first frame update
    private void Awake()
    {
        inst = this;
    }

    public void GameStart(int _currentSong, int _bpm)
    {
        GameManager.inst.SplashScene();
        retrySong = _currentSong;
        retryBpm = _bpm;
        // 게임리셋.
        cameraTR.localPosition = Vector3.zero;
        cameraTR.localEulerAngles = Vector3.zero;
        theMusic.bgmName = "BGM" + _currentSong;
        theMusic.ResetMusic();
        theNote.bpm = _bpm;
        theNote.RemoveNote();
        theStage.RemoveStage();
        theStage.SettingStage(_currentSong);
        theScore.Initialized();
        theScore.ResetCombo();
        theTiming.Initialized();
        theStatus.Initialized();
        thePlayer.Initialized();
        theResult.SetCurrentSont(_currentSong);

        AudioManager.inst.StopBGM();

        isStartGame = true;
    }
    // Retry용 오버로딩함수
    public void GameStart()
    {
        GameManager.inst.SplashScene();
        // 게임리셋.
        cameraTR.localPosition = Vector3.zero;
        cameraTR.localEulerAngles = Vector3.zero;
        theMusic.bgmName = "BGM" + retrySong;
        theMusic.ResetMusic();
        theNote.bpm = retryBpm;
        theNote.RemoveNote();
        theStage.RemoveStage();
        theStage.SettingStage(retrySong);
        theScore.Initialized();
        theScore.ResetCombo();
        theTiming.Initialized();
        theStatus.Initialized();
        thePlayer.Initialized();
        theResult.SetCurrentSont(retrySong);

        AudioManager.inst.StopBGM();

        isStartGame = true;
    }

    public void MainMenu()
    {
        GameManager.inst.SplashScene();
        isStartGame = false;
        go_StageTitle.SetActive(true);
        theStageMenu.SettingSong();
    }

    public void SplashScene()
    {
        splashAnim.SetTrigger(splash);
    }
}
