using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistGrunt_V2 : EnemyAI_V2
{
    [SerializeField]
    private float FixedShootAngle;

    private enum ShootingMode
    {
        tracking, fixedAngle
    }
    [SerializeField]
    private ShootingMode shootMode = ShootingMode.tracking;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Shoot();
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

    private void Shoot()
    {
        if (Time.time > shotCooldown + timeLastFired)
        {
            //Debug.Log("shot");
            // tracking shoot mode
            if (shootMode == ShootingMode.tracking)
            {
                GameObject projectile = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
                Destroy(projectile,2f);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                Vector3 dir = player.transform.position - gunBarrel.position;
                rb.AddForce(dir.normalized * shotForce, ForceMode2D.Impulse);
            }

            // fixed angle shoot mode
            if (shootMode == ShootingMode.fixedAngle)
            {
                GameObject projectile = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                Destroy(projectile,2f);


                Vector3 dir = player.transform.position - gunBarrel.position;
                Vector2 BulletDir = new Vector2(Mathf.Cos(FixedShootAngle), Mathf.Sin(FixedShootAngle));
                rb.AddForce(BulletDir.normalized * shotForce, ForceMode2D.Impulse);
            }

            timeLastFired = Time.time;
        }
    }
}
