using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Inicio : MonoBehaviour {
	public InputField nickname;
	public identificacion id = new identificacion();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play(){
		StartCoroutine(conect_server());
	}

	IEnumerator conect_server(){
		string dataUrl = "http://192.168.0.14:8000/jugador/api/jugador/";
		// Create a form object for sending high score data to the server
		var form = new WWWForm();
		// Assuming the perl script manages high scores for different games
		form.AddField( "nombre", nickname.text );

		// Create a download object
		WWW downloadW = new WWW( dataUrl, form );

		// Wait until the download is done
		yield return downloadW;


		if(!string.IsNullOrEmpty(downloadW.error)) {
			Debug.Log( "Error downloading: " + downloadW.error );
		} else {
			id = JsonUtility.FromJson<identificacion>(downloadW.text);
			info.id = id.id;
			info.sala = id.sala_id;
			info.nickname = nickname.text;
			SceneManager.LoadScene("principal");
		}
	}
}
