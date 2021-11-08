using System;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using System.Diagnostics;


namespace PDFtoText
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
      
    private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            openFileDialog.ShowDialog();
            textBox1.Text = openFileDialog.FileName;
            try
            {
                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(openFileDialog.FileName);
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    //Read page
                    sb.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                textBox2.Text = sb.ToString();
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            string messaggioSalva = "Non puoi salvare un file vuoto";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ("txt |*.txt");
            try
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show(messaggioSalva);
                }
                else
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        StreamWriter testo = new StreamWriter(saveFileDialog.FileName, true, Encoding.Unicode);
                        ;
                        testo.WriteLine(textBox2.Text, true);
                        testo.Close();




                    }
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
      
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")

            {
                MessageBox.Show("Non c'è nulla da copiare");
            }
             else
                Clipboard.SetDataObject(textBox2.Text);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
    }

}