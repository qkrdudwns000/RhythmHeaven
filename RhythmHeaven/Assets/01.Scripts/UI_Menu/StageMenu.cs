using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Song
{
    public string name;
    public int bpm;
    public Sprite sprite;
}

public class StageMenu : MonoBehaviour
{
    [SerializeField] Song[] songList = null;

    [SerializeField] Image imgSong = null;

    [SerializeField] GameObject go_TitleMenu = null;
    [SerializeField] TitleMenu theTitle;
    ScrollMenu theScroll;

    int currentSong = 0;
    int prevSong = 0;

    private void Start()
    {
        theScroll = FindObjectOfType<ScrollMenu>();
        for(int i = 0; i < songList.Length; i++)
        {
            theScroll.songNames[i].text = songList[i].name;
        }
        SettingSong();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(theScroll.ScrollMoveDown());
            theScroll.ChangeScaleAndColor();
            currentSong = theScroll.CurrentSongNum();
            if(currentSong != prevSong)
                SettingSong();
            prevSong = currentSong;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(theScroll.ScrollMoveUp());
            theScroll.ChangeScaleAndColor();
            currentSong = theScroll.CurrentSongNum();
            if (currentSong != prevSong)
                SettingSong();
            prevSong = currentSong;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            BtnPlay();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BtnBack();
        }
    }
    public void SettingSong()
    {
        imgSong.sprite = songList[currentSong].sprite;

        AudioManager.inst.PlayBGM("BGM" + currentSong);
    }

    public void BtnPlay()
    {
        int _bpm = songList[currentSong].bpm;

        GameManager.inst.GameStart(currentSong, _bpm);
        this.gameObject.SetActive(false);
    }
    public void BtnBack()
    {
        go_TitleMenu.SetActive(true);
        theTitle.isPressAnyKey = true;
        theTitle.MenuAnim();
        this.gameObject.SetActive(false);
    }
}
