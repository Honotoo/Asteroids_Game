using UnityEngine;

namespace Complete
{
    public class playerHealth : MonoBehaviour
    {	
		private GameObject[] GameManager;
		private Rigidbody GameManagerRigidbody;
		private GameManagerScript ManagerScript;
		private Rigidbody m_Rigidbody;
		Collider m_ObjectCollider;
		
        private void Start ()
        {
			m_ObjectCollider = GetComponent<Collider>();
			GameManager = GameObject.FindGameObjectsWithTag ("Manger");
			Rigidbody GameManagerRigidbody = GameManager[0].GetComponent<Rigidbody> ();
			GameManagerScript ManagerScript = GameManagerRigidbody.GetComponent<GameManagerScript>();
			m_Rigidbody = GetComponent<Rigidbody> ();
		}
		
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.tag == "asteroid")
			{
				Destroy (gameObject);
				GameManager = GameObject.FindGameObjectsWithTag ("Manger");
				Rigidbody GameManagerRigidbody = GameManager[0].GetComponent<Rigidbody> ();
				GameManagerScript ManagerScript = GameManagerRigidbody.GetComponent<GameManagerScript>();
				ManagerScript.BigEvent(true);
			}
		}
	}
}