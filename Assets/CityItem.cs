using UnityEngine;
using System.Collections;

public class CityItem : MonoBehaviour
{
	public Vector2 Position
	{
		get { return new Vector2(transform.position.x, transform.position.z); }
		set { transform.position = new Vector3(value.x, 0, value.y); }
	}

	public float Rotation
	{
		get { return transform.eulerAngles.y; }
		set { transform.eulerAngles = new Vector3(0, value, 0); }
	}

	public string ID;
	public Size size;

	[Header("Animation")]
	public AnimationCurve moveCurve;
	public AnimationCurve rotateCurve;

	private IEnumerator moveRoutine;
	private IEnumerator rotateRoutine;

	public void MoveTo(Vector2 target)
	{
		StopMove();

		moveRoutine = MoveRoutine(Position, target, 0.2f);
		StartCoroutine(moveRoutine);
	}

	public void RotateTo(float target)
	{
		StopRotate();

		rotateRoutine = RotateRoutine(Rotation, target, 0.2f);
		StartCoroutine(rotateRoutine);
	}

	public void StopMove()
	{
		if (rotateRoutine != null)
			StopCoroutine(moveRoutine);
	}

	public void StopRotate()
	{
		if (rotateRoutine != null)
			StopCoroutine(rotateRoutine);
	}

	private IEnumerator MoveRoutine(Vector2 origin, Vector2 target, float duration)
	{
		float time = Time.time;
		while (Time.time - time < duration)
		{
			float value = (Time.time - time) / duration;
			Position = Vector2.Lerp(origin, target, moveCurve.Evaluate(value));

			yield return null;
		}
		Position = target;
	}

	private IEnumerator RotateRoutine(float origin, float target, float duration)
	{
		float time = Time.time;
		while (Time.time - time < duration)
		{
			float value = (Time.time - time) / duration;
			Rotation = Mathf.Lerp(origin, target, rotateCurve.Evaluate(value));

			yield return null;
		}
		Rotation = target;
	}
}
