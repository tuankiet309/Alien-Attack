using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class AbilityUI : MonoBehaviour
{
    Ability ability;
    [SerializeField] Image abilityIcon;
    [SerializeField] Image abilityCooldownWheel;
    [SerializeField] float highlightedSize = 1.5f;
    [SerializeField] float offset = 200f;
    [SerializeField] float scaleSpeed = 20f;
    [SerializeField] RectTransform offsetPivot;

    Vector3 GoalScale = Vector3.one;
    Vector3 GoalOffset = Vector3.zero;

    public void ScaleAmount(float amount)
    {
        GoalScale = Vector3.one * (1 + (highlightedSize-1) * amount);
        GoalOffset = Vector3.left * amount * offset;
    }

    bool isOnCoolDown = false;
    float CooldownCounter = 0f;


    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, GoalScale, scaleSpeed * Time.deltaTime);
        offsetPivot.localPosition = Vector3.Lerp(offsetPivot.localPosition, GoalOffset, scaleSpeed * Time.deltaTime);

    }
    internal void ActivateAbility()
    {
        ability.Active();
    }

    internal void Init(Ability ability)
    {
        this.ability = ability;
        abilityIcon.sprite = ability.GetAbilityIcon();
        abilityCooldownWheel.enabled = false;
        this.ability.onCoolDownStart += StartCoolDown;
    }

    private void StartCoolDown()
    {
        if (isOnCoolDown)
            return;
        StartCoroutine(CooldownCoroutine());
    }
    IEnumerator CooldownCoroutine()
    {
        isOnCoolDown=true;
        CooldownCounter = ability.GetCoolDownDuration();
        float cooldownDuration = CooldownCounter;
        abilityCooldownWheel.enabled = true;
        while(CooldownCounter >0)
        {
            CooldownCounter -= Time.deltaTime;
            abilityCooldownWheel.fillAmount = CooldownCounter / cooldownDuration;
            yield return new WaitForEndOfFrame();
        }
        isOnCoolDown = false;
        abilityCooldownWheel.enabled =false;
    }

}
