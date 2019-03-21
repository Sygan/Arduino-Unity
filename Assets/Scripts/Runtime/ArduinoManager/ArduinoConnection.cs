using System;
using System.Collections;
using System.IO.Ports;
using Runtime.Utilities;
using UnityEngine;

namespace Sygan.ArduinoUnity
{
    /// <summary>
    /// Basic class that can be used to create connection with Arduino board.
    /// </summary>
    public class ArduinoConnection
    {
        private SerialPort _serialStream;

        /// <summary>
        /// Friendly name that can be used to recognize the connection in Arduino Manager.
        /// </summary>
        public string FriendlyName { get; private set; }
        
        /// <summary>
        /// Name of the serial port on the computer that will be used to communicate with Arduino board. 
        /// </summary>
        public string PortName { get; private set; }
        
        /// <summary>
        /// Baud Rate that will be used to communicate with Arduino board.
        /// </summary>
        public int BaudRate { get; private set; }
        
        /// <summary>
        /// Read Timeout that will be used to communicate with Arduino board.
        /// </summary>
        public int ReadTimeout { get; private set; }
        
        /// <summary>
        /// Is this connection initialized.
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Serial Port used for communication with Arduino board.
        /// </summary>
        public SerialPort SerialStream
        {
            get { return _serialStream; }
        }

        public ArduinoConnection(int baudRate, string portName, string friendlyName = null, int readTimeout = 50)
        {
            BaudRate = baudRate;
            PortName = portName;
            ReadTimeout = readTimeout;
            FriendlyName = string.IsNullOrEmpty(friendlyName) ? GetNextAvailableFriendlyName() : friendlyName;
        }

        /// <summary>
        /// Can be used to generate FriendlyName for this Arduino connection.
        /// Generated name are generated as following: Arduino-1, Arduino-2, etc.
        /// TODO: Add code that will allow to generate friendly name like Arduino-1, Arduino-2, Arduino-3, etc.
        /// </summary>
        /// <returns>Generated FriendlyName for this Arduino connection</returns>
        private string GetNextAvailableFriendlyName()
        {
            return "Arduino-1";
        }

        /// <summary>
        /// Use this method to initialize connection with Arduino board.
        /// </summary>
        /// <returns>Has connection been initialized?</returns>
        public bool InitializeConnection()
        {
            if(Initialized)
                Debug.LogErrorFormat("ArduinoManager | Arduino Connection '{0}' is already initialized.", this);
            
            try
            {
                _serialStream = new SerialPort(PortName, BaudRate);
                SerialStream.ReadTimeout = ReadTimeout;
                SerialStream.RtsEnable = true;
                SerialStream.DataReceived += new SerialDataReceivedEventHandler(SerialStreamOnDataReceived);

                SerialStream.Open();

                
                Debug.LogFormat("ArduinoManager | Successfully initialized Arduino Connection '{0}'.", this);
                Initialized = true;
            }
            catch (Exception e)
            {
                Initialized = false;
                Debug.LogErrorFormat("ArduinoManager | Failed to initialize Arduino Connection. Original error:\n{0}", e.Message);
            }

            return Initialized;
        }

        public void SendMessage(string message)
        {
            _serialStream.WriteLine(message);
        }
        
        public void CloseConnection()
        {
            if (Initialized)
            {
                Debug.LogFormat("ArduinoManager | Stopping Arduino Connection '{0}'.", this);
                _serialStream.Close();
            }
        }

        private void SerialStreamOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Debug.LogFormat("Arduino Manager | Serial: {0}", _serialStream.ReadLine());
        }

        public override string ToString()
        {
            return FriendlyName;
        }
    }
}