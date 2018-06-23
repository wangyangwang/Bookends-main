using UnityEngine;
using System.Collections.Generic;
public class MGCurve : MonoBehaviour {

	 
	[SerializeField]
	private List<Vector3> points;

	[SerializeField]
	private bool loop;

	public bool Loop {
		get {
			return loop;
		}
		set {
			loop = value;
			if (value == true) {
				SetControlPoint(0, points[0]);
			}
		}
	}

	public int ControlPointCount {
		get {
			return points.Count;
		}
	}

	public Vector3 GetControlPoint (int index) {
		return points[index];
	}

	public void SetControlPoint (int index, Vector3 point) {
		if (index % 3 == 0) {
			Vector3 delta = point - points[index];
			if (loop) {
				if (index == 0) {
					points[1] += delta;
					points[points.Count - 2] += delta;
					points[points.Count - 1] = point;
				}
				else if (index == points.Count - 1) {
					points[0] = point;
					points[1] += delta;
					points[index - 1] += delta;
				}
				else {
					points[index - 1] += delta;
					points[index + 1] += delta;
				}
			}
			else {
				if (index > 0) {
					points[index - 1] += delta;
				}
				if (index + 1 < points.Count) {
					points[index + 1] += delta;
				}
			}
		}
		points[index] = point;
	}


	public int CurveCount {
		get {
			return (points.Count - 1) / 3;
		}
	}

	public Vector3 GetPoint (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Count - 4;
		}
		else {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Curve.GetPoint(points[i], points[i + 1], points[i + 2], points[i + 3], t));
	}

	public Vector3 GetVelocity (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Count - 4;
		}
		else {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Curve.GetFirstD(points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
	}

	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}

	public void AddCurve () {
		Vector3 point = points[points.Count - 1];
		point.x += 1f;
		points.Add (point);
		point.x += 1f;
		points.Add (point);
		point.x += 1f;
		points.Add (point);
		if (loop) {
			points[points.Count - 1] = points[0];
		}
	}

	public void Reset () {
		points = new  List<Vector3> {
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f),
			new Vector3(3f, 0f, 0f),
			new Vector3(4f, 0f, 0f)
		};

	}
}
public static class Curve {

	public static Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, float t) {
		t = Mathf.Clamp01(t);
		float negt = 1f - t;
		return negt * negt * p0 + 2f * negt * t * p1 + t * t * p2;
	}
	public static Vector3 GetPoint (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		t = Mathf.Clamp01(t);
		float negt = 1f - t;
		return negt * negt * negt * p0 + 3f * negt * negt * t * p1 + 3f * negt * t * t * p2 + t * t * t * p3;
	}

	public static Vector3 GetFirstD (Vector3 p0, Vector3 p1, Vector3 p2, float t) {
		return 2f * (1f - t) * (p1 - p0) + 2f * t * (p2 - p1);
	}
		
	public static Vector3 GetFirstD(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		t = Mathf.Clamp01(t);
		float negt = 1f - t;
		return 3f * negt * negt * (p1 - p0) + 6f * negt * t * (p2 - p1) + 3f * t * t * (p3 - p2);
	}
}