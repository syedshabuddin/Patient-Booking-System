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

namespace WebApplication1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string str = (string)Session["username"];
            username.Text = str;
            if (str == "")
                Response.Redirect("logindetails.aspx");
           
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Response.Redirect("logindetails.aspx");
        }

        protected void weluser0_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
