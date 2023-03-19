using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    bool isDead = false;

    [SerializeField] int maxHp = 100;
    [SerializeField] int currentHp = 100;
    [SerializeField] float blinkSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;
    int currentBlinkCount = 0;
    bool isBlink = false;

    [SerializeField] Image hpImage = null;
    [SerializeField] SkinnedMeshRenderer playerRender;

    Result theResult;

    private void Start()
    {
        theResult = FindObjectOfType<Result>();
        SettingHPImage();
    }
    public void IncreaseHp(int _num)
    {
        currentHp += _num;

        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        SettingHPImage();
    }
    public void DecreaseHp(int _num)
    {
        if(!isBlink)
        {
            currentHp -= _num;

            if (currentHp <= 0)
            {
                // 플레이어 사망
                theResult.ShowResult();
            }
            else
            {
                // 블링크시에는 일시무적
                StartCoroutine(BlinkCo());
            }

            SettingHPImage();

        }
    }

    void SettingHPImage()
    {
        hpImage.fillAmount = currentHp / (float)maxHp;
    }

    public bool IsDead()
    {
        return isDead;
    }
    IEnumerator BlinkCo()
    {
        isBlink = true;

        while(currentBlinkCount <= blinkCount)
        {
            playerRender.enabled = !playerRender.enabled;
            yield return new WaitForSeconds(blinkSpeed);
            currentBlinkCount++;
        }

        playerRender.enabled = true;
        currentBlinkCount = 0;
        isBlink = false;
    }
}
