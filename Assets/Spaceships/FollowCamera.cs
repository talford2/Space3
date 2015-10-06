using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
	public Transform Target;

	public Vector3 Offset = new Vector3(0, 0.5f, -3f);

	public float CatchupTime = 1f;

	public bool UseSphereLerp = false;

	void Start()
	{

	}

	void Update()
	{
		//if (UseSphereLerp)
		//{
		//	transform.position = Vector3.Slerp(transform.position, Target.position + Offset, Time.deltaTime * CatchupTime);
		//}
		//else
		//{

		transform.position = Vector3.Lerp(transform.position, Target.position + Target.TransformDirection(Offset), Time.deltaTime * CatchupTime);
		//}

		var d = Target.position - transform.position;
		transform.rotation = Quaternion.LookRotation(d);
	}
}
