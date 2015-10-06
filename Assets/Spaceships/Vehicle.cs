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

	#endregion

	private float Speed = 0;

	private float yaw = 0;
	private float pitch = 0;
	private float roll;

	void Start()
	{

	}

	void Update()
	{
		// Forward backward
		if (Input.GetAxis("Vertical") > 0.5)
		{
			Speed += Acceleration * Time.deltaTime;
		}
		else if (Input.GetAxis("Vertical") < -0.5)
		{
			Speed -= Brake * Time.deltaTime;
		}
		else
		{
			Speed -= NaturalDecelleration * Time.deltaTime;
		}
		Speed = Mathf.Clamp(Speed, IdleSpeed, MaxSpeed);


		// Roll
		//Input.GetAxis("Horizontal");
		//roll += 8 * Time.deltaTime;
		//transform.rotation = Quaternion.AngleAxis(roll, transform.forward);

		// Mouse Steering
		var mx = Input.GetAxis("Mouse X");
		var my = Input.GetAxis("Mouse Y");

		yaw += MouseXSensitivity * mx * Time.deltaTime;
		pitch -= MouseYSensitivity * my * Time.deltaTime;


		roll = MouseXSensitivity * mx * Time.deltaTime * -40;
		//roll -= Time.deltaTime;

		//transform.Rotate(0, yaw, 0);

		transform.rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
		//transform.rotation *= Quaternion.AngleAxis(roll, Vector3.forward);

		transform.position += transform.forward * Speed;

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			Debug.Log("Break!");
			Debug.Break();
		}
	}
}

