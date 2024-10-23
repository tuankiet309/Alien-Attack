using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] Transform shakeTransform;
    [SerializeField] float shakeMagnitude = 1f;
    [SerializeField] float shakeDuration = .2f;
    [SerializeField] float shakeRecoverySpeed = 10f;

    Coroutine ShakeCourtine;
    bool isShaking;
    Vector3 originalPosition;
    private void Start()
    {
        originalPosition = transform.position;
    }
    public void StartShake()
    {

        if(ShakeCourtine == null )
        {
            isShaking = true;
            ShakeCourtine = StartCoroutine(Shake());
        }
    }
    IEnumerator Shake()
    {
        yield return new WaitForSeconds(shakeDuration);
        isShaking = false;
        ShakeCourtine = null;
    }

    private void LateUpdate()
    {
        ProcessShake();
    }

    private void ProcessShake()
    {
        if (isShaking) 
        {
            Vector3 ShakeAmt = new Vector3(Random.value,Random.value, Random.value) * shakeMagnitude * (Random.value>0.5?-1:1);
            shakeTransform.localPosition += ShakeAmt;
        }
        else
        {
            shakeTransform.localPosition = Vector3.Lerp(shakeTransform.localPosition, originalPosition, shakeRecoverySpeed * Time.deltaTime);
        }
    }
}
