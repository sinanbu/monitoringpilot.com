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


public partial class yonet_statisticsd : System.Web.UI.Page
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
            GridView4.DataSource = veri2.ToList();
            GridView4.DataBind();
            Lwoidpp.Text = "";
        }

        //if (DDLPilots.SelectedItem.Text!="") DDLPilots.Items.FindByText(DDLPilots.SelectedItem.Text).Selected = true;

        DataTable DTkaplar = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where  kapsirano<1000 order by kapadisoyadi asc");
        DDLPilots.Items.Clear();
        DDLPilots.DataTextField = "kapadisoyadi";
        DDLPilots.DataValueField = "kapno";
        DDLPilots.DataSource = DTkaplar;
        DDLPilots.DataBind();
        //DDLPilots.Items.Insert(0, new ListItem("Select", String.Empty));
        //DDLPilots.SelectedIndex = 0;

    }

 
    protected void GridView4_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView4.SelectedIndex)
        {
            e.Cancel = true;
            GridView4.SelectedIndex = -1;
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

        int kapnoal = Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString());


        using (PilotdbEntitiesvardetay entiki = new PilotdbEntitiesvardetay())
        {
            entiki.Configuration.ProxyCreationEnabled = false; // bütün entitilere eklenecek.
 //           System.Globalization.CultureInfo trTR = new System.Globalization.CultureInfo("tr-TR");
//            .OrderByDescending(b => Convert.ToDateTime(b.istasyoncikis.ToString(),trTR))
            var veri = from b in entiki.vardiyadetay.Where(b => b.kapno == kapnoal || b.degismecikapno == kapnoal).ToList() select b;

                foreach (var c in veri)
                {
                    string tipi="";
                    string tipik = "";
                    if (c.tip.ToString() == "" || c.tip.ToString() == null)
                    {
                        tipi = tipik;
                    }
                    else
                    {
                        tipi = c.tip.ToString();
                        c.tipi = tipi.Substring(0, 1);
                    }
                    c.orderbay = Convert.ToDateTime(c.istasyoncikis);

                string kapadisoyadi = "";

                if (c.kapno == c.degismecikapno)
                { c.pilotismi = DDLPilots.SelectedItem.Text.ToString(); }
                else { 

                    if (c.kapno.ToString() == DDLPilots.SelectedItem.Value.ToString())
                        {
                        SqlConnection baglanti = AnaKlas.baglan();
                        SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                        cmdPilotismial.CommandType = CommandType.StoredProcedure;
                        cmdPilotismial.Parameters.AddWithValue("@secilikapno", c.degismecikapno);
                        SqlDataReader dr = cmdPilotismial.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                kapadisoyadi = dr["kapadisoyadi"].ToString();
                            }
                        }
                        dr.Close();
                        cmdPilotismial.Dispose();
                        baglanti.Close();

                        c.pilotismi = DDLPilots.SelectedItem.Text.ToString() + "(" + kapadisoyadi + ")"; }

                    else if (c.degismecikapno.ToString() == DDLPilots.SelectedItem.Value.ToString())
                        {
                        SqlConnection baglanti = AnaKlas.baglan();
                        SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                        cmdPilotismial.CommandType = CommandType.StoredProcedure;
                        cmdPilotismial.Parameters.AddWithValue("@secilikapno", c.kapno);
                        SqlDataReader dr = cmdPilotismial.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                kapadisoyadi = dr["kapadisoyadi"].ToString();
                            }
                        }
                        dr.Close();
                        cmdPilotismial.Dispose();
                        baglanti.Close();
                        c.pilotismi = kapadisoyadi + "(" + DDLPilots.SelectedItem.Text.ToString() + ")"; }
                     }




            }

            Lwoidpp.Text = veri.Count()+ " jobs of Capt. " + DDLPilots.SelectedItem.Text.ToString();

                GridView4.DataSource = veri.OrderByDescending(b=> b.orderbay).ToList();
                GridView4.DataBind();
                    
               
            }
         }

    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView4.PageIndex = e.NewPageIndex;

        int kapnoal = Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString());

        using (PilotdbEntitiesvardetay entiki = new PilotdbEntitiesvardetay())
        {
            var veri = from b in entiki.vardiyadetay.Where(b => b.kapno == kapnoal || b.degismecikapno == kapnoal).ToList() select b;
      
                foreach (var c in veri)
                {
                    string tipik = "";
                    if (c.tip.ToString() == "" || c.tip.ToString() == null)
                    {
                        string tipi = tipik;
                    }
                    else
                    {
                        string tipi = c.tip.ToString();
                        c.tipi = tipi.Substring(0, 1);
                    }
                    c.orderbay = Convert.ToDateTime(c.istasyoncikis);

                string kapadisoyadi = "";
                if (c.kapno == c.degismecikapno)
                { c.pilotismi = DDLPilots.SelectedItem.Text.ToString(); }
                else
                {

                    if (c.kapno.ToString() == DDLPilots.SelectedItem.Value.ToString())
                    {
                        SqlConnection baglanti = AnaKlas.baglan();
                        SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                        cmdPilotismial.CommandType = CommandType.StoredProcedure;
                        cmdPilotismial.Parameters.AddWithValue("@secilikapno", c.degismecikapno);
                        SqlDataReader dr = cmdPilotismial.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                kapadisoyadi = dr["kapadisoyadi"].ToString();
                            }
                        }
                        dr.Close();
                        cmdPilotismial.Dispose();
                        baglanti.Close();

                        c.pilotismi = DDLPilots.SelectedItem.Text.ToString() + "(" + kapadisoyadi + ")";
                    }

                    else if (c.degismecikapno.ToString() == DDLPilots.SelectedItem.Value.ToString())
                    {
                        SqlConnection baglanti = AnaKlas.baglan();
                        SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                        cmdPilotismial.CommandType = CommandType.StoredProcedure;
                        cmdPilotismial.Parameters.AddWithValue("@secilikapno", c.kapno);
                        SqlDataReader dr = cmdPilotismial.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                kapadisoyadi = dr["kapadisoyadi"].ToString();
                            }
                        }
                        dr.Close();
                        cmdPilotismial.Dispose();
                        baglanti.Close();
                        c.pilotismi = kapadisoyadi + "(" + DDLPilots.SelectedItem.Text.ToString() + ")";
                    }
                }




            }

            GridView4.DataSource = veri.OrderByDescending(b => b.orderbay).ToList();
                GridView4.DataBind();
                Lwoidpp.Text = veri.Count() + " jobs of Capt. " + DDLPilots.SelectedItem.Text.ToString();
           
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
    protected void liistatik5_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticse.aspx");
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