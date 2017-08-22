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
        if (YearIsValid(yearInputField.text) && MonthIsValid(monthInputField.text) && DayIsValid(dayInputField.text)) {
            DateTime selectedDateTime = new DateTime(
                Int32.Parse(yearInputField.text.TrimStart('0')), 
                Int32.Parse(monthInputField.text.TrimStart('0')), 
                Int32.Parse(dayInputField.text.TrimStart('0'))
            );
            return selectedDateTime;
        } else {
            // TODO: replacing this...
            return new DateTime(1111, 11, 11);
        }
    }

    #endregion

    #region Private Behaviour

    private bool InputIsValid (string inputText) {
        return inputText != null && inputText != string.Empty && inputText != "0";
    }

    private bool YearIsValid(string yearInputText) {
        int year = int.Parse(yearInputText);
        return year > 1000 && year < 9999 ? InputIsValid(yearInputText) : false;
    }

    private bool MonthIsValid(string monthInputText) {
        int month = int.Parse(monthInputText);
        return month > 0 && month <= 12 ? InputIsValid(monthInputText) : false;
    }

    private bool DayIsValid(string dayInputText) {
        int day = int.Parse(dayInputText);
        return day > 0 && day <= 31 ? InputIsValid(dayInputText) : false;
    }

    #endregion

}
