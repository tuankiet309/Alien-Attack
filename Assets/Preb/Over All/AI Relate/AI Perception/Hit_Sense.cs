using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Sense : Sense_Component //Giác quan nhận biết khi bị đánh trúng
{
    HealthComponent healthComp; //Lấy health comp đẻ nhận biệt mỗi khi nhận sát thương


    Dictionary<Perception_Stimuli,Coroutine> hit_Record = new Dictionary<Perception_Stimuli, Coroutine> (); //Danh sách bị đánh
    protected override bool IsStimuliSensable(Perception_Stimuli perception_Stimuli) //Kiểm tra xem có cảm nhận đc không
    {
        return hit_Record.ContainsKey(perception_Stimuli); //Tìm xem có vừa bị đánh không, nếu có trả true
    }

    IEnumerator ForgetStimuli(Perception_Stimuli perception_Stimuli) //Quá trình quên stimuli
    {
        yield return new WaitForSeconds (forgetTime);
        hit_Record.Remove (perception_Stimuli); //Xóa khỏi từ điển
    }
    private void Start()
    {
        healthComp = GetComponent<HealthComponent>();
        healthComp.onTakeDamamge += TookDamage; //Đăng kí hàm này để gọi mỗi khi quái nhận sát thương
    }

    private void TookDamage(float health, float delta, float maxHealth, GameObject Instigator)  //Nếu nhận sát thương
    {
        Perception_Stimuli perception_Stimuli = Instigator.GetComponent<Perception_Stimuli>(); //Gán stimuli vào stimuli của vật gây sát thương
        if(perception_Stimuli != null) //Nếu kẻ đó không có stimuli bỏ quá
        {
            Coroutine newForgettingCoroutine = StartCoroutine(ForgetStimuli(perception_Stimuli));
            if(hit_Record.TryGetValue(perception_Stimuli,out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                hit_Record[perception_Stimuli] = newForgettingCoroutine;
            }
            else
            {
                hit_Record.Add(perception_Stimuli, newForgettingCoroutine);
            }
        }
    }

}
