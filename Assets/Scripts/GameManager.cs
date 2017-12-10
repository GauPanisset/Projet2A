using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Transform mark;
	public string typeObject = null;
	public List<Vector2> positions = new List<Vector2>();
	public List<string> placedObjects = new List<string> ();
	public KeyCode clic;

	public Toggle isBaignoire;
	public Toggle isCanape;
	public Toggle isChaise;
	public Toggle isEvier;
	public Toggle isTableRec;
	public Toggle isTableRon;
	public Toggle isToilette;

	public int nbObjects = 3;
	public int counter = 0;
	public Text nbObjectsText;

	// Use this for initialization
	void Start () {
		nbObjectsText.text = "0/" + nbObjects;
	}

	// Update is called once per frame
	void Update () {

		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 obj = Camera.main.ScreenToWorldPoint (mousePosition);
		SetTypeObject ();
		if (Input.GetKey (clic) && obj[0] < 4.78 && typeObject != null && counter < nbObjects) { 	//Check if the clic is on the GamePanel. Check if one specific object is active.
			if (positions.Contains (mousePosition) == false) { 	//To solve the multiple input for one click issue.
				positions.Add (mousePosition);
				placedObjects.Add(typeObject);
				counter++;
				Instantiate (mark, new Vector3(obj[0], obj[1], -1), mark.rotation);
				nbObjectsText.text = counter + "/" + nbObjects;
				for (int i = 0; i < counter; i++) {
					Debug.Log (positions[i]);
					Debug.Log (placedObjects[i]);
				}
			}
		}
	}

	void SetTypeObject(){
		if (isBaignoire.isOn) {
			typeObject = "baignoire";
		} else if (isCanape.isOn) {
			typeObject = "canape";
		} else if (isChaise.isOn) {
			typeObject = "chaise";
		} else if (isEvier.isOn) {
			typeObject = "evier";
		} else if (isTableRec.isOn) {
			typeObject = "tablerec";
		} else if (isTableRon.isOn) {
			typeObject = "tableron";
		} else if (isToilette.isOn) {
			typeObject = "toilette";
		} else {
			typeObject = null;
		}
	}
}
