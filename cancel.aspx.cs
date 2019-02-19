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
    public partial class WebForm10 : System.Web.UI.Page
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
            username.Text = str;
            if (str == "")
                Response.Redirect("logindetails.aspx");
            cmob.Focus();
            docid.Enabled = false;
            bname.Enabled = false;
            purpose.Enabled = false;
            date.Enabled = false;
            time.Enabled = false;
            date1.Enabled = false;
            time1.Enabled = false;
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Response.Redirect("logindetails.aspx");
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (cmob.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Mobile No');", true);
            }
            else
            {
                cmd = new SqlCommand("Select * from dbo.bookapp where contactnum='" + cmob.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    docid.Text = dr.GetString(0);
                    bname.Text = dr.GetString(3);
                    purpose.Text = dr.GetString(4);
                    date.Text=dr.GetString(1);
                    time.Text = dr.GetString(2);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Record Found');", true);
                    docid.Text = "";
                    bname.Text = "";
                    purpose.Text = "";
                    date.Text = "";
                    time.Text = "";
                }
            }
        }

        protected void btnconfirm_Click(object sender, EventArgs e)
        {
            if (cmob.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid!');", true);
            }
            else
            {
                cmd = new SqlCommand("delete from dbo.bookapp where contactnum='" + cmob.Text + "'  and docid='" + docid.Text + "' and date='" + date.Text + "' and time='" + time.Text + "'", con);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your Appointment Has Been Cancelled! Thank You!');", true);
                cmob.Text = "";
                docid.Text = "";
                bname.Text = "";
                purpose.Text = "";
                date.Text = "";
                time.Text = "";
            }
        }
        protected void btncheck_Click(object sender, EventArgs e)
        {
            if (cmob1.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Mobile No');", true);
            }
            else
            {
                cmd = new SqlCommand("Select * from dbo.bookapp where contactnum='" + cmob1.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    date1.Text = dr.GetString(1);
                    time1.Text = dr.GetString(2);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Appointment Booked!');", true);
                    cmob1.Text = "";
                }
            }

        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            cmob.Text = "";
            docid.Text = "";
            bname.Text = "";
            purpose.Text = "";
            date.Text = "";
            time.Text = "";
        }
        protected void btnok_Click(object sender, EventArgs e)
        {
            cmob1.Text = "";
            date1.Text = "";
            time1.Text = "";
        }
    }
}
