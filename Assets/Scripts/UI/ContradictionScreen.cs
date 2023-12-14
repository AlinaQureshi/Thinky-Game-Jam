using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContradictionScreen : MonoBehaviour
{
    [SerializeField] TMP_Text _option1Text;
    [SerializeField] TMP_Text _option2Text;
    [SerializeField] GameObject _screenObj;

    public void UpdateScreenInfo(string op1, string op2)
    {
        _option1Text.text = op1;
        _option2Text.text = op2;
        ToggleScreen(true);
    }
    
    public void ToggleScreen(bool active)
    {
        _screenObj.SetActive(active);
    }
}
