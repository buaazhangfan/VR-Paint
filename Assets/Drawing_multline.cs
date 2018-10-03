using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Drawing_multline : MonoBehaviour
{

	public float width = 0.02f;
    public Transform lefthand;
	private List<GameObject> Sphere = new List<GameObject>();

	private List<Vector3> points = new List<Vector3>();
	private LineRenderer currLine;
	private int select_index = -1;



	private List<Color> colors = new List<Color>();
	private int colorIndex = 0;
	private int sphereindex = -1;
	private bool old_p = false;
	private Color tem_color;
	void Start()
	{
		// Initialize colors
		colors.Add(Color.black);
		colors.Add(Color.blue);
		colors.Add(Color.cyan);
		colors.Add(Color.gray);
		colors.Add(Color.green);
		colors.Add(Color.white);
		colors.Add(Color.red);
		colors.Add(Color.yellow);


	}

	// Update is called once per frame
	void Update()
	{
		float pressed = OVRInput.Get (OVRInput.Axis1D.SecondaryIndexTrigger);
		//float pressed_trig = OVRInput.GetDown(OVRInput.Axis1D.SecondaryIndexTrigger);
		// Stroke size
		if (OVRInput.Get (OVRInput.Button.SecondaryThumbstickUp)) {
			
			width += 0.001f;
			currLine.SetWidth (width, width);
		}
		if (OVRInput.Get (OVRInput.Button.SecondaryThumbstickDown)) {
			width -= 0.001f;
			currLine.SetWidth (width, width);
		}

		// Color change
		if (OVRInput.GetDown (OVRInput.Button.One)) {
			colorIndex++;
			currLine.material.color = colors [colorIndex % colors.Count];
			tem_color = colors [colorIndex % colors.Count];
		}

		// Draw
		if (pressed > 0) {
			//print ("pressed");
			if (old_p == false) {
				print ("pressed_trig");
				//GameObject.CreatePrimitive (PrimitiveType.Sphere);
				GameObject temp = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				temp.transform.localScale = new Vector3 (0.0F, 0.0F, 0.0F);
				temp.transform.position = gameObject.transform.position;
				Sphere.Add (temp);
				old_p = true;
				sphereindex += 1;
				Sphere [sphereindex].transform.parent = gameObject.transform;
				currLine = Sphere [sphereindex].AddComponent<LineRenderer> ();
				// currLine.SetColors(c1, c2);
				currLine.material.color = colors [0];
				currLine.SetWidth (width, width);
				currLine.SetVertexCount (0);
				points.Clear ();
			}

			Vector3 pos = Sphere [sphereindex].transform.position;
			if (points.Count == 0 || points [points.Count - 1] != pos)
				points.Add (pos);
			if (points.Count > 1) {
				currLine.SetVertexCount (points.Count);
				currLine.SetPositions (points.ToArray ());
			}
		}
		if (pressed == 0)
			old_p = false;
	/*	if (points.Count > 1) {
			currLine.SetVertexCount (points.Count);
			currLine.SetPositions (points.ToArray ());
		}*/

		// Clear last stroke
		if (OVRInput.GetDown(OVRInput.Button.Two)) {
			Sphere[select_index].GetComponent<LineRenderer> ().SetVertexCount (0);
			points.Clear ();
			Destroy (Sphere [select_index]);
			Sphere.RemoveAt (select_index);
			select_index--;
			sphereindex--;
			currLine = Sphere [select_index].GetComponent<LineRenderer> ();
		}
		if (OVRInput.Get (OVRInput.Button.Three)) {
			print ("cleear");
			//Sphere.Clear ();
			for (int i = 0; i < Sphere.Count; i++) {
				Destroy (Sphere [i]);
			}
			points.Clear ();
			Sphere.Clear ();
			sphereindex = -1;
			select_index = -1;
		}

		if (OVRInput.GetDown (OVRInput.Button.Four)) {
			print ("button");
			if (select_index != -1)
				Sphere [select_index].GetComponent<LineRenderer> ().material.color = tem_color;
			select_index++;
			select_index = select_index % Sphere.Count;
			tem_color = Sphere [select_index].GetComponent<LineRenderer> ().material.color;
			currLine = Sphere [select_index].GetComponent<LineRenderer> ();
			print (select_index);
			currLine.material.color = Color.magenta;
		}

           
            
        

	}
}