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
    public partial class WebForm46 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ToString());
        //SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            //con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=mydb");
            con.Open();
            docid.Focus();
            string str = (string)Session["username"];
            username.Text = str;
            if (str == "")
                Response.Redirect("adminlogin.aspx");
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {

            if (docid.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter DOCID');", true);
            }
            else if (docname.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Doctor Name');", true);
            }
            else if (qualification.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Qualification');", true);
            }
            else if (experience.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Experience');", true);
            }
            else if (clinicname.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Clinic/Hosp Name');", true);
            }
            else if (address.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Doc Address');", true);
            }
            else if (fee.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Consulting Fee');", true);
            }
            else if (timings.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Doc Timings');", true);
            }
            else if (docnum.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Contact Number');", true);
            }
            else if (dept.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Department Of Doc');", true);
            }
            else if (city.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter City');", true);
            }
            else
            {
                int res;
                object n;
                cmd = new SqlCommand("Select count(*) from dbo.createdoctor where Docid='" + docid.Text + "'", con);
                n = cmd.ExecuteScalar();
                res = (int)n;
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Doctor ID Already Exists');", true);
                }
                else
                {
                    cmd = new SqlCommand("insert into dbo.createdoctor values('" + docid.Text + "','" + docname.Text + "','" + qualification.Text + "','" + experience.Text + "','" + clinicname.Text + "','" + address.Text + "','" + fee.Text + "','" + timings.Text + "','" + docnum.Text + "','" + dept.Text + "','" + city.Text + "','" + location.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Doctor Added!!');", true);
                    docid.Text = "";
                    docname.Text = "";
                    qualification.Text = "";
                    experience.Text = "";
                    clinicname.Text = "";
                    address.Text = "";
                    fee.Text = "";
                    timings.Text = "";
                    docnum.Text = "";
                    dept.Text = "";
                    city.Text = "";
                    location.Text = "";
                }
            }
        }
        

        protected void btnlogout_Click1(object sender, EventArgs e)
        {
            Session["username"] = "";
            Response.Redirect("logindetails.aspx");
        }



    }
}