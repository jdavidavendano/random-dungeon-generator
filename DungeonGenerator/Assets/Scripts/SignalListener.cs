using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour {

    public Signal _signal;
    public UnityEvent _signalEvent;

    public void OnSignalRaised() {
        _signalEvent.Invoke();
    }

    void OnEnable() {
        _signal.RegisterListener(this);
    }

    void Disable() {
        _signal.DeRegisterListener(this);
    }
}