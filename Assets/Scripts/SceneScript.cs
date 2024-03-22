using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneScript : NetworkBehaviour
{
    public Text canvasStatusText;
    public PlayerManager playerManager;

    [SyncVar(hook=nameof(OnStatusTextChange))]

    public string statusText;


    private void OnStatusTextChange(string oldStr,string newStr)
    {
        canvasStatusText.text = statusText;
    }


    public void ButtonSendMessage()
    {
        if(playerManager != null)
        {
            playerManager.CmdSendPlayerMessage();
        }
    }

}
