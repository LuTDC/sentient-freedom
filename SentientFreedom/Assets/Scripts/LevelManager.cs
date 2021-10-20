using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Image screen;

    public void Fade(){
        screen.GetComponent<Animator>().SetTrigger("Fade");
    }

    public void loadNext(){
        if(SceneManager.GetActiveScene().name != "Level05") SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
