using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour {

    #region Fields / Properties

    [SerializeField] private GameObject currencyConverterScreen;

    #endregion

    #region Mono Behaviour

    void Awake() {
        currencyConverterScreen.SetActive(false);
        FixerIOClient.CurrencyResultEvent += OnCurrencyResultEvent;
    }

    void OnDestroy() {
        FixerIOClient.CurrencyResultEvent -= OnCurrencyResultEvent;
    }

    #endregion

    #region Public Behaviour

    public void OnCurrencyResultEvent(CurrencyResultEventArgs currencyResultEventArgs) {
        if (!currencyConverterScreen.activeInHierarchy)
            currencyConverterScreen.SetActive(true);
    }

    #endregion

}