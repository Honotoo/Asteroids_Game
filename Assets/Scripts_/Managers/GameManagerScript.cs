using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Complete
{
    public class GameManagerScript : MonoBehaviour
    {
		public int MaxDifficultyLevel = 50 ;
		public float DifficultyIncreaseSpeed = 0.3f;
		public float spawnGap = 16.0f;
		public float minimumSpawnGap = 2.5f;
		public float TimeBetweenDifficultyIncrease = 20.1f;
		public int Points;
		public int PositionVerticalMax = 200;
		public int PositionHorizontalMax = 400;
		
		public Rigidbody PlayerRigid;
		public GameObject SpawnerObject;
		
		
		private float SpawnTimer = 0.0f;
		private float HardnessTimer = 0.0f;
		private int DifficultyLevel = 0;
		
		public GameObject[] UI;
		private Rigidbody UIRigidbody;
		private UIManager UIScript;
		
		public GameObject[] Enemyes;
		private Rigidbody EnemyesRigidbody;
		private AsteroidBehavior AsteroidBehaviorScript;
		private UFOBehavior UFOBehaviorScript;
		
		
		private Vector3 SpawnPosition;
		private float SpawnPositionX = 0;
		private float SpawnPositionZ = 0;
		private float PlayerX = 0;
		private float PlayerZ = 0;
		
		
        public AudioSource m_Audio;
        public AudioClip MenuMusic;
        public AudioClip GameMusic; 
		
		public AudioSource NotLoopAudio;
        public AudioClip StartMusic; 
        public AudioClip DeadMusic; 
		
		private void AudioMusic (int nowPlay)
        {
            if (nowPlay==0)
            {
                    m_Audio.clip = GameMusic;
                    m_Audio.Play();
            }
            else
            {
                    m_Audio.clip = MenuMusic;
                    m_Audio.Play();
            }
        }
		
		private void Start()
		{			
			UI = GameObject.FindGameObjectsWithTag ("UI");
			Rigidbody UIRigidbody = UI[0].GetComponent<Rigidbody> ();
			UIManager UIScript = UIRigidbody.GetComponent<UIManager>();
			Enemyes = GameObject.FindGameObjectsWithTag("asteroid");
			
		}
		
		
		private void Update()
		{
			HardnessTimer += Time.deltaTime;
			SpawnTimer += Time.deltaTime;
			
			if (DifficultyLevel<=MaxDifficultyLevel){
				if (HardnessTimer >=TimeBetweenDifficultyIncrease)
				{	
					HardnessTimer = 0.0f;
					DifficultyLevel +=1;
					spawnGap -=DifficultyIncreaseSpeed; 
					if (spawnGap<minimumSpawnGap){spawnGap = minimumSpawnGap;}
					
				}
			}
			
			if (SpawnTimer >=spawnGap)
			{	
				MakeEnemySpawn(PlayerX,PlayerZ);
				SpawnTimer = 0.0f;
			}
		}
		
		
        private void MakeEnemySpawn(float x, float z)
        {
			if (x<0.0f)
			{x=x+Random.Range( -50.0f,  -1*(-Mathf.Abs(x)+PositionHorizontalMax)+30.0f);} 
			else
			{x=x+Random.Range( 50.0f,  -1*(-Mathf.Abs(x)+PositionHorizontalMax)-30.0f);}
			if (z<0.0f)
			{z=z+Random.Range( -50.0f, -1*(-Mathf.Abs(x)+PositionVerticalMax)+30.0f);} 
			else
			{z=z+Random.Range( 50.0f,  -1*(-Mathf.Abs(x)+PositionVerticalMax)-30.0f);}

			SpawnPositionX = x*-1;
			SpawnPositionZ = z*-1;
			
			SpawnPosition = new Vector3(SpawnPositionX, 0.0f,  SpawnPositionZ);
			Rigidbody SpawnerRigidbody = SpawnerObject.GetComponent<Rigidbody> ();
			
			SpawnerRigidbody.MovePosition(SpawnPosition);
			EnemySpawnersScript SpawnerScript = SpawnerRigidbody.GetComponent<EnemySpawnersScript>();
			
			
			if (Mathf.Abs(SpawnPositionX)<=PositionHorizontalMax-50)
			{SpawnerScript.DOSPOWN(true);}
			else
			{SpawnerScript.DOSPOWN(false);}
        }
		
		
		private void ClaerEnemyes()
		{
			Enemyes = GameObject.FindGameObjectsWithTag("asteroid");
			
			for (int i = 0; i < Enemyes.Length; i++)
            {
				Rigidbody EnemyesRigidbody = Enemyes[i].GetComponent<Rigidbody> ();
				if (!EnemyesRigidbody)
					continue;
				
				AsteroidBehavior AsteroidBehaviorScript = EnemyesRigidbody.GetComponent<AsteroidBehavior>();
				if (!AsteroidBehaviorScript)
					continue;
				
				AsteroidBehaviorScript.KillAll();
			}
			
			
			Enemyes = GameObject.FindGameObjectsWithTag("asteroid");
			
			for (int i = 0; i < Enemyes.Length; i++)
            {
				Rigidbody EnemyesRigidbody = Enemyes[i].GetComponent<Rigidbody> ();
				if (!EnemyesRigidbody)
					continue;
				
				UFOBehavior UFOBehaviorScript = EnemyesRigidbody.GetComponent<UFOBehavior>();
				if (!UFOBehaviorScript)
					continue;
				
				UFOBehaviorScript.KillAll();
			}
		}
		

		public void onClick()
		{
			PlayerX = 1.0f;
			PlayerZ = 1.0f;
			SpawnTimer = 0.0f;
			HardnessTimer = 0.0f;
			DifficultyLevel = 0;
			Points = 0;
			ClaerEnemyes();
			Rigidbody PlayerINstance = Instantiate (PlayerRigid, SpawnerObject.transform.position, SpawnerObject.transform.rotation) as Rigidbody;
			PlayerINstance.transform.parent = null;
			PlayerINstance.velocity = 15.3f * SpawnerObject.transform.forward; 
			
			
			
			UI = GameObject.FindGameObjectsWithTag ("UI");
			Rigidbody UIRigidbody = UI[0].GetComponent<Rigidbody> ();
			UIManager UIScript = UIRigidbody.GetComponent<UIManager>();
			UIScript.ButtonPressed();
			
			
			NotLoopAudio.clip = StartMusic;
			NotLoopAudio.Play();
			AudioMusic (0);
		}
		
				
		public void BigEvent(bool Destoyed)	
		{
			ClaerEnemyes();
			
			UI = GameObject.FindGameObjectsWithTag ("UI");
			Rigidbody UIRigidbody = UI[0].GetComponent<Rigidbody> ();
			UIManager UIScript = UIRigidbody.GetComponent<UIManager>();
			UIScript.BigEvent(Points);
			NotLoopAudio.clip = DeadMusic;
			NotLoopAudio.Play();
			AudioMusic (1);
			
			
		}
		
		
		public void PlayerCoordinates(float x,float y,float z)
		{
			UI = GameObject.FindGameObjectsWithTag ("UI");
			Rigidbody UIRigidbody = UI[0].GetComponent<Rigidbody> ();
			UIManager UIScript = UIRigidbody.GetComponent<UIManager>();
			UIScript.PlayerCoordinates(x,y,z);
			PlayerX = x;
			PlayerZ = z;
			
			
		}
		public void PlayerAngle(float theText)
		{
			UI = GameObject.FindGameObjectsWithTag ("UI");
			Rigidbody UIRigidbody = UI[0].GetComponent<Rigidbody> ();
			UIManager UIScript = UIRigidbody.GetComponent<UIManager>();
			UIScript.PlayerAngle(theText);
		}
		
		public void PlayerVelocity(float theText)
		{
			UI = GameObject.FindGameObjectsWithTag ("UI");
			Rigidbody UIRigidbody = UI[0].GetComponent<Rigidbody> ();
			UIManager UIScript = UIRigidbody.GetComponent<UIManager>();
			UIScript.PlayerVelocity(theText);
			
		}
		public void PlayerLasersLeft(int theText)
		{
			UI = GameObject.FindGameObjectsWithTag ("UI");
			Rigidbody UIRigidbody = UI[0].GetComponent<Rigidbody> ();
			UIManager UIScript = UIRigidbody.GetComponent<UIManager>();
			UIScript.PlayerLasersLeft(theText);
		}
		
		public void PlayerLaserCooling(float theText)
		{
			UI = GameObject.FindGameObjectsWithTag ("UI");
			Rigidbody UIRigidbody = UI[0].GetComponent<Rigidbody> ();
			UIManager UIScript = UIRigidbody.GetComponent<UIManager>();
			UIScript.PlayerLaserCooling(theText);
		}
		
		
		public void PLayerPoints(int theText)	
		{
			UI = GameObject.FindGameObjectsWithTag ("UI");
			Rigidbody UIRigidbody = UI[0].GetComponent<Rigidbody> ();
			UIManager UIScript = UIRigidbody.GetComponent<UIManager>();
			Points= theText+Points;
			UIScript.PLayerPoints(Points);
		}
		
    }
}