using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missil : MonoBehaviour {

	private float velocidade = 10;
	private Inimigo alvo;
	[SerializeField] private int pontosDeDano;


	// Use this for initialization
	void Start() 
	{
		//alvo = GameObject.Find ("Inimigo");
		AutoDestroiDepoisDeSegundos (5);
		
	}
	
	// Update is called once per frame
	void Update () {
		Anda();
		if (GameObject.Find ("Inimigo") != null) {
			AlterarDirecao ();
		}


	}
	private void Anda(){
		Vector3 posicaoAtual = transform.position;
		Vector3 deslocamento = transform.forward * Time.deltaTime * velocidade;
		transform.position = posicaoAtual + deslocamento;
	}
	private void AlterarDirecao(){
		Vector3 posicaoAtual = transform.position;
		Vector3 posicaoDoAlvo = alvo.transform.position;
		Vector3 direcaoDoAlvo = posicaoDoAlvo - posicaoAtual;
		transform.rotation = Quaternion.LookRotation (direcaoDoAlvo);
	}
	void OnTriggerEnter(Collider elementoColidido)
	{
		if (elementoColidido.CompareTag ("Inimigo")) 
		{
			Destroy (this.gameObject);
			Inimigo inimigo = elementoColidido.GetComponent<Inimigo> ();
			inimigo.RecebeDano(pontosDeDano);
		}
	}
	private void AutoDestroiDepoisDeSegundos (float segundos)
	{
		Destroy (this.gameObject, segundos);
	}

	public void DefineAlvo(Inimigo inimigo) {
		alvo = inimigo;    
	}

}