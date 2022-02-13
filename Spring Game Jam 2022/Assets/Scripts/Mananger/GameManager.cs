using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    

    private int score;

    private int currentPlayerHp;

    private int money;

    private const int MAX_PLAYER_HP=4;

    public enum value{
        Money,
        score,
        currentPlayerHp
    };

public enum Weapon{
NineMill,
ShotGun

};
public Weapon currentWeapon= Weapon.NineMill;
    


    // Start is called before the first frame update
    void Start()
    {
     if(instance==null){
         instance=this;
         DontDestroyOnLoad(this);
         SetPlayerHP();
         UIMananger.instance.UpdateCurrentWeapon();
         

        
     }   
     else{
         Destroy(instance);

    }

    }
    /// <summary>
    /// sets the player's hp to be the max hp
    /// </summary>
    public void SetPlayerHP(){
    currentPlayerHp=MAX_PLAYER_HP;

    }
    public void SetWeapon(Weapon weapon){
        currentWeapon=weapon;
        GameObject player= GameObject.FindGameObjectWithTag("Player");

        switch(currentWeapon){
            case Weapon.NineMill:
            player.GetComponent<SpreadShot>().enabled=false;
            player.GetComponent<NineMill>().enabled=true;
            

            //TODO UPDATE WHAT WEAPON THE PLAYER HAS
            break;
            case Weapon.ShotGun:
            player.GetComponent<SpreadShot>().enabled=true;
            player.GetComponent<NineMill>().enabled=false;
            //TODO UPDATE WHAT WEAPON THE PLAYER HAS
            break;
        }
    }
    public Weapon GetCurrentWeapon(){return currentWeapon;}
    public void DoAction(int amount, value enumValue, bool isAdding){
        switch(enumValue){
            case value.currentPlayerHp:
            if(isAdding){
                currentPlayerHp+=amount;
                if(currentPlayerHp>MAX_PLAYER_HP){
                    currentPlayerHp=MAX_PLAYER_HP;
                }
            }
            else{
                currentPlayerHp-=amount;
                if(currentPlayerHp<0){
                    currentPlayerHp=0;
                }
            }
            print("HP IS after damamge "+ currentPlayerHp);
            switch(currentPlayerHp){
                case 4:
                UIMananger.instance.UpdatePlayerHealth(UIMananger.HealthState.Four);

                break;
                case 3:
                 UIMananger.instance.UpdatePlayerHealth(UIMananger.HealthState.Three);

                break;
                case 2:
                UIMananger.instance.UpdatePlayerHealth(UIMananger.HealthState.Two);

                break;
                case 1:
                UIMananger.instance.UpdatePlayerHealth(UIMananger.HealthState.One);

                break;
                default:
                UIMananger.instance.UpdatePlayerHealth(UIMananger.HealthState.Dead);
                break;


            }
            break;

            case value.Money:
            if(isAdding){
                money+=amount;
            }
            else{
                money-=amount;
                if(money<0){
                    money=0;
                }
            }
            Debug.Log("current money is "+ money);

            UIMananger.instance.UpdateMoneyUI();
            break;

            case value.score:
            if(isAdding){
                score+=amount;
            }
            else{
                score-=amount;
                if(score<0){
                    score=0;
                }
            }
            break;
            default:
            Debug.LogError("Enum value not implemented");
            break;
           


        }
    }
  /// <summary>
  /// load's a level
  /// </summary>
  /// <param name="levelName"></param>
public void LoadLevel(string levelName){
    SceneManager.LoadScene(levelName);
}

public int GetPlayerHealth(){
    return currentPlayerHp;
}

public int GetMoney(){
    return money;
}

}

