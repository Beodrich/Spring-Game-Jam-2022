using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMananger : MonoBehaviour
{
    public static UIMananger instance;


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
  
   }

