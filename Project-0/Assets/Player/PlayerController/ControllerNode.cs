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

    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] ProfileData profileData;
    [SerializeField] BoxCollider2D groundCheck;
    
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] public float playerDirection = 1f;
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
        actions.PlayerControl.Walk.performed += WalkHook;
        actions.PlayerControl.Jump.performed += JumpHook;


        actions.PlayerControl.OpenMap.performed += ctx => ShowMap();

        actions.PlayerControl.QuickSave.performed += ctx => profileData.SaveProfile();
        actions.PlayerControl.QuickLoad.performed += ctx => profileData.LoadProfile();
        actions.PlayerControl.Interact.performed += ctx => InteractPerformed();
        actions.PlayerControl.Interact.canceled += ctx => InteractCanceled();
        actions.PlayerControl.Submit.performed += ctx => SubmitPerformed();
        actions.PlayerControl.Submit.canceled += ctx => SubmitCanceled();

    }

    #region  player
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
            playerDirection = 1f;
            break;
            case -1:
            playerSpeed = -movementSpeed;
            playerDirection = -1f;
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

    #endregion

    #region  Menu
    void ShowMap()
    {
        menuPanel.gameObject.SetActive(menuPanel.activeSelf ? false : true);
    }
    #endregion


    #region  Menu
    void InteractPerformed()
    {
        dialogueManager.interactPressed = true ;
    }
    void InteractCanceled()
    {
        dialogueManager.interactPressed = false ;
    }
    void SubmitPerformed()
    {
        dialogueManager.submitPressed = true ;
    }
    void SubmitCanceled()
    {
        dialogueManager.submitPressed = false ;
    }
    #endregion

    #region Interacting Objects




    #endregion
}
