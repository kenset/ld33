using UnityEngine;
using System.Collections;

public class Enemy : MessageBehaviour {

	// How long the enemy survives after being possessed
	public float timeToLive = 10.0f;

	// Awareness distances
	public float alarmedThreshold = 5.0f;
	public float aggressiveThreshold = 4.0f;
	public float sightAngle = 90.0f;

	// How long the enemy remains in an alarmed state.
	public float alarmedStateLength = 2.0f;

	// Timer variables
	private bool dispossessTimerActive = false;
	private bool alarmedTimerActive = false;	
	private float alarmedTimerLeft;

	private enum Awareness {
		Unaware,
		Alarmed,
		Aggressive
	}

	private Awareness awarenessLevel;

	public bool debug = false;

	protected void OnMouseDown() {
		Messenger.SendToListeners(new PossessMessage(gameObject, "Possess", "Requesting to become possessed."));
		dispossessTimerActive = true;

		GetComponent<Renderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
	}

	protected void FixedUpdate() {

		// Retrieve a list of objects that are in the enemies alarmed zone.
		Collider2D[] inSight = Physics2D.OverlapCircleAll(transform.position, alarmedThreshold);

		// For every object that the enemy has the potential to see...
		foreach (Collider2D seenObject in inSight) {

			// If the player is within the enemy's alarmed zone...
			if (seenObject.gameObject.tag.Equals("Player")) {
				
				// Calculate the direction between the enemy and the player.
				Vector3 direction = (seenObject.transform.position - transform.position).normalized;
				
				// If the angle between the enemy and the player is within the
				// enemy's line of sight...
				if (Vector3.Angle(transform.up, direction) > sightAngle) {
					
					// Check to see if anything is between the enemy and the player.
					RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
					
					if (debug) {

						// In alarmed zone, but not yet seen.
						Color rayColor = Color.blue;
						
						if (awarenessLevel == Awareness.Alarmed) {
							rayColor = Color.yellow;
						} else if (awarenessLevel == Awareness.Aggressive) {
							rayColor = Color.red;
						}
						
						Debug.DrawRay(transform.position, direction * hit.distance, rayColor);
					}
					
					// If there is nothing between the enemy and the player...
					if (hit.collider.gameObject.tag.Equals("Player")) {
						if (awarenessLevel == Awareness.Unaware) {
							
							// Make the enemy alarmed.
							awarenessLevel = Awareness.Alarmed;
							alarmedTimerActive = true;

							PerformAlarmedBehavior(seenObject.transform);
						} else if (awarenessLevel == Awareness.Aggressive) {
							PerformAggressiveBehavior(seenObject.transform);
						}
					} 

					// Otherwise something interrupted the enemy's line of sight, but
					// the enemy was merely alarmed.
					else if (awarenessLevel == Awareness.Alarmed) {

						// Reset the enemy's level of awareness.
						awarenessLevel = Awareness.Unaware;
						alarmedTimerLeft = alarmedStateLength;
					}
				}
			}
		}
	}

	protected virtual void PerformAlarmedBehavior(Transform t) {

	}

	protected virtual void PerformAggressiveBehavior(Transform t) {

		// Set the seen Player as the object of pursuit.
		GetComponent<SimpleAI2D>().Player = t;
	}

	protected override void OnStart () {
		awarenessLevel = Awareness.Unaware;

		alarmedTimerLeft = alarmedStateLength;

		if (alarmedThreshold < aggressiveThreshold) {
			print ("'alarmedThreshold' should be greater than 'aggressiveThreshold'");
		}
	}

	protected void Update () {
		if (dispossessTimerActive) {
			DispossessTimer();
		}
		if (alarmedTimerActive) {
			AlarmedTimer();
		}
	}

	private void AlarmedTimer() {
		alarmedTimerLeft -= Time.deltaTime;

		if (alarmedTimerLeft <= 0.0f) {
			awarenessLevel = Awareness.Aggressive;

			alarmedTimerActive = false;
		}
	}

	private void DispossessTimer() {
		timeToLive -= Time.deltaTime;

		print (timeToLive);

		if (timeToLive <= 0.0f) {
			dispossessTimerActive = false;
			Messenger.SendToListeners(new PossessMessage(gameObject, "Dispossess", "Requesting to become dispossessed"));
			DestroyObject(this.gameObject);
		} else {

			if (debug) {
				print((int)timeToLive + " seconds");
			}
		}
	}
}
