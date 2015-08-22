using UnityEngine;
using System.Collections;

public class GUIWindowRight : MessageBehaviour {
	
	protected string chatLog = "";
	
	protected override void OnStart () {
	
		// register a listener for one of our methods
		Messenger.RegisterListener(new Listener("WindowRightMessage", gameObject, "HandleMessage"));
		Messenger.RegisterListener(new Listener("WindowRightNewPlayerMessage", gameObject, "HandleNewPlayerMessage"));
		
	}
	
	// handler for generic messages
	public void HandleMessage(Message m)
	{
		chatLog += m.MessageValue + "\n";
	}
	
	// handler for NewPlayer messages
	public void HandleNewPlayerMessage(NewPlayerMessage m)
	{
		chatLog += m.MessageValue + ", PlayerGUID=" + m.PlayerGUID.ToString() + "\n";
	}
	
	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(Screen.width * 0.5F,0,Screen.width * 0.5F, Screen.height));
		GUILayout.TextArea(chatLog);
		GUILayout.EndArea();
	}
	
}
