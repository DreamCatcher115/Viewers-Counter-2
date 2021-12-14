using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Viewers_Counter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            TopMost = true;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            var url = "https://facecast.net/api/v1/get_event";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.ContentType = "application/x-www-form-urlencoded";

            string data1 = "uid=9267&api_key=giRZpuROmXqrioK30cF3gKvjPSuGKKEs";

            string data2 = "&event_id=";

            string data3 = textBox1.Text;

            var data = data1 + data2 + data3;

            if (data3.Length == 0)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "Введите ID!";
            }
            else
            {
                var NumData = Convert.ToInt32(data3);

                if (NumData < 85300 || NumData > 87500)
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = "Некорректный ID";
                }
                else
                {
                    float currentSize = label1.Font.Size;
                    currentSize = 50;
                    label1.Font = new Font(label1.Font.Name, currentSize,
                    label1.Font.Style, label1.Font.Unit);
                    ErrorLabel.Visible = false;
                    button1.Visible = false;
                    label2.Visible = false;
                    textBox1.Visible = false;
                    label1.Location = new Point(15, 0);
                    // label1.Size = new Size(100, 100);
                    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    this.ControlBox = false;

                        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                        {
                            streamWriter.Write(data);
                        }
                        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            var found = result.IndexOf("viewers_now\":");
                            var FoundResult = Convert.ToString(result.Substring(found + 13, 5));
                            char[] delimiterChars = { ' ', ',' };
                            string[] words = FoundResult.Split(delimiterChars);
                            label1.Text = words[0];
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Size = new Size(400, 400);
            button1.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            label1.Location = new Point(175, 100);
            // label1.Size = new Size(100, 50);
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.Size = new Size(400, 400);
            this.ControlBox = true;
            float currentSize = label1.Font.Size;
            currentSize = 22;
            label1.Font = new Font(label1.Font.Name, currentSize,
            label1.Font.Style, label1.Font.Unit);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void FaceRequest_Tick(object sender, EventArgs e)
        {

        }
    }
}
