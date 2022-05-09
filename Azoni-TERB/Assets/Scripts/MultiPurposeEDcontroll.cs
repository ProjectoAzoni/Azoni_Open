using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPurposeEDcontroll : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;

    private bool objEnable;

    // don't know why but lets just leave it there
    public void SetEnableSettingsPanel() {
        optionPanel.SetActive(true);
    }

    public void SetDisableSettingPanel() {
        optionPanel.SetActive(false);
    }

    //change the enabled state of an object
    public void ToogleEnable(GameObject obj) {
        // if the object is active in the hierarchy then disable it
        if (obj.activeInHierarchy)
        {
            objEnable = false;
            obj.SetActive(objEnable);
        }
        // if the object is not active in the hierarchy then enable it
        else
        {
            objEnable = true;
            obj.SetActive(objEnable);
        }
    }
}
