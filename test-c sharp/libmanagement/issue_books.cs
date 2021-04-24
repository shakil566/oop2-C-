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

namespace libmanagement
{
    public partial class issue_books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-32IK7D6\SQLEXPRESS;Initial Catalog=library_managment;Integrated Security=True;Pooling=False");
 
        public issue_books()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void issue_books_Load(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from student_info where student_enrollment_no='" + txt_enrollment.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            int i = Convert.ToInt32(dt.Rows.Count.ToString());

            if(i==0)
            {
                MessageBox.Show("This enrollment not found");
            }
            else
            {
                foreach(DataRow dr in dt.Rows)
                {
                    studentname.Text = dr["student_name"].ToString();
                    studentemail.Text = dr["student_email"].ToString();
                    studentdepartment.Text = dr["student_department"].ToString();
                    studentsem.Text = dr["student_sem"].ToString();
                    studentdepartment.Text = dr["student_department"].ToString();
                    studentcontact.Text = dr["student_contact"].ToString();

                }
            }
        }

        private void booksissuedate_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void booksname_KeyUp(object sender, KeyEventArgs e)
        {
            int count = 0;

            if (e.KeyCode != Keys.Enter)
            {
                listBox1.Items.Clear();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_name like('%" + booksname.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());

                if (count > 0)
                {
                    listBox1.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr["books_name"].ToString());
                    }
                }
            }
        }

        private void booksname_KeyDown(object sender, KeyEventArgs e)
        {
            int count = 0;
            if (e.KeyCode != Keys.Down)
            {
                listBox1.Items.Clear();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_name like('%" + booksname.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());

                if (count > 0)
                {
                    listBox1.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr["books_name"].ToString());
                    }
                }
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                booksname.Text = listBox1.SelectedItem.ToString();
                listBox1.Visible = true;

            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            booksname.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int books_qty =0;
            SqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from books_info where books_name='"+booksname.Text+"'";
            cmd2.ExecuteNonQuery();

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);

            foreach(DataRow dr2 in dt2.Rows)
            {
                books_qty = Convert.ToInt32(dr2["available_qty"].ToString());
            }
            
            if(books_qty >0)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into issue_books values('" + txt_enrollment.Text + "','" + studentname.Text + "','" + studentsem.Text + "','" + studentcontact.Text + "','" + studentemail.Text + "','" + booksname.Text + "','" + booksissuedate.Text + "')";
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "update books_info set available_qty = available_qty-1 where books_name='" + booksname.Text + "'";
                cmd1.ExecuteNonQuery();


                MessageBox.Show("Book Issued Successfully");

            }
            else
            {
                MessageBox.Show("Book Not available");
            }


        }

        private void booksname_MouseClick(object sender, MouseEventArgs e)
        {
            int count = 0;
                listBox1.Items.Clear();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_name like('%" + booksname.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());

                if (count > 0)
                {
                    listBox1.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr["books_name"].ToString());
                    }
                }
            
        }
    }
}
