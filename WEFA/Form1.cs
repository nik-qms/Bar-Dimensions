using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;

namespace Wefa
{
    public partial class Form1 : Form
    {
        private int calls = 0;
        //Header to Write
        DataWriterHeader mainheader = new DataWriterHeader();
        //List of Lines to Write
        List<DataWriterLine> LineList = new List<DataWriterLine>();

        List<DataWriterLine> HistorieLineList = new List<DataWriterLine>(); 

        private int createClicks_1db = 0;

        private int createClicks_2db = 0;

        private bool save = false;

        private string mandant;
        
        public Form1()
        {
            InitializeComponent();
            //this.tabControl1.TabPages.Remove(tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill; 
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            //cB_dfn.Items.Add(-1);

            //cB_pfn.Items.Add(-1);
            
        }


        //Gather all Form Textbox Data
        private (DataWriterHeader, DataWriterLine) Form1_daten_erfassen()
        {
            int something = 9;

            DataWriterHeader headerobj = new DataWriterHeader();

            DataWriterLine lineobj = new DataWriterLine();

            headerobj.rueckmeldenummer = tB_rmn.Text;
            headerobj.zeichnungsnummer = tB_zn.Text;
            headerobj.auftragsnummer = tB_atn.Text;
            headerobj.k0008_pruefer = tB_p.Text;
            headerobj.k0014_folgenr1 = cB_dfn.Text;
            headerobj.k0015_folgenr2 = cB_pfn.Text;                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
            headerobj.k1001_werkzeugnr = tB_wzg.Text;
            headerobj.k1063_kunde = tB_k.Text;
            headerobj.k1082_mandant = tB_m.Text;


            string comma = ",";

            if (!string.IsNullOrWhiteSpace(tB_m101.Text))
            {
                string value = tB_m101.Text;
                if ((tB_m101.Text).Contains(comma)) { value = (tB_m101.Text).Replace(',', '.'); }
                lineobj.m101 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m101 = -1;
                something--;
            }
            if (!string.IsNullOrWhiteSpace(tB_m102.Text))
            {
                string value = tB_m102.Text;
                if ((tB_m102.Text).Contains(comma)) { value = (tB_m102.Text).Replace(',', '.'); }
                lineobj.m102 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m102 = -1;
                something--;
            }
            if (!string.IsNullOrWhiteSpace(tB_m103.Text))
            {
                string value = tB_m103.Text;
                if ((tB_m103.Text).Contains(comma)) { value = (tB_m103.Text).Replace(',', '.'); }
                lineobj.m103 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m103 = -1;
                something--;
            }
            if (!string.IsNullOrWhiteSpace(tB_m104.Text))
            {
                string value = tB_m104.Text;
                if ((tB_m104.Text).Contains(comma)) { value = (tB_m104.Text).Replace(',', '.'); }
                lineobj.m104 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m104 = -1;
                something--;
            }
            if (!string.IsNullOrWhiteSpace(tB_m105.Text))
            {
                string value = tB_m105.Text;
                if ((tB_m105.Text).Contains(comma)) { value = (tB_m105.Text).Replace(',', '.'); }
                lineobj.m105 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m105 = -1;
                something--;
            }
            if (!string.IsNullOrWhiteSpace(tB_m106.Text))
            {
                string value = tB_m106.Text;
                if ((tB_m106.Text).Contains(comma)) { value = (tB_m106.Text).Replace(',', '.'); }
                lineobj.m106 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m106 = -1;
                something--;
            }
            if (!string.IsNullOrWhiteSpace(tB_m9rmin.Text))
            {
                string value = tB_m9rmin.Text;
                if ((tB_m9rmin.Text).Contains(comma)) { value = (tB_m9rmin.Text).Replace(',', '.'); }
                lineobj.Rmin = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.Rmin = -1;
                something--;
            }
            if (!string.IsNullOrWhiteSpace(tB_m9rmax.Text))
            {
                string value = tB_m9rmax.Text;
                if ((tB_m9rmax.Text).Contains(comma)) { value = (tB_m9rmax.Text).Replace(',', '.'); }
                lineobj.Rmax = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.Rmax = -1;
                something--;
            }
            if (!string.IsNullOrWhiteSpace(tB_m9ra.Text))
            {
                string value = tB_m9ra.Text;
                if ((tB_m9ra.Text).Contains(comma)) {value =  (tB_m9ra.Text).Replace(',', '.'); }
                lineobj.Raussen = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.Raussen = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB_sm101.Text))
            {
                string value = (tB_sm101.Text).Replace(',', '.');
                lineobj.sm101 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB_sm102.Text))
            {
                string value = (tB_sm102.Text).Replace(',', '.');
                lineobj.sm102 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB_sm103.Text))
            {
                string value = (tB_sm103.Text).Replace(',', '.');
                lineobj.sm103 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB_sm104.Text))
            {
                string value = (tB_sm104.Text).Replace(',', '.');
                lineobj.sm104 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB_sm105.Text))
            {
                string value = (tB_sm105.Text).Replace(',', '.');
                lineobj.sm105 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB_sm106.Text))
            {
                string value = (tB_sm106.Text).Replace(',', '.');
                lineobj.sm106 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB_sm9rmin.Text))
            {
                string value = (tB_sm9rmin.Text).Replace(',', '.');
                lineobj.sRmin = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB_sm9rmax.Text))
            {
                string value = (tB_sm9rmax.Text).Replace(',', '.');
                lineobj.sRmax = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB_sm9ra.Text))
            {
                string value = (tB_sm9ra.Text).Replace(',', '.');
                lineobj.sRaussen = decimal.Parse(value, CultureInfo.InvariantCulture);
            }

            headerobj.k0100_anzahlmm = something;

            return (headerobj, lineobj);

        }

        private void button_create_dfq_Click(object sender, EventArgs e)
        {
            if (LineList.Any())
            {
                DataWriterHeader headerobj = new DataWriterHeader();

                DataWriterLine lineobj = new DataWriterLine();

                string pathToData;

                (headerobj, lineobj) = Form1_daten_erfassen();

                pathToData = ConfigurationManager.AppSettings["gSavePath"] + "\\" + headerobj.rueckmeldenummer + "_" + createClicks_1db + ".dfq";               
                
                headerobj.k0008_pruefer = "K0008 " + headerobj.k0008_pruefer;
                headerobj.k0014_folgenr1 = "K0014 " + headerobj.k0014_folgenr1;
                headerobj.k0015_folgenr2 = "K0015 " + headerobj.k0015_folgenr2;
                headerobj.k1001_werkzeugnr = "K1001 " + headerobj.k1001_werkzeugnr;
                headerobj.k1063_kunde = "K1063 " + headerobj.k1063_kunde;
                headerobj.k1082_mandant = "K1082 " + headerobj.k1082_mandant;


                using (var writer = new StreamWriter(pathToData))
                using (var csv = new CsvWriter(writer))
                {
                    csv.Configuration.Delimiter = "";
                    csv.WriteField("K0100 " + "9" + "\r\n", false);
                    csv.WriteField(headerobj.k1063_kunde + "\r\n", false);
                    csv.WriteField(headerobj.k1001_werkzeugnr + "\r\n", false);
                    csv.WriteField(headerobj.k0014_folgenr1 + "\r\n", false);
                    csv.WriteField(headerobj.k0015_folgenr2 + "\r\n", false);
                    csv.WriteField(headerobj.k0008_pruefer + "\r\n", false);
                    csv.WriteField(headerobj.k1082_mandant + "\r\n", false);
                    csv.WriteField("K2002/1" + " M10_1" + "\r\n", false);
                    csv.WriteField("K2002/2" + " M10_2" + "\r\n", false);
                    csv.WriteField("K2002/3" + " M10_3" + "\r\n", false);
                    csv.WriteField("K2002/4" + " M10_4" + "\r\n", false);
                    csv.WriteField("K2002/5" + " M10_5" + "\r\n", false);
                    csv.WriteField("K2002/6" + " M10_6" + "\r\n", false);
                    csv.WriteField("K2002/7" + " M9_Rmin" + "\r\n", false);
                    csv.WriteField("K2002/8" + " M9_Rmax" + "\r\n", false);
                    csv.WriteField("K2002/9" + " M9_Raussen" + "\r\n", false);
                }

                List<string> newLines = new List<string>();
                string datenow = DateTime.Now.ToString("dd.MM.yyyy");
                string timenow = DateTime.Now.ToString("HH:mm:ss");

                

                foreach (DataWriterLine result in LineList)
                {
                    
                    string m101 = result.m101.ToString("F4", CultureInfo.InvariantCulture);                    
                    string m102 = result.m102.ToString("F4", CultureInfo.InvariantCulture);                    
                    string m103 = result.m103.ToString("F4", CultureInfo.InvariantCulture);                    
                    string m104 = result.m104.ToString("F4", CultureInfo.InvariantCulture);                    
                    string m105 = result.m105.ToString("F4", CultureInfo.InvariantCulture);                    
                    string m106 = result.m106.ToString("F4", CultureInfo.InvariantCulture);                     
                    string Rmin = result.Rmin.ToString("F4", CultureInfo.InvariantCulture);                   
                    string Rmax = result.Rmax.ToString("F4", CultureInfo.InvariantCulture);                    
                    string Raussen = result.Raussen.ToString("F4", CultureInfo.InvariantCulture);
                    



                    string formatted = m101 + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                           m102 + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                m103 + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                    m104 + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                        m105 + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                            m106 + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                Rmin + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                    Rmax + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                        Raussen + "\u00140\u0014" + datenow + "/" + timenow + "\u000F";

                    newLines.Add(formatted);
                }
                File.AppendAllLines(pathToData, newLines);
                //LineList.Clear();
                MessageBox.Show("Das Datei wurde erfolgreich angelegt bei " + pathToData);
                //Button_new_rmn_Click(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Keine Messwerte vorhanden.");
            }
            

        }
        //Regexcontrolling Entered Characters
        private void tB_rmn_KeyPress(object sender, KeyPressEventArgs e)
        {
            //      var regex = new Regex(@"[^a-zA-Z0-9\b\s]");
            //      if (regex.IsMatch(e.KeyChar.ToString()))
            //      {
            //          e.Handled = true;
            //      }
        }
       
        
      

        //Enter Press on Rückmeldenummer Implementation
        private void tB_rmnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //button1.PerformClick();
                Button_Lupe_Click(this, new EventArgs());
                e.Handled = true;
            }
        }
        
        //Clear Form
        private void Button_new_rmn_Click(object sender, EventArgs e)
        {
            if (save == false)
            {
                DialogResult dialogResult = MessageBox.Show("Möchten Sie alle Felder leeren?", "Neue Datenblatt", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Action<Control.ControlCollection> func = null;

                    func = (controls) =>
                    {
                        foreach (Control control in controls)
                            if (control is TextBox)
                                (control as TextBox).Clear();
                            else if (control is ComboBox)
                                (control as ComboBox).SelectedIndex = -1;
                            else
                                func(control.Controls);
                    };

                    func(Controls);

                    cB_dfn.SelectedIndex = -1;

                    cB_pfn.SelectedIndex = -1;

                    cB_dfn.Items.Clear();

                    cB_pfn.Items.Clear();

                    LineList.Clear();
                    HistorieLineList.Clear();

                    createClicks_1db = 0;

                    save = false;
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            else
            {
                Action<Control.ControlCollection> func = null;

                func = (controls) =>
                {
                    foreach (Control control in controls)
                        if (control is TextBox)
                            (control as TextBox).Clear();
                        else if (control is ComboBox)
                            (control as ComboBox).SelectedIndex = -1;
                        else
                            func(control.Controls);
                };

                func(Controls);

                cB_dfn.SelectedIndex = -1;

                cB_pfn.SelectedIndex = -1;

                cB_dfn.Items.Clear();

                cB_pfn.Items.Clear();

                LineList.Clear();
                HistorieLineList.Clear();

                createClicks_1db = 0;

                save = false;
            }
            // TODO: This line of code loads data into the 'wefaDataSet2._MAiT_MM_2DB_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_2DB_MITTELSTANDTableAdapter.Fill(this.wefaDataSet2._MAiT_MM_2DB_MITTELSTAND);
            // TODO: This line of code loads data into the 'wefaDataSet1._MAiT_MM_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_MITTELSTANDTableAdapter2.Fill(this.wefaDataSet1._MAiT_MM_MITTELSTAND);
            this.mAiTMMMITTELSTANDBindingSource.ResetBindings(false);
            this.dataGridView1.Refresh();
           this.dataGridView1.Parent.Refresh();
        }

        //Add Line to LineList (Header and Body)
        private void Button_new_entry_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tB_rmn.Text))
            {
                string errorMessage = "Bitte tragen eine Rückmeldenummer ein";
                MessageBox.Show(errorMessage);
            }
            else
            {                
                // Add New Line Button +
                //New Line added to Caché 
                DataWriterHeader headerobj = new DataWriterHeader();

                DataWriterLine lineobj = new DataWriterLine();

                //Form Feld Daten erfassen
                (headerobj, lineobj) = Form1_daten_erfassen();
                LineList.Clear();
                LineList.Add(lineobj);

                if (!string.IsNullOrWhiteSpace((headerobj.k0008_pruefer)))
                {
                    if (headerobj.k0100_anzahlmm == 9)
                    {
                        button_create_dfq_Click(this, new EventArgs());
                        Button_save_Click(this, new EventArgs());

                        createClicks_1db++;

                        mainheader = headerobj;
                        LineList.Clear();
                        LineList.Add(lineobj);

                        tB_m101.Clear();
                        tB_m102.Clear();
                        tB_m103.Clear();
                        tB_m104.Clear();
                        tB_m105.Clear();
                        tB_m106.Clear();
                        tB_m9rmin.Clear();
                        tB_m9rmax.Clear();
                        tB_m9ra.Clear();

                       
                    }
                    else
                    {
                        string errorMessage = "Datensatz nicht vollständig. Eintrag nicht angelegt.";
                        MessageBox.Show(errorMessage);
                    }
                }
                else
                {
                    string errorMessage = "Bitte Prüfer Name mit eingeben.";
                    MessageBox.Show(errorMessage);
                }
                
            }
            // TODO: This line of code loads data into the 'wefaDataSet2._MAiT_MM_2DB_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_2DB_MITTELSTANDTableAdapter.Fill(this.wefaDataSet2._MAiT_MM_2DB_MITTELSTAND);
            // TODO: This line of code loads data into the 'wefaDataSet1._MAiT_MM_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_MITTELSTANDTableAdapter2.Fill(this.wefaDataSet1._MAiT_MM_MITTELSTAND);
            this.mAiTMMMITTELSTANDBindingSource.ResetBindings(false);
            this.dataGridView1.Refresh();
           
        }
                    
        //Save to Cache/Historie
        private void Button_save_Click(object sender, EventArgs e)
        {
            save = true;

            if (string.IsNullOrWhiteSpace(tB_rmn.Text))
            {
                string errorMessage = "Bitte tragen eine Rückmeldenummer ein";
                MessageBox.Show(errorMessage);
            }
            else
            {
                int dataLines = LineList.Count;

                //Are there any existing Data Sätze?
                if (dataLines == 0)
                {
                    DataWriterLine lineobj = new DataWriterLine();

                    DataWriterHeader headerobj = new DataWriterHeader();

                    (headerobj, lineobj) = Form1_daten_erfassen();
                    LineList.Add(lineobj);
                    // Button_new_entry_Click(this, new EventArgs());

                    if (headerobj.k0100_anzahlmm == 0)
                    {
                        //
                    }
                    //At least 1 measurement entered
                    else
                    {
                        foreach (DataWriterLine line in LineList)
                        {
                            QmsDFQWriter.SaveCachetoDB(headerobj, line);
                        }
                        string message = "Auftrag wurde gespeichert";
                        MessageBox.Show(message);
                        LineList.Clear();
                        //DialogResult result = MessageBox.Show("Aktuelle Inhalt behalten?", "Neue Auftrag", MessageBoxButtons.YesNo);
                        //if (result == DialogResult.Yes)
                        //{
                        //    LineList.Clear();
                        //}
                        //if (result == DialogResult.No)
                        //{
                        //    Button_new_rmn_Click(this, new EventArgs());
                        //}



                    }
                   
                }
                else
                {
                    DataWriterLine lineobj = new DataWriterLine();

                    DataWriterHeader headerobj = new DataWriterHeader();

                    (headerobj, lineobj) = Form1_daten_erfassen();

                    foreach (DataWriterLine line in LineList)
                    {
                        QmsDFQWriter.SaveCachetoDB(headerobj, line);
                    }
                    string errorMessage = "Auftrag wurde gespeichert";
                    MessageBox.Show(errorMessage);

                    //this._MAiT_MM_MITTELSTANDTableAdapter2.Fill(this.wefaDataSet1._MAiT_MM_MITTELSTAND);
                    //DialogResult result = MessageBox.Show("Aktuelle Inhalt behalten?", "Neue Auftrag", MessageBoxButtons.YesNo);
                    //if (result == DialogResult.Yes)
                    //{
                    //
                    //}
                    //if (result == DialogResult.No)
                    //{
                    //    Button_new_rmn_Click(this, new EventArgs());
                    //}
                    LineList.Clear();
                                                          
                }
            }
            // TODO: This line of code loads data into the 'wefaDataSet2._MAiT_MM_2DB_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_2DB_MITTELSTANDTableAdapter.Fill(this.wefaDataSet2._MAiT_MM_2DB_MITTELSTAND);
            // TODO: This line of code loads data into the 'wefaDataSet1._MAiT_MM_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_MITTELSTANDTableAdapter2.Fill(this.wefaDataSet1._MAiT_MM_MITTELSTAND);
            this.mAiTMMMITTELSTANDBindingSource.ResetBindings(false);
            this.dataGridView1.Refresh();
            

        }




        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'wefaDataSet2._MAiT_MM_2DB_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_2DB_MITTELSTANDTableAdapter.Fill(this.wefaDataSet2._MAiT_MM_2DB_MITTELSTAND);
            // TODO: This line of code loads data into the 'wefaDataSet1._MAiT_MM_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_MITTELSTANDTableAdapter2.Fill(this.wefaDataSet1._MAiT_MM_MITTELSTAND);

        }





        //Messprotokoll 2
        private (DataWriterHeader, DataWriterLine) Form12_daten_erfassen()
        {
            int something = 19;

            DataWriterHeader headerobj = new DataWriterHeader();

            DataWriterLine lineobj = new DataWriterLine();

            headerobj.rueckmeldenummer = tB2_rmn.Text;
            headerobj.zeichnungsnummer = tB2_zn.Text;
            headerobj.auftragsnummer = tB2_atn.Text;
            headerobj.k0008_pruefer = tB2_p.Text;
            headerobj.k0014_folgenr1 = cB2_dfn.Text;
            headerobj.k0015_folgenr2 = cB2_pfn.Text;
            headerobj.k1001_werkzeugnr = tB2_wzg.Text;
            headerobj.k1063_kunde = tB2_k.Text;
            headerobj.k1082_mandant = tB2_m.Text;
            string comma = ",";


            if (!string.IsNullOrWhiteSpace(tB2_m101.Text))
            {
                string value = tB2_m101.Text;
                if ((tB2_m101.Text).Contains(comma)) { value = (tB2_m101.Text).Replace(',', '.'); }
                lineobj.m101 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m101 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_m102.Text))
            {
                string value = tB2_m102.Text;
                if ((tB2_m102.Text).Contains(comma)) { value = (tB2_m102.Text).Replace(',', '.'); }
                lineobj.m102 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m102 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_m103.Text))
            {
                string value = tB2_m103.Text;
                if ((tB2_m103.Text).Contains(comma)) { value = (tB2_m103.Text).Replace(',', '.'); }
                lineobj.m103 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m103 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_m104.Text))
            {
                string value = tB2_m104.Text;
                if ((tB2_m104.Text).Contains(comma)) { value = (tB2_m104.Text).Replace(',', '.'); }
                lineobj.m104 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m104 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_m105.Text))
            {
                string value = tB2_m105.Text;
                if ((tB2_m105.Text).Contains(comma)) { value = (tB2_m105.Text).Replace(',', '.'); }
                lineobj.m105 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m105 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_m106.Text))
            {
                string value = tB2_m106.Text;
                if ((tB2_m106.Text).Contains(comma)) { value = (tB2_m106.Text).Replace(',', '.'); }
                lineobj.m106 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m106 = -1;
                something--;
            }
            //////
            if (!string.IsNullOrWhiteSpace(tB2_m1221.Text))
            {
                string value = tB2_m1221.Text;
                if ((tB2_m1221.Text).Contains(comma)) { value = (tB2_m1221.Text).Replace(',', '.'); }
                lineobj.m1221 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m1221 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_m1011.Text))
            {
                string value = tB2_m1011.Text;
                if ((tB2_m1011.Text).Contains(comma)) { value = (tB2_m1011.Text).Replace(',', '.'); }
                lineobj.m1011 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m1011 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_m1012.Text))
            {
                string value = tB2_m1012.Text;
                if ((tB2_m1012.Text).Contains(comma)) { value = (tB2_m1012.Text).Replace(',', '.'); }
                lineobj.m1012 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m1012 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_m1013.Text))
            {
                string value = tB2_m1013.Text;
                if ((tB2_m1013.Text).Contains(comma)) { value = (tB2_m1013.Text).Replace(',', '.'); }
                lineobj.m1013 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m1013 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_m1014.Text))
            {
                string value = tB2_m1014.Text;
                if ((tB2_m1014.Text).Contains(comma)) { value = (tB2_m1014.Text).Replace(',', '.'); }
                lineobj.m1014 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m1014 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_m1015.Text))
            {
                string value = tB2_m1015.Text;
                if ((tB2_m1015.Text).Contains(comma)) { value = (tB2_m1015.Text).Replace(',', '.'); }
                lineobj.m1015 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m1015 = -1;
                something--;
            }
            if (!string.IsNullOrWhiteSpace(tB2_m1016.Text))
            {
                string value = tB2_m1016.Text;
                if ((tB2_m1016.Text).Contains(comma)) { value = (tB2_m1016.Text).Replace(',', '.'); }
                lineobj.m1016 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.m1016 = -1;
                something--;
            }

            //////
            if (!string.IsNullOrWhiteSpace(tB2d1_m9rmin.Text))
            {
                string value = tB2d1_m9rmin.Text;
                if ((tB2d1_m9rmin.Text).Contains(comma)) { value = (tB2d1_m9rmin.Text).Replace(',', '.'); }
                lineobj.Rmin = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.Rmin = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2d1_m9rmax.Text))
            {
                string value = tB2d1_m9rmax.Text;
                if ((tB2d1_m9rmax.Text).Contains(comma)) { value = (tB2d1_m9rmax.Text).Replace(',', '.'); }
                lineobj.Rmax = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.Rmax = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2d1_m9ra.Text))
            {
                string value = tB2d1_m9ra.Text;
                if ((tB2d1_m9ra.Text).Contains(comma)) { value = (tB2d1_m9ra.Text).Replace(',', '.'); }
                lineobj.Raussen = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.Raussen = -1;
                something--;
            }


            if (!string.IsNullOrWhiteSpace(tB2d2_m9rmin.Text))
            {
                string value = tB2d2_m9rmin.Text;
                if ((tB2d2_m9rmin.Text).Contains(comma)) { value = (tB2d2_m9rmin.Text).Replace(',', '.'); }
                lineobj.Rmin2 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.Rmin2 = -1;
                something--;
            }


            if (!string.IsNullOrWhiteSpace(tB2d2_m9rmax.Text))
            {
                string value = tB2d2_m9rmax.Text;
                if ((tB2d2_m9rmax.Text).Contains(comma)) { value = (tB2d2_m9rmax.Text).Replace(',', '.'); }
                lineobj.Rmax2 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.Rmax2 = -1;
                something--;
            }


            if (!string.IsNullOrWhiteSpace(tB2d2_m9ra.Text))
            {
                string value = tB2d2_m9ra.Text;
                if ((tB2d2_m9ra.Text).Contains(comma)) { value = (tB2d2_m9ra.Text).Replace(',', '.'); }
                lineobj.Raussen2 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            else
            {
                lineobj.Raussen2 = -1;
                something--;
            }

            if (!string.IsNullOrWhiteSpace(tB2_sm101.Text))
            {
                string value = (tB2_sm101.Text).Replace(',', '.');
                lineobj.sm101 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm102.Text))
            {
                string value = (tB2_sm102.Text).Replace(',', '.');
                lineobj.sm102 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm103.Text))
            {
                string value = (tB2_sm103.Text).Replace(',', '.');
                lineobj.sm103 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm104.Text))
            {
                string value = (tB2_sm104.Text).Replace(',', '.');
                lineobj.sm104 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm105.Text))
            {
                string value = (tB2_sm105.Text).Replace(',', '.');
                lineobj.sm105 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm106.Text))
            {
                string value = (tB2_sm106.Text).Replace(',', '.');
                lineobj.sm106 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm1011.Text))
            {
                string value = (tB2_sm1011.Text).Replace(',', '.');
                lineobj.sm1011 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm1012.Text))
            {
                string value = (tB2_sm1012.Text).Replace(',', '.');
                lineobj.sm1012 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm1013.Text))
            {
                string value = (tB2_sm1013.Text).Replace(',', '.');
                lineobj.sm1013 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm1014.Text))
            {
                string value = (tB2_sm1014.Text).Replace(',', '.');
                lineobj.sm1014 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm1015.Text))
            {
                string value = (tB2_sm1015.Text).Replace(',', '.');
                lineobj.sm1015 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm1016.Text))
            {
                string value = (tB2_sm1016.Text).Replace(',', '.');
                lineobj.sm1016 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2_sm1221.Text))
            {
                string value = (tB2_sm1221.Text).Replace(',', '.');
                lineobj.sm1221 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2d1_sm9rmin.Text))
            {
                string value = (tB2d1_sm9rmin.Text).Replace(',', '.');
                lineobj.sRmin = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2d1_sm9rmax.Text))
            {
                string value = (tB2d1_sm9rmax.Text).Replace(',', '.');
                lineobj.sRmax = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2d1_sm9ra.Text))
            {
                string value = (tB2d1_sm9ra.Text).Replace(',', '.');
                lineobj.sRaussen = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2d2_sm9rmin.Text))
            {
                string value = (tB2d2_sm9rmin.Text).Replace(',', '.');
                lineobj.sRmin2 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2d2_sm9rmax.Text))
            {
                string value = (tB2d2_sm9rmax.Text).Replace(',', '.');
                lineobj.sRmax2 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(tB2d2_sm9ra.Text))
            {
                string value = (tB2d2_sm9ra.Text).Replace(',', '.');
                lineobj.sRaussen2 = decimal.Parse(value, CultureInfo.InvariantCulture);
            }

            headerobj.k0100_anzahlmm = something;

            return (headerobj, lineobj);

        }

        private void button_create_dfq_2_Click(object sender, EventArgs e)  
        {
            if (LineList.Any())
            {
                DataWriterHeader headerobj = new DataWriterHeader();

                DataWriterLine lineobj = new DataWriterLine();

                string pathToData;

                (headerobj, lineobj) = Form12_daten_erfassen();

                pathToData = ConfigurationManager.AppSettings["gSavePath"] + "\\" + headerobj.rueckmeldenummer + "_" + createClicks_2db + ".dfq";                

                headerobj.k0008_pruefer = "K0008 " + headerobj.k0008_pruefer;
                headerobj.k0014_folgenr1 = "K0014 " + headerobj.k0014_folgenr1;
                headerobj.k0015_folgenr2 = "K0015 " + headerobj.k0015_folgenr2;
                headerobj.k1001_werkzeugnr = "K1001 " + headerobj.k1001_werkzeugnr;
                headerobj.k1063_kunde = "K1063 " + headerobj.k1063_kunde;
                headerobj.k1082_mandant = "K1082 " + headerobj.k1082_mandant;

                using (var writer = new StreamWriter(pathToData))
                using (var csv = new CsvWriter(writer))
                {
                    csv.Configuration.Delimiter = "";
                    csv.WriteField("K0100 " + "19" + "\r\n", false);
                    csv.WriteField(headerobj.k1063_kunde + "\r\n", false);
                    csv.WriteField(headerobj.k1001_werkzeugnr + "\r\n", false);
                    csv.WriteField(headerobj.k0014_folgenr1 + "\r\n", false);
                    csv.WriteField(headerobj.k0015_folgenr2 + "\r\n", false);
                    csv.WriteField(headerobj.k0008_pruefer + "\r\n", false);
                    csv.WriteField(headerobj.k1082_mandant + "\r\n", false);
                    csv.WriteField("K2002/1" + " M10_1" + "\r\n", false);
                    csv.WriteField("K2002/2" + " M10_2" + "\r\n", false);
                    csv.WriteField("K2002/3" + " M10_3" + "\r\n", false);
                    csv.WriteField("K2002/4" + " M10_4" + "\r\n", false);
                    csv.WriteField("K2002/5" + " M10_5" + "\r\n", false);
                    csv.WriteField("K2002/6" + " M10_6" + "\r\n", false);
                    csv.WriteField("K2002/7" + " M9_Rmin" + "\r\n", false);
                    csv.WriteField("K2002/8" + " M9_Rmax" + "\r\n", false);
                    csv.WriteField("K2002/9" + " M9_Raussen" + "\r\n", false);
                    csv.WriteField("K2002/10" + " M12_21" + "\r\n", false);
                    csv.WriteField("K2002/11" + " M10_11" + "\r\n", false);
                    csv.WriteField("K2002/12" + " M10_12" + "\r\n", false);
                    csv.WriteField("K2002/13" + " M10_13" + "\r\n", false);
                    csv.WriteField("K2002/14" + " M10_14" + "\r\n", false);
                    csv.WriteField("K2002/15" + " M10_15" + "\r\n", false);
                    csv.WriteField("K2002/15" + " M10_16" + "\r\n", false);
                    csv.WriteField("K2002/16" + " M9_Rmin1" + "\r\n", false);
                    csv.WriteField("K2002/17" + " M9_Rmax2" + "\r\n", false);
                    csv.WriteField("K2002/18" + " M9_Raussen2" + "\r\n", false);
                }

                List<string> newLines = new List<string>();
                string datenow = DateTime.Now.ToString("dd.MM.yyyy");
                string timenow = DateTime.Now.ToString("HH:mm:ss");

                foreach (DataWriterLine result in LineList)
                {
                    string formatted = result.m101.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                            result.m102.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                result.m103.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                    result.m104.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                        result.m105.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                            result.m106.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                result.Rmin.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                    result.Rmax.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                        result.Raussen.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                            result.m1221.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                               result.m1011.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                                 result.m1012.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                                     result.m1013.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                                         result.m1014.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                                             result.m1015.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                                                 result.m1016.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                                                    result.Rmin2.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                                                        result.Rmax2.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F" +
                                                                                                            result.Raussen2.ToString("F4", CultureInfo.InvariantCulture) + "\u00140\u0014" + datenow + "/" + timenow + "\u000F";

                    newLines.Add(formatted);
                }
                File.AppendAllLines(pathToData, newLines);
                //LineList.Clear();
                MessageBox.Show("Das Datei wurde erfolgreich angelegt bei " + pathToData);

                //Button_new_rmn_2_Click(this, new EventArgs());
            }
            else
            {
                //MessageBox.Show("Keine Messwerte vorhanden.");
            }

        }

        private void button_Lupe_2_Click(object sender, EventArgs e)
        {

            DataWriterHeader headerObj = new DataWriterHeader();

            //existiert bereit in Historie? Historie Check 
            //(LineList, headerObj, choiceFlag) = QmsDb.GetHistorieSet(tB_rmn.Text); 

            List<KopfDatenAbfrageDto> KopfDataList = new List<KopfDatenAbfrageDto>();

            List<SerienNrAbfrageDto> SerienDataList = new List<SerienNrAbfrageDto>();

            List<FolgeNummerDto> FolgeNummerList = new List<FolgeNummerDto>();

            

            if (string.IsNullOrWhiteSpace(tB2_rmn.Text))
            {

                string errorMessage = "Bitte tragen eine Rückmeldenummer ein";
                MessageBox.Show(errorMessage);
            }
            else
            {
                //read Kopfdata
                KopfDataList = QmsDb.GetKopfResultsSet(tB2_rmn.Text);
                //read serial data
                SerienDataList = QmsDb.GetSerienResultsSet(tB2_rmn.Text);

                

                if (KopfDataList.Any())
                {
                    foreach (KopfDatenAbfrageDto result in KopfDataList)
                    {
                        tB2_m.Text = (get_mandant_nummer(result.mandant.ToString()).ToString()); 
                        if (result.mmBez.Equals("M10_1"))
                        {
                            //Search for Mandant
                            mandant = result.mandant.ToString();
                            tB2_sm101.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2_m101ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m101ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }

                        else if (result.mmBez.Equals("M10_2"))
                        {
                            tB2_m102ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m102ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                            tB2_sm102.Text = result.sollMass.ToString("F4").Replace(".", ",");
                        }

                        else if (result.mmBez.Equals("M10_3"))
                        {
                            tB2_sm103.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2_m103ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m103ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M10_4"))
                        {
                            tB2_sm104.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2_m104ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m104ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M10_5"))
                        {
                            tB2_sm105.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2_m105ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m105ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M10_6"))
                        {
                            tB2_sm106.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2_m106ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m106ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        
                        else if (result.mmBez.Equals("M10_11"))
                        {
                            
                            tB2_sm1011.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2_m1011ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m1011ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }

                        else if (result.mmBez.Equals("M10_12"))
                        {
                            tB2_m1012ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m1012ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                            tB2_sm1012.Text = result.sollMass.ToString("F4").Replace(".", ",");
                        }

                        else if (result.mmBez.Equals("M10_13"))
                        {
                            tB2_sm1013.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2_m1013ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m1013ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M10_14"))
                        {
                            tB2_sm1014.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2_m1014ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m1014ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M10_15"))
                        {
                            tB2_sm1015.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2_m1015ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m1015ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M10_16"))
                        {
                            tB2_sm1016.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2_m1016ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2_m1016ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M9_Rmin1"))
                        {
                            tB2d1_sm9rmin.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2d1_m9rminot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2d1_m9rminut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M9_Rmax1"))
                        {
                            tB2d1_sm9rmax.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2d1_m9rmaxot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2d1_m9rmaxut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M9_Raussen1"))
                        {
                            tB2d1_sm9ra.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2d1_m9raot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2d1_m9raut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M9_Rmin2"))
                        {
                            tB2d2_sm9rmin.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2d2_m9rminot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2d2_m9rminut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M9_Rmax2"))
                        {
                            tB2d2_sm9rmax.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2d2_m9rmaxot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2d2_m9rmaxut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M9_Raussen2"))
                        {
                            tB2d2_sm9ra.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2d2_m9raot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2d2_m9raut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M12_21"))
                        {
                            tB2d2_sm9ra.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB2d2_m9raot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB2d2_m9raut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        
                        tB2_zn.Text = result.zeichnungsNummer;
                        tB2_wzg.Text = result.werkZeugNummer;
                        tB2_k.Text = result.kunde;
                        
                    }
                }
                else
                {
                    Action<Control.ControlCollection> func = null;

                    func = (controls) =>
                    {
                        foreach (Control control in controls)
                            if (control is TextBox)
                                (control as TextBox).Clear();
                            else if (control is ComboBox)
                                (control as ComboBox).SelectedIndex = -1;
                            else
                                func(control.Controls);
                    };

                    func(Controls);
                    string errorMessage = "Keine Treffer gefunden. Bitte tragen Sie eine andere Rückmeldenummer ein";
                    MessageBox.Show(errorMessage);
                }
                FolgeNummerList = QmsDb.GetFolgeNummersResultsSet(tB2_rmn.Text, tB2_zn.Text);
                if (SerienDataList.Any())
                {
                    foreach (SerienNrAbfrageDto result in SerienDataList)
                    {
                        
                        tB2_atn.Text = result.auftragsNummer;
                                                                      
                    }
                }

                if (FolgeNummerList.Any())
                {
                    foreach (FolgeNummerDto result in FolgeNummerList)
                    {
                        if (result.materialArt == "MMPDT")
                        {
                            if (!cB2_dfn.Items.Contains(result.folgeNummer))
                            {
                                cB2_dfn.Items.Add(result.folgeNummer);
                            }
                        }
                        else if (result.materialArt == "MMPPL")
                        {
                            if (!cB2_pfn.Items.Contains(result.folgeNummer))
                            {
                                cB2_pfn.Items.Add(result.folgeNummer);
                            }
                        }                                       
                    }
                }
            }
        }

        private void tB2_rmn_KeyPress(object sender, KeyPressEventArgs e)
        {
           // var regex = new Regex(@"[^a-zA-Z0-9\b\s]");
           // if (regex.IsMatch(e.KeyChar.ToString()))
           // {
           //     e.Handled = true;
           // }
        }

        private void tB2_rmnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //button1.PerformClick();
                button_Lupe_2_Click(this, new EventArgs());
                e.Handled = true;
            }
        }

        private void Button_new_rmn_2_Click(object sender, EventArgs e)
        {
            if (save == false)
            {
                DialogResult dialogResult = MessageBox.Show("Möchten Sie alle Felder ausleeren?", "Neue Datenblatt", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Action<Control.ControlCollection> func = null;

                    func = (controls) =>
                    {
                        foreach (Control control in controls)
                            if (control is TextBox)
                                (control as TextBox).Clear();
                            else if (control is ComboBox)
                                (control as ComboBox).SelectedIndex = -1;
                            else
                                func(control.Controls);
                    };

                    func(Controls);

                    cB2_dfn.SelectedIndex = -1;

                    cB2_pfn.SelectedIndex = -1;

                    cB2_dfn.Items.Clear();

                    cB2_pfn.Items.Clear();

                    LineList.Clear();

                    HistorieLineList.Clear();
                    createClicks_2db = 0;
                    save = false;

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            else
            {
                Action<Control.ControlCollection> func = null;

                func = (controls) =>
                {
                    foreach (Control control in controls)
                        if (control is TextBox)
                            (control as TextBox).Clear();
                        else if (control is ComboBox)
                            (control as ComboBox).SelectedIndex = -1;
                        else
                            func(control.Controls);
                };

                func(Controls);

                cB2_dfn.SelectedIndex = -1;

                cB2_pfn.SelectedIndex = -1;

                cB2_dfn.Items.Clear();

                cB2_pfn.Items.Clear();

                LineList.Clear();

                HistorieLineList.Clear();
                createClicks_2db = 0;
                save = false;
            }
            // TODO: This line of code loads data into the 'wefaDataSet2._MAiT_MM_2DB_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_2DB_MITTELSTANDTableAdapter.Fill(this.wefaDataSet2._MAiT_MM_2DB_MITTELSTAND);
            // TODO: This line of code loads data into the 'wefaDataSet1._MAiT_MM_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_MITTELSTANDTableAdapter2.Fill(this.wefaDataSet1._MAiT_MM_MITTELSTAND);
            this.dataGridView2.Refresh();
            this.dataGridView2.Parent.Refresh();
        }
        private int get_mandant_nummer(string input)
        {
            string singen = "Singen";
            string inotec=  "Inotec";
            string swiss = "Swiss";
            string bohemia = "Bohemia";
            Boolean caught = false;

            if (String.Equals(input, singen, StringComparison.OrdinalIgnoreCase)) 
            { 
                caught = true;
                return 111;
            }
            if (String.Equals(input, inotec, StringComparison.OrdinalIgnoreCase)) 
            { 
                caught = true;
                return 112;
            }
            if (String.Equals(input, swiss, StringComparison.OrdinalIgnoreCase)) 
            { 
                caught = true;
                return 123;
            }
            if (String.Equals(input, bohemia, StringComparison.OrdinalIgnoreCase)) 
            { 
                caught = true;
                return 134;
            }
            if (caught == false)
            {
                return 1;
            }
            return 1;
        }
        private void button_new_entry_2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tB2_rmn.Text))
            {
                string errorMessage = "Bitte tragen eine Rückmeldenummer ein";
                MessageBox.Show(errorMessage);
            }
            else
            {
                DataWriterHeader headerobj = new DataWriterHeader();

                DataWriterLine lineobj = new DataWriterLine();

                //Form Feld Daten erfassen
                (headerobj, lineobj) = Form12_daten_erfassen();
                if (!string.IsNullOrWhiteSpace((headerobj.k0008_pruefer)))
                {
                    if (headerobj.k0100_anzahlmm == 19)
                    {
                        createClicks_2db++;

                        mainheader = headerobj;
                        LineList.Clear();
                        LineList.Add(lineobj);


                        button_create_dfq_2_Click(this, new EventArgs());
                        Button_save_2_Click(this, new EventArgs());

                        tB2_m101.Clear();
                        tB2_m102.Clear();
                        tB2_m103.Clear();
                        tB2_m104.Clear();
                        tB2_m105.Clear();
                        tB2_m106.Clear();
                        tB2_m1011.Clear();
                        tB2_m1012.Clear();
                        tB2_m1013.Clear();
                        tB2_m1014.Clear();
                        tB2_m1015.Clear();
                        tB2_m1016.Clear();
                        tB2_m1221.Clear();
                        tB2d1_m9rmin.Clear();
                        tB2d1_m9rmax.Clear();
                        tB2d1_m9ra.Clear();
                        tB2d2_m9rmin.Clear();
                        tB2d2_m9rmax.Clear();
                        tB2d2_m9ra.Clear();

                      
                    }
                    else
                    {
                        string errorMessage = "Datensatz nicht vollständig.Eintrag nicht angelegt.";
                        MessageBox.Show(errorMessage);
                    }                   
                }
                else
                {
                    string errorMessage = "Bitte Prüfer Name mit eingeben.";
                    MessageBox.Show(errorMessage);
                }
                    

            }
            // TODO: This line of code loads data into the 'wefaDataSet2._MAiT_MM_2DB_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_2DB_MITTELSTANDTableAdapter.Fill(this.wefaDataSet2._MAiT_MM_2DB_MITTELSTAND);
            // TODO: This line of code loads data into the 'wefaDataSet1._MAiT_MM_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_MITTELSTANDTableAdapter2.Fill(this.wefaDataSet1._MAiT_MM_MITTELSTAND);
            this.dataGridView2.Refresh();
            this.dataGridView2.Parent.Refresh();

        }                        


        private void Button_save_2_Click(object sender, EventArgs e)
        {
            save = true;

            if (string.IsNullOrWhiteSpace(tB2_rmn.Text))
            {
                string errorMessage = "Bitte tragen eine Rückmeldenummer ein";
                MessageBox.Show(errorMessage);
            }
            else
            {
                int dataLines = LineList.Count();

                //Are there any existing Data Sätze?
                if (dataLines == 0)
                {

                    DataWriterLine lineobj = new DataWriterLine();

                    DataWriterHeader headerobj = new DataWriterHeader();

                    (headerobj, lineobj) = Form12_daten_erfassen();

                    // button_new_entry_2_Click(this, new EventArgs());
                    LineList.Add(lineobj);

                    if (headerobj.k0100_anzahlmm == 0)
                    {
                        //
                    }
                    //At least 1 measurement entered
                    else
                    {
                        foreach (DataWriterLine line in LineList)
                        {
                            QmsDFQWriter.SaveCachetoDB2DB(headerobj, line);
                        }                       


                        string message = "Auftrag wurde gespeichert";
                        MessageBox.Show(message);
                    }
                }
                else
                {
                    DataWriterLine lineobj = new DataWriterLine();

                    DataWriterHeader headerobj = new DataWriterHeader();

                    (headerobj, lineobj) = Form12_daten_erfassen();
                    
                    foreach (DataWriterLine line in LineList)
                    {
                        QmsDFQWriter.SaveCachetoDB2DB(headerobj, line);
                    }
                    string errorMessage = "Auftrag wurde gespeichert";
                    MessageBox.Show(errorMessage);                    
                    
                    
                }
            }
            // TODO: This line of code loads data into the 'wefaDataSet2._MAiT_MM_2DB_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_2DB_MITTELSTANDTableAdapter.Fill(this.wefaDataSet2._MAiT_MM_2DB_MITTELSTAND);
            // TODO: This line of code loads data into the 'wefaDataSet1._MAiT_MM_MITTELSTAND' table. You can move, or remove it, as needed.
            this._MAiT_MM_MITTELSTANDTableAdapter2.Fill(this.wefaDataSet1._MAiT_MM_MITTELSTAND);
            this.mAiTMM2DBMITTELSTANDBindingSource.ResetBindings(false);
            this.dataGridView2.Refresh();
            this.dataGridView2.Parent.Refresh();
        }
        //Suchfunktion
        private void Button_Lupe_Click(object sender, EventArgs e)
        {
            DataWriterHeader headerObj = new DataWriterHeader();

            List<KopfDatenAbfrageDto> KopfDataList = new List<KopfDatenAbfrageDto>();

            List<SerienNrAbfrageDto> SerienDataList = new List<SerienNrAbfrageDto>();

            List<FolgeNummerDto> FolgeNummerList = new List<FolgeNummerDto>();

            

          

            if (string.IsNullOrWhiteSpace(tB_rmn.Text))
            {
                string errorMessage = "Bitte tragen eine Rückmeldenummer ein";
                MessageBox.Show(errorMessage);
            }
            else
            {
                //read Kopfdata
                KopfDataList = QmsDb.GetKopfResultsSet(tB_rmn.Text);

                //read serial data
                SerienDataList = QmsDb.GetSerienResultsSet(tB_rmn.Text);

                if (KopfDataList.Any())
                {
                    foreach (KopfDatenAbfrageDto result in KopfDataList)
                    {
                        tB_m.Text = (get_mandant_nummer(result.mandant).ToString());

                        if (result.mmBez.Equals("M10_1"))
                        {
                            //Search for Mandant
                            mandant = result.mandant.ToString();
                            tB_sm101.Text = result.sollMass.ToString("F4").Replace(".", ",");                            
                            tB_m101ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB_m101ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }

                        else if (result.mmBez.Equals("M10_2"))
                        {
                            tB_m102ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB_m102ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                            tB_sm102.Text = result.sollMass.ToString("F4").Replace(".", ",");
                        }

                        else if (result.mmBez.Equals("M10_3"))
                        {
                            tB_sm103.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB_sm103.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB_m103ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB_m103ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M10_4"))
                        {
                            tB_sm104.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB_m104ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB_m104ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M10_5"))
                        {
                            tB_sm105.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB_m105ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB_m105ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M10_6"))
                        {
                            tB_sm106.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB_m106ot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB_m106ut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M9_Rmin"))
                        {
                            tB_sm9rmin.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB_m9rminot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB_m9rminut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M9_Rmax"))
                        {
                            tB_sm9rmax.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB_m9rmaxot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB_m9rmaxut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        else if (result.mmBez.Equals("M9_Raussen"))
                        {
                            tB_sm9ra.Text = result.sollMass.ToString("F4").Replace(".", ",");
                            tB_m9raot.Text = result.obertol.ToString("F4").Replace(".", ",");
                            tB_m9raut.Text = result.untertol.ToString("F4").Replace(".", ",");
                        }
                        
                        tB_zn.Text = result.zeichnungsNummer;
                        tB_k.Text = result.kunde;
                        tB_wzg.Text = result.werkZeugNummer;
                        
                    }
                }
                else
                {
                    Action<Control.ControlCollection> func = null;

                    func = (controls) =>
                    {
                        foreach (Control control in controls)
                            if (control is TextBox)
                                (control as TextBox).Clear();
                            else if (control is ComboBox)
                                (control as ComboBox).SelectedIndex = -1;
                            else
                                func(control.Controls);
                    };

                    func(Controls);
                    string errorMessage = "Keine Treffer gefunden. Bitte tragen Sie eine andere Rückmeldenummer ein";
                    MessageBox.Show(errorMessage);
                }
              

                FolgeNummerList = QmsDb.GetFolgeNummersResultsSet(tB_rmn.Text, tB_zn.Text);

                if (FolgeNummerList.Any())
                {
                    foreach (FolgeNummerDto result in FolgeNummerList)
                    {
                        //
                        
                        if (result.materialArt == "MMPDT") 
                        {
                            if (!cB_dfn.Items.Contains(result.folgeNummer))
                            {
                                cB_dfn.Items.Add(result.folgeNummer);
                            }
                        }
                        else if (result.materialArt == "MMPPL") 
                        {
                            if (!cB_pfn.Items.Contains(result.folgeNummer))
                            {
                                cB_pfn.Items.Add(result.folgeNummer);
                            }
                        }                        
                    }
                }
                if (SerienDataList.Any())
                {
                    foreach (SerienNrAbfrageDto result in SerienDataList)
                    {
                        //

                        tB_atn.Text = result.auftragsNummer;

                    }
                }

            }
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                tB2_rmn.Text = row.Cells[0].Value.ToString();
                if (tB2_rmn.Text == "")
                {
                    MessageBox.Show("Ungültige Rückmeldenummer in Historie");
                }
                else
                {
                    this.tabControl1.SelectedTab = this.tabPage3;
                    button_Lupe_2_Click(this, new EventArgs());

                  decimal m101  = decimal.Parse(row.Cells[10].Value.ToString());
                  decimal m102  = decimal.Parse(row.Cells[11].Value.ToString());
                  decimal m103  = decimal.Parse(row.Cells[12].Value.ToString());
                  decimal m104  = decimal.Parse(row.Cells[13].Value.ToString());
                  decimal m105  = decimal.Parse(row.Cells[14].Value.ToString());
                  decimal m106  = decimal.Parse(row.Cells[15].Value.ToString());
                  decimal m1011 = decimal.Parse(row.Cells[16].Value.ToString());
                  decimal m1012  = decimal.Parse(row.Cells[17].Value.ToString());
                  decimal m1013  = decimal.Parse(row.Cells[18].Value.ToString());
                  decimal m1014  = decimal.Parse(row.Cells[19].Value.ToString());
                  decimal m1015  = decimal.Parse(row.Cells[20].Value.ToString());
                  decimal m1016  = decimal.Parse(row.Cells[21].Value.ToString());
                  decimal m1221  = decimal.Parse(row.Cells[22].Value.ToString());
                  decimal d1m9rmin = decimal.Parse(row.Cells[23].Value.ToString());
                  decimal d1m9rmax = decimal.Parse(row.Cells[24].Value.ToString());
                  decimal d1m9ra = decimal.Parse(row.Cells[25].Value.ToString());
                  decimal d2m9rmin = decimal.Parse(row.Cells[26].Value.ToString());
                  decimal d2m9rmax = decimal.Parse(row.Cells[27].Value.ToString());
                  decimal d2m9ra = decimal.Parse(row.Cells[28].Value.ToString());
                    tB2_m101.Text        = m101.ToString("F4");
                    tB2_m102.Text        =  m102.ToString("F4");
                    tB2_m103.Text        =  m103.ToString("F4");
                    tB2_m104.Text        =  m104.ToString("F4");
                    tB2_m105.Text        =  m105.ToString("F4");
                    tB2_m106.Text        =  m106.ToString("F4");
                    tB2_m1011.Text       =  m1011.ToString("F4");
                    tB2_m1012.Text       =  m1012.ToString("F4");
                    tB2_m1013.Text       =  m1013.ToString("F4");
                    tB2_m1014.Text       =  m1014.ToString("F4");
                    tB2_m1015.Text       =  m1015.ToString("F4");
                    tB2_m1016.Text       =  m1016.ToString("F4");
                    tB2_m1221.Text       = m1221.ToString("F4");
                    tB2d1_m9rmin.Text    = d1m9rmin.ToString("F4");
                    tB2d1_m9rmax.Text    = d1m9rmax.ToString("F4");
                    tB2d1_m9ra.Text      = d1m9ra.ToString("F4");
                    tB2d2_m9rmin.Text    = d2m9rmin.ToString("F4");
                    tB2d2_m9rmax.Text    = d2m9rmax.ToString("F4");
                    tB2d2_m9ra.Text = d2m9ra.ToString("F4");


                    tB2_p.Text = row.Cells[6].Value.ToString();
                    if (row.Cells[3].Value.ToString() != "") { cB2_dfn.SelectedIndex = cB2_dfn.FindString(row.Cells[3].Value.ToString()); }
                    if (row.Cells[4].Value.ToString() != "") { cB2_pfn.SelectedIndex = cB2_dfn.FindString(row.Cells[4].Value.ToString()); }

                }
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                
                tB_rmn.Text = row.Cells[0].Value.ToString();
                if (tB_rmn.Text == "") 
                {
                    MessageBox.Show("Ungültige Rückmeldenummer in Historie");
                }
                else 
                {
                    this.tabControl1.SelectedTab = this.tabPage1;    
                    Button_Lupe_Click(this, new EventArgs());
                    decimal m101 = decimal.Parse(row.Cells[10].Value.ToString());
                    tB_m101.Text = m101.ToString("F4");   
                    decimal m102 = decimal.Parse(row.Cells[11].Value.ToString());
                    tB_m102.Text = m102.ToString("F4");      
                    decimal m103 = decimal.Parse(row.Cells[12].Value.ToString());
                    tB_m103.Text = m103.ToString("F4");    
                    decimal m104 = decimal.Parse(row.Cells[13].Value.ToString());
                    tB_m104.Text = m104.ToString("F4");   
                    decimal m105 = decimal.Parse(row.Cells[14].Value.ToString());
                    tB_m105.Text = m105.ToString("F4");   
                    decimal m106 = decimal.Parse(row.Cells[15].Value.ToString());
                    tB_m106.Text = m106.ToString("F4");   
                    decimal rmin = decimal.Parse(row.Cells[16].Value.ToString());
                    tB_m9rmin.Text = rmin.ToString("F4");   
                    decimal rmax = decimal.Parse(row.Cells[17].Value.ToString());
                    tB_m9rmax.Text = rmax.ToString("F4");   
                    decimal ra = decimal.Parse(row.Cells[18].Value.ToString());
                    tB_m9ra.Text = ra.ToString("F4");   


                    tB_p.Text= row.Cells[6].Value.ToString();       
                    if(row.Cells[3].Value.ToString() != "") { cB_dfn.SelectedIndex = cB_dfn.FindString(row.Cells[3].Value.ToString()); }
                    if (row.Cells[4].Value.ToString() != "") { cB_pfn.SelectedIndex = cB_dfn.FindString(row.Cells[4].Value.ToString()); }
                    
                }
                
            }
        }

        private void button_get_history2_Click(object sender, EventArgs e)
        {
            Dbr2HistorieDto history = new Dbr2HistorieDto();
            if ((string.IsNullOrWhiteSpace(tB2_rmn.Text)) || (cB2_pfn.SelectedIndex == -1) || (cB2_dfn.SelectedIndex == -1))
            {
                string errorMessage = "Daten ungültig. Historie kann nicht angezeigt werden";
                MessageBox.Show(errorMessage);
            }
            else 
            {
                history = QmsDb.GetCurrentRecord2Dbr(tB2_rmn.Text, cB2_dfn.Text, cB2_pfn.Text);
                if ((history.sdtserial != null) || (history.sptserial != null)) 
                {
                    tB2_m101.Text = history.dm101.ToString("F4");
                    tB2_m102.Text = history.dm102.ToString("F4");
                    tB2_m103.Text = history.dm103.ToString("F4");
                    tB2_m104.Text = history.dm104.ToString("F4");
                    tB2_m105.Text = history.dm105.ToString("F4");
                    tB2_m106.Text = history.dm106.ToString("F4");
                    tB2_m1011.Text = history.dm1011.ToString("F4");
                    tB2_m1012.Text = history.dm1012.ToString("F4");
                    tB2_m1013.Text = history.dm1013.ToString("F4");
                    tB2_m1014.Text = history.dm1014.ToString("F4");
                    tB2_m1015.Text = history.dm1015.ToString("F4");
                    tB2_m1016.Text = history.dm1016.ToString("F4");
                    tB2_m1221.Text = history.dm1221.ToString("F4");
                    tB2d1_m9rmin.Text = history.dm9rmin.ToString("F4");
                    tB2d1_m9rmax.Text = history.dm9rmax.ToString("F4");
                    tB2d1_m9ra.Text = history.dm9raussen.ToString("F4");
                    tB2d2_m9rmin.Text = history.dd2m9rmin.ToString("F4");
                    tB2d2_m9rmax.Text = history.dd2m9rmax.ToString("F4");
                    tB2d2_m9ra.Text = history.dd2m9raussen.ToString("F4");
                }
                else 
                {
                    MessageBox.Show("Keine Ergebnisse gefunden.");
                }
                   
            }
        }

        private void button_get_history_Click(object sender, EventArgs e)
        {
            Dbr1HistorieDto history = new Dbr1HistorieDto();
            if ((string.IsNullOrWhiteSpace(tB_rmn.Text)) || ((cB_pfn.SelectedIndex == -1) && (cB_dfn.SelectedIndex == -1)))
            {
                string errorMessage = "Daten ungültig. Historie kann nicht angezeigt werden";
                MessageBox.Show(errorMessage);
            }
            else 
            {
                history = QmsDb.GetCurrentRecord1Dbr(tB_rmn.Text, cB_dfn.Text, cB_pfn.Text);
                if((history.sdtserial != null) || (history.sptserial != null)) 
                {
                    tB_m101.Text = history.dm101.ToString("F4");
                    tB_m102.Text = history.dm102.ToString("F4");
                    tB_m103.Text = history.dm103.ToString("F4");
                    tB_m104.Text = history.dm104.ToString("F4");
                    tB_m105.Text = history.dm105.ToString("F4");
                    tB_m106.Text = history.dm106.ToString("F4");
                    tB_m9rmin.Text = history.dm9rmin.ToString("F4");
                    tB_m9rmax.Text = history.dm9rmax.ToString("F4");
                    tB_m9ra.Text = history.dm9raussen.ToString("F4");
                }
                else 
                {
                    MessageBox.Show("Keine Ergebnisse gefunden.");
                }
                

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.mAiTMMMITTELSTANDBindingSource.ResetBindings(false);
            this.dataGridView1.Refresh();
            this.dataGridView1.Parent.Refresh();
            this.dataGridView2.Refresh();
            this.dataGridView2.Parent.Refresh();
        }
    }
}
