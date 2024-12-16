
using UnityEngine;

public class ScoreModel
{
    ScoreUI scoreUI;
    private float startTime;
    private float startZ;
    private float referenceSpeed;
    private int trashDisposeCount;
    private int trashMissCount;
    private int trashPickupCount;

    public static ScoreModel Instance;

    public ScoreModel(float startZ, float referenceSpeed, ScoreUI scoreUI)
    {
        startTime = Time.time;
        this.startZ = startZ;
        this.referenceSpeed = referenceSpeed;
        this.scoreUI = scoreUI;
    }


    public int CalculateFinalScore(float currentZ)
    {
        float offsetZ = currentZ - startZ;
        float referenceTime = offsetZ / referenceSpeed;
        float playTime = Time.time - startTime;
        float clampedTimeScore = Mathf.Clamp(referenceTime - playTime, -20.0f, 20.0f);
        return (int)(clampedTimeScore + 5 * (trashPickupCount + trashDisposeCount - trashMissCount) + 100);
    }

    public void UpdateScore(float currentZ)
    {
        float offsetZ = currentZ - startZ;
        float referenceTime = offsetZ / referenceSpeed;
        float playTime = Time.time - startTime;
        float clampedTimeScore = Mathf.Clamp(referenceTime - playTime, -20.0f, 20.0f);
        int score = (int)(clampedTimeScore + 5 * (trashPickupCount + trashDisposeCount - trashMissCount) + 100);
        scoreUI.UpdateScore(score);
    }

    public void IncTrashPickupCount()
    {
        ++trashPickupCount;
    }

    public void IncTrashDisposeCount()
    {
        ++trashDisposeCount;
    }

    public void IncTrashMissCount()
    {
        ++trashMissCount;
    }

}
