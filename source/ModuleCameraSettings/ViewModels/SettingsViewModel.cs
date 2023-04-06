using ModuleCameraSettings.Infrastructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using static ModuleCameraSettings.Enums;

namespace ModuleCameraSettings.ViewModels
{
    /// <summary>
    /// Wireless network connection setting source.
    /// </summary>
    public class SettingsViewModel : BindableBase
    {
        private const string NewLine = "\r\n";

        /// <summary>
        /// Property of SSID.
        /// </summary>
        public string Ssid
        {
            get => _ssid;
            set
            {
                if (SetProperty(ref _ssid, value))
                {
                    NotifyPropertyChanged(nameof(Valid));
                }
            }
        }
        private string _ssid = "";

        /// <summary>
        /// Property of NetworkKey.
        /// </summary>
        public string NetworkKey
        {
            get => _networkKey;
            set
            {
                if (SetProperty(ref _networkKey, value))
                {
                    NotifyPropertyChanged(nameof(Valid));
                }
            }
        }
        private string _networkKey = "";

        /// <summary>
        /// Property of CommunicationMode.
        /// </summary>
        public CommunicationMode CommunicationMode
        {
            get => _communicationMode;
            set => SetProperty(ref _communicationMode, value);
        }
        private CommunicationMode _communicationMode = CommunicationMode.Auto;

        /// <summary>
        /// Property of EncryptionMethod.
        /// </summary>
        public EncryptionMethod EncryptionMethod
        {
            get => _encryptionMethod;
            set => SetProperty(ref _encryptionMethod, value);
        }
        private EncryptionMethod _encryptionMethod = EncryptionMethod.Auto;

        /// <summary>
        /// Property of NetworkSetting.
        /// </summary>
        public NetworkSetting NetworkSetting
        {
            get => _networkSetting;
            set
            {
                if (SetProperty(ref _networkSetting, value))
                {
                    NotifyPropertyChanged(nameof(RequireIpAddress));
                    NotifyPropertyChanged(nameof(Valid));
                }
            }
        }
        private NetworkSetting _networkSetting = NetworkSetting.Dhcp;

        /// <summary>
        /// Property of IP address.
        /// </summary>
        public IpV4TextViewModel IpAddress { get; } = new IpV4TextViewModel();

        /// <summary>
        /// Property of SubnetMask.
        /// </summary>
        public IpV4TextViewModel SubnetMask { get; } = new IpV4TextViewModel();

        /// <summary>
        /// Property of DefaultGateway.
        /// </summary>
        public IpV4TextViewModel DefaultGateway { get; } = new IpV4TextViewModel();

        private IEnumerable<IpV4TextViewModel> IpV4Texts => new[] { IpAddress, SubnetMask, DefaultGateway };

        /// <summary>
        /// An IP address is required.
        /// </summary>
        public bool RequireIpAddress => NetworkSetting == NetworkSetting.Static;

        /// <summary>
        /// Returns true if each property is valid.
        /// </summary>
        public bool Valid
        {
            get
            {
                static bool IsAvailableCharacters(string text, int maxLength)
                {
                    if (text.Length < 1 || maxLength < text.Length)
                        return false;

                    return ContainsOnlyAsciiCharacters(text);
                }

                if (!IsAvailableCharacters(Ssid, 32))
                    return false;

                if (!IsAvailableCharacters(NetworkKey, 63))
                    return false;

                if (RequireIpAddress)
                {
                    foreach (var ipText in IpV4Texts)
                    {
                        if (!ipText.Valid)
                            return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public SettingsViewModel()
        {
            foreach (var ipText in IpV4Texts)
            {
                ipText.PropertyChanged += (_, __) => NotifyPropertyChanged(nameof(Valid));
            }
        }

        /// <summary>
        /// Whether the string contains only ASCII characters.
        /// </summary>
        /// <returns>only ASCII characters.</returns>
        public static bool ContainsOnlyAsciiCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            // 0x20(SP) ～ 0x7E(~)
            return Regex.IsMatch(text, @"^[\u0020-\u007E]+$");
        }

        /// <summary>
        /// Get the source text to convert to base-64 from each property.
        /// </summary>
        /// <returns>source text.</returns>
        private string GetSettingsText()
        {
            var texts = new List<string>
            {
                "001",
                Ssid,
                NetworkKey,
                ((int)CommunicationMode).ToString(),
                ((int)EncryptionMethod).ToString(),
                ((int)NetworkSetting).ToString(),
                "0"
            };

            if (RequireIpAddress)
            {
                texts.Add(IpAddress.ToText());
                texts.Add(SubnetMask.PopCount().ToString());
                texts.Add(DefaultGateway.ToText());
            }
            texts.Add("");
            return string.Join(NewLine, texts);
        }

        /// <summary>
        /// Convert the source text generated from each property to base-64
        /// and save it in a new file.
        /// </summary>
        /// <param name="path">The file to write.</param>
        public void Save(string path)
        {
            try
            {
                if (!Valid)
                    throw new InvalidOperationException("Include invalid property.");

                string sourceText = GetSettingsText();
                string base64String = Base64Converter.Encode(sourceText);

                // without BOM
                File.WriteAllText(path, base64String, new UTF8Encoding(false));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
