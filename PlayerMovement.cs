using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f; //set movement speed
    public float gravity = -9.81f; //set default gravity value
    public float jumpHeight = 2f; //set default jump height
    public float gameAreaHeight = 5f; // slightly below actual

    public Transform groundCheck; //reference to our GroundCheck Empty Object
    public float groundDistance = 0.45f; //radius of collision check sphere
    public LayerMask groundMask; //define what kind of object we want to recognize as ground collision (e.g. hitting another player is not the ground colliding under)

    public GameObject gameOver; // game Over pop up reference
    //public AudioSource gamerOverSound;



    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void CheckOutOfBounds() // checks if the user is outside the defined game area
    {
        if (transform.position.y < gameAreaHeight)
        {
            Debug.Log("Game Over");
            isGrounded = false; // regular calculation wont work
            //gamerOverSound.Play();
            Cursor.lockState = CursorLockMode.Confined; // release cursor
            gameOver.SetActive(true); // show pop up
            Time.timeScale = 0f; //stop time
        }
        /*else
        {
            gravity = -9.81f;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // create as sphere around the groundCheck Object to check if it collisdes with anything that is defined in the groundMask
        CheckOutOfBounds();

        if (isGrounded && velocity.y < 0) //if player is colliding to ground (not on the air) and his velocity is not of ground-default value (if the player has a falling velocity) then
        {
            velocity.y = -2f; //set velocity to -2, (works better that setting to 0)
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime); // deltaTime to make it framerate independent

        if (Input.GetButtonDown("Jump") && isGrounded) // if space button is pushed while player is grounded then
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded) // sprint mechanic
        {
            speed = 12f;
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 6f;
        }
  

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
