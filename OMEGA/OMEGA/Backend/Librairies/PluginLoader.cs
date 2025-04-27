using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace OMEGA.Backend.Librairies
{
    public class CustomPlugin
    {
        public string Name;
        public string Description;

        public MethodInfo StartMethod;
        public MethodInfo UpdateMethod;
        public MethodInfo OnClickMethod;

        public bool isTogglable;
        public bool State;
    }

    public static class PluginLoader
    {
        private static List<Assembly> _pluginAssemblies = new List<Assembly>();
        private static List<CustomPlugin> _customPlugins = new List<CustomPlugin>();
        
        private static bool CheckPluginSanity(Type plugin)
        {
            if (plugin.GetProperty("Name") == null || plugin.GetProperty("Name").PropertyType != typeof(string)) return false;
            if (plugin.GetProperty("isTogglable") == null || plugin.GetProperty("isTogglable").PropertyType != typeof(bool)) return false;
            if (plugin.GetProperty("State") == null || plugin.GetProperty("State").PropertyType != typeof(bool)) return false;

            return true;
        }

        private static CustomPlugin MakeCustomPlugin(Type plugin)
        {
            string Name = (string)plugin.GetProperty("Name").GetValue(null);
            bool isTogglable = (bool)plugin.GetProperty("isTogglable").GetValue(null);
            string Description = "No description provided";
            if (plugin.GetProperty("Description") != null && plugin.GetProperty("Description").PropertyType == typeof(string)) Description = (string)plugin.GetProperty("Description").GetValue(null);

            MethodInfo Start = plugin.GetMethod("OnClick");
            MethodInfo OnClick = plugin.GetMethod("OnClick");
            MethodInfo Update = plugin.GetMethod("OnClick");

            return new CustomPlugin
            {
                Name = Name,
                Description = Description,
                isTogglable = isTogglable,
                StartMethod = Start,
                OnClickMethod = OnClick,
                UpdateMethod = Update,
            };
        }

        public static void LoadPlugins()
        {
            _pluginAssemblies.Clear();
            _customPlugins.Clear();

            if (!Directory.Exists("Omega")) Directory.CreateDirectory("Omega");
            if (!Directory.Exists("Omega/plugins")) Directory.CreateDirectory("Omega/plugins");

            foreach (string dir in Directory.EnumerateDirectories("Omega/plugins"))
                _pluginAssemblies.Add(Assembly.Load(dir));
            
            foreach(Assembly assembly in _pluginAssemblies)
                foreach (Type type in assembly.GetTypes())
                {
                    if (!CheckPluginSanity(type)) continue;
                    _customPlugins.Add(MakeCustomPlugin(type));
                }

            Debug.Log($"Successfully loaded {_customPlugins.Count}");

            foreach (CustomPlugin plugin in _customPlugins)
                plugin.StartMethod?.Invoke(null, null);
            

        }

    }
}
