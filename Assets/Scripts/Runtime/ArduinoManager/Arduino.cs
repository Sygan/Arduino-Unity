using System;
using UnityEngine;

namespace gd.Sygan.ArduinoUnity
{
    /// <summary>
    /// This class defines single Arduino Instance. It allows to define Serial port name and number.
    /// It also allows to send and receive Serial messages from Arduino.
    /// </summary>
    [Serializable]
    public class Arduino
    {
        [SerializeField]
        private int _port;

        [SerializeField]
        private int _portName;
        
        
    }
}