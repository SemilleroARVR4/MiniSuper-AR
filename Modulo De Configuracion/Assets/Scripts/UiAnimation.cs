using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnimation : MonoBehaviour
{
    [SerializeField]
    float duration;
    [SerializeField]
    float delay;

    [SerializeField]
    AnimationCurve animationCurve;
    [SerializeField]
    RectTransform target;
    [SerializeField]
    Vector2 startingPoint;
    [SerializeField]
    Vector2 finalPoint;

    public void FadeIn(){
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine());
    }
    IEnumerator FadeInCoroutine(){
        float elapsed = 0;
        while (elapsed <= delay){
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0;
        while (elapsed<= duration){
            float percentage = elapsed / duration;
            Vector2 currentPosition = Vector2.Lerp(startingPoint,finalPoint,percentage);
            target.anchoredPosition = currentPosition;
            yield return null;
        }
    }
}
