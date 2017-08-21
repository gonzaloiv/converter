using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour {

  #region Mono Behaviour
  
  void OnDestroy() {
    RemoveListeners();
  }
  
  #endregion

  #region Public Behaviour

  public virtual void Enter() {
    Debug.Log(gameObject.name + " enters " + this.GetType());
    AddListeners();
  }

  public virtual void Exit() {
    RemoveListeners();
  }

  public virtual void Play() {}

  #endregion

  #region Protected Behaviour

  protected virtual void AddListeners() {}

  protected virtual void RemoveListeners() {}

  #endregion

}