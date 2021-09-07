using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Laser : MonoBehaviour {
	
	public float damage;
	public GameObject TouchingPrefab ;	
	public float TimeUpdate = 1f;
	private LineRenderer lr;
	
	
	private bool Instantiated;
	private Transform positionForTouch;
	
	public AudioSource m_Audio;
	public AudioClip LaserSound;
	
	
	IEnumerator ExampleCoroutine(Transform positionForTouch, float delayTime )
    {
		Instantiated = true;
		GameObject shellInstance = Instantiate (TouchingPrefab, positionForTouch) ;
		yield return new WaitForSeconds(delayTime);
		
		Instantiated = false;
    }
	

	private void Start () 
	{
		lr= GetComponent<LineRenderer>();
		damage = 100f;
		Instantiated = false;
		m_Audio.clip = LaserSound;
		m_Audio.Play();
		
	}

	void FixedUpdate () 
	{
		lr.SetPosition(0, transform.position);
		RaycastHit hit;
		
		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			if (hit.collider)
			{
				lr.SetPosition(1, hit.point);
			}

			Rigidbody targetRigidbody1  = hit.rigidbody;
			
			if (hit.rigidbody != null)
			{
				if (Instantiated == false	)
				{
					
				positionForTouch = hit.transform;
				StartCoroutine(ExampleCoroutine(positionForTouch, TimeUpdate));
				}
			

			}
			

		}else lr.SetPosition(1, transform.forward*5000);
	}
	
	
	
	
	
	
	
	
	
	
}

