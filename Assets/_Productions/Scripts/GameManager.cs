using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SceneService
{
    [Header("Game Properties")]
    [SerializeField]
    private int currentScore;
    [SerializeField]
    private Player player;

    public UnityEvent<int> OnScoreChange;

    private AsteroidSpawner _asteroidSpawner;

    private void Awake()
    {
        _asteroidSpawner = SceneServiceProvider.GetService<AsteroidSpawner>();
    }

    public void AddScore()
    {
        currentScore++;
        OnScoreChange?.Invoke(currentScore);
    }

    public void StartGame()
    {
        _asteroidSpawner.BeginSpawning();
        player.SetPlaying(true);
    }
}
