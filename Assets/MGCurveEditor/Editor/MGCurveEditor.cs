using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(MGCurve))]
public class MGCurveEditor : Editor {



	private MGCurve mgcurves;
	private Transform handleTr;
	private Quaternion handleRot;
	private int selectedIndex = -1;
	Color colorCurve = Color.cyan;
	public override void OnInspectorGUI () {
		mgcurves = target as MGCurve;
		EditorGUI.BeginChangeCheck();

		colorCurve = EditorGUILayout.ColorField(colorCurve);
		if (selectedIndex >= 0 && selectedIndex < mgcurves.ControlPointCount) {
			DrawSelectedPointInspector();
		}
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button("Add Point")) {
			Undo.RecordObject(mgcurves, "Add Point");
			mgcurves.AddCurve();
			EditorUtility.SetDirty(mgcurves);
		}
		if (GUILayout.Button("Loop",GUILayout.Width(50))) {
			Undo.RecordObject(mgcurves, "Loop");
			EditorUtility.SetDirty(mgcurves);
			mgcurves.Loop = true;
		}
		EditorGUILayout.EndHorizontal ();
	}

	private void DrawSelectedPointInspector() {
		GUILayout.Label("Current Point");
		EditorGUI.BeginChangeCheck();
		Vector3 point = EditorGUILayout.Vector3Field("Position", mgcurves.GetControlPoint(selectedIndex));
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(mgcurves, "Move Point");
			EditorUtility.SetDirty(mgcurves);
			mgcurves.SetControlPoint(selectedIndex, point);
		}

	}

	private void OnSceneGUI () {
		mgcurves = target as MGCurve;
		handleTr = mgcurves.transform;
		handleRot = Tools.pivotRotation == PivotRotation.Local ?
	    handleTr.rotation : Quaternion.identity;

		Vector3 p0 = ShowPoint(0);
		for (int i = 1; i < mgcurves.ControlPointCount; i += 3) {
			Vector3 p1 = ShowPoint(i);
			Vector3 p2 = ShowPoint(i + 1);
			Vector3 p3 = ShowPoint(i + 2);

			Handles.color = Color.gray;
			Handles.DrawLine(p0, p1);
			Handles.DrawLine(p2, p3);

			Handles.DrawBezier(p0, p3, p1, p2, colorCurve, null, 2f);
			p0 = p3;
		}
	}


	private Vector3 ShowPoint (int index) {
		Vector3 point = handleTr.TransformPoint(mgcurves.GetControlPoint(index));
		float size = HandleUtility.GetHandleSize(point);
		if (index == 0) {
			size *= 2f;
		}
		Handles.color = Color.magenta;
		if (Handles.Button(point, handleRot, size * 0.04f, size * 0.06f, Handles.DotCap)) {
			selectedIndex = index;
			Repaint();
		}
		if (selectedIndex == index) {
			EditorGUI.BeginChangeCheck();
			point = Handles.DoPositionHandle(point, handleRot);
			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObject(mgcurves, "Move Point");
				EditorUtility.SetDirty(mgcurves);
				mgcurves.SetControlPoint(index, handleTr.InverseTransformPoint(point));
			}
		}
		return point;
	}
}
