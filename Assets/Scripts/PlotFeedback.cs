using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotFeedback : MonoBehaviour
{
    [Header("Passive Animation")]
    [SerializeField] private float rotationSpeed = 90f;
    [Header("Bulge Effect")]
    [SerializeField] private float bulgeStartTime = 0.15f;
    [SerializeField] private float bulgeStopTime = 0.3f;
    [SerializeField] private float bulgeScale = 1.3f;
    [Header("Spin Effect")]
    [SerializeField] private float spinTime = 0.3f;

    private void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rot);
    }

    public void Bulge()
    {
        transform.DOScale(bulgeScale, bulgeStartTime)
            .OnComplete(() => transform.DOScale(1f, bulgeStopTime));
    }

    public void Spin()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), spinTime, RotateMode.LocalAxisAdd);
    }
}
