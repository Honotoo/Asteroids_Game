using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
	public Text BigText;
	public Text Coordinates;
	public Text Angle;
	public Text Velocity;
	public Text LasersLeft;
	public Text TimeTillLaser;
	public Text PointsText;
	public int HardnessTimer = 0;
	public GameObject button;
		
	
	void Start()
    {	
		BigText.text = " ";
		Coordinates.text = "Coordinates";
		Angle.text = "Angle: ";
		Velocity.text = "Speed: ";
		LasersLeft.text = "Laser beams: ";
		TimeTillLaser.text = "+laser in: ";
		PointsText.text = "Points: ";
		
		button = GameObject.Find ("Button");
		button.SetActive(false); 
	}
	
	public void ButtonPressed()
	{
		BigText.text = " ";
		button.SetActive(false); 
	}
	
	public void BigEvent(int theText)
	{
		button.SetActive(true); 
		BigText.text = "YourScore: "+ theText;
	}
	
	public void PlayerCoordinates(float x,float y,float z)
	{
		Coordinates.text = "x "+Mathf.Round(x).ToString()+" y "+Mathf.Round(y).ToString()+" z "+Mathf.Round(z).ToString();
	}
	
	public void PlayerAngle(float theText)
	{
		Angle.text = "Angle "+Mathf.Round(theText).ToString();
	}
	
	public void PlayerVelocity(float theText)
	{
		Velocity.text = "Speed "+Mathf.Round(theText).ToString();
	}
	
	public void PlayerLasersLeft(int theText)
	{
		LasersLeft.text = "Lasers "+Mathf.Round(theText).ToString();
	}
	
	public void PlayerLaserCooling(float theText)
	{
		print(theText);
		TimeTillLaser.text = "+laser in "+Mathf.Round(theText).ToString();
	}
	
	public void PLayerPoints(int theText)
	{
		PointsText.text = "Points "+theText.ToString();
	}
		
		
		
		
		
		
}
