using UnityEngine;
using System.Collections;

public class ElevatorBehavior : MonoBehaviour {

	public bool open = false;
	public Sprite openSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (open && openSprite != null) {
			GetComponent<SpriteRenderer>().sprite = openSprite;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals("Player")) {
			if (other.gameObject.GetComponent<PlayerPossession>().possessed.GetComponent<Enemy>().enemyType == Enemy.EnemyType.Scientist) {
				open = true;
			}
		}
	}
}
