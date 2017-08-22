using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Based on: https://github.com/Lukejkw/Fixer-IO-Sharp/blob/master/Fixer-IO-Sharp/CurrencyResult.cs
public class CurrencyResultEventArgs : EventArgs {

    public string BaseRate;
    public List<Rate> Rates;

    public CurrencyResultEventArgs(string baseRate) {
        this.BaseRate = baseRate;
        Rates = new List<Rate>();
    }
	
}