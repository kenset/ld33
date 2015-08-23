using UnityEngine;
using System.Collections;

public class Enemy : MessageBehaviour {

	public float timeToLive = 10.0f;
	public float sightDistance = 2.0f;

	private bool timerActive = false;

	private enum Awareness {
		Unaware,
		Alarmed,
		Agressive
	}

	private Awareness awareness;

	public void OnMouseDown() {
		Messenger.SendToListeners(new PossessMessage(gameObject, "Possess", "Requesting to become possessed."));
		timerActive = true;

		GetComponent<Renderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
	}

	void FixedUpdate() {
		Collider2D[] inSight = Physics2D.OverlapCircleAll(transform.position, sightDistance);

		foreach (Collider2D seenObject in inSight) {
			if (seenObject.gameObject.tag.Equals("Player")) {           
				Vector3 direction = (seenObject.transform.position - transform.position).normalized;

//				RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, sightDistance);
//				if (hit.collider.gameObject.tag.Equals("Player")) {                             
//					Debug.DrawRay(transform.position, direction, Color.red);                                         

					if (Vector3.Angle(transform.up, direction) > 90) {
						awareness = Awareness.Agressive;
						GetComponent<SimpleAI2D>().Player = seenObject.transform;
//						print ("seen");
					}
//				}
//				print ("unseen");
			}
		}
	}

	protected override void OnStart () {
		awareness = Awareness.Unaware;
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
			print((int)timeToLive + " seconds");
		}
	}
}
