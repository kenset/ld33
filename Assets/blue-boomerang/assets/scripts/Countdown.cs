using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

	public float timeLeft = 50.0f;
	private PlayerMobility player;

	void Start() {
		player = (PlayerMobility)FindObjectOfType(typeof(PlayerMobility)); 
	}

	public void Update()
	{
		timeLeft -= Time.deltaTime;
		
		if (timeLeft <= 0.0f)
		{
			// End the level here.
			GetComponent<GUIText>().text = "You ran out of time";
			player.Unposess();
			DestroyObject(this.gameObject);
		}
		else
		{
			GetComponent<GUIText>().text = (int)timeLeft + " seconds";
		}
		
	}
}
