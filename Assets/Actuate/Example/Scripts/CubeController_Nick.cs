using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class CubeController_Nick : MonoBehaviour {
	//public float MaxForce = 500;

	//0.5f is weak, 1 is good.
	//need (slightly) higher numbers when moving along the z axis (force3x and 4x)
	//easier to move chair forwards and backwards than side to side

	private Vector3 force1x = new Vector3(0, 0, 1);		//
    private Vector3 force2x = new Vector3(0, 0, -2);	//
    private Vector3 force3x = new Vector3(-2, 0, 0);	//
    private Vector3 force4x = new Vector3(3, 0, 0);		//
    void Start()
	{
		StartCoroutine(JustWait());
    }

	void FixedUpdate()
	{


		if (Input.GetKey(KeyCode.UpArrow))
		{
			//add force to the rigidbody
			this.gameObject.GetComponent<Rigidbody>().AddForce(force1x, ForceMode.Impulse);
			Debug.Log("FORWARD");
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			this.gameObject.GetComponent<Rigidbody>().AddForce(force2x, ForceMode.Impulse);
            Debug.Log("BACK");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(force3x, ForceMode.Impulse);
            Debug.Log("LEFT");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(force4x, ForceMode.Impulse);
            Debug.Log("RIGHT");
        }
    }

	IEnumerator JustWait()
	{
		yield return new WaitForSeconds(5);
        this.gameObject.GetComponent<Rigidbody>().AddForce(force1x, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        this.gameObject.GetComponent<Rigidbody>().AddForce(force2x, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        this.gameObject.GetComponent<Rigidbody>().AddForce(force3x, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        this.gameObject.GetComponent<Rigidbody>().AddForce(force4x, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        
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
