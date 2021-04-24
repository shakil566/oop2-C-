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
using System.Data;
using System.Data.SqlClient;

namespace libmanagement
{
    public partial class add_std_info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-32IK7D6\SQLEXPRESS;Initial Catalog=library_managment;Integrated Security=True;Pooling=False");

        string pwd;
        string wanted_path;
        public add_std_info()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pwd = Class1.GetRandomPassword(20);
            wanted_path = Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory());
            DialogResult result = openFileDialog1.ShowDialog();
            openFileDialog1.Filter = "JPEG Files (*.jpeg)|* .jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*. jpg|GIF Files (*.gif)|*.gif";
            if((result == DialogResult.OK))
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string img_path;
                File.Copy(openFileDialog1.FileName, wanted_path + "\\student_image\\" + pwd + ".jpg");
                con.Open();
                mdi_user mu = new mdi_user();
                img_path = "student_image\\" + pwd + ".jpg";
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into student_info values ('" + textBox1.Text + "','" + img_path.ToString() + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record Inserted Successfully");
                //mu.Show();
                this.Close();
                //this.Show();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
