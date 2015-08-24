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
		}
	}

	protected override void OnStart() {
		base.OnStart();
		
		enemyType = EnemyType.Soldier;
	}
}
