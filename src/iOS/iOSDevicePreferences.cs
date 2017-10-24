using System.Collections;
using System.Collections.Generic;
using Sweet.Preferences;
using UnityEngine;

namespace Sweet.Game.Preferences.iOS
{
    public class iOSDevicePreferences : IDevicePreferences
    {
        public string GetValue(string key)
        {
            return UKeychain.PasswordForService("uPreferences", key);
        }

        public bool HasKey(string key)
        {
            return GetValue(key) != null;
        }

        public void SetValue(string key, string value)
        {
            UKeychain.SetPassword(value, "uPreferences", key);
        }

        public bool TryGetValue(string key, out string value)
        {
            value = GetValue(key);
            return value == null;
        }
    }
}