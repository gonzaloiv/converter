using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Based on: https://github.com/Lukejkw/Fixer-IO-Sharp/blob/master/Fixer-IO-Sharp/CurrencyResult.cs
public class CurrencyResultArgs : EventArgs {

    public string Base { get; set; }
    public DateTime Date { get; set; }
    public Dictionary<string, decimal> Rates { get; set; }
	
}
