using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour {
	private Status status;
	public Slider SliderVidaJogador;
	// Use this for initialization
	void Start () {
		status = GameObject.FindWithTag("Player").GetComponent<Status>();
	}
	
	 
	public void AtualizaVida()
	{
		SliderVidaJogador.value = (float)status.Vida / status.VidaInicial;
	}
}
