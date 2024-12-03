using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceBarUI : MonoBehaviour
{
    public Image distancePointer;

    public GameObject camera;
    private float levelStartPositionZ;
    private float levelEndPositionZ;
    private float levelRangeZ;
    private float pointerRangeX = 492f;
    private Vector2 pointerStartPosition;
    private float cameraOffsetZ = 5f;

    // Start is called before the first frame update
    void Start()
    {
        levelStartPositionZ = camera.transform.position.z + cameraOffsetZ;
        levelEndPositionZ = StageManager.Instance.GetGoalZ() - cameraOffsetZ;
        levelRangeZ = levelEndPositionZ - levelStartPositionZ;
        pointerStartPosition = distancePointer.rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float cameraProgress = Mathf.Clamp01((camera.transform.position.z - levelStartPositionZ) / levelRangeZ);
        float pointerOffsetX = cameraProgress * pointerRangeX;
        RectTransform pointerRect = distancePointer.rectTransform;
        pointerRect.anchoredPosition = new Vector2(pointerStartPosition.x + pointerOffsetX, pointerRect.anchoredPosition.y);
    }
}
