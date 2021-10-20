using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public GameObject player;
    private AudioManager audioManager;
    private bool isFinal = false;

    void Start(){
        StartCoroutine(allowIdle());
        StartCoroutine(quitGame());

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFinal){
            if(!audioManager.isPlaying("Walk")) audioManager.Play("Walk");
            Vector3 move = new Vector3(0, 0, -1);
            player.GetComponent<CharacterController>().Move(move * Time.deltaTime * 2f);
            player.gameObject.transform.forward = move;

        }
        else{
            audioManager.Stop("Walk");
            Vector3 move = new Vector3(0, 0, 0);
            player.GetComponent<CharacterController>().Move(move * Time.deltaTime * 2f);
            player.gameObject.transform.forward = move;
        }
    }

    private IEnumerator allowIdle(){
        yield return new WaitForSeconds(3);

        isFinal = true;
    }

    private IEnumerator quitGame(){
        yield return new WaitForSeconds(11);

        Application.Quit();
    }
}
