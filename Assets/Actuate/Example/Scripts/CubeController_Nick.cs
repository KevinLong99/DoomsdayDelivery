using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CubeController_Nick : MonoBehaviour {
	public float MaxForce = 500;
	private Vector3 lastPosition;

	void Start()
	{

	}

	void FixedUpdate()
	{
		if (Input.GetKey("up"))
		{
			//add force to the rigidbody
			
		}

		if (Input.GetKey("down"))
		{

		}
	}

	/*

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

	*/

	

}
