using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace libmanagement
{
    public partial class view_std_info : Form
    {
        
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-32IK7D6\SQLEXPRESS;Initial Catalog=library_managment;Integrated Security=True;Pooling=False");
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        string wanted_path;
        string img_path;
        string pwd = Class1.GetRandomPassword(20);
        DialogResult result;

        public view_std_info()
        {
            InitializeComponent();
        }

        private void view_std_info_Load(object sender, EventArgs e)
        {
            

            if (con.State == ConnectionState.Open) ;
            {
                con.Close();
            }
            con.Open();
            fill_grid();

        }

        public void fill_grid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from student_info";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt); ;
            dataGridView1.DataSource = dt;

            Bitmap img;
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            imageCol.HeaderText = "student image";
            imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            imageCol.Width = 100;
            dataGridView1.Columns.Add(imageCol);

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                /* string wanted_path = Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory());
                 MessageBox.Show(wanted_path);*/
                img = new Bitmap(@"D:\test-c sharp\libmanagement\bin\" + dr["student_image"].ToString());
                dataGridView1.Rows[i].Cells[8].Value = img;
                dataGridView1.Rows[i].Height = 100;
                i = i + 1;
            }


            dataGridView1.DataSource = dt;

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Refresh();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from student_info  where student_name LIKE ('%" + textBox1.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt); ;
                dataGridView1.DataSource = dt;

                Bitmap img;
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                imageCol.HeaderText = "student image";
                imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imageCol.Width = 100;
                dataGridView1.Columns.Add(imageCol);

                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    /* string wanted_path = Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory());
                     MessageBox.Show(wanted_path);*/
                    img = new Bitmap(@"D:\test-c sharp\libmanagement\bin\" + dr["student_image"].ToString());
                    dataGridView1.Rows[i].Cells[8].Value = img;
                    dataGridView1.Rows[i].Height = 100;
                    i = i + 1;
                }


                dataGridView1.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from student_info where id=" +i+ "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                studentname.Text = dr["student_name"].ToString();
                studentemail.Text = dr["student_email"].ToString();
                studentsem.Text = dr["student_sem"].ToString();
                studentcontact.Text = dr["student_contact"].ToString();
                studentdepartment.Text = dr["student_department"].ToString();
                enrollmentno.Text = dr["student_enrollment_no"].ToString();

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            wanted_path = Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory());
            result = openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "JPEG Files (*.jpeg)|* .jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*. jpg|GIF Files (*.gif)|*.gif";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(studentname.Text);


            if ((result == DialogResult.OK))
            {
                int i;
                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

                
                File.Copy(openFileDialog1.FileName, wanted_path + "\\student_image\\" + pwd + ".jpg");
                img_path = "student_image\\" + pwd + ".jpg";

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update student_info  set student_name='" + studentname.Text +"',student_image='" + img_path.ToString() +"',student_department='" + studentdepartment.Text +"',student_sem='" + studentsem.Text +"',student_enrollment_no='" + enrollmentno.Text +"',student_contact='" + studentcontact.Text + "',student_email='" + studentemail.Text +"' where id=" + i+ "";
                cmd.ExecuteNonQuery();
                fill_grid();
                MessageBox.Show("Data Updated Successfully");
            }
            else if(result == DialogResult.Cancel){
                int i;
                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update student_info  set student_name='" + studentname.Text +  "',student_department='" + studentdepartment.Text + "',student_sem='" + studentsem.Text + "',student_enrollment_no='" + enrollmentno.Text + "',student_contact='" + studentcontact.Text + "',student_email='" + studentemail.Text + "' where id=" + i + "";
                cmd.ExecuteNonQuery();
                fill_grid();
                MessageBox.Show("Data Updated Successfully");
            }
            else
            {
                int i;
                i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update student_info  set student_name='" + studentname.Text +  "',student_department='" + studentdepartment.Text + "',student_sem='" + studentsem.Text + "',student_enrollment_no='" + enrollmentno.Text + "',student_contact='" + studentcontact.Text + "',student_email='" + studentemail.Text + "' where id=" + i + "";
                cmd.ExecuteNonQuery();
                fill_grid();
                MessageBox.Show("Data Updated Successfully");
            }
            
        }
    }
}
