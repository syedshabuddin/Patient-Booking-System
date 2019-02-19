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
    public partial class WebForm6 : System.Web.UI.Page
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
            fcomments.Enabled = false;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from dbo.comments", con);
            ddname.Items.Clear();
            dr = cmd.ExecuteReader();
            ddname.Items.Add("---Name---");
            while (dr.Read())
            {
                ddname.Items.Add(dr.GetString(0));
            }
            dr.Close();
        }
        protected void ddname_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from dbo.comments where  fname = '" + ddname.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                fcomments.Text = dr.GetString(1);
            }
            dr.Close();
        }
        protected void btnok_Click(object sender, EventArgs e)
        {
            if (ddname.Text == "" || fcomments.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Search For Comments');", true);
            }
            else
            {
                cmd = new SqlCommand("insert into dbo.backupfeed values('" + ddname.Text + "','" + fcomments.Text + "')", con);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Saved! Thank You!');", true);
                cmd = new SqlCommand("delete from dbo.comments where fname='" + ddname.Text + "' and fcomments='" + fcomments.Text + "'", con);
                cmd.ExecuteNonQuery();
                fcomments.Text = "";
            }
        }
        protected void btnlogout_Click1(object sender, EventArgs e)
        {
            Session["username"] = "";
            Response.Redirect("logindetails.aspx");
        }
    }
}