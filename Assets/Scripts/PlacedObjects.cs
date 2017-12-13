using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjects : MonoBehaviour {

	protected List<List<string>> listObjects = new List<List<string>>();
	protected List<List<Vector2>> positionObjects = new List<List<Vector2>>();

	static int nbList = -1;
	static PlacedObjects instance;

	void Start(){

		if (instance != null) {
			Destroy (this.gameObject);
			return; 
		} 

		instance = this;

		DontDestroyOnLoad (gameObject);
	}
		
	public void NewList(){
		listObjects.Add (new List<string> ());
		positionObjects.Add (new List<Vector2> ());
		nbList++;
	}

	public int GetNbList(){
		return nbList;
	}

    public void AddObject(int id, string obj)
    {
		listObjects[id].Add(obj);
    }

    public void AddPosition(int id, Vector2 pos)
    {
        this.positionObjects[id].Add(pos);
    }
		
    public string GetObject(int id, int ind)
    {
		return this.listObjects[id][ind];
    }

    public Vector2 GetPosition(int id, int ind)
    {
		return this.positionObjects[id][ind];
    }
	
	public bool IsIn(int id, Vector2 pos)
    {
		return this.positionObjects[id].Contains(pos);
    }
}
