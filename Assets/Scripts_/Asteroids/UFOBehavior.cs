using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Complete
{
    public class UFOBehavior : MonoBehaviour
    {
	public float m_StartingHealth = 1f;
	public GameObject m_ExplosionPrefab;
	private Collider m_ObjectCollider;
	public float m_Speed = 15f;
	public AudioSource m_ExplosionAudio;
	
	public AudioClip ExplosionSound;
	
	
	
	private ParticleSystem m_ExplosionParticles;
	private float m_CurrentHealth;
	private Vector3 movement;
	
	private Vector3 pos;
	private GameObject[] followme;
	
	private Rigidbody m_Rigidbody;



	private void MakeRotation()
    {
		pos = followme[0].transform.position;
		m_Rigidbody.transform.LookAt(pos);

	}
	
	
	
	
    IEnumerator OnDeath(float delayTimeBefore)
    {
		yield return new WaitForSeconds(delayTimeBefore);
		
		m_ExplosionParticles.transform.position = transform.position;
		m_ExplosionParticles.gameObject.SetActive (true);
		m_ExplosionParticles.transform.parent = null;
		m_ExplosionParticles.Play();
		
		
		//m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource> ();
		
		m_ExplosionAudio.clip = ExplosionSound;
		m_ExplosionAudio.Play();

		ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
		Destroy (m_ExplosionParticles.gameObject, mainModule.duration+9);
    }
	
	

        private void Start ()
        {
		followme = GameObject.FindGameObjectsWithTag ("Player");
		m_ObjectCollider = GetComponent<Collider>();
		m_Rigidbody = GetComponent<Rigidbody> ();
		
		m_ExplosionParticles = Instantiate (m_ExplosionPrefab).GetComponent<ParticleSystem> ();
		//m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource> ();
		m_ExplosionParticles.gameObject.SetActive (false);
		
		m_CurrentHealth = m_StartingHealth;
        }


        private void Update ()
        {
			Vector3 movement = transform.forward * m_Speed * Time.deltaTime;
			m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
			MakeRotation();
        }


        public void TakeDamage (float amount)
        {
            m_CurrentHealth -= amount;
            if (m_CurrentHealth <= 0f)
            {
                StartCoroutine(OnDeath (0.03f));
				Invoke("KillAll", 0.04f);
            }
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