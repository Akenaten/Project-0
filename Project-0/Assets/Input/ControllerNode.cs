using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerNode : MonoBehaviour
{


    PlayerControls actions;

    [SerializeField] GameObject menuPanel;

    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] ProfileData profileData;
    public StateMachine meleeStateMachine;
    public float playerMovementInput;
    public bool desiredJump;

    void Awake()
    {
        actions = new PlayerControls();
    }

    void Start()
    {
        // meleeStateMachine = GetComponent<StateMachine>();
    }


    void OnEnable()
    {
        actions.Enable();
        actions.PlayerControl.Walk.performed += WalkHook;
        actions.PlayerControl.Jump.performed += ctx => JumpPressed();
        actions.PlayerControl.Jump.canceled += ctx => JumpCanceled();


        actions.PlayerControl.OpenMap.performed += ctx => ShowMap();

        actions.PlayerControl.QuickSave.performed += ctx => profileData.SaveProfile();
        actions.PlayerControl.QuickLoad.performed += ctx => profileData.LoadProfile();
        actions.PlayerControl.Interact.performed += ctx => InteractPerformed();
        actions.PlayerControl.Interact.canceled += ctx => InteractCanceled();
        actions.PlayerControl.Submit.performed += ctx => SubmitPerformed();
        actions.PlayerControl.Submit.canceled += ctx => SubmitCanceled();
        actions.PlayerControl.Attack.performed += ctx => AttackPerformed();
    }

    #region  player
    public void JumpPressed()
    {
        desiredJump = true;
    }

    public void JumpCanceled()
    {
        desiredJump = false;
    }

    public void WalkHook(InputAction.CallbackContext context) {
        playerMovementInput = context.ReadValue<float>();
    }

    // Update is called once per frame
    void Update()
    {
    }

        #region Attack

    void AttackPerformed()
    {
        if (meleeStateMachine.currentState.GetType() == typeof(IdleCombatState))
        {
            meleeStateMachine.SetNextState(new GroundEntryState());
        }
        if (meleeStateMachine.currentState.GetType().IsSubclassOf(typeof(MeleeBaseState)))
        {
            meleeStateMachine.currentState.AttackPressedTimer = 2;
        }
    }
        #endregion

    #endregion

    #region  Menu
    void ShowMap()
    {
        menuPanel.gameObject.SetActive(menuPanel.activeSelf ? false : true);
    }
    #endregion


    #region  Dialog
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
}
