using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineMill : MonoBehaviour
{
    private PlayerControl playerControl;

    [SerializeField] private GameObject bullet;

    [SerializeField]private float timer;

    private float timeToShoot;
    // Start is called before the first frame update
    void Start()
    {
        timeToShoot=0;
        playerControl=GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Mouse0)|| Input.GetKeyDown(KeyCode.Space)) && timeToShoot>timer){
            Shoot();
            timeToShoot=0;
        }
        else{
            timeToShoot+=Time.deltaTime;

        }
    }

    void Shoot(){
    var mousePos= Input.mousePosition;
    mousePos.z=2.0f;
    var objPos=Camera.main.ScreenToWorldPoint(mousePos);

    Instantiate(bullet, objPos,Quaternion.identity);



    }
    
}
