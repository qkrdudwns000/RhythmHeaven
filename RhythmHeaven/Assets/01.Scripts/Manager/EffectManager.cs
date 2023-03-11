using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    string hit = "Hit";
    // 히트시 파동 anim
    [SerializeField] Animator noteHitAnim = null;
    // 히트시 판정 anim
    [SerializeField] Animator judgementAnim = null;
    [SerializeField] UnityEngine.UI.Image judgementImage = null;
    [SerializeField] Sprite[] judgementSprite = null;
    
    public void JudgementEffect(int _num)
    {
        judgementImage.sprite = judgementSprite[_num];
        judgementAnim.SetTrigger(hit);
    }
    // 노트 히트시 파동 이펙트
    public void NoteHitEffect()
    {
        noteHitAnim.SetTrigger(hit);
    }
}
