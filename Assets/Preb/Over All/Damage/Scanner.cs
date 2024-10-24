using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] Transform scannerPivot;
    public delegate void OnScanDetectionUpdate(GameObject newDetection);
    public event OnScanDetectionUpdate onScanDetectionUpdate;
    [SerializeField]float scanRange;
    [SerializeField]float scanDuration;
    internal void SetScanRange(float scanRange)
    {
        this.scanRange = scanRange;
    }
    internal void SetScanDuration(float duration)
    {
        this.scanDuration = duration;   
    }
    internal void AddChildAttached(Transform newChild)
    {
        newChild.parent = scannerPivot;
        newChild.localPosition = Vector3.zero;
    }

    internal void StartScanning()
    {
        scannerPivot.localScale =Vector3.zero;
        StartCoroutine(StartScanningCoroutine());
    }
    IEnumerator StartScanningCoroutine()
    {
        float scanGrowthRare = scanRange/scanDuration;
        float startTime = 0;
        while(startTime<scanDuration)
        {
            startTime += Time.deltaTime;
            scannerPivot.localScale += Vector3.one * scanGrowthRare * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        onScanDetectionUpdate?.Invoke(other.gameObject);
    }
}

