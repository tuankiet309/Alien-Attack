using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight_Sense : Sense_Component //Giác quan nhìn
{
    [SerializeField] private float sight_Distance = 5f; //Độ xa của tầm nhìn
    [SerializeField] private float sight_Half_Angle = 30f; //Độ rộng của tầm nhìn

    [SerializeField] private float eye_height = 5f; //Để chỉnh độ cao tránh bị nhìn dưới đất
    protected override bool IsStimuliSensable(Perception_Stimuli perception_Stimuli) //Override để xác định cách nhìn
    {
        float distance = Vector3.Distance(transform.position, perception_Stimuli.transform.position); // Tính toán độ xa gần của npc và stimuli
        if (distance > sight_Distance) //Nếu quá xa trả false
        {
            return false;
        }
        Vector3 forwardDir = transform.forward; //Tìm hướng nhìn của vật
        Vector3 stimuliDir = (perception_Stimuli.transform.position - transform.position).normalized; //Tìm hướng của npc đến stimuli
        if (Vector3.Angle(stimuliDir, forwardDir) > sight_Half_Angle) //Góc nhìn thẳng và góc đến nhân vật > hơn độ rộng tầm nhìn thì trả false
            return false;
        if (Physics.Raycast(transform.position + Vector3.up * eye_height, stimuliDir , out RaycastHit hitInfo, sight_Distance)) //Xác định có
                                                                                                                               //vật cản nào giữa npc và stimuli ko
        {
            if (hitInfo.collider.gameObject != perception_Stimuli.gameObject) //nếu có vật cản trả false
            {
                return false;
            }
        }
        return true; //nếu pass hết trả true
        
    }

    protected override void DrawDebug() //Vẽ debug
    {
        base.DrawDebug();
        Vector3 drawCenter = transform.position + Vector3.up * eye_height;
        Gizmos.DrawWireSphere(drawCenter, sight_Distance);
        Vector3 leftLimitDir = Quaternion.AngleAxis(sight_Half_Angle, Vector3.up) * transform.forward;
        Vector3 rightLimitDir = Quaternion.AngleAxis(-sight_Half_Angle, Vector3.up) * transform.forward;

        Gizmos.DrawLine(drawCenter, drawCenter + leftLimitDir * sight_Distance);
        Gizmos.DrawLine(drawCenter, drawCenter + rightLimitDir * sight_Distance);
    }
    
}
