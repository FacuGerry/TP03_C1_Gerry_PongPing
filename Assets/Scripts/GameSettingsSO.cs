using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettingsSO", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    [Header("Game Settings")]
    public int goalsToWin = 3;
    public float gameDuration = 20f;
    public float kickOffTime = 3f;
}