using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Complete
{
    public class AsteroidBehavior : MonoBehaviour
    {
        public float m_StartingHealth = 1f;
		public GameObject m_ExplosionPrefab;
        public Rigidbody AsteroidSmaler;
		Collider m_ObjectCollider;
        public Transform PlaceForAsteroidSmaler1;
        public Transform PlaceForAsteroidSmaler2; 
        private ParticleSystem m_ExplosionParticles;
        private float m_CurrentHealth;
		private Vector3 movement;
		
		private Vector3 m_EulerAngleVelocity;
		
        private GameObject[] GameManager;
		private Rigidbody GameManagerRigidbody;
		private GameManagerScript ManagerScript;
        private Rigidbody m_Rigidbody;
		
		
		
		
		private GameObject MeshInside1;
		private GameObject MeshInside2;
		private GameObject MeshInside3;

    IEnumerator OnDeath(float delayTimeBefore)
    {
		yield return new WaitForSeconds(delayTimeBefore);
		
		m_ExplosionParticles.transform.position = transform.position;
		m_ExplosionParticles.gameObject.SetActive (true);

		m_ExplosionParticles.transform.parent = null;

		m_ExplosionParticles.Play();


		ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
		Destroy (m_ExplosionParticles.gameObject, mainModule.duration+7);
    }
	
	
        private void Start ()
        {
			m_ObjectCollider = GetComponent<Collider>();
			m_Rigidbody = GetComponent<Rigidbody> ();
			
			GameManager = GameObject.FindGameObjectsWithTag ("Manger");
			Rigidbody GameManagerRigidbody = GameManager[0].GetComponent<Rigidbody> ();
			GameManagerScript ManagerScript = GameManagerRigidbody.GetComponent<GameManagerScript>();
			
			m_EulerAngleVelocity = new Vector3(Random.Range( -2f, 2f), Random.Range( -2f, 2f), Random.Range( -2f, 2f));
			Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
			m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
            m_ExplosionParticles = Instantiate (m_ExplosionPrefab).GetComponent<ParticleSystem> ();

            m_ExplosionParticles.gameObject.SetActive (false);
			m_CurrentHealth = m_StartingHealth;
			
			
			
			
			MeshInside1 = GameObject.Find("SM_Env_Astroid_Large_Holes_02 Variant");
			MeshInside2 = GameObject.Find("SM_Env_Astroid_Large_Holes_01 Variant (1)");
			MeshInside3 = GameObject.Find("SM_Env_Astroid_Holes_03 Variant");
			if (MeshInside1!=null)
			{movement = new Vector3(Random.Range( -0.055f,  0.055f), 0.0f,  Random.Range( -0.055f,  0.055f));}
			else if (MeshInside2!=null)
			{movement = new Vector3(Random.Range( -0.095f,  0.095f), 0.0f,  Random.Range( -0.095f,  0.095f));}
			else 
			{movement = new Vector3(Random.Range( -0.225f,  0.225f), 0.0f,  Random.Range( -0.225f,  0.225f));}
			
			
        }


        private void Update ()
        {
			

			m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

			
			Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
			m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);

        }


        public void TakeDamage (float amount)
        {
            m_CurrentHealth -= amount;
			
            if (m_CurrentHealth <= 0f)
            {
                StartCoroutine(OnDeath (0.001f));
				Invoke("destroyy", 0.005f);
            }
        }

		private void destroyy()
		{
			
			GameManager = GameObject.FindGameObjectsWithTag ("Manger");
			Rigidbody GameManagerRigidbody = GameManager[0].GetComponent<Rigidbody> ();
			
			GameManagerScript ManagerScript = GameManagerRigidbody.GetComponent<GameManagerScript>();
			
			ManagerScript.PLayerPoints(1); 
			if (AsteroidSmaler!=null){
				Rigidbody AsteroidInstance1 = Instantiate (AsteroidSmaler, PlaceForAsteroidSmaler1.position, PlaceForAsteroidSmaler1.rotation) as Rigidbody;
				AsteroidInstance1.transform.parent = null;
				
				
				Rigidbody AsteroidInstance2 = Instantiate (AsteroidSmaler, PlaceForAsteroidSmaler2.position, PlaceForAsteroidSmaler2.rotation) as Rigidbody;
				AsteroidInstance2.transform.parent = null;
			}
			Destroy (gameObject);  
		}


		public void	KillAll()
		{
			Destroy (gameObject); 
		}



		private void OnCollisionEnter(Collision collision)
		{
			
			if (collision.gameObject.tag == "asteroid")
			{
			  Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
			}
			
		}
		
		
		
		
		
		
		

    }
}