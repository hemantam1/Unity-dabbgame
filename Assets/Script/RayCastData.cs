using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RayCastData : MonoBehaviour
{
	public GameObject[] ChooseLevel;
	public GameObject[] Buttons;

	public Vector2 PanelScale;

	// Update is called once per frame
	void Update ()
	{
		RaycastHit2D hit = Physics2D.Raycast (this.transform.position, Vector2.up, 10000.0f);


		if (hit != null && hit.collider != null) {
//			print ("Hit : " + hit.collider.gameObject.tag);
			if (hit.collider.gameObject.CompareTag ("1VS1")) {
				ChooseLevel [0].gameObject.transform.DOScale (new Vector3 (PanelScale.x, PanelScale.y, 1f), 0.5f);
				ChooseLevel [1].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [2].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [3].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [4].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
//				Buttons [0].transform.DOLocalMoveY (this.transform.position.y - 0.5f, 0, false);
//				Buttons [1].transform.DOLocalMoveY (this.transform.position.y - 0.5f, 0, false);
//				Buttons [2].transform.DOLocalMoveY (this.transform.position.y - 0.5f, 0, false);
//				Buttons [3].transform.DOLocalMoveY (this.transform.position.y - 0.5f, 0, false);
//				Buttons [4].transform.DOLocalMoveY (this.transform.position.y - 0.5f, 0, false);
			}
			if (hit.collider.gameObject.CompareTag ("2VS2")) {
				ChooseLevel [1].gameObject.transform.DOScale (new Vector3 (PanelScale.x, PanelScale.y, 1f), 0.5f);
				ChooseLevel [0].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [2].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [3].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [4].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
			}
			if (hit.collider.gameObject.CompareTag ("4VS4")) {
				ChooseLevel [2].gameObject.transform.DOScale (new Vector3 (PanelScale.x, PanelScale.y, 1f), 0.5f);
				ChooseLevel [1].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [0].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [3].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [4].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
			}
			if (hit.collider.gameObject.CompareTag ("PrivateVSPrivate")) {
				ChooseLevel [3].gameObject.transform.DOScale (new Vector3 (PanelScale.x, PanelScale.y, 1f), 0.5f);
				ChooseLevel [1].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [2].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [0].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [4].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
			}
			if (hit.collider.gameObject.CompareTag ("OfflineVSOffline")) {
				ChooseLevel [4].gameObject.transform.DOScale (new Vector3 (PanelScale.x, PanelScale.y, 1f), 0.5f);
				ChooseLevel [1].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [2].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [3].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
				ChooseLevel [0].gameObject.transform.DOScale (new Vector3 (0.8f, 1f, 1f), 0.5f);
			}
		}

	}
}
