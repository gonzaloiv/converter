using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class CurrencyConverterScreenController : MonoBehaviour {

    #region Fields

    private string BASE_CURRENCY = "EUR";

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
        baseRateSymbol = BASE_CURRENCY;
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

    public void OnCurrencyAmountInputFieldChangeEvent () {
        conversionResultPanel.SetActive(ConvertCurrentCurrencySelection());
    }

    public void OnFromCurrencyDropdownChangeEvent () {
        if (currentRates != null)
            baseRateSymbol = ratesSymbols[fromCurrencyDropdown.value];
        RequestFixerIORates();
        conversionResultPanel.SetActive(ConvertCurrentCurrencySelection());
    }

    public void OnToCurrencyDropdownChangeEvent () {
        conversionResultPanel.SetActive(ConvertCurrentCurrencySelection());
    }

    public void OnDatePanelChangeEvent () {
        RequestFixerIORates();
        conversionResultPanel.SetActive(ConvertCurrentCurrencySelection());
    }

    public void OnConvertButtonClick () {
        conversionResultPanel.SetActive(ConvertCurrentCurrencySelection());
    }

    #endregion

    #region Private Behaviour

    private void InitializeCurrencyConverterScreen () {
        ratesSymbols = new List<string>();
        ratesSymbols.Add(BASE_CURRENCY);
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
        int baseRateIndex = ratesSymbols.IndexOf(ratesSymbols.FirstOrDefault(x => string.Equals(x, BASE_CURRENCY)));
        fromCurrencyDropdown.value = baseRateIndex;
    }

    private void UpdateToCurrencyDropdown () {
        toCurrencyDropdown.ClearOptions();
        List<string> options = new List<string>();
        currentRates.ForEach(x => options.Add(x.Symbol));
        toCurrencyDropdown.AddOptions(options);
    }

    private void RequestFixerIORates () {
        DateTime selectedDate = datePanelController.GetSelectedDate();
        // TODO: informing the user about this
        if (selectedDate < new DateTime(1999, 1, 1))
            return;
        fixerIOClient.GetRates(baseRateSymbol, selectedDate);
    }

    private bool ConvertCurrentCurrencySelection () {
        string currencyAmountInputText = currencyAmountInputField.text;
        int currencyAmount;
        if (currencyAmountInputText != null && currencyAmountInputText != string.Empty && int.TryParse(currencyAmountInputField.text, out currencyAmount)) {
            currencyConversionResult.text = (currencyAmount * 1 / currentRates[toCurrencyDropdown.value].Value).ToString();
            return true;
        } else {
            return false;
        }
    }

    #endregion
	
}