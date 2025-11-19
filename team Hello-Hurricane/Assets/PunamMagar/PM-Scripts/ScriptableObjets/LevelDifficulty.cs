using UnityEngine;

[CreateAssetMenu(fileName = "LevelDifficulty", menuName = "Scriptable Objects/LevelDifficulty")]
public class LevelDifficulty : ScriptableObject
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard,
    }

    public DifficultyLevel difficultyLevel;

    [Range(1, 10)]
    public int timeToReachStartSpeed;

    [Range(1, 100)]
    public int startingSpeed;

    [Range(0.1f, 10f)]
    public float speedIncrementRate;

    [Range(1, 100)]
    public int scoreMultiplier;
}
