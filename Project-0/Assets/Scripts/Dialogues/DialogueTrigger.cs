using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Emote Animator")]
    [SerializeField] private Animator emoteAnimator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake() 
    {
        playerInRange = false;
        // visualCue.SetActive(false);
    }

    private void Update() 
    {
        // TODO : Uncomment to condition when the player is in range of the object, a visual cue will appear

        // if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) 
        // {
            // visualCue.SetActive(true);
            // if (DialogueManager.GetInstance().GetInteractPressed()) 
            if (Input.GetKeyDown(KeyCode.T) && !DialogueManager.GetInstance().dialogueIsPlaying) 
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        // }
        // else 
        // {
            // visualCue.SetActive(false);
        // }
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}