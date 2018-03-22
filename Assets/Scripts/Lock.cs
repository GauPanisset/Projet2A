using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour {

	public Transform locks;
	public string typeObject = null;

	public KeyCode clic;

	public Toggle is1;
	public Toggle is2;
	public Toggle is3;
	public Toggle is4;
	public Toggle is5;

	public static int nbObjects = 3;
	public int counter = 0;
	public Text nbObjectsText;

	public PlacedObjects po;
	public int id;

	// Use this for initialization
	void Start () {
		nbObjectsText.text = "0/" + nbObjects;

		GameObject go = GameObject.Find("PlacedObjects");
		if(go == null){
			Debug.LogError("Failed to find an object named 'PlacedObjects'");
			this.enabled = false;
			return;
		}

		po = go.GetComponent<PlacedObjects>();
		po.NewList ();
		id = po.GetNbList ();
	}

	// Update is called once per frame
	void Update () {

		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 obj = Camera.main.ScreenToWorldPoint (mousePosition);
		Vector2 screenPosition = Camera.main.ScreenToViewportPoint (mousePosition);
		SetTypeObject ();
		if (Input.GetKeyDown(clic) && screenPosition [0] < 0.8 && typeObject != null  && counter < nbObjects) { 	//Check if the clic is on the GamePanel. Check if one specific object is active.
			po.AddPosition (id, mousePosition);
			po.AddObject(id, typeObject);
			counter++;
			Instantiate (locks, new Vector3(obj[0], obj[1], -1), locks.rotation);
			nbObjectsText.text = counter + "/" + nbObjects;
		}
	}


	private void SetTypeObject(){
		if (is1.isOn) {
			typeObject = "Ele1";
		} else if (is2.isOn) {
			typeObject = "Ele2";
		} else if (is3.isOn) {
			typeObject = "Ele3";
		} else if (is4.isOn) {
			typeObject = "Ele4";
		} else if (is5.isOn) {
			typeObject = "Ele5";
		} else {
			typeObject = null;
		}
	}
}
