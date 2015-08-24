using UnityEngine;
using System.Collections;

public class gameover : menu {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
			if (selection == 0) {
				Application.LoadLevel("testing");
			} else if (selection == 1) {
				Application.LoadLevel("MainMenu");
			}
		}
		
		if (selection == 0) {
			arrow.transform.position = new Vector3(-2.8f, -1.13f, 0);
		} else if (selection == 1) {
			arrow.transform.position = new Vector3(-2.8f, -2.4f, 0);
		}
	}
}
