using UnityEngine;
using System.Collections;

public class Enemy : MessageBehaviour {

	public float timeToLive = 10.0f;
	private bool timerActive = false;

	GameObject player;

	public void OnMouseDown() {
		Messenger.SendToListeners(new PossessMessage(gameObject, "Possess", "Requesting to become possessed."));
		timerActive = true;

		GetComponent<Renderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
	}
	
	protected override void OnStart () {
		player = GameObject.FindWithTag("Player");
	}

	void Update () {
		if (timerActive) {
			updateTimer();
		}

//		GetComponent<Pathfinding2D>().FindPath(transform.position, player.transform.position);
	}

	private void updateTimer() {
		timeToLive -= Time.deltaTime;

		print (timeToLive);

		if (timeToLive <= 0.0f) {
			Messenger.SendToListeners(new PossessMessage(gameObject, "Dispossess", "Requesting to become dispossessed"));
			DestroyObject(this.gameObject);
		} else{
			print((int)timeToLive + " seconds");
		}
	}
}
