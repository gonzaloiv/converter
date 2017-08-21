using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Rate {

    public string Symbol;
    public float Value;

    public Rate(string symbol, float value) {
        this.Symbol = symbol;
        this.Value = value;
    }
	
}
