﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Hero : MonoBehaviour {

	public Rigidbody2D rb;
	public string id;
	public ubicacion ubi = new ubicacion();
	private const float max_magnitude = 54.0f;
	private const float min_magnitude = 51.0f;
	private float speed = 10.0f;
	private const float tiempo_espera = 0.1f;
	private float tiempo_transcurrido = 0.0f;
	public GameObject principal;
	public GameObject m_camaraPrincipal;
	public GameObject planeta;
	private string json_bytes;
	public float x = 0f;
	public float y = 0f;
	private float tiempo_paso = 0.0f;
	private float delta_angulo;
	private float delta_magnitud;
	private ubicacion ubicacion_paso = new ubicacion();
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		id = info.id;
	}

	// Update is called once per frame
	void Update () {
		tiempo_transcurrido = tiempo_transcurrido + Time.deltaTime;
		tiempo_paso = tiempo_paso + Time.deltaTime;
		Principal info_base = principal.GetComponent<Principal>();

		if(info_base.heros.ContainsKey(id) && info_base.juego_iniciado && info_base.heros[id].reloj <= ubi.reloj){
			delta_angulo = (ubi.angulo - info_base.heros[id].angulo)*0.25f;
			delta_magnitud = (ubi.magnitud - info_base.heros[id].magnitud)*0.25f;
			ubicacion_paso = info_base.heros[id];
		}

		if(tiempo_paso > 0.025f && info_base.juego_iniciado){
			ubicacion_paso.angulo = ubicacion_paso.angulo + delta_angulo;
			ubicacion_paso.magnitud = ubicacion_paso.magnitud + delta_magnitud;
			tiempo_paso = 0.0f;
		}

		if(info_base.heros.ContainsKey(id) && info_base.juego_iniciado){
			y = Mathf.Cos(ubicacion_paso.angulo*Mathf.PI/180) * ubicacion_paso.magnitud;
			x = -Mathf.Sin(ubicacion_paso.angulo*Mathf.PI/180) * ubicacion_paso.magnitud;
			transform.position = new Vector3(x, y, -0.5f);
			float angle = (planeta.transform.position.x < transform.position.x)?
						360 - Vector3.Angle(Vector2.up, transform.position):
						Vector3.Angle(Vector2.up, transform.position);
			transform.eulerAngles = new Vector3(0, 0, angle);

			m_camaraPrincipal.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
			m_camaraPrincipal.transform.rotation = transform.rotation;
		}

		if(info_base.heros.ContainsKey(id) && !info_base.juego_iniciado){
			ubi = info_base.heros[id];
		}
		/*
		//////////////////////////////
		//// Aquí se descibe el control de hero dependiendo del dispositivo
		/// Este es un ejemplo para pc:
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.localScale = new Vector3(-5.0f, 5.0f, 1.0f);
			ubi.angulo = ubi.angulo + speed*Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);
			ubi.angulo = ubi.angulo - speed*Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.UpArrow)){
			ubi.magnitud = ubi.magnitud + speed*Time.deltaTime/2;
			if(ubi.magnitud > max_magnitude){
				ubi.magnitud = max_magnitude;
			}
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			ubi.magnitud = ubi.magnitud - speed*Time.deltaTime/2;
			if(ubi.magnitud < min_magnitude){
				ubi.magnitud = min_magnitude;
			}
		}
		*/
		if(tiempo_transcurrido > tiempo_espera && info_base.juego_iniciado){
			ubi.reloj = info_base.m_reloj;
			json_bytes = JsonUtility.ToJson(ubi);
			byte[] bytes = Encoding.ASCII.GetBytes(json_bytes);
			info_base._websocket.Send(bytes);
			tiempo_transcurrido = 0.0f;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
	}
}