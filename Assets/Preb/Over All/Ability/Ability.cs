using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : ScriptableObject
{
    AbilityComponent abilityComponent;
    public AbilityComponent AbilityComponent {  get { return abilityComponent; } private set { abilityComponent = value; } }
    [SerializeField] Sprite abilityIcon;
    [SerializeField] float staminaCost;
    [SerializeField] float cooldownDuration = 2f;
    bool onAbilityCooldown = false;
    public delegate void OnCoolDownStart();
    public event OnCoolDownStart onCoolDownStart;

    [Header("Audio")]
    [SerializeField] AudioClip abilitySound;
    [SerializeField] float volume;

    public abstract void Active();


    internal void Initialize(AbilityComponent abilityComponent)
    {
        this.abilityComponent = abilityComponent;
    }

    //check all condition before activate the ability
    protected bool CommitAbility()
    {
        if(onAbilityCooldown)
            return false;
        if (abilityComponent == null || !abilityComponent.TryConsumeStamina(staminaCost))
        {
            return false;
        }

        StartAbilityCoolDown();
        GameStatic.PlayAudioAtPlayer(abilitySound, volume);
        return true;    
    }
    void StartAbilityCoolDown()
    {
        abilityComponent.StartCoroutine(CooldownCoroutine());
    }
    IEnumerator CooldownCoroutine()
    {
        onAbilityCooldown = true;
        onCoolDownStart?.Invoke();
        yield return new WaitForSeconds(cooldownDuration);
        onAbilityCooldown = false;
    }

    internal Sprite GetAbilityIcon()
    {
        return abilityIcon;
    }

    internal float GetCoolDownDuration()
    {
        return cooldownDuration;
    }
}
