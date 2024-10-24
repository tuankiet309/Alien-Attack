using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string AttachSlotTag; //Tag để nhận biết slot để gán vũ khí vào sao cho hợp với model và animation
    [SerializeField] AnimatorOverrideController overrideController; //Animatior override để thay đổi animation cầm súng với từng sứng khác nhau
    [SerializeField] protected float amount = -2f; //Sát thương
    [SerializeField] protected float attackRateMult = 2f;
    [SerializeField] protected AudioClip weaponAudio;
    [SerializeField] protected float volume;

    AudioSource weaponAudioSource;
    protected virtual void Start()
    {
        weaponAudioSource = GetComponent<AudioSource>();

    }
    public void PlayWeaponAudio()
    { 
        weaponAudioSource.PlayOneShot(weaponAudio, volume);
    }

    public abstract void Attack() ; //Abstract để có thể tùy chỉnh ở class con, có thể là range attack hoặc melee
    public string GetAttachSlotTag() //Lấy tag của weapon
    {
        return AttachSlotTag;
    }
    public GameObject Owner {  get; private set; } //Lấy owner của weapon

    public void Init(GameObject owner) //Cài owner và unequip vũ khí mỗi khi khởi tạo
    { 
        this.Owner = owner;
        Unequip();
    }
    public void Equip() //Hàm equip vĩ khí 
    {
        gameObject.SetActive(true); //Activate gameObject của vũ khí
        Owner.GetComponent<Animator>().runtimeAnimatorController = overrideController; //Đổi animator của player thành animator của vũ khi để thay đổi animation cầm súng với từng sứng khác nhau
        Owner.GetComponent<Animator>().SetFloat("attackSpeed",attackRateMult);
    }
    public void Unequip()
    {
        gameObject.SetActive(false); //Deactivate
    }

    protected void DamageOnGameObject(GameObject gameObject,float amount) //Gây sát thương lên vật bị bắn
    {
        HealthComponent healthComponent = gameObject.GetComponent<HealthComponent>(); //Lấy thành phần health của vật bị bắn
        if (healthComponent != null) //Nếu ko có không làm gì hết
        {
            healthComponent.ChangeHealth(-amount,Owner); //Trừ máu của vật
        }

    }

}
