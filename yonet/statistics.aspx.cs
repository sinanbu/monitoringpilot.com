using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;


public partial class yonet_statistics : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        if (Session["kapno"] == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["yetki"].ToString() != "9")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (cmdlogofbak.ExecuteScalar() == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }

        
       else
        {
            if (!IsPostBack)
            {
            }

            Litpagebaslik.Text = "PMTR Admin Page";
        }
        cmdlogofbak.Dispose();
        baglanti.Close();         
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





    protected void liistatik1_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2") { }
        else { Response.Redirect("statisticsa.aspx"); }
    }
    protected void liistatik2_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2") { }
        else { Response.Redirect("statisticsb.aspx"); }
    }
    protected void liistatik3_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2") { }
        else { Response.Redirect("statisticsc.aspx");}
    }
    protected void liistatik4_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2") { }
        else { Response.Redirect("statisticsd.aspx");}
    }
    protected void liistatik5_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2") { }
        else { Response.Redirect("statisticse.aspx");}
    }
    protected void liistatik6_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2") { }
        else { Response.Redirect("statisticsf.aspx");}
    }
    protected void liistatik7_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2") { }
        else { Response.Redirect("statisticsg.aspx");}
    }
    protected void liistatik8_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2") { }
        else { Response.Redirect("statisticsh.aspx");}
    }
    protected void liistatik9_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "0" || Session["yetki"].ToString() == "1" || Session["yetki"].ToString() == "2") { }
        else { Response.Redirect("statisticsi.aspx");}
    }




}