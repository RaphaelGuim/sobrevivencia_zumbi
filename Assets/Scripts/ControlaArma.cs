using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour {

	public GameObject Bala;
	public GameObject Cano;
	public AudioClip somTiro;
	private int Balas = 20;
	private ControlaInterface controlaInterface;
	// Use this for initialization
	void Start () {
		controlaInterface = GameObject.FindObjectOfType<ControlaInterface>();
	}
	
	public void AdicionaBalas(int bala)
	{
		Balas += bala;
		controlaInterface.AtualizarQuantidadeBalas(Balas);

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1") && Balas > 0)
		{
			ControlaAudio.instancia.PlayOneShot(somTiro);
			Instantiate(Bala, Cano.transform.position, Cano.transform.rotation);
			Balas--;
			controlaInterface.AtualizarQuantidadeBalas(Balas);
		}
	}
}
