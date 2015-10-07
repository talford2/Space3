using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
	public Transform Target;

	public Vector3 Offset = new Vector3(0, 0.5f, -3f);

	public float CatchupTime = 1f;

	public float TurnSmoothing = 30f;

	public bool UseSphereLerp = false;

	public bool UseLateUpdate = false;

	void Start()
	{

	}

	private void UpdateCamera(float t)
	{
		if (UseSphereLerp)
		{
			transform.position = Vector3.Slerp(transform.position, Target.position + Target.TransformDirection(Offset), t * CatchupTime);
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, Target.position + Target.TransformDirection(Offset), t * CatchupTime);
		}
		
		var relativePosition = Target.position - transform.position;
		var targetRotation = Quaternion.LookRotation(relativePosition);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * CatchupTime);
	}

	void Update()
	{
		if (!UseLateUpdate)
		{
			UpdateCamera(Time.deltaTime);
		}
	}

	void LateUpdate()
	{
		if (UseLateUpdate)
		{
			UpdateCamera(Time.deltaTime);
		}
	}
}
