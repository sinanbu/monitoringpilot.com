using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Globalization;

public partial class rhreportspob : System.Web.UI.Page
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

            SqlCommand seskapadi = new SqlCommand("SP_seskapadi", baglanti);
            seskapadi.CommandType = CommandType.StoredProcedure;
            seskapadi.Parameters.AddWithValue("@kapno", Session["kapno"].ToString());
            seskapadi.Parameters.Add("@kapadisonuc", SqlDbType.NVarChar, 30);
            seskapadi.Parameters["@kapadisonuc"].Direction = ParameterDirection.Output;
            seskapadi.ExecuteNonQuery();
            string seskapadis = seskapadi.Parameters["@kapadisonuc"].Value.ToString().Trim();
            seskapadi.Dispose();

            SqlCommand seskapsoyadi = new SqlCommand("SP_seskapsoyadi", baglanti);
            seskapsoyadi.CommandType = CommandType.StoredProcedure;
            seskapsoyadi.Parameters.AddWithValue("@kapno", Session["kapno"].ToString());
            seskapsoyadi.Parameters.Add("@kapsoyadisonuc", SqlDbType.NVarChar, 30);
            seskapsoyadi.Parameters["@kapsoyadisonuc"].Direction = ParameterDirection.Output;
            seskapsoyadi.ExecuteNonQuery();
            string seskapsoyadis = seskapsoyadi.Parameters["@kapsoyadisonuc"].Value.ToString().Trim();
            seskapsoyadi.Dispose();

            LBonline.Text = seskapadis + " " + seskapsoyadis; // +Session.Timeout.ToString();

            if(TextBox7.Text=="") { TextBox7.Text = TarihYaziYapDMY(DateTime.Now); }

            if (!IsPostBack)
            {  
                Lwoidgunluk2.Text = "";
                databagla();
            }
            
        }


        cmdlogofbak.Dispose();

        baglanti.Close();

    }


    private void databagla()
    {

            //DataTable DTkaplar = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi  from pilotlar where  kapsirano<1000 order by kapadisoyadi asc");
            //DDLPilots.Items.Clear();
            //DDLPilots.DataTextField = "kapadisoyadi";
            //DDLPilots.DataValueField = "kapno";
            //DDLPilots.DataSource = DTkaplar;
            //DDLPilots.DataBind();
            //DDLPilots.Items.Insert(0, new ListItem("Select", String.Empty));
            //DDLPilots.SelectedIndex = 0;
            //int kapnoal = Convert.ToInt32(DDLPilots.SelectedItem.Value.ToString());

            //DTYarimca Canlı Ekran 
            SqlConnection baglanti = AnaKlas.baglan();

            SqlCommand cmdDTRepList = new SqlCommand("SP_RHrepList_istgel", baglanti);
            cmdDTRepList.CommandType = CommandType.StoredProcedure;
            cmdDTRepList.Parameters.AddWithValue("@istenendate", TextBox7.Text);
            SqlDataAdapter adaptery = new SqlDataAdapter();
            adaptery.SelectCommand = cmdDTRepList;
            DataSet rhrep = new DataSet();
            adaptery.Fill(rhrep, "pilotlar");
            DLDarica.DataSource = rhrep;
            DLDarica.DataBind();

            cmdDTRepList.Dispose();
            baglanti.Close();
            Lwoidgunluk2.Text = "";
            
            baslik.Text = "KILAVUZ KAPTANLARIN " + TextBox7.Text + " TARİHLİ ÇALIŞMA VE İSTİRAHAT SAATLERİ";
            Label49x.Text="0";
            Label50x.Text="0";
            Label51x.Text="0";


    }


    protected void DLDarica_ItemDataBound(object sender, DataListItemEventArgs e)
    {
     
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {


            //pilot ismini bul
            Label LBpilotismikop = (Label)e.Item.FindControl("LBpilotismi");
            string kapno = LBpilotismikop.Text;

         
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
            cmdPilotismial.CommandType = CommandType.StoredProcedure;
            cmdPilotismial.Parameters.AddWithValue("@secilikapno", kapno);
            string kapadisoyadi = "";
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


            Label Label1kop = (Label)e.Item.FindControl("Label1");
            Label Label2kop = (Label)e.Item.FindControl("Label2");
            Label Label3kop = (Label)e.Item.FindControl("Label3");
            Label Label4kop = (Label)e.Item.FindControl("Label4");
            Label Label5kop = (Label)e.Item.FindControl("Label5");
            Label Label6kop = (Label)e.Item.FindControl("Label6");
            Label Label7kop = (Label)e.Item.FindControl("Label7");
            Label Label8kop = (Label)e.Item.FindControl("Label8");
            Label Label9kop = (Label)e.Item.FindControl("Label9");
            Label Label10kop = (Label)e.Item.FindControl("Label10");

            Label Label11kop = (Label)e.Item.FindControl("Label11");
            Label Label12kop = (Label)e.Item.FindControl("Label12");
            Label Label13kop = (Label)e.Item.FindControl("Label13");
            Label Label14kop = (Label)e.Item.FindControl("Label14");
            Label Label15kop = (Label)e.Item.FindControl("Label15");
            Label Label16kop = (Label)e.Item.FindControl("Label16");
            Label Label17kop = (Label)e.Item.FindControl("Label17");
            Label Label18kop = (Label)e.Item.FindControl("Label18");
            Label Label19kop = (Label)e.Item.FindControl("Label19");
            Label Label20kop = (Label)e.Item.FindControl("Label20");

            Label Label21kop = (Label)e.Item.FindControl("Label21");
            Label Label22kop = (Label)e.Item.FindControl("Label22");
            Label Label23kop = (Label)e.Item.FindControl("Label23");
            Label Label24kop = (Label)e.Item.FindControl("Label24");
            Label Label25kop = (Label)e.Item.FindControl("Label25");
            Label Label26kop = (Label)e.Item.FindControl("Label26");
            Label Label27kop = (Label)e.Item.FindControl("Label27");
            Label Label28kop = (Label)e.Item.FindControl("Label28");
            Label Label29kop = (Label)e.Item.FindControl("Label29");
            Label Label30kop = (Label)e.Item.FindControl("Label30");

            Label Label31kop = (Label)e.Item.FindControl("Label31");
            Label Label32kop = (Label)e.Item.FindControl("Label32");
            Label Label33kop = (Label)e.Item.FindControl("Label33");
            Label Label34kop = (Label)e.Item.FindControl("Label34");
            Label Label35kop = (Label)e.Item.FindControl("Label35");
            Label Label36kop = (Label)e.Item.FindControl("Label36");
            Label Label37kop = (Label)e.Item.FindControl("Label37");
            Label Label38kop = (Label)e.Item.FindControl("Label38");
            Label Label39kop = (Label)e.Item.FindControl("Label39");
            Label Label40kop = (Label)e.Item.FindControl("Label40");

            Label Label41kop = (Label)e.Item.FindControl("Label41");
            Label Label42kop = (Label)e.Item.FindControl("Label42");
            Label Label43kop = (Label)e.Item.FindControl("Label43");
            Label Label44kop = (Label)e.Item.FindControl("Label44");
            Label Label45kop = (Label)e.Item.FindControl("Label45");
            Label Label46kop = (Label)e.Item.FindControl("Label46");
            Label Label47kop = (Label)e.Item.FindControl("Label47");
            Label Label48kop = (Label)e.Item.FindControl("Label48");

            Label Label49kop = (Label)e.Item.FindControl("Label49");
            Label Label50kop = (Label)e.Item.FindControl("Label50");
            Label Label51kop = (Label)e.Item.FindControl("Label51");

            Label1kop.Text = "";
            Label2kop.Text = "";
            Label3kop.Text = "";
            Label4kop.Text = "";
            Label5kop.Text = "";
            Label6kop.Text = "";
            Label7kop.Text = "";
            Label8kop.Text = "";
            Label9kop.Text = "";
            Label10kop.Text = "";
            Label11kop.Text = "";
            Label12kop.Text = "";
            Label13kop.Text = "";
            Label14kop.Text = "";
            Label15kop.Text = "";
            Label16kop.Text = "";
            Label17kop.Text = "";
            Label18kop.Text = "";
            Label19kop.Text = "";
            Label20kop.Text = "";
            Label21kop.Text = "";
            Label22kop.Text = "";
            Label23kop.Text = "";
            Label24kop.Text = "";
            Label25kop.Text = "";
            Label26kop.Text = "";
            Label27kop.Text = "";
            Label28kop.Text = "";
            Label29kop.Text = "";
            Label30kop.Text = "";
            Label31kop.Text = "";
            Label32kop.Text = "";
            Label33kop.Text = "";
            Label34kop.Text = "";
            Label35kop.Text = "";
            Label36kop.Text = "";
            Label37kop.Text = "";
            Label38kop.Text = "";
            Label39kop.Text = "";
            Label40kop.Text = "";
            Label41kop.Text = "";
            Label42kop.Text = "";
            Label43kop.Text = "";
            Label44kop.Text = "";
            Label45kop.Text = "";
            Label46kop.Text = "";
            Label47kop.Text = "";
            Label48kop.Text = "";
            Label49kop.Text = "";
            Label50kop.Text = "";
            Label51kop.Text = "";



            string istcik = "";
            string pob = "";
            string poff = "";
            string istgel = "";

            int issay = 0;
            double topcalsur = 0;
            double topdinsur = 0;
            double topcalsurpart = 0;

            SqlCommand cmdDTRepwork = new SqlCommand("SP_RHrepwork", baglanti);
            cmdDTRepwork.CommandType = CommandType.StoredProcedure;
            cmdDTRepwork.Parameters.AddWithValue("@istenendate", TextBox7.Text);
            cmdDTRepwork.Parameters.AddWithValue("@degismecikapno", kapno);
            SqlDataReader kaprdr = cmdDTRepwork.ExecuteReader();

            if (kaprdr.HasRows)
            {
                while (kaprdr.Read())
                {
                    issay++;

                    istcik = kaprdr["istasyoncikis"].ToString();
                    pob = kaprdr["pob"].ToString();
                    poff = kaprdr["poff"].ToString();
                    istgel = kaprdr["istasyongelis"].ToString();

                    if (istcik.Substring(0,10) != TextBox7.Text && pob.Substring(0, 10) == TextBox7.Text && poff.Substring(0, 10) == TextBox7.Text && istgel.Substring(0, 10) == TextBox7.Text)
                    { istcik = TextBox7.Text + " 00:01"; }

                    if (istcik.Substring(0, 10) != TextBox7.Text && pob.Substring(0, 10) != TextBox7.Text && poff.Substring(0, 10) == TextBox7.Text && istgel.Substring(0, 10) == TextBox7.Text)
                    { istcik = TextBox7.Text + " 00:01";
                      pob = TextBox7.Text + " 00:01";
                    }

                    if (istcik.Substring(0, 10) != TextBox7.Text && pob.Substring(0, 10) != TextBox7.Text && poff.Substring(0, 10) != TextBox7.Text && istgel.Substring(0, 10) == TextBox7.Text)
                    {
                        istcik = TextBox7.Text + " 00:01";
                        pob = TextBox7.Text + " 00:01";
                        poff = TextBox7.Text + " 00:01";
                    }
                    //---------------
                    if (istcik.Substring(0, 10) == TextBox7.Text && pob.Substring(0, 10) == TextBox7.Text && poff.Substring(0, 10) == TextBox7.Text && istgel.Substring(0, 10) != TextBox7.Text)
                    {
                        istgel = TextBox7.Text + " 23:59";
                    }


                    if (istcik.Substring(0, 10) == TextBox7.Text && pob.Substring(0, 10) == TextBox7.Text && poff.Substring(0, 10) != TextBox7.Text && istgel.Substring(0, 10) != TextBox7.Text)
                    {
                        poff = TextBox7.Text + " 23:59";
                        istgel = TextBox7.Text + " 23:59";
                    }

                    if (istcik.Substring(0, 10) == TextBox7.Text && pob.Substring(0, 10) != TextBox7.Text && poff.Substring(0, 10) != TextBox7.Text && istgel.Substring(0, 10) != TextBox7.Text)
                    {
                        pob = TextBox7.Text + " 23:59";
                        poff = TextBox7.Text + " 23:59";
                        istgel = TextBox7.Text + " 23:59";
                    }



                    SqlConnection baglanti2 = AnaKlas.baglan2();
                    istcik = istcik.Substring(11, 5);
                    pob = pob.Substring(11, 5);
                    poff = poff.Substring(11, 5);
                    istgel = istgel.Substring(11, 5);

                    // iş baş ve bitiş ve arası dolgu
                    double i = 1;

                    double istcikdec = 0;
                    double pobdec = 0;
                    double poffdec = 0;
                    double istgeldec = 0;

                    string[] parca1 = istcik.Split(':');
                    string[] parca2 = pob.Split(':');
                    string[] parca3 = poff.Split(':');
                    string[] parca4 = istgel.Split(':');

                    istcikdec = Convert.ToDouble(parca1[0]) + Convert.ToDouble(parca1[1]) / 60 ;
                    pobdec = Convert.ToDouble(parca2[0]) + Convert.ToDouble(parca2[1]) / 60;
                    poffdec = Convert.ToDouble(parca3[0]) + Convert.ToDouble(parca3[1]) / 60;
                    istgeldec = Convert.ToDouble(parca4[0]) + Convert.ToDouble(parca4[1]) / 60;


                    while (i < 49)
                    {


                        if ((pobdec < i / 2 && poffdec + 0.5 >= i / 2))
                        {
                            if (i == 1) { Label1kop.Text = "x"; }
                            else if (i == 2) { Label2kop.Text = "x"; }
                            else if (i == 3) { Label3kop.Text = "x"; }
                            else if (i == 4) { Label4kop.Text = "x"; }
                            else if (i == 5) { Label5kop.Text = "x"; }
                            else if (i == 6) { Label6kop.Text = "x"; }
                            else if (i == 7) { Label7kop.Text = "x"; }
                            else if (i == 8) { Label8kop.Text = "x"; }
                            else if (i == 9) { Label9kop.Text = "x"; }
                            else if (i == 10) { Label10kop.Text = "x"; }

                            else if (i == 11) { Label11kop.Text = "x"; }
                            else if (i == 12) { Label12kop.Text = "x"; }
                            else if (i == 13) { Label13kop.Text = "x"; }
                            else if (i == 14) { Label14kop.Text = "x"; }
                            else if (i == 15) { Label15kop.Text = "x"; }
                            else if (i == 16) { Label16kop.Text = "x"; }
                            else if (i == 17) { Label17kop.Text = "x"; }
                            else if (i == 18) { Label18kop.Text = "x"; }
                            else if (i == 19) { Label19kop.Text = "x"; }
                            else if (i == 20) { Label20kop.Text = "x"; }

                            else if (i == 21) { Label21kop.Text = "x"; }
                            else if (i == 22) { Label22kop.Text = "x"; }
                            else if (i == 23) { Label23kop.Text = "x"; }
                            else if (i == 24) { Label24kop.Text = "x"; }
                            else if (i == 25) { Label25kop.Text = "x"; }
                            else if (i == 26) { Label26kop.Text = "x"; }
                            else if (i == 27) { Label27kop.Text = "x"; }
                            else if (i == 28) { Label28kop.Text = "x"; }
                            else if (i == 29) { Label29kop.Text = "x"; }
                            else if (i == 30) { Label30kop.Text = "x"; }

                            else if (i == 31) { Label31kop.Text = "x"; }
                            else if (i == 32) { Label32kop.Text = "x"; }
                            else if (i == 33) { Label33kop.Text = "x"; }
                            else if (i == 34) { Label34kop.Text = "x"; }
                            else if (i == 35) { Label35kop.Text = "x"; }
                            else if (i == 36) { Label36kop.Text = "x"; }
                            else if (i == 37) { Label37kop.Text = "x"; }
                            else if (i == 38) { Label38kop.Text = "x"; }
                            else if (i == 39) { Label39kop.Text = "x"; }
                            else if (i == 40) { Label40kop.Text = "x"; }

                            else if (i == 41) { Label41kop.Text = "x"; }
                            else if (i == 42) { Label42kop.Text = "x"; }
                            else if (i == 43) { Label43kop.Text = "x"; }
                            else if (i == 44) { Label44kop.Text = "x"; }
                            else if (i == 45) { Label45kop.Text = "x"; }
                            else if (i == 46) { Label46kop.Text = "x"; }
                            else if (i == 47) { Label47kop.Text = "x"; }
                            else if (i == 48) { Label48kop.Text = "x"; }

                        }

                        i = i+1;
                    }

                    //zemin boyama
                    HtmlTableCell td1k = (HtmlTableCell)e.Item.FindControl("td1");
                    HtmlTableCell td2k = (HtmlTableCell)e.Item.FindControl("td2");
                    HtmlTableCell td3k = (HtmlTableCell)e.Item.FindControl("td3");
                    HtmlTableCell td4k = (HtmlTableCell)e.Item.FindControl("td4");
                    HtmlTableCell td5k = (HtmlTableCell)e.Item.FindControl("td5");
                    HtmlTableCell td6k = (HtmlTableCell)e.Item.FindControl("td6");
                    HtmlTableCell td7k = (HtmlTableCell)e.Item.FindControl("td7");
                    HtmlTableCell td8k = (HtmlTableCell)e.Item.FindControl("td8");
                    HtmlTableCell td9k = (HtmlTableCell)e.Item.FindControl("td9");
                    HtmlTableCell td10k = (HtmlTableCell)e.Item.FindControl("td10");

                    HtmlTableCell td11k = (HtmlTableCell)e.Item.FindControl("td11");
                    HtmlTableCell td12k = (HtmlTableCell)e.Item.FindControl("td12");
                    HtmlTableCell td13k = (HtmlTableCell)e.Item.FindControl("td13");
                    HtmlTableCell td14k = (HtmlTableCell)e.Item.FindControl("td14");
                    HtmlTableCell td15k = (HtmlTableCell)e.Item.FindControl("td15");
                    HtmlTableCell td16k = (HtmlTableCell)e.Item.FindControl("td16");
                    HtmlTableCell td17k = (HtmlTableCell)e.Item.FindControl("td17");
                    HtmlTableCell td18k = (HtmlTableCell)e.Item.FindControl("td18");
                    HtmlTableCell td19k = (HtmlTableCell)e.Item.FindControl("td19");
                    HtmlTableCell td20k = (HtmlTableCell)e.Item.FindControl("td20");

                    HtmlTableCell td21k = (HtmlTableCell)e.Item.FindControl("td21");
                    HtmlTableCell td22k = (HtmlTableCell)e.Item.FindControl("td22");
                    HtmlTableCell td23k = (HtmlTableCell)e.Item.FindControl("td23");
                    HtmlTableCell td24k = (HtmlTableCell)e.Item.FindControl("td24");
                    HtmlTableCell td25k = (HtmlTableCell)e.Item.FindControl("td25");
                    HtmlTableCell td26k = (HtmlTableCell)e.Item.FindControl("td26");
                    HtmlTableCell td27k = (HtmlTableCell)e.Item.FindControl("td27");
                    HtmlTableCell td28k = (HtmlTableCell)e.Item.FindControl("td28");
                    HtmlTableCell td29k = (HtmlTableCell)e.Item.FindControl("td29");
                    HtmlTableCell td30k = (HtmlTableCell)e.Item.FindControl("td30");

                    HtmlTableCell td31k = (HtmlTableCell)e.Item.FindControl("td31");
                    HtmlTableCell td32k = (HtmlTableCell)e.Item.FindControl("td32");
                    HtmlTableCell td33k = (HtmlTableCell)e.Item.FindControl("td33");
                    HtmlTableCell td34k = (HtmlTableCell)e.Item.FindControl("td34");
                    HtmlTableCell td35k = (HtmlTableCell)e.Item.FindControl("td35");
                    HtmlTableCell td36k = (HtmlTableCell)e.Item.FindControl("td36");
                    HtmlTableCell td37k = (HtmlTableCell)e.Item.FindControl("td37");
                    HtmlTableCell td38k = (HtmlTableCell)e.Item.FindControl("td38");
                    HtmlTableCell td39k = (HtmlTableCell)e.Item.FindControl("td39");
                    HtmlTableCell td40k = (HtmlTableCell)e.Item.FindControl("td40");

                    HtmlTableCell td41k = (HtmlTableCell)e.Item.FindControl("td41");
                    HtmlTableCell td42k = (HtmlTableCell)e.Item.FindControl("td42");
                    HtmlTableCell td43k = (HtmlTableCell)e.Item.FindControl("td43");
                    HtmlTableCell td44k = (HtmlTableCell)e.Item.FindControl("td44");
                    HtmlTableCell td45k = (HtmlTableCell)e.Item.FindControl("td45");
                    HtmlTableCell td46k = (HtmlTableCell)e.Item.FindControl("td46");
                    HtmlTableCell td47k = (HtmlTableCell)e.Item.FindControl("td47");
                    HtmlTableCell td48k = (HtmlTableCell)e.Item.FindControl("td48");

                    if (Label1kop.Text == "x") { td1k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label2kop.Text == "x") { td2k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label3kop.Text == "x") { td3k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label4kop.Text == "x") { td4k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label5kop.Text == "x") { td5k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label6kop.Text == "x") { td6k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label7kop.Text == "x") { td7k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label8kop.Text == "x") { td8k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label9kop.Text == "x") { td9k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label10kop.Text == "x") { td10k.Attributes.Add("style", "background-color:#ee1111"); }

                    if (Label11kop.Text == "x") { td11k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label12kop.Text == "x") { td12k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label13kop.Text == "x") { td13k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label14kop.Text == "x") { td14k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label15kop.Text == "x") { td15k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label16kop.Text == "x") { td16k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label17kop.Text == "x") { td17k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label18kop.Text == "x") { td18k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label19kop.Text == "x") { td19k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label20kop.Text == "x") { td20k.Attributes.Add("style", "background-color:#ee1111"); }

                    if (Label21kop.Text == "x") { td21k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label22kop.Text == "x") { td22k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label23kop.Text == "x") { td23k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label24kop.Text == "x") { td24k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label25kop.Text == "x") { td25k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label26kop.Text == "x") { td26k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label27kop.Text == "x") { td27k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label28kop.Text == "x") { td28k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label29kop.Text == "x") { td29k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label30kop.Text == "x") { td30k.Attributes.Add("style", "background-color:#ee1111"); }

                    if (Label31kop.Text == "x") { td31k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label32kop.Text == "x") { td32k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label33kop.Text == "x") { td33k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label34kop.Text == "x") { td34k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label35kop.Text == "x") { td35k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label36kop.Text == "x") { td36k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label37kop.Text == "x") { td37k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label38kop.Text == "x") { td38k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label39kop.Text == "x") { td39k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label40kop.Text == "x") { td40k.Attributes.Add("style", "background-color:#ee1111"); }

                    if (Label41kop.Text == "x") { td41k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label42kop.Text == "x") { td42k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label43kop.Text == "x") { td43k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label44kop.Text == "x") { td44k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label45kop.Text == "x") { td45k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label46kop.Text == "x") { td46k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label47kop.Text == "x") { td47k.Attributes.Add("style", "background-color:#ee1111"); }
                    if (Label48kop.Text == "x") { td48k.Attributes.Add("style", "background-color:#ee1111"); }

                    // toplamlar son 3 sutun

                    //TimeSpan issuresi = new TimeSpan();
                    topcalsurpart = poffdec - pobdec;
                    topcalsur = topcalsur + topcalsurpart;
                    topdinsur = 24 - topcalsur;

                    baglanti2.Close();
                }
            }

            if(topdinsur==0) { topdinsur = 24; }

            Label49kop.Text = issay.ToString();
            Label50kop.Text = Math.Round(topcalsur, 2).ToString();
            Label51kop.Text = Math.Round(topdinsur, 2).ToString();

            //ful toplamlar için 

            Label50x.Text = (Convert.ToInt32(Label50x.Text) + issay).ToString();
            Label51x.Text = Math.Round(Convert.ToDouble(Label51x.Text) + topcalsur, 2).ToString();

           
            kaprdr.Close();
            cmdDTRepwork.Dispose();
            
            baglanti.Close();

            LBpilotismikop.Text = kapadisoyadi;
        }

        Label49x.Text = DLDarica.Items.Count.ToString();

        if (e.Item.ItemType == ListItemType.Footer)
        {
            // dip full toplamlar

            Label Label52kop = (Label)e.Item.FindControl("Label52");//toplam çalışma süresi
            Label Label53kop = (Label)e.Item.FindControl("Label53");//toplam dinlenme süresi
            Label Label54kop = (Label)e.Item.FindControl("Label54");//Kılavuz Kaptan Sayısı
            Label Label55kop = (Label)e.Item.FindControl("Label55");//Ortalama Pilot Başına Manevra
            Label Label56kop = (Label)e.Item.FindControl("Label56");//Ort.Pilot Başına Çalışılan Saat
            Label Label57kop = (Label)e.Item.FindControl("Label57");//Ort.Pilot Başına Dinlenilen Saat

            // Label49x.Text; // pilot sayısı
            // Label50x.Text; // toplam iş sayısı
            // Label51x.Text; // toplam çalışma süresi


            Label52kop.Text = Math.Round(Convert.ToDouble(Label51x.Text),1).ToString(); 
            Label53kop.Text = Math.Round((Convert.ToDouble(Label49x.Text)*24 - Convert.ToDouble(Label51x.Text)),1).ToString(); 
            Label54kop.Text = Label49x.Text;
            if(Label49x.Text!="0") { 
            Label55kop.Text = Math.Round((Convert.ToDouble(Label50x.Text) / Convert.ToDouble(Label49x.Text)),2).ToString();
            Label56kop.Text = Math.Round((Convert.ToDouble(Label51x.Text) / Convert.ToDouble(Label49x.Text)),2).ToString();
            Label57kop.Text = Math.Round((Convert.ToDouble(Label53kop.Text) / Convert.ToDouble(Label49x.Text)),2).ToString();
            }
        }
          

    }


    public DateTime TarihSaatYapDMYhm(string Tarihsaaatcumlesi)
    {
        IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
        DateTime Tarihsaatok = DateTime.Parse(Tarihsaaatcumlesi, culture, System.Globalization.DateTimeStyles.AssumeLocal);
        // result = DateTime.TryParseExact(Tarihsaaatcumlesi, dtFormats, new CultureInfo("tr-TR"), DateTimeStyles.None, out dt);
        //DateTime Tarihsaatok = DateTime.ParseExact(Tarihsaaatcumlesi, "DD.mm.yyyy HH:mm", null);
        return Tarihsaatok;
    }
    public bool IsDate2(string tarihyazi)
    {
        DateTime Temp;
        IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
        if (DateTime.TryParse(tarihyazi, culture, System.Globalization.DateTimeStyles.AssumeLocal, out Temp) == true)
            return true;
        else
            return false;
    }

    public bool KayitTarihCek(string kayittarihkontrol)
    {
        string eta = kayittarihkontrol;

        if (eta == "" || eta == null || eta == "__.__.____ __:__")
        {
            return false;
        }
        else if (IsDate2(eta) != true)
        {
            return false;
        }
        else if (Convert.ToDateTime(eta) < DateTime.Now.AddMinutes(-46))
        {
            return false;
        }
        else if (Convert.ToDateTime(eta) > DateTime.Now.AddMinutes(16))
        {
            return false;
        }


        else
        {
            return true;
        }
    }


    protected void LBworkrest_Click(object sender, EventArgs e)
    {
        string gunal = TextBox7.Text.ToString().Trim();
        if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
        {
            Lwoidgunluk2.Text = "Please Enter a Valid Date.";
            TextBox7.Text = "";
        }
        else
        {

            databagla();

        }
    }

    protected void LBgeri_Click(object sender, EventArgs e)
    {
        string gunal = TextBox7.Text.ToString().Trim();
        if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
        {
            Lwoidgunluk2.Text = "Please Enter a Valid Date.";
            TextBox7.Text = "";
        }
        else
        {
            Lwoidgunluk2.Text = "";
            gunal = TarihYaziYapDMY(Convert.ToDateTime(gunal).AddDays(-1));
            TextBox7.Text = gunal;
            databagla();
        }
    }
    protected void LBileri_Click(object sender, EventArgs e)
    {
        string gunal = TextBox7.Text.ToString().Trim();
        if (gunal.Length == 0 || Altcizgisil(gunal).Length != 10 || IsDate2(gunal) != true)
        {
            Lwoidgunluk2.Text = "Please Enter a Valid Date.";
            TextBox7.Text = "";
        }
        else
        {
            Lwoidgunluk2.Text = "";
            gunal = TarihYaziYapDMY(Convert.ToDateTime(gunal).AddDays(+1));
            TextBox7.Text = gunal;
            databagla();
        }
    }


    protected void ButtonLiveScreen_Click(object sender, EventArgs e)
    {
        Response.Redirect("statisticsg.aspx");
    }
    protected void LBonlineoff_Click(object sender, EventArgs e)
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
            Response.Redirect("http://www.monitoringpilot.com");
        }
    }
    protected void LBonline_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonetim.aspx");
        }
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

    private string Altcizgisil(string Metin)
    {
        string deger = Metin;
        deger = deger.Replace("_", "");
        return deger;
    }



    protected void LBrange_Click(object sender, EventArgs e)
    {
        Response.Redirect("rhreports.aspx");
    }
}




