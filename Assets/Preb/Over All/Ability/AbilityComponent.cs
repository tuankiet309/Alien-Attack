using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour, IPurchaseListener, IRewardListener
{
    [SerializeField] Ability[] IniitialAbilities; //

    private List<Ability> abilities = new List<Ability>();

    public delegate void OnNewAbilityAdded(Ability ability);

    public event OnNewAbilityAdded onNewAbilityAdded;

    public delegate void OnStaminaChanged(float stamina, float maxStamina);
    public event OnStaminaChanged onStaminaChanged;

    [SerializeField] float stamina = 200f;
    [SerializeField] float maxStamina = 200f;

    private void Start()
    {
        foreach (var ability in IniitialAbilities)
        {
            GiveAbility(ability);
        }
    }

    private void GiveAbility(Ability ability)
    {
        Ability newAbility = Instantiate(ability);
        newAbility.Initialize(this);
        abilities.Add(newAbility);
        onNewAbilityAdded?.Invoke(newAbility);
    }


    public void ActivateAbility(Ability abilityToActivate)
    {
        if (abilities.Contains(abilityToActivate)) 
        {
            abilityToActivate.Active();
        }
    }
    public float GetStamina()
    {

        return stamina; 
    }

    public bool TryConsumeStamina(float amountStaminaConsume)
    {
        if (stamina < amountStaminaConsume)
            return false;
        stamina -= amountStaminaConsume;
        onStaminaChanged?.Invoke(stamina,maxStamina);
        return true;
    }
    public void BroadCastStaminaRightAway()
    {
        onStaminaChanged?.Invoke(stamina, maxStamina);
    }

    public bool HandlePurchase(UnityEngine.Object newPurchase)
    {
        Ability itemAbility = newPurchase as Ability;
        if (itemAbility == null) return false;
        GiveAbility(itemAbility);
        return true;
    }

    public void Reward(Reward reward)
    {
        stamina = Mathf.Clamp(stamina+reward.staminaReward,0,maxStamina);
        BroadCastStaminaRightAway();
    }
}
