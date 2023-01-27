using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health = 12;
    public PlayerMovement player;
    GameManager manager;
    Rigidbody2D rb;

    public float followStart = 8f;
    public float followTo = 5f;
    public float speed;

    bool following = false;

    public GameObject bulletPrefab;
    public float bulletSpeed = 3f;
    public float shootDelay = 1f;

    float lastShot;

    public bool staticAim;
    public int bulletCount;

    void Start()
    {
        manager = GameManager.gameManager;
        player = manager.player;
        rb = GetComponent<Rigidbody2D>();
        lastShot = Time.time;
    }
    void IDamageable.Damage(float amount, IDamageable source)
    {
        health -= amount;
        if(health < 0)
        {
            Die();
        }
    }

    IDamageable.Alignment IDamageable.GetAlignment()
    {
        return IDamageable.Alignment.Enemy;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }



    void Shoot()
    {
        if (staticAim)
        {
            ShootCircle(transform.up, bulletCount);
        }
        else {
            Vector2 shootDir = player.transform.position - this.transform.position;
            shootDir = shootDir.normalized;
            ShootCircle(shootDir, bulletCount);
        }
    }

    void ShootCircle(Vector2 upDir, int count)
    {
        upDir = upDir.normalized;
        for(int i = 0; i< count; i++)
        {
            Quaternion rot = Quaternion.Euler(0,0, (360f / count) * i);
            Vector2 shootDir = rot * upDir;
            shootDir = shootDir * bulletSpeed;
            Bullet newBullet = Object.Instantiate(bulletPrefab, this.transform.position, Quaternion.identity, null).GetComponent<Bullet>();
            newBullet.SetStats(1f, 0);
            newBullet.Shoot(shootDir, this, new IDamageable.AlignmentMask(IDamageable.Alignment.Player));
        }
    }

    void FixedUpdate()
    {
        if (!manager.roundActive)
        {
            return;
        }
        if (Time.time > lastShot + shootDelay)
        {
            lastShot = Time.time;
            Shoot();
        }
        float dist = Vector2.Distance(rb.position, player.GetRB().position);
        if (!following && dist > followStart)
        {
            following = true;
        }
        if (following)
        {
            Vector2 dir = player.GetRB().position - rb.position;
            dir = dir.normalized * speed;
            rb.velocity = dir;
            if(dist <= followTo)
            {
                rb.velocity = Vector2.zero;
                following = false;
            }
        }
    }
}
