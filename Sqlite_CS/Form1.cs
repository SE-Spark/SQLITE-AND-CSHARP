using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Sqlite_CS
{
    public partial class Form1 : Form
    {
        private SQLiteConnection con;
        private SQLiteCommand cmd;
        private SQLiteDataAdapter adt;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                txtId.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
               txtName.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "insert into tvUsers values('" + txtId.Text+ "','"+txtName.Text+"')";
            execQuery(query);
            loadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string query = "update tvUsers set name='" + txtName.Text+ "' where id='" + txtId.Text + "'";
            execQuery(query);
            loadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "delete from tvUsers where id='" + txtId.Text + "'";
            execQuery(query);
            loadData();
        }
        //setting connection
        public void setConnection()
        {
            con = new SQLiteConnection("Data Source=Sqlite_CS.db;Version=3;New=False;Compress=True");            
        }
        //execute Query generic
        public void execQuery(String query)
        {
            setConnection();
            con.Open();
            cmd =con.CreateCommand();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //load data
        public void loadData()
        {
            setConnection();
            con.Open();
            cmd = con.CreateCommand();
            String commandText = "select * from tvUsers";
            adt = new SQLiteDataAdapter(commandText, con);
            dt = new DataTable();
            adt.Fill(dt);
            dataGridView1.DataSource =dt;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        ///install ==>  System.Data.SQLite (which supports X64 and X86 , LINQ and EF) from  nuget 
    }
}
