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

	public float RollAcceleration = 100f;

	public float TimeMultiplier = 1;

	public float BankingRestore = 3f;
	#endregion

	private float Speed = 0;

	private float yaw = 0;
	private float pitch = 0;
	private float roll = 0;


	private float rollSpeed = 0;

	private float bank = 0;

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
		transform.position += transform.forward * Speed * t;

		// Mouse Steering
		//var mx = Input.GetAxis("Mouse X");
		//var my = Input.GetAxis("Mouse Y");

		var pitch = Input.GetAxis("Mouse Y") * -1;
		//if (Input.GetKey(KeyCode.W)) { pitch = 1; }
		//if (Input.GetKey(KeyCode.S)) { pitch = -1; }


		//var roll = 0f;
		if (Input.GetKey(KeyCode.A))
		{
			rollSpeed += RollAcceleration * Time.deltaTime;
			roll = 1;

			//roll = Mathf.Lerp(roll, 1, Time.deltaTime * 1000);
		}
		if (Input.GetKey(KeyCode.D))
		{
			rollSpeed -= RollAcceleration * Time.deltaTime;
			roll = -1;
		}

		if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			roll *= (1 - Time.deltaTime * 3f);
		}

		//roll = rollSpeed * Time.deltaTime * RollAcceleration;

		var yaw = Input.GetAxis("Mouse X");
		//if (Input.GetKey(KeyCode.Q)) { yaw = 1; }
		//if (Input.GetKey(KeyCode.E)) { yaw = -1; }

		Debug.Log("roll = " + roll);

		// Rotation transforms
		transform.rotation *= Quaternion.AngleAxis(MouseYSensitivity * pitch * t, Vector3.right);// transform.right);
																								 //transform.rotation *= Quaternion.AngleAxis(MouseXSensitivity * mx * t, transform.up);
		transform.rotation *= Quaternion.AngleAxis(150 * roll * t, Vector3.forward);

		transform.rotation *= Quaternion.AngleAxis(MouseXSensitivity * yaw * t, Vector3.up);


		Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);

		//transform.rotation = Quaternion.AngleAxis(pitch, transform.right);
		//transform.rotation = Quaternion.AngleAxis(roll, transform.forward);

		//transform.rotation = Quaternion.AngleAxis(roll, transform.forward);
		//transform.rotation *= Quaternion.AngleAxis(pitch, Vector3.right);
		//transform.rotation *= Quaternion.AngleAxis(yaw, Vector3.up);

		//transform.rotation *= Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
		//transform.rotation *= Quaternion.AngleAxis(bank, Vector3.forward);

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			Debug.Log("Break!");
			Debug.Break();
		}
	}
}

