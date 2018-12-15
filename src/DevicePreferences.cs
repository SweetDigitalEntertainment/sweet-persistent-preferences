namespace Sweet.Game.Preferences
{
    public sealed class DevicePreferences : IDevicePreferences
    {
        IDevicePreferences _devicePreferences;


        public DevicePreferences()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            _devicePreferences = new Sweet.Game.Preferences.Standalone.StandaloneDevicePreferences();
#elif UNITY_IOS
            _devicePreferences = new Sweet.Game.Preferences.iOS.iOSDevicePreferences();
#elif UNITY_ANDROID
            _devicePreferences = new Sweet.Game.Preferences.Android.AndroidDevicePreferences();
#endif
        }


        public string GetValue(string key)
        {
            return _devicePreferences.GetValue(key);
        }

        public bool HasKey(string key)
        {
            return _devicePreferences.HasKey(key);
        }

        public void SetValue(string key, string value)
        {
            _devicePreferences.SetValue(key, value);
        }

        public bool TryGetValue(string key, out string value)
        {
            return _devicePreferences.TryGetValue(key, out value);
        }
    }
}