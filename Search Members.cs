using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace City_Gym                                      // Ruth Davis 4201345 //
{
    public partial class Search_Members : Form
    {
        public Search_Members()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            string message = "Are you sure you want to exit the programme";                      // message to appear in the message box for the user
            string title = "Exit";                                                  // title of the message box
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;                    // assign buttons to the message box  for the user to choose from
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);  // declare and initialise a DiaglogResult variable to show a message box
                                                                                                     // to the user with the message, messagebox title, buttons and an exclamation
            if (result == DialogResult.Yes)                                                             // point warning icon to the user. Variable capture user button choice
            {
                Application.Exit();                                                    // if the user selects the yes button the application will be closed
            }                                                                         // if the user selects the no button the messagebox will close and the user will be able to 
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            MainMenu m = new MainMenu();
            m.Show();                                                                       // return to the main menu form
            this.Close();                                                                   // close this form
        }

        private void membersBindingNavigatorSaveItem_Click(object sender, EventArgs e)                  // bindings for the database
        {
            this.Validate();
            this.membersBindingSource.EndEdit();                                            
            this.tableAdapterManager.UpdateAll(this.gymDataSet);

        }

        private void Search_Members_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'gymDataSet.Members' table. You can move, or remove it, as needed.
            this.membersTableAdapter.Fill(this.gymDataSet.Members);

        }

        private void btnClear_Click(object sender, EventArgs e)                                 
        {
            try
            {
                this.membersTableAdapter.ShowAll(this.gymDataSet.Members);                          // SQL query method created in the query buildier to show all the table data again
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }


             textName.Text = "";                                                                // clear all text from the text fields
             textType.Text = "";

        }


        private void btnSearchbyType_Click(object sender, EventArgs e)                          // SQL query methods created in the query builder to search the table 
        {                                                                                       // by Membership Type and Name if there is a number in the membership type ID text box
            if (textType.Text != "")                                                            // use of wildcard '%' in the query for the name field means it will run if this field is empty
            {
                try
                {
                    this.membersTableAdapter.nameAndIDType(this.gymDataSet.Members, ((int)(System.Convert.ChangeType(textType.Text, typeof(int)))), textName.Text);
                }
                catch 
                {
                    MessageBox.Show("Please enter a number.");
                }
            }
            else
            {
                this.membersTableAdapter.SearchLastName(this.gymDataSet.Members, textName.Text);        // if the membership ID text box is empty run the query to search by name only
            }
        }
    }
}
