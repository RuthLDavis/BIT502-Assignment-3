using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace City_Gym                                              // Ruth Davis 4201345 //
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            new Membership_Form().Show();                                     // upon button click show the "membership form" form to the user 
            this.Hide();                                                               // the main screen form to be hidden
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            new Search_Members().Show();                                               // upon button click show the "search members" form to the user 
            this.Hide();                                                               // the main screen form to be hidden
        }

        private void btnBookClass_Click(object sender, EventArgs e)
        {
            new Book_Fitness_Class().Show();                                           // upon button click show the "fook fitness class" form to the user 
            this.Hide();                                                               // the main screen form to be hidden
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            new Help().Show();                                                         // upon button click show the "help" form to the user 
            this.Hide();                                                               // the main screen form to be hidden
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
                Application.Exit();                                                      // if the user selects the yes button the application will be closed
            }                                                                         // if the user selects the no button the messagebox will close and the user will be able to 
        }                                                                             // continue with the form and choose another option. 
    }
}
