using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    string hit = "Hit";
    // ��Ʈ�� �ĵ� anim
    [SerializeField] Animator noteHitAnim = null;
    // ��Ʈ�� ���� anim
    [SerializeField] Animator judgementAnim = null;
    [SerializeField] UnityEngine.UI.Image judgementImage = null;
    [SerializeField] Sprite[] judgementSprite = null;
    
    public void JudgementEffect(int _num)
    {
        judgementImage.sprite = judgementSprite[_num];
        judgementAnim.SetTrigger(hit);
    }
    // ��Ʈ ��Ʈ�� �ĵ� ����Ʈ
    public void NoteHitEffect()
    {
        noteHitAnim.SetTrigger(hit);
    }
}
