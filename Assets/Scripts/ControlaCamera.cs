using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour {
	public GameObject jogador;
	Vector3 distancia;
	// Use this for initialization
	void Start () {
		distancia = transform.position - jogador.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = jogador.transform.position + distancia;
	}
}
