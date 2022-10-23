using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndShow : MonoBehaviour
{
    [SerializeField] private Animator _panel;
    private bool _show = false;


    private void Awake()
    {
        _panel.Play("Hide");
    }

    public void HidePanel()
    {
        if (_show)
        {
            _show = false;
            _panel.Play("Hide");
        }
        else
        {
            _show = true;
            _panel.Play("Show");
        }
    }
}
