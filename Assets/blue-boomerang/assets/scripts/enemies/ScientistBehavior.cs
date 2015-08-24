using UnityEngine;
using System.Collections;

public class ScientistBehavior : Enemy {

	private GameObject[] allies;
	private bool searchingForHelp = false;

	private int currentHelper = 1;
	private Transform monster;

	protected override void PerformAlarmedBehavior(Transform t) {
		if (isPossessed != true) {
		}
	}

	protected override void PerformAggressiveBehavior(Transform t) {
		if (isPossessed != true) {
			monster = t;
			allies = GameObject.FindGameObjectsWithTag("Enemy");
			searchingForHelp = true;
		}
	}

	protected override void OnStart() {
		base.OnStart();

		enemyType = EnemyType.Scientist;
	}

	protected override void Update() {
		base.Update();

		// Only do things when you are not possessed.
		if (isPossessed != true) {
			// Stop looking for help if you are not aggressive.
			if (base.awarenessLevel != Enemy.Awareness.Aggressive) {
				searchingForHelp = false;
			}

			// If we are searching for help and our allies includes more than just ourselves...
			if (searchingForHelp && allies.Length > 1) {

				// Sort the allies by distance.
				SortDistances(ref allies, transform.position);

				// For all of the sorted allies...
				for (int i = 0; i < allies.Length; i++) {

					// Make the current ally the closest one that is not already aggressive.
					if (allies[i] != null && allies[i].GetComponent<Enemy>().awarenessLevel != Awareness.Aggressive &&
					    allies[i].GetComponent<Enemy>().dispossessTimerActive == false) {
						currentHelper = i;
						break;
					}

					if (i == allies.Length - 1) {
						currentHelper = allies.Length;
					}
				}

				// If we have a helper to search for...
				if (currentHelper <= (allies.Length - 1)) {

					// Travel to the nearest ally to alert them about the monster.
					GetComponent<SimpleAI2D>().Player = allies[currentHelper].transform;

					// If we are close enough to our ally to alert them to the dangeer...
					if (Vector3.Distance(transform.position, allies[currentHelper].transform.position) <= 1.0) {
						
						// Alert the ally.
						allies[currentHelper].SendMessage("ReceiveAlert", monster);

						// Move on to the next ally.
						currentHelper++;
					}

				} else {

					searchingForHelp = false;

					// Start chasing the player after notifying everyone
					GetComponent<SimpleAI2D>().Player = monster.gameObject.transform;
				}
			} else if (monster != null) {

				// Start chasing the player after notifying everyone
				GetComponent<SimpleAI2D>().Player = monster.gameObject.transform;
			}
		}
	}

	private void SortDistances(ref GameObject[] objects, Vector3 origin) {
		float[] distances = new float[objects.Length];
		for (int i = 0; i < objects.Length; i++) {

			if (objects[i] != null) {
				distances[i] = (objects[i].transform.position - origin).sqrMagnitude;
			}
		}

		System.Array.Sort(distances, objects);
	}
}
