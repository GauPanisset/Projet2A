using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;


public class DataController {

	public string url = "http://localhost:3131";

	protected int id;				
	protected string username;
	protected string src;
	protected List<string> objects = new List<string>();
	protected List<Vector2> positions = new List<Vector2>();
	protected List<int> circuits = new List<int>();
	protected int level;
	protected int score;
	protected int flat;
	protected int count;

	public IEnumerator RequestGetPlayers (string username){
		string res;
		string request = url + "/" + WWW.EscapeURL (username);
		UnityWebRequest www = UnityWebRequest.Get (request); 

		yield return www.SendWebRequest ();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		} else if ((res = www.downloadHandler.text) != "") {

			JSONNode result = JSON.Parse (res);

			id = int.Parse (result [0][0]);
			username = result [0][1];
			level = int.Parse (result [0][2]);
			score = int.Parse (result [0][3]);
			flat = int.Parse (result [0][4]);

			Debug.Log ("Get");

		} else {
			Debug.Log ("Can't get");
		}
	}

	public IEnumerator RequestGetFlats (int id){
		string res;
		string request = url + "/flats/" + id;					//url of the request
		UnityWebRequest www = UnityWebRequest.Get (request); 	//create the GET request

		yield return www.SendWebRequest ();

		if (www.isNetworkError || www.isHttpError) {			//Error managment
			Debug.Log (www.error);
		} else if ((res = www.downloadHandler.text) != "") {
			
			JSONNode result = JSON.Parse (res);					//Parsing JSON

			string[] obj = ((string)result [0] [0]).Split (';');
			string[] pos = ((string)result [0] [1]).Split (';');
			string[] cir = ((string)result [0] [2]).Split (';');

			for (int i = 0; i < obj.Length; i++) {
				objects.Add (obj [i]);
				float x = float.Parse (pos [2 * i]);
				float y = float.Parse (pos [2 * i + 1]);
				positions.Add (new Vector2 (x, y));
			}
			for (int i = 0; i < cir.Length; i++) {
				circuits.Add (int.Parse(cir [i]));
			}

			Debug.Log ("Get");

		} else {
			Debug.Log ("Can't get");
		}
	}

	public IEnumerator RequestGetIdFlats(){
		string res;
		string request = url + "/idflats";

		UnityWebRequest www = UnityWebRequest.Get (request);

		yield return www.SendWebRequest ();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		} else if ((res = www.downloadHandler.text) != "") {
			
			JSONNode result = JSON.Parse(res);

			string[] obj = ((string)result [0] [0]).Split (';');
			string[] pos = ((string)result [0] [1]).Split (';');
			string[] cir = ((string)result [0] [2]).Split (';');

			for (int i = 0; i < obj.Length; i++) {
				objects.Add (obj [i]);
				float x = float.Parse (pos [2 * i]);
				float y = float.Parse (pos [2 * i + 1]);
				positions.Add (new Vector2 (x, y));
			}
			for (int i = 0; i < cir.Length; i++) {
				circuits.Add (int.Parse(cir [i]));
			}

			Debug.Log ("Get");

		} else {
			Debug.Log ("Can't get");
		}
	}

	public IEnumerator RequestGetRandFlat(){
		string res;
		string request = url + "/flatsrand";

		UnityWebRequest www = UnityWebRequest.Get (request);

		yield return www.SendWebRequest ();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		} else if ((res = www.downloadHandler.text) != "") {

			JSONNode result = JSON.Parse(res);
		
			string[] obj = (result[0].ToString().Substring(1,result[0].ToString().Length - 2)).Split(';');
			string[] pos = (result[1].ToString().Substring(1,result[1].ToString().Length - 2)).Split(';');
			string[] cir = (result[2].ToString().Substring(1,result[2].ToString().Length - 2)).Split(';');

			for (int i = 0; i < obj.Length; i++) {
				objects.Add (obj [i]);
				float x = float.Parse (pos [2 * i]);
				float y = float.Parse (pos [2 * i + 1]);
				positions.Add (new Vector2 (x, y));
			}
			if (cir.Length == 0) {
				for (int i = 0; i < cir.Length; i++) {
					circuits.Add (int.Parse (cir [i]));
				}
			}

			Debug.Log ("Get");

		} else {
			Debug.Log ("Can't get");
		} 
	}
		
	public IEnumerator RequestPostFlats(string src, string objects, string pos, string circuits){
		string request = url + "/flats";						//url of the request

		WWWForm form = new WWWForm ();							//create the body of the request

		Debug.Log (request);

		form.AddField ("url", src);
		form.AddField ("objects", objects);
		form.AddField ("pos", pos);
		form.AddField ("circuits", circuits);

		UnityWebRequest www = UnityWebRequest.Post (request, form);			//create the POST request

		yield return www.SendWebRequest();						 

		if (www.isNetworkError || www.isHttpError) {			//Error managment
			Debug.Log (www.error);
		} else {
			Debug.Log ("Form uplaod complete!");
		}
	}
		
	public IEnumerator RequestPostPlayers(string username, int level, int score, int flat){
		string request = url + "/players";			//url of the request

		WWWForm form = new WWWForm ();				//create the body of the request

		form.AddField ("name", username);
		form.AddField ("level", level);
		form.AddField ("score", score);
		form.AddField ("flat", flat);

		UnityWebRequest www = UnityWebRequest.Post (request, form);			//create the POST request

		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {			//Error managment
			Debug.Log (www.error);
		} else {
			Debug.Log ("Form uplaod complete!");
		}
	}

	public IEnumerator RequestPatchPlayersScore(string username, int score){
		string request = url + "/players/score/" + WWW.EscapeURL (username);		//url of the request

		WWWForm form = new WWWForm ();							//create the body of the request
		form.AddField ("score", score);

		UnityWebRequest www = UnityWebRequest.Post (request, form);			//create the PATCH request with a POST request
		//content-type : application/x-www-form-urelencoded
		Debug.Log (request);
		yield return www.SendWebRequest ();

		if (www.isNetworkError || www.isHttpError) {			//Error managment
			Debug.Log (www.error);
		} else {
			Debug.Log ("Form uplaod complete!");
		}
	}

	/*public void OnGUI(){														//GUI for tests
		if (GUI.Button (new Rect (20, 250, 100, 30), "Get")) {
			StartCoroutine(RequestGetPlayers (username));
		}
		if (GUI.Button (new Rect (150, 250, 100, 30), "Post")) {
			StartCoroutine (RequestPostPlayers (username, level, score, id));
		}
		if (GUI.Button (new Rect (280, 250, 100, 30), "Patch")) {
			StartCoroutine(RequestPatchPlayersScore(username, score));
		}
	}*/

	public int GetId(){
		return this.id;
	}

	public int GetLevel(){
		return this.level;
	}

	public int GetScore(){
		return this.score;
	}

	public int GetFlat(){
		return this.flat;
	}

	public int GetCount(){
		return this.count;
	}

	public string GetUsername(){
		return this.username;
	}

	public string GetSource(){
		return this.src;
	}

	public List<string> GetObjects(){
		return this.objects;
	}

	public List<int> GetCircuits(){
		return this.circuits;
	}

	public List<Vector2> GetPositions(){
		return this.positions;
	}
}


