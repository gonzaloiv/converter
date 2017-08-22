using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelController : Singleton<InfoPanelController> {

    #region Fields / Properties

    [SerializeField] private float infoPanelTime = 3;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Text infoPanelLabel;

    #endregion

    #region Mono Behaviour

    void Awake() {
        infoPanel.SetActive(false);
        FixerIOClient.CurrencyErrorEvent += OnCurrencyErrorEvent;
    }

    void OnDestroy() {
        FixerIOClient.CurrencyErrorEvent -= OnCurrencyErrorEvent;
    }

    #endregion

    #region Public Behaviour

    public void ShowInfoPanel(string infoMessage) {
        infoPanelLabel.text = infoMessage;
        StartCoroutine(InfoPanelRoutine());
    }

    public void OnCurrencyErrorEvent(string errorMessage) {
        infoPanelLabel.text = errorMessage;
        StartCoroutine(InfoPanelRoutine());
    }

    #endregion

    #region Private Behaviour

    private IEnumerator InfoPanelRoutine() {
        infoPanel.SetActive(true);
        yield return new WaitForSeconds(infoPanelTime);
        infoPanel.SetActive(false);
    }

    #endregion
	
}
