using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MessageBehaviour {

	// How long the enemy survives after being possessed
	public float timeToLive = 10.0f;

	// Awareness distances
	public float alarmedThreshold = 5.0f;
	public float aggressiveThreshold = 4.0f;
	public float sightAngle = 90.0f;
	
	// Timer variables
	public float alarmedStateLength = 2.0f;
	public float aggressionCooldownLength = 5.0f;

	public bool dispossessTimerActive = false;
	private bool alarmedTimerActive = false;	
	private bool aggressionCooldownTimerActive = false;

	private float alarmedTimerLeft;
	private float aggressionCooldownTimerLeft;

	public List<Transform> waypoints = new List<Transform>();
	private int currentWaypoint;

	protected bool isPossessed = false;

	public enum Awareness {
		Unaware,
		Alarmed,
		Aggressive
	}

	public Awareness awarenessLevel;

	public enum EnemyType {
		Enemy,
		Scientist,
		Soldier
	}

	public EnemyType enemyType = EnemyType.Enemy;

	public bool debug = false;

	protected void OnMouseDown() {

		// We don't want to update if the player is possessed.
		if (dispossessTimerActive == false) {

			// Get the player.
			GameObject player = GameObject.FindGameObjectWithTag("Player");

			// Are we close enough to the player to become possessed?
			if ((player.transform.position - transform.position).magnitude < 
			    player.GetComponent<PlayerPossession>().maxPossessionDistance) {

				// Make sure that there is nothing between the enemy and the player.

				// Make a layer mask to ignore the enemy layer when raycasting.
				
				// Bit shift the index of the layer (8) to get a bit mask
				int layerMask = 1 << 8;
				
				// This would cast rays only against colliders in layer 8.
				// But instead we want to collide against everything except layer 8. 
				// The ~ operator inverts a bitmask.
				layerMask = ~layerMask;

				Vector3 direction = (player.transform.position - transform.position).normalized;

				RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layerMask);

				if (hit.collider.gameObject.tag.Equals("Player")) {

					// Start possession.
					Messenger.SendToListeners(new PossessMessage(gameObject, "Possess", "Requesting to become possessed."));
					dispossessTimerActive = true;
					isPossessed = true;
					
					GetComponent<Renderer>().enabled = false;
					GetComponent<BoxCollider2D>().enabled = false;
				}
			}
		}
	}

	private float CalculateAngle(Vector3 v1, Vector2 v2) {

		Vector3 n = new Vector3(0, 0, -1.0f);
		return Mathf.Atan2(
			Vector3.Dot(n, Vector3.Cross(v1, v2)),
			Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
	}

	protected void FixedUpdate() {

		// We don't want to update if the player is possessed.
		if (dispossessTimerActive == false) {

			// Retrieve a list of objects that are in the enemies alarmed zone.
			Collider2D[] inSight = Physics2D.OverlapCircleAll(transform.position, alarmedThreshold);

			// For every object that the enemy has the potential to see...
			foreach (Collider2D seenObject in inSight) {

				// If the player is within the enemy's alarmed zone...
				if (seenObject.gameObject.tag.Equals("Player")) {
					
					// Calculate the direction between the enemy and the player.
					Vector3 direction = (seenObject.transform.position - transform.position).normalized;
					float angle = CalculateAngle(transform.up, direction);
					// If the angle between the enemy and the player is within the
					// enemy's line of sight...

					SimpleAI2D.Facing facing = GetComponent<SimpleAI2D>().facing;

					// Yikes...
					if (((facing == SimpleAI2D.Facing.down) && (angle <= (-90 - sightAngle)) || (angle >= (90 + sightAngle))) ||
					    ((facing == SimpleAI2D.Facing.up) && (angle >= (-90 + sightAngle)) && (angle <= (90 - sightAngle))) ||
					    ((facing == SimpleAI2D.Facing.left) && (angle <= (0 - sightAngle)) && (angle >= (-180 + sightAngle))) ||
					    ((facing == SimpleAI2D.Facing.right) && (angle >= (0 + sightAngle)) && (angle <= (180 - sightAngle)))) {
				
						// Make a layer mask to ignore the enemy layer when raycasting.

						// Bit shift the index of the layer (8) to get a bit mask
						int layerMask = 1 << 8;
						
						// This would cast rays only against colliders in layer 8.
						// But instead we want to collide against everything except layer 8. 
						// The ~ operator inverts a bitmask.
						layerMask = ~layerMask;

						// Check to see if anything is between the enemy and the player.
						RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layerMask);

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
							alarmedTimerActive = false;
							alarmedTimerLeft = alarmedStateLength;
						}

						// Otherwise something interrupted the enemy's line of sight, but
						// they are still aggressive. We should start the aggression cooldown.
						// The enemy should be aggressive but not be in an aggression cooldown.
						else if (awarenessLevel == Awareness.Aggressive && aggressionCooldownTimerActive == false) {
							aggressionCooldownTimerActive = true;
							aggressionCooldownTimerLeft = aggressionCooldownLength;
						}
					}
				}
			}
		}
	}

	protected virtual void PerformAlarmedBehavior(Transform t) {}

	protected virtual void PerformAggressiveBehavior(Transform t) {}

	protected override void OnStart () {
		awarenessLevel = Awareness.Unaware;

		alarmedTimerLeft = alarmedStateLength;
		aggressionCooldownTimerLeft = aggressionCooldownLength;

		if (alarmedThreshold < aggressiveThreshold) {
			print ("'alarmedThreshold' should be greater than 'aggressiveThreshold'");
		}

		if (waypoints.Count > 0) {
			currentWaypoint = 0;
		}
	}

	void OnMouseOver() {

	}

	protected virtual void Update () {

		// We don't want to update if the player is possessing us.

		// Call different timers if applicable.
		if (dispossessTimerActive) {
			DispossessTimer();
		} else {
			if (alarmedTimerActive) {
				AlarmedTimer();
			}
			if (aggressionCooldownTimerActive) {
				AggressionCooldownTimer();
			}

			if (awarenessLevel != Awareness.Aggressive) {
				Patrol();
			}
		}

		// Increase the enemy speed if they are aggressive.
		if (awarenessLevel == Awareness.Aggressive) {
			GetComponent<SimpleAI2D>().Speed = 3.0f;
		} else {
			GetComponent<SimpleAI2D>().Speed = 2.0f;
		}
	}

	public void ReceiveAlert(Transform target) {

		if (dispossessTimerActive == false) {
			awarenessLevel = Awareness.Aggressive;
			PerformAggressiveBehavior(target);

			GetComponent<SpriteRenderer>().color = Color.red;
		}
	}

	private void Patrol() {

		if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 0.01) {
			if ((currentWaypoint + 1) == waypoints.Count) {
				currentWaypoint = 0;
			} else {
				currentWaypoint++;
			}
		}

		GetComponent<SimpleAI2D>().Player = waypoints[currentWaypoint];
	}

	private void AggressionCooldownTimer() {
		aggressionCooldownTimerLeft -= Time.deltaTime;

		if (aggressionCooldownTimerLeft <= 0.0f) {
			awarenessLevel = Awareness.Unaware;

			aggressionCooldownTimerActive = false;
		}
	}

	private void AlarmedTimer() {
		alarmedTimerLeft -= Time.deltaTime;

		float timeUp = 0.0f;

		// If the player is posessing someone, increase the time left on the timer.
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPossession>().possessed != null) {
			timeUp = -5.0f;
		}

		if (alarmedTimerLeft <= timeUp) {
			awarenessLevel = Awareness.Aggressive;

			alarmedTimerActive = false;
		}
	}

	private void DispossessTimer() {
		timeToLive -= Time.deltaTime;

//		print (timeToLive);

		if (timeToLive <= 0.0f) {
			dispossessTimerActive = false;
			Messenger.SendToListeners(new PossessMessage(gameObject, "Dispossess", "Requesting to become dispossessed"));
			DestroyObject(this.gameObject);
		} else {

			if (debug) {
//				print((int)timeToLive + " seconds");
			}
		}
	}
}
