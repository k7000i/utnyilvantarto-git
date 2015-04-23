using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utnyilvantarto.Properties;

namespace Utnyilvantarto
{
    public partial class partnerForm : Form
    {

        //Global variables
        private bool isValid = true;
        private bool isValid_Jarmu = true;

        //global enums
        enum TestType : byte
        {
            IsEmpty,
            IsMatch
        };
        

        public partnerForm()
        {
            InitializeComponent();
            partnerekDataGridView.CellStateChanged += partnerekDataGridView_CellStateChanged;
            nevTextBox.Validating += TextBoxGlobalValidating;
            rendszamTextBox.Validating += jarmuGobalValidating;

            
        }

        //első lépés cellák ellenőrzése bool változók állítása
        void partnerekDataGridView_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            /* bool ervenyes;
             DataGridView dw = partnerekDataGridView;

             Regex iranyszamRegEx = new Regex("^[1-9][0-9]{3}$"); // iranyszam

             DataGridViewCell cegCell = dw.Rows[e.Cell.RowIndex].Cells[1]; //új sorban a cégnév
             DataGridViewCell iranyszamCell = dw.Rows[e.Cell.RowIndex].Cells[2]; //új sorban a cégnév

             string cegCellData = dw.Rows[e.Cell.RowIndex].Cells[1].Value as string; //cégnév



             if (string.IsNullOrEmpty(cegCellData)) //üres cégnév
             {
                 ervenyes = false;
                 cegCell.ErrorText = "Nem lehet üres a Cégnév";
             }
             else
             {
                 ervenyes = true;
                 cegCell.ErrorText = "";
             }

             if (!iranyszamRegEx.IsMatch(iranyszamCell.Value.ToString())) //helytelen irányítószám
             {
                 ervenyes = false;
                 iranyszamCell.ErrorText = "Helytelen irányítószám formátum";
             }
             else
             {
                 ervenyes = true;
                 iranyszamCell.ErrorText = "";
             }
             if (ervenyes)
             {
                 ervenyesCella = true;
             }
             else
             {
                 ervenyesCella = false;
             }*/
        }
        

        /// <summary>
        /// Regulax Expression check method. This will check the pattern and causes validation.
        /// </summary>
        /// <param name="testtype">A teszt típusa. TestType típusu változó</param>
        /// <param name="pattern">A regex kifejezés, ha testtype IsEmpty akkor pattern legyen ""</param>
        /// <param name="textCheck">Az ellenőrizendő szöveg</param>
        private bool RegExFunc(TestType testtype, string pattern, Control textCheck, string errorStr)
        {
            bool valid = true; 
            errorProvider.SetIconPadding(textCheck, 5);
            switch (testtype)
            {
                case TestType.IsEmpty:
                    if (string.IsNullOrEmpty(textCheck.Text) || string.IsNullOrWhiteSpace(textCheck.Text))
                        {
                            valid = false;
                            errorProvider.SetError(textCheck, errorStr);
                        }
                        else
                        {
                            errorProvider.SetError(textCheck, "");
                        }
                        return valid;
                    

                case TestType.IsMatch:
                        Regex RegexTest = new Regex(pattern);
                        if (!RegexTest.IsMatch(textCheck.Text))
                        {
                            valid = false;
                            errorProvider.SetError(textCheck, errorStr);
                        }
                        else
                        {
                            errorProvider.SetError(textCheck, "");
                        }
                        return valid;
            }
            return valid;
        }

        //Globális textbox validáció---------------------------------------------
        private void TextBoxGlobalValidating(object sender, CancelEventArgs e)
        {
            string iranyPattern = "^[1-9][0-9]{3}$"; //irányítószám regex


            isValid = true; //globális érvényesség

            if (!RegExFunc(TestType.IsEmpty, "", nevTextBox, Resources.str_E_uresCeg)) { isValid = false;}
            if (!RegExFunc(TestType.IsMatch, iranyPattern, iranyitoszamTextBox, Resources.str_E_iranyszam)) { isValid = false; }


        }

        void jarmuGobalValidating(object sender, CancelEventArgs e)
        {
            string fogyasztPattern = "^\\d{0,2},?\\d{1,2}$"; //fogyasztás regex
            string rendszamPattern = "^[A-Z]{3}-[0-9]{3}$"; //rendszam regex

            isValid_Jarmu = true; //globális jármű valid

            if (!RegExFunc(TestType.IsMatch, fogyasztPattern, fogyasztasTextBox, Resources.str_E_fogyasztas)) { isValid_Jarmu = false; }
            if (!RegExFunc(TestType.IsMatch, rendszamPattern, rendszamTextBox, Resources.str_E_rendszam)) { isValid_Jarmu = false; }
        }

        //Global textbox jarmuvek tab validáció----------------------------------

        private void partnerForm_Load(object sender, EventArgs e) //form load
        {

            // TODO: This line of code loads data into the 'utnyilvantartoDataSet1.UzemanyagTipusok' table. You can move, or remove it, as needed.
            this.uzemanyagTipusokTableAdapter.Fill(this.utnyilvantartoDataSet1.UzemanyagTipusok);
            // TODO: This line of code loads data into the 'utnyilvantartoDataSet1.UzemanyagTipusok' table. You can move, or remove it, as needed.
            this.uzemanyagTipusokTableAdapter.Fill(this.utnyilvantartoDataSet1.UzemanyagTipusok);
            // TODO: This line of code loads data into the 'utnyilvantartoDataSet1.Jarmuvek' table. You can move, or remove it, as needed.
            this.jarmuvekTableAdapter.Fill(this.utnyilvantartoDataSet1.Jarmuvek);
            // TODO: This line of code loads data into the 'utnyilvantartoDataSet1.Partnerek' table. You can move, or remove it, as needed.
            this.partnerekTableAdapter.Fill(this.utnyilvantartoDataSet1.Partnerek);
            newpBtn.Image = Image.FromFile("C:/Users/Public/Pictures/Ico/Add.ico");
        }


        //label számozás---------------------------------------------------------
        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedIndex == 0)
            {
                string current = (partnerekBindingSource.Position + 1).ToString();
                string count = partnerekBindingSource.Count.ToString();
                label1.Text = current + " a " + count + " ből";
            }
            
        }
        //új rekord hozzáadása gomb----------------------------------------------
        private void newpBtn_Click(object sender, EventArgs e)
        {
            try
            {
                partnerekBindingSource.AddNew();
            }
            catch (Exception)
            {
                Console.WriteLine("Csak egyszer nyomd a new gombot");
            }
                      
        }
        //mentés gomb------------------------------------------------------------
        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValid) //ha az összes textbox érvényes
                {
                    this.Validate();
                    this.partnerekBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.utnyilvantartoDataSet1);
                    partnerekTableAdapter.Update(utnyilvantartoDataSet1.Partnerek);
                    MessageBox.Show("A partner sikeresen el lett mentve", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    partnerekDataGridView.Refresh();
                }
                else
                {
                    MessageBox.Show("Hiba az adatbevitelben", "Hibás adatbevitel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Nem sikerült menteni");
            }
            
                         
        }
        //Törlés gomb------------------------------------------
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Biztos hogy törli a Partnert?", "Törlés", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Validate();
                    partnerekBindingSource.RemoveCurrent();
                    this.tableAdapterManager.UpdateAll(this.utnyilvantartoDataSet1);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Nincs mit törölni!", "Törlés", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        /*protected override void OnFormClosing(FormClosingEventArgs e)
        {
           
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            switch (MessageBox.Show(this, "Menti a változtatásokat?", "Mentés...", MessageBoxButtons.YesNo,MessageBoxIcon.Warning))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }  
        }*/

        //------------------------Járművek adatbázis metódusai

        //mentés gomb------------------------------------------------------------
        private void jarmuvekSaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValid_Jarmu)
                {
                    this.Validate();
                    this.jarmuvekBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.utnyilvantartoDataSet1);
                    MessageBox.Show("A jármű sikeresen el lett mentve", "Siker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    jarmuvekDataGridView.Refresh();
                }
                else
                {
                    MessageBox.Show("Hiba az adatbevitelben", "Hibás adatbevitel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                
            }
            
            
        }
        //új rekord hozzáadása gomb----------------------------------------------
        private void jarmuNewBtn_Click(object sender, EventArgs e)
        {
            jarmuvekBindingSource.AddNew();
        }

    }
}
