using UnityEngine;
using System.Collections;

public class RotationBG : MonoBehaviour {

    public float turnSpeed = 10;

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
	}
}
