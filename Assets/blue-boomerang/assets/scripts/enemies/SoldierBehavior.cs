using UnityEngine;
using System.Collections;

public class SoldierBehavior : Enemy {

	protected override void PerformAlarmedBehavior(Transform t) {
//		print ("performing soldier alarmed behavior");
	}
	
	protected override void PerformAggressiveBehavior(Transform t) {
		print ("performing soldier aggressive behavior");

		// Set the seen Player as the object of pursuit.
		GetComponent<SimpleAI2D>().Player = t;
	}
}
