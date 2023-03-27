using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;

    public AudioMixer mixer;

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] Slider bgmSlider = null;
    [SerializeField] Slider sfxSlider = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(inst);
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        bgmSlider.value = 1.0f;
        sfxSlider.value = 1.0f;
    }
    private void Update()
    {
        if(GameManager.inst.isOption)
        {
            // BGM slider
            if(Input.GetKeyDown(KeyCode.F1))
            {
                bgmSlider.value -= 0.1f;
                if(bgmSlider.value < 0.0001f)
                {
                    bgmSlider.value = 0.0001f;
                }
            }else if(Input.GetKeyDown(KeyCode.F2))
            {
                bgmSlider.value += 0.1f;
                if (bgmSlider.value > 1.0f)
                {
                    bgmSlider.value = 1.0f;
                }
            }
            // SFX slider
            if(Input.GetKeyDown(KeyCode.F3))
            {
                sfxSlider.value -= 0.1f;
                if (sfxSlider.value < 0.0001f)
                {
                    sfxSlider.value = 0.0001f;
                }
            }
            else if(Input.GetKeyDown(KeyCode.F4))
            {
                sfxSlider.value += 0.1f;
                if (sfxSlider.value > 1.0f)
                {
                    sfxSlider.value = 1.0f;
                }
            }
        }
    }


    public void PlayBGM(string _bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        { 
            if(_bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void PauseBGM()
    {
        bgmPlayer.Pause();
    }
    public void ReplayBGM()
    {
        bgmPlayer.Play();
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string _sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (_sfxName == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 오디오 플레이어가 재생중입니다.");
                return;
            }
        }
        Debug.Log(_sfxName + "이름의 사운드클립이 없습니다.");
    }

    // 볼륨 조절
    public void BGMSoundVolume(float v)
    {
        mixer.SetFloat("BgmSound", Mathf.Log10(v) * 20);
    }
    public void SFXSoundVolume(float v)
    {
        mixer.SetFloat("SfxSound", Mathf.Log10(v) * 20);
    }
}
