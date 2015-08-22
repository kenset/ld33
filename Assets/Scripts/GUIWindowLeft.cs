using UnityEngine;
using System.Collections;

public class GUIWindowLeft : MessageBehaviour {

	
	protected override void OnStart () {
		// put code here that you would
		// normally put in Start()
	}
	
	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0,0,Screen.width * 0.5F, Screen.height));
		if(GUILayout.Button("Send Generic Message")) 
		{
			Messenger.SendToListeners(new Message(gameObject, "WindowRightMessage", "I am a generic message with just a set of strings for a name/value pair"));
		}
		
		if(GUILayout.Button("Send New Player Message"))
		{
			int np = Mathf.FloorToInt(Random.Range(1F, 65535F)); // <-- just for demonstration purposes
			Messenger.SendToListeners(new NewPlayerMessage(gameObject, "WindowRightNewPlayerMessage", "I am a specialized message with a PlayerGUID", np));
		}
		GUILayout.EndArea();
	}
	
}
