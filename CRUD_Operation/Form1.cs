using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRUD_Operation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-FV075RRK\\SQLEXPRESS;Initial Catalog=MyDatabase;Integrated Security=True;Encrypt=False");
        public int StudentID;
        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }

        private void GetStudentRecord()
        {

            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            StudentRecordDataGridView.DataSource = dt;
        
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (IsVadid())
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES (@Id ,@Name, @Mobile, @Address )",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Student is successfully saved in the database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                GetStudentRecord();
                ResetForm();

            }
        }

        private bool IsVadid(){

            if(txtStudentName.Text == string.Empty)
            {
                MessageBox.Show("Student name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            StudentID = 0;
            txtStudentName.Clear();
            txtId.Clear();
            txtAddress.Clear();
            txtMobile.Clear();

            txtStudentName.Focus();
        }

        private void StudentRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e) // to select the whole information by selecting the ID(Sell Click) only
        {
            StudentID = Convert.ToInt32(StudentRecordDataGridView.SelectedRows[0].Cells[0].Value);  //This is the value of the ID will pass into the StudentID which is in the 1st row and 1st column in the table of the database
            
            txtId.Text = StudentRecordDataGridView.SelectedRows[0].Cells[0].Value.ToString(); // Row[0]Cell[1] is the value of Name 
            txtStudentName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString(); // Row[0]Cell[1] is the value of Name 
            txtAddress.Text = StudentRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString(); // Row[0]Cell[2] is the value of Address 
            txtMobile.Text = StudentRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString(); // Row[0]Cell[3] is the value of Mobile 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {

                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET StudentID=@Id ,Name=@Name, Mobile=@Mobile, Address=@Address WHERE StudentID=@Id ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information is successfully updated in the database", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);


                GetStudentRecord();
                ResetForm();

            }

            else
            {
                MessageBox.Show("Please select a student to update", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StudentID >= 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID=@Id ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student  is deleted from the system", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);


                GetStudentRecord();
                ResetForm();

            }
            else
            {
                MessageBox.Show("Please select a student to delete", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
