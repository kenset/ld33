using UnityEngine;
using System.Collections;

public class AlertMessage : Message {

	public GameObject target;
	
	public AlertMessage(GameObject s, string n, string v) : base(s,n,v) {
		this.target = s;
	}
}
