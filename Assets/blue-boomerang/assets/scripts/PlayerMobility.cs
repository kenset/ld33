using UnityEngine;
using System.Collections;

public class PlayerMobility : MessageBehaviour {

	public float speed;
	private Animator anim;

	public bool monsterDead = false;

	void FixedUpdate(){ 

		// Always point the player towards the cursor.
//		var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		transform.rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

		// To keep the rotation from skewing.
//		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
	}

	public void Die() {
	
		// If you are possessing someone, they die before you do.
		if (GetComponent<PlayerPossession>().possessed != null) {
			GetComponent<PlayerPossession>().EjectPossessedBody(false);
		} else {
			monsterDead = true;
		}

		Invoke("GoToMainMenu", 3.0f);
	}

	void GoToMainMenu() {
		Application.LoadLevel("MainMenu");
	}

	protected override void OnStart () {
		anim = GetComponent<Animator>();
	}

	void Update () {

		if (monsterDead == false) {
			transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0), Space.World);
			transform.Translate(new Vector3(0, Input.GetAxis ("Vertical") * speed, 0), Space.World);
		} else {
			GetComponent<SpriteRenderer>().color = Color.red;
		} 
	}	
}
