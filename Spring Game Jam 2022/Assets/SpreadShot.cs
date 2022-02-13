using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{
     private GameObject player;

     [SerializeField] private Transform gunTip;

    [SerializeField] private GameObject bullet;


    [SerializeField] private GameObject crosshair;

    [SerializeField]private float timer;

    private float timeToShoot;

    [SerializeField] private float speed;


    private Vector3 target;

    [SerializeField] private float angle;

    [SerializeField] private int pellotCount;


    private List<Quaternion> pellets;

    void Awake(){
        pellets= new List<Quaternion>(pellotCount);
        for(int i=0; i<pellotCount;++i){
            pellets.Add(Quaternion.Euler(Vector2.zero));
        }
    }

    private float numConeBullets=5;
    // Start is called before the first frame update
    void Start()
    {
        timeToShoot=0;
        player=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        target=Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,transform.position.z));
        crosshair.transform.position=new Vector2(target.x,target.y);
        Vector3 difference= target-player.transform.position;
        float rotationZ= Mathf.Atan2(difference.y,difference.x)*Mathf.Rad2Deg;
        if((Input.GetKeyDown(KeyCode.Mouse0)|| Input.GetKeyDown(KeyCode.Space)) && timeToShoot>timer){
            float distance= difference.magnitude;
            Vector2 direction= difference/distance;
            direction.Normalize();
            Shoot(difference,rotationZ);


            timeToShoot=0;
        }
        else{
            timeToShoot+=Time.deltaTime;

        }
    }

    void Shoot(Vector2 direction, float roation){

        for(int i=0; i<pellotCount;++i){
            pellets[i]=Random.rotation;
            GameObject p= Instantiate(bullet,gunTip.position,gunTip.rotation);
            p.transform.rotation=Quaternion.RotateTowards(p.transform.rotation,pellets[i],angle);
            p.GetComponent<Rigidbody2D>().AddForce(p.transform.right*direction*speed);


        }
            






    }

   
   }

