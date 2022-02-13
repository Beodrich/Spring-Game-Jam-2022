 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_V2 : MonoBehaviour
{
    // status variables
    public int health = 1;
    //public Vector2 acceleration;

    // gameObject related
    protected Rigidbody2D rb = new Rigidbody2D();
    protected GameObject player;
    [SerializeField]
    protected Transform gunBarrel;

    // projectile logic
    [SerializeField]
    protected float shotCooldown = 0.5f;
    [SerializeField]
    protected float shotForce = 3f;
    [SerializeField]
    protected GameObject bulletPrefab;

    protected float calculatedSpeed;
    protected float timeLastFired;

    // Movement related
    [SerializeField]
    private List<Move> moveSet = new List<Move>(1);
    protected Move currentMove;
    protected int moveIndex;
    protected float moveStartTime;
    protected bool hitWall;

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
    [SerializeField]
    private movementModes mode = movementModes.moveSet;

    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentMove = moveSet[0];
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (health <= 0)
            Die();
        if (mode == movementModes.moveSet)
            move();
        if (mode == movementModes.charge)
            ChargeAtPlayer();
        if (mode == movementModes.follow)
            FollowPlayer();
        if (mode == movementModes.orbit)
            Orbit();
        if (mode == movementModes.agressiveOrbit)
            AgressiveOrbit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("layer: " + collision.gameObject.layer);
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            hitWall = true;
            //Debug.Log("hit wall");
        }
    }

    protected virtual void DecideMove()
    {
        if (Time.time > moveStartTime + currentMove.duration || hitWall)
        {
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
