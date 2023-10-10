using Core;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : SceneService
{
    [Header("Asteroid Spawner Properties")]
    public Asteroid asteroidPrefab;
    public float spawnInterval = 1f;
    public Vector2 spawnArea = new Vector2(-4f, 4f);

    private float _spawnIntervalCounter;
    private bool _isSpawning;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = SceneServiceProvider.GetService<GameManager>();
    }

    public void BeginSpawning()
    {
        _isSpawning = true;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }

    private void Update()
    {
        if (_isSpawning == false)
            return;

        _spawnIntervalCounter += Time.deltaTime;

        if (_spawnIntervalCounter > spawnInterval)
        {
            var xPosition = Random.Range(spawnArea.x, spawnArea.y);
            var position = new Vector3(xPosition, transform.position.y);
            var asteroid = LeanPool.Spawn(asteroidPrefab, position, Quaternion.identity);
            asteroid.OnAsteroidDestroyed.AddListener(OnAsteroidDestroyed);

            _spawnIntervalCounter = 0;
        }
    }

    private void OnAsteroidDestroyed(Asteroid asteroid)
    {
        _gameManager.AddScore();
        asteroid.OnAsteroidDestroyed.RemoveListener(OnAsteroidDestroyed);
    }
}
