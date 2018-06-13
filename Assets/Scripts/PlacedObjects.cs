using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjects : MonoBehaviour {

	public List<string> listObjects = new List<string>();
	public List<Vector2> positionObjects = new List<Vector2>();
	public List<int> circuits = new List<int>();

	private int id;
	public string source = "test";

	public DataController data;

	public void Start(){
	}

	public void Awake(){
		data = new DataController ();
	}

	public void AddValues(int id, string source){
		this.id = id;
		this.source = source;
	}

	public void InitObjects(){
		StartCoroutine (data.RequestGetFlats (this.id));

		this.listObjects = data.GetObjects ();
		this.positionObjects = data.GetPositions ();
		this.circuits = data.GetCircuits ();
	}

	public void RandInitObjects(){
		StartCoroutine (data.RequestGetRandFlat ());

		this.listObjects = data.GetObjects ();

		this.positionObjects = data.GetPositions ();
		if (data.GetCircuits () != null) {
			this.circuits = data.GetCircuits ();
		}
	}

	public void SendObjects(){
		string obj = "";
		string pos = "";
		string cir = "";

		if (this.listObjects != null && this.circuits != null) {
			for (int i = 0; i < this.listObjects.Count; i++) {
				obj = string.Concat (obj, this.listObjects [i]);
				pos = string.Concat (pos, this.positionObjects [i][0]);
				pos = string.Concat (pos, ";");
				pos = string.Concat (pos, this.positionObjects [i][1]);
				if (i < this.listObjects.Count - 1) {
					obj = string.Concat (obj, ";");
					pos = string.Concat (pos, ";");
				}
			}
			for (int i = 0; i < this.circuits.Count; i++) {
				string.Concat (cir, this.circuits [i].ToString ());
				if (i < this.circuits.Count - 1) {
					string.Concat (cir, ";");
				}
			}
		}

		StartCoroutine(data.RequestPostFlats(this.source, obj, pos, cir));
	}

    public void AddObject(string obj)
    {
		this.listObjects.Add(obj);
    }

    public void AddPosition(Vector2 pos)
    {
        this.positionObjects.Add(pos);
    }
		
    public List<string> GetObjects()
    {
		return this.listObjects;
    }

	public List<Vector2> GetPositions()
    {
		return this.positionObjects;
    }

	public bool isReady(){
		if (this.listObjects.Count > 0) {
			return true;
		}
		return false;
	}
}
