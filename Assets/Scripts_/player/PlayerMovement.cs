using UnityEngine;
using System;
namespace Complete
{
    public class PlayerMovement : MonoBehaviour
    {
		public float m_Speed = 30f; 
        public float m_TurnSpeed = 210f; 
        private string m_MovementAxisName; 
        private string m_TurnAxisName;  
        private Rigidbody m_Rigidbody;
		
        private float m_MovementInputValue;
        private float m_TurnInputValue;  
		private float StraifValue;
		
        private ParticleSystem[] m_particleSystems; 
		private Vector3 movement;
		private GameObject fire;
		
		private GameObject[] GameManager;
		private Rigidbody GameManagerRigidbody;
		private GameManagerScript ManagerScript;
		
		
		
        private void Start ()
        {
			movement = new Vector3(0.0f, 0.0f,  0.0f);
			
			fire = GameObject.Find("FX_Flame_Booster_Square");
			fire.SetActive(true);
			m_Rigidbody = GetComponent<Rigidbody> ();
			
			
            m_MovementInputValue = Input.GetAxis ("Vertical1");
            m_TurnInputValue = Input.GetAxis ("Horizontal1");
			StraifValue = Input.GetAxis ("StraifUI");

			GameManager = GameObject.FindGameObjectsWithTag ("Manger");
			Rigidbody GameManagerRigidbody = GameManager[0].GetComponent<Rigidbody> ();
			GameManagerScript ManagerScript = GameManagerRigidbody.GetComponent<GameManagerScript>();
        }
		
		private void ChangeMovement()
		{
			
			if ( m_MovementInputValue > 0)
			{
			m_Rigidbody.AddRelativeForce(Vector3.forward * m_Speed);
			fire.SetActive(true);
			
			} else if (m_MovementInputValue < 0) 
			{
				fire.SetActive(false);
			} else 
			{
				fire.SetActive(false);
				m_Rigidbody.drag = 0.1f;
			}
			
			GameManager = GameObject.FindGameObjectsWithTag ("Manger");
			Rigidbody GameManagerRigidbody = GameManager[0].GetComponent<Rigidbody> ();
			GameManagerScript ManagerScript = GameManagerRigidbody.GetComponent<GameManagerScript>();
			ManagerScript.PlayerVelocity(m_Rigidbody.velocity[0]);
			ManagerScript.PlayerAngle(Quaternion.Angle(transform.rotation, GameManagerRigidbody.rotation));
			ManagerScript.PlayerCoordinates(m_Rigidbody.position[0],m_Rigidbody.position[1],m_Rigidbody.position[2]);
		}
		
		private void Turn ()
        {
			float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
			
			if ( m_TurnInputValue > 0)
			{
				Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
				m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
			} else if (m_TurnInputValue < 0) 
			{
				Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
				m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
			} else 
			{
				Quaternion turnRotation = Quaternion.Euler (0f, 0f, 0f);
				m_Rigidbody.MoveRotation (m_Rigidbody.rotation);
			}
        }
		
		

		
        private void Update ()
        {
            m_MovementInputValue = Input.GetAxis ("Vertical1");
            m_TurnInputValue = Input.GetAxis ("Horizontal1");
			StraifValue = Input.GetAxis ("StraifUI");
        }

		
		
		
        private void FixedUpdate ()
        {
            ChangeMovement();
            Turn ();
        }
    }
}