using System.Collections;
using UnityEngine;

public class UIShake : MonoBehaviour
{
    private RectTransform targetUIElement;

    private Vector3 originalPosition;
    private Coroutine currentShakeRoutine;

    void Start()
    {
        if (targetUIElement == null)
            targetUIElement = GetComponent<RectTransform>();
        originalPosition = targetUIElement.localPosition;
    }

    public void Shake(float shakeDuration, float shakeMagnitude)
    {
        if (currentShakeRoutine != null)
        {
            StopCoroutine(currentShakeRoutine);
            targetUIElement.localPosition = originalPosition; // 위치 초기화
        }

        currentShakeRoutine = StartCoroutine(ShakeRoutine(shakeDuration, shakeMagnitude));
    }

    private IEnumerator ShakeRoutine(float shakeDuration, float shakeMagnitude)
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            targetUIElement.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        targetUIElement.localPosition = originalPosition;
        currentShakeRoutine = null;
    }
}
