using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other){
       if(other.gameObject.tag=="Player"){
           GameManager.instance.DoAction(1,GameManager.value.Money,true);
           Destroy(this.gameObject);
       }
   }
}
