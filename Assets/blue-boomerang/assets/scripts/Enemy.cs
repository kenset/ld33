using UnityEngine;
using System.Collections;

public class Enemy : MessageBehaviour {

	public float timeToLive = 10.0f;
	private bool timerActive = false;

	public void OnMouseDown() {
		Messenger.SendToListeners(new PossessMessage(gameObject, "Possess", "Requesting to become possessed."));
		timerActive = true;

		GetComponent<Renderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
	}
	
	protected override void OnStart () {

	}

	void Update () {
		if (timerActive) {
			updateTimer();
		}
	}

	private void updateTimer() {
		timeToLive -= Time.deltaTime;

		print (timeToLive);

		if (timeToLive <= 0.0f) {
			Messenger.SendToListeners(new PossessMessage(gameObject, "Dispossess", "Requesting to become dispossessed"));
			DestroyObject(this.gameObject);
		} else{
			GetComponent<GUIText>().text = (int)timeToLive + " seconds";
		}
	}
}
