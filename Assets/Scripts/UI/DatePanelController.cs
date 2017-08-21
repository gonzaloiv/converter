using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DatePanelController : MonoBehaviour {

    #region Fields / Properties

    // DATE PANEL
    [SerializeField] private InputField yearInputField;
    [SerializeField] private InputField monthInputField;
    [SerializeField] private InputField dayInputField;

    #endregion

    #region Mono Behaviour


    #endregion

    #region Public Behaviour

    public DateTime GetSelectedDate () {
        if (InputIsValid(yearInputField.text) && InputIsValid(monthInputField.text) && InputIsValid(dayInputField.text)) {
            return new DateTime(Int32.Parse(yearInputField.text), Int32.Parse(monthInputField.text), Int32.Parse(dayInputField.text));
        } else {
            // TODO: finding an alternative to this...
            return new DateTime(1111, 11, 11);
        }
    }

    #endregion

    #region Private Behaviour

    private bool InputIsValid (string inputText) {
        return inputText != null && inputText != string.Empty;
    }

    #endregion

}
