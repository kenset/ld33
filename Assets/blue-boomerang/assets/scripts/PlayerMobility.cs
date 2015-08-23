using UnityEngine;
using System.Collections;

public class PlayerMobility : MessageBehaviour {

	public float speed;
	private Animator anim;

	public bool hotline = true;

	void FixedUpdate(){ 

		// Always point the player towards the cursor.
		var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		transform.rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

		// To keep the rotation from skewing.
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
	}
	
	protected override void OnStart () {
		anim = GetComponent<Animator>();
	}

	void Update () {

		if (hotline) {
			transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0), Space.World);
			transform.Translate(new Vector3(0, Input.GetAxis ("Vertical") * speed, 0), Space.World);
		} else {
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
				
				Vector3 newPosition = transform.position;
				
				if (Input.GetKey(KeyCode.D)) {
					newPosition += Vector3.right;
					anim.SetTrigger("walk_down");
				}
				if (Input.GetKey(KeyCode.A)) {
					newPosition += Vector3.left;
					anim.SetTrigger("walk_down");
				}
				if (Input.GetKey(KeyCode.W)) {
					newPosition += Vector3.up;
					anim.SetTrigger("walk_down");
				}
				if (Input.GetKey(KeyCode.S)) {
					newPosition += Vector3.down;
					anim.SetTrigger("walk_down");
				}
				
				transform.position = Vector3.MoveTowards(transform.position, newPosition, speed);
			}
		}
	}	
}
