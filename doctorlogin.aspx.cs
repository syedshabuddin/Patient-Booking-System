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
    public partial class WebForm43 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ToString());
        //SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            //con = new SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=mydb");
            con.Open();
            docid.Focus();
        }
        protected void doclogin_Click(object sender, EventArgs e)
        {
            if (docid.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Docid');", true);
            }
            else if (docpassword.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Password');", true);
            }
            object n;
            int result;
            cmd = new SqlCommand("Select count(*) from dbo.doctorid where docid='" + docid.Text + "' and docpass='" + docpassword.Text + "'", con);
            
            n = cmd.ExecuteScalar();
            result = (int)n;
            if (result > 0)
            {
                Session["docid"] = docid.Text; 
                Response.Redirect("doctorhome.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Doctor ID/Password');", true);
                docpassword.Text="";
                con.Close();
            }
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}
