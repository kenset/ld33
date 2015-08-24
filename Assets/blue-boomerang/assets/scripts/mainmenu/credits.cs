using UnityEngine;
using System.Collections;

public class credits : menu {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
			if (selection == 0) {
				Application.LoadLevel("MainMenu");
			}
		}
		
		if (selection == 0) {
			arrow.transform.position = new Vector3(-4f, -2.5f, 0);
		} 
	}
}
