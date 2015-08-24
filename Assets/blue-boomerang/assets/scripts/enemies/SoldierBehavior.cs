using UnityEngine;
using System.Collections;

public class SoldierBehavior : Enemy {

	protected override void PerformAlarmedBehavior(Transform t) {
		if (isPossessed != true) {}
	}
	
	protected override void PerformAggressiveBehavior(Transform t) {
		if (isPossessed != true) {
			// Set the seen Player as the object of pursuit.
			GetComponent<SimpleAI2D>().Player = t;

			Vector3 direction = (t.position - transform.position).normalized;

			// Make a layer mask to ignore the enemy layer when raycasting.
			
			// Bit shift the index of the layer (8) to get a bit mask
			int layerMask = 1 << 8;
			
			// This would cast rays only against colliders in layer 8.
			// But instead we want to collide against everything except layer 8. 
			// The ~ operator inverts a bitmask.
			layerMask = ~layerMask;
			
			// Check to see if anything is between the enemy and the player.
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layerMask);

			if (hit.collider.gameObject.tag.Equals("Player")) {
//				DestroyObject(hit.collider.gameObject);
				hit.collider.gameObject.GetComponent<PlayerMobility>().monsterDead = true;
			}
		}
	}

	protected override void OnStart() {
		base.OnStart();
		
		enemyType = EnemyType.Soldier;
	}
}
