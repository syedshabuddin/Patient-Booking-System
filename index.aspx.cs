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
    public partial class _Default : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ToString());
        //SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            //con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=mydb");
            fname.Focus();

            
        }
        protected void btndoctorsingup_Click(object sender, EventArgs e)
        {
            Response.Redirect("fordoctor.aspx");
        }
        protected void btndoctorlogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("doctorlogin.aspx");
        }
        protected void btnadmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminlogin.aspx");
        }
        protected void btnclickhere_Click(object sender, EventArgs e)
        {
            Response.Redirect("logindetails.aspx");
        }
        protected void btnloginclick_Click(object sender, EventArgs e)
        {
            Response.Redirect("logindetails.aspx");
        }
        protected void btncreateaccount_Click(object sender, EventArgs e)
        {
            if (fname.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Name');", true);
            }
            else if (eaddress.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Email Address');", true);
            }
            else if (mob.Text == "")
            {
                 ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Mobile Number');", true);
            }
            else if (cpass.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Password');", true);
            }
            else
            {
                int res;
                object n;
                cmd = new SqlCommand("Select count(*) from dbo.login where eaddress='" + eaddress.Text + "'", con);
                con.Open();
                n = cmd.ExecuteScalar();
                res = (int)n;
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Email Address Already Exist');", true);
                    con.Close();
                }
                else
                {

                }
                {
                    cmd = new SqlCommand("insert into dbo.login values('" + fname.Text + "','" + eaddress.Text + "','" + mob.Text + "','" + cpass.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Created!');", true);
                    con.Close();
                    Response.Redirect("logindetails.aspx");
                }
            }
        }
    }
}
