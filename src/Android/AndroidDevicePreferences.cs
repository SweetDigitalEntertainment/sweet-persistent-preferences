using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sweet.Game.Preferences.Android
{
    public class AndroidDevicePreferences : IDevicePreferences
    {
        public string GetValue(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool HasKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public void SetValue(string key, string value)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(string key, out string value)
        {
            throw new System.NotImplementedException();
        }
    }
}