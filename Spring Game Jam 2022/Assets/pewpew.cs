using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pewpew : MonoBehaviour
{
     private Transform player;
    private Vector3 target;

    [SerializeField] private AudioSource music;

    [SerializeField] private float destoryTimer=3f;

    [SerializeField]private float speed;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player").transform;
        target=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z=2.0f;
        Destroy(this.gameObject,destoryTimer);
        music.Play();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=target.normalized*speed;       
    }



}
