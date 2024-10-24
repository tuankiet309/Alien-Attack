using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    private Transform attachedPoin;
    public void Init(Transform attachedPoin)
    {
        this.attachedPoin = attachedPoin;
    }
    public void SetHealthSliderValue(float health, float delta, float maxHealth)
    {
        healthSlider.value = (health / maxHealth);
    }
    private void Update()
    {
        if (this.attachedPoin == null) return;
        Vector3 attachedScreenPoint = Camera.main.WorldToScreenPoint(attachedPoin.position);
        transform.position = attachedScreenPoint;

    }
    internal void OnOwnerDead(GameObject killer)
    {
        Destroy(gameObject);
    }
}
