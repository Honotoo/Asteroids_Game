using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class PlayerShooting : MonoBehaviour
    {
        public Rigidbody m_Shell;
        public Transform m_FireTransform;
		public AudioSource m_ShootingAudio;
		
        public AudioClip m_FireClip;
		public float m_CurrentLaunchForce = 15f;
		
        private string m_FireButton;
		
        private void Start ()
        {
            m_FireButton = "Fire1";


        }


        private void Update ()
        {
            if (Input.GetButtonUp (m_FireButton) )
            {
            Rigidbody shellInstance = Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 
			
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play ();
            }
        }
    }
}