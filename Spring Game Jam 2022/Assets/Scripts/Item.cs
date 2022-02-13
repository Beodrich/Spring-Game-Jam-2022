using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{   
    
    [SerializeField] private int cost;
    [SerializeField] private ShopSystem.ItemType itemType;




    void Start(){
        Debug.Assert(cost>=0);//make sure cost is >= 0

    }
    void OnTriggerEnter2D(Collider2D col){
       
        if(col.gameObject.tag=="Player"){

            if(ShopSystem.instance.CanBuyItem(cost)){
                ShopSystem.instance.ProcessItem(itemType,cost);
                Destroy(this.gameObject);
            }
            else{
            UIMananger.instance.UpdateItemUI(itemType,cost);
            UIMananger.instance.SetItemUIActive(true);
            }
        }
    }
    void OnTriggerExit2D(Collider2D col){
        UIMananger.instance.SetItemUIActive(false);
    }
    IEnumerator BuyItem(){
        yield return new WaitForSeconds(1f);
    }
}
