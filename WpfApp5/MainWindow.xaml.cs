﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using core.service;


namespace WpfApp5
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int id;
        private int date;
        private int temp;
        private int humidity;
        private object chart1;
        

        public MainWindow()
        {
            InitializeComponent();
        }



        //private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        /*private void dataGridView1_SelectedCellsChanged(object sender, DataGridRowEventArgs e)
        {
            int idsuppr = int.Parse(dataGridView1.SelectedItem[e.RowIndex].Cells[id].Value.ToString());
            textBox1.Text = idsuppr.ToString();

         

        }*/

        /**
        * use to display the file in table form
        * 
        **/
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection((@"Server=DOM\SQLEXPRESS;Database=meteodata;User=domi;Password=domi;Trusted_Connection=Yes;"));
           
            conn.Open();
            string Get_Data = "SELECT * from meteodata";

            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = Get_Data;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("meteodata");
                sda.Fill(dt);

                dataGridView1.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /**
         * use to delete the selected line
         * 
         **/
        private void button2_Click(object sender, EventArgs e)
        {
            //SqlConnection conn = new SqlConnection((@"Server=DOM\SQLEXPRESS;Database=meteodata;User=domi;Password=domi;Trusted_Connection=Yes;"));
            //conn.Open();
            //TODO format de la chaine incorrect = > à traiter
            //string Query = "DELETE FROM meteodata WHERE id=" + int.Parse(dataGridView1.ToString()) + ";";
            
            try
            {
                this.dataGridView1.Items.Remove(this.dataGridView1.SelectedItem);
                /*this.dataGridView1.Rows[e.RowIndex].Selected = true;
                this.rowIndex = e.RowIndex;
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                contextMenuStrip1.Show(Cursor.Position);*/
                MessageBox.Show("line éliminée");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        /**
         * use to generate a graph
         * 
         **/
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection((@"Server=DOM\SQLEXPRESS;Database=meteodata;User=domi;Password=domi;Trusted_Connection=Yes;"));

            Console.WriteLine("Connecting to MySQL...");
            //MessageBox.Show("connection en cours");
            conn.Open();
            Console.WriteLine("Connected");
            //MessageBox.Show("connection ok");

            
            SqlCommand query = new SqlCommand("SELECT * FROM `meteodata`", conn);

            SqlDataReader myReader;

            myReader = query.ExecuteReader();

            while (myReader.Read())
            {
                //this.chart1.Series["Temperature"].Points.AddXY(myReader.GetString(id), myReader.GetDouble(temp));
                //this.chart1.Series["Taux Humidité"].Points.AddXY(myReader.GetString(id), myReader.GetDouble(humidity));

                //LineSeries lineSeries1 = new LineSeries();
                /*lineSeries1.Title = "Title";
                lineSeries1.DependentValuePath = "Value";
                lineSeries1.IndependentValuePath = "Key";
                lineSeries1.ItemsSource = valueList;
                lineChart.Series.Add(lineSeries1);*/

            }
        }

        /**
         * use to send by email a pdf file
         * 
         **/
        private void envoiEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MailMessage email = new MailMessage();
            email.From = new MailAddress("bo76h211@gmail.com");
            email.To.Add(new MailAddress("dominique.hathi@gmail.com"));
            email.IsBodyHtml = true;
            email.Subject = "objet du mail";
            email.Body = " contenu du mail";
            //SmtpClient smtp = new SmtpClient("127.0.0.1", 587);
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("bo76h211@gmail.com", "Cesi2017");

            string file = @"C:\Users\Dominique H\Downloads\src2\test.pdf";
            // Create  the file attachment for this e-mail message.
            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add the file attachment to this e-mail message.
            email.Attachments.Add(data);

            try
            {
                smtp.Send(email);
                MessageBox.Show("émail envoyé");
            }
            catch (SmtpException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /**
         * use to export pdf file
         * 
         **/
        /*private void exportPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string number = "11161806 20170524 11585745";
            string notes = "Test 1 semaine";
            string description = "SN: 11161806";
            string interval = " 300 secondes";
            string alarm = "-20.0 , 100.0";
            int totalRecord = 11;
            string tempStat = "31.0 / 28.1 / 29.0 / 29.0";
            string humidityStat = "56.0 / 44.0 / 49.0 %";
            DateTime startTime = new DateTime(2017, 05, 24, 12, 32, 35);
            DateTime endTime = new DateTime(2017, 05, 24, 13, 57, 35);
            string totalTime = "01:25:00";

            Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C: \Users\Dominique H\Downloads\src2\test.pdf", FileMode.Create));
            doc.Open();
            Paragraph paragraph1 = new Paragraph("Fichier créé le : " + DateTime.Now);
            paragraph1.SpacingAfter = 10;

            PdfPTable table = new PdfPTable(2);
            PdfPCell cell = new PdfPCell(new Phrase("DATA REPORT"));
            cell.BackgroundColor = new BaseColor(0, 150, 0);
            cell.Colspan = 2;
            cell.HorizontalAlignment = 1;
            cell.Padding = 10;

            table.AddCell(cell);

            table.AddCell("No:");
            table.AddCell(number);
            table.AddCell("Notes: ");
            table.AddCell(notes);
            table.AddCell("Description:");
            table.AddCell(description);
            table.AddCell("Storage Interval:");
            table.AddCell(interval);
            table.AddCell("Alarm Settings:");
            table.AddCell(alarm);
            table.AddCell("Total Records:");
            table.AddCell((totalRecord).ToString());
            table.AddCell("Temp Max/ Min / Avg / MKT:");
            table.AddCell(tempStat);
            table.AddCell("Humidity Max/ Min / Avg:");
            table.AddCell(humidityStat);
            table.AddCell(" Start Time: ");
            table.AddCell(startTime.ToString());
            table.AddCell("End Time: ");
            table.AddCell(endTime.ToString());
            table.AddCell("Total Time:");
            table.AddCell(totalTime);

            Paragraph paragraph2 = new Paragraph("Temperature and humidity data");
            paragraph2.SpacingBefore = 10;
            paragraph2.SpacingAfter = 10;

            Paragraph paragraph3 = new Paragraph("Temperature and humidity graph");
            paragraph2.SpacingBefore = 10;
            paragraph2.SpacingAfter = 10;

            /* TABLEAU 
              -----------------------------------------*/
            /*PdfPTable table1 = new PdfPTable(dataGridView1.Columns.Count);

            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                table1.AddCell(new Phrase(dataGridView1.Columns[j].HeaderText));
            }

            table1.HeaderRows = 1;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int k = 0; k < dataGridView1.Columns.Count; k++)
                {
                    if (dataGridView1[k, i].Value != null)
                    {
                        table1.AddCell(new Phrase(dataGridView1[k, i].Value.ToString()));
                    }
                }
            }

            /* GRAPHIQUE
            ----------------------------------------------- */
            /*var chartimage = new MemoryStream();
            chart1.SaveImage(chartimage, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
            iTextSharp.text.Image Chart_image = iTextSharp.text.Image.GetInstance(chartimage.GetBuffer());

            /* AJOUT AU FICHIER
             * ---------------------------------------------*/
            /*doc.Add(paragraph1);
            doc.Add(table);
            doc.Add(paragraph2);
            doc.Add(table1);
            doc.Add(paragraph2);
            doc.Add(Chart_image);

            doc.Close();
        }*/

        private void button4_Click(object sender, EventArgs e)
        {

        }

        /**
         * use to export csv file
         * 
         **/

        private void exportCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportCsv test = new ExportCsv();
            test.Export();
            MessageBox.Show("votre fichier .csv a bien été céer");
        }

        /**
         * use to import txt file
         * 
         **/
        private void importTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection((@"Server=DOM\SQLEXPRESS;Database=meteodata;User=domi;Password=domi;Trusted_Connection=Yes;"));

            Console.WriteLine("Connecting to MySQL...");
            //MessageBox.Show("connection en cours");
            conn.Open();
            Console.WriteLine("Connected");
            //MessageBox.Show("connection ok");

            //string path = @"C:\Users\Dominique H\Downloads\src2\source_donnees.txt";
            string path = @"C: \Users\Dominique H\source\repos\WpfApp5\WpfApp5\source_donnees.txt";
            int counter = 0;

            String[] file = File.ReadAllLines(path);         // Read the file and display it line by line.  
            //MessageBox.Show(file[1]);

            foreach (String lines in file)
            {
                string[] entries = lines.Split(' ');

                int id = int.Parse(Regex.Replace(entries[0], "[^0-9 ]", ""));
                DateTime date = System.DateTime.Parse(entries[1] + " " + entries[2]);
                double temp = double.Parse(Regex.Replace(entries[3], "[^0-9., ]", "").Replace(".", ","));
                double humidity = double.Parse(Regex.Replace(entries[4], "[^0-9., ]", "").Replace(".", ","));
                //MessageBox.Show(id.ToString(), date.ToString());
                try
                {
                    //SqlCommand cmd = new SqlCommand("INSERT INTO meteodata (`id`, `date`, `temp`, `humidity`) VALUES(@id, @date, @temp, @humidity)", conn);
                    SqlCommand cmd = new SqlCommand("INSERT dbo.meteodata (id, date, temp, humidity) VALUES(@id, @date, @temp, @humidity)", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    //Console.WriteLine(cmd);
                    //MessageBox.Show(id.ToString(), date.ToString());
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@temp", temp);
                    cmd.Parameters.AddWithValue("@humidity", humidity);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("ko");
                }

                counter++;

            }


            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.  
            System.Console.ReadLine();
            conn.Close();
        }

        /*private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }*/

    }
}
