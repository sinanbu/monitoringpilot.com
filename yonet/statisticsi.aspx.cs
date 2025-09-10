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


public partial class yonet_statisticsi : System.Web.UI.Page
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
            GridView9.DataSource = veri2.ToList();
            GridView9.DataBind();
        }
    }

    protected void GridView9_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView9.SelectedIndex)
        {
            e.Cancel = true;
            GridView9.SelectedIndex = -1;
        }
        TextBox9.Visible = true;
        LBgetist3.Visible = true;
    }
    private bool aykontrol(string aykopi)
    {
        int degeryil = Convert.ToInt32(aykopi.Substring(0, 4));
            if (degeryil > 2000 & degeryil < 2100)
            {
                return true;
            }
            else
                return false;
    }
    protected void LBgetist3_Click(object sender, EventArgs e)
    {
       
            string ayal = TextBox9.Text.ToString().Trim();

            using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
            {
                var veri = from b in entikib.vardiyadetay.Where(b => b.gemiadi!="TAKVİYE").Where(b => b.poff.Contains(ayal)).ToList()  select b;

                 if (ayal.Length == 0 || AnaKlas.Altcizgisil(ayal).Length != 4)
                {
                    Lwoidyillik.Text = "Please Enter a Year!";
                    TextBox9.Text = "";
                    GridView9.DataSource = veri.Where(x => x.grt == "99999999").ToList();
                    GridView9.DataBind();
                }
                else
                {
                    if (aykontrol(ayal) == false)
                    {
                        Lwoidyillik.Text = "Please Enter a Valid Year(2000-2100)!";
                        TextBox9.Text = "";
                        GridView9.DataSource = veri.Where(x => x.grt == "99999999").ToList();
                        GridView9.DataBind();
                    }
                    else
                    {
                    SqlConnection baglanti = AnaKlas.baglan();
                    string tipik = "";
                    string tipi = "";
                    string isimal = "";

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
                        GridView9.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                        GridView9.DataBind();
                        Lwoidyillik.Text = veri.Count() + " vessel traffics in " + ayal;
                    }
                }
           
    }

    }

    protected void GridView9_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView9.PageIndex = e.NewPageIndex;

        string ayal = TextBox9.Text.ToString().Trim();

        using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
        {
            var veri = from b in entikib.vardiyadetay.Where(b => b.gemiadi != "TAKVİYE").Where(b => b.poff.Contains(ayal)).ToList() select b;

            if (ayal.Length == 0 || AnaKlas.Altcizgisil(ayal).Length != 4)
            {
                Lwoidyillik.Text = "Please Enter a Year!";
                TextBox9.Text = "";
                GridView9.DataSource = veri.Where(x => x.grt == "99999999").ToList();
                GridView9.DataBind();
            }
            else
            {
                if (aykontrol(ayal) == false)
                {
                    Lwoidyillik.Text = "Please Enter a Valid Year(2000-2100)!";
                    TextBox9.Text = "";
                    GridView9.DataSource = veri.Where(x => x.grt == "99999999").ToList();
                    GridView9.DataBind();
                }
                else
                {
                    SqlConnection baglanti = AnaKlas.baglan();
                    string tipik = "";
                    string tipi = "";
                    string isimal = "";

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
                    GridView9.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                    GridView9.DataBind();
                    Lwoidyillik.Text = veri.Count() + " vessel traffics in " + ayal;
                }
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



}