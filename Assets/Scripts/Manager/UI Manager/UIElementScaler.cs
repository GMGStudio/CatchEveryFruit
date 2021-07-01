using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementScaler : MonoBehaviour
{
    Vector3 initialScale;
    private readonly float scaleDuration = .6f;
    private readonly float scaleDurationOut = .2f;

    private void Awake()
    {
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void Hide()
    {
        LeanTween.scale(gameObject, Vector3.zero, scaleDurationOut).setEaseInBack()
            .setIgnoreTimeScale(true)
            .setOnComplete(() => gameObject.SetActive(false));
    }

    public void Show()
    {
        gameObject.SetActive(true);
        LeanTween.scale(gameObject, initialScale, scaleDuration).setEaseOutBack().setIgnoreTimeScale(true);
    }
}
