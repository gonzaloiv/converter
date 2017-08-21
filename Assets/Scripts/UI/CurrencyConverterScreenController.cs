using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class CurrencyConverterScreenController : MonoBehaviour {

    #region Fields

    private string BASE_CURENCY = "EUR";

    [SerializeField] private FixerIOClient fixerIOClient;

    // RESULT PANEL
    [SerializeField] private GameObject conversionResultPanel;
    [SerializeField] private Text currencyConversionResult;

    // CURRENCY PANEL
    [SerializeField] private InputField currencyAmountInputField;
    [SerializeField] private Dropdown fromCurrencyDropdown;
    [SerializeField] private Dropdown toCurrencyDropdown;

    // DATE PANEL
    [SerializeField] private DatePanelController datePanelController;

    private List<string> ratesSymbols;
    private List<Rate> currentRates;
    private string baseRateSymbol;
    private bool initialized = false;

    #endregion

    #region Mono Behaviour

    void Awake () {
        conversionResultPanel.SetActive(false);
        FixerIOClient.CurrencyResultEvent += OnCurrencyResultEvent;
    }

    void OnDestroy () {
        FixerIOClient.CurrencyResultEvent -= OnCurrencyResultEvent;
    }

    #endregion

    #region Public Behaviour

    public void OnCurrencyResultEvent (CurrencyResultEventArgs currencyResultEventArgs) {
        currentRates = currencyResultEventArgs.Rates;
        UpdateToCurrencyDropdown();
        if (!initialized && currentRates != null)
            InitializeCurrencyConverterScreen();
    }

    public void OnFromCurrencyDropdownChangeEvent () {
        if (currentRates != null)
            baseRateSymbol = ratesSymbols[fromCurrencyDropdown.value];
        RequestFixerIORates();
    }

    public void OnToCurrencyDropdownChangeEvent () {
        if (!conversionResultPanel.activeInHierarchy)
            conversionResultPanel.SetActive(true);
        // TODO: convert current amount...
    }

    public void OnDatePanelChangeEvent () {
        RequestFixerIORates();
    }

    public void OnConvertButtonClick () {
        if (!conversionResultPanel.activeInHierarchy)
            conversionResultPanel.SetActive(true);
        // TODO: convert current amount...
    }

    #endregion

    #region Private Behaviour

    private void InitializeCurrencyConverterScreen () {
        ratesSymbols = new List<string>();
        ratesSymbols.Add(BASE_CURENCY);
        currentRates.ForEach(x => ratesSymbols.Add(x.Symbol));
        UpdateFromCurrencyDropdown();
        UpdateToCurrencyDropdown();
        initialized = true;
    }

    private void UpdateFromCurrencyDropdown () {
        fromCurrencyDropdown.ClearOptions();
        List<string> options = new List<string>();
        ratesSymbols.ForEach(x => options.Add(x));
        fromCurrencyDropdown.AddOptions(options);
        int baseRateIndex = ratesSymbols.IndexOf(ratesSymbols.FirstOrDefault(x => string.Equals(x, BASE_CURENCY)));
        fromCurrencyDropdown.value = baseRateIndex;
    }

    private void UpdateToCurrencyDropdown () {
        toCurrencyDropdown.ClearOptions();
        List<string> options = new List<string>();
        currentRates.ForEach(x => options.Add(x.Symbol));
        toCurrencyDropdown.AddOptions(options);
    }

    private void RequestFixerIORates() {
        DateTime selectedDate = datePanelController.GetSelectedDate();
        if (selectedDate ==  new DateTime(1111, 11, 11)) {
            fixerIOClient.GetRates(baseRateSymbol);
        } else {
            fixerIOClient.GetRates(baseRateSymbol, selectedDate);
        }
    }

    #endregion
	
}