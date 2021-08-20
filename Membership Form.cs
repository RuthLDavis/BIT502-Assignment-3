using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace City_Gym                                      // Ruth Davis 4201345 // 
{
    public partial class Membership_Form : Form
    {
        public Membership_Form()
        {
            InitializeComponent();
        }



        // code for the Exit Button
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
        }                                                                             // continue with the form and choose another option. 





        // Code for the Main Menu Button

        private void btnMain_Click(object sender, EventArgs e)
        {
            MainMenu m = new MainMenu();                                                // return to the main menu and close this form
            m.Show();
            this.Close();
        }





        // code for Clear Form button
        private void btnClear_Click(object sender, EventArgs e)                             // provide an option to clear all the fields and start over
        {
            textFirstName.Text = "";                                            // clear all text from the customer details section text fields
            textLastName.Text = "";
            textMobile.Text = "";
            textAddress1.Text = "";
            textAddress2.Text = "";
            textCity.Text = "";
            textPostCode.Text = "";

            checkAccess.Checked = false;                                        // clear all the optional extras section check boxes. Set to unchecked
            checkPTrainer.Checked = false;
            checkDiet.Checked = false;
            checkVideo.Checked = false;

            radioBasic.Checked = false;                                         // clear all the membership details section radio buttons. Set to unchecked
            radioRegular.Checked = false;
            radioPremium.Checked = false;
            radio3Mths.Checked = false;
            radio12Mths.Checked = false;
            radio24Mths.Checked = false;

            radioWeekly.Checked = false;                                        // clear all the payment details section radio buttons. Set to unchecked
            radioMonthly.Checked = false;
            radioDDYes.Checked = false;
            radioDDNo.Checked = false;


            textBaseCost.Text = "";                                             // clear all text from the Membership Costs section text boxes. 
            textDiscountAmount.Text = "";
            textExtras.Text = "";
            textNetCost.Text = "";
            textRegPay.Text = "";

            errorProvider1.SetError(labelFirstName, "");                        // clear all error provider icons from the screen if there were any
            errorProvider2.SetError(labelLastName, "");
            errorProvider3.SetError(labelMobile, "");
            errorProvider4.SetError(labelAddress1, "");
            errorProvider5.SetError(labelCity, "");
            errorProvider6.SetError(labelType, "");
            errorProvider7.SetError(labelDuration, "");
            errorProvider8.SetError(labelFrequency, "");
            errorProvider9.SetError(labelDDebit, "");
            errorProvider10.SetError(btnCalculate, "");
        }




        // code for the calculate button

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //declare and initialise the local variables

            decimal baseAmount = 0;
            decimal duration = 0;
            decimal debitDiscount = 0;
            decimal totalDiscount;
            decimal sevenDays = 0;
            decimal pTrainer = 0;
            decimal dietCons = 0;
            decimal videos = 0;
            decimal totalExtras;
            decimal netMembership;
            decimal regPay = 0;

            // check membership type selected and apply the base cost

            if (radioBasic.Checked)                                                         // set first condition for membership type - radio button basic is selected
            {
                baseAmount = 10;                                                            // set base amount as $10.00 per week
            }
            else if (radioRegular.Checked)                                                  // set second condition for membeshipi type - radio button regular is selected
            {
                baseAmount = 15;                                                            // set the base amount as $15.00 per week
            }
            else if (radioPremium.Checked)                                                  // set the third condition for membership type - radio button premium is selected
            {
                baseAmount = 20;                                                            // set the base amount as $20.00 per week
            }
            else
            {
                MembershipType();                                                           // if no option selected call method MembershipType() to show the error message to the user
            }



            // check duration selected and assign the discount if applicable

            if (radio3Mths.Checked)                                                         // set first condition for duration - 3 months
            {
                duration = 0;                                                               // discount is 0
            }
            else if (radio12Mths.Checked)                                                   // set second condition for duration - 12 months
            {
                duration = 2;                                                               // $2.00 per week discount assigned to the variable
            }
            else if (radio24Mths.Checked)                                                   // set third condition for duration  - 24 months
            {
                duration = 5;                                                               // $5.00 per week discount assigned to the variable
            }
            else
            {
                Duration();                                                                 // if no option selected call method Duration() to show the error message to the user.
            }




            // check if user has opted in or out of payment by direct debit. Assign discount if applicable

            if (radioDDYes.Checked)                                                         // set first condition for direct debit - yes
            {
                debitDiscount = baseAmount * 1 / 100;                                       //  assign 1% discount on the base cost to the variable debitDiscount
            }
            else if (radioDDNo.Checked)                                                     // set second condition for direct debit - no
            {
                debitDiscount = 0;                                                          // discount is 0
            }
            else
            {
                DirectDebit();                                                              // if no option selected call method DirectDebit() to show the error message to the user.
            }



            // check for optional extras selected by user and assign costs if applicable. 

            if (checkAccess.Checked)                                                        // set condition for check box for 24hr/7day access selected
            {
                sevenDays = 1;                                                              // assign cost of $1.00 per week to the variable
            }
            else
            {
                // do nothing                                                               // if not selected do nothing and original assigned value of $0 is applied
            }


            if (checkPTrainer.Checked)                                                      // set condition for check box for personal trainer selected
            {
                pTrainer = 20;                                                              // assign cost of $20.00 per week to the variable
            }
            else
            {
                // do nothing                                                               // if not selected do nothing and original assigned value of $0 is applied
            }

            if (checkDiet.Checked)                                                          // set condition for check box for diet consultation selected
            {
                dietCons = 20;                                                              // assign cost of $20.00 per week to the variable
            }
            else
            {
                //do nothing                                                                // if not selected do nothing and original assigned value of $0 is applied
            }

            if (checkVideo.Checked)                                                         // set condition for check box for online video access selected
            {
                videos = 2;                                                                 // assign cost of $2.00 per week to the variable
            }
            else
            {
                // do nothing                                                               // if not selected do nothing and original assigned value of $0 is applied
            }

            // calculate the costs for extras, discounts and net cost

            totalExtras = sevenDays + pTrainer + dietCons + videos;                         // calculate total of all optional extras selected
            totalDiscount = duration + debitDiscount;                                       // calculate total of all discounts applied
            netMembership = (baseAmount - totalDiscount) + totalExtras;                     // deduct the value of the discounts from the base cost and then add the total of the 
                                                                                            // optional extras to get the net membership cost



            // check payment frequency selected and calculate the applicable weekly or monthly payment cost 

            if (radioWeekly.Checked)                                                        // set the first condition for frequency - weekly payment
            {
                regPay = netMembership;                                                     // cost is equal to net membership as this is calculated on a weekly basis
            }
            else if (radioMonthly.Checked)                                                  // set the second condition for frequency - monthly payment
            {
                regPay = (netMembership * 52) / 12;                                         // calculate as weekly (net membership) x 52 weeks in the year. Then divide into
            }                                                                               // 12 equal payments to be made monthly
            else
            {
                Frequency();                                                               // if no option selected from this group box call method Frequency() to show the 
            }                                                                              // error message to the user. 



            

                // display the results in the text boxes in the Membership Cost section

                textBaseCost.Text = baseAmount.ToString("C");                                  // use ("C") to show as currency with $ sign and decimal places
                textDiscountAmount.Text = totalDiscount.ToString("C");
                textExtras.Text = totalExtras.ToString("C");
                textNetCost.Text = netMembership.ToString("C");
                textRegPay.Text = regPay.ToString("C");
         
        }





        // methods for validation of fields to ensure users have entered text and made selections where mandatory on the form. Set up error messages for missing information
        private void textFirstName_Validating(object sender, CancelEventArgs e)
        {
            FirstName();                                                                // call method for FirstName
        }

        private bool FirstName()                                                        // method for FirstName
        {
            bool flag = true;                                                           //declare and initialise variable
            if (textFirstName.Text == "")
            {
                errorProvider1.SetError(labelFirstName, "Please enter your first name.");      // if the text box is empty use error provider 1 to mark the first name label and show 
                flag = false;
            }
            else
            {
                errorProvider1.SetError(labelFirstName, "");                                    // if first name text box contains text do not show an error
            }
            return flag;
        }

        private void textLastName_Validating(object sender, CancelEventArgs e)         // validating that user has entered text in the Last Name text box
        {
            Surname();                                                                 // call method for Surname
        }

        private bool Surname()                                                          // method for Surname
        {
            bool flag = true;                                                           //declare and initialise variable
            if (textLastName.Text == "")                                                // if the text box is empty use error provider 2 to mark the last name label and show
            {                                                                           // message to user to enter name
                errorProvider2.SetError(labelLastName, "Please enter your last name.");
                flag = false;
            }
            else
            {
                errorProvider2.SetError(labelLastName, "");                                    // if last name text box contains text do not show an error
            }
            return flag;
        }
        private void textMobile_Validating(object sender, CancelEventArgs e)            // validating that user has entered text in the Mobile text box
        {
            Mobile();                                                                   // call method for mobile
        }

        private bool Mobile()                                                           // method for Mobile
        {
            bool flag = true;                                                           // declare and initialise the variable
            if (textMobile.Text == "")
            {                                                                           // if the text box is empty use error provider 3 to mark the mobile label and show
                                                                                        // message to user to enter name
                errorProvider3.SetError(labelMobile, "Please enter your mobile number.");
                flag = false;
            }
            else
            {
                errorProvider3.SetError(labelMobile, "");                                    // if mobile text box contains text do not show an error
            }
            return flag;
        }
        private void textAddress1_Validating(object sender, CancelEventArgs e)            // validating that user has entered text in the Address 1 text box
        {
            Address1();                                                                   // call method for Address1
        }

        private bool Address1()                                                           // method for Address1
        {
            bool flag = true;                                                           // declare and initialise the variable
            if (textAddress1.Text == "")
            {                                                                           // if the text box is empty use error provider 4 to mark the address 1 label and show
                                                                                        // message to user to enter name
                errorProvider4.SetError(labelAddress1, "Please enter your address.");
                flag = false;
            }
            else
            {
                errorProvider4.SetError(labelAddress1, "");                                    // if address 1 text box contains text do not show an error
            }
            return flag;
        }
        private void textCity_Validating(object sender, CancelEventArgs e)            // validating that user has entered text in the City text box
        {
            City();                                                                   // call method for City
        }

        private bool City()                                                           // method for City
        {
            bool flag = true;                                                           // declare and initialise the variable
            if (textCity.Text == "")
            {                                                                           // if the text box is empty use error provider 5 to mark the City label and show
                                                                                        // message to user to enter name
                errorProvider5.SetError(labelCity, "Please enter your city.");
                flag = false;
            }
            else
            {
                errorProvider5.SetError(labelCity, "");                                    // if City text box contains text do not show an error
            }
            return flag;
        }


        private bool MembershipType()                                                      // method to show error message to user if no membership type has been selected
        {
            bool flag = true;
            if (!radioBasic.Checked || !radioRegular.Checked || !radioPremium.Checked)      // if no option selected from the group box set error provider 6 to mark the label
            {                                                                               // Type and show the message to the user
                errorProvider6.SetError(labelType, "Please select a membership type");
                flag = false;
            }
            else
            {
                errorProvider6.SetError(labelType, "");                                     // if a radio button in this group has been selected do not show an error
            }
            return flag;
        }


        private bool Duration()                                                             // method to show error message to user if no duration has been selected
        {
            bool flag = true;
            if (!radio3Mths.Checked || !radio12Mths.Checked || !radio24Mths.Checked)        // if no option selected from the group box set error provider 7 to mark the label
            {                                                                               // Duration and show the message to the user
                errorProvider7.SetError(labelDuration, "Please select a membership duration.");
                flag = false;
            }
            else
            {
                errorProvider7.SetError(labelDuration, "");                                        // if a radio button in this group has been selected do not show an error
            }
            return flag;
        }


        private bool Frequency()                                                            // method to show error message to user if no frequency has been selected
        {
            bool flag = true;
            if (!radioWeekly.Checked || !radioMonthly.Checked)                              // if no option selected from the group box set error provider 8 to mark the label
            {                                                                               // Frequency and show the message to the user
                errorProvider8.SetError(labelFrequency, "Please select a payment frequency.");
                flag = false;
            }
            else
            {
                errorProvider8.SetError(labelFrequency, "");                                // if a radio button in this group has been selected do not show an error
            }
            return flag;
        }



        private bool DirectDebit()                                                            // method to show error message to user if user has not opted in or out of direct debit
        {
            bool flag = true;
            if (!radioDDYes.Checked || !radioDDNo.Checked)                              // if no option selected from the group box set error provider 9 to mark the label
            {                                                                               // DDebit and show the message to the user
                errorProvider9.SetError(labelDDebit, "Please select Yes or No for payment by direct debit.");
                flag = false;
            }
            else
            {
                errorProvider9.SetError(labelDDebit, "");                                // if a radio button in this group has been selected do not show an error
            }
            return flag;
        }


        private void textBaseCost_Validating(object sender, CancelEventArgs e)             // validate if the text box for base cost has text in it using metohd BaseAmount(). Do not need to validate
        {                                                                                  // all the cost text boxes on submission because if the user has clicked on the calculate button, all costing
            BaseAmount();                                                                  // fields will have text. If the user has not clicked on calculate button, no costing fields will have text.
        }                                                                                  // So only need to validate 1 of the costing text boxes for form submission.

        private bool BaseAmount()                                                           // method to check for text in the base cost text box.
        {
            bool flag = true;
            if (textBaseCost.Text == "")                                                    // if the base cost text box is empty set error provider 10 to mark the calculate 
            {                                                                               // button and show the error message to the user. 
                errorProvider10.SetError(btnCalculate, "Please click the calculate button to calculate your membership cost.");
                flag = false;
            }
            else
            {
                errorProvider10.SetError(btnCalculate, "");                                   // if the text box contains text do not show an error
            }
            return flag;
        }

        private void membersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.membersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.gymDataSet);

        }

        private void Membership_Form_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'gymDataSet.Members' table. You can move, or remove it, as needed.
            this.membersTableAdapter.Fill(this.gymDataSet.Members);

            radioBasic.Checked = false;                                                             // clear all the membership details section radio buttons. Set to unchecked
            radioRegular.Checked = false;                                                              // so that nothing is selected by default
            radioPremium.Checked = false;
            radio3Mths.Checked = false;
            radio12Mths.Checked = false;
            radio24Mths.Checked = false;

            radioWeekly.Checked = false;                                                            // clear all the payment details section radio buttons. Set to unchecked
            radioMonthly.Checked = false;                                                           // so that nothing is selected by default
            radioDDYes.Checked = false;
            radioDDNo.Checked = false;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // assign boolean variables to call the methods and validate fields have been completed before allowing submission and writing to text file

            bool name = FirstName();
            bool surname = Surname();
            bool mobile = Mobile();
            bool address = Address1();
            bool city = City();
            bool calculate = BaseAmount();



            if (name && surname && mobile && address && city && calculate)                              // set condition if all boolean values true  - will write to database
            {
                //database variables
                string connectionString;
                SqlConnection con;
                SqlCommand insert;
                string sql;


                string dDebit = "";                                                                     // get values from the radio buttons for payment for direct debit
                if (radioDDYes.Checked)
                {
                    dDebit = "Yes";                                                                     // and create the string to write to the database 
                }
                if (radioDDNo.Checked)
                {
                    dDebit = "No";
                }


                string payFreq = "";                                                                    // get values from the radio buttons for payment frequency
                if (radioWeekly.Checked)
                {
                    payFreq = "Weekly";                                                                 // and create the string to write to the database
                }
                if (radioMonthly.Checked)
                {
                    payFreq = "Monthly";
                }


                string Access = "null";                                                                 // get values from the check boxes for optional extras and create the string to write to the database
                if (checkAccess.Checked)
                {
                    Access = "24/7 Access";                                                             // and create the string to write to the database
                }


                string trainer = "null";
                if (checkPTrainer.Checked)
                {
                    trainer = "Personal Trainer";
                }

                string diet = "null";
                if (checkDiet.Checked)
                {
                    diet = "Diet Consultation";
                }

                string video = "null";
                if (checkVideo.Checked)
                {
                    video = "Access Online Fitness Videos";
                }



                string date = "";                                                                           // get values from the radio buttons for duration  
                if (radio3Mths.Checked)
                {
                    DateTime currentDateTime = DateTime.Now;                            
                    DateTime expiryDate = currentDateTime.AddMonths(3);                                     // calculate the expiry date based on the duration selected 
                    date = expiryDate.ToString("dd MMM yyyy");                                              // convert the date to a string to write to the database
                }
                if (radio12Mths.Checked)
                {
                    DateTime currentDateTime = DateTime.Now;
                    DateTime expiryDate = currentDateTime.AddMonths(12);
                    date = expiryDate.ToString("dd MMM yyyy");
                }
                if (radio24Mths.Checked)
                {
                    DateTime currentDateTime = DateTime.Now;
                    DateTime expiryDate = currentDateTime.AddMonths(24);
                    date = expiryDate.ToString("dd MMM yyyy");
                }


                int type = 0;                                                                                  // get values from the radio buttons  for membership type
                if (radioBasic.Checked)
                {
                    type = 1;                                                                                   // // and create the integer to write to the database
                }
                if (radioRegular.Checked)
                {
                    type = 2;
                }
                if (radioPremium.Checked)
                {
                    type = 3;
                }


                // create the connection details and query to insert the new member data into the database

                connectionString = ConfigurationManager.ConnectionStrings["City_Gym.Properties.Settings.GymConnectionString"].ConnectionString;
                con = new SqlConnection(connectionString);
                sql = "INSERT INTO Members (FirstName, LastName, AddressOne, AddressTwo, City, PostCode, Mobile, DirectDebit, PaymentFrequency, AllDayAccess, PersonalTrainer, DietConsultation, AccessOnlineFitnessVideos, MembershipExpiry, MembershipID) " +
                    "VALUES (@FirstName, @LastName, @AddressOne, @AddressTwo, @City, @PostCode, @Mobile, @DirectDebit, @PaymentFrequency, @AllDayAccess, @PersonalTrainer, @DietConsultation, @AccessOnlineFitnessVideos, @MembershipExpiry, @MembershipID)";
                insert = new SqlCommand(sql, con);
                insert.Parameters.AddWithValue("@FirstName", textFirstName.Text);                           // get the values to insert from the text boxes
                insert.Parameters.AddWithValue("@LastName", textLastName.Text);
                insert.Parameters.AddWithValue("@AddressOne", textAddress1.Text);
                insert.Parameters.AddWithValue("@AddressTwo", textAddress2.Text);
                insert.Parameters.AddWithValue("@City", textCity.Text);
                insert.Parameters.AddWithValue("@PostCode", textPostCode.Text);
                insert.Parameters.AddWithValue("@Mobile", textMobile.Text);
                insert.Parameters.AddWithValue("@DirectDebit", dDebit);                                     // get the values to insert from the variables created above
                insert.Parameters.AddWithValue("@PaymentFrequency", payFreq);
                insert.Parameters.AddWithValue("@AllDayAccess", Access);
                insert.Parameters.AddWithValue("@PersonalTrainer", trainer);
                insert.Parameters.AddWithValue("@DietConsultation", diet);
                insert.Parameters.AddWithValue("@AccessOnlineFitnessVideos", video);
                insert.Parameters.AddWithValue("@MembershipExpiry", date);
                insert.Parameters.AddWithValue("@MembershipID", type);




                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = insert;
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Thank you for registering.", "Registration Successful");              // show message box to confirm sucessful registration
                insert.Dispose();
                con.Close();


                // clear the form so user can enter details of the next new user without closing the application. 

                textFirstName.Text = "";                                                                // clear all text from the customer details section text fields
                textLastName.Text = "";
                textMobile.Text = "";
                textAddress1.Text = "";
                textAddress2.Text = "";
                textCity.Text = "";
                textPostCode.Text = "";

                checkAccess.Checked = false;                                                            // clear all the optional extras section check boxes. Set to unchecked
                checkPTrainer.Checked = false;
                checkDiet.Checked = false;
                checkVideo.Checked = false;

                radioBasic.Checked = false;                                                             // clear all the membership details section radio buttons. Set to unchecked
                radioRegular.Checked = false;
                radioPremium.Checked = false;
                radio3Mths.Checked = false;
                radio12Mths.Checked = false;
                radio24Mths.Checked = false;

                radioWeekly.Checked = false;                                                            // clear all the payment details section radio buttons. Set to unchecked
                radioMonthly.Checked = false;
                radioDDYes.Checked = false;
                radioDDNo.Checked = false;


                textBaseCost.Text = "";                                                                 // clear all text from the Membership Costs section text boxes.
                textDiscountAmount.Text = "";
                textExtras.Text = "";
                textNetCost.Text = "";
                textRegPay.Text = "";

                errorProvider1.SetError(labelFirstName, "");                                            // clear all error provider icons from the screen if there were any
                errorProvider2.SetError(labelLastName, "");
                errorProvider3.SetError(labelMobile, "");
                errorProvider4.SetError(labelAddress1, "");
                errorProvider5.SetError(labelCity, "");
                errorProvider6.SetError(labelType, "");
                errorProvider7.SetError(labelDuration, "");
                errorProvider8.SetError(labelFrequency, "");
                errorProvider9.SetError(labelDDebit, "");
                errorProvider10.SetError(btnCalculate, "");

            }
            else
            {
                // do nothing
            }

        }

    }


}



