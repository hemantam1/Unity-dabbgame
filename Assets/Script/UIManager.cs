using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;


	void Awake ()
	{
		instance = this;
	}

	public GameManager gameManager;
	public Select_Environment selectEnvironment;

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}
}
