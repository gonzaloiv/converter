using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

// Based on: https://github.com/Lukejkw/Fixer-IO-Sharp/blob/master/Fixer-IO-Sharp/FixerIOClient.cs
public class FixerIOClient : MonoBehaviour {

    #region Fields

    private const string FIXER_BASE_URL = "http://api.fixer.io/";
    private string BASE_CURENCY = "EUR";

    #endregion
	
    #region Events


    public delegate void CurrencyResultEventHandler (CurrencyResultArgs currencyResultArgs);
    public static event CurrencyResultEventHandler CurrencyResultEvent;

    #endregion

    #region Mono Behaviour

    void Awake() {
        GetLatest();
        GetRatesForDate(new DateTime(2004, 12, 20));
    }

    #endregion

    #region Public  Behaviour

    public void GetLatest() {
        string url = FIXER_BASE_URL + "latest";
        StartCoroutine(RequestRoutine(url));
    }

    public void GetRatesForDate(DateTime date) {
        if (date < new DateTime(1999, 1, 1))
            throw new NotSupportedException("Only currency information from 1999 is available");
        string url = FIXER_BASE_URL + date.ToString("yyyy-MM-dd");
        StartCoroutine(RequestRoutine(url));
    }

    #endregion

    #region Private Behaviour

    private IEnumerator RequestRoutine(string url) {
        WWW www = new WWW(url);
        yield return www;
        while (!www.isDone)
            yield return null;
        if (www.error != null) {
            Debug.Log(www.error);
        } else {
            Debug.Log(www.text);
        }
    }

    #endregion

}