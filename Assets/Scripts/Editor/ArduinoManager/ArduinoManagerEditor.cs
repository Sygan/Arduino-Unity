using UnityEditor;

namespace Sygan.ArduinoUnity.Editor
{ 
    [CustomEditor(typeof(ArduinoManager))]
    public class ArduinoManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}