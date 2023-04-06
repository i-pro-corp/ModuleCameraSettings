using System.ComponentModel;

namespace ModuleCameraSettings
{
    public static class Enums
    {
        /// <summary>
        /// Defined value of CommunicationMode.
        /// </summary>
        public enum CommunicationMode
        {
            [Description("802.11n/b/g (2.4GHz)")]
            Ieee24GHz = 2,
            [Description("802.11n/a (5GHz)")]
            Ieee5GHz = 3,
            Auto = 4,
        };

        /// <summary>
        /// Defined value of EncryptionMethod.
        /// </summary>
        public enum EncryptionMethod
        {
            [Description("WPA2-PSK (AES)")]
            Wpa2Psk = 6,
            Auto = 8,
        };

        /// <summary>
        /// Defined value of NetworkSetting.
        /// </summary>
        public enum NetworkSetting
        {
            Static = 0,
            [Description("DHCP")]
            Dhcp = 1,
            [Description("Auto (AutoIP)")]
            AutoIp = 2,
            [Description("Auto (Advanced)")]
            AutoAdvanced = 3,
        };
    }
}
