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
    public partial class WebForm47 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ToString());
        //SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            //con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=mydb");
            con.Open();
            docname.Focus();
            string str = (string)Session["username"];
            username.Text = str;
            if (str == "")
                Response.Redirect("adminlogin.aspx");
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (docname.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Doctor Name');", true);
            }
            else if(docid.Text=="")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Docid');", true);
            }
            else if(docpass.Text=="")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Password For Doctor');", true);
            }
            else
            {
                object n;
                int res;
                cmd = new SqlCommand("Select count(*) from dbo.doctorid where (docid='" + docid.Text + "')", con);
                n = cmd.ExecuteScalar();
                res = (int)n;
                if (res > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('DocID Already exist');", true);
                    docname.Text = "";
                    docid.Text = "";
                    docpass.Text = "";
                    con.Close();
                }
                else
                {
                    cmd = new SqlCommand("insert into dbo.doctorid values('" + docname.Text + "','" + docid.Text + "','" + docpass.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Doctor ID Created!!');", true);
                    docname.Text = "";
                    docid.Text = "";
                    docpass.Text = "";
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
