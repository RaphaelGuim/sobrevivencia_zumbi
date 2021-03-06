﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Bala : MonoBehaviour
{

	Rigidbody Rb;
	public float Velocidade;
	float time = 0;
	public int DanoTiro = 1;
	private void Start()
	{
		Rb = GetComponent<Rigidbody>();
	}
	private void FixedUpdate()
	{

		Rb.MovePosition((Rb.position + transform.forward * Velocidade * Time.deltaTime));
		time += Time.deltaTime;
		if (time > 3)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == Tags.Inimigo)
		{
			Quaternion direcao = Quaternion.LookRotation(-transform.forward);
			IMatavel imatavel = other.GetComponent<IMatavel>();
			imatavel.TomarDano(DanoTiro);
			imatavel.Sangrar(transform.position, direcao);

		}
		Destroy(gameObject);

	}
}
 
