using System;
using System.IO;
using System.Reflection;
using IISPerformance.Models;
using Newtonsoft.Json;

namespace IISPerformance.Infrastructure
{
    /// <summary>
    /// Small utility class to read and write a JSON file for settings.
    /// </summary>
    public static class JsonSettings
    {
        public static string settingsPath;

        static JsonSettings()
        {
            // For Windows Services we have to get the working directory by examining the executing assembly
            settingsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\settings.json";
        }

        /// <summary>
        /// Static method to return a fully populated, deserilaized Settings class
        /// </summary>
        /// <returns></returns>
        public static Settings LoadSettings()
        {
            Settings settings = new Settings();

            try
            {
                if (File.Exists(settingsPath))
                {
                    var settingsData = File.ReadAllText(settingsPath);
                    settings = JsonConvert.DeserializeObject<Settings>(settingsData);
                    //_log.Info("Successfully loaded settings.json file.");
                }
                else
                {
                    // Save a new settings file if htere wasn't one there previously
                    SaveSettings(settings);
                }
            }
            catch (Exception ex)
            {
                //_log.InfoFormat("Failed to load settings.json file: {0}", ex.InnerException);
            }

            return settings;
        }

        /// <summary>
        /// Static method to serialize and save the Settings class to a settings.json file
        /// </summary>
        /// <param name="settings"></param>
        public static void SaveSettings(Settings settings)
        {
            try
            {
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(settingsPath, json);
            }
            catch (Exception ex)
            {
                //_log.InfoFormat("Failed to save settings.json file: {0}", ex.InnerException);
            }
        }
    }
}
