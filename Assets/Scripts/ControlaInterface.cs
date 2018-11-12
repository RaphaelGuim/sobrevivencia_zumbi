using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour {
	private ControlaJogador controlaJogador;
	public Slider SliderVidaJogador;
	// Use this for initialization
	void Start () {
		controlaJogador = GameObject.FindWithTag("Player").GetComponent<ControlaJogador>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void AtualizaVida()
	{
		SliderVidaJogador.value = (float)controlaJogador.Vida / controlaJogador.vidaInicial;
	}
}
