using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletLogic : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col){
       if(col.gameObject.tag=="Wall" || col.gameObject.tag=="Enemy"){
           Destroy(gameObject);
       }
    }
}
