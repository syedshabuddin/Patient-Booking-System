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
    public partial class WebForm51 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ToString());
        //SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = (string)Session["docid"];
            docid.Text = str;
            if (str == "")
                Response.Redirect("doctorlogin.aspx");
            docid1.Focus();
            //con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=mydb");
            con.Open();
            purpose.Enabled = false;
            pnum.Enabled = false;
            pdate.Enabled = false;
            ptime.Enabled = false;
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["docid"] = "";
            Response.Redirect("doctorlogin.aspx");
        }
        protected void ddpatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from bookapp where  pname = '" + ddpatient.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                purpose.Text = dr.GetString(4);
                pnum.Text = dr.GetString(5);
                pdate.Text = dr.GetString(1);
                ptime.Text = dr.GetString(2);
            }
            dr.Close();
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from dbo.bookapp where docid='" + docid1.Text + "'", con);
            ddpatient.Items.Clear();
            dr = cmd.ExecuteReader();
            ddpatient.Items.Add("---Select Patient---");
            while (dr.Read())
            {
                ddpatient.Items.Add(dr.GetString(3));
            }
            dr.Close();
        }
        protected void btnok_Click(object sender, EventArgs e)
        {
            purpose.Text = "";
            pnum.Text = "";
            pdate.Text = "";
            ptime.Text = "";
        }
        protected void btnverify_Click(object sender, EventArgs e)
        {
            if (pres.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Fill Prescription');", true);
            }
            else
            {
                cmd = new SqlCommand("insert into dbo.verified values('" + docid.Text + "','"+ddpatient.Text+"','" + purpose.Text + "','" + pnum.Text + "','" + pdate.Text + "','" + ptime.Text + "','" + pres.Text + "')", con);
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Saved! Thank You!');", true);
                cmd = new SqlCommand("delete from bookapp where docid='" + docid.Text + "'and contactnum='" + pnum.Text + "' and date='"+pdate.Text+"' and time='"+ptime.Text+"'",con);
                cmd.ExecuteNonQuery();
                pres.Text = "";
                purpose.Text = "";
                pnum.Text = "";
                pdate.Text = "";
                ptime.Text = "";
                pres.Text = "";
            }
        }
        protected void ptime_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

