using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class yonet_yonetim : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
        LBA.Style.Add("cursor", "default");

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

            if (Session["kapno"] == null)
            {
                Response.Redirect("http://www.monitoringpilot.com");
            }
            else if (Session["yetki"].ToString() != "9" )
            {
                Response.Redirect("http://www.monitoringpilot.com");
            }
            else if (cmdlogofbak.ExecuteScalar() == null)
            {
                Response.Redirect("http://www.monitoringpilot.com");
            }

            cmdlogofbak.Dispose();
            baglanti.Close();
 
        //    else
        //{
        //    if (Session["yetki"].ToString() != "9")
        //    {
        //        menuleft.Visible = false;
        //        menuonline.Visible = false;
        //}
        //    //  Lblonline.Text = Session["kapno"].ToString() + ' ' + Session["kapadi"].ToString() + ' ' + Session["kapsoyadi"].ToString() + ' ' + Session["Yetki"].ToString();
        //}


    }
    protected void LBguvcik_Click(object sender, EventArgs e)
    {
        if (Session["kapno"] == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else
        {
            AnaKlas.logrecup(Session["kapno"].ToString());
            Session.Abandon();
            Response.Redirect("../pmtr.aspx");
        }
    }

    protected void LBA_Click(object sender, EventArgs e)
    {
        if (Session["kapno"].ToString() == "28" || (Session["kapno"].ToString()) == "135") 
        {
            Response.Redirect("adminsayfalar.aspx");
        }
    }
}