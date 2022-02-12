using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health = 1;
    public Vector2 acceleration;
    //public float acceleration = 100f;
    public float shotCooldown;
    public float timeLastFired;

    public GameObject bulletPrefab;
    private Rigidbody2D rb = new Rigidbody2D();

    private GameObject player;
    //private float chargeAcceleration = 5;

    private List<Move> moveSet = new List<Move>() { 
        new Move("up", 1),
        new Move("left", 1),
        new Move("down", 2),
        new Move("right", 2)
    };
    private Move currentMove;
    private int moveIndex;
    private float moveStartTime;

    private bool hitWall;

    [HideInInspector]
    public enum movementModes
    {
        moveSet, follow, charge, orbit, agressiveOrbit
    }

    public movementModes mode = movementModes.moveSet;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentMove = moveSet[0];
    }

    // Update is called once per frame
    void Update()
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

    private void DecideMove()
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

    private void move()
    {
        DecideMove();
        Vector2 velocity = rb.velocity;

        if(currentMove.moveDirection == "right")
        {
            velocity.x += acceleration.x * Time.deltaTime;
        }
        if (currentMove.moveDirection == "left")
        {
            velocity.x -= acceleration.x * Time.deltaTime;
        }
        if (currentMove.moveDirection == "up")
        {
            velocity.y += acceleration.y * Time.deltaTime;
        }
        if (currentMove.moveDirection == "down")
        {
            velocity.y -= acceleration.y * Time.deltaTime;
        }

        rb.velocity = velocity;
    }

    private void shoot()
    {

    }

    private void TakeDamage()
    {
        health -= 1;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void FollowPlayer()
    {
        Vector2 dest = player.transform.position - transform.position;
        rb.velocity = dest;
    }

    private void Orbit() // very janky, wouldn't reccomend using
    {
        Vector3 zAxis = new Vector3(0, 0, 1);
        Vector3 dest = player.transform.position;
        transform.RotateAround(dest, zAxis, 0.2f);
    }

    private void AgressiveOrbit() //really just a slower charge
    {
        Vector2 dest = player.transform.position - transform.position;
        rb.velocity += dest * Time.deltaTime;
    }
    private void ChargeAtPlayer()
    {
        Vector2 dest = player.transform.position - transform.position;
        rb.velocity += dest * 2f * Time.deltaTime;
    }
}

public class Move
{
    public float duration;
    public string moveDirection;

    public Move(string moveDirection, float duration)
    {
        this.moveDirection = moveDirection;
        this.duration = duration;
    }
}
