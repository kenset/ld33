using UnityEngine;
using System.Collections;

public class mainmenu : menu {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
			if (selection == 0) {
				Application.LoadLevel("HowToPlay");
			} else if (selection == 1) {
				Application.LoadLevel("Credits");
			} else if (selection == 2) {
				Application.Quit();
			}
		}

		if (selection == 0) {
			arrow.transform.position = new Vector3(-2.3f, -0.7f, 0);
		} else if (selection == 1) {
			arrow.transform.position = new Vector3(-2.3f, -1.7f, 0);
		} else if (selection == 2) {
			arrow.transform.position = new Vector3(-2.3f, -2.8f, 0);
		}
	}
}
