using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class KineAnimTriggers : MessageBehaviour {

	string olddir;
	string dir;
	public Animator anim;
	private Vector3 lastPosition = Vector3.zero;
	//public bool spider;
	// Use this for initialization
	protected override void OnStart () {

	anim = this.gameObject.GetComponent<Animator>();
		olddir = "";
		dir = "";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 velocity = (this.transform.position - lastPosition) / Time.deltaTime;
		lastPosition = this.transform.position;
		PickDirection (velocity);
	}

	void PickDirection (Vector3 vel) {

//		// Calculate direction
//		Vector3 direction = GetComponent<SimpleAI2D>().direction;
//		if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x) && direction.y < 0) {
//			dir = "down";
//		} else if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x) && direction.y > 0) {
//			dir = "up";
//		} else if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && direction.x < 0) {
//			dir = "left";
//		} else if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && direction.x > 0) {
//			dir = "right";
//		}

		if (Mathf.Abs (vel.x) >= 0.5f || Mathf.Abs (vel.y) >= 0.5f) {
			//anim.ResetTrigger("idle");

			if (Mathf.Abs (vel.x) > Mathf.Abs (vel.y)) {
				if (vel.x >= 0.5f) {
					dir = "right";
				} else if (vel.x < 0.5f) {
					dir = "left";
				}

			} else {
				if (vel.y >= 0.5f) {
					dir = "up";
				} else if (vel.y <= -1f*0.5f){
					dir = "down";
				}
				else { 
					dir = "down";
				}
			}

		
	} else{

			anim.SetTrigger("idle"); //sets idle bool
		}


			anim.SetTrigger (dir);
		

		olddir = dir;

		if (gameObject.GetComponentInParent<PlayerMobility>()) {
			Debug.Log ("player moves" + dir);
			Debug.Log("player speed x" + vel.x);
			Debug.Log ("player speed y" + vel.y);
		}

	}

	void SetNewStates (string dirSend){

	


	}

	public void NPCattack(){

		// set bool or trigger for attack + dir?
		anim.SetTrigger ("attack");
		anim.SetBool("idle",false);
	}

	public void Possess(){
		if (this.gameObject.GetComponentInParent<PlayerMobility> ()) {
			anim.SetTrigger("possession");
			anim.SetBool("idle", false);
		}
	}

}
