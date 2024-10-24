using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IRewardListener
{
    public delegate void OnHealthChange(float health, float delta, float maxHealth); // delegate khi máu thay đổi
    public delegate void OnTakeDamamge(float health, float delta, float maxHealth, GameObject Instigator); // delegate khi nhận sát thương
    public delegate void OnHealthEmpty(GameObject killer); // delegate khi hết máu

    [SerializeField] float maxHealth = 100;
    [SerializeField] float health = 100;

    public event OnHealthChange onHealthChange; // Các event tương ứng
    public event OnTakeDamamge onTakeDamamge;
    public event OnHealthEmpty onHealthEmpty;

    [Header("Audio")]
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip death;
    [SerializeField] float volume;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public float GetMaxHealth => this.maxHealth;
    public float GetHealth => this.health;

    protected void SetHealth(float amount)
    {
        this.health += amount;
    }
    
    protected void InvokeOnHealthChange(float health, float delta, float maxHealth)
    {
        onHealthChange?.Invoke(health, delta, maxHealth);
    }

    protected void InvokeOnTakeDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
        onTakeDamamge?.Invoke(health, delta, maxHealth, Instigator);
    }

    protected void InvokeOnHealthEmpty(GameObject killer)
    {
        onHealthEmpty?.Invoke(killer);
    }
    
    protected void PlayHitAudio()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(hit);
        }
    }
    
    protected void PlayDeathAudioAtPos(Vector3 pos)
    {
        GameStatic.PlayAudioAtLoc(death, pos, volume);
    }

    public virtual void ChangeHealth(float amount, GameObject Instigator)
    {
        if (amount == 0 || health == 0) return;
        
        health += amount; // thay đổi máu bằng damage
        health = Mathf.Clamp(health, 0, maxHealth);

        if (amount < 0) // Nếu nhận sát thương
        {
            InvokeOnTakeDamage(health, amount, maxHealth, Instigator); // Gọi sự kiện thông qua phương thức bảo vệ
            Vector3 loc = transform.position;
            this.PlayHitAudio();
        }

        InvokeOnHealthChange(health, amount, maxHealth); // Gọi sự kiện thông qua phương thức bảo vệ

        if (health <= 0)
        {
            health = 0;
            InvokeOnHealthEmpty(Instigator); // Gọi sự kiện thông qua phương thức bảo vệ
            Vector3 loc = transform.position;
            PlayDeathAudioAtPos(loc);
        }

        Debug.Log($"{gameObject} taking Damage {amount}, health now is {health}"); // Log để kiểm tra
    }

    public void BroadCastHealthValueImmediately()
    {
        InvokeOnHealthChange(health, 0, maxHealth);
    }

    public void Reward(Reward reward)
    {
        health = Mathf.Clamp(health + reward.healthReward, 0, maxHealth);
        InvokeOnHealthChange(health, reward.healthReward, maxHealth); // Gọi sự kiện thông qua phương thức bảo vệ
    }
}

