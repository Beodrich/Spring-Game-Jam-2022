using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMananger : MonoBehaviour
{
    [SerializeField] private GameObject dealthText;
    public static UIMananger instance;

    [SerializeField] private TMP_Text itemCostText;


    [SerializeField] private GameObject dealthScene;

    bool isDead=true;


    public GameObject FullHP;

    public GameObject ThreeHP;

    public GameObject TwoHP;

    public GameObject OneHP;

    public GameObject Dead;






    public enum HealthState{
        Four=4
        ,Three=3,
        Two=2,
        One=1,
        Dead=0
    }
    [SerializeField] private TMP_Text moneyText;

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
    void Update(){
        if(isDead){
            if(Input.GetKey(KeyCode.Space)){
                GameManager.instance.LoadLevel(SceneManager.GetActiveScene().name);
                isDead=false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled=true;
                dealthText.SetActive(false);
                dealthScene.SetActive(false);
            }
            else if(Input.GetKey(KeyCode.Escape)){
                GameManager.instance.Quit();
            }
        }
    }

   public void UpdatePlayerHealth(HealthState state){
       switch (state){
        case HealthState.Four:
        FullHP.SetActive(true);
        ThreeHP.SetActive(false);
        TwoHP.SetActive(false);
        OneHP.SetActive(false);
        Dead.SetActive(false);

        break;
        case HealthState.Three:
        FullHP.SetActive(false);
        ThreeHP.SetActive(true);
        TwoHP.SetActive(false);
        OneHP.SetActive(false);
        Dead.SetActive(false);
        break;
        case HealthState.Two:
        FullHP.SetActive(false);
        ThreeHP.SetActive(false);
        TwoHP.SetActive(true);
        OneHP.SetActive(false);
        Dead.SetActive(false);
        break;
        case HealthState.One:
        FullHP.SetActive(false);
        ThreeHP.SetActive(false);
        TwoHP.SetActive(false);
        OneHP.SetActive(true);
        Dead.SetActive(false);
        break;
        case HealthState.Dead:
        FullHP.SetActive(false);
        ThreeHP.SetActive(false);
        TwoHP.SetActive(false);
        OneHP.SetActive(false);
        Dead.SetActive(true);
        dealthScene.SetActive(true);
        dealthText.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled=false;

        break;
        default:
        Debug.LogError("Health not supported");
        break;





       }
    

    
   }
   public void UpdateMoneyUI(){
       int money=GameManager.instance.GetMoney();
       moneyText.text= "$ "+ money;



   }


   public void UpdateItemUI(ShopSystem.ItemType item,int cost){
       switch(item){
           case ShopSystem.ItemType.Health:
           itemCostText.text="Potion cost $ "+ cost;
           break;
           case ShopSystem.ItemType.NineMill:
           itemCostText.text="NineMill cost $ "+cost;
           break;
           case ShopSystem.ItemType.ShotGun:
           itemCostText.text="ShotGun cost $ "+cost;
           break;
           default:
           itemCostText.text="Item not supported";
           break;
       }
       

   }
   public void SetItemUIActive(bool value){
        itemCostText.gameObject.SetActive(value);    
    }
   public void UpdateCurrentWeapon(){
       switch(GameManager.instance.GetCurrentWeapon()){
           case GameManager.Weapon.NineMill:
           break;

         case GameManager.Weapon.ShotGun:
          break;


       }
       
   }
  
   }

