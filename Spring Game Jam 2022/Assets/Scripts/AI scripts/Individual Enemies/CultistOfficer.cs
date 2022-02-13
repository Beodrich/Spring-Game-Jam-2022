using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistOfficer : EnemyAI
{
    [SerializeField]
    private int numConeBullets = 10;
    [SerializeField]
    private float startAngle = 0f;
    [SerializeField]
    private float endAngle = 180f;
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
        float angleStep = (endAngle - startAngle) / numConeBullets;
        float angle = startAngle;

        if (Time.time > shotCooldown + timeLastFired)
        {
            if (shootMode == ShootingMode.Cone)
            {
                Debug.Log("conemode");
                for (int i = 0; i < numConeBullets + 1; i++)
                {
                    float dirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float dirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                    Vector3 bulletMoveVector = new Vector3(dirX, dirY, 0f);
                    Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

                    GameObject bullet = Instantiate(bulletPrefab);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    bullet.transform.position = gunBarrel.position;
                    bullet.transform.rotation = gunBarrel.rotation;
                    rb.AddForce(bulletDir * shotForce, ForceMode2D.Impulse);

                    angle += angleStep;

                }
            }
        }
        base.shoot();
    }

    protected override void TakeDamage()
    {
        base.TakeDamage();
    }
}
