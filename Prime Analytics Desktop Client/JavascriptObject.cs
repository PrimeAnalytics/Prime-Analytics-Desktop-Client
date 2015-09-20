using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Prime_Analytics_Desktop_Client
{
    class JavascriptObject
    {
        string baseuri = "http://admin.primeanalytics.io/";

        public HttpClient Authenticate(HttpClient client)
        {
            var email = "data@primeanalytics.io";
            var password = "somepassword";

            var formContent = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("email", email),
            new KeyValuePair<string, string>("password", password)
        });

            var response = client.PostAsync(baseuri+"session/start", formContent);
            return client;
        }

        public void AnalyticsGui(int process_id)
        {
            
            HttpClient client = new HttpClient();

            client = Authenticate(client);

            client.BaseAddress = new Uri(baseuri);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/xml"));

            var url = "process/getxml/" + process_id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var xml = response.Content.ToString();

                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/xml"));

                url = "process/getdata/" + process_id;

                response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    var data = response.Content.ToString();

                    //
                    //
                    //call rapidminer gui here function(xml, data, process_id);
                    //
                    //

                    var processInfo = new ProcessStartInfo("java.exe", "-jar app.jar")
                    {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };
                    Process proc;

                    if ((proc = Process.Start(processInfo)) == null)
                    {
                        throw new InvalidOperationException("??");
                    }

                    proc.WaitForExit();
                    int exitCode = proc.ExitCode;
                    proc.Close();

                    //
                    //
                    //call rapidminer gui here function(xml, data, process_id);
                    //
                    //



                    //javascript function
                    //    var result = DesktopClient.AnalyticsGui(process_id);


                }

            }
            else
            {

            }

        }


        public void AnalyticsSave(string xml, int process_id)
        {
            HttpClient client = new HttpClient();
            client = Authenticate(client);

            client.BaseAddress = new Uri(baseuri);

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/xml"));

            var response = client.PostAsync("process/xmlsave", new StringContent(xml));

        }



    }
}
