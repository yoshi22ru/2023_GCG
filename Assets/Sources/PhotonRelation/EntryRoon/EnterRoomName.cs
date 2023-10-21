using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterRoomName : Utility.PhotonUtility
{
    #region SettingName
    [SerializeField] InputField input_room_name_field;
    [SerializeField] Button ok_button;
    [SerializeField] Button cancel_button;

    void Send_New_Name() {
        var new_name = input_room_name_field.text;
        SetMyName(new_name);
    }

    void Cancel_Set_Name() {
        input_room_name_field.text = "";
        this.gameObject.SetActive(false);
    }

    #endregion

    private void Start() {
        ok_button.onClick.AddListener(Send_New_Name);
        cancel_button.onClick.AddListener(Cancel_Set_Name);
    }


}
