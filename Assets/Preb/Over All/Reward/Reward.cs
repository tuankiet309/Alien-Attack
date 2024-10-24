using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    [Header("Reward Info")]
    public int healthReward;
    public int creditReward;
    public int staminaReward;

    [Header("Physic Info")]
    Rigidbody rb;
    [SerializeField] float maxRange;
    [SerializeField] float maxHeight;
    [SerializeField] float collectRange;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        Vector3 randomDirection = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)).normalized;
        Vector3 randomVelocity = randomDirection * maxRange + Vector3.up * maxHeight;
        rb.velocity = randomVelocity;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        IRewardListener[] listeners = other.GetComponents<IRewardListener>();
        Perception_Stimuli stimuli = other.GetComponent<Perception_Stimuli>();
        if(listeners != null && stimuli !=null)
        {
            foreach(IRewardListener listener in listeners)
            {
                listener.Reward(this);
            }
            Destroy(gameObject);
        }
        
    }
}

public interface IRewardListener
{
    public void Reward(Reward reward);
}