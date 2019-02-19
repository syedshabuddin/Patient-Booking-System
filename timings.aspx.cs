using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ToString());
        //SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            //con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=mydb");
            con.Open();
            string str = (string)Session["username"];
            username.Text = str;
            if (str == "")
                Response.Redirect("adminlogin.aspx");
            docid.Focus();
            docname.Enabled = false;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (time.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Timings');", true);
            }
            else
            {
                int res;
                object n;
                cmd = new SqlCommand("Select count(*) from dbo.doctimings where docid='"+docid.Text+"' and time='" + time.Text + "'", con);
                n = cmd.ExecuteScalar();
                res = (int)n;
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('App Time exist');", true);
                    time.Text = "";
                }
                else
                {
                    cmd = new SqlCommand("insert into dbo.doctimings values('" + docid.Text + "','" + docname.Text + "','" + time.Text + "')", con);
                    cmd.ExecuteScalar();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Doctor AppTime Added');", true);          
                    docname.Text = "";
                    time.Text = "";
                }
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from dbo.createdoctor where docid='" + docid.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                docname.Text = dr.GetString(1);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Records not found/Invalid Doctor ID');", true);
            }
            dr.Close();
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            docid.Text = "";
            docname.Text = "";
            time.Text = "";
        }
        protected void btnsearch1_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select time from dbo.doctimings where docid='" + docid1.Text + "'", con);
            ddtime.Items.Clear();
            dr = cmd.ExecuteReader();
            ddtime.Items.Add("Time");
            while (dr.Read())
            {
                ddtime.Items.Add(dr.GetString(0));
            }
            dr.Close();
        }
        protected void ddtime_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void btnsave1_Click(object sender, EventArgs e)
        {
              
        }
        protected void btnlogout_Click1(object sender, EventArgs e)
        {
            Session["username"] = "";
            Response.Redirect("logindetails.aspx");
        }

        protected void docname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}