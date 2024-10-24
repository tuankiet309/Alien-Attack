using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValueGauge : MonoBehaviour
{
    [SerializeField] Image amountImage;
    [SerializeField] TextMeshProUGUI amountText;

    public void UpdateValueVisualization(float value, float delta, float maxValue)
    {
        amountImage.fillAmount = value/maxValue;
        amountText.SetText(value.ToString("F0"));
    }
}
