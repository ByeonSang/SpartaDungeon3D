using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class InteractionUI : MonoBehaviour, IMovableUI
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private AnimationCurve animDelta;
    [SerializeField] private float duration;

    private RectTransform rectTrans;
    [SerializeField] private Vector3 _endPosition;
    private Vector3 _startPosition;

    private void Awake()
    {
    }

    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        _startPosition = rectTrans.anchoredPosition;
    }

    public void ResetAnimation()
    {
        StopAllCoroutines();
        rectTrans.anchoredPosition = _startPosition;
    }

    public void StartAnimation()
    {
        ResetAnimation();
        StartCoroutine(MoveToEnd());
    }

    private IEnumerator MoveToEnd()
    {
        float currentTime = 0f;
        float percent = 0f;

        while (percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / duration;

            rectTrans.anchoredPosition = Vector3.Lerp(_startPosition, _endPosition, animDelta.Evaluate(percent));
            yield return null;
        }
    }

    public void StopAnimation()
    {
        StartCoroutine(MoveToStart());
    }

    private IEnumerator MoveToStart()
    {
        float currentTime = 0f;
        float percent = 0f;

        while (percent < 1f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / duration;

            rectTrans.anchoredPosition = Vector3.Lerp(_endPosition, _startPosition, animDelta.Evaluate(percent));
            yield return null;
        }
    }

    public void SetText(ItemData item)
    {
        text.text = item.name;
    }
}
