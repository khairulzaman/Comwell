using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.GData.Client;
using Google.GData.Spreadsheets;

namespace Comwell.Model.External
{
    public static class GApi
    {
        private static readonly string gDocsURL = "https://docs.google.com/spreadsheet/ccc?key={0}";
        public static readonly string ClientId = ConfigurationManager.AppSettings["ClientId"];
        public static readonly string ClientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        public static readonly string ClientName = "Comwell";
        public static readonly string Scope = ConfigurationManager.AppSettings["Scope"];
        public static readonly string RedirectUri = ConfigurationManager.AppSettings["RedirectUri"];
        public static string AccessToken { 
            get { 
                if (String.IsNullOrEmpty(p_AccessToken)){
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["AccessToken"]))
                        p_AccessToken = ConfigurationManager.AppSettings["AccessToken"];
                }
                else
                    p_AccessToken = String.Empty;
                return p_AccessToken;
            }
            set {
                /*
                ConfigurationManager.AppSettings.Remove("AccessToken");
                ConfigurationManager.AppSettings.Add("AccessToken", value);
                 */
                p_AccessToken = value;
            }
        }
        private static string p_AccessToken;

        public static string RefreshToken { 
            get { 
                if (String.IsNullOrEmpty(p_RefreshToken)){
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["RefreshToken"]))
                        p_RefreshToken = ConfigurationManager.AppSettings["RefreshToken"];
                }
                else
                    p_RefreshToken = String.Empty;
                return p_RefreshToken;
            }
            set {/*
                ConfigurationManager.AppSettings.Remove("RefreshToken");
                ConfigurationManager.AppSettings.Add("RefreshToken", value);
                  */
                p_RefreshToken = value;
            }
        }
        private static string p_RefreshToken;

        public static readonly string SpreadsheetSRF = ConfigurationManager.AppSettings["SpreadsheetSRF"];
        public static readonly string SpreadsheetSRFUrl = String.Format(gDocsURL, SpreadsheetSRF);
        public static readonly string SpreadsheetSubmissions = ConfigurationManager.AppSettings["SpreadsheetSubmissions"];
        public static readonly string SpreadsheetSubmissionsUrl = String.Format(gDocsURL, SpreadsheetSubmissions);

        public static OAuth2Parameters Parameters
        {
            get
            {
                if (p_parameters == null)
                {
                    OAuth2Parameters parameters = new OAuth2Parameters();
                    parameters.ClientId = ClientId;
                    parameters.ClientSecret = ClientSecret;
                    parameters.RedirectUri = RedirectUri;
                    parameters.Scope = Scope;
                    parameters.AccessToken = AccessToken;
                    parameters.RefreshToken = RefreshToken;
                    p_parameters = parameters;
                }
                return p_parameters;
            }
            set { p_parameters = value; }
        }
        private static OAuth2Parameters p_parameters;

        public static GOAuth2RequestFactory RequestFactory
        {
            get
            {
                if (p_request_factory == null)
                {
                    p_request_factory = new GOAuth2RequestFactory(null, ClientName, Parameters);
                }
                return p_request_factory;
            }
            set
            {
                p_request_factory = value;
            }
        }
        private static GOAuth2RequestFactory p_request_factory;


        public static void Authenticate()
        {
            string authorizationUrl = OAuthUtil.CreateOAuth2AuthorizationUrl(Parameters);
            Clipboard.SetText(authorizationUrl);
            Console.WriteLine("Please visit the URL copied to your clipboard to authorize your OAuth "
              + "request token.  Once that is complete, type in your access code to "
              + "continue...");
            Parameters.AccessCode = Console.ReadLine();

            OAuthUtil.GetAccessToken(Parameters);
            Console.WriteLine("OAuth Access Token: " + Parameters.AccessToken);
            Clipboard.SetText(Parameters.AccessToken);
            GApi.AccessToken = Parameters.AccessToken;
            Console.ReadLine();
            Clipboard.SetText(Parameters.AccessToken);
            Console.WriteLine("OAuth Refresh Token: " + Parameters.RefreshToken);
            GApi.RefreshToken = Parameters.RefreshToken;
            Console.ReadLine();
        }
    }
}
