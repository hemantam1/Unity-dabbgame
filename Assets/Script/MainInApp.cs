using UnityEngine;
using System.Collections;
//using OnePF;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainInApp : MonoBehaviour
{
	public static MainInApp Instance;
	public GameObject BTremoveadsObj;

	bool IsBillingPermisionCheck = false;

	const string SKU_Rush_25 = "com.brokenelbow.boombricksrush.25rush";
	const string SKU_Rush_100 = "com.brokenelbow.boombricksrush.100rush";
	const string SKU_Rush_250 = "com.brokenelbow.boombricksrush.250rush";

	//Inventory _inventory = null;

	void Awake ()
	{
		Instance = this;
	}

	void Start ()
	{
		InitializeAllSku ();
		InitializeStore ();
	}

	void OnEnable ()
	{
		// Listen to all events for illustration purposes
		//OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
		//OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
		//OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		//OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		//OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
		//OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
		//OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		//OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;

//		if (PlayerPrefs.GetInt (Constant.RemoveAdsPrefas) == 100) {
//			BTremoveadsObj.SetActive (false);
//		} else {
//			BTremoveadsObj.SetActive (true);
//		}
	}

	private void OnDisable ()
	{
		// Remove all event handlers
		//OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
		//OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		//OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		//OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		//OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		//OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
		//OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		//OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}

	void InitializeAllSku ()
	{
		// Map skus for different stores   

		#if UNITY_ANDROID
		//OpenIAB.mapSku (SKU_Rush_25, OpenIAB_Android.STORE_GOOGLE, SKU_Rush_25);
		//OpenIAB.mapSku (SKU_Rush_100, OpenIAB_Android.STORE_GOOGLE, SKU_Rush_100);
		//OpenIAB.mapSku (SKU_Rush_250, OpenIAB_Android.STORE_GOOGLE, SKU_Rush_250);
		#elif UNITY_IOS || UNITY_IPHONE


		OpenIAB.mapSku (SKU_Rush_25, OpenIAB_iOS.STORE, SKU_Rush_25);
		OpenIAB.mapSku (SKU_Rush_100, OpenIAB_iOS.STORE, SKU_Rush_100);
		OpenIAB.mapSku (SKU_Rush_250, OpenIAB_iOS.STORE, SKU_Rush_250);

//		OpenIAB.mapSku(SKU_Coins_2500, OpenIAB_iOS.STORE, SKU_Coins_2500);
//		OpenIAB.mapSku(SKU_Coins_10000, OpenIAB_iOS.STORE, SKU_Coins_10000);
//		OpenIAB.mapSku(SKU_Coins_25000, OpenIAB_iOS.STORE, SKU_Coins_25000);
//		OpenIAB.mapSku(SKU_Coins_50000, OpenIAB_iOS.STORE, SKU_Coins_50000);
//		OpenIAB.mapSku(SKU_Coins_100000, OpenIAB_iOS.STORE, SKU_Coins_100000);
//		OpenIAB.mapSku (SKU_removeads, OpenIAB_iOS.STORE, SKU_removeads);
		#endif
	}

	void InitializeStore ()
	{
		//var options = new Options ();
		//options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
		//options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
		//options.checkInventory = false;
		//options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;

		//#if UNITY_ANDROID
		//// Application public key
		//var googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApqg9pUrTQ6xpp7qtsMarHyXCxrGxsPOhChtOYVnWJCczcTYI5GbUN/PqWPhlXcmtCaOp+dwd86+ZNqJK55fHyhUAYDfWBe8iQUO9mJp5nJ5mqqPVnelNzcH7uIpN722NFv9u1WvOpHgLocG1uNtdakEwVCJgzDSnGbNxNClYPYX0SHPzh3yTE/93TnekbjrSkA4OwgJYrcfp/LeknB3LnSNaJlqSqz9S/osPEFY2UwtrrxmyaCPl8oNRX+VKtJWFdyp+ThNltEqPSqwBWSFG3jdIKdMPnxNAnoEFd6fId5Zsjz9jWBg4cPdl31xSOXcuII8nsM9nICKNmxtTuWvpYQIDAQAB";
		//options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
		//options.availableStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
		//options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_GOOGLE, googlePublicKey } };
		//#elif UNITY_IOS || UNITY_IPHONE
		//options.prefferedStoreNames = new string[] { OpenIAB_iOS.STORE };
		//options.availableStoreNames = new string[] { OpenIAB_iOS.STORE };
		//#endif

		//options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;

		//// Transmit options and start the service
		//OpenIAB.init (options);
	}

	public void Activate ()
	{
		gameObject.SetActive (true);
	}

	public void Deactivate ()
	{
		gameObject.SetActive (false);
	}

	public void Buy1DollarCoin ()
	{
		//OpenIAB.purchaseProduct (SKU_Rush_25);
	}

	public void Buy3DollarCoin ()
	{
		//OpenIAB.purchaseProduct (SKU_Rush_100);
	}

	public void Buy5DollarCoin ()
	{
		//OpenIAB.purchaseProduct (SKU_Rush_250);
	}



	public void OnRestoreInAppClick ()
	{
		//OpenIAB.queryInventory ();
	}

	//-----------------------------------------------------> Listener <-----------------------------------------------
	private void billingSupportedEvent ()
	{
		Debug.Log ("billingSupportedEvent");
		IsBillingPermisionCheck = true;
		//OpenIAB.queryInventory ();
	}

	private void billingNotSupportedEvent (string error)
	{
		Debug.Log ("billingNotSupportedEvent: " + error);
	}

	//private void queryInventorySucceededEvent (Inventory inventory)
	//{
	//	Debug.Log ("queryInventorySucceededEvent: " + inventory);
	//	if (inventory != null) {
	//		_inventory = inventory;
	//	}

	//}

	private void queryInventoryFailedEvent (string error)
	{
		Debug.Log ("queryInventoryFailedEvent: " + error);
	}

	//private void purchaseSucceededEvent (Purchase purchase)
	//{
	//	Debug.Log ("purchase succeed event: " + purchase);
	//	switch (purchase.Sku) {
	//	case SKU_Rush_25:
	//		break;

	//	case SKU_Rush_100:
	//		break;

	//	case SKU_Rush_250:
	//		break;

	//	default:
	//		Debug.LogWarning ("Unknown SKU: " + purchase.Sku);
	//		break;
	//	}
	//}

	private void purchaseFailedEvent (int errorCode, string errorMessage)
	{
		Debug.Log ("purchaseFailedEvent: " + errorMessage);
	}

	//private void consumePurchaseSucceededEvent (Purchase purchase)
	//{
	//	Debug.Log ("consumePurchaseSucceededEvent: " + purchase);
	//}

	private void consumePurchaseFailedEvent (string error)
	{
		Debug.Log ("consumePurchaseFailedEvent: " + error);
	}


}
