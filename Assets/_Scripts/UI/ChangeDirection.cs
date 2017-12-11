using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class ChangeDirection : NetworkBehaviour
{
    public Sprite[] arrowSprites;

    private Button[] arrowButtons;
    private int[] indices;

    private void Start()
    {
        arrowButtons = GetComponentsInChildren<Button>();
        indices = new int[arrowButtons.Length];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = 0;
        }
    }

    [Command]
    public void CmdRotateArrow(int index)
    {
        RpcRotateArrow(index);
    }

    [ClientRpc]
    private void RpcRotateArrow(int index)
    {
        ++indices[index];
        indices[index] %= 4;
        arrowButtons[index].GetComponent<Image>().sprite = arrowSprites[indices[index]];
    }
}
