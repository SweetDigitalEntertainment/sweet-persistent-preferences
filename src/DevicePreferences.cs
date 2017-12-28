namespace Sweet.Game.Preferences
{
    public sealed class DevicePreferences : IDevicePreferences
    {
        IDevicePreferences _devicePreferences;




        public DevicePreferences()
        {
#if !UNITY_EDITOR && UNITY_IOS
            _devicePreferences = new Sweet.Game.Preferences.iOS.iOSDevicePreferences();
#elif !UNITY_EDITOR && UNITY_ANDROID
            _devicePreferences = new Sweet.Game.Preferences.Android.AndroidDevicePreferences();
#else
            _devicePreferences = new Sweet.Game.Preferences.Standalone.StandaloneDevicePreferences();
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