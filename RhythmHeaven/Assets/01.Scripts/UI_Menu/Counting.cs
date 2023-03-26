using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counting : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text countText = null;
    [SerializeField] EscMenu menu;
    
    void Start()
    {
        countText = GetComponentInChildren<TMPro.TMP_Text>();
    }

    public void CountNumChange(int _count)
    {
        countText.text = _count.ToString();
    }
    public void ContinueGame()
    {
        menu.ContinueGame();
    }
    public void TiktokSound()
    {
        AudioManager.inst.PlaySFX("Count");
    }
}
