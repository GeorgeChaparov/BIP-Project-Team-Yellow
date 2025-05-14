using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;

using KVK_DataAccess.EWS_PME;

using Newtonsoft.Json.Linq;


namespace KVK_DataAccess
{
    public partial class Form1 : Form
    {
        double CurrentPrices = 0;
        List<string> Forecast = new List<string>();
        List<double> m_BigSolar = new List<double>();
        List<double> m_SmallSolar = new List<double>();
        List<double> m_ISS = new List<double>();
        List<double> m_JSS = new List<double>();

        public Form1()
        {
            InitializeComponent();
            //MSSQL();
            ISS();
            JSS();
            SmallSolar();
            BigSolar();
            CalculateConsumtion();
            
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            double ISS = GetPower();
            NordPool();
           

            Console.WriteLine($"ISS: {ISS}");
            lblValue1.Text = $"{CurrentPrices} EUR/kWh";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        async void Weather(int hoursForecast)
        {
            string url = "https://api.open-meteo.com/v1/forecast?latitude=55.7033&longitude=21.1443&hourly=temperature_2m,cloudcover,direct_radiation,weathercode&timezone=Europe%2FRiga";

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);

            JsonDocument doc = JsonDocument.Parse(response);
            var root = doc.RootElement;

            var hours = root.GetProperty("hourly").GetProperty("time");
            var clouds = root.GetProperty("hourly").GetProperty("cloudcover");
            var codes = root.GetProperty("hourly").GetProperty("weathercode");

            for (int i = 0; i < hoursForecast; i++)
            {
                Forecast.Add($"[{hours[i].GetString()}] Cloud cover: {clouds[i]}%, Weather code: {codes[i]}");
            }
        }

        async void NordPool()
        {
            var client = new HttpClient();
            string url = "https://dashboard.elering.ee/api/nps/price";

            var response = await client.GetStringAsync(url);
            JObject json = JObject.Parse(response);

            var prices = json["data"]["ee"];

            foreach (var item in prices)
            {
                long timestamp = item["timestamp"].Value<long>();
                CurrentPrices = item["price"].Value<double>() / 10000;
            }
        }

        double GetPower()
        {
            var client = new DataExchangeClient();
            client.ClientCredentials.HttpDigest.ClientCredential = new NetworkCredential("kvk", "KvK-DataAccess1", "172.16.16.60");

            client.ClientCredentials.HttpDigest.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            try
            {
                // Create the request object
                var request = new GetValuesRequest
                {
                    GetValuesIds = new[] {"4@1042@V"}, // Replace with actual value ID
                    version = "1.0"
                };

                var request2 = new SetValuesRequest
                {
                    SetValuesItems = { }
                };

                // Call the service method
                var response = client.GetValues(request);

                if (response.GetValuesItems == null || response.GetValuesItems.Length == 0)
                {
                    return 0;
                }

                var ISS = response.GetValuesItems[0];
                return Convert.ToDouble(ISS.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Service Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                client.Close();
            }

            return 0;
        }

        void MSSQL()
        {
            string connectionString = "Server=172.16.16.60;Database=BIP_Project;User Id=kvk;Password=KvK-DataAccess1;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                int WhatToDoWithElectricity = 0;
                int ElectricityScenario = 5;

                // Corrected query with parameters
                string updateQuery = "UPDATE BIP_Result SET BatteryStatus = @WhatToDo, Scene = @Scenario WHERE Team_Name = 'Yellow_Team'";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // Add parameters with proper values
                    command.Parameters.AddWithValue("@WhatToDo", WhatToDoWithElectricity);
                    command.Parameters.AddWithValue("@Scenario", ElectricityScenario);

                    command.ExecuteNonQuery();
                }
            }
        }


        void ISS()
        {
            string connectionString = "Server=172.16.16.60;Database=ION_Data;User Id=kvk;Password=KvK-DataAccess1;";
            List<double> results = new List<double>(); // To store the retrieved values

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT [kW tot mean] FROM [vw_ISS-01_History_Mean]";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                m_ISS.Add(reader.GetDouble(0));
                            }
                        }
                    }
                }
            }
        }

        void JSS()
        {
            string connectionString = "Server=172.16.16.60;Database=ION_Data;User Id=kvk;Password=KvK-DataAccess1;";
            List<double> results = new List<double>(); // To store the retrieved values

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT [kW tot mean] FROM [vw_JSS-308_History_Mean]";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                m_JSS.Add(reader.GetDouble(0));
                            }
                        }
                    }
                }
            }
        }

        void SmallSolar()
        {
            string connectionString = "Server=172.16.16.60;Database=ION_Data;User Id=kvk;Password=KvK-DataAccess1;";
            List<double> results = new List<double>(); // To store the retrieved values

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT [Real Power] FROM [vw_Solar-4kW_Power_history]";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                m_SmallSolar.Add(reader.GetDouble(0));
                            }
                        }
                    }
                }
            }
        }

        void BigSolar()
        {
            string connectionString = "Server=172.16.16.60;Database=ION_Data;User Id=kvk;Password=KvK-DataAccess1;";
            List<double> results = new List<double>(); // To store the retrieved values

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT [Real Power] FROM [vw_Solar-90kW_Power_history]";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                m_BigSolar.Add(reader.GetDouble(0));
                            }
                        }
                    }
                }
            }
        }

        void CalculateConsumtion()
        {
            List<double> smallest = m_BigSolar;

            if (smallest.Count > m_SmallSolar.Count)
            {
                smallest = m_SmallSolar;
            }

            if (smallest.Count > m_ISS.Count)
            {
                smallest = m_ISS;
            }

            List<double> consumption = new List<double>(smallest);

            for (int i = 0; i < smallest.Count; i++)
            {
                if (m_ISS[i] > 0)
                {
                    consumption[i] = m_BigSolar[i] + m_SmallSolar[i] + Math.Abs(m_ISS[i]) + Math.Abs(m_JSS[i]);

                }
                else
                {
                    //consumption[i] = m_BigSolar[i] + m_SmallSolar[i] + m_JSS[i] + m_ISS[i] ;
                    consumption[i] = -5;
                }
                
            }

            int count = 0;
            while (true)
            {
                double sum = 0;
                for (int i = 0; i < 97; i++)
                {
                    count++;
                    sum += consumption[i + count];

                }

            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Weather(5);
        }
    }
}


