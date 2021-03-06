﻿using Ais.Internal.Dcm.ModernUIV2.Common;
using Microsoft.Shell;
using NLog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

namespace Ais.Internal.Dcm.ModernUIV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        public static FirstFloor.ModernUI.Windows.Controls.ModernFrame SelectedFrame { get; set; }
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string Unique = "My_Unique_Application_String";
        private const string JSON_DATA_TYPE = "application/json";
        private const string BASIC_AUTHENTICATION_HEADER = "Basic";

        [STAThread]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
            {
                var application = new App();

                application.InitializeComponent();

               // MediaContextHelper.FetchSearchIndex();
                application.Run();

                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            }
        }

       
        public static HttpClient GetHttpClient()
        {
            string serviceUrl = string.Empty;
            string credential = string.Empty;
            SettingHelper helper = new SettingHelper();
            serviceUrl = helper.GetUrlFromSettingFile();
            credential = helper.GetCredential();
           var handler = new HttpClientHandler { UseDefaultCredentials = true };
            

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(serviceUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_DATA_TYPE));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BASIC_AUTHENTICATION_HEADER, credential);
            return client;
        }

        #region ISingleInstanceApp Members

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            return true;
        }

        #endregion
    }
}
