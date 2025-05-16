namespace KVK_DataAccess
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.WeatherDataTimer = new System.Windows.Forms.Timer(this.components);
            this.BuyingPrice = new System.Windows.Forms.TextBox();
            this.ComingHomeTime = new System.Windows.Forms.TextBox();
            this.BuyingPriceLabel = new System.Windows.Forms.Label();
            this.ComingHomeTimeLabel = new System.Windows.Forms.Label();
            this.ISSLabel = new System.Windows.Forms.Label();
            this.BatteryLabel = new System.Windows.Forms.Label();
            this.CurrentPricesLabel = new System.Windows.Forms.Label();
            this.BatteryStatusLabel = new System.Windows.Forms.Label();
            this.ISSValue = new System.Windows.Forms.Label();
            this.BatteryValue = new System.Windows.Forms.Label();
            this.CurrentPricesValue = new System.Windows.Forms.Label();
            this.BatteryStatusValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WeatherDataTimer
            // 
            this.WeatherDataTimer.Enabled = true;
            this.WeatherDataTimer.Interval = 4000;
            this.WeatherDataTimer.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // BuyingPrice
            // 
            this.BuyingPrice.Location = new System.Drawing.Point(168, 53);
            this.BuyingPrice.Name = "BuyingPrice";
            this.BuyingPrice.Size = new System.Drawing.Size(100, 22);
            this.BuyingPrice.TabIndex = 0;
            this.BuyingPrice.Text = "0.0035";
            // 
            // ComingHomeTime
            // 
            this.ComingHomeTime.Location = new System.Drawing.Point(168, 116);
            this.ComingHomeTime.Name = "ComingHomeTime";
            this.ComingHomeTime.Size = new System.Drawing.Size(100, 22);
            this.ComingHomeTime.TabIndex = 1;
            this.ComingHomeTime.Text = "17";
            // 
            // BuyingPriceLabel
            // 
            this.BuyingPriceLabel.AutoSize = true;
            this.BuyingPriceLabel.Location = new System.Drawing.Point(38, 56);
            this.BuyingPriceLabel.Name = "BuyingPriceLabel";
            this.BuyingPriceLabel.Size = new System.Drawing.Size(87, 16);
            this.BuyingPriceLabel.TabIndex = 2;
            this.BuyingPriceLabel.Text = "Buying price: ";
            // 
            // ComingHomeTimeLabel
            // 
            this.ComingHomeTimeLabel.AutoSize = true;
            this.ComingHomeTimeLabel.Location = new System.Drawing.Point(38, 116);
            this.ComingHomeTimeLabel.Name = "ComingHomeTimeLabel";
            this.ComingHomeTimeLabel.Size = new System.Drawing.Size(124, 16);
            this.ComingHomeTimeLabel.TabIndex = 3;
            this.ComingHomeTimeLabel.Text = "Coming home time: ";
            // 
            // ISSLabel
            // 
            this.ISSLabel.AutoSize = true;
            this.ISSLabel.Location = new System.Drawing.Point(38, 190);
            this.ISSLabel.Name = "ISSLabel";
            this.ISSLabel.Size = new System.Drawing.Size(34, 16);
            this.ISSLabel.TabIndex = 4;
            this.ISSLabel.Text = "ISS: ";
            // 
            // BatteryLabel
            // 
            this.BatteryLabel.AutoSize = true;
            this.BatteryLabel.Location = new System.Drawing.Point(38, 215);
            this.BatteryLabel.Name = "BatteryLabel";
            this.BatteryLabel.Size = new System.Drawing.Size(55, 16);
            this.BatteryLabel.TabIndex = 5;
            this.BatteryLabel.Text = "Battery: ";
            // 
            // CurrentPricesLabel
            // 
            this.CurrentPricesLabel.AutoSize = true;
            this.CurrentPricesLabel.Location = new System.Drawing.Point(38, 240);
            this.CurrentPricesLabel.Name = "CurrentPricesLabel";
            this.CurrentPricesLabel.Size = new System.Drawing.Size(95, 16);
            this.CurrentPricesLabel.TabIndex = 6;
            this.CurrentPricesLabel.Text = "Current prices: ";
            // 
            // BatteryStatusLabel
            // 
            this.BatteryStatusLabel.AutoSize = true;
            this.BatteryStatusLabel.Location = new System.Drawing.Point(38, 269);
            this.BatteryStatusLabel.Name = "BatteryStatusLabel";
            this.BatteryStatusLabel.Size = new System.Drawing.Size(93, 16);
            this.BatteryStatusLabel.TabIndex = 7;
            this.BatteryStatusLabel.Text = "Battery status: ";
            // 
            // ISSValue
            // 
            this.ISSValue.AutoSize = true;
            this.ISSValue.Location = new System.Drawing.Point(78, 190);
            this.ISSValue.Name = "ISSValue";
            this.ISSValue.Size = new System.Drawing.Size(14, 16);
            this.ISSValue.TabIndex = 8;
            this.ISSValue.Text = "0";
            // 
            // BatteryValue
            // 
            this.BatteryValue.AutoSize = true;
            this.BatteryValue.Location = new System.Drawing.Point(131, 215);
            this.BatteryValue.Name = "BatteryValue";
            this.BatteryValue.Size = new System.Drawing.Size(14, 16);
            this.BatteryValue.TabIndex = 9;
            this.BatteryValue.Text = "0";
            // 
            // CurrentPricesValue
            // 
            this.CurrentPricesValue.AutoSize = true;
            this.CurrentPricesValue.Location = new System.Drawing.Point(148, 240);
            this.CurrentPricesValue.Name = "CurrentPricesValue";
            this.CurrentPricesValue.Size = new System.Drawing.Size(14, 16);
            this.CurrentPricesValue.TabIndex = 10;
            this.CurrentPricesValue.Text = "0";
            // 
            // BatteryStatusValue
            // 
            this.BatteryStatusValue.AutoSize = true;
            this.BatteryStatusValue.Location = new System.Drawing.Point(148, 269);
            this.BatteryStatusValue.Name = "BatteryStatusValue";
            this.BatteryStatusValue.Size = new System.Drawing.Size(14, 16);
            this.BatteryStatusValue.TabIndex = 11;
            this.BatteryStatusValue.Text = "0";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(574, 392);
            this.Controls.Add(this.BatteryStatusValue);
            this.Controls.Add(this.CurrentPricesValue);
            this.Controls.Add(this.BatteryValue);
            this.Controls.Add(this.ISSValue);
            this.Controls.Add(this.BatteryStatusLabel);
            this.Controls.Add(this.CurrentPricesLabel);
            this.Controls.Add(this.BatteryLabel);
            this.Controls.Add(this.ISSLabel);
            this.Controls.Add(this.ComingHomeTimeLabel);
            this.Controls.Add(this.BuyingPriceLabel);
            this.Controls.Add(this.ComingHomeTime);
            this.Controls.Add(this.BuyingPrice);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetInfo;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer WeatherDataTimer;
        private System.Windows.Forms.TextBox BuyingPrice;
        private System.Windows.Forms.TextBox ComingHomeTime;
        private System.Windows.Forms.Label BuyingPriceLabel;
        private System.Windows.Forms.Label ComingHomeTimeLabel;
        private System.Windows.Forms.Label ISSLabel;
        private System.Windows.Forms.Label BatteryLabel;
        private System.Windows.Forms.Label CurrentPricesLabel;
        private System.Windows.Forms.Label BatteryStatusLabel;
        private System.Windows.Forms.Label ISSValue;
        private System.Windows.Forms.Label BatteryValue;
        private System.Windows.Forms.Label CurrentPricesValue;
        private System.Windows.Forms.Label BatteryStatusValue;
    }
}

