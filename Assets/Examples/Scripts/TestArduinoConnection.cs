using System.Collections;
using UnityEngine;

namespace Sygan.ArduinoUnity.Examples
{
    public class TestArduinoConnection : MonoBehaviour
    {
        [SerializeField]
        private ArduinoDefinition definition;

        [SerializeField]
        private bool _stop;

        [SerializeField]
        private float _waitTimeBetweenMessages;

        private ArduinoConnection _connection;

        private void Start()
        {
            _connection = new ArduinoConnection(definition.BaudRate, definition.PortName, "Test-Arduino");

            _connection.InitializeConnection();

            StartCoroutine(TestConnection());
        }

        private IEnumerator TestConnection()
        {
            int i = 0;
            
            while (!_stop)
            {
                _connection.SendMessage("Test " + ++i);

                yield return new WaitForSeconds(_waitTimeBetweenMessages);
            }

            _connection.SendMessage("Stopped.");
        }

        
        private void OnDestroy()
        {
            _connection?.CloseConnection();
        }
        
    }
}