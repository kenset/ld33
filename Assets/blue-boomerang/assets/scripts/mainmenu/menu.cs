using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	protected int selection;
	public int numOptions;

	public GameObject arrow;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	protected virtual void Update () {

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			if (selection > 0) {
				selection--;
			}
		}
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			if (selection < numOptions) {
				selection++;
			}
		}
	}
}
