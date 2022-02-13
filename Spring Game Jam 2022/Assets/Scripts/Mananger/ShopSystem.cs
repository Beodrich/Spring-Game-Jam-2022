using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem instance;

    public enum ItemType{
        Health,
        NineMill,
        ShotGun

    };
    // Start is called before the first frame update
    void Start()
    {
        if(instance==null){
            instance=this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(instance);
        }
    }

    public bool CanBuyItem(int itemCost){
        return itemCost<=GameManager.instance.GetMoney();
    }

    public void ProcessItem(ItemType item,int cost){
        switch(item){
            case ItemType.Health:
            GameManager.instance.DoAction(cost,GameManager.value.Money,false);

            GameManager.instance.DoAction(1,GameManager.value.currentPlayerHp,true);
            break;
            case ItemType.ShotGun:
            
             GameManager.instance.DoAction(cost,GameManager.value.Money,false);
             GameManager.instance.SetWeapon(GameManager.Weapon.ShotGun);

             UIMananger.instance.UpdateCurrentWeapon();
             break;

            case ItemType.NineMill:
             GameManager.instance.DoAction(cost,GameManager.value.Money,false);
             GameManager.instance.SetWeapon(GameManager.Weapon.NineMill);
             UIMananger.instance.UpdateCurrentWeapon();
             break;

            default:
            Debug.LogError("Item does not exist");
            break;


        }
    }
}
