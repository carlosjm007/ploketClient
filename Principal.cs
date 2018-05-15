using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;
using System.Text;

public class Principal : MonoBehaviour {
	public WebSocket	_websocket;
	public identificacion id = new identificacion();
	private string url;
	public ubicacion ubicacion_hero;
	public server m_server;
	public Dictionary<string, ubicacion> heros = new Dictionary<string, ubicacion>();
	public bool juego_iniciado = false;
	public Text cuenta_regresiva;
	private string text = "10";
	private const string start = "¡Start!";
	public float m_reloj = 0;
	float m_duracion = 2.0f;
	public GameObject m_enemy;
	public GameObject planeta;
	public GameObject control_touch;
	private Dictionary<string, GameObject> instancia_enemy = new Dictionary<string, GameObject>();
	public Configuracion dispositivo = new Configuracion();
	// Use this for initialization
	void Start () {
		conect_server();
		if(dispositivo.android){
			control_touch.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (text == start){
			m_reloj += Time.deltaTime;
		}else{
			foreach(KeyValuePair<string, ubicacion> entry in heros){
				if(!instancia_enemy.ContainsKey(entry.Key) && entry.Key != info.id){
					GameObject enemigo = Instantiate(m_enemy, new Vector3(0.0f,50.0f, -0.5f), Quaternion.identity) as GameObject;
					instancia_enemy.Add(entry.Key, enemigo);
					Enemy id_m_enemy = instancia_enemy[entry.Key].GetComponent<Enemy>();
					id_m_enemy.id = entry.Key;
					id_m_enemy.principal = gameObject;
					id_m_enemy.planeta = planeta;
					id_m_enemy.ubi = entry.Value;
				}
			}
		}
		if(m_reloj<m_duracion){
			cuenta_regresiva.text = text;
		}else{
			cuenta_regresiva.enabled = false;
		}
	}

	private void configure (WebSocket socket)
	{
		socket.OnOpen += (sender, e) =>{
		};

		socket.OnMessage += (sender, e) => {
			if (e.IsText) {
				ubicacion_hero = JsonUtility.FromJson<ubicacion>(e.Data);
			}else{				
				string texto = Encoding.ASCII.GetString(e.RawData);
				ubicacion_hero = JsonUtility.FromJson<ubicacion>(texto);
			}
			if (ubicacion_hero.id != "Server"){
				if(heros.ContainsKey(ubicacion_hero.id)){
					heros[ubicacion_hero.id] = ubicacion_hero;
				}else{
					heros.Add(ubicacion_hero.id, ubicacion_hero);
				}
			}else{
				m_server = JsonUtility.FromJson<server>(e.Data);
				mensaje_server(m_server);
			}
		};

		socket.OnClose += (sender, e) =>
			Debug.Log("Se cerró");
	}

	private void conect_server(){
		url = "ws://192.168.0.14:8000/live/" + info.sala + "/" + info.id + "/";
		_websocket = new WebSocket (url);
		configure (_websocket);
		_websocket.Connect ();
	}

	private void mensaje_server(server s){
		text = s.cuenta.ToString();
		if(s.cuenta == 0){
			juego_iniciado = true;
			text = start;
		}
	}
}