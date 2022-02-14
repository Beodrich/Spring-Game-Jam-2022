using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeshit : MonoBehaviour
{
    public void LoadLevel(){
        SceneManager.LoadScene("Level1");
    }
    public void Quit(){
        Application.Quit();
    }
}
