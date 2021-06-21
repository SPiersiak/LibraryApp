using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace LibraryApp.Mobile
{
    public static class Settings
    {
        public const string Server_Endpoint = "https://192.168.0.107:45455/api/";

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        private const string UserIdKey = "UserId_Key";
        private const string RoleKey = "Role_Key";
        private const string FirstNameKey = "FirstName_Key";
        private const string LastNameKey = "LastName_Key";
        private const string UserNameKey = "UserName_Key";
        private const string EmailKey = "Email_Key";
        private const string PasswordKey = "Password_Key";
        #endregion

        public static string Role
        {
            get
            {
                return AppSettings.GetValueOrDefault(RoleKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(RoleKey, value);
            }
        }

        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserIdKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserIdKey, value);
            }
        }
        public static string FirstName
        {
            get
            {
                return AppSettings.GetValueOrDefault(FirstNameKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FirstNameKey, value);
            }
        }
        public static string LastName
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastNameKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastNameKey, value);
            }
        }
        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault(EmailKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(EmailKey, value);
            }
        }
        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserNameKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserNameKey, value);
            }
        }
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault(PasswordKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(PasswordKey, value);
            }
        }
    }
}
