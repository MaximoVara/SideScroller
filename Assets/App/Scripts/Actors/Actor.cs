using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class Actor : MonoBehaviour, IInteractiveHandler {

    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private GameObject ding;

    private CharacterController controller;
    private Animator animator;
    private bool wasGrounded = true;

    public CharacterController Controller {
        get {
            if (this.controller == null)
                this.controller = this.GetComponent<CharacterController>();

            return this.controller;
        }
    }

    public Animator Animator {
        get {
            if (this.animator == null)
                this.animator = this.GetComponent<Animator>();

            return this.animator;
        }
    }

    protected virtual void Update() {
        this.MoveUpdate();

        if (Input.GetButtonDown("Punch")) {
            this.Animator.SetTrigger("Punch");
        }

        if (Input.GetButtonDown("Interact")) {
            this.interaction?.Invoke();
        }

    }

    protected virtual void MoveUpdate() {
        var input = new Vector3 {
            x = Input.GetAxis("Horizontal"),
            y = 0.0f,
            z = Input.GetAxis("Vertical")
        };

        input = Vector3.ClampMagnitude(input, 1.0F);

        // Optional - Only do this . . if you want your character
        // To look in the direction in which you are moving . . .
        if (Mathf.Abs(input.x) > .1f)
            this.transform.forward = Vector3.right * input.x;

        // Set movement animation flag . . .
        // this.Animator.SetBool("Walk", input.sqrMagnitude > .01f);
        this.Animator.SetFloat("Forward", Mathf.Abs(input.x));

        // Threshold . . .
        if (Mathf.Abs(input.x) > .1f) {
            this.Animator.SetBool("Mirror", input.x < 0f);
        }

        // Currently if we remove this, you won't be able to move outside of animations
        // Aka, no 'W' or 'S' movement.
        //this.Controller.Move(input * Time.deltaTime * this.speed);

        //make ur char affected by gravity
        this.Controller.Move(Vector3.down * 9.82f * Time.deltaTime);

        if (this.Controller.isGrounded == true)
            //this.Animator.SetBool("Fall", false);
            //this.Animator.speed

        if(this.wasGrounded != this.Controller.isGrounded) {
            if (this.Controller.isGrounded == false) {
                this.Animator.SetBool("Fall", true);
            }
        }

        this.wasGrounded = this.Controller.isGrounded;

        //Jumping -----------
        if (Input.GetKeyDown(KeyCode.Space)) {
            this.Animator.SetTrigger("Jump");
        }
    }

    protected virtual void FootR() {
        // Play a footstep audio effect . . .

    }

    protected virtual void FootL() {
        // Play a footstep audio effect . . . 

    }

    /*
    IEnumerator jumpAction() {
        this.Animator.SetTrigger("Jump");
        var initPos = this.transform.position;
        var timer = 0.0f;
        var jumpHeight = 1.25f;

        while (timer < 1.0f) {
            var height = Mathf.Sin(Mathf.PI * timer) * jumpHeight;
            this.transform.position = Vector3.Lerp(initPos, )
        }

        yield return null;
    }*/

    System.Action interaction = null;

    public void OnHandleInteraction(System.Action interact) {
        this.interaction = interact;

        this.ding.SetActive(this.interaction != null);
    }
}
