using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettingsSO", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    [Header("Game Settings")]
    public int goalsToWin = 3;
    public float gameDuration = 20f;

    [Header("Power Ups Spawn")]
    public string prefabName;
    public int numberOfPrefabsToCreate;
    public Vector2[] spawnPoints;
}