using UnityEngine;
using System.Collections;

public class ScientistBehavior : Enemy {

	protected override void PerformAlarmedBehavior(Transform t) {
		print ("performing scientist alarmed behavior");
	}

	protected override void PerformAggressiveBehavior(Transform t) {
		print ("performing scientist aggressive behavior");

		GetComponent<SimpleAI2D>().Player = t;
	}
}
