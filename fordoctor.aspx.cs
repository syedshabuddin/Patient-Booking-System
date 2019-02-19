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
    public partial class WebForm44 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ToString());
        //SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            //con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=mydb");
            con.Open();
            dname.Focus();
        }
        protected void btndoctorlogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("doctorlogin.aspx");
        }
        protected void btndsubmit_Click(object sender, EventArgs e)
        {
            if (dname.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Name');", true);
            }
            else if (demail.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Email Address');", true);
            }
            else if (dmob.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Mobile Number');", true);
            }
            else if (dclinic.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Clinic Name');", true);
            }
            else if (daddress.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Your Address');", true);
            }
            else if (dspeciality.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Your Speciality');", true);
            }
            else if (dcity.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Your City');", true);
            }
            else if (dlocation.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Your Location');", true);
            }
            else
            {
                cmd = new SqlCommand("insert into dbo.fordoctor values('" + dname.Text + "','" + demail.Text + "','" + dmob.Text + "','" + dclinic.Text + "','" + daddress.Text + "','" + dspeciality.Text + "','" + dcity.Text + "', '" + dlocation.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your Request Has Been Sent. Thank You');", true);
                dname.Text = "";
                demail.Text = "";
                dmob.Text = "";
                dclinic.Text = "";
                daddress.Text = "";
                dspeciality.Text = "";
                dcity.Text = "";
                dlocation.Text = "";
            }
            }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            dname.Text = "";
            demail.Text = "";
            dmob.Text = "";
            dclinic.Text = "";
            daddress.Text = "";
            dspeciality.Text = "";
            dcity.Text = "";
            dlocation.Text = "";
        }
        }
    }