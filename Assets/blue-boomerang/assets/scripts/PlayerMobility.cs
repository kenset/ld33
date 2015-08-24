using UnityEngine;
using System.Collections;

public class PlayerMobility : MessageBehaviour {

	public float speed;
	public bool monsterDead = false;

	private Transform me;

	//movement by vector translate
	Vector3 inMov;
	Vector3 mPos;
	Vector3 oldMov;
	public bool changeR;

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
		Application.LoadLevel("GameOver");
	}

	protected override void OnStart () {
		me = this.gameObject.transform;
		inMov =  Vector3.zero;
		mPos =  Vector3.zero;
		oldMov = (me.transform.position);
	}

	void Update () {

		if (monsterDead == false) {
//			transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0), Space.World);
//			transform.Translate(new Vector3(0, Input.GetAxis ("Vertical") * speed, 0), Space.World);

			inMov = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), me.transform.position.z);
			MovePlayer (inMov);
		} else {
			GetComponent<SpriteRenderer>().color = Color.red;
		} 
	}	

	void MovePlayer(Vector3 goHere) {
		Vector3 translateMove = Vector3.zero;
		
		oldMov = me.transform.position;
		
		//debugs
		Debug.Log ("old" + oldMov);
		
		if (goHere.magnitude > 0.01f) {
			translateMove = (new Vector3 (goHere.x,goHere.y,0f));
			mPos = (oldMov+translateMove);
			
			Debug.Log ("move" + mPos);

			translateMove = Vector3.MoveTowards(oldMov, mPos, Time.deltaTime * speed);
			
			me.transform.position = translateMove;

			//show translation
			Debug.Log ("translating 2" + translateMove);
			
		} else {
			Debug.Log("dont");
			transform.position = this.transform.position;
			//inMov = Vector3.zero;
		}
	}
}
