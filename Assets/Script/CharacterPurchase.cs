using SaveConstantVal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterPurchase : MonoBehaviour
{
    public static CharacterPurchase instance;
    public Sprite[] PinCharImg;
    [HideInInspector] public GameObject BlastParticle;
    [HideInInspector] public GameObject[] Char_Purcgase_bt;
    public Sprite[] AllImageChar;
    [HideInInspector] public Image PlayerImg;
    [HideInInspector] public GameObject[] tickMark;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //==== check character already purchase or not ========

        if (PlayerPrefs.GetInt(ApiConstant.chrPur1) == 1)
        {
            Char_Purcgase_bt[0].SetActive(false);
        }
        if (PlayerPrefs.GetInt(ApiConstant.chrPur2) == 1)
        {
            Char_Purcgase_bt[1].SetActive(false);
        }
        if (PlayerPrefs.GetInt(ApiConstant.chrPur3) == 1)
        {
            Char_Purcgase_bt[2].SetActive(false);
        }
        if (PlayerPrefs.GetInt(ApiConstant.chrPur4) == 1)
        {
            Char_Purcgase_bt[3].SetActive(false);
        }
        if (PlayerPrefs.GetInt(ApiConstant.chrPur5) == 1)
        {
            Char_Purcgase_bt[4].SetActive(false);
        }
        if (PlayerPrefs.GetInt(ApiConstant.chrPur6) == 1)
        {
            Char_Purcgase_bt[5].SetActive(false);
        }

        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
        {
            switch (PlayerPrefs.GetInt(ApiConstant.Chr_Apply))
            {
                case 1:
                    PlayerImg.sprite = AllImageChar[0];
                    CloseALlRickMark(0);
                    break;
                case 2:
                    PlayerImg.sprite = AllImageChar[1];
                    CloseALlRickMark(1);
                    break;
                case 3:
                    PlayerImg.sprite = AllImageChar[2];
                    CloseALlRickMark(2);
                    break;
                case 4:
                    PlayerImg.sprite = AllImageChar[3];
                    CloseALlRickMark(3);
                    break;
                case 5:
                    PlayerImg.sprite = AllImageChar[4];
                    CloseALlRickMark(4);
                    break;
                case 6:
                    PlayerImg.sprite = AllImageChar[5];
                    CloseALlRickMark(6);
                    break;

            }
        }
    }

    public void CloseALlRickMark(int val)
    {
        for (int i = 0; i < 6; i++)
        {
            tickMark[i].SetActive(false);
        }
        tickMark[val].SetActive(true);
    }

    //===== character purchase button  =================


    public void BuyChar_1()
    {
        //		OpenIABTest.instance.ProductsPurchase (ApiConstant.chrPur1);
        print("Button Pressed Charcater 1");

        StartCoroutine(Blast_Effect());
        PlayerPrefs.SetInt(ApiConstant.chrPur1, 1);
        Char_Purcgase_bt[0].SetActive(false);
    }

    public void BuyChar_2()
    {
        //		OpenIABTest.instance.ProductsPurchase (ApiConstant.chrPur2);
        print("Button Pressed Charcater 2");
        StartCoroutine(Blast_Effect());
        PlayerPrefs.SetInt(ApiConstant.chrPur2, 1);
        Char_Purcgase_bt[1].SetActive(false);
    }

    public void BuyChar_3()
    {
        //		OpenIABTest.instance.ProductsPurchase (ApiConstant.chrPur3);

        print("Button Pressed Charcater 3");
        StartCoroutine(Blast_Effect());
        PlayerPrefs.SetInt(ApiConstant.chrPur3, 1);
        Char_Purcgase_bt[2].SetActive(false);
    }

    public void BuyChar_4()
    {
        //		OpenIABTest.instance.ProductsPurchase (ApiConstant.chrPur4);

        print("Button Pressed Charcater 4");
        StartCoroutine(Blast_Effect());
        PlayerPrefs.SetInt(ApiConstant.chrPur4, 1);
        Char_Purcgase_bt[3].SetActive(false);
    }

    public void BuyChar_5()
    {
        //		OpenIABTest.instance.ProductsPurchase (ApiConstant.chrPur5);
        print("Button Pressed Charcater 5");
        StartCoroutine(Blast_Effect());
        PlayerPrefs.SetInt(ApiConstant.chrPur5, 1);
        Char_Purcgase_bt[4].SetActive(false);
    }

    public void BuyChar_6()
    {
        //		OpenIABTest.instance.ProductsPurchase (ApiConstant.chrPur6);

        print("Button Pressed Charcater 6");
        StartCoroutine(Blast_Effect());
        PlayerPrefs.SetInt(ApiConstant.chrPur6, 1);
        Char_Purcgase_bt[5].SetActive(false);
    }

    //====  select player after buy

    public int playerSelect = 0;

    public void SelectChar_1()
    {
        if (PlayerPrefs.GetInt(ApiConstant.chrPur1) == 1)
        {
            GameManager.instt.ChooseLevel_PlayerImg.sprite = AllImageChar[0]; // Vishal Added
            GameManager.instt.OfflinePanel.sprite = AllImageChar[0]; // Vishal Added
            playerSelect = 0;
            CloseALlRickMark(0);
            SetPinImage(0);
            PlayerPrefs.SetInt(ApiConstant.Chr_Apply, 1);
            tickMark[0].SetActive(true);
            //============
            if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
            {
                //string PlayerNo = (string)PhotonNetwork.player.CustomProperties["save_indx"];
                //int indx = int.Parse(PlayerNo);

                //GameManager.instt.PlayerImg[indx - 1].sprite = AllImageChar[0];

            }
            else
            {
                PlayerImg.sprite = AllImageChar[0];
            }
            //============

        }
    }

    public void SelectChar_2()
    {
        if (PlayerPrefs.GetInt(ApiConstant.chrPur2) == 1)
        {
            GameManager.instt.ChooseLevel_PlayerImg.sprite = AllImageChar[1]; // Vishal Added
            GameManager.instt.OfflinePanel.sprite = AllImageChar[1]; // Vishal Added
            playerSelect = 1;
            CloseALlRickMark(1);
            SetPinImage(1);
            PlayerPrefs.SetInt(ApiConstant.Chr_Apply, 2);
            tickMark[1].SetActive(true);
            //============
            if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
            {
                //string PlayerNo = (string)PhotonNetwork.player.CustomProperties["save_indx"];
                //int indx = int.Parse(PlayerNo);

                //GameManager.instt.PlayerImg[indx - 1].sprite = AllImageChar[1];

            }
            else
            {
                PlayerImg.sprite = AllImageChar[1];
            }
            //============
        }
    }

    public void SelectChar_3()
    {
        if (PlayerPrefs.GetInt(ApiConstant.chrPur3) == 1)
        {
            GameManager.instt.ChooseLevel_PlayerImg.sprite = AllImageChar[2]; // Vishal Added
            GameManager.instt.OfflinePanel.sprite = AllImageChar[2]; // Vishal Added
            playerSelect = 2;
            CloseALlRickMark(2);
            SetPinImage(2);
            PlayerPrefs.SetInt(ApiConstant.Chr_Apply, 3);
            tickMark[2].SetActive(true);
            //============
            if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
            {
                //string PlayerNo = (string)PhotonNetwork.player.CustomProperties["save_indx"];
                //int indx = int.Parse(PlayerNo);
                //GameManager.instt.PlayerImg[indx - 1].sprite = AllImageChar[2];
            }
            else
            {
                PlayerImg.sprite = AllImageChar[2];
            }
            //============
        }
    }

    public void SelectChar_4()
    {
        if (PlayerPrefs.GetInt(ApiConstant.chrPur4) == 1)
        {
            GameManager.instt.ChooseLevel_PlayerImg.sprite = AllImageChar[3]; // Vishal Added
            GameManager.instt.OfflinePanel.sprite = AllImageChar[3]; // Vishal Added
            playerSelect = 3;
            CloseALlRickMark(3);
            SetPinImage(3);
            PlayerPrefs.SetInt(ApiConstant.Chr_Apply, 4);
            tickMark[3].SetActive(true);
            //============
            if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
            {
                //string PlayerNo = (string)PhotonNetwork.player.CustomProperties["save_indx"];
                //int indx = int.Parse(PlayerNo);
                //GameManager.instt.PlayerImg[indx - 1].sprite = AllImageChar[3];
            }
            else
            {
                PlayerImg.sprite = AllImageChar[3];
            }
            //============
        }
    }

    public void SelectChar_5()
    {
        if (PlayerPrefs.GetInt(ApiConstant.chrPur5) == 1)
        {
            GameManager.instt.ChooseLevel_PlayerImg.sprite = AllImageChar[4]; // Vishal Added
            GameManager.instt.OfflinePanel.sprite = AllImageChar[4]; // Vishal Added
            playerSelect = 4;
            CloseALlRickMark(4);
            SetPinImage(4);
            PlayerPrefs.SetInt(ApiConstant.Chr_Apply, 5);
            tickMark[4].SetActive(true);
            //============
            if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
            {
                //string PlayerNo = (string)PhotonNetwork.player.CustomProperties["save_indx"];
                //int indx = int.Parse(PlayerNo);
                //GameManager.instt.PlayerImg[indx - 1].sprite = AllImageChar[4];
            }
            else
            {
                PlayerImg.sprite = AllImageChar[4];
            }
            //============
        }
    }

    public void SelectChar_6()
    {
        if (PlayerPrefs.GetInt(ApiConstant.chrPur6) == 1)
        {
            GameManager.instt.ChooseLevel_PlayerImg.sprite = AllImageChar[5]; // Vishal Added
            GameManager.instt.OfflinePanel.sprite = AllImageChar[5]; // Vishal Added
            playerSelect = 5;
            CloseALlRickMark(5);
            SetPinImage(5);
            PlayerPrefs.SetInt(ApiConstant.Chr_Apply, 6);
            tickMark[5].SetActive(true);
            //============
            if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
            {
                //string PlayerNo = (string)PhotonNetwork.player.CustomProperties["save_indx"];
                //int indx = int.Parse(PlayerNo);
                //GameManager.instt.PlayerImg[indx - 1].sprite = AllImageChar[5];
            }
            else
            {
                PlayerImg.sprite = AllImageChar[5];
            }
            //============
        }
    }

    public IEnumerator Blast_Effect()
    {
        BlastParticle.SetActive(true);
        yield return new WaitForSeconds(2f);
        BlastParticle.SetActive(false);
    }

    void SetPinImage(int Val)
    {
        Playarea.instt.Player1Manager[0].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(150, 150);
        Playarea.instt.Player1Manager[1].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(150, 150);
        Playarea.instt.Player1Manager[0].GetComponent<Image>().sprite = PinCharImg[Val];
        Playarea.instt.Player1Manager[1].GetComponent<Image>().sprite = PinCharImg[Val];

        for (int i = 0; i <= 6; i++)
        {

            Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().LadderAnimation[i] = Playarea.instt.NewCharAnimObj[i];
            Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().LadderAnimation[i] = Playarea.instt.NewCharAnimObj[i];

            Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().LadderAnimation[i].GetComponent<Image>().sprite = PinCharImg[Val];
            Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().LadderAnimation[i].GetComponent<Image>().sprite = PinCharImg[Val];

            Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().LadderAnimation[i].GetComponent<ActivePlayer>().CharInPin = true;
            Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().LadderAnimation[i].GetComponent<ActivePlayer>().CharInPin = true;


        }
    }

    public IEnumerator SetPinImage1(int Val)
    {

        if (PlayerPrefs.GetInt(ApiConstant.multiplayerGame) == 1)
        {
            //string PlayerNo = (string)PhotonNetwork.player.CustomProperties["save_indx"];
            //int indx = int.Parse(PlayerNo);
            //GameManager.instt.PlayerImg[indx - 1].sprite = AllImageChar[Val];
        }
        else
        {
            PlayerImg.sprite = AllImageChar[Val];
        }

        yield return new WaitForSeconds(0.1f);

        print("Value Is : " + Val);

        Playarea.instt.Player1Manager[0].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(150, 150);
        Playarea.instt.Player1Manager[1].GetComponent<Image>().rectTransform.sizeDelta = new Vector2(150, 150);
        Playarea.instt.Player1Manager[0].GetComponent<Image>().sprite = PinCharImg[Val];
        Playarea.instt.Player1Manager[1].GetComponent<Image>().sprite = PinCharImg[Val];
        Playarea.instt.Player1Manager[0].transform.GetChild(0).GetComponent<Image>().sprite = PinCharImg[Val];
        Playarea.instt.Player1Manager[1].transform.GetChild(0).GetComponent<Image>().sprite = PinCharImg[Val];


        for (int i = 0; i <= 6; i++)
        {

            Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().LadderAnimation[i] = Playarea.instt.NewCharAnimObj[i];
            Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().LadderAnimation[i] = Playarea.instt.NewCharAnimObj[i];

            Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().LadderAnimation[i].GetComponent<Image>().sprite = PinCharImg[Val];
            Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().LadderAnimation[i].GetComponent<Image>().sprite = PinCharImg[Val];

            Playarea.instt.Player1Manager[0].GetComponent<PlayerManager>().LadderAnimation[i].GetComponent<ActivePlayer>().CharInPin = true;
            Playarea.instt.Player1Manager[1].GetComponent<PlayerManager>().LadderAnimation[i].GetComponent<ActivePlayer>().CharInPin = true;
        }
    }

    public void SetCharacterInGamePlay()
    {
        //		print ("Function Called....");
        for (int i = 0; i < tickMark.Length; i++)
        {
            //			print ("Tick Mark Length");
            if (tickMark[i].gameObject.activeSelf == true)
            {
                //				print ("Candition Is True");
                GameManager.instt.ChooseLevel_PlayerImg.sprite = AllImageChar[i];
                StartCoroutine(SetPinImage1(i));
            }
        }
    }

}