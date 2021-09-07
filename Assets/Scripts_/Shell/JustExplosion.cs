using UnityEngine;
using System.Collections;
namespace Complete
{
    public class JustExplosion : MonoBehaviour
    {
        public LayerMask Mask;
        public ParticleSystem m_ExplosionParticles;
        public AudioSource m_ExplosionAudio;
        public float m_MaxDamage = 100f;
        public float m_MaxLifeTime = 1f;
		public float m_ExplosionRadius = 1f;
        Collider m_ObjectCollider;
		
        private void Start ()	
        {
			m_ObjectCollider = GetComponent<Collider>();
			m_MaxLifeTime = 1f;
            Invoke("destroyy", m_MaxLifeTime);
        }

		IEnumerator ExampleCoroutine(Collider other, float ExplosionRadius, int delayTimeBefore, int delayTimeAfter )
		{
			yield return new WaitForSeconds(delayTimeBefore);
			Collider[] colliders = Physics.OverlapSphere (transform.position, ExplosionRadius, Mask);
			
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
				StartCoroutine(ExampleCoroutine(other, m_ExplosionRadius, 0,0));
			}

			private void destroyy()
			{	
				Destroy (gameObject);
			}
	}
}