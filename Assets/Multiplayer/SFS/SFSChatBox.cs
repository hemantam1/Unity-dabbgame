using UnityEngine;
using System.Collections;
using Sfs2X.Core;
using UnityEngine.UI;
using System.Collections.Generic;
using Sfs2X.Entities;


public class SFSChatBox : MonoBehaviour
{

    public class Message
    {
        public string sender;
        public string msg;

        public Message(string _sender, string _msg)
        {
            sender = _sender;
            msg = _msg;
        }
    }

    public InputField msgInputField;
    public Text recentMessagesText;
    public bool shy;
    public Room sfsRoom;

    public List<Message> recentMessages = new List<Message>();

//    #if ENTER_KEY_AVAILABLE
    void Update()
    {
//        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
//        {
//            SendNewMsg();
//        }
    } 
//    #endif

    void OnEnable()
    {
//        #if !ENTER_KEY_AVAILABLE
//        msgInputField.onva.AddListener(SendNewMsg);
		//SFS.OnNewPublicMessage += OnNewPublicMsg;
//        #endif 
//		BackButtonHandler.Register (Hide);
    }

    void OnDisable()
    {
//        #if !ENTER_KEY_AVAILABLE
//        msgInputField.onEndEdit.RemoveListener(SendNewMsg);
		//SFS.OnNewPublicMessage -= OnNewPublicMsg;
//        #endif
//		BackButtonHandler.Remove (Hide);
    }

    public void Toggle()
    {
        print("Toggle");
        if (gameObject.activeSelf)
            Hide();
        else
            Show();
    }

    public void Show()
    {
        print("Show");
        gameObject.SetActive(true);
        msgInputField.Select();
    }

    public void Hide()
    {
        print("Hide");
        msgInputField.text = "";
        gameObject.SetActive(false); 
    }

    //just so that it can be assigned to InputField OnEndEdit event, parameter does nothing
    public void SendNewMsg(string s)
    {
        print("Send s");
        SendNewMsg();
    }

    public void SendNewMsg()
    {
        print("Send");
        if (string.IsNullOrEmpty(msgInputField.text))
        {
            Hide();
            return;
        }

        if (sfsRoom == null) SFS.SendNewPublicMessage(msgInputField.text);
        else SFS.SendNewPublicMessage(msgInputField.text, sfsRoom);
        msgInputField.text = "";
        if (shy) Hide();
    }

    void OnNewPublicMsg(string sender, string msg, Room room)
    {
        if (room != null && room.Id != sfsRoom.Id) return;
        recentMessages.Add(new Message(sender, msg));
        UpdateChat();
    }

    int showHistoryUpto = 50;

    void UpdateChat()
    {
        if (recentMessagesText == null)
            return;

        int showFrom = Mathf.Clamp(recentMessages.Count - showHistoryUpto, 0, recentMessages.Count);
        string s = "";
        for (int i = showFrom; i < recentMessages.Count; i++)
        {
            s += "<b>" + recentMessages[i].sender + "</b>" + " : " + recentMessages[i].msg + "\n"; 
        }
        recentMessagesText.text = s;
        recentMessagesText.rectTransform.localPosition = new Vector3(0, recentMessagesText.rectTransform.rect.height + 100, 0);
    }


}
