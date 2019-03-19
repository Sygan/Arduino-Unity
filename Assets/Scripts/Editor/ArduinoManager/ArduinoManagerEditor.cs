using UnityEditor;
using UnityEngine;

namespace gd.Sygan.ArduinoUnity.Editor
{ 
    [CustomEditor(typeof(ArduinoManager))]
    public class ArduinoManagerEditor : UnityEditor.Editor
    {
        private const string INIT_ARDUINO_MANAGER_BUTTON = "Initialize";
        private const string SETTINGS_LABEL = "Settings";
        private const string ACTIONS_LABEL = "Actions";
        private const string SCRIPT_PROPERTY_NAME = "m_Script";
        
        private const string FIND_MANAGER_QUERY = "t:ArduinoManager";
        private const string NO_ARDUINO_MANAGER_FOUND = ArduinoManager.ARDUINO_MANAGER_LOG_MESSAGE + "Arduino Manager haven't been found in project. Please create one using 'Assets/Create/Sygan/Arduino/New Arduino Manager' menu item.";
        private const string MORE_THAN_ONE_INSTANCE_FOUND = ArduinoManager.ARDUINO_MANAGER_LOG_MESSAGE + "There is more than one instance of Arduino Manager. Using first one found. Please leave only one instance in project.";
        private const string YOU_CAN_ONLY_INITIALIZE_WHEN_APPLICATION_IS_PLAYING = ArduinoManager.ARDUINO_MANAGER_LOG_MESSAGE + "You can only initialize Arduino Manager when game is in play mode.";
        
        //Use Instance property instead.
        private static ArduinoManager _instance;

        public static ArduinoManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindInstance();

                return _instance;
            }
        }
        
        public override void OnInspectorGUI()
        {
            GUILayout.Label(SETTINGS_LABEL, EditorStyles.boldLabel);
            
            serializedObject.Update();
            
            SerializedProperty iterator = serializedObject.GetIterator();
            
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                //Don't draw m_Script property.
                if(SCRIPT_PROPERTY_NAME == iterator.propertyPath)
                    continue;

                EditorGUILayout.PropertyField(iterator, true);
            }

            serializedObject.ApplyModifiedProperties();
            
            GUILayout.Label(ACTIONS_LABEL, EditorStyles.boldLabel);
            
            if (GUILayout.Button(INIT_ARDUINO_MANAGER_BUTTON))
            {
                if (!Application.isPlaying && !Instance.EnabledInEditMode)
                    Debug.LogError(YOU_CAN_ONLY_INITIALIZE_WHEN_APPLICATION_IS_PLAYING, Instance);
                else
                {
                    Instance.Initialize();
                }
            }

            if (GUILayout.Button("Close"))
            {
                Instance.Close();
            }

            if (GUILayout.Button("Send 'Hello World!'"))
            {
                Instance.WriteLine("Hello Wordl!");
            }
        }

        public static ArduinoManager FindInstance()
        {
            var managerAssets = AssetDatabase.FindAssets(FIND_MANAGER_QUERY);

            if (managerAssets == null || managerAssets.Length == 0)
            {
                Debug.LogError(NO_ARDUINO_MANAGER_FOUND);
                return null;
            }
            
            if(managerAssets.Length > 1)
                Debug.LogWarning(MORE_THAN_ONE_INSTANCE_FOUND);

            var path = AssetDatabase.GUIDToAssetPath(managerAssets[0]);            
            var manager = AssetDatabase.LoadAssetAtPath<ArduinoManager>(path);

            return manager;
        }
    }
}