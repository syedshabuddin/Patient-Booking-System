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
    public partial class WebForm3 : System.Web.UI.Page
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
            demail.Enabled = false;
            dmob.Enabled = false;
            dclinic.Enabled = false;
            daddress.Enabled = false;
            dspeciality.Enabled = false;
            dcity.Enabled = false;
            dlocation.Enabled = false;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {   
            cmd = new SqlCommand("Select * from dbo.fordoctor", con);
            dddoctor.Items.Clear();
            dr = cmd.ExecuteReader();
            dddoctor.Items.Add("Select Doctor");
            while (dr.Read())
            {
                dddoctor.Items.Add(dr.GetString(0));
            }
            dr.Close();
        }
        protected void dddoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from dbo.fordoctor where  dname = '" + dddoctor.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                demail.Text = dr.GetString(1);
                dmob.Text = dr.GetString(2);
                dclinic.Text = dr.GetString(3);
                daddress.Text = dr.GetString(4);
                dspeciality.Text = dr.GetString(5);
                dcity.Text = dr.GetString(6);
                dlocation.Text = dr.GetString(7);
            }
            dr.Close();
        }
        protected void btnok_Click(object sender, EventArgs e)
        {
            demail.Text = "";
            dmob.Text = "";
            dclinic.Text = "";
            daddress.Text = "";
            dspeciality.Text = "";
            dcity.Text = "";
            dlocation.Text = "";
        }
        protected void btnverified_Click(object sender, EventArgs e)
        {   
            cmd = new SqlCommand("insert into dbo.docverified values('" + dddoctor.Text + "','" + demail.Text + "','" + dmob.Text + "','" + dclinic.Text + "','" + daddress.Text + "','" + dspeciality.Text + "','" + dcity.Text + "','" + dlocation.Text + "')", con);
            cmd.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Saved! Thank You!');", true);
            cmd = new SqlCommand("delete from dbo.fordoctor where dname='" + dddoctor.Text + "' and dmob='" + dmob.Text + "'", con);
            cmd.ExecuteNonQuery();
            demail.Text = "";
            dmob.Text = "";
            dclinic.Text = "";
            daddress.Text = "";
            dspeciality.Text = "";
            dcity.Text = "";
            dlocation.Text = "";
        }
        protected void btnlogout_Click1(object sender, EventArgs e)
        {
            Session["username"] = "";
            Response.Redirect("logindetails.aspx");
        }
    }
}