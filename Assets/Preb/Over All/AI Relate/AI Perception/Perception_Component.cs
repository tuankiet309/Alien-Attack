using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception_Component : MonoBehaviour //Script tri giác dùng để quản lí thông tin đưa về từ các giác quan
{
    [SerializeField] List<Sense_Component> senses = new List<Sense_Component>(); // Danh sách các giác quan của object này
    LinkedList<Perception_Stimuli> currentlyPerceivedStimuli = new LinkedList<Perception_Stimuli>(); //Danh sách các stimuli đang được cảm nhận

    Perception_Stimuli targetStimuli; //Mục tiêu cảm nhận chính hiện tại để thực hiên các hành động liên quan

    [Header("Audio")]
    [SerializeField] AudioClip detecedClip;
    [SerializeField] float volume;


    public delegate void OnPerceptionTargetChanged(GameObject target, bool sense);  //Trong trường họp target bị thay đổi
    public event OnPerceptionTargetChanged onPerceptionTargetChanged;
    void Awake() //Đưa vào awake vì có thể đc spawn bởi spawner và được cài sẵn stimuli là người chơi.
    {
        foreach (Sense_Component s in senses) //Duyệt các các giác quan
        {
            s.onPerceptionUpdate += SenseUpdate; //Đăng kí hàm Sense Update vào từng event của từng giác quan
                                                 //tức là hàm nãy sẽ đc gọi mỗi khi 1 giác quan có invoke event
                                                 //aka mỗi khi giác quan cảm nhận hoặc mất cảm nhận 1 stimuli
        }
    }

    private void SenseUpdate(Perception_Stimuli perception_Stimuli, bool sensedSuccessful) //Hàm đây, nhận vào stimuli đó và check có cảm nhận đc ko
    {
        var nodeFound = currentlyPerceivedStimuli.Find(perception_Stimuli);  //Kiếm xem trong các stimuli đc cảm nhận đã có stimuli này chưa
        if (sensedSuccessful) //Nếu cảm nhận được
        {
            if (nodeFound != null)  //Node  tồn tại aka có node này trong link list các stimuli đc cảm nhận
            {
                currentlyPerceivedStimuli.AddAfter(nodeFound,perception_Stimuli); //Add stimuli này vào sau node vừa tìm thấy
                                                                                  //Lí giải vì sao đã tìm thấy rồi mà vẫn cần add after
                                                                                  //Thực chật có thể 2 3 giác quan đồng thời cảm nhận được cùng 1 stimuli, nếu
                                                                                  //giác quan này mất cảm nhận vẫn sẽ có giác quan khác
                                                                                  //đồng thời add phía sau để nếu cái trc bị remove không ảnh hưởng 
            }
            else
            {
                currentlyPerceivedStimuli.AddLast(perception_Stimuli); //Nếu không tồn tại thì add stimuli này vào sau cùng
            }
        }
        else
        {
                currentlyPerceivedStimuli.Remove(nodeFound); //Nếu ko cảm nhận đc thì remove chỉ 1 cái node này khỏi link list                                                                  
                                                             //Hàm remove : node đầu tiên (tìm thấy) có giá trị Perception_Stimuli đó sẽ bị loại bỏ.

        }

        if (currentlyPerceivedStimuli.Count !=0) //Kiểm tra danh sách các stimuli khác 0
        {
            Perception_Stimuli highest_Stimuli = currentlyPerceivedStimuli.First.Value; //Lấy ra stimuli đầu tiên
            if(targetStimuli==null||targetStimuli!=highest_Stimuli) //Nếu stimuli target bằng null hoặc khác vs stimuli đầu tiên
            {
                targetStimuli = highest_Stimuli; //Gán target là cái đầu tiên
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject,true); //Gọi các hàm và thông báo có thay đổi và có cảm nhạn được target
                Vector3 audioPos = transform.position;
                GameStatic.PlayAudioAtLoc(detecedClip, audioPos,volume);
            }
        }
        else //nếu danh sách các stimuli bằng 0
        {
            if (targetStimuli != null) //nếu target khác null
            {
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, false); //Gọi các hàm thông báo là có thay đổi và ko thấy target
                targetStimuli = null; //Cài target bằng null

            }
        }
    }
    internal void AssignPercivedStimuli(Perception_Stimuli target)
    {
        if(senses.Count !=0)
        {
            senses[0].AssignPercivedStimuli(target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
