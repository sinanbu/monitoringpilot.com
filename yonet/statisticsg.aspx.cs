using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using NameTextSharp = iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text;


public partial class yonet_statisticsg : System.Web.UI.Page
{
    Metodlar AnaKlas = new Metodlar();

    public object Responce { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);


        if (Session["kapno"] == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (cmdlogofbak.ExecuteScalar() == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (Session["yetki"].ToString() != "9")
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }

        else
        {
            this.Page.Form.DefaultFocus = this.TextBox7.ClientID;
            this.Page.Form.DefaultButton = this.LBgetist3.UniqueID;


            if (!IsPostBack)
            {
                gridload();
                TextBox7.Text = DateTime.Now.ToShortDateString();
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
            GridView7.DataSource = veri2.ToList();
            GridView7.DataBind();
        }

        Ldikbol.Visible = false;
        LBgeri.Visible = false;
        LBileri.Visible = false;

    }



    protected void LBgetist3_Click(object sender, EventArgs e)
    {
        GridView7.PageIndex = 0; 
        procesok();
    }
    protected void GridView7_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView7.PageIndex = e.NewPageIndex;
        procesok();
    }
    protected void GridView7_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView7.SelectedIndex)
        {
            e.Cancel = true;
            GridView7.SelectedIndex = -1;
        }
        TextBox7.Visible = true;
        LBgetist3.Visible = true;
    }


    private void procesok()
    {

        Panel1.Visible = true;
        Panel2.Visible = false;

        string gunal = TextBox7.Text.ToString().Trim();

        using (PilotdbEntitiesvardetay entikib = new PilotdbEntitiesvardetay())
        {
            var veri = from b in entikib.vardiyadetay.Where(b => b.gemiadi != "TAKVİYE").Where(b => b.poff.Contains(gunal)).ToList() select b;

            if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
            {
                Lwoidgunluk.Text = "Please Enter a Valid Date.";
                TextBox7.Text = "";
                GridView7.DataSource = veri.Where(x => x.grt == "99999999").ToList();
                GridView7.DataBind();
                Ldikbol.Visible = false;
                LBgeri.Visible = false;
                LBileri.Visible = false;
            }
            else
            {
                SqlConnection baglanti = AnaKlas.baglan();
                string tipik = "";
                string isimal = "";

                foreach (var c in veri)
                {
                    tipik = "";
                    if (c.tip.ToString() == "" || c.tip.ToString() == null)
                    {
                        string tipi = tipik;
                    }
                    else
                    {
                        string tipi = c.tip.ToString();
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


                GridView7.DataSource = veri.OrderByDescending(x => x.orderbay).ToList();
                GridView7.DataBind();
                Lwoidgunluk.Text = veri.Count() + " vessel traffics in " + gunal;
                Ldikbol.Visible = true;
                LBgeri.Visible = true;
                LBileri.Visible = true;
            }
        }
    }
    protected void LBgeri_Click(object sender, EventArgs e)
    {
        string gunal = TextBox7.Text.ToString().Trim();
        if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
        {
            Lwoidgunluk.Text = "Please Enter a Valid Date.";
            TextBox7.Text = "";
            Ldikbol.Visible = false;
            LBgeri.Visible = false;
            LBileri.Visible = false;
        }
        else
        {
            TextBox7.Text = TarihYaziYapDMY(Convert.ToDateTime(gunal).AddDays(-1));
            GridView7.PageIndex = 0;
            procesok();
        }
    }
    protected void LBileri_Click(object sender, EventArgs e)
    {
        string gunal = TextBox7.Text.ToString().Trim();
        if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
        {
            Lwoidgunluk.Text = "Please Enter a Valid Date.";
            TextBox7.Text = "";
            Ldikbol.Visible = false;
            LBgeri.Visible = false;
            LBileri.Visible = false;
        }
        else
        {
            TextBox7.Text = TarihYaziYapDMY(Convert.ToDateTime(gunal).AddDays(1));
            GridView7.PageIndex = 0;
            procesok();
        }
    }




    protected void LBjsr_Click(object sender, EventArgs e)
    {
        procesokrep();
 }
    private void procesokrep()
    {
        Panel1.Visible =false ;
        Panel2.Visible = true;

        TBcancel.Visible = false;

        string tarih = "";
        string oncegunissay = "";
        string yanasiklar = "";
        string eskiyalodemir = "";
        string herekedemir = "";
        string yarizdemir = "";
        string incoming = "";
        string sontalepiz = "";
        string sontalepyal = "";
        string sontalepsaf = "";
        string dekasis = "";
        string safis = "";
        string pilotsay = "";

        TBsatir5.Text = "";
        TBsatir7.Text = "";
        TBsatir8.Text = "";
        TBsatir9.Text = "";
        TBsatir10.Text = "";
        TBsatir11.Text = "";
        TBsatir12a.Text = "";
        TBsatir12b.Text = "";
        TBsatir12c.Text = "";
        TBsatir14.Text = "";
        TBsatir15.Text = "";
        TBsatir16.Text = "";

        TTBsatir5.Text = "";
        TTBsatir7.Text = "";
        TTBsatir8.Text = "";
        TTBsatir9.Text = "";
        TTBsatir10.Text = "";
        TTBsatir11.Text = "";
        TTBsatir12a.Text = "";
        TTBsatir12b.Text = "";
        TTBsatir12c.Text = "";
        TTBsatir14.Text = "";
        TTBsatir15.Text = "";
        TTBsatir16.Text = "";

        string gunal = TextBox7.Text.ToString().Trim();
        if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
        {
            Lwoidgunluk2.Text = "Please Enter a Valid Date.";
            TextBox7.Text = "";
            Ldikbol2.Visible = false;
            LBgeri2.Visible = false;
            LBileri2.Visible = false;

        }
        else {


            Lwoidgunluk2.Text = " Job Status Report for " + gunal;
        Ldikbol2.Visible = true;
        LBgeri2.Visible = true;
        LBileri2.Visible = true;

            TBSatir1.Text = gunal;
            Labelsatir4.Text = Convert.ToDateTime(gunal).AddDays(-1).ToShortDateString() + "/00:01&nbsp;&nbsp;-&nbsp;&nbsp;" + Convert.ToDateTime(gunal).AddDays(-1).ToShortDateString() + "/23:59 itibari ile;";
            Labelsatir6.Text = gunal + " gününe ait;";
            mailmes.Text = "Günlük İş Raporu :" + TBSatir1.Text + "</br></br> E-Mail Sent.";

            SqlConnection baglanti = AnaKlas.baglan();

            tarih = gunal;//TarihYaziYapDMY(Convert.ToDateTime(gunal).AddDays(-1));


            //OKUMA OK

            SqlCommand cmdjsroku = new SqlCommand("SP_jsreport", baglanti); // jsrapordan sıralı oku
            cmdjsroku.CommandType = CommandType.StoredProcedure;
            cmdjsroku.Parameters.AddWithValue("@tarih", tarih);
            SqlDataReader dr = cmdjsroku.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    oncegunissay = dr["oncegunissay"].ToString();
                    yanasiklar = dr["yanasiklar"].ToString();
                    eskiyalodemir = dr["eskiyalodemir"].ToString();
                    herekedemir = dr["herekedemir"].ToString();
                    yarizdemir = dr["yarizdemir"].ToString();
                    incoming = dr["incoming"].ToString();
                    sontalepiz = dr["sontalepiz"].ToString();
                    sontalepyal = dr["sontalepyal"].ToString();
                    sontalepsaf = dr["sontalepsaf"].ToString();
                    dekasis = dr["dekasis"].ToString();
                    safis = dr["safis"].ToString();
                    pilotsay = dr["pilotsay"].ToString();


                    TBsatir5.Text = oncegunissay;
                    TBsatir7.Text = yanasiklar;
                    TBsatir8.Text = eskiyalodemir;
                    TBsatir9.Text = herekedemir;
                    TBsatir10.Text = yarizdemir;
                    TBsatir11.Text = incoming;
                    TBsatir12a.Text = sontalepiz;
                    TBsatir12b.Text = sontalepyal;
                    TBsatir12c.Text = sontalepsaf;
                    TBsatir14.Text = dekasis;
                    TBsatir15.Text = safis;
                    TBsatir16.Text = pilotsay;


                    TTBsatir5.Text = oncegunissay;
                    TTBsatir7.Text = yanasiklar;
                    TTBsatir8.Text = eskiyalodemir;
                    TTBsatir9.Text = herekedemir;
                    TTBsatir10.Text = yarizdemir;
                    TTBsatir11.Text = incoming;
                    TTBsatir12a.Text = sontalepiz;
                    TTBsatir12b.Text = sontalepyal;
                    TTBsatir12c.Text = sontalepsaf;
                    TTBsatir14.Text = dekasis;
                    TTBsatir15.Text = safis;
                    TTBsatir16.Text = pilotsay;

                }
            }
            dr.Close();
            cmdjsroku.Dispose();
            baglanti.Close();

        }
    }
    protected void LBsave_Click(object sender, EventArgs e)
    {

        string gunal = TextBox7.Text.ToString().Trim();
        if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
        {
            Lwoidgunluk2.Text = "Please Enter a Valid Date.";
            TextBox7.Text = "";
            Ldikbol2.Visible = false;
            LBgeri2.Visible = false;
            LBileri2.Visible = false;
        }
        else
        {

            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand cmdjsrup = new SqlCommand("SP_jsreport_UpClien", baglanti);
            cmdjsrup.CommandType = CommandType.StoredProcedure;
            cmdjsrup.Parameters.AddWithValue("@tarih", TextBox7.Text);
            cmdjsrup.Parameters.AddWithValue("@oncegunissay", TTBsatir5.Text);
            cmdjsrup.Parameters.AddWithValue("@yanasiklar", TTBsatir7.Text);
            cmdjsrup.Parameters.AddWithValue("@eskiyalodemir", TTBsatir8.Text);
            cmdjsrup.Parameters.AddWithValue("@herekedemir", TTBsatir9.Text);
            cmdjsrup.Parameters.AddWithValue("@yarizdemir", TTBsatir10.Text);
            cmdjsrup.Parameters.AddWithValue("@incoming", TTBsatir11.Text);
            cmdjsrup.Parameters.AddWithValue("@sontalepiz", TTBsatir12a.Text);
            cmdjsrup.Parameters.AddWithValue("@sontalepyal", TTBsatir12b.Text);
            cmdjsrup.Parameters.AddWithValue("@sontalepsaf", TTBsatir12c.Text);
            cmdjsrup.Parameters.AddWithValue("@dekasis", TTBsatir14.Text);
            cmdjsrup.Parameters.AddWithValue("@safis", TTBsatir15.Text);
            cmdjsrup.Parameters.AddWithValue("@pilotsay", TTBsatir16.Text);

            cmdjsrup.ExecuteNonQuery();
            cmdjsrup.Dispose();
            baglanti.Close();
            baglanti.Dispose();

            TTBsatir5.Visible = false;
            TTBsatir7.Visible = false;
            TTBsatir8.Visible = false;
            TTBsatir9.Visible = false;
            TTBsatir10.Visible = false;
            TTBsatir11.Visible = false;
            TTBsatir12a.Visible = false;
            TTBsatir12b.Visible = false;
            TTBsatir12c.Visible = false;
            TTBsatir14.Visible = false;
            TTBsatir15.Visible = false;
            TTBsatir16.Visible = false;

            LBsave.Visible = false;
            LBupdate.Visible = true;

            TBsatir5.Visible = true;
            TBsatir7.Visible = true;
            TBsatir8.Visible = true;
            TBsatir9.Visible = true;
            TBsatir10.Visible = true;
            TBsatir11.Visible = true;
            TBsatir12a.Visible = true;
            TBsatir12b.Visible = true;
            TBsatir12c.Visible = true;
            TBsatir14.Visible = true;
            TBsatir15.Visible = true;
            TBsatir16.Visible = true;
            procesokrep();
        }



    }
    protected void LBupdate_Click(object sender, EventArgs e)
    {
        LBsave.Visible = true;
        LBupdate.Visible = false;
        TBcancel.Visible = true;

        TBsatir5.Visible = false;
        TBsatir7.Visible = false;
        TBsatir8.Visible = false;
        TBsatir9.Visible = false;
        TBsatir10.Visible = false;
        TBsatir11.Visible = false;
        TBsatir12a.Visible = false;
        TBsatir12b.Visible = false;
        TBsatir12c.Visible = false;
        TBsatir14.Visible = false;
        TBsatir15.Visible = false;
        TBsatir16.Visible = false;

        TTBsatir5.Visible = true;
        TTBsatir7.Visible = true;
        TTBsatir8.Visible = true;
        TTBsatir9.Visible = true;
        TTBsatir10.Visible = true;
        TTBsatir11.Visible = true;
        TTBsatir12a.Visible = true;
        TTBsatir12b.Visible = true;
        TTBsatir12c.Visible = true;
        TTBsatir14.Visible = true;
        TTBsatir15.Visible = true;
        TTBsatir16.Visible = true;

    }
    protected void TBcancel_Click(object sender, EventArgs e)
    {
        TTBsatir5.Visible = false;
        TTBsatir7.Visible = false;
        TTBsatir8.Visible = false;
        TTBsatir9.Visible = false;
        TTBsatir10.Visible = false;
        TTBsatir11.Visible = false;
        TTBsatir12a.Visible = false;
        TTBsatir12b.Visible = false;
        TTBsatir12c.Visible = false;
        TTBsatir14.Visible = false;
        TTBsatir15.Visible = false;
        TTBsatir16.Visible = false;

        LBsave.Visible = false;
        LBupdate.Visible = true;
        TBcancel.Visible = false;

        TBsatir5.Visible = true;
        TBsatir7.Visible = true;
        TBsatir8.Visible = true;
        TBsatir9.Visible = true;
        TBsatir10.Visible = true;
        TBsatir11.Visible = true;
        TBsatir12a.Visible = true;
        TBsatir12b.Visible = true;
        TBsatir12c.Visible = true;
        TBsatir14.Visible = true;
        TBsatir15.Visible = true;
        TBsatir16.Visible = true;
        procesokrep();
    }
    protected void LBgeri2_Click(object sender, EventArgs e)
    {
        string gunal = TextBox7.Text.ToString().Trim();
        if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
        {
            Lwoidgunluk2.Text = "Please Enter a Valid Date.";
            TextBox7.Text = "";
            Ldikbol2.Visible = false;
            LBgeri2.Visible = false;
            LBileri2.Visible = false;
        }
        else
        {
            TextBox7.Text = TarihYaziYapDMY(Convert.ToDateTime(gunal).AddDays(-1));

            TTBsatir5.Visible = false;
            TTBsatir7.Visible = false;
            TTBsatir8.Visible = false;
            TTBsatir9.Visible = false;
            TTBsatir10.Visible = false;
            TTBsatir11.Visible = false;
            TTBsatir12a.Visible = false;
            TTBsatir12b.Visible = false;
            TTBsatir12c.Visible = false;
            TTBsatir14.Visible = false;
            TTBsatir15.Visible = false;
            TTBsatir16.Visible = false;

            LBsave.Visible = false;
            LBupdate.Visible = true;

            TBsatir5.Visible = true;
            TBsatir7.Visible = true;
            TBsatir8.Visible = true;
            TBsatir9.Visible = true;
            TBsatir10.Visible = true;
            TBsatir11.Visible = true;
            TBsatir12a.Visible = true;
            TBsatir12b.Visible = true;
            TBsatir12c.Visible = true;
            TBsatir14.Visible = true;
            TBsatir15.Visible = true;
            TBsatir16.Visible = true;

            procesokrep();

        }
    }
    protected void LBileri2_Click(object sender, EventArgs e)
    {
        string gunal = TextBox7.Text.ToString().Trim();
        if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
        {
            Lwoidgunluk2.Text = "Please Enter a Valid Date.";
            TextBox7.Text = "";
            Ldikbol2.Visible = false;
            LBgeri2.Visible = false;
            LBileri2.Visible = false;
        }
        else
        {
            TextBox7.Text = TarihYaziYapDMY(Convert.ToDateTime(gunal).AddDays(1));
            TTBsatir5.Visible = false;
            TTBsatir7.Visible = false;
            TTBsatir8.Visible = false;
            TTBsatir9.Visible = false;
            TTBsatir10.Visible = false;
            TTBsatir11.Visible = false;
            TTBsatir12a.Visible = false;
            TTBsatir12b.Visible = false;
            TTBsatir12c.Visible = false;
            TTBsatir14.Visible = false;
            TTBsatir15.Visible = false;
            TTBsatir16.Visible = false;

            LBsave.Visible = false;
            LBupdate.Visible = true;

            TBsatir5.Visible = true;
            TBsatir7.Visible = true;
            TBsatir8.Visible = true;
            TBsatir9.Visible = true;
            TBsatir10.Visible = true;
            TBsatir11.Visible = true;
            TBsatir12a.Visible = true;
            TBsatir12b.Visible = true;
            TBsatir12c.Visible = true;
            TBsatir14.Visible = true;
            TBsatir15.Visible = true;
            TBsatir16.Visible = true;

            procesokrep();

        }
    }

    protected void LBworkrest_Click(object sender, EventArgs e)
    {
        string gunal = TextBox7.Text.ToString().Trim();
        if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
        {
            Lwoidgunluk2.Text = "Please Enter a Valid Date.";
            TextBox7.Text = "";
            Ldikbol3.Visible = false;
            LBgeri2.Visible = false;
            LBileri2.Visible = false;

        }
        else
        {

            Response.Redirect("rhreports.aspx");

        }
    }


    private string Altcizgisil(string Metin)
    {
        string deger = Metin;
        deger = deger.Replace("_", "");
        return deger;
    }
    private string TarihSaatYaziYapDMYhm(DateTime TarihsaatDMYhms)
    {
        string TarihsaatYaziok, moys, doms, hhs, mms;

        int dayofmonth = TarihsaatDMYhms.Day;
        int monthofyear = TarihsaatDMYhms.Month;
        int yearofdate = TarihsaatDMYhms.Year;
        int saatofnow = TarihsaatDMYhms.Hour;
        int dakikaofnow = TarihsaatDMYhms.Minute;

        if (monthofyear < 10)
        {
            moys = "0" + monthofyear.ToString();
        }
        else
        {
            moys = monthofyear.ToString();
        }

        if (dayofmonth < 10)
        {
            doms = "0" + dayofmonth.ToString();
        }
        else
        {
            doms = dayofmonth.ToString();
        }

        if (saatofnow < 10)
        {
            hhs = "0" + saatofnow.ToString();
        }
        else
        {
            hhs = saatofnow.ToString();
        }

        if (dakikaofnow < 10)
        {
            mms = "0" + dakikaofnow.ToString();
        }
        else
        {
            mms = dakikaofnow.ToString();
        }

        TarihsaatYaziok = doms + "." + moys + "." + yearofdate.ToString() + " " + hhs + ":" + mms;

        return TarihsaatYaziok;
    }
    private string TarihYaziYapDMY(DateTime TarihsaatDMYhms)
    {
        string TarihYaziok = TarihSaatYaziYapDMYhm(TarihsaatDMYhms).Substring(0, 10);
        return TarihYaziok;
    }
    private bool IsDate2(string tarihyazi)
    {
        DateTime Temp;
        IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
        if (DateTime.TryParse(tarihyazi, culture, System.Globalization.DateTimeStyles.AssumeLocal, out Temp) == true)
            return true;
        else
            return false;
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
    protected void liistatik8_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsh.aspx");
    }
    protected void liistatik9_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsi.aspx");
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







    protected void LBsendemail_Click(object sender, EventArgs e)
    {

        string Subject = "İş Durumu Raporu / " + TBSatir1.Text; /* DateTime.Now.ToString();*/
        string Body = "";
        string toadres = "ccengiz@dekaspilot.com";

        Body = Body + "<table  style='width: 620px; height:400px; padding:2px; border:1px solid blue; background-color:white; font-family:Arial; font-size:10px;' >";
        Body = Body + "<tr><td colspan='5' style='text-align:right; Height:25px;  Width:90px; '>" + TBSatir1.Text + "</td></tr>";
        Body = Body + "<tr><td colspan='5' style='text-align:right;  font-weight:bold; font-style:italic;'> Darıca / Yarımca </td></tr>";
        Body = Body + "<tr><td colspan='5' style='text-align:center; font-weight:bold; font-size:14px; text-decoration:underline;'> İŞ DURUMU RAPORU </td></tr>";
        Body = Body + "<tr><td colspan='5' style='text-align:center; font-weight:bold;'>" + Labelsatir4.Text + "</td></tr>";
        Body = Body + "<tr><td style='text-align:left'> Toplam İş</td>";
        Body = Body + "<td style='text-align:center;' >&nbsp;:&nbsp;</td>";
        Body = Body + "<td  colspan='3' style='text-align:left;  Height:25px' >" + TBsatir5.Text + "</td></tr>";
        Body = Body + "<tr><td colspan='5' style='text-align:center; font-weight:bold;  Height:25px'> " + Labelsatir6.Text + "</td></tr>";

        Body = Body + "<tr><td style='text-align:left'>Yanaşık gemilerin sayısı</td>";
        Body = Body + "<td style='text-align:center;' >&nbsp;:&nbsp;</td>";
        Body = Body + "<td  colspan='3' style='text-align:left;  Height:25px'>" + TBsatir7.Text + "</td></tr>";

        Body = Body + "<tr><td style='text-align:left'>Eskihisar ve Yalova demirdeki gemi sayısı  </td>";
        Body = Body + "<td style='text-align:center;'>&nbsp;:&nbsp;</td>";
        Body = Body + "<td  colspan='3' style='text-align:left;  Height:25px'>" + TBsatir8.Text + "</td></tr>";

        Body = Body + "<tr><td style='text-align:left'>Hereke demirdeki gemi sayısı</td>";
        Body = Body + "<td style='text-align:center;' >&nbsp;:&nbsp;</td>";
        Body = Body + "<td  colspan='3' style='text-align:left;  Height:25px'>" + TBsatir9.Text + "</td></tr>";

        Body = Body + "<tr><td style='text-align:left'>Yarımca ve İzmit  demirdeki gemilerin sayısı  </td>";
        Body = Body + "<td style='text-align:center;' >&nbsp;:&nbsp;</td>";
        Body = Body + "<td  colspan='3' style='text-align:left;  Height:25px'>" + TBsatir10.Text + "</td></tr>";

        Body = Body + "<tr><td style='text-align:left'>Beklenen gemi sayısı</td>";
        Body = Body + "<td style='text-align:center; ' >&nbsp;:&nbsp;</td>";
        Body = Body + "<td  colspan='3' style='text-align:left;  Height:25px'>" + TBsatir11.Text + "</td></tr>";

        Body = Body + "<tr><td style='text-align:left'>Son talep numarası</td>";
        Body = Body + "<td style='text-align:center;' >&nbsp;:&nbsp;</td>";
        Body = Body + "<td style='text-align:left;  Height:25px; ' >" + TBsatir12a.Text + " / </td>";
        Body = Body + "<td style='text-align:left;'>" + TBsatir12b.Text + " / </td>";
        Body = Body + "<td style='text-align:left;'>" + TBsatir12c.Text + " / </td></tr>";

        Body = Body + "<tr><td style='text-align:center; width:300px;' >&nbsp;</td>";
        Body = Body + "<td style='text-align:center; width:20px;' >&nbsp;</td>";
        Body = Body + "<td style='text-align:center; width:100px;' >&nbsp;</td>";
        Body = Body + "<td style='text-align:center; width:100px;' >&nbsp;</td>";
        Body = Body + "<td style='text-align:center; width:100px;' >&nbsp;</td></tr>";

        Body = Body + "<tr><td style='text-align:left'>Dekaş</td>";
        Body = Body + "<td style='text-align:center; '>&nbsp;:&nbsp;</td>";
        Body = Body + "<td  colspan='3' style='text-align:left;  Height:25px '>" + TBsatir14.Text + "</td></tr>";

        Body = Body + "<tr><td style='text-align:left'>Derince</td>";
        Body = Body + "<td style='text-align:center; '>&nbsp;:&nbsp;</td>";
        Body = Body + "<td  colspan='3' style='text-align:left;  Height:25px '>" + TBsatir15.Text + "</td></tr>";

        Body = Body + "<tr><td style='text-align:left'>Çalışan Pilot Sayısı</td>";
        Body = Body + "<td style='text-align:center; '>&nbsp;:&nbsp;</td>";
        Body = Body + "<td colspan='3' style='text-align:left;  Height:25px '>" + TBsatir16.Text + "</td></tr></table>";

        Body = Body + "</br>DENİZ KILAVUZLUK A.Ş.</br>Send by Pilot Monitoring System.</br></br>";

        Mailtoone(toadres, Subject, Body);

        this.ModalPopupMessageok1.Show();
        this.ModalPopupMessageok.Show();


    }


    protected bool Mailtoone(string toadres, string Subject, string Body)
    {
        try
        {
            MailMessage mesaj = new MailMessage();

            mesaj.From = new MailAddress("dekasmonitoringpilot@gmail.com");
            mesaj.To.Add(new MailAddress(toadres));
            mesaj.Subject = Subject;// Mailinizin Konusunu, başlığını giriyorsunuz			
            mesaj.Body = Body;  // Göndereceğiniz mailin içeriğini girin, IsBodyHtml = true yaptıysanız html etiketleri ok
            mesaj.IsBodyHtml = true;// Mail içeriğinde html kullanılacaksa true, mail içereğinde htmli engellemek için false giriniz.
            mesaj.BodyEncoding = System.Text.Encoding.UTF8;//bu da olabilir : mesaj.BodyEncoding = UTF8Encoding.UTF8;
            mesaj.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            string host = "smtp.gmail.com";
            string smtpUser = "dekasmonitoringpilot@gmail.com";
            string smtpPassword = "Dekas1996";

            SmtpClient smtp = new SmtpClient(host, 587);// Genelde mail.domain.com şeklinde olan smtp mail sunucu adresinizi girmelisiniz.
            smtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);  // mail adresinizi ve şifrenizi giriyorsunuz 		//	smtp.UseDefaultCredentials = false;
            smtp.Timeout = 10000;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.EnableSsl = true; // Sunucunuz mail göndermek için ssl gerektiriyorsa true, gerektirmiyorsa false girin. 
            smtp.Send(mesaj);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }




    protected void Bclosedok_Click(object sender, EventArgs e)
    {
        this.ModalPopupMessageok.Hide();
        this.ModalPopupMessageok1.Hide();
    }

    protected void LBjsrprint_Click(object sender, EventArgs e)
    {
        // Document document = new Document(PageSize.A4.Rotate());
        NameTextSharp.Document doc = new NameTextSharp.Document(iTextSharp.text.PageSize.A4, 40, 40, 50, 40);
        MemoryStream outstream = new MemoryStream();
        PdfWriter wri = PdfWriter.GetInstance(doc, outstream);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);//Sayfamızın cache'lenmesini kapatıyoruz

        doc.Open();
        doc = BindingData(doc);
        doc.Close();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", String.Format("attachment;filename=" + "is durumu raporu" + " (" + TBSatir1.Text + ").pdf"));
        Response.BinaryWrite(outstream.ToArray());


    }

    private NameTextSharp.Document BindingData(NameTextSharp.Document doc)
    {
        // Türkçe Karakterlerini tanımlıyoruz 
        BaseFont STF_Helvetica_Turkish = BaseFont.CreateFont("Helvetica", "CP1254", BaseFont.NOT_EMBEDDED);
        Font fontNormal = new Font(STF_Helvetica_Turkish, 10, Font.NORMAL);
        Font fontNormalboldaltbaslik = new Font(STF_Helvetica_Turkish,12,Font.UNDERLINE);
        Font fontNormalaltciz = new Font(STF_Helvetica_Turkish, 10, Font.UNDERLINE);
        Font fontNormalitalik = new Font(STF_Helvetica_Turkish, 10, Font.ITALIC);
        Font fontNormalbold = new Font(STF_Helvetica_Turkish, 10, Font.BOLD);



        doc.Add(new Paragraph("\n")); // Alt satıra atar BR gibi 

        PdfPTable tablemain = new PdfPTable(new float[] { 440, 20, 240 });
        tablemain.TotalWidth = 440f;
        tablemain.LockedWidth = true;
        tablemain.HorizontalAlignment = 0;
        tablemain.DefaultCell.Padding = 1f;
        tablemain.DefaultCell.BorderWidth = 0;
        tablemain.DefaultCell.MinimumHeight = 20f;

        //ALT 3satır tablo
        PdfPTable tablefooter = new PdfPTable(new float[] { 440, 20, 240 });
        tablefooter.TotalWidth = 440f;
        tablefooter.LockedWidth = true;
        tablefooter.HorizontalAlignment = 0;
        tablefooter.DefaultCell.Padding = 1f;
        tablefooter.DefaultCell.BorderWidth = 0;
        tablefooter.DefaultCell.MinimumHeight = 20f;

        //boşluk satırı tanımla
        PdfPCell bosluk = new PdfPCell(new NameTextSharp.Phrase("", fontNormal));
        bosluk.Colspan = 3;
        bosluk.BorderWidth = 0;
        bosluk.MinimumHeight = 10f;

        //tarih satır
        PdfPCell baslik = new PdfPCell(new NameTextSharp.Phrase(TBSatir1.Text , fontNormal));
        baslik.Colspan = 3;
        baslik.BorderWidth = 0;
        baslik.HorizontalAlignment = Element.ALIGN_RIGHT ; // 0-sola, 1-ortala, 2-saga yasla
        //baslik.Rotation = 90; dik yazı için
        tablemain.AddCell(baslik);

        tablemain.AddCell(bosluk);

        //darıca yarımca
        PdfPCell baslik2 = new PdfPCell(new NameTextSharp.Phrase("DARICA / YARIMCA", fontNormalitalik));
        baslik2.Colspan = 3;
        baslik2.BorderWidth = 0;
        baslik2.HorizontalAlignment = 2; 
        tablemain.AddCell(baslik2);

        PdfPCell baslik3 = new PdfPCell(new NameTextSharp.Phrase("İŞ DURUMU RAPORU", fontNormalboldaltbaslik));
        baslik3.Colspan = 3;
        baslik3.BorderWidth = 0;
        baslik3.HorizontalAlignment = 1; 
        tablemain.AddCell(baslik3);

        PdfPCell baslik4 = new PdfPCell(new NameTextSharp.Phrase(Convert.ToDateTime(TBSatir1.Text).AddDays(-1).ToShortDateString() + " / 00:01  -  " + Convert.ToDateTime(TBSatir1.Text).AddDays(-1).ToShortDateString() + " / 23:59  itibari ile;", fontNormalbold));
        baslik4.Colspan = 3;
        baslik4.BorderWidth = 0;
        baslik4.HorizontalAlignment = 1;
        tablemain.AddCell(baslik4);

        tablemain.AddCell(bosluk);

        tablemain.AddCell(new NameTextSharp.Phrase("Toplam İş", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(":", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(TBsatir5.Text, fontNormal));

        PdfPCell baslik5 = new PdfPCell(new NameTextSharp.Phrase(Labelsatir6.Text, fontNormalbold));
        baslik5.Colspan = 3;
        baslik5.BorderWidth = 0;
        baslik5.HorizontalAlignment = 1;
        tablemain.AddCell(baslik5);

        tablemain.AddCell(bosluk);

        tablemain.AddCell(new NameTextSharp.Phrase("Yanaşık gemilerin sayısı", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(":", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(TBsatir7.Text, fontNormal));

        tablemain.AddCell(new NameTextSharp.Phrase("Eskihisar ve Yalova demirdeki gemi sayısı", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(":", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(TBsatir8.Text, fontNormal));

        tablemain.AddCell(new NameTextSharp.Phrase("Hereke demirdeki gemi sayısı", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(":", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(TBsatir9.Text, fontNormal));

        tablemain.AddCell(new NameTextSharp.Phrase("Yarımca ve İzmit demirdeki gemilerin sayısı", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(":", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(TBsatir10.Text, fontNormal));

        tablemain.AddCell(new NameTextSharp.Phrase("Beklenen gemi sayısı", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(":", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(TBsatir11.Text, fontNormal));

        tablemain.AddCell(new NameTextSharp.Phrase("Son talep numarası", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(":", fontNormal));
        tablemain.AddCell(new NameTextSharp.Phrase(TBsatir12a.Text + "   /   " + TBsatir12b.Text + "   /   " + TBsatir12c.Text, fontNormal));



        tablefooter.AddCell(new NameTextSharp.Phrase("Dekaş", fontNormal));
        tablefooter.AddCell(new NameTextSharp.Phrase(":", fontNormal));
        tablefooter.AddCell(new NameTextSharp.Phrase(TBsatir14.Text, fontNormal));

        tablefooter.AddCell(new NameTextSharp.Phrase("Derince", fontNormal));
        tablefooter.AddCell(new NameTextSharp.Phrase(":", fontNormal));
        tablefooter.AddCell(new NameTextSharp.Phrase(TBsatir15.Text, fontNormal));

        tablefooter.AddCell(new NameTextSharp.Phrase("Çalışan Pilot Sayısı", fontNormal));
        tablefooter.AddCell(new NameTextSharp.Phrase(":", fontNormal));
        tablefooter.AddCell(new NameTextSharp.Phrase(TBsatir16.Text, fontNormal));


        tablemain.AddCell(bosluk);



        PdfPTable tablezemin = new PdfPTable(1);
        tablezemin.TotalWidth = 450f;
        tablezemin.LockedWidth = true;
        tablezemin.HorizontalAlignment = 0;
        tablezemin.DefaultCell.BorderWidth = 1;
        tablezemin.DefaultCell.BorderColor = iTextSharp.text.Color.BLACK;
        tablezemin.AddCell(tablemain);
        tablezemin.AddCell(tablefooter);
        doc.Add(tablezemin);
        return doc;

    }


    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    base.VerifyRenderingInServerForm(control);
    //}


}
