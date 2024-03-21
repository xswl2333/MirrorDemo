using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Web;

public class PlayerManager : NetworkBehaviour
{
    public GameObject floatingInfo;
    public TextMesh nameText;

    private Material playerMaterialClone;

    [SyncVar(hook=nameof(OnPlayerNameChanged))]
    private string playerName;  
    [SyncVar(hook=nameof(OnPlayerColorChanged))]
    private Color playerColor;


    private void OnPlayerNameChanged(string oldString,string newString)
    {
        nameText.text = newString;
    }

    private void OnPlayerColorChanged(Color oldColor, Color newColor)
    {
        nameText.color= newColor;

        playerMaterialClone=new Material(GetComponent<Renderer>().material);
        playerMaterialClone.SetColor("_Color", newColor);
        GetComponent<Renderer>().material = playerMaterialClone;
    }

    [Command]
    private void CmdSetupPlayer(string nameValue, Color colorValue)
    {

        playerName=nameValue;
        playerColor=colorValue;
    }

    public override void OnStartLocalPlayer()
    {
        //摄像机与player绑定
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = Vector3.zero;


        floatingInfo.transform.localPosition = new Vector3(0, -0.3f, 0.6f);
        floatingInfo.transform.localPosition = new Vector3(0.1f, 0.1f, 0.1f);

        ChangeNameAndColor();

    }


    private void Update()
    {

        if (!isLocalPlayer) {

            floatingInfo.transform.LookAt(Camera.main.transform);   //使其他玩家对象朝向
            return;
        } 
        var movex = Input.GetAxis("Horizontal") * Time.deltaTime * 110f;
        var movez= Input.GetAxis("Vertical") * Time.deltaTime * 4.0f;

        transform.Rotate(0,movex,0);
        transform.Translate(0, 0, movez);

        if(Input.GetKeyDown(KeyCode.C)) {
            ChangeNameAndColor();
        }
    }


    private void ChangeNameAndColor()
    {
        var tempName = $"Player{Random.Range(1, 999)}";
        var tempColor = new Color
        (
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            1
        );

        CmdSetupPlayer(tempName, tempColor);
    }
}
