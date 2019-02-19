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
    public partial class WebForm49 : System.Web.UI.Page
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
            qualification.Enabled = false;
            experience.Enabled = false;
            clinicname.Enabled = false;
            address.Enabled = false;
            fee.Enabled = false;
            timings.Enabled = false;
            docnum.Enabled = false;
            dept.Enabled = false;
            city.Enabled = false;
            location.Enabled = false;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (docid.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Doctor Id!!');", true);
            }
            else
            {
                cmd = new SqlCommand("Select * from dbo.createdoctor where docid='" + docid.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    docname.Text = dr.GetString(1);
                    qualification.Text = dr.GetString(2);
                    experience.Text = dr.GetString(3);
                    clinicname.Text = dr.GetString(4);
                    address.Text = dr.GetString(5);
                    fee.Text = dr.GetString(6);
                    timings.Text = dr.GetString(7);
                    docnum.Text = dr.GetString(8);
                    dept.Text = dr.GetString(9);
                    city.Text = dr.GetString(10);
                    location.Text = dr.GetString(11);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Records not found/Invalid Doctor ID');", true);
                }
            }
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("delete from dbo.createdoctor where docid='" + docid.Text + "'", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted!!');", true);
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
        protected void btnlogout_Click1(object sender, EventArgs e)
        {
            Session["username"] = "";
            Response.Redirect("logindetails.aspx");
        }
    }
}
