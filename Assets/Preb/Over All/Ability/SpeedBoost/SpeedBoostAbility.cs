using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/SpeedBoost")]
public class SpeedBoostAbility : Ability
{
    [SerializeField] float boostAmount;
    [SerializeField] float boostDuration;

    Player player;
    public override void Active()
    {
        if (!CommitAbility())
        {
            return;
        }
        player = AbilityComponent.GetComponent<Player>();
        player.AddMoveSpeed(boostAmount);
        AbilityComponent.StartCoroutine(ResetSpeed());

    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(boostDuration);
        player.AddMoveSpeed(-boostAmount);
    }
}
