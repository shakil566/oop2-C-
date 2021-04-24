using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace libmanagement
{
    public partial class view_books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-32IK7D6\SQLEXPRESS;Initial Catalog=library_managment;Integrated Security=True;Pooling=False");
        private object booksname;

        public view_books()
        {
            InitializeComponent();
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void view_books_Load(object sender, EventArgs e)
        {
            disp_books();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_name like('%" + textBox1.Text +"%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                i = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                con.Close();
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                    MessageBox.Show("No books found");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int i = 0;
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_name like('%" + textBox1.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt); 
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                con.Close();
               
                if (i == 0)
                    MessageBox.Show("No books found");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int i = 0;
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_author like('%" + textBox2.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                con.Close();

                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                    MessageBox.Show("No books found");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where books_author like('%" + textBox2.Text + "%')";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                con.Close();
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                if (i == 0)
                    MessageBox.Show("No books found");

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel3.Visible = true;
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            //MessageBox.Show(i.ToString());

            try
            {
                
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info where id=" +i+"";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach(DataRow dr in dt.Rows)
                {
                    books_name.Text = dr["books_name"].ToString();
                    booksauthor.Text = dr["books_author"].ToString();
                    bookspublication.Text = dr["books_publication"].ToString();
                    bookspurchasedate.Text = dr["books_purchase_date"].ToString();
                    bookprice.Text = dr["book_price"].ToString();
                    booksqty.Text = dr["books_quantity"].ToString();


                }
                con.Close();


                i = Convert.ToInt32(dt.Rows.Count.ToString());
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;



                cmd.CommandText = "update books_info set books_name= '" + books_name.Text + "',books_author='" + booksauthor.Text + "',books_publication='" + bookspublication.Text + "',books_purchase_date='" + bookspurchasedate.Text + "',book_price=" + bookprice.Text + ",books_quantity=" + booksqty.Text + " where id=" + i + "";
                cmd.ExecuteNonQuery();
                con.Close();
                disp_books();
                MessageBox.Show("Record Updated");
                panel3.Visible = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void disp_books()
        {
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from books_info";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
