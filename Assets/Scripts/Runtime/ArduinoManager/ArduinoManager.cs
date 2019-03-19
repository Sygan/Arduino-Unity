using UnityEngine;
using System.IO.Ports;

namespace gd.Sygan.ArduinoUnity
{
    [CreateAssetMenu(fileName = "ArduinoManager", menuName = "Sygan/Arduino Manager/New Arduino Manager")]
    public class ArduinoManager : ScriptableObject
    {
        [SerializeField]
        private int _serialPort = 9600;

        [SerializeField]
        private string _serialPortName = "COM4";

        [SerializeField]
        private int _timeout = 50;

        [SerializeField]
        private bool _enabledInEditMode = false;

        private SerialPort _arduinoStream;
        private bool _initialized;

        public const string ARDUINO_MANAGER_LOG_MESSAGE = "Arduino Manager | ";

        private const string ARDUINO_MANAGER_IS_ALREADY_INITIALIZED = ARDUINO_MANAGER_LOG_MESSAGE + "Arduino Manager is already initialized but the Initialize() method was invoked again.";
        private const string ARDUINO_MANAGER_HAS_BEEN_INITIALIZED = ARDUINO_MANAGER_LOG_MESSAGE + "Arduino Manager has been initialized";
        private const string TRYING_TO_INITIALIZE_ARDUINO_MANAGER = ARDUINO_MANAGER_LOG_MESSAGE + "Arduino Manager is not initialized. Trying to initialize now.";
        public bool EnabledInEditMode
        {
            get { return _enabledInEditMode; }
        }

        public void Initialize()
        {
            if (_initialized)
            {
                Debug.LogWarning(ARDUINO_MANAGER_IS_ALREADY_INITIALIZED, this);
                //return;
            }

            _initialized = true;

            _arduinoStream = new SerialPort(_serialPortName, _serialPort);

            _arduinoStream.ReadTimeout = _timeout;
            _arduinoStream.Open();
            

            Debug.Log(ARDUINO_MANAGER_HAS_BEEN_INITIALIZED);
        }

        public void Close()
        {
            _arduinoStream.Close();
        }

        public void WriteLine(string message)
        {
            TryToInitialize();
                
            _arduinoStream.WriteLine(message);
            _arduinoStream.BaseStream.Flush();
        }

        private void TryToInitialize()
        {
            if (!_initialized)
            {
                Debug.Log(TRYING_TO_INITIALIZE_ARDUINO_MANAGER);
                Initialize();
            }            
        }
    }
}