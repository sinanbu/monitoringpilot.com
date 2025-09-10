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


public partial class yonet_statisticse : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        if (Session["kapno"] == "" || (Session["kapno"] == null) )
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else
        {
            if (Session["yetki"].ToString() != "9")
            {
                Response.Redirect("http://www.monitoringpilot.com");
            }

            else
            {
                if (!IsPostBack)
                {
                    gridload();
                }
            }
           Litpagebaslik.Text = "MPTR Admin Page";
}
        baglanti.Close();
    }
    private void gridload()
    {
        if (Session["yetki"].ToString() == "9")
        {
            JLbuttondiv.Visible=false;
        }
        

        using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
        {
            var veri2 = from b2 in entikib.vardiyadetay.Where(b2 => b2.grt.Contains("99999999")) select b2;
            GridView5.DataSource = veri2.ToList();
            GridView5.DataBind();
        }
    }

    protected void GridView5_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView5.SelectedIndex)
        {
            e.Cancel = true;
            GridView5.SelectedIndex = -1;
        }
        LBgetist3.Visible = true;
    }

    protected void LBguvcik_Click(object sender, EventArgs e)
    {
        if (Session["kapno"] == "" || (Session["kapno"] == null))
        {
            Response.Redirect("../pmtr.aspx");
        }
        else
        {
            AnaKlas.logrecup(Session["kapno"].ToString());
            Session.Abandon();
            Response.Redirect("../pmtr.aspx");
        }
    }


    protected void LBgetist3_Click(object sender, EventArgs e)
    {
            string shipal = AnaKlas.Temizle(TextBox5.Text.ToString());

            using (PilotdbEntitiesvardetay entiki = new PilotdbEntitiesvardetay())
            {
                var veri = from b in entiki.vardiyadetay.Where(b => b.gemiadi.Contains(shipal)).ToList() select b;

                if (shipal.Length < 2 || shipal == null || shipal == "")
                {
                    Lwoidps.Text = "";
                    GridView5.DataSource = veri.Where(x => x.grt == "99999999").ToList();
                    GridView5.DataBind();
                }
                else
                {
                    foreach (var c in veri)
                    {

                    SqlConnection baglanti = AnaKlas.baglan();

                    SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                    cmdPilotismial.CommandType = CommandType.StoredProcedure;
                    cmdPilotismial.Parameters.AddWithValue("@secilikapno", c.degismecikapno);
                    string isimal = "";
                    SqlDataReader dr = cmdPilotismial.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            isimal = dr["kapadisoyadi"].ToString();
                        }
                    }
                    dr.Close();
                    cmdPilotismial.Dispose();

                    baglanti.Close();
                    c.pilotismi = isimal;
                    c.orderbay = AnaKlas.TarihSaatYapDMYhm(c.istasyoncikis);
                    }

                    GridView5.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                    GridView5.DataBind();

                    Lwoidps.Text = veri.Count() + "  movement of " + shipal;
                    
                }
        }

    }

    protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView5.PageIndex = e.NewPageIndex;
        string shipal = AnaKlas.Temizle(TextBox5.Text.ToString());

                using (PilotdbEntitiesvardetay entiki = new PilotdbEntitiesvardetay())
        {
            var veri = from b in entiki.vardiyadetay.Where(b => b.gemiadi.Contains(shipal)).ToList() select b;

            if (shipal.Length == 0 || shipal == null || shipal == "")
            {
                Lwoidps.Text = "";
                GridView5.DataSource = veri.Where(x => x.grt == "99999999").ToList();
                GridView5.DataBind();
            }
            else
            {
                foreach (var c in veri)
                {

                    SqlConnection baglanti = AnaKlas.baglan();

                    SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                    cmdPilotismial.CommandType = CommandType.StoredProcedure;
                    cmdPilotismial.Parameters.AddWithValue("@secilikapno", c.degismecikapno);
                    string isimal = "";
                    SqlDataReader dr = cmdPilotismial.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            isimal = dr["kapadisoyadi"].ToString();
                        }
                    }
                    dr.Close();
                    cmdPilotismial.Dispose();

                    baglanti.Close();
                    c.pilotismi = isimal;
                    c.orderbay = AnaKlas.TarihSaatYapDMYhm(c.istasyoncikis);
                }

                GridView5.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                GridView5.DataBind();

                Lwoidps.Text = veri.Count() + "  movement of " + shipal;
                
            }
        }


    }


    protected void liistatik1_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsa.aspx");
    }
    protected void liistatik2_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsb.aspx");
    }
    protected void liistatik3_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsc.aspx");
    }
    protected void liistatik4_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsd.aspx");
    }

    protected void liistatik6_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsf.aspx");
    }
    protected void liistatik7_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsg.aspx");
    }
    protected void liistatik8_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsh.aspx");
    }
    protected void liistatik9_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsi.aspx");
    }




}