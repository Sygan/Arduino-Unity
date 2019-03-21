using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sygan.ArduinoUnity
{
    /// <summary>
    /// This class defines single Arduino Instance. It allows to define Serial port name and number.
    /// It also allows to send and receive Serial messages from Arduino.
    /// </summary>
    [CreateAssetMenu(menuName = "Sygan/Arduino Manager/New Arduino Instance")]
    public class ArduinoDefinition : ScriptableObject
    {
        [SerializeField]
        private int _baudRate;

        [SerializeField]
        private string _portName;

        public int BaudRate
        {
            get { return _baudRate; }
        }

        public string PortName
        {
            get { return _portName; }
        }
    }
}