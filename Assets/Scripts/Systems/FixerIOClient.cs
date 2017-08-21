using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

// Based on: https://github.com/Lukejkw/Fixer-IO-Sharp
// and: https://github.com/rmorrin/FixerSharp
public class FixerIOClient : MonoBehaviour {

    #region Fields

    private const string FIXER_BASE_URL = "http://api.fixer.io/";

    #endregion

    #region Events

    public delegate void CurrencyResultEventHandler (CurrencyResultEventArgs currencyResultEventArgs);
    public static event CurrencyResultEventHandler CurrencyResultEvent;

    #endregion

    #region Mono Behaviour

    void OnEnable () {
        GetRates();
    }

    #endregion

    #region Public  Behaviour

    public void GetRates () {
        string url = FIXER_BASE_URL + "latest";
        StartCoroutine(RequestRoutine(url));
    }

    public void GetRates(string currencySymbol) {
        string url = FIXER_BASE_URL + "latest?base=" + currencySymbol;
        StartCoroutine(RequestRoutine(url));
    }

    public void GetRates(string currencySymbol, DateTime date) {
        string url = FIXER_BASE_URL + date.ToString("yyyy-MM-dd") + "?base=" + currencySymbol;
        StartCoroutine(RequestRoutine(url));
    }

    #endregion

    #region Private Behaviour

    private IEnumerator RequestRoutine (string url) {
        WWW www = new WWW(url);
        yield return www;
        while (!www.isDone)
            yield return null;
        if (www.error != null) {
            Debug.Log("It was impossible to get Fixer.io rates");
            Debug.Log(www.error);
        } else {
            Debug.Log("Fixer.io rates downloaded correctly");
            Debug.Log(www.text);
            CurrencyResultEvent(ProcessResponse(www.text));
        }
    }

    private CurrencyResultEventArgs ProcessResponse (string responseText) {
        JsonData responseData = JsonMapper.ToObject(responseText);
        CurrencyResultEventArgs currencyResultEventArgs = new CurrencyResultEventArgs(responseData["base"].ToString());
        foreach (var key in responseData["rates"].Keys)
            currencyResultEventArgs.Rates.Add(new Rate(key.ToString(), float.Parse(responseData["rates"][key].ToString())));
        return currencyResultEventArgs;
    }

    #endregion

}