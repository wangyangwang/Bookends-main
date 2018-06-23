using UnityEngine;
using System.Collections;

public class MGMoveCurves: MonoBehaviour {

	public MGCurve mgcurves;
	public float duration;
	private float progress;
	private void Update () {
			progress += Time.deltaTime / duration;
		if (progress > 1f) {
			progress =0;
		}
		Vector3 position = mgcurves.GetPoint(progress);
		transform.localPosition = position;

		Vector3 dir = position + mgcurves.GetDirection (progress);
		transform.LookAt (dir);
	}
}
