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
    public partial class WebForm7 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ToString());
        //SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            //con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=mydb");
            con.Open();
            string str = (string)Session["username"];
            username.Text = str;
            if (str == "")
                Response.Redirect("logindetails.aspx");
            fname.Focus();
        }
        protected void btnfsubmit_Click(object sender, EventArgs e)
        {
            if (fname.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Name');", true);
            }
            else if(fcomments.Text=="")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Type Comments');", true);
            }
            else
            {       
                cmd = new SqlCommand("insert into dbo.comments values('" + fname.Text + "','" + fcomments.Text + "')", con);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Thank You!!');", true);
                fname.Text = "";
                fcomments.Text = "";
            }
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Response.Redirect("logindetails.aspx");
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            fname.Text = "";
            fcomments.Text = "";
        }
    }
}
