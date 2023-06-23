using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MachineTest
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source = DESKTOP-MV5LOPL\\MSSQLSERVER01;DataBase = MachineTest;Integrated Security=true");
            SqlCommand cmd = new SqlCommand("Usp_Insert_CategoryMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@CategoryId", txtCategoryId.Text);
            cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source = DESKTOP-MV5LOPL\\MSSQLSERVER01;DataBase = MachineTest;Integrated Security=true");
            SqlCommand cmd = new SqlCommand("Usp_Update_CategoryMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Id", Convert.ToInt64(GridView1.DataKeys[e.RowIndex].Values[0]));
            cmd.Parameters.AddWithValue("@CategoryId", txtCategoryId.Text);
            cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            GridView1.EditIndex = -1;
            this.BindData();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.BindData();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source = DESKTOP-MV5LOPL\\MSSQLSERVER01;DataBase = MachineTest;Integrated Security=true");
            SqlCommand cmd = new SqlCommand("Usp_deleteCategoryMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]));
            cmd.ExecuteNonQuery();
            con.Close();
            this.BindData();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataBind();
        }
        public void BindData()
        {
            SqlConnection con = new SqlConnection("Data Source = DESKTOP-MV5LOPL\\MSSQLSERVER01;DataBase = MachineTest;Integrated Security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from CategoryMaster", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}