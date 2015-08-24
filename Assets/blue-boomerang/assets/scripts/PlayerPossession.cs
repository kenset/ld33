using UnityEngine;
using System.Collections;

public class PlayerPossession : MessageBehaviour {

	public GameObject effect;
	public GameObject deadScientist;
	public GameObject deadSoldier;

	// The farthest an enemy can be before you cannot possess it.
	public float maxPossessionDistance = 3.0f;
	public GameObject possessed;

	private Sprite originalSprite;

	protected override void OnStart () {
		Messenger.RegisterListener(new Listener("Possess", gameObject, "Possess"));
		Messenger.RegisterListener(new Listener("Dispossess", gameObject, "Dispossess"));

		originalSprite = GetComponent<SpriteRenderer>().sprite;
	}

	void Update () {
		if (Input.GetKeyDown("space")) {
			EjectPossessedBody(true);
		}
	}

	public void EjectPossessedBody(bool areaEffect) {
		GameObject toDispossess = possessed;
		possessed.GetComponent<Enemy>().dispossessTimerActive = false;
		Dispossess(new PossessMessage(possessed, "Dispossess", "Force dispossess currently possessed"));
		DestroyObject(toDispossess);

		if (areaEffect) {
			foreach (Collider2D o in Physics2D.OverlapCircleAll(transform.position, 1.5f)) {
				if (o.gameObject.tag.Equals("Enemy")) {

					if (o.GetComponent<Enemy>().enemyType == Enemy.EnemyType.Scientist) {
						Instantiate(deadScientist, o.transform.position, Quaternion.identity);
					}
					if (o.GetComponent<Enemy>().enemyType == Enemy.EnemyType.Soldier) {
						Instantiate(deadSoldier, o.transform.position, Quaternion.identity);
					}

					DestroyObject(o.gameObject);
				}
			}
		}
	}

	void Possess (PossessMessage message) {

		// Eject the currently possessed body if we want to possess someone else.
		if (possessed != null) {
			EjectPossessedBody(false);
		}

		// Move to the possessed's location.
		transform.position = message.possessed.transform.position;
		GetComponent<SpriteRenderer>().sprite = message.possessed.GetComponent<SpriteRenderer>().sprite;
		possessed = message.possessed;

		// Display the possess effect.
		Instantiate(effect, transform.position, Quaternion.identity);
	}

	public void Dispossess(PossessMessage message) {

		// Display the possess effect.
		Instantiate(effect, transform.position, Quaternion.identity);

		// Revert to the Player's original sprite.
		GetComponent<SpriteRenderer>().sprite = originalSprite;

		if (message.possessed.GetComponent<Enemy>().enemyType == Enemy.EnemyType.Scientist) {
			Instantiate(deadScientist, transform.position, Quaternion.identity);
		}
		if (message.possessed.GetComponent<Enemy>().enemyType == Enemy.EnemyType.Soldier) {
			Instantiate(deadSoldier, transform.position, Quaternion.identity);
		}

		possessed = null;
	}
}
