using UnityEngine;
using System.Collections;
namespace Complete
{
    public class ShellExplosion : MonoBehaviour
    {
        public LayerMask m_TankMask;
        public ParticleSystem m_ExplosionParticles;
        public AudioSource m_ExplosionAudio;
        public float m_MaxDamage = 100f;
        public float m_MaxLifeTime = 30f;
        Collider m_ObjectCollider;
		public float m_ExplosionRadius = 3f;
		public int delayTime = 4;
		[SerializeField] private LayerMask m_Layers;
		
		
        private void Start ()	
        {
			m_ObjectCollider = GetComponent<Collider>();
            Invoke("destroyy", m_MaxLifeTime);
        }
		
		IEnumerator ExampleCoroutine(Collider other, float ExplosionRadius, int delayTimeBefore, int delayTimeAfter )
		{
			yield return new WaitForSeconds(delayTimeBefore);
			
			Collider[] colliders = Physics.OverlapSphere (transform.position, ExplosionRadius, m_TankMask);
			
			for (int i = 0; i < colliders.Length; i++)
			{
				Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();
				if (!targetRigidbody)
					continue;
				AsteroidBehavior targetHealth1 = targetRigidbody.GetComponent<AsteroidBehavior>();
				if (!targetHealth1)
					continue;
				targetHealth1.TakeDamage (m_MaxDamage);
			}
			
			
			for (int i = 0; i < colliders.Length; i++)
			{
				Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();
				
				if (!targetRigidbody)
					continue;
				
				UFOBehavior targetHealth = targetRigidbody.GetComponent<UFOBehavior>();
				
				if (!targetHealth)
					continue;
				
				targetHealth.TakeDamage (m_MaxDamage);
			}
				
			yield return new WaitForSeconds(delayTimeAfter);
		}
	
		
        private void OnTriggerEnter (Collider other)
        {	
			StartCoroutine(ExampleCoroutine(other, 2f, 0,delayTime));
			m_ObjectCollider.isTrigger = false;	
			
			Invoke("destroyy", delayTime);
			StartCoroutine(ExampleCoroutine(other, m_ExplosionRadius,delayTime, 0));
		}
		
		private void destroyy()
		{	
            m_ExplosionParticles.transform.parent = null;
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
			
            ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
            Destroy (m_ExplosionParticles.gameObject, mainModule.duration+9);
            Destroy (gameObject);
		}
	}
}