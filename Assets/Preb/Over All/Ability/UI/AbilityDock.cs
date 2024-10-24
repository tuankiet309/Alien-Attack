using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityDock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] AbilityComponent abilityComponent;
    [SerializeField] RectTransform root;
    [SerializeField] VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] AbilityUI abilityUIPrefab;
    [SerializeField] float scaleRange = 200f;
    [SerializeField] float highlightedSize = 1.5f;
    [SerializeField] float highlightedSpeed = 20f;

    Vector3 goalScale = Vector3.one;

    List<AbilityUI> abilitiesUI = new List<AbilityUI>();
    PointerEventData touchData;
    AbilityUI hightlightedAbility;

    private void Awake()
    {
        abilityComponent.onNewAbilityAdded+=AbilityAdded;
    }

    private void AbilityAdded(Ability ability)
    {
        AbilityUI newAbilityUI = Instantiate(abilityUIPrefab,root);
        newAbilityUI.Init(ability); 
        abilitiesUI.Add(newAbilityUI);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touchData != null)
        {
            GetUIUnderPointer(touchData, out hightlightedAbility);
            ArrangeScale(touchData);
        }
        transform.localScale = Vector3.Lerp(transform.localScale, goalScale, highlightedSpeed * Time.deltaTime);
    }

    private void ArrangeScale(PointerEventData touchData)
    {
        if (scaleRange == 0)
            return;
        float touchVerticalPos = touchData.position.y;
        foreach (var ability in abilitiesUI) 
        {
            float abilityUiVerticalPos = ability.transform.position.y;
            float distance = Mathf.Abs(touchVerticalPos - abilityUiVerticalPos);
            if (distance > scaleRange) 
            {
                ability.ScaleAmount(0);
                continue;
            }
            float scaleAmt = (scaleRange - distance)/scaleRange;
            ability.ScaleAmount(scaleAmt);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchData = eventData;
        goalScale = Vector3.one * highlightedSize;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(hightlightedAbility)
        {
            hightlightedAbility.ActivateAbility();
        }
        touchData =null;
        goalScale = Vector3.one;
        ResetScale();
    }

    private void ResetScale()
    {
        foreach(AbilityUI ability in abilitiesUI)
        {
            ability.ScaleAmount(0);
        }
    }

    bool GetUIUnderPointer(PointerEventData eventData, out AbilityUI abilityUI)
    {
        List<RaycastResult> findAbility = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, findAbility);
        abilityUI = null;
        foreach (RaycastResult result in findAbility) 
        {
            abilityUI = result.gameObject.GetComponentInParent<AbilityUI>();
            if (abilityUI != null) 
            {
                return true;
            }
        }
        return false;
        
    }
}
