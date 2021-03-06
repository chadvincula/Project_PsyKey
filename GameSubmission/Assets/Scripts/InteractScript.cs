using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractScript : MonoBehaviour
{
    private PlayerControls _playerControls;
    protected bool _canInteract = false, _isInteracting = false;
    protected Player _player;
    protected GameObject currentTextbox = null;

    public GameObject textBox;
    public GameObject interactableIcon;

    protected virtual void Awake()
    {
        _player = FindObjectOfType<Player>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.Interact.performed += HandleInteract;
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Player.Interact.performed -= HandleInteract;
    }

    //Note: Unfinished, you need to set isInteracting to true so that you don't trigger it twice.
    protected virtual void HandleInteract(InputAction.CallbackContext context)
    {
        if (!_canInteract) return;
        //textBox.SetActive(true);
        currentTextbox = textBox;
        _isInteracting = true;
        Debug.Log("You've reached a checkpoint!");
        PerformInteraction();
    }

    protected virtual void PerformInteraction()
    {
        Debug.Log("Performing Interaction from " + gameObject.name);
        currentTextbox.SetActive(true);
    }

    //Triggers when in range to interact with something.
    protected virtual void OnTriggerEnter(Collider other)
    {
        _canInteract = true;
        // if(other.gameObject.GetComponent<Player>() != null)
        //     _player = other.gameObject.GetComponent<Player>();
    }

    //Makes sure the player doesn't interact with something out of range.
    protected virtual void OnTriggerExit(Collider other)
    {
        _canInteract = false;
        _isInteracting = false;
        if(textBox.activeInHierarchy)
            textBox.SetActive(false);
    }
}
