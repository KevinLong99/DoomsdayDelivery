using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallController : MonoBehaviour {
	public float MaxForce = 500;
	public Text countText;
	public Text winText;
	public int pickupCount = 12;
	private int count;
	private Vector3 lastPosition;

	void Start() {
		count = 0;
		SetCountText();
		winText.text = "";
	}

	private void SetCountText() {
		countText.text = string.Format ("Count: {0}", count);
		if (count >= pickupCount)
			winText.text = "WINNER!!!";
	}


	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

		Vector3 position = gameObject.transform.position;

		float speed = Mathf.Clamp((lastPosition - position).magnitude * 2.0f, 0, 1);
		float force = (MaxForce * -(speed - 1));
		GetComponent<Rigidbody>().AddForce(movement * force * Time.deltaTime);

		lastPosition = position;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "PickUp") {
			other.gameObject.SetActive(false);
			count++;
			SetCountText();
		}
	}
}
