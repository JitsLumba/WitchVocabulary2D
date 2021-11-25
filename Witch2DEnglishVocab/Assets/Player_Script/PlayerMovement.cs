using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public float runSpeed = 40f;

	float horzMove = 0f;
	bool jump = false;
    public Animator animator;

	void Update() {
		
		horzMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horzMove));

		if(Input.GetButtonDown("Jump")) {
			jump = true;
		}
		
	}

	void FixedUpdate() {
		
		controller.Move(horzMove * Time.fixedDeltaTime, false, jump);
		jump = false;
	}
}