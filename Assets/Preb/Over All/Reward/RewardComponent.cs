using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RewardEntry
{
    public Reward reward;
    [Range(0, 100)]
    public int chance;
}

public class RewardComponent : MonoBehaviour
{
    [SerializeField] List<RewardEntry> rewardEntries = new List<RewardEntry>();

    public void OnDeathReward()
    {
        foreach (RewardEntry rewardEntry in rewardEntries)
        {
            int chances = Random.Range(0, 100);
            if (rewardEntry.chance > chances)
            {
                Instantiate(rewardEntry.reward, transform.position + Vector3.up, Quaternion.identity);
            }
        }
    }
}
