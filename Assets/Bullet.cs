using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    Rigidbody2D rb;
    public int pierce;
    List<IDamageable> hitTargets = new List<IDamageable>();
    IDamageable shooter = null;

    IDamageable.AlignmentMask mask;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetStats(float damage, int pierce)
    {
        this.damage = damage;
        this.pierce = pierce;
    }

    public void Shoot(Vector2 velocity, IDamageable shooter, IDamageable.AlignmentMask mask)
    {
        rb.velocity = velocity;
        this.shooter = shooter;
        this.mask = mask;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable other = collider.gameObject.GetComponent<IDamageable>();
        if (other != null && mask.HasAlignment(other.GetAlignment()))
        {
            other.Damage(damage, shooter);
            hitTargets.Add(other);
            pierce -= 1;
            if(pierce < 0)
            {
                Remove(false, true);
            }
        }
    }

    public void Remove(bool killed, bool expired)
    {
        Destroy(this.gameObject);
    }
}
