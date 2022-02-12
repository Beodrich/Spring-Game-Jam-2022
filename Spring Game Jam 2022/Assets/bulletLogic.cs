using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletLogic : MonoBehaviour
{
    void OnCollisonEnter2D(Collider2D col){
       if(col.gameObject.tag=="Wall"){
           Destroy(gameObject);
       }
    }
}
