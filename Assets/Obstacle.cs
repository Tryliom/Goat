using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float AppearTime = 1f;
    public float StayTime = 5f;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private Vector3 originalPosition;
    private Vector3 targetPosition;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = new Vector3(originalScale.x, originalScale.y + 1, originalScale.z);
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x, originalPosition.y + 1, originalPosition.z);
        
        StartCoroutine(AnimateAppearance());
    }

    IEnumerator AnimateAppearance()
    {
        float elapsedTime = 0f;

        while (elapsedTime < AppearTime)
        {
            float progress = elapsedTime / AppearTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, progress);
            transform.position = Vector3.Lerp(originalPosition, targetPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(StayTime);
        StartCoroutine(AnimateDisappearance());
    }

    IEnumerator AnimateDisappearance()
    {
        float elapsedTime = 0f;

        while (elapsedTime < AppearTime)
        {
            float progress = elapsedTime / AppearTime;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, progress);
            transform.position = Vector3.Lerp(targetPosition, originalPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
