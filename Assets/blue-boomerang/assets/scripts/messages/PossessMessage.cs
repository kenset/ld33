using UnityEngine;
using System.Collections;

public class PossessMessage : Message {

	public GameObject possessed;

	public PossessMessage(GameObject s, string n, string v) : base(s,n,v) {
		this.possessed = s;
	}
}
