using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Gun Properties")]
    public Projectile projectilePrefab;
    public Transform gunFirePoint;

    [Header("Move Properties")]
    public float speed = 10f;
    public Vector2 positionBorder = new Vector2(-10, 10);

    private bool _isPlaying;

    public void SetPlaying(bool condition)
    {
        _isPlaying = condition;
    }

    private void Update()
    {
        if (_isPlaying == false)
            return;

        Shoot();
        Move();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LeanPool.Spawn(projectilePrefab, gunFirePoint.position, Quaternion.identity);            
        }
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");

        transform.Translate(speed * (Vector3.right * horizontalMove) * Time.deltaTime);

        var pos = transform.position;
        if (pos.x < positionBorder.x)
            pos.x = positionBorder.x;
        else if (pos.x > positionBorder.y)
            pos.x = positionBorder.y;

        transform.position = pos;
    }
}
