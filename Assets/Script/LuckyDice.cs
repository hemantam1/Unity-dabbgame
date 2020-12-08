using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckyDice : MonoBehaviour
{
	int diceCt2;
	public Image diceImg;
	public Image[] threeDice;
	public Sprite[] diceAllImage;
	public Sprite[] OneToSix;
	public int[] valSave;
	int PressCount;


	void Start ()
	{
		valSave = new int[3];
		PressCount = 0;
	}

	public void PlayNow ()
	{
		
		if (PressCount != 3) {
			PressCount++;
			StartCoroutine ("Multi_PlayDiceAnim");
		}
	}


	IEnumerator Multi_PlayDiceAnim ()
	{
		yield return new WaitForSeconds (0.05f);

		if (diceCt2 == 8) {
			diceCt2 = 0;

			int Rnd = Random.Range (0, 6);
			threeDice [PressCount - 1].sprite = OneToSix [Rnd];

			diceImg.sprite = OneToSix [Rnd];
			valSave [PressCount - 1] = Rnd;


			StopCoroutine (Multi_PlayDiceAnim ());
		} else {
			
			diceImg.sprite = diceAllImage [diceCt2];
			threeDice [PressCount - 1].sprite = diceAllImage [diceCt2];
			diceCt2++;
			StartCoroutine (Multi_PlayDiceAnim ());

		}
	}
}
