using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float move, speed = 10f;

    private bool isUp = false, isDown = false;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);

        isUp = Input.GetButtonDown("AimUp");
        isDown = Input.GetButtonDown("AimDown");

        if(isUp) move = 1;
        else if(isDown) move = -1;
        else if(!isUp && !isDown) move = 0;

        transform.GetChild(0).GetComponent<Rigidbody>().velocity = new Vector3(0, move*speed, 0);

        Vector3 direction = transform.GetChild(0).transform.position - transform.position;

        lineRenderer.SetPosition(1, transform.GetChild(0).transform.position);
        Debug.DrawRay(transform.position, direction, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit)){
            if(Input.GetButtonDown("Fire")){
                audioManager.Play("Laser");
                if(hit.collider.tag == "Box"){
                    hit.collider.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                    hit.collider.transform.GetChild(0).gameObject.SetActive(false);
                    StartCoroutine(destroyObject(hit.collider.gameObject));
                }
            }
        }
        else lineRenderer.SetPosition(1, transform.GetChild(0).transform.position);
    }

    private IEnumerator destroyObject(GameObject go){
        yield return new WaitForSeconds(0.5f);
        Destroy(go);
    }
}
