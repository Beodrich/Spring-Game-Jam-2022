using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedBullet : MonoBehaviour
{
    private Transform player;
    private Vector2 target;
    public float speed = 1f;
    private float calculatedSpeed;
    Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        //target = target * 100;
        dest = player.position - transform.position;
        //target = new Vector2(dest.x, dest.y);
    }

    // Update is called once per frame
    void Update()
    {
        //calculatedSpeed = speed * Time.deltaTime;
        //transform.position = Vector2.MoveTowards(transform.position, target, calculatedSpeed);
        ////this.transform.position = Vector3.MoveTowards(transform.position, target, calculatedSpeed);

        //if (transform.position.x == target.x && transform.position.y == target.y)
        //{
        //    Debug.Log("reached");
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    //Debug.l
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            // damage player
        }
    }

}
