using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
	public static CameraManager Instance { get; private set; }

	public float Zoom
	{
		get { return zoom; }
		set
		{
			zoom = Mathf.Clamp01(value);

			float height = Mathf.Lerp(maxHeight, minHeight, heightCurve.Evaluate(zoom));
			float shift = Mathf.Lerp(maxShift, minShift, shiftCurve.Evaluate(zoom));
			float angle = Mathf.Atan2(height, shift) * Mathf.Rad2Deg;

			target.localPosition = new Vector3(0, height, -shift);
			target.localEulerAngles = new Vector3(angle, 0, 0);
		}
	}

	public Vector2 Position
	{
		get { return new Vector2(transform.position.x, transform.position.z); }
		set
		{
			if (limit.width * limit.height > 0)
			{
				value.x = Mathf.Clamp(value.x, limit.xMin, limit.xMax);
				value.y = Mathf.Clamp(value.y, limit.yMin, limit.yMax);
			}
			transform.position = new Vector3(value.x, 0, value.y);
		}
	}

	public float Rotation
	{
		get { return transform.eulerAngles.y; }
		set { transform.eulerAngles = new Vector3(0, value, 0); }
	}

	[SerializeField]
	private Transform target;

	[Header("Height")]
	public float minHeight;
	public float maxHeight;
	public AnimationCurve heightCurve = AnimationCurve.Linear(0, 0, 1, 1);

	[Header("Shift")]
	public float minShift;
	public float maxShift;
	public AnimationCurve shiftCurve = AnimationCurve.Linear(0, 0, 1, 1);

	[Header("Limit")]
	public Rect limit;

	[Header("Animation")]
	public AnimationCurve zoomCurve = AnimationCurve.Linear(0, 0, 1, 1);
	public AnimationCurve moveCurve = AnimationCurve.Linear(0, 0, 1, 1);
	public AnimationCurve rotateCurve = AnimationCurve.Linear(0, 0, 1, 1);

	[SerializeField, Range(0, 1)]
	private float zoom;
	private IEnumerator zoomRoutine;
	private IEnumerator moveRoutine;
	private IEnumerator rotateRoutine;


	private void Awake()
	{
		Instance = this;
		Zoom = zoom;
	}


	public void ZoomTo(float Target)
	{
		StopZoom();

		zoomRoutine = ZoomRoutine(Zoom, Target, 0.2f);
		StartCoroutine(zoomRoutine);
	}

	public void MoveTo(Vector2 Target)
	{
		StopMove();

		moveRoutine = MoveRoutine(Position, Target, 0.2f);
		StartCoroutine(moveRoutine);
	}

	public void RotateTo(float Target)
	{
		StopRotate();

		rotateRoutine = RotateRoutine(Rotation, Target, 0.2f);
		StartCoroutine(rotateRoutine);
	}

	public void StopZoom()
	{
		if (zoomRoutine != null)
			StopCoroutine(zoomRoutine);
	}

	public void StopMove()
	{
		if (moveRoutine != null)
			StopCoroutine(moveRoutine);
	}

	public void StopRotate()
	{
		if (rotateRoutine != null)
			StopCoroutine(rotateRoutine);
	}


	private IEnumerator ZoomRoutine(float Origin, float Target, float Duration)
	{
		float time = Time.time;
		while (Time.time - time < Duration)
		{
			float value = (Time.time - time) / Duration;
			Zoom = Mathf.Lerp(Origin, Target, rotateCurve.Evaluate(value));

			yield return null;
		}
		Zoom = Target;
	}

	private IEnumerator MoveRoutine(Vector2 Origin, Vector2 Target, float Duration)
	{
		float time = Time.time;
		while (Time.time - time < Duration)
		{
			float value = (Time.time - time) / Duration;
			Position = Vector2.Lerp(Origin, Target, moveCurve.Evaluate(value));

			yield return null;
		}
		Position = Target;
	}

	private IEnumerator RotateRoutine(float Origin, float Target, float Duration)
	{
		float time = Time.time;
		while (Time.time - time < Duration)
		{
			float value = (Time.time - time) / Duration;
			Rotation = Mathf.Lerp(Origin, Target, rotateCurve.Evaluate(value));

			yield return null;
		}
		Rotation = Target;
	}
}
