using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChangeDirection : NetworkBehaviour
{
    public Sprite[] arrowSprites;

    private Button[] arrowButtons;
    private int[] indeces;

    private void Start()
    {
        arrowButtons = GetComponentsInChildren<Button>();
        indeces = new int[arrowButtons.Length];
        for (int i = 0; i < indeces.Length; i++)
        {
            indeces[i] = 0;
        }
    }

    [Command]
    public void CmdRotateArrow(int index)
    {
        if (isLocalPlayer)
        {
            RpcRotateArrow(index);
        }
    }

    [ClientRpc]
    private void RpcRotateArrow(int index)
    {
        ++indeces[index];
        indeces[index] %= 4;
        arrowButtons[index].GetComponent<Image>().sprite = arrowSprites[index];
    }
}
