using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using System.Linq;


public class DataController : MonoBehaviour {

	public string url; //"http://localhost:3131"
	//public ArrayList selectdata;
	public string username;
	public string password;

	public string result;

	void Start(){
		//selectdata = new ArrayList();
		//StartCoroutine (SelectData ("admin"));
	}
		

	IEnumerator SendDataGet(string username){
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
		/*Parsing with JSON

		} else if (data.text != "") {
			JSONArray parsed_data = JSON.Parse (data.text).AsArray;
			foreach (KeyValuePair<string, SimpleJSON.JSONNode>row in parsed_data) {			//var
				selectdata.Add (row);
			}
		}*/
	}
		
	IEnumerator SendDataPost(string username, string password){
		WWWForm form = new WWWForm ();

		form.AddField ("username", username);
		form.AddField ("password", password);

		Dictionary<string, string> headers = form.headers;
		Debug.Log (headers ["Content-Type"]);
		/*if (headers.ContainsKey ("Content-Type")) {
			headers ["Content-Type"] = "application/x-www-form-urlencoded";
		} else {
			headers.Add ("Content-Type", "application/x-www-form-urlencoded");
		}*/

		WWW data = new WWW (url, form.data, headers); 

		yield return data;

		if (data.error != null) {
			Debug.Log (data.error);
		}

		/*UnityWebRequest www = UnityWebRequest.Post (url, form);

		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		} else {
			Debug.Log ("Form uplaod complete!");
		}*/
	}

	/*	Obsolete
	 * public IEnumerator SelectData(string username){
		
		WWWForm form = new WWWForm ();
		form.AddField ("username", username);
		WWW data_get = new WWW (url, form);
		yield return data_get;
		Debug.Log (data_get.text);
		if (data_get.error != null) {
			Debug.Log (data_get.error);
		} else if (data_get.text != "") {
			var parsed_data = JSON.Parse (data_get.text).AsArray;
			foreach (var row in parsed_data) {
				this.selectdata.Add (row);
			}
			Debug.Log (selectdata.ToString ());
		}
	}*/

	void OnGUI(){
		if (GUI.Button (new Rect (20, 250, 100, 30), "Get")) {
			StartCoroutine(SendDataGet (username));
		}
		if (GUI.Button (new Rect (150, 250, 100, 30), "Post")) {
			StartCoroutine (SendDataPost (username, password));
		}
	}
}

