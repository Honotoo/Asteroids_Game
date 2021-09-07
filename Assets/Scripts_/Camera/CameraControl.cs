using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.1f;                 
    public float m_ScreenEdgeBuffer = 7f;           
    public float m_MinSize = 65f;   
	public Transform playerPrefab;


    private Camera m_Camera;                        
    private Vector3 m_MoveVelocity;                 
    private Vector3 m_DesiredPosition;              


    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
		
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
		m_DesiredPosition = playerPrefab.position;
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }



    public void SetStartPositionAndSize()
    {
        transform.position = m_DesiredPosition;
    }
}