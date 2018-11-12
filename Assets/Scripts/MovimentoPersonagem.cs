using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour {
	Rigidbody rb;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void Movimentar(Vector3 direcao, float Velocidade)
	{
		rb.MovePosition(rb.position + direcao * Time.deltaTime * Velocidade);
	}
}
