using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMananger : MonoBehaviour
{
    public static UIMananger instance;

    public GameObject[] playerHPBar;

    private GameObject currentHPBar;
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

   public void UpdatePlayerHealth(){
    int amount= GameManager.instance.GetPlayerHealth();
    switch(amount){
        case 0:
        currentHPBar=playerHPBar[0];//shoule be dead
        
        break;

        case 1:
        currentHPBar=playerHPBar[1];
        break;

        case 2:
        currentHPBar=playerHPBar[2];
        break;

        case 3:
        currentHPBar=playerHPBar[3];

        break;

        case 4:
        currentHPBar=playerHPBar[4];
        break;

        default:
        Debug.LogError("Hp needs to be between 0-4");
        break;


    }
   }
}
