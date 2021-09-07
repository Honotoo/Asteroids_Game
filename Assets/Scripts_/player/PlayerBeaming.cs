using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Complete
{
	public class PlayerBeaming : MonoBehaviour
	{
		public GameObject m_LaserPrefab;
		public Transform m_FireTransform; 
		public float BeamingTime;
		public int LasersOnBoard = 9;
		
		public float NewLaserIn = 9.0f;
		private float LaserTimer;
		
		private Transform m_LaserTransform;
		private string m_FireButton;
		
		private bool Beaming;
			
		private GameObject[] GameManager;
		private Rigidbody GameManagerRigidbody;
		private GameManagerScript ManagerScript;
		
		
		private void Start ()
		{
			GameManager = GameObject.FindGameObjectsWithTag ("Manger");
			Rigidbody GameManagerRigidbody = GameManager[0].GetComponent<Rigidbody> ();
			GameManagerScript ManagerScript = GameManagerRigidbody.GetComponent<GameManagerScript>();
			
			LaserTimer = NewLaserIn;
			m_FireButton = "Fire2";
			Beaming = false;
		}


			private void Update ()
			{	
				LaserTimer -= Time.deltaTime;
				
				GameManager = GameObject.FindGameObjectsWithTag ("Manger");
				Rigidbody GameManagerRigidbody = GameManager[0].GetComponent<Rigidbody> ();
				GameManagerScript ManagerScript = GameManagerRigidbody.GetComponent<GameManagerScript>();
				ManagerScript.PlayerLasersLeft(LasersOnBoard);
				ManagerScript.PlayerLaserCooling(LaserTimer);
				
				if (LaserTimer<0.0f)
				{
					LaserTimer = NewLaserIn;
					LasersOnBoard+=1;	
				}
				
				
				
				if (Input.GetButtonDown(m_FireButton) && !Beaming && LasersOnBoard>0 )
				{
					m_LaserTransform = Instantiate (m_LaserPrefab).GetComponent<Transform>();
					Beaming = true;
					Invoke("OnBeaming", BeamingTime);
				}
				else if (Beaming)
				{
					m_LaserTransform.transform.position = m_FireTransform.position;
					m_LaserTransform.transform.rotation = m_FireTransform.rotation;
				}
			}
			
		private void OnBeaming()
		{	
			LasersOnBoard -=1;
			Beaming = false;
			Destroy (m_LaserTransform.gameObject);
		}
	}
}