using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerEventHandler : MonoBehaviour
{

    string loginZone = "DabbZone";
   
    void OnEnable()
    {
        LoginToServer();
        SFS.ExtensionResponseReceived += OnExtensionResponseRecieved;
    }

    void OnDisable()
    {
        SFS.ExtensionResponseReceived -= OnExtensionResponseRecieved;
    }

    void OnExtensionResponseRecieved(string cmd, SFSObject dataObject, Room room)
    {
        switch (cmd)
        {
            case "OnLogin":
                Debug.Log(dataObject);
                break;
        }
    }

    void LoginToServer()
    {
        SFS.Connect(() =>
        {
            SFS.Login("username", "", loginZone, () => {

                SFSObject obj = new SFSObject();
                obj.PutUtfString("playername","astha");
                obj.PutUtfString("avatarurl", "");
                obj.PutUtfString("loginType", "loginType");
                SFS.SendExtensionRequest("OnLogin", obj);

            }, (string error) => { });
        }, (string error) => { });
    }
}
