using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UserSearching : MonoBehaviour
{
    [SerializeField] InputField inputFrom;
    [SerializeField] InputField inputTo;

    public void FromValChanged()
    {
        gameObject.GetComponent<FindCabs>().desiredCab = Convert.ToInt32(inputTo.text);
    }
    public void ToValChanged()
    {
        gameObject.GetComponent<FindCabs>().fromUserCab = Convert.ToInt32(inputFrom.text);
    }
}
