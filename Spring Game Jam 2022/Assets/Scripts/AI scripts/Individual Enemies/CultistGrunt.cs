using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistGrunt : EnemyAI
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void AgressiveOrbit()
    {
        base.AgressiveOrbit();
    }

    protected override void ChargeAtPlayer()
    {
        base.ChargeAtPlayer();
    }

    protected override void DecideMove()
    {
        base.DecideMove();
    }

    protected override void Die()
    {
        base.Die();
    }

    protected override void FollowPlayer()
    {
        base.FollowPlayer();
    }

    protected override void move()
    {
        base.move();
    }

    protected override void Orbit()
    {
        base.Orbit();
    }

    protected override void shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        //Vector3 dir = player.transform.position - gunBarrel.position;
        Vector3 dir = player.transform.position - gunBarrel.position;
        Vector2 BulletDir = new Vector2(Mathf.Cos(ShootAngle), Mathf.Sin(ShootAngle));
        rb.AddForce(BulletDir.normalized * shotForce, ForceMode2D.Impulse);

        timeLastFired = Time.time;
    }

    protected override void TakeDamage()
    {
        base.TakeDamage();
    }
}
