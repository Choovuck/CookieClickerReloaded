using UnityEngine;
using System.Collections;

public class RotationBG : MonoBehaviour {

  public float turnSpeed = 10;
  public float maxSpeed = 15;
  public float speedIncrease = 1;
  public float decreaseRate = 0.4f;
  private float currentSpeed = 0.0f;

	void Update()
  {
    float speedDecrease = Mathf.Abs(currentSpeed - turnSpeed) * decreaseRate * Mathf.Sign(turnSpeed) * -1.0f;
    currentSpeed += speedDecrease * Time.deltaTime;
    if (Mathf.Abs(currentSpeed) < Mathf.Abs(turnSpeed))
      currentSpeed = turnSpeed;

    transform.Rotate(Vector3.forward, currentSpeed * Time.deltaTime);
	}

  void Awake()
  {
    currentSpeed = turnSpeed;
  }

  public void Push()
  {
    currentSpeed += speedIncrease;
    if (Mathf.Abs(currentSpeed) > Mathf.Abs(maxSpeed))
      currentSpeed = maxSpeed;
  }
}
