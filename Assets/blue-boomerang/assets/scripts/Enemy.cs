using UnityEngine;
using System.Collections;

public class Enemy : MessageBehaviour {

	public float timeToLive = 10.0f;
	public float sightDistance = 2.0f;
	public float sightAngle = 90.0f;

	public bool debug = false;

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

		// Retrieve a list of objects that are in the enemies aggression zone.
		Collider2D[] inSight = Physics2D.OverlapCircleAll(transform.position, sightDistance);

		// For every object that the enemy has the potential to see...
		foreach (Collider2D seenObject in inSight) {

			// If the player is within the enemy's aggression zone...
			if (seenObject.gameObject.tag.Equals("Player")) { 

				// Calculate the direction between the enemy and the player.
				Vector3 direction = (seenObject.transform.position - transform.position).normalized;

				// If the angle between the enemy and the player is within the
				// enemy's line of sight...
				if (Vector3.Angle(transform.up, direction) > sightAngle) {

					// Check to see if anything is between the enemy and the player.
					RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

					if (debug) {
						Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
						print (hit.collider.gameObject);
					}

					// If there is nothing between the enemy and the player...
					if (hit.collider.gameObject.tag.Equals("Player")) {       

						// Make the enemy aggressive.
						awareness = Awareness.Agressive;

						// Set the seen Player as the object of pursuit.
						GetComponent<SimpleAI2D>().Player = seenObject.transform;
					}
				}
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
