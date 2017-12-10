using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjects : MonoBehaviour {

	protected List<string> listObjects = new List<string>();
	protected List<Vector2> positionObjects = new List<Vector2>();
    protected int capacity = 3;

	static PlacedObjects instance;

	void Start(){

		if (instance != null) {
			Destroy (this.gameObject);
			return; 
		}

		instance = this;

		DontDestroyOnLoad (gameObject);
	}

		
    public void AddObject(string obj)
    {
        listObjects.Add(obj);
    }

    public void AddPosition(Vector2 pos)
    {
        this.positionObjects.Add(pos);
    }
		
    public string GetObject(int ind)
    {
        return this.listObjects[ind];
    }

    public Vector2 GetPosition(int ind)
    {
        return this.positionObjects[ind];
    }
	
    public bool IsIn(Vector2 pos)
    {
        return this.positionObjects.Contains(pos);
    }
}
