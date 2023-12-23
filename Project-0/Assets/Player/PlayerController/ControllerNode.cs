using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ControllerNode : MonoBehaviour
{


    PlayerControls actions;

    Rigidbody2D rb;

    [SerializeField] GameObject menuPanel;
    [SerializeField] BoxCollider2D groundCheck;
    
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float jumpForce = 4f;
    [SerializeField] float playerSpeed = 0;
    [SerializeField] bool canMove = true;
    [SerializeField] bool canJump = true;

    void Awake()
    {

        actions = new PlayerControls();
        rb = transform.GetComponent<Rigidbody2D>();

    }


    void OnEnable()
    {
        actions.Enable();
        actions.Movement.Walk.performed += WalkHook;
        actions.Movement.Jump.performed += JumpHook;
        actions.Movement.OpenMap.performed += ctx => ShowMap();
    }


    public void JumpHook(InputAction.CallbackContext context)
    {

        if(canJump){
            Jump();
        }
    }

    public void WalkHook(InputAction.CallbackContext context){
        var speedInput = context.ReadValue<float>();
        switch(speedInput){
            case 1:
            playerSpeed = movementSpeed;
            break;
            case -1:
            playerSpeed = -movementSpeed;
            break;
            default:
            playerSpeed = 0;
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            
            rb.velocity = new Vector3(playerSpeed * Time.deltaTime, rb.velocity.y, 0);
        }
    }

    void Jump(){
        rb.velocity = new Vector3(rb.velocity.x , jumpForce , 0);
    }




    public void outsideOrderGround(bool state){ //IS TRIGGERED BY THE GROUNC CHECKING OBJECT TO DETERMINE IF THE PLAYER CAN JUMP
        canJump = state;
    }

    #region  Menu

    void ShowMap()
    {
        menuPanel.gameObject.SetActive(menuPanel.activeSelf ? false : true);
    }
    #endregion
}
