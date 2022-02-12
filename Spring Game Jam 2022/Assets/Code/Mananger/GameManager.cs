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

    


    // Start is called before the first frame update
    void Start()
    {
     if(instance==null){
         instance=this;
         DontDestroyOnLoad(this);
         SetPlayerHP();
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
    /// <summary>
    /// adds or subtracks the player hp
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="isAdding"></param>
    public void DoHPAction(int amount, bool isAdding){
        if(isAdding){
            currentPlayerHp+=amount;
        }
        else{
            currentPlayerHp-=amount;
            if(currentPlayerHp<0){
                currentPlayerHp=0;
            }
        }
    }
  /// <summary>
  /// load's a level
  /// </summary>
  /// <param name="levelName"></param>
public void LoadLevel(string levelName){
    SceneManager.LoadScene(levelName);
}

public void DoScore(int amount, bool isAdding){
    if(isAdding){
        score+=amount;
    }
    else{
        score-=amount;
        if(score<0){
            score=0;
        }
    }
}
 public void DoMoney(int amount, bool isAdding){
       if(isAdding){
        money+=amount;
    }
    else{
        money-=amount;
        if(money<0){
            money=0;
        }
    }
 }
}

