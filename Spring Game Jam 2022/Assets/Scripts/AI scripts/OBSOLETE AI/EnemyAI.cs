using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // status variables
    public int health = 1;
    //public Vector2 acceleration;

    // gameObject related
    protected Rigidbody2D rb = new Rigidbody2D();
    protected GameObject player;
    public Transform gunBarrel;

    // projectile logic
    public float shotCooldown = 0.5f;
    //public float projSpeed = 5f;
    public float shotForce = 3f;
    protected float calculatedSpeed;
    protected float timeLastFired;
    public GameObject bulletPrefab;
    
    // Movement related
    public List<Move> moveSet = new List<Move>(1);
    protected Move currentMove;
    protected int moveIndex;
    protected float moveStartTime;
    protected bool hitWall;

    public float ShootAngle;

    // Cone shooting mode stuff
    [SerializeField]
    private int numConeBullets = 10;
    [SerializeField]
    private float startAngle = 0f;
    [SerializeField]
    private float endAngle = 180f;

    // Shotgun Shooting Mode
    [SerializeField]
    private int numShotgunBullets = 2;
    [SerializeField]
    private float shotgunSA = 60;
    [SerializeField]
    private float shotgunEA = 120;



    // enums
    [HideInInspector]
    public enum movementModes
    {
        moveSet, follow, charge, orbit, agressiveOrbit, still
    }
    [HideInInspector]
    public enum movementDirections
    {
        up, down, left, right
    }
    private enum ShootingMode
    {
        tracking, fixedAngle, Cone, Shotgun
    }

    [SerializeField]
    private ShootingMode shootMode = ShootingMode.tracking;
    public movementModes mode = movementModes.moveSet;

    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentMove = moveSet[0];
        ShootAngle*=Mathf.Rad2Deg;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (health <= 0)
            Die();
        if(mode == movementModes.moveSet)
            move();
        if (mode == movementModes.charge)
            ChargeAtPlayer();
        if (mode == movementModes.follow)
            FollowPlayer();
        if (mode == movementModes.orbit)
            Orbit();
        if (mode == movementModes.agressiveOrbit)
            AgressiveOrbit();
        //calculatedSpeed = projSpeed * Time.deltaTime;
        shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("layer: " + collision.gameObject.layer);
        if(collision.gameObject.tag == "PlayerProjectile")
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            hitWall = true;
            //Debug.Log("hit wall");
        }
    }

    protected virtual void DecideMove()
    {
        if(Time.time > moveStartTime + currentMove.duration || hitWall)
        {
            //rb.velocity = new Vector2(0, 0);
            moveIndex++;
            if (hitWall)
            {
                //Debug.Log("Hit wall");
                hitWall = false;
            }
            if (moveIndex == moveSet.Count)
            {
                moveIndex = 0;
            }
            currentMove = moveSet[moveIndex];
            //Debug.Log(currentMove.moveDirection);
            moveStartTime = Time.time;
        }
    }

    protected virtual void move()
    {
        DecideMove();
        Vector2 velocity = rb.velocity;

        if (currentMove.MoveClassDirection == movementDirections.right)
        {
            velocity.x += currentMove.movementAccel.x * Time.deltaTime;
        }
        if (currentMove.MoveClassDirection == movementDirections.left)
        {
            velocity.x -= currentMove.movementAccel.x * Time.deltaTime;
        }
        if (currentMove.MoveClassDirection == movementDirections.up)
        {
            velocity.y += currentMove.movementAccel.y * Time.deltaTime;
        }
        if (currentMove.MoveClassDirection == movementDirections.down)
        {
            velocity.y -= currentMove.movementAccel.y * Time.deltaTime;
        }

        rb.velocity = velocity;
    }

    protected virtual void shoot()
    {
        if (Time.time > shotCooldown + timeLastFired)
        {
            // tracking shoot mode
            if (shootMode == ShootingMode.tracking)
            {
                GameObject projectile = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                Vector3 dir = player.transform.position - gunBarrel.position;
                rb.AddForce(dir.normalized * shotForce, ForceMode2D.Impulse);
            }

            // fixed angle shoot mode
            if (shootMode == ShootingMode.fixedAngle)
            {
                GameObject projectile = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                Vector3 dir = player.transform.position - gunBarrel.position;
                Vector2 BulletDir = new Vector2(Mathf.Cos(ShootAngle), Mathf.Sin(ShootAngle));
                rb.AddForce(BulletDir.normalized * shotForce, ForceMode2D.Impulse);
            }

            // cone mode
            if (shootMode == ShootingMode.Cone)
            {
                float angleStep = (endAngle - startAngle) / numConeBullets;
                float angle = startAngle;

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

            // Shotgun mode
            if (shootMode == ShootingMode.Shotgun)
            {
                float angleStep = (shotgunEA - shotgunSA) / numShotgunBullets;
                float angle = startAngle;

                Debug.Log("shotgun");
                for (int i = 0; i < numShotgunBullets + 1; i++)
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

    protected virtual void TakeDamage()
    {
        health -= 1;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void FollowPlayer()
    {
        Vector2 dest = player.transform.position - transform.position;
        rb.velocity = dest;
    }

    protected virtual void Orbit() // very janky, wouldn't reccomend using
    {
        Vector3 zAxis = new Vector3(0, 0, 1);
        Vector3 dest = player.transform.position;
        transform.RotateAround(dest, zAxis, 0.2f);
    }

    protected virtual void AgressiveOrbit() //really just a slower charge
    {
        Vector2 dest = player.transform.position - transform.position;
        rb.velocity += dest * 1.5f * Time.deltaTime;
    }
    protected virtual void ChargeAtPlayer()
    {
        Vector2 dest = player.transform.position - transform.position;
        rb.velocity += dest * 2f * Time.deltaTime;
    }


    [System.Serializable]
    public class Move
    {
        public float duration;
        public Vector2 movementAccel = new Vector2(3, 2);
        public movementDirections MoveClassDirection;

        [SerializeField]
        private Move(movementDirections MoveClassDirection, float duration)
        {
            this.MoveClassDirection = MoveClassDirection;
            this.duration = duration;
        }
    }

}


