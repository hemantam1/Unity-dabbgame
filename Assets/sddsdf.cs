using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sddsdf : MonoBehaviour
{

	void Start ()
	{
		float xx = transform.position.x - 25;
		transform.position = new Vector2 (xx, transform.position.y);
//		transform.position.x -= 25;
	}

	void Update ()
	{
		
	}
}
