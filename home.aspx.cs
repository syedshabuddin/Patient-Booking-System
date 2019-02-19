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
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ToString());
       // SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
           // con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=mydb");
            con.Open();
            string str = (string)Session["username"];
            username.Text = str;
           
            if (Page.IsPostBack == false)
            {
                cmd = new SqlCommand("Select distinct dept from dbo.location ", con);
                dr = cmd.ExecuteReader();
                dddept.Items.Clear();
                dddept.Items.Add("---Select Dept---");
                while (dr.Read())
                {
                    dddept.Items.Add(dr.GetString(0));
                }
                dr.Close();
                qualification.Enabled = false;
                experience.Enabled = false;
                clinicname.Enabled = false;
                address.Enabled = false;
                fee.Enabled = false;
                timings.Enabled = false;
                docnum.Enabled = false;
                docid.Enabled = false;
                date.Enabled = false;
                time.Enabled = false;
                prescription.Enabled = false;
            }
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {   
            Session["username"] = "";
            Response.Redirect("logindetails.aspx");
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (dddept.Text == "" || ddcity.Text == "" || ddlocation.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Details');", true);
            }
            else
            {
                cmd = new SqlCommand("Select * from dbo.createdoctor where dept='" + dddept.Text + "' and city='" + ddcity.Text + "' and location='" + ddlocation.Text + "'", con);
                dddoctor.Items.Clear();
                dr = cmd.ExecuteReader();
                dddoctor.Items.Add("---Select Doctor---");
                while (dr.Read())
                {
                    dddoctor.Items.Add(dr.GetString(1));
                }
                dr.Close();
            }
        }
        protected void dddoctor_SelectedIndexChanged(object sender, EventArgs e)
        { 
            cmd = new SqlCommand("Select * from createdoctor where  docname = '" +  dddoctor.Text  +"'", con);
            dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                qualification.Text = dr.GetString(2);
                experience.Text = dr.GetString(3);
                clinicname.Text = dr.GetString(4);
                address.Text = dr.GetString(5);
                fee.Text = dr.GetString(6);
                timings.Text = dr.GetString(7);
                docnum.Text = dr.GetString(8);
            }
            dr.Close();
        }
        protected void ddcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select place from dbo.location where dept='" + dddept.Text + "' and city='" + ddcity.Text + "'", con);
                dr = cmd.ExecuteReader();
                ddlocation.Items.Clear();
                ddlocation.Items.Add("--Select Your Location--");
                while (dr.Read())
                {
                    ddlocation.Items.Add(dr.GetString(0));
                }
                dr.Close();  
        }
        protected void dddept_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select distinct city from dbo.location where dept='" + dddept.Text + "'", con);
            dr = cmd.ExecuteReader();
            ddcity.Items.Clear();
            ddcity.Items.Add("--Select Your City--");
            while (dr.Read())
            {
                ddcity.Items.Add(dr.GetString(0));
            }
            dr.Close();
        }
        protected void ddlocation_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        public void ddtime_SelectedIndexChanged(object sender, EventArgs e)
        {
            object n;
            int res;
            cmd = new SqlCommand("Select count(*) from dbo.bookapp where time= '" + ddtime.SelectedItem.Value + "' and date='"+date.Text+"'", con);
            n = cmd.ExecuteScalar();
                res = (int)n;
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Appointment Time not avalilable');", true);
                    time.Text = "";
                }
                else
                {
                    time.Text = ddtime.SelectedItem.Value;
                }
        }
        protected void btnrequest_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from dbo.createdoctor where  docname = '" + dddoctor.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                docid.Text = dr.GetString(0);
                dr.Close();
            }
            cmd = new SqlCommand("Select time from dbo.doctimings where docid='" + docid.Text + "'", con);
            ddtime.Items.Clear();
            dr = cmd.ExecuteReader();
            ddtime.Items.Add("Time");
            while (dr.Read())
            {
                ddtime.Items.Add(dr.GetString(0));
            }

            dr.Close();
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
                if(date.Text=="")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Date from Calendar');", true);
                }
            else if (time.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Time');", true);
            }
            else if (pname.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Patients Name');", true);
            }
            else if (purpose.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Purpose');", true);
            }
            else if (contactnum.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Mobile Number');", true);
            }
            else
            {
                object n;
                int res;
                cmd = new SqlCommand("Select count(*) from dbo.bookapp where time= '" + ddtime.SelectedItem.Value + "' and date='" + date.Text + "'", con);
                n = cmd.ExecuteScalar();
                res = (int)n;
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Appointment Time not avalilable');", true);
                    time.Text = "";
                }
                else
                {
                    cmd = new SqlCommand("insert into dbo.bookapp values('" + docid.Text + "','" + date.Text + "','" + time.Text + "','" + pname.Text + "','" + purpose.Text + "','" + contactnum.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your Appointment Has Been Booked! Thank You!');", true);
                    con.Close();
                    docid.Text = "";
                    date.Text = "";
                    time.Text = "";
                    pname.Text = "";
                    purpose.Text = "";
                    contactnum.Text = "";
                    qualification.Text = "";
                    experience.Text = "";
                    clinicname.Text = "";
                    address.Text = "";
                    fee.Text = "";
                    timings.Text = "";
                    docnum.Text = "";
                }
            }
        }

        public void Calendar1_DayRender(object o, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Today)
            {
                e.Day.IsSelectable = false;
            }
            if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                e.Day.IsSelectable = false;
            }
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            date.Text = Calendar1.SelectedDate.ToShortDateString();
        }
        protected void btnview_Click(object sender, EventArgs e)
        {
             cmd = new SqlCommand("Select * from dbo.verified where date='"+ddate.Text+"' and num='" + pnum.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                prescription.Text = dr.GetString(6);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Prescription Found/Check Date/Check Mob No:');", true);
                pnum.Text = "";
            }
        }
        }
    }
