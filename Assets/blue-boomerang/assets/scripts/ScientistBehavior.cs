using UnityEngine;
using System.Collections;

public class ScientistBehavior : Enemy {

	protected override void PerformAlarmedBehavior(Transform t) {
		print ("performing scientist alarmed behavior");
	}

	protected override void PerformAggressiveBehavior(Transform t) {
		print ("performing scientist aggressive behavior");
	}

	// Use this for initialization
	void OnStart () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
