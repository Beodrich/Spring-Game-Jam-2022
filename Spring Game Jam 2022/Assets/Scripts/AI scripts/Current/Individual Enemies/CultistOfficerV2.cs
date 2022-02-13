using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistOfficerV2 : EnemyAI_V2
{
    // Cone Variables
    [SerializeField]
    private int ConeBulletNum = 10;
    [SerializeField]
    private float ConeStartAngle = 0f;
    [SerializeField]
    private float ConeEndAngle = 180f;

    // Shotgun Variables
    [SerializeField]
    private int ShotgunBulletNum = 10;
    [SerializeField]
    private float ShotgunStartAngle = 0f;
    [SerializeField]
    private float ShotgunEndAngle = 180f;

    public enum ShootingMode
    {
        Cone, Shotgun
    }
    public ShootingMode shootMode = ShootingMode.Cone;

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
            // cone mode
            if (shootMode == ShootingMode.Cone)
            {
                float angleStep = (ConeEndAngle - ConeStartAngle) / ConeBulletNum;
                float angle = ConeStartAngle;

                Debug.Log("conemode");
                for (int i = 0; i < ConeBulletNum + 1; i++)
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

            if (shootMode == ShootingMode.Shotgun)
            {
                float angleStep = (ShotgunEndAngle - ShotgunStartAngle) / ShotgunBulletNum;
                float angle = ShotgunStartAngle;

                Debug.Log("shotgun");
                for (int i = 0; i < ShotgunBulletNum + 1; i++)
                {
                    float dirX = player.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float dirY = player.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

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

            timeLastFired = Time.time;
        }
    
    }
    protected override void TakeDamage()
    {
        base.TakeDamage();
    }
}
