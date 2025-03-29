using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 350; //set mouse sensitivity

    public Transform playerBody; // reference to our player object

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //hide and lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //get mouse x moevement *  by set mouse sensitivity * by the time that has passed since the last update function was called
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //same thing for y axis mouse movement
                                                                                     /* Note on Time.deltaTime: This is to ensure that in lower fps scenarios, the screen doesnt turn slower that higher fps
                                                                                      * meaning the oeverall fps values do not affect our screen movement speed at all*/

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
