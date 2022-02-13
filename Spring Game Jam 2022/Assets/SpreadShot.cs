using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour
{
     private GameObject player;

    [SerializeField] private GameObject bullet;


    [SerializeField] private GameObject crosshair;

    [SerializeField]private float timer;

    private float timeToShoot;

    [SerializeField] private float speed;


    private Vector3 target;
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
            Shoot(difference,rotationZ,0f);
            Shoot(difference,rotationZ,30f);
            Shoot(difference,rotationZ,60f);


            timeToShoot=0;
        }
        else{
            timeToShoot+=Time.deltaTime;

        }
    }

    void Shoot(Vector2 direction, float roation, float offset){

   GameObject bulletCopy= Instantiate(bullet) as GameObject;
   bulletCopy.transform.position= player.transform.position;
   bulletCopy.transform.rotation= Quaternion.Euler(0.0f,0.0f,roation+offset);
   bulletCopy.GetComponent<Rigidbody2D>().velocity=direction*speed;





    }

   
   }

