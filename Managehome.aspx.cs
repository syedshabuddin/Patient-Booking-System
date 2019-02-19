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
    public partial class WebForm50 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ToString());
        //SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            //con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=mydb");
            dept.Focus();
            con.Open();
            string str = (string)Session["username"];
            username.Text = str;
            if (str == "")
                Response.Redirect("adminlogin.aspx");
        }
         
         protected void btnlogout_Click1(object sender, EventArgs e)
         {
             Session["username"] = "";
             Response.Redirect("logindetails.aspx");
         }

         protected void btnsave_Click(object sender, EventArgs e)
         {
             if (dept.Text == "" || city.Text == "" || place.Text == "")
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter City & Location');", true);
             }
             else
             {
                 int res;
                 object n;
                 cmd = new SqlCommand("Select count(*) from dbo.location where dept='" + dept.Text + "' and city='" + city.Text + "' and place='" + place.Text + "'", con);
                 n = cmd.ExecuteScalar();
                 res = (int)n;
                 if (res > 0)
                 {
                     ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already Exists');", true);
                     dept.Text = "";
                     city.Text = "";
                     place.Text = "";
                     
                 }
                 else
                 {
                     cmd = new SqlCommand("insert into dbo.location values('" + dept.Text + "','" + city.Text + "','" + place.Text + "')", con);
                     cmd.ExecuteScalar();
                     ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Added!');", true);
                     dept.Text = "";
                     city.Text = "";
                     place.Text = "";
                     con.Close();
                 }
             }
         }
    }
}
