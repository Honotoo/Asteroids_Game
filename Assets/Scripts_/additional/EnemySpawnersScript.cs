using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnersScript : MonoBehaviour
{

	public GameObject ToSpawnPrefab1;
	public GameObject ToSpawnPrefab2;
	private Rigidbody ToSpawnPrefabRigidBody1;
	private Rigidbody ToSpawnPrefabRigidBody2;
	
	private Rigidbody m_Rigidbody;
	

    void Start()
    {
		m_Rigidbody = GetComponent<Rigidbody> ();
		ToSpawnPrefabRigidBody1 = ToSpawnPrefab1.GetComponent<Rigidbody> ();
		ToSpawnPrefabRigidBody2 = ToSpawnPrefab2.GetComponent<Rigidbody> ();
		
    }



	public void DOSPOWN(bool asteroidUFO)
	{	
	
	
		if (asteroidUFO==true)
		{
			Rigidbody AsteroidInstance1 = Instantiate (ToSpawnPrefabRigidBody1, m_Rigidbody.transform.position, m_Rigidbody.transform.rotation) as Rigidbody;
			AsteroidInstance1.transform.parent = null;

		} else
		{
			Rigidbody AsteroidInstance2 = Instantiate (ToSpawnPrefabRigidBody2, m_Rigidbody.transform.position, m_Rigidbody.transform.rotation) as Rigidbody;
			AsteroidInstance2.transform.parent = null;
		}
	}

	
	

	
	
	
	
}
