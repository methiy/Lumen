using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MirrorAmountVisual : MonoBehaviour
{
    public Text amountText;

    public void ChangeAmount(int amount){
        Debug.Log("���׶�");
        amountText.text=Convert.ToString(amount);
    }
}
