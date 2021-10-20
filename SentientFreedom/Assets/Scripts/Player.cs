using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants{
    public const float minimumSpeed = 3f;
    public const float maximumSpeed = 6f;
}

public class Player : MonoBehaviour
{
    public Vector3 checkpoint;

    public GameObject tank, robot;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed;
    private float jumpHeight = 5f;
    private float gravityValue = 14f;
    float verticalVelocity = 0f;

    public Transform groundCheck;
    private float groundCheckRadius = 0.5f;
    public LayerMask whatIsGround;

    private bool isTank = false;

    public ParticleSystem particles;

    private AudioManager audioManager;
    private LevelManager levelManager;

    public GameObject laser;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        audioManager = FindObjectOfType<AudioManager>();
        levelManager = FindObjectOfType<LevelManager>();

        playerSpeed = Constants.minimumSpeed;

        laser.SetActive(false);

        checkpoint = transform.position;

        tank.SetActive(false);
    }

    void FixedUpdate()
    {
        Walk();

        checkGround();

        Jump();
            
        if(!isTank) Run();

        Transform();
    }

    private void Walk(){
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;

            if(!isTank && groundedPlayer){
                if(playerSpeed == Constants.minimumSpeed && !audioManager.isPlaying("Walk")) audioManager.Play("Walk");
                else if(!audioManager.isPlaying("Run")) audioManager.Play("Run");
            }
            else if(!audioManager.isPlaying("Tank") && groundedPlayer) audioManager.Play("Tank");
        }
        else{
            if(audioManager.isPlaying("Walk")) audioManager.Stop("Walk");
            if(audioManager.isPlaying("Run")) audioManager.Stop("Run");
            if(audioManager.isPlaying("Tank")) audioManager.Stop("Tank");
        }

        transform.GetChild(0).gameObject.GetComponent<Animator>().SetFloat("Speed", move.magnitude * playerSpeed);
    }

    private void checkGround(){
        Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);
        
        if(hitColliders.Length > 0) groundedPlayer = true;
        else groundedPlayer = false;
    }

    private void Jump(){
        if(groundedPlayer){
            verticalVelocity = -gravityValue * Time.deltaTime;
            if (Input.GetButtonDown("Jump") && !isTank) verticalVelocity = jumpHeight;
        }
        else verticalVelocity -= gravityValue * Time.deltaTime;

        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveVector * Time.deltaTime);
    }

    private void Run(){
        if(Input.GetButtonDown("Run")) playerSpeed = Constants.maximumSpeed;
        else if(Input.GetButtonUp("Run")) playerSpeed = Constants.minimumSpeed;
    }

    private void Transform(){
        if(Input.GetButtonDown("Transform") && groundedPlayer){
            isTank = !isTank;
            playerSpeed = Constants.minimumSpeed;
            particles.Play();

            if(isTank){
                laser.SetActive(true);
                tank.SetActive(true);
                robot.SetActive(false);
            }
            else{
                laser.SetActive(false);
                tank.SetActive(false);
                robot.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Final") levelManager.Fade();
        else if(other.tag == "GameOver"){
            controller.enabled = false;
            transform.position = checkpoint;
            controller.enabled = true;
        }
    }
}
