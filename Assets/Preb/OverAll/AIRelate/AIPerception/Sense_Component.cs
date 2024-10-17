using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sense component dùng để áp dụng xây dựng các giác quan cụ thể của quái vật/npc
public abstract class Sense_Component : MonoBehaviour //abstract
{
    [SerializeField] protected float forgetTime = 3f; // thời gian để quên đối tượng sau khi đối tượng mất khỏi tầm phát hiện của giác quan

    static List<Perception_Stimuli> registeredStimuli = new List<Perception_Stimuli>(); //Danh sách các stimulu đã được đăng ký
                                                                                        //trong sense bao gồm cả sti đã phát hiện và chưa

    List<Perception_Stimuli> percivedStimuli = new List<Perception_Stimuli>(); //Danh sách các stimuli đã được phát hiện bới 1 sense (bị quên sẽ bị xóa đi)

    Dictionary<Perception_Stimuli, Coroutine> ForgettingRoutine = new Dictionary<Perception_Stimuli, Coroutine>(); //Từ điển bao gồm các stimuli và quá trình quên đi chúng,
                                                                                                                   //dùng để tìm kiếm stimuli để thực hiện dừng việc quên stimuli
                                                                                                                   //nếu stimuli đó đc phát hiện trở lại trở lại 

    public delegate void OnPerceptionUpdate(Perception_Stimuli perception,bool successfulSensed);// delegate và event dùng để invoke các hàm mỗi khi chúng có sự thay đổi
    public event OnPerceptionUpdate onPerceptionUpdate;//

    static public void RegisterStimuli(Perception_Stimuli perception_Stimuli) //Hàm đăng kí stimuli, static để tất cả các giác quan dùng chung
    {
        if (registeredStimuli.Contains(perception_Stimuli))
            return;
        registeredStimuli.Add(perception_Stimuli); //Đăng kí các stimuli đang tồn tại trong sennce
        
    }
    static public void UnRegisterStimuli(Perception_Stimuli perception_Stimuli) //hủy đăng ký stimuli
    {
        registeredStimuli.Remove(perception_Stimuli); //hủy
    }

    protected abstract bool IsStimuliSensable(Perception_Stimuli perception_Stimuli); //Hàm abstract để phát hiện 1 stimuli, cài đặt khác nhau
                                                                                      //phụ thuộc vào từng giác quan
   
    
    void Update() //Update mỗi khung hình để kiểm tra các stimuli 
    {
        foreach(var perception_Stimuli in registeredStimuli) //duyệt qua các stimuli tồn tại trong sense
        {
            if (IsStimuliSensable(perception_Stimuli)) //Nếu stimuli này có thể cảm nhận được
            {
                if (!percivedStimuli.Contains(perception_Stimuli)) //Nếu stimuli này chưa được add vào danh sách các stimuli có thể cảm nhận
                {
                    percivedStimuli.Add(perception_Stimuli); //Add sti đó vào
                    if (ForgettingRoutine.TryGetValue(perception_Stimuli, out Coroutine coroutine)) //Tìm xem stimuli này có đang trong quá trình quên hay không
                    {
                        StopCoroutine(coroutine); //dừng việc quên lại
                        ForgettingRoutine.Remove(perception_Stimuli); //Xóa stimuli này khỏi quá trình quên
                    }
                    else //Nếu đang không quên
                    {
                        onPerceptionUpdate?.Invoke(perception_Stimuli,true); //Invoke các hàm đăng kí cho biết stimuli đang được cảm nhận
                        Debug.Log("I sense the player");
                    }

                }

            }
            else //Nếu stimuli này ko cảm nhận đc
            {
                if (percivedStimuli.Contains(perception_Stimuli)) //Nếu stimuli này đang trong danh sách các sti đc cảm nhận
                {
                    percivedStimuli.Remove(perception_Stimuli); //xóa chúng đi
                    ForgettingRoutine.Add(perception_Stimuli,StartCoroutine(ForgetPlayer(perception_Stimuli))); //Thêm chúng vào quá trình quên
                }
            }
        }
    }

    IEnumerator ForgetPlayer(Perception_Stimuli perception_Stimuli) //Quá trình quên
    {
        yield return new WaitForSeconds(forgetTime); //Đợi sau s giây
        ForgettingRoutine.Remove(perception_Stimuli); //Xóa khỏi quá trình quên
        onPerceptionUpdate?.Invoke(perception_Stimuli, false); //Thông báo cho các hàm đăng kí stimuli này đã không còn đc cảm nhận
        Debug.Log("I cant sense the player");
        
    }

    protected virtual void DrawDebug() //Vẽ debug thể hiện cách cảm nhận của từng giác quan
    {
        
    }
    private void OnDrawGizmos()
    {
        DrawDebug();
    }
}
