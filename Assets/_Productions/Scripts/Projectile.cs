using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public float projectileLifetime = 5f;

    [Header("Asteroid")]
    public LayerMask asteroidLayer;

    private float _projectileLifetimeCounter = 0;

    private void OnEnable()
    {
        _projectileLifetimeCounter = 0f;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);

        _projectileLifetimeCounter += Time.deltaTime;
        if (_projectileLifetimeCounter > projectileLifetime)
            LeanPool.Despawn(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsEqual(collision.gameObject.layer, asteroidLayer) == false)
            return;

        _projectileLifetimeCounter = 0;
        LeanPool.Despawn(this);
        LeanPool.Despawn(collision.gameObject);
    }

    private bool IsEqual(LayerMask layer1, LayerMask layer2)
    {
        return ((1 << layer1) & layer2) != 0;
    }
}
