using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject objectToControl;
	public ExtraData extraData;
	public Actuate.ActuateAgent actuateAgent;

	// Use this for initialization
	void Start () {
		actuateAgent.SetMotionSource(this.gameObject, extraData);
	}

	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position = objectToControl.transform.position;
	}
}
