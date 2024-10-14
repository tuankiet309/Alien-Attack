using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception_Stimuli : MonoBehaviour //Stimuli là script đánh dấu các object là các vật thể cần đc các npc nhận biết
{
    // Start is called before the first frame update
    void Start()
    {
        Sense_Component.RegisterStimuli(this); //Khi bắt đầu, tự đăng kí vào các stimuli đang tồn tại
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        Sense_Component.UnRegisterStimuli(this); //Khi bị hủy, tự hủy đăng kí
    }
}
