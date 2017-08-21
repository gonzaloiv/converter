using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

  #region Mono Behaviour

  private static DontDestroyOnLoad instance;

  #endregion

  #region Mono Behaviour
 
  void Awake() {
    if (instance)
      Destroy(gameObject);
    else {
      DontDestroyOnLoad(gameObject);
      instance = this;
    }
  }

  #endregion
  
}