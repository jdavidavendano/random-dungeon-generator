using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver {
    // Serialización es cargar y descargar objetos de la memoria
    public float _initialValue;
    [HideInInspector] public float _runTimeValue;

    // Después de descargar los objetos del juego
    public void OnAfterDeserialize() {
        _runTimeValue = _initialValue;
    }

    public void OnBeforeSerialize() {

    }
}