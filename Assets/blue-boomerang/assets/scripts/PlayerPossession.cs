using UnityEngine;
using System.Collections;

public class PlayerPossession : MessageBehaviour {

	public GameObject effect;

	private Sprite originalSprite;

	protected override void OnStart () {
		Messenger.RegisterListener(new Listener("Possess", gameObject, "Possess"));
		Messenger.RegisterListener(new Listener("Dispossess", gameObject, "Dispossess"));

		originalSprite = GetComponent<SpriteRenderer>().sprite;
	}

	void Update () {

	}

	void Possess (PossessMessage message) {

		// Move to the possessed's location.
		transform.position = message.possessed.transform.position;
		GetComponent<SpriteRenderer>().sprite = message.possessed.GetComponent<SpriteRenderer>().sprite;;

		// Display the possess effect.
		Instantiate(effect, transform.position, Quaternion.identity);
	}

	public void Dispossess() {

		// Display the possess effect.
		Instantiate(effect, transform.position, Quaternion.identity);

		// Revert to the Player's original sprite.
		GetComponent<SpriteRenderer>().sprite = originalSprite;
	}
}
