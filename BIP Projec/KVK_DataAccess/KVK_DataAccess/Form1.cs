using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using KVK_DataAccess.EWS_PME;

using Newtonsoft.Json.Linq;



namespace KVK_DataAccess
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();



        }

        private void button2_Click(object sender, EventArgs e)
        {
          

            //var request = new GetWebServiceInformationRequest { };
            //try
            //{
            //    // Call the service methodvar response = client.GetWebServiceInformation(request);
            //    // Extract data from the response and display it
            //    var response = client.GetWebServiceInformation(request);
            //    if (response.GetWebServiceInformationVersion != null)
            //    {
            //        var ver = response.GetWebServiceInformationVersion;
            //        lblResult.Text = $"Service Version: {ver.MajorVersion}.{ver.MinorVersion}";
            //    }
            //    else
            //    {
            //        lblResult.Text = "No version info returned.";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error calling service: " + ex.Message);
            //}
            //finally
            //{
            //    client.Close();
            //}

            //var request = new GetItemsRequest
            //{
            //    GetItemsIds = new[] { "4@1042@V" }, // Replace with actual IDs
            //    version = "1.0", // Optional, specify if required
            //    metadata = true  // Optional, set to true if metadata is needed
            //};

            //var response = client.GetItems(request);


            //// Clear the ListBox before adding new items
            //listBoxMeasurements.Items.Clear();

            //if (response.GetItemsItems?.ValueItems != null)
            //{
            //    foreach (var item in response.GetItemsItems.ValueItems)
            //    {
            //        // Add each item's details to the ListBox
            //        listBoxMeasurements.Items.Add($"Item Name: {item.Name}, Item ID: {item.Id}");
            //    }
            //}
            //else
            //{
            //    listBoxMeasurements.Items.Add("No items returned by the service.");
            //}

            //var request = new GetContainerItemsRequest
            //{
            //    GetContainerItemsIds = new[] { "0" }, // Replace with actual IDs
            //    version = "1.0", // Optional, specify if required
            //    metadata = true  // Optional, set to true if metadata is needed
            //};

            //try
            //{
            //    // Call the service method
            //    var response = client.GetContainerItems(request);

            //    // Clear the ListBox before adding new items
            //    listBoxMeasurements.Items.Clear();

            //    if (response.GetContainerItemsItems != null)
            //    {
            //        foreach (var item in response.GetContainerItemsItems)
            //        {
            //            // Add each item's details to the ListBox
            //            listBoxMeasurements.Items.Add($"Item Name: {item.Name}, Item ID: {item.Id}");

            //            if (item.Items != null && item.Items.ContainerItems != null)
            //            {
            //                foreach (var subItem in item.Items.ContainerItems)
            //                {
            //                    // Add sub-container details to the ListBox
            //                    listBoxMeasurements.Items.Add($"  Sub-Item Name: {subItem.Name}, Sub-Item ID: {subItem.Id}");
            //                }
            //            }

            //        }
            //    }
            //    else
            //    {
            //        listBoxMeasurements.Items.Add("No container items returned by the service.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error: {ex.Message}", "Service Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    client.Close();
            //}

            //var infoRequest = new GetWebServiceInformationRequest();
            //var infoResponse = client.GetWebServiceInformation(infoRequest);

            //if (infoResponse.GetWebServiceInformationSupportedOperations != null)
            ////{
            ////    foreach (var operation in infoResponse.GetWebServiceInformationSupportedOperations)
            ////    {
            ////        listBoxMeasurements.Items.Add($"Supported Operation: {operation}");
            ////    }
            ////}
            ////else
            ////{
            ////    listBoxMeasurements.Items.Add("No supported operations returned by the service.");
            ////}

        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            SqlConnection();
            NordPool();
            Weather();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        async void Weather()
        {
            string url2 = "https://api.open-meteo.com/v1/forecast?latitude=55.7033&longitude=21.1443&hourly=temperature_2m,cloudcover,direct_radiation,weathercode&timezone=Europe%2FRiga";

            HttpClient client3 = new HttpClient();
            var response3 = await client3.GetStringAsync(url2);

            JsonDocument doc2 = JsonDocument.Parse(response3);
            var root = doc2.RootElement;

            var hours = root.GetProperty("hourly").GetProperty("time");
            var temperatures = root.GetProperty("hourly").GetProperty("temperature_2m");
            var clouds = root.GetProperty("hourly").GetProperty("cloudcover");
            var codes = root.GetProperty("hourly").GetProperty("weathercode");

            Console.WriteLine("Weather in Klaipeda (hourly forecast):");

            for (int i = 0; i < 5; i++) // show next 5 hours
            {
                Console.WriteLine($"[{hours[i].GetString()}] Temp: {temperatures[i]}°C, Cloud cover: {clouds[i]}%, Weather code: {codes[i]}");
            }

            switch (codes[1].ToString())
            {
                case "0":
                    Console.WriteLine("Clear"); break;
                default:
                    break;
            }
        }

        async void NordPool()
        {
            var client2 = new HttpClient();
            string url = "https://dashboard.elering.ee/api/nps/price";

            var response2 = await client2.GetStringAsync(url);
            JObject json = JObject.Parse(response2);

            var prices = json["data"]["ee"]; // gali būti "lt", "lv", "ee"

            foreach (var item in prices)
            {
                long timestamp = item["timestamp"].Value<long>();
                double price = item["price"].Value<double>() / 10000; // API grąžina ct/kWh

                DateTime time = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
                textBox1.Text = $"{time:yyyy-MM-dd HH:mm} - {price} EUR/kWh";
            }

        }


        void SqlConnection()
        {
            var client = new DataExchangeClient();
            client.ClientCredentials.HttpDigest.ClientCredential = new NetworkCredential("kvk", "KvK-DataAccess1", "172.30.10.11");

            client.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            try
            {
                // Create the request object
                var request = new GetValuesRequest
                {
                    GetValuesIds = new[] { "4@1042@V", "6@1042@V", "7@1042@V", "8@1042@V" }, // Replace with actual value ID
                    version = "1.0"
                };

                // Call the service method
                var response = client.GetValues(request);

                if (response.GetValuesItems != null && response.GetValuesItems.Length > 0)
                {
                    var valueItem1 = response.GetValuesItems[0];
                    lblValue1.Text = $"ISS-01 Real Power: {valueItem1.Value}";
                    var valueItem2 = response.GetValuesItems[1];
                    lblValue2.Text = $"Sun plant 90kW Real Value: {valueItem2.Value}";
                    var valueItem3 = response.GetValuesItems[2];
                    lblValue3.Text = $"JSS-308 Real Power: {valueItem3.Value}";
                    var valueItem4 = response.GetValuesItems[3];
                    lblValue4.Text = $"Sun plant 4kW Real Value: {valueItem4.Value}";
                }
                else
                {
                    lblValue1.Text = "Value: N/A";
                    lblValue2.Text = "Value: N/A";
                    lblValue3.Text = "Value: N/A";
                    lblValue4.Text = "Value: N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Service Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                client.Close();
            }
        }
    }
}


