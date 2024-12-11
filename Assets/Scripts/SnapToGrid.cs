using UnityEngine;

[ExecuteInEditMode] // 에디터 모드에서도 스크립트 실행
public class SnapToGrid : MonoBehaviour
{
    public float xStart = 0.51f; // X축 시작점
    public float xStep = 1.0f;   // X축 간격

    public float zStart = 0.5f;  // Z축 시작점
    public float zStep = 1.0f;   // Z축 간격

    public float y = 0.49f;

    public void Update()
    {
        if (!Application.isPlaying) // 에디터 모드에서만 실행
        {
            SnapPositionToGrid();
        }
    }

    void SnapPositionToGrid()
    {
        Vector3 position = transform.position;

        // X와 Z 값을 규칙적으로 스냅
        float snappedX = Mathf.Round((position.x - xStart) / xStep) * xStep + xStart;
        float snappedZ = Mathf.Round((position.z - zStart) / zStep) * zStep + zStart;

        // Y값은 기존 값을 유지
        transform.position = new Vector3(snappedX, y, snappedZ);
    }
}
