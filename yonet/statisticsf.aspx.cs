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


public partial class yonet_statisticsf : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        if (Session["kapno"] == "" || (Session["kapno"] == null) || cmdlogofbak.ExecuteScalar() == null || Session["yetki"].ToString() != "9")
        {
            Response.Redirect("../pmtr.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                gridload();
            } 
            Litpagebaslik.Text = "PMTR Admin Page";
        }

       
        baglanti.Close();
    }

    private void gridload()
    {
        using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
        {
            var veri2 = from b2 in entikib.vardiyadetay.Where(b2 => b2.grt.Contains("99999999")) select b2;
            GridView6.DataSource = veri2.ToList();
            GridView6.DataBind();
        }
        DataTable DTkaplar = AnaKlas.GetDataTable("Select distinct limanadi, limanno  from limanlar where goster=1 and limanno<1100  order by limanadi");
        DDLPilots.Items.Clear();
        DDLPilots.DataTextField = "limanadi";
        DDLPilots.DataValueField = "limanno";
        DDLPilots.DataSource = DTkaplar;
        DDLPilots.DataBind();
     }


    protected void GridView6_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView6.SelectedIndex)
        {
            e.Cancel = true;
            GridView6.SelectedIndex = -1;
        }
        DDLPilots.Visible = true;
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

        string limanal = DDLPilots.SelectedItem.Text.ToString();

                using (PilotdbEntitiesvardetay entiki = new PilotdbEntitiesvardetay())
        {
            var veri = from b in entiki.vardiyadetay.Where(b => b.gemiadi != "TAKVİYE").Where(b => b.binisyeri == limanal || b.inisyeri==limanal).ToList() select b;

            SqlConnection baglanti = AnaKlas.baglan();
            string isimal = "";
            string tipik = "";
            string tipi = "";
            foreach (var c in veri)
            {
                 tipik = "";
                if (c.tip.ToString() == "" || c.tip.ToString() == null)
                {
                     tipi = tipik;
                }
                else
                {
                     tipi = c.tip.ToString();
                    c.tipi = tipi.Substring(0, 3);
                }

                SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                cmdPilotismial.CommandType = CommandType.StoredProcedure;
                cmdPilotismial.Parameters.AddWithValue("@secilikapno", c.degismecikapno);
                isimal = "";
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

            
                c.pilotismi = isimal;
                c.orderbay = Convert.ToDateTime(c.poff);
            }

            baglanti.Close();

                GridView6.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                GridView6.DataBind();


                Lwoidpp.Text = veri.Count() + " jobs in port of " + DDLPilots.SelectedItem.Text.ToString();
           }


    }

    protected void GridView6_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView6.PageIndex = e.NewPageIndex;

        string limanal = DDLPilots.SelectedItem.Text.ToString();

                using (PilotdbEntitiesvardetay entiki = new PilotdbEntitiesvardetay())
        {
            var veri = from b in entiki.vardiyadetay.Where(b => b.gemiadi != "TAKVİYE").Where(b => b.binisyeri == limanal || b.inisyeri == limanal).ToList()  select b;
            SqlConnection baglanti = AnaKlas.baglan();
            string isimal = "";
            string tipik = "";
            string tipi = "";

            foreach (var c in veri)
            {
                 tipik = "";
                if (c.tip.ToString() == "" || c.tip.ToString() == null)
                {
                     tipi = tipik;
                }
                else
                {
                     tipi = c.tip.ToString();
                    c.tipi = tipi.Substring(0, 3);
                }

                SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                cmdPilotismial.CommandType = CommandType.StoredProcedure;
                cmdPilotismial.Parameters.AddWithValue("@secilikapno", c.degismecikapno);
                isimal = "";
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

              
                c.pilotismi = isimal;

                c.orderbay = Convert.ToDateTime(c.poff);
            }

  baglanti.Close();
            GridView6.DataSource = veri.OrderByDescending(x=> x.orderbay).ToList();
            GridView6.DataBind();
            
                Lwoidpp.Text = veri.Count() + " jobs in port of " + DDLPilots.SelectedItem.Text.ToString();
            
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
    protected void liistatik5_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticse.aspx");
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