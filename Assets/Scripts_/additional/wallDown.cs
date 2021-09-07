using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class wallDown : MonoBehaviour
{
	public int gap =7;
	private Rigidbody m_Rigidbody;
	private Vector3 itsPosition;
	
	Collider m_ObjectCollider;
	private Vector3 collidedPosition;
	
	
    void Start()
    {
		
		m_Rigidbody = GetComponent<Rigidbody> ();
        itsPosition = m_Rigidbody.position;
		
		m_ObjectCollider = GetComponent<Collider>();
			
    }




	
	
	private void OnCollisionEnter(Collision collision)
	{
		Rigidbody collided = collision.gameObject.GetComponent<Rigidbody>();
		
		Vector3 collidedPosition = new Vector3(collided.position[0], collided.position[1],  -gap+Math.Abs(itsPosition[2]));		
		 
		collided.MovePosition(collidedPosition);

		
		
		
	}
}
