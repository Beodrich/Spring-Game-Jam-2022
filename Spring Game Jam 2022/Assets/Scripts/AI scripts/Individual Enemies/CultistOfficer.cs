using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistOfficer : EnemyAI
{
    //public enum ShootingMode 
    //{ 
    //    tracking, fixedAngle, Cone
    //}
    
    //public ShootingMode shootMode = ShootingMode.tracking; 
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
        base.shoot();
        if (Time.time > shotCooldown + timeLastFired)
        {
            if (shootMode == ShootingMode.Cone)
            {
                // tbd
            }
        }
    }

    protected override void TakeDamage()
    {
        base.TakeDamage();
    }
}
