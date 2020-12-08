using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RayCastData : MonoBehaviour
{
    public GameObject[] ChooseLevel;
    public GameObject[] Buttons;

    public Vector2 PanelScale;

    private Collider2D m_lastHit = null;
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.up, 10000.0f);

        if (hit && hit.collider != null)
        {
            //			print ("Hit : " + hit.collider.gameObject.tag);

            if (hit.collider == m_lastHit)
                return;

            m_lastHit = hit.collider;

            string s = hit.collider.gameObject.tag;

            if (s.Equals("1VS1"))
                ApplyTo(0);

            if (s.Equals("2VS2"))
                ApplyTo(1);

            if (s.Equals("4VS4"))
                ApplyTo(2);

            if (s.Equals("PrivateVSPrivate"))
                ApplyTo(3);

            if (s.Equals("OfflineVSOffline"))
                ApplyTo(4);

        }

    }
    const float k_Speed = 0.5f;
    readonly Vector3 k_RestScale = new Vector3(0.8f, 1f, 1f);
    void ApplyTo(int index)
    {
        for (int i = 0; i < ChooseLevel.Length; i++)
        {
            if(i == index)
            {
                ChooseLevel[i].transform.DOScale(new Vector3(PanelScale.x, PanelScale.y, 1f), k_Speed);
                continue;
            }

            ChooseLevel[i].transform.DOScale(k_RestScale, k_Speed);
        }
    }
}
