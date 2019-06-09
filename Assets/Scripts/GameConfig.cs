using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Create game config")]
public class GameConfig : ScriptableObject
{
    [Header("Player")]
    public float speed = 8f;
    public float jumpForce = 30f;

    [Header("Bonuses")]
    public List<Bonuses> bonuses = new List<Bonuses>();
    public float spawnDelay = 3f;
    public int timeToDestroy = 5;
    public int bonusesAmount = 3;
    public int coinScore = 10;
    public float doubleScoreDuration = 20f;
    public float jumpModeDuration = 10f;
    [Space]
    public Sprite jumpModeIcon;
    public Sprite doubleCoinsScoreIcon;

    [Header("Audio")]
    public AudioClip jumpClip;
    public AudioClip gameOverClip;
    
    [Header("Platforms on the screen")]
    public int platformsOnScreen = 10;

    [Header("UI")]
    public string scoreText = "Score:";
    public string bestScoreText = "HighScore:";
}
