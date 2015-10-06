using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour
{
	#region Public Members

	public float Acceleration = 2f;

	public float NaturalDecelleration = 1f;

	public float IdleSpeed = 1;

	public float Brake = 3f;

	public float MaxSpeed = 5f;

	public float MouseXSensitivity = 100f;
	public float MouseYSensitivity = 100f;

	public float TimeMultiplier = 1;
	#endregion

	private float Speed = 0;

	private float yaw = 0;
	private float pitch = 0;
	private float roll;

	void Start() { }

	public void Update()
	{
		Drive(Time.deltaTime * TimeMultiplier);
	}

	void Drive(float t)
	{
		// Forward backward
		if (Input.GetAxis("Vertical") > 0.5)
		{
			Speed += Acceleration * t;
		}
		else if (Input.GetAxis("Vertical") < -0.5)
		{
			Speed -= Brake * t;
		}
		else
		{
			Speed -= NaturalDecelleration * t;
		}
		Speed = Mathf.Clamp(Speed, IdleSpeed, MaxSpeed);

		// Roll
		Input.GetAxis("Horizontal");
		roll += 8 * t;
		//transform.rotation = Quaternion.AngleAxis(roll, transform.forward);

		// Mouse Steering
		var mx = Input.GetAxis("Mouse X");
		var my = Input.GetAxis("Mouse Y");

		yaw += MouseXSensitivity * mx * t;
		pitch -= MouseYSensitivity * my * t;
		//yaw += MouseXSensitivity * Mathf.Cos(Time.deltaTime * Mathf.Deg2Rad * 1);// * t;
		//pitch -= MouseYSensitivity * 1f * t;


		roll = MouseXSensitivity * mx * t * -40;
		//roll -= Time.deltaTime;

		//transform.Rotate(0, yaw, 0);

		//transform.rotation = Quaternion.AngleAxis(yaw, Vector3.up);

		transform.rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
		//transform.rotation *= Quaternion.AngleAxis(roll, Vector3.forward);

		transform.position += transform.forward * Speed * t;

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			Debug.Log("Break!");
			Debug.Break();
		}
	}
}

