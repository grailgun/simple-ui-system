using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    public float asteroidFallSpeedMin = 2f;
    public float asteroidFallSpeedMax = 8f;
    public float asteroidLifetime = 10f;

    public UnityEvent<Asteroid> OnAsteroidDestroyed;

    private Rigidbody2D _rigidBody;
    private float _asteroidLifetimeCounter;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _asteroidLifetimeCounter = 0;
        _rigidBody.velocity = Vector3.down * Random.Range(asteroidFallSpeedMin, asteroidFallSpeedMax);
    }

    private void OnDisable()
    {
        OnAsteroidDestroyed?.Invoke(this);
    }

    private void Update()
    {
        _asteroidLifetimeCounter += Time.deltaTime;
        if (_asteroidLifetimeCounter > asteroidLifetime)
        {
            LeanPool.Despawn(this);
        }
    }
}
