using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualize : MonoBehaviour
{
    [SerializeField] Renderer mesh;
    [SerializeField] Color damageEmissionColor;
    [SerializeField] float blinkSpeed;
    [SerializeField] string EmmissionColor = "_Addition";
    [SerializeField] HealthComponent healthComponent;

    Color originalColor;
    void Start()
    {
        Material material = mesh.material;
        mesh.material = new Material(material);
        originalColor = mesh.material.GetColor(EmmissionColor);
        healthComponent.onTakeDamamge += TookDamage;
    }

    protected virtual void TookDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
        Color currentEmission = mesh.material.GetColor(EmmissionColor);
        if (Mathf.Abs((currentEmission - originalColor).grayscale) < 0.1)
        {
            mesh.material.SetColor(EmmissionColor, damageEmissionColor);
        }
    }

    private void Update()
    {
        Color current = mesh.material.GetColor(EmmissionColor);
        Color newColor = Color.Lerp(current,originalColor, blinkSpeed*Time.deltaTime);
        mesh.material.SetColor(EmmissionColor, newColor); 
    }

}
