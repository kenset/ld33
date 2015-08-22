using UnityEngine;
using System.Collections;

public class PlayerMobility : MonoBehaviour {

	public float speed;
	private Animator anim;
	private SpriteRenderer sr;
	private Sprite originalSprite;

	public GameObject explosion;
	public GameObject countdown;

	public bool hotline = true;

	void FixedUpdate(){ 

		// Always point the player towards the cursor.
		var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

		// To keep the rotation from skewing.
		transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
	}
	
	void Start () {
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		originalSprite = sr.sprite;
	}

	void Update () {

		if (hotline) {
			transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0), Space.World);
			transform.Translate(new Vector3(0, Input.GetAxis ("Vertical") * speed, 0), Space.World);
		} else {
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
				
				Vector3 newPosition = transform.position;
				
				if (Input.GetKeyDown(KeyCode.D)) {
					newPosition += Vector3.right;
					anim.SetTrigger("walk_down");
				}
				if (Input.GetKeyDown(KeyCode.A)) {
					newPosition += Vector3.left;
					anim.SetTrigger("walk_down");
				}
				if (Input.GetKeyDown(KeyCode.W)) {
					newPosition += Vector3.up;
					anim.SetTrigger("walk_down");
				}
				if (Input.GetKeyDown(KeyCode.S)) {
					newPosition += Vector3.down;
					anim.SetTrigger("walk_down");
				}
				
				transform.position = Vector3.MoveTowards(transform.position, newPosition, speed);
			}
		}

		if (Input.GetMouseButtonDown(0)) {
			Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 2);

			if (enemies.Length > 1) {
				transform.position = enemies[1].transform.position;
				sr.sprite = enemies[1].GetComponent<SpriteRenderer>().sprite;
				Instantiate(explosion, transform.position, Quaternion.identity);
				DestroyObject(enemies[1].gameObject);
				Instantiate(countdown, new Vector3(0.5f, 0.1f, 0f), Quaternion.identity);
				print ("in range");
			} else {
				print("out of range");
			}
		}
	}

	public void Unposess() {

		// Uncomment for dragon
		Instantiate(explosion, transform.position, Quaternion.identity);
		sr.sprite = originalSprite;
	}
}
