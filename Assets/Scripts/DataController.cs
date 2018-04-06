using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class DataController : MonoBehaviour {

	public string url; //"http://localhost:3131"

	public int id;
	public string username;
	public string objects;
	public string pos;
	public string circuits;
	public int level;
	public int score;

	public string result;

	void Start(){
	}

	IEnumerator RequestGetPlayers (string username){
		string request = url + "/" + WWW.EscapeURL (username);
		UnityWebRequest www = UnityWebRequest.Get (request); 

		Debug.Log (request);

		yield return www.SendWebRequest ();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		} else if ((result = www.downloadHandler.text) != "") {
			result = www.downloadHandler.text;
			Debug.Log (result); 
		} else {
			Debug.Log ("Can't get");
		}
	}		

	IEnumerator RequestGetFlats (int id){
		string request = url + "/flats/" + id;					//url of the request
		UnityWebRequest www = UnityWebRequest.Get (request); 	//create the GET request

		yield return www.SendWebRequest ();

		if (www.isNetworkError || www.isHttpError) {			//Error managment
			Debug.Log (www.error);
		} else if ((result = www.downloadHandler.text) != "") {
			result = www.downloadHandler.text;
			Debug.Log (result); 
		} else {
			Debug.Log ("Can't get");
		}
	}
		
	IEnumerator RequestPostFlats(string src, string objects, string pos, string circuits){
		string request = url + "/flats";						//url of the request

		WWWForm form = new WWWForm ();							//create the body of the request

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

	IEnumerator RequestPostPlayers(string username, int level, int score, int flat){
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

	IEnumerator RequestPatchPlayersScore(string username, int score){
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

	void OnGUI(){
		if (GUI.Button (new Rect (20, 250, 100, 30), "Get")) {
			StartCoroutine(RequestGetPlayers (username));
		}
		if (GUI.Button (new Rect (150, 250, 100, 30), "Post")) {
			StartCoroutine (RequestPostPlayers (username, level, score, id));
		}
		if (GUI.Button (new Rect (280, 250, 100, 30), "Patch")) {
			StartCoroutine(RequestPatchPlayersScore(username, score));
		}
	}
}


