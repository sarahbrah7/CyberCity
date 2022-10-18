using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement
    private Rigidbody rb;
    public Vector2 moveValue;
    public float speed;
    public float turnSpeed;

    // For quaternion 
    public float maxAngleChange;

    public SaveData savedata;

    void Start() {
        rb = GetComponent<Rigidbody>();
        /*    if (savedata == false)
        {
            transform.position = new Vector3(savedata.getX(), savedata.getY(), savedata.getZ());
        } */
    }
    
    // Input action
    // Can move with multiple control schemes, e.g. keyboard, controller etc
    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
        
    }

    
    void Update()
    {
        // Moving player position with translation and rotation based on input (old)
        //float forwardMovement = moveValue.y * speed * Time.deltaTime;
        //float turnMovement = moveValue.x * turnSpeed * Time.deltaTime;

        //transform.Translate(Vector3.forward * forwardMovement);
        //transform.Rotate(Vector3.up * turnMovement);


        if (Input.GetKeyDown(KeyCode.G) ) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private void FixedUpdate()
    {
       
       Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

       if (movement == Vector3.zero)
       {
            return;
       }

       //Rigidbody movement of player, and rotation, using quaternion
       Quaternion targetRotation = Quaternion.LookRotation(movement);
       targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxAngleChange * Time.fixedDeltaTime);
       rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
       rb.MoveRotation(targetRotation);
    }

}