using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Globalization;

public partial class Main : System.Web.UI.Page
{

    Metodlar AnaKlas = new Metodlar();

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (Session["kapno"] == null)
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Convert.ToInt32(Session["kapno"]) < 0 || Convert.ToInt32(Session["kapno"]) > 999)
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "99")
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }

        if (Convert.ToInt32(Session["yetki"]) == 6)
        {
            Response.Redirect("stn.aspx");
        }

        else if (Convert.ToInt32(Session["yetki"]) < 1 || Convert.ToInt32(Session["yetki"]) > 2)
        {
            Response.Redirect("stations.aspx");
        }

        else  //if (cmdlogofbak.ExecuteScalar() != null)
        {
            if (!IsPostBack)
            {
                if (Session["yetki"].ToString() == "9") { LBonline.Enabled = true; }
                else { LBonline.Enabled = false; }


                if (Session["yetki"].ToString() == "1")
                {
                    Byarimcaships.Visible = false;
                }
                else if (Session["yetki"].ToString() == "2")
                {
                    Bdaricaships.Visible = false;
                    //Byalovaships.Visible = false;
                }

                LiteralYaz(baglanti);

                SqlCommand cmdlogonal1 = new SqlCommand("Select kapadi + ' ' + kapsoyadi as kapadisoyadi from onoffline where yetki='1' and time = 0.001 ", baglanti);
                SqlCommand cmdlogonal2 = new SqlCommand("Select kapadi + ' ' + kapsoyadi as kapadisoyadi from onoffline where yetki='2' and time = 0.001 ", baglanti);

                if (cmdlogonal1.ExecuteScalar() != null) { LBonlined.Text = cmdlogonal1.ExecuteScalar().ToString(); }
                if (cmdlogonal2.ExecuteScalar() != null) { LBonliney.Text = cmdlogonal2.ExecuteScalar().ToString(); }
                cmdlogonal1.Dispose();
                cmdlogonal2.Dispose();

                SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                cmdPilotismial.CommandType = CommandType.StoredProcedure;
                cmdPilotismial.Parameters.AddWithValue("@secilikapno", Convert.ToInt32(Session["kapno"]));
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

                LBonline.Text = kapadisoyadi; // +Session.Timeout.ToString();

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
                LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();
                DTloading(baglanti);
            }
        }

        baglanti.Close();


    }  //DURUMA GÖRE LABEL VE BUTON GÖRÜNTÜLEME

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        Lblsaat.Text = DateTime.Now.ToShortTimeString();
        LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();
    }

    private void LiteralYaz(SqlConnection baglanti)
    {
        Lblsaat.Text = DateTime.Now.ToShortTimeString();
        SqlCommand cmdvarbilvarid = new SqlCommand("SP_varbilgivaridoku", baglanti);
        cmdvarbilvarid.CommandType = CommandType.StoredProcedure;
        cmdvarbilvarid.Parameters.Add("@bilgivaridoku", SqlDbType.Char, 1);
        cmdvarbilvarid.Parameters["@bilgivaridoku"].Direction = ParameterDirection.Output;
        cmdvarbilvarid.ExecuteNonQuery();
        varbilvarid.Text = cmdvarbilvarid.Parameters["@bilgivaridoku"].Value.ToString().Trim();
        cmdvarbilvarid.Dispose();

        SqlCommand cmdvarbilvarno = new SqlCommand("SP_varbilgivarnooku", baglanti);
        cmdvarbilvarno.CommandType = CommandType.StoredProcedure;
        cmdvarbilvarno.Parameters.Add("@bilgivarnooku", SqlDbType.Char, 6);
        cmdvarbilvarno.Parameters["@bilgivarnooku"].Direction = ParameterDirection.Output;
        cmdvarbilvarno.ExecuteNonQuery();
        varbilvarno.Text = cmdvarbilvarno.Parameters["@bilgivarnooku"].Value.ToString().Trim();
        cmdvarbilvarno.Dispose();

        SqlCommand cmdvarbaslar = new SqlCommand("SP_Vardiyabaslangic", baglanti);
        cmdvarbaslar.CommandType = CommandType.StoredProcedure;
        cmdvarbaslar.Parameters.Add("@sonuc", SqlDbType.Char, 16);
        cmdvarbaslar.Parameters["@sonuc"].Direction = ParameterDirection.Output;
        cmdvarbaslar.ExecuteNonQuery();
        varbaslar.Text = cmdvarbaslar.Parameters["@sonuc"].Value.ToString().Trim();
        cmdvarbaslar.Dispose();

        SqlCommand cmdvarbiter = new SqlCommand("SP_Vardiyabitisi", baglanti);
        cmdvarbiter.CommandType = CommandType.StoredProcedure;
        cmdvarbiter.Parameters.Add("@sonuc", SqlDbType.Char, 16);
        cmdvarbiter.Parameters["@sonuc"].Direction = ParameterDirection.Output;
        cmdvarbiter.ExecuteNonQuery();
        varbiter.Text = cmdvarbiter.Parameters["@sonuc"].Value.ToString().Trim();
        cmdvarbiter.Dispose();
    }

    private void DTloading(SqlConnection baglanti)
    {
        Lblsaat.Text = DateTime.Now.ToShortTimeString();
        //DTDarica Canlı Ekran 
        SqlCommand cmdDTDaricaEkran = new SqlCommand("SP_DTDaricaCanliEkran", baglanti);
        cmdDTDaricaEkran.CommandType = CommandType.StoredProcedure;
        cmdDTDaricaEkran.Parameters.AddWithValue("@varbilvarid", varbilvarid.Text);
        cmdDTDaricaEkran.Parameters.AddWithValue("@varbilvarno", varbilvarno.Text);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdDTDaricaEkran;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "pilotlar");
        DLDarica.DataSource = ds;
        DLDarica.DataBind();

        //DTYarimca Canlı Ekran 
        SqlCommand cmdDTYarimcaEkran = new SqlCommand("SP_DTYarimcaCanliEkran", baglanti);
        cmdDTYarimcaEkran.CommandType = CommandType.StoredProcedure;
        cmdDTYarimcaEkran.Parameters.AddWithValue("@varbilvarid", varbilvarid.Text);
        cmdDTYarimcaEkran.Parameters.AddWithValue("@varbilvarno", varbilvarno.Text);
        SqlDataAdapter adaptery = new SqlDataAdapter();
        adaptery.SelectCommand = cmdDTYarimcaEkran;
        DataSet dsy = new DataSet();
        adaptery.Fill(dsy, "pilotlar");
        DLDaricay.DataSource = dsy;
        DLDaricay.DataBind();




        Litizinler.Text = "";
        Litizinler.Style.Add("color", "#111111");
        SqlCommand cmdizinalanlar = new SqlCommand("SP_Tumizinalanlar", baglanti);
        cmdizinalanlar.CommandType = CommandType.StoredProcedure;
        cmdizinalanlar.Parameters.AddWithValue("@varbilvarid", varbilvarid.Text);
        string kapadisoyadi = "";
        string degismeciadisoyadi = "";
        string gorevde = "";
        SqlDataReader dr = cmdizinalanlar.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                kapadisoyadi = dr["kapadisoyadi"].ToString();
                degismeciadisoyadi = dr["degismeciadisoyadi"].ToString();
                gorevde = dr["gorevde"].ToString();

                if (gorevde == "0")        // değişmeci gelmiş (izinde=1, gorevde=0 olur)
                {
                    //if (kapadisoyadi == degismeciadisoyadi)
                    { Litizinler.Text = Litizinler.Text + kapadisoyadi + " (" + degismeciadisoyadi + ") - "; }
                }
                if (gorevde == "1")// direkt izindedir (izinde=1, gorevde=1 olur)
                {
                    if (kapadisoyadi == degismeciadisoyadi)
                    { Litizinler.Text = Litizinler.Text + kapadisoyadi + " - "; }
                }

                if (gorevde == "2")// hasta olmuş      (izinde=1, gorevde=2 olur)
                {
                    if (kapadisoyadi == degismeciadisoyadi)
                    { Litizinler.Text = Litizinler.Text + kapadisoyadi + " (Off Duty) - "; }
                }

                if (gorevde == "3")// add pilot        (izinde=1, gorevde=3 olur)
                {
                    if (kapadisoyadi == degismeciadisoyadi)
                    { Litizinler.Text = Litizinler.Text + kapadisoyadi + " (AddedPilot) - "; }
                }


            }
        }
        dr.Close();
        cmdizinalanlar.Dispose();

        if (Litizinler.Text.Length > 3)
        {
            Litizinler.Text = Litizinler.Text.Substring(0, Litizinler.Text.Length - 2);
        }

        //duyuru
        SqlCommand duyurubak = new SqlCommand("SP_duyurubak", baglanti);
        duyurubak.CommandType = CommandType.StoredProcedure;
        duyurubak.Parameters.Add("@duyurusonuc", SqlDbType.NVarChar, 700);
        duyurubak.Parameters["@duyurusonuc"].Direction = ParameterDirection.Output;
        duyurubak.ExecuteNonQuery();
        LblDuyuru.Text = "Yeni Duyuru Yok";
        if (string.IsNullOrEmpty(duyurubak.Parameters["@duyurusonuc"].Value.ToString().Trim()) != true)
        { LblDuyuru.Text = duyurubak.Parameters["@duyurusonuc"].Value.ToString().Trim(); }
        duyurubak.Dispose();

        //duyuru daily
        string jnotdaily = "";
        SqlCommand cmdduydayoku = new SqlCommand("SP_duydailyoku", baglanti); //enson duyuru oku
        cmdduydayoku.CommandType = CommandType.StoredProcedure;
        cmdduydayoku.Parameters.AddWithValue("@aktif", "2");
        SqlDataReader limread = cmdduydayoku.ExecuteReader();

        string kayittarihi = "";
        string iptaltarihi = "";

        if (limread.HasRows)
        {
            while (limread.Read())
            {
                jnotdaily = limread["duyuru"].ToString();
                kayittarihi = limread["kayittarihi"].ToString();
                iptaltarihi = limread["iptaltarihi"].ToString();
            }
        }
        limread.Close();
        cmdduydayoku.Dispose();


        if (iptaltarihi.Trim() == null || iptaltarihi.Trim() == "" || iptaltarihi.Trim().Length == 0)
        {
        }
        else
        {
            jnotdaily = "";
        }
        LBLjdaily.Text = jnotdaily;


        //takviye isteği varmı
        SqlCommand cmdtakistekoku = new SqlCommand("SP_takviyeistekbak", baglanti);
        cmdtakistekoku.CommandType = CommandType.StoredProcedure;
        cmdtakistekoku.Parameters.Add("@sonuc", SqlDbType.Char, 1);
        cmdtakistekoku.Parameters["@sonuc"].Direction = ParameterDirection.Output;
        cmdtakistekoku.ExecuteNonQuery();
        string takistekoku = cmdtakistekoku.Parameters["@sonuc"].Value.ToString().Trim();
        cmdtakistekoku.Dispose();
        if (takistekoku == "2") // durum 1 ise istek yapılmış 2 ise kabul edilmiştir
        {
            ButtonTKVYsp.ForeColor = System.Drawing.Color.Black;
        }
        else if (takistekoku == "1")
        {
            ButtonTKVYsp.ForeColor = System.Drawing.Color.Red;
        }

    }
    protected void DLDarica_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //acente bilgisi
            Label Lblimonokop = (Label)e.Item.FindControl("Lblimono");
            string imonobul = Lblimonokop.Text;
            LinkButton Lblgemiadikop = (LinkButton)e.Item.FindControl("Lblgemiadi");
            Label Lblacentekop = (Label)e.Item.FindControl("Lblacente");

            if (Lblgemiadikop.Text.ToLower() == "takviye")
            { Lblgemiadikop.Enabled = false; }

            if (string.IsNullOrEmpty(Lblgemiadikop.Text) != true && Lblgemiadikop.Text.ToLower() != "takviye")
            {
                string acente = "";
                string reqno = "";
                string loa = "";
                string bowt = "";
                string strnt = "";
                string draft = "";
                string bilgi = "";
                string notlar = "";
                string dest = "";
                string destrih = "";
                string bayrak = "";

                SqlCommand cmdisokuup = new SqlCommand("SP_Isliste_AcenteReqnoal", baglanti);
                cmdisokuup.CommandType = CommandType.StoredProcedure;
                cmdisokuup.Parameters.AddWithValue("@imono", Convert.ToInt32(imonobul));
                SqlDataReader dr = cmdisokuup.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dest = dr["yanasmalimani"].ToString();
                        destrih = dr["yanasmarihtimi"].ToString();
                        acente = dr["acente"].ToString();
                        reqno = dr["talepno"].ToString();
                        loa = dr["loa"].ToString();
                        bowt = dr["bowt"].ToString();
                        strnt = dr["strnt"].ToString();
                        draft = dr["draft"].ToString();
                        bilgi = dr["bilgi"].ToString();
                        notlar = dr["notlar"].ToString();
                        bayrak = dr["bayrak"].ToString();
                    }
                }
                dr.Close();
                cmdisokuup.Dispose();

                if (acente.Length > 9)
                { Lblacentekop.Text = acente.Substring(0, 9) + "."; }
                else
                { Lblacentekop.Text = acente; }
                Lblacentekop.ToolTip = acente;

                Lblgemiadikop.ToolTip = "Req.no: " + reqno + " Dest: " + dest + "/" + destrih + " Flag: " + bayrak + " Loa: " + loa + " Bow-Strn Th: " + bowt + "-" + strnt + " Tpp: " + bilgi + " Tug: " + draft + " Notes: " + notlar;
            }



            //Fatique last 24 açık gri ve total de koyu gri font color değiş
            Label lblcolor = (Label)e.Item.FindControl("LBpgecmis");
            Label lblyorul = (Label)e.Item.FindControl("Lblyorulma");
            Label lbllastdy = (Label)e.Item.FindControl("Lbllastday");
            if (float.Parse(lblyorul.Text) > 0.46) // toplamdaki yorulma hesabı
            { lblcolor.Style.Add("color", "#999"); }
            if (float.Parse(lbllastdy.Text) < 10) // last24 yorulma hesabı
            { lblcolor.Style.Add("color", "#999"); }

            //darıcaya ait gemi mavi
            Label Lblinisyerigizcolor = (Label)e.Item.FindControl("Lblinisyerigiz");

            SqlCommand cmdRespistal = new SqlCommand("SP_RespistFmPort", baglanti);
            cmdRespistal.CommandType = CommandType.StoredProcedure;
            cmdRespistal.Parameters.AddWithValue("@seciliport", Lblinisyerigizcolor.Text);
            cmdRespistal.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
            cmdRespistal.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
            cmdRespistal.ExecuteNonQuery();
            string bagliist = cmdRespistal.Parameters["@bagliistasyon"].Value.ToString().Trim();
            cmdRespistal.Dispose();

            if (Lblinisyerigizcolor.Text.ToLower() == "yelkenkaya")
            { Lblgemiadikop.Style.Add("color", "#6632FF"); }// yelkenkaya gemi rengi farklı
            else if (bagliist == "2")
            { Lblgemiadikop.Style.Add("color", "#b85503"); }

            //terste kalan pillot renkli
            if (Convert.ToInt16(DateTime.Now.Day.ToString()) == Convert.ToInt16(varbiter.Text.Substring(0, 2)))
            {
                Label LblKapnocolor = (Label)e.Item.FindControl("LblKapno");
                SqlCommand cmdgiristal = new SqlCommand("SP_Pilotgirisist", baglanti);
                cmdgiristal.CommandType = CommandType.StoredProcedure;
                cmdgiristal.Parameters.AddWithValue("@kapno", LblKapnocolor.Text.Trim());
                cmdgiristal.Parameters.Add("@girisist", SqlDbType.Char, 1);
                cmdgiristal.Parameters["@girisist"].Direction = ParameterDirection.Output;
                cmdgiristal.ExecuteNonQuery();
                string girisist = cmdgiristal.Parameters["@girisist"].Value.ToString().Trim();
                cmdgiristal.Dispose();
                Label LBpgecmiscolor = (Label)e.Item.FindControl("LBpgecmis");
                if (girisist == "2")
                { LBpgecmiscolor.Style.Add("color", "#b85503"); }
            }

            // tuş göster gizle ayarlanıyor
            Label mylabel = (Label)e.Item.FindControl("lblDurum");
            String puan = mylabel.Text;


            if (Session["yetki"].ToString() != "1") // operator yetki 1 değilse gizle
            {
                e.Item.FindControl("BtnProcessiptal").Visible = (false);
                e.Item.FindControl("BtnProcessChgStn").Visible = (false);
                e.Item.FindControl("BtnProcessChgDest").Visible = (false);

                e.Item.FindControl("BtnPob").Visible = (false);
                e.Item.FindControl("BtnPoff").Visible = (false);
                e.Item.FindControl("BtnIstasyoncikis").Visible = (false);
                e.Item.FindControl("BtnIstasyongelis").Visible = (false);

                e.Item.FindControl("LblIstasyoncikis").Visible = (false);
                e.Item.FindControl("LblPob").Visible = (false);
                e.Item.FindControl("LblPoff").Visible = (false);
                e.Item.FindControl("LblIstasyongelis").Visible = (false);

                e.Item.FindControl("LblIstasyoncikiseta").Visible = (false);
                e.Item.FindControl("LblPobeta").Visible = (false);
                e.Item.FindControl("LblPoffeta").Visible = (false);
                e.Item.FindControl("LblIstasyongeliseta").Visible = (false);

                if (puan == "1")
                {
                    e.Item.FindControl("LblIstasyoncikiseta").Visible = (true);
                    e.Item.FindControl("LblPobeta").Visible = (true);
                    e.Item.FindControl("LblPoffeta").Visible = (true);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);
                }

                else if (puan == "2")
                {
                    e.Item.FindControl("LblIstasyoncikis").Visible = (true);
                    e.Item.FindControl("LblPobeta").Visible = (true);
                    e.Item.FindControl("LblPoffeta").Visible = (true);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);

                }

                else if (puan == "3")
                {
                    e.Item.FindControl("LblIstasyoncikis").Visible = (true);
                    e.Item.FindControl("LblPob").Visible = (true);
                    e.Item.FindControl("LblPoffeta").Visible = (true);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);
                }

                else if (puan == "4")
                {
                    e.Item.FindControl("LblIstasyoncikis").Visible = (true);
                    e.Item.FindControl("LblPob").Visible = (true);
                    e.Item.FindControl("LblPoff").Visible = (true);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);
                }
            }
            else // giren opr ise
            {


                if (puan == "0")
                {
                    e.Item.FindControl("BtnProcessiptal").Visible = (false);
                    e.Item.FindControl("BtnProcessChgStn").Visible = (true);
                    e.Item.FindControl("BtnProcessChgDest").Visible = (false);

                    e.Item.FindControl("BtnPob").Visible = (false);
                    e.Item.FindControl("BtnPoff").Visible = (false);
                    e.Item.FindControl("BtnIstasyoncikis").Visible = (false);
                    e.Item.FindControl("BtnIstasyongelis").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikis").Visible = (false);
                    e.Item.FindControl("LblPob").Visible = (false);
                    e.Item.FindControl("LblPoff").Visible = (false);
                    e.Item.FindControl("LblIstasyongelis").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikiseta").Visible = (false);
                    e.Item.FindControl("LblPobeta").Visible = (false);
                    e.Item.FindControl("LblPoffeta").Visible = (false);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (false);

                }

                else if (puan == "1") // iş atanmış
                {
                    e.Item.FindControl("BtnProcessiptal").Visible = (true);
                    e.Item.FindControl("BtnProcessChgStn").Visible = (false);
                    e.Item.FindControl("BtnProcessChgDest").Visible = (false);

                    e.Item.FindControl("BtnIstasyoncikis").Visible = (true);
                    e.Item.FindControl("BtnPob").Visible = (false);
                    e.Item.FindControl("BtnPoff").Visible = (false);
                    e.Item.FindControl("BtnIstasyongelis").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikiseta").Visible = (false);
                    e.Item.FindControl("LblIstasyoncikis").Visible = (false);
                    e.Item.FindControl("LblPobeta").Visible = (true);
                    e.Item.FindControl("LblPob").Visible = (false);
                    e.Item.FindControl("LblPoffeta").Visible = (true);
                    e.Item.FindControl("LblPoff").Visible = (false);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);
                    e.Item.FindControl("LblIstasyongelis").Visible = (false);
                }

                else if (puan == "2")// istasyondan çıkmış
                {
                    e.Item.FindControl("BtnProcessiptal").Visible = (false);
                    e.Item.FindControl("BtnProcessChgStn").Visible = (false);
                    e.Item.FindControl("BtnProcessChgDest").Visible = (true);

                    e.Item.FindControl("BtnIstasyoncikis").Visible = (false);
                    e.Item.FindControl("BtnPob").Visible = (true);
                    e.Item.FindControl("BtnPoff").Visible = (false);
                    e.Item.FindControl("BtnIstasyongelis").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikiseta").Visible = (false);
                    e.Item.FindControl("LblIstasyoncikis").Visible = (true);
                    e.Item.FindControl("LblPobeta").Visible = (false);
                    e.Item.FindControl("LblPob").Visible = (false);
                    e.Item.FindControl("LblPoffeta").Visible = (true);
                    e.Item.FindControl("LblPoff").Visible = (false);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);
                    e.Item.FindControl("LblIstasyongelis").Visible = (false);

                }

                else if (puan == "3") // pob gerçekleşmiş
                {
                    e.Item.FindControl("BtnProcessiptal").Visible = (false);
                    e.Item.FindControl("BtnProcessChgStn").Visible = (false);
                    e.Item.FindControl("BtnProcessChgDest").Visible = (true);

                    e.Item.FindControl("BtnIstasyoncikis").Visible = (false);
                    e.Item.FindControl("BtnPob").Visible = (false);
                    e.Item.FindControl("BtnPoff").Visible = (true);
                    e.Item.FindControl("BtnIstasyongelis").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikiseta").Visible = (false);
                    e.Item.FindControl("LblIstasyoncikis").Visible = (true);
                    e.Item.FindControl("LblPobeta").Visible = (false);
                    e.Item.FindControl("LblPob").Visible = (true);
                    e.Item.FindControl("LblPoffeta").Visible = (false);
                    e.Item.FindControl("LblPoff").Visible = (false);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (true);
                    e.Item.FindControl("LblIstasyongelis").Visible = (false);
                }

                else if (puan == "4")// poff gerçekleşmiş
                {
                    if (Lblgemiadikop.Text.ToLower() == "takviye")   //takviyede takviye yolda iptali mümkün
                    {
                        e.Item.FindControl("BtnProcessiptal").Visible = (true);
                        e.Item.FindControl("BtnProcessChgStn").Visible = (false);
                    }
                    else
                    {
                        e.Item.FindControl("BtnProcessiptal").Visible = (false);
                        e.Item.FindControl("BtnProcessChgStn").Visible = (true);
                    }

                    e.Item.FindControl("BtnProcessChgDest").Visible = (false);

                    e.Item.FindControl("BtnIstasyoncikis").Visible = (false);
                    e.Item.FindControl("BtnPob").Visible = (false);
                    e.Item.FindControl("BtnPoff").Visible = (false);
                    e.Item.FindControl("BtnIstasyongelis").Visible = (true);

                    e.Item.FindControl("LblIstasyoncikiseta").Visible = (false);
                    e.Item.FindControl("LblIstasyoncikis").Visible = (true);
                    e.Item.FindControl("LblPobeta").Visible = (false);
                    e.Item.FindControl("LblPob").Visible = (true);
                    e.Item.FindControl("LblPoffeta").Visible = (false);
                    e.Item.FindControl("LblPoff").Visible = (true);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (false);
                    e.Item.FindControl("LblIstasyongelis").Visible = (false);
                }

                else
                {
                    e.Item.FindControl("BtnProcessiptal").Visible = (false);
                    e.Item.FindControl("BtnProcessChgStn").Visible = (false);
                    e.Item.FindControl("BtnProcessChgDest").Visible = (false);

                    e.Item.FindControl("BtnPob").Visible = (false);
                    e.Item.FindControl("BtnPoff").Visible = (false);
                    e.Item.FindControl("BtnIstasyoncikis").Visible = (false);
                    e.Item.FindControl("BtnIstasyongelis").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikis").Visible = (false);
                    e.Item.FindControl("LblPob").Visible = (false);
                    e.Item.FindControl("LblPoff").Visible = (false);
                    e.Item.FindControl("LblIstasyongelis").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikiseta").Visible = (false);
                    e.Item.FindControl("LblPobeta").Visible = (false);
                    e.Item.FindControl("LblPoffeta").Visible = (false);
                    e.Item.FindControl("LblIstasyongeliseta").Visible = (false);
                }
            }
            //cs tuşu 3 saat sonra gizleniyor 
            if (puan != "4")
            {
                if (Convert.ToDateTime(varbaslar.Text).AddHours(3) < (DateTime.Now))
                {
                    e.Item.FindControl("BtnProcessChgStn").Visible = (false);
                }
            }

            if (puan == "0")
            {
                Label LblNokop = (Label)e.Item.FindControl("LblNo");
                LblNokop.Style.Add("color", "#ee1111");
                LblNokop.Style.Add("font-weight", "bold");
            }

            // port plan için yelkenkaya bold degil
            LinkButton Lblportcolorb = (LinkButton)e.Item.FindControl("binisport");
            if (Lblportcolorb.Text.ToLower() == "yelkenkaya")
            {
                Lblportcolorb.Style.Add("font-weight", "normal");
                Lblportcolorb.Style.Add("cursor", "default");
            }
            LinkButton Lblportcolori = (LinkButton)e.Item.FindControl("inisport");
            if (Lblportcolori.Text.ToLower() == "yelkenkaya")
            {
                Lblportcolori.Style.Add("font-weight", "normal");
                Lblportcolori.Style.Add("cursor", "default");
            }
        }
        baglanti.Close();
    }



    //private void makemainlog(string kapadim,string gemiadim,string tusadim,string tusyolum,string islemaciklam)
    //{

    //    SqlConnection baglanti = AnaKlas.baglan();
    //    SqlCommand cmdmainlog = new SqlCommand("SP_Into_mainlog_all", baglanti);
    //    cmdmainlog.CommandType = CommandType.StoredProcedure;
    //    cmdmainlog.Parameters.AddWithValue("@sessionadi", LBonline.Text);
    //    cmdmainlog.Parameters.AddWithValue("@kapadisoyadi", kapadim);
    //    cmdmainlog.Parameters.AddWithValue("@gemiadi", gemiadim);
    //    cmdmainlog.Parameters.AddWithValue("@tusadi", tusadim);
    //    cmdmainlog.Parameters.AddWithValue("@tusyolu", tusyolum);
    //    cmdmainlog.Parameters.AddWithValue("@islemacikla", islemaciklam);
    //    cmdmainlog.Parameters.AddWithValue("@tiklemeani", DateTime.Now.ToString());
    //    cmdmainlog.ExecuteNonQuery();
    //    cmdmainlog.Dispose();
    //    baglanti.Close();
    //}

    protected void DLDarica_ItemCommand(object source, DataListCommandEventArgs e) // DURUM VE DATETIME GÜNCELLEME İLE DETAY KAYIT SONDA
    {

        SqlConnection baglanti = AnaKlas.baglan();
        LinkButton BtnProcessiptalkopya = (LinkButton)e.Item.FindControl("BtnProcessiptal");
        String secilikapno = (BtnProcessiptalkopya.CommandArgument).ToString();

        SqlCommand cmdPilotDegisismial = new SqlCommand("SP_PilotDegismeciismial", baglanti);
        cmdPilotDegisismial.CommandType = CommandType.StoredProcedure;
        cmdPilotDegisismial.Parameters.AddWithValue("@secilikapno", secilikapno);
        string degismeciadisoyadi = "";
        SqlDataReader dr = cmdPilotDegisismial.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                degismeciadisoyadi = dr["degismeciadisoyadi"].ToString();
            }
        }
        dr.Close();
        cmdPilotDegisismial.Dispose();

        SqlCommand cmdPilotGemisiismial = new SqlCommand("SP_PilotGemisiismial", baglanti);
        cmdPilotGemisiismial.CommandType = CommandType.StoredProcedure;
        cmdPilotGemisiismial.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdPilotGemisiismial.Parameters.Add("@pilotgemisiismi", SqlDbType.Char, 30);
        cmdPilotGemisiismial.Parameters["@pilotgemisiismi"].Direction = ParameterDirection.Output;
        cmdPilotGemisiismial.ExecuteNonQuery();
        string shipadi = cmdPilotGemisiismial.Parameters["@pilotgemisiismi"].Value.ToString().Trim();
        cmdPilotGemisiismial.Dispose();

        string islemzamani = TarihSaatYaziYapDMYhm(DateTime.Now);
        Lblprocesstime.Text = islemzamani;
        Lblprocesstime.BorderColor = System.Drawing.Color.Gray;
        Lblprocesstime.BorderWidth = 1;
        Lblprocesstime.Enabled = true;
        Lblprocesstime.Visible = true;

        DDCjob.Visible = false;
        DDCjob.Items.Clear();
        DDCjob.Items.Add("No");
        DDCjob.Items.Add("Yes");
        DDCjob.SelectedItem.Selected = false;
        DDCjob.Items.FindByText("No").Selected = true;
        LtCjobText.Visible = false;
        DDCjobReason.Visible = false;


        if (e.CommandName == "Istasyoncikis")
        {
            {
                BacpCSok.CommandName = "istcik";
                BacpCSok.CommandArgument = secilikapno.ToString();
                Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/> Off-Station </span>";
                this.ModalPopupCSonayMessage.Show();

                //makemainlog(degismeciadisoyadi, shipadi, "BtnIstasyoncikis", "DLDarica_ItemCommand Istasyoncikis", "Darıca istasyon çıkış");

            }
        }
        else if (e.CommandName == "Gemiyebinis")
        {
            BacpCSok.CommandName = "pobbin";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/> POB </span>";
            this.ModalPopupCSonayMessage.Show();
        }
        else if (e.CommandName == "Gemideninis")
        {
            BacpCSok.CommandName = "poffin";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/> POFF </span>";
            this.ModalPopupCSonayMessage.Show();

        }

        else if (e.CommandName == "ProcessChgStn")
        {
            BacpCSok.CommandName = "ChgStn";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Lblprocesstime.Enabled = false;

            SqlCommand cmdRespistal = new SqlCommand("SP_ResponsibleStation", baglanti);
            cmdRespistal.CommandType = CommandType.StoredProcedure;
            cmdRespistal.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdRespistal.Parameters.Add("@sonuc", SqlDbType.VarChar, 1);
            cmdRespistal.Parameters["@sonuc"].Direction = ParameterDirection.Output;
            cmdRespistal.ExecuteNonQuery();
            string reskontrol = cmdRespistal.Parameters["@sonuc"].Value.ToString().Trim();
            cmdRespistal.Dispose();

            if (reskontrol == "1")
            {
                Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/>  Change Station </span>";
            }
            else
            {
                Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/>  Change Station </span>";
            }

            this.ModalPopupCSonayMessage.Show();

        }

        else if (e.CommandName == "Processiptal")
        {
            BacpCSok.CommandName = "isiptal";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Lblprocesstime.Enabled = false;
            Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/>  Cancel Job </span>";
            this.ModalPopupCSonayMessage.Show();

        }


        else if (e.CommandName == "BtnProcessChgDest")
        {
            SqlCommand cmdPilotGemisiVarisyeri = new SqlCommand("SP_PilotGemisiVarisyeri", baglanti);
            cmdPilotGemisiVarisyeri.CommandType = CommandType.StoredProcedure;
            cmdPilotGemisiVarisyeri.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdPilotGemisiVarisyeri.Parameters.Add("@pilotvarisyeri", SqlDbType.VarChar, 20);
            cmdPilotGemisiVarisyeri.Parameters["@pilotvarisyeri"].Direction = ParameterDirection.Output;
            cmdPilotGemisiVarisyeri.ExecuteNonQuery();
            string kayitliliman = cmdPilotGemisiVarisyeri.Parameters["@pilotvarisyeri"].Value.ToString().Trim();
            cmdPilotGemisiVarisyeri.Dispose();

            SqlCommand cmdPilotGemisiVarisrihtimi = new SqlCommand("SP_PilotGemisiVarisRihtimi", baglanti);
            cmdPilotGemisiVarisrihtimi.CommandType = CommandType.StoredProcedure;
            cmdPilotGemisiVarisrihtimi.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdPilotGemisiVarisrihtimi.Parameters.Add("@pilotvarisrihtimi", SqlDbType.VarChar, 16);
            cmdPilotGemisiVarisrihtimi.Parameters["@pilotvarisrihtimi"].Direction = ParameterDirection.Output;
            cmdPilotGemisiVarisrihtimi.ExecuteNonQuery();
            string kayitlirihtim = cmdPilotGemisiVarisrihtimi.Parameters["@pilotvarisrihtimi"].Value.ToString().Trim();
            cmdPilotGemisiVarisrihtimi.Dispose();

            BacpCSokcd.CommandArgument = secilikapno;
            Litmodstnmescd.Text = "New Destination of " + shipadi.ToUpper();


            SqlCommand cmdDDLlim = new SqlCommand("SP_DDLlimanal_notorder", baglanti);
            cmdDDLlim.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmdDDLlim;
            DataSet ds = new DataSet();
            adapter.Fill(ds, "limanlar");
            DDLdesplace.Items.Clear();
            DDLdesplace.DataValueField = "limanno";
            DDLdesplace.DataTextField = "limanadi";
            DDLdesplace.DataSource = ds;
            DDLdesplace.DataBind();
            DDLdesplace.ClearSelection();
            cmdDDLlim.Dispose();

            ListItem cdp = DDLdesplace.Items.FindByText(kayitliliman);
            if (cdp != null)
            {
                DDLdesplace.Items.FindByText(kayitliliman).Selected = true;
            }


            SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
            cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
            cmdDDLrihtim.Parameters.AddWithValue("@limanadi", kayitliliman);
            SqlDataAdapter adapterr = new SqlDataAdapter();
            adapterr.SelectCommand = cmdDDLrihtim;
            DataSet dsr = new DataSet();
            adapterr.Fill(dsr, "limanlarr");
            DDLdesplaceno.Items.Clear();
            DDLdesplaceno.DataValueField = "id";
            DDLdesplaceno.DataTextField = "rihtimadi";
            DDLdesplaceno.DataSource = dsr;
            DDLdesplaceno.DataBind();
            DDLdesplaceno.ClearSelection();
            cmdDDLrihtim.Dispose();

            if (kayitlirihtim == "0")
            {
                DDLdesplaceno.Visible = false;
            }

            ListItem cdpn = DDLdesplaceno.Items.FindByText(kayitlirihtim);
            if (cdpn != null)
            {
                DDLdesplaceno.Items.FindByText(kayitlirihtim).Selected = true;
            }
            this.MPCDonay.Show();
        }

        else if (e.CommandName == "Istasyongelis")
        {
            BacpCSok.CommandName = "istgir";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/> On-Station </span>";

            DDCjob.Visible = true;
            DDCjob.Items.Clear();
            DDCjob.Items.Add("No");
            DDCjob.Items.Add("Yes");
            DDCjob.SelectedItem.Selected = false;
            DDCjob.Items.FindByText("No").Selected = true;
            LtCjobText.Visible = true;
            DDCjobReason.Visible = false;

            this.ModalPopupCSonayMessage.Show();
        }

        else if (e.CommandName == "jurnot")
        {

            jurnotfill(secilikapno, baglanti);

            //byd bak
            SqlCommand cmdPilotGemisijoblist = new SqlCommand("SP_PilotGemisijoblist", baglanti);
            cmdPilotGemisijoblist.CommandType = CommandType.StoredProcedure;
            cmdPilotGemisijoblist.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdPilotGemisijoblist.Parameters.Add("@gemijoblistid", SqlDbType.Int);
            cmdPilotGemisijoblist.Parameters["@gemijoblistid"].Direction = ParameterDirection.Output;
            cmdPilotGemisijoblist.ExecuteNonQuery();
            string gemijoblistid = cmdPilotGemisijoblist.Parameters["@gemijoblistid"].Value.ToString().Trim();
            cmdPilotGemisijoblist.Dispose();

            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(gemijoblistid));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            string lybyol = lybbak.Parameters["@lybyol"].Value.ToString().Trim();
            lybbak.Dispose();

            Bshowbyd.Enabled = false;
            Bshowbyd.CommandName = "";
            Bshowbyd.ForeColor = System.Drawing.Color.Gray;
            if (string.IsNullOrEmpty(lybyol) != true)
            {
                Bshowbyd.Enabled = true;
                Bshowbyd.ForeColor = System.Drawing.Color.Blue;
                Bshowbyd.CommandName = gemijoblistid;
            }
            //byd biter


            this.ModalPopupjurnot.Show();

        }

        //portplan
        String Btnbinisportadi = "Yelkenkaya";
        String Btninisportadi = "Yelkenkaya";
        LinkButton Btnbinisport = (LinkButton)e.Item.FindControl("binisport");
        if (string.IsNullOrEmpty(Btnbinisport.Text) != true)
        {
            Btnbinisportadi = (Btnbinisport.Text).ToString();
        }

        LinkButton Btninisport = (LinkButton)e.Item.FindControl("inisport");
        if (string.IsNullOrEmpty(Btninisport.Text) != true)
        {
            Btninisportadi = (Btninisport.Text).ToString();
        }

        if (e.CommandName == "linkleb")
        {
            if (dogrulinkyap(Btnbinisportadi.ToLower()) != "yelkenkaya")
            {
                Buttonlbaddnotes.CommandName = Btnbinisportadi;
                Image1.ImageUrl = "../images/limanplan/" + dogrulinkyap(Btnbinisportadi.ToLower()) + ".jpg";
                Image1.Width = 747;
                this.MPEdrawing.Show();
            }
        }
        if (e.CommandName == "linklei")
        {
            if (dogrulinkyap(Btninisportadi.ToLower()) != "yelkenkaya")
            {
                Buttonlbaddnotes.CommandName = Btninisportadi;
                Image1.ImageUrl = "../images/limanplan/" + dogrulinkyap(Btninisportadi.ToLower()) + ".jpg";
                Image1.Width = 747;
                this.MPEdrawing.Show();
            }
        }


        baglanti.Close();

    }


    private void jurnotfill(string secilikapno, SqlConnection baglanti)
    {
        SqlCommand cmdPilotGemisijoblist = new SqlCommand("SP_PilotGemisijoblist", baglanti);
        cmdPilotGemisijoblist.CommandType = CommandType.StoredProcedure;
        cmdPilotGemisijoblist.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdPilotGemisijoblist.Parameters.Add("@gemijoblistid", SqlDbType.Int);
        cmdPilotGemisijoblist.Parameters["@gemijoblistid"].Direction = ParameterDirection.Output;
        cmdPilotGemisijoblist.ExecuteNonQuery();
        string gemijoblistid = cmdPilotGemisijoblist.Parameters["@gemijoblistid"].Value.ToString().Trim();
        cmdPilotGemisijoblist.Dispose();

        string gemiadi = "";
        string rom1 = "";
        string rom2 = "";
        string rom3 = "";
        string rom4 = "";
        string rom5 = "";
        string mboat = "";
        string calsaat1 = "";
        string calsaat2 = "";
        string calsaat3 = "";
        string calsaat4 = "";
        string acente = "";
        string oper = "";
        string durum = "";
        string destinat = "";
        //string jnot = "";
        string jnotlast = ""; //TBjurnotlast

        SqlCommand cmdpilotokufull = new SqlCommand("SP_PilotOkuFull_fmKapno", baglanti);
        cmdpilotokufull.CommandType = CommandType.StoredProcedure;
        cmdpilotokufull.Parameters.AddWithValue("@secilikapno", secilikapno);
        SqlDataReader readpilotfull = cmdpilotokufull.ExecuteReader();
        if (readpilotfull.HasRows)
        {
            while (readpilotfull.Read())
            {
                gemiadi = readpilotfull["gemiadi"].ToString();
                rom1 = readpilotfull["rom1"].ToString();
                rom2 = readpilotfull["rom2"].ToString();
                rom3 = readpilotfull["rom3"].ToString();
                rom4 = readpilotfull["rom4"].ToString();
                rom5 = readpilotfull["rom5"].ToString();
                mboat = readpilotfull["mboat"].ToString();
                calsaat1 = readpilotfull["calsaat1"].ToString();
                calsaat2 = readpilotfull["calsaat2"].ToString();
                calsaat3 = readpilotfull["calsaat3"].ToString();
                calsaat4 = readpilotfull["calsaat4"].ToString();
                acente = readpilotfull["acente"].ToString();
                oper = readpilotfull["oper"].ToString();
                durum = readpilotfull["durum"].ToString();
                destinat = readpilotfull["inisyeri"].ToString();
                //jnot = readpilotfull["jnot"].ToString();
                jnotlast = readpilotfull["jnot"].ToString();
                TBjurnotfalse.Text = readpilotfull["jnot"].ToString();
            }
        }
        readpilotfull.Close();
        cmdpilotokufull.Dispose();

        LBJEsn.Text = gemiadi;
        TBJEagency.Text = acente;
        TBJEetadt.Text = calsaat1;
        TBJEgrt.Text = calsaat2;
        TBJEarhr1.Text = calsaat3;
        TBJEarhr2.Text = calsaat4;
        TBJEloa.Text = LBonline.Text;
        //TBjurnot.Text = jnot;
        TBjurnotlast.Items.Clear();


        string[] metinayir = jnotlast.Split('|');         //metinayir dizi değişkenine Split metoduyla "]" ayraç göstererek kelimeleri teker teker al.
        for (int ik = 0; ik < metinayir.Length; ik++)
        {
            TBjurnotlast.Items.Add(metinayir[ik]);
            TBjurnotlast.Items[ik].Attributes["title"] = TBjurnotlast.Items[ik].Text;
        }

        SqlCommand cmdtugs = new SqlCommand("SP_tugs_oku", baglanti);
        cmdtugs.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterr = new SqlDataAdapter();
        adapterr.SelectCommand = cmdtugs;
        DataSet dsr = new DataSet();
        adapterr.Fill(dsr, "tuglar");
        cmdtugs.Dispose();

        DDLJEap.Items.Clear();
        DDLJEap.DataValueField = "id";
        DDLJEap.DataTextField = "short";
        DDLJEap.DataSource = dsr;
        DDLJEap.DataBind();
        DDLJEap.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEap.SelectedIndex = 0;
        DDLJEap.Visible = true;
        DDLJEap.Enabled = true;
        DDLJEap.ClearSelection();
        if (rom1 == "" || rom1 == null)
        { }
        else
        {
            ListItem cdpn = DDLJEap.Items.FindByText(rom1);

            if (cdpn != null)
            {
                DDLJEap.Items.FindByText(rom1).Selected = true;
            }
        }

        DDLJEdp.Items.Clear();
        DDLJEdp.DataValueField = "id";
        DDLJEdp.DataTextField = "short";
        DDLJEdp.DataSource = dsr;
        DDLJEdp.DataBind();
        DDLJEdp.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEdp.SelectedIndex = 0;
        DDLJEdp.Visible = true;
        DDLJEdp.Enabled = true;
        DDLJEdp.ClearSelection();
        if (rom2 == "" || rom2 == null)
        { }
        else
        {
            ListItem cdpn2 = DDLJEdp.Items.FindByText(rom2);

            if (cdpn2 != null)
            {
                DDLJEdp.Items.FindByText(rom2).Selected = true;


            }
        }

        DDLJEdb.Items.Clear();
        DDLJEdb.DataValueField = "id";
        DDLJEdb.DataTextField = "short";
        DDLJEdb.DataSource = dsr;
        DDLJEdb.DataBind();
        DDLJEdb.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEdb.SelectedIndex = 0;
        DDLJEdb.Visible = true;
        DDLJEdb.Enabled = true;
        DDLJEdb.ClearSelection();
        if (rom3 == "" || rom3 == null)
        { }
        else
        {
            ListItem cdpn3 = DDLJEdb.Items.FindByText(rom3);

            if (cdpn3 != null)
            {
                DDLJEdb.Items.FindByText(rom3).Selected = true;

            }
        }

        DDLJEflag.Items.Clear();
        DDLJEflag.DataValueField = "id";
        DDLJEflag.DataTextField = "short";
        DDLJEflag.DataSource = dsr;
        DDLJEflag.DataBind();
        DDLJEflag.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEflag.SelectedIndex = 0;
        DDLJEflag.Visible = true;
        DDLJEflag.Enabled = true;
        DDLJEflag.ClearSelection();
        if (rom4 == "" || rom4 == null)
        { }
        else
        {
            ListItem cdpn4 = DDLJEflag.Items.FindByText(rom4);

            if (cdpn4 != null)
            {
                DDLJEflag.Items.FindByText(rom4).Selected = true;
                //if (DDLJEap.SelectedItem.Text != "...")
                //{ DDLJEflag.Items.FindByText(DDLJEap.SelectedItem.Text).Enabled = false; }
                //if (DDLJEdp.SelectedItem.Text != "...")
                //{ DDLJEflag.Items.FindByText(DDLJEdp.SelectedItem.Text).Enabled = false; }
                //if (DDLJEdb.SelectedItem.Text != "...")
                //{ DDLJEflag.Items.FindByText(DDLJEdb.SelectedItem.Text).Enabled = false; }
            }
        }

        DDLJEextra.Items.Clear();
        DDLJEextra.DataValueField = "id";
        DDLJEextra.DataTextField = "short";
        DDLJEextra.DataSource = dsr;
        DDLJEextra.DataBind();
        DDLJEextra.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEextra.SelectedIndex = 0;
        DDLJEextra.Visible = true;
        DDLJEextra.Enabled = true;
        DDLJEextra.ClearSelection();
        if (rom5 == "" || rom5 == null)
        { }
        else
        {
            ListItem cdpn = DDLJEextra.Items.FindByText(rom5);

            if (cdpn != null)
            {
                DDLJEextra.Items.FindByText(rom5).Selected = true;
            }
        }

        /// Romorkör satır renklendirme
        SqlCommand cmdtdoku = new SqlCommand("SP_tugs_oku", baglanti);
        cmdtdoku.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdt = cmdtdoku.ExecuteReader();
        string bolge = "";
        int i = 1;

        if (drdt.HasRows)
        {
            while (drdt.Read())
            {
                bolge = drdt["bolge"].ToString();
                if (bolge == "2")
                {
                    DDLJEap.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdp.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdb.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEflag.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEextra.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                }
                i++;
            }
        }
        drdt.Close();
        cmdtdoku.Dispose();


        SqlCommand cmdtugs2 = new SqlCommand("SP_tugs_okumoor", baglanti);
        cmdtugs2.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterr2 = new SqlDataAdapter();
        adapterr2.SelectCommand = cmdtugs2;
        DataSet dsr2 = new DataSet();
        adapterr2.Fill(dsr2, "tuglar2");
        cmdtugs2.Dispose();

        DDLJEtip.Items.Clear();
        DDLJEtip.DataValueField = "id";
        DDLJEtip.DataTextField = "short";
        DDLJEtip.DataSource = dsr2;
        DDLJEtip.DataBind();

        DDLJEtip.Items.Insert(0, new ListItem("...", String.Empty));
        DDLJEtip.SelectedIndex = 0;
        DDLJEtip.Visible = true;
        DDLJEtip.Enabled = true;
        DDLJEtip.ClearSelection();
        if (mboat == "" || mboat == null)
        { }
        else
        {
            ListItem cdpn5 = DDLJEtip.Items.FindByText(mboat);

            if (cdpn5 != null)
            {
                DDLJEtip.Items.FindByText(mboat).Selected = true;
            }
        }


        /// Palamar satır renklendirme
        SqlCommand cmdtdokumoor = new SqlCommand("SP_tugs_okumoor", baglanti);
        cmdtdokumoor.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdtm = cmdtdokumoor.ExecuteReader();
        string bolgem = "";
        int im = 1;

        if (drdtm.HasRows)
        {
            while (drdtm.Read())
            {
                bolgem = drdtm["bolge"].ToString();
                if (bolgem == "2")
                {
                    DDLJEtip.Items[im].Attributes.Add("style", "background-color: Orange !important;");
                }
                im++;
            }
        }
        drdtm.Close();
        cmdtdokumoor.Dispose();






        Bacceptedjur.CommandArgument = gemijoblistid;
        LJEid.Text = secilikapno;

        if (DDLJEap.SelectedItem.Text == "...") { DDLJEdp.Enabled = false; }
        //if (DDLJEdp.SelectedItem.Text == "...") { DDLJEdb.Enabled = false; }
        if (DDLJEdb.SelectedItem.Text == "...") { DDLJEflag.Enabled = false; }

        if (durum == "2" || durum == "3" || durum == "4")
        {
            if (destinat.Length > 4 && destinat.Substring(0, 5) != "Demir")
            {
                TBJEgrt.Enabled = true;
                TBJEetadt.Enabled = true;
                TBJEarhr1.Enabled = true;
                TBJEarhr2.Enabled = true;
            }
        }
        else
        {
            TBJEetadt.Enabled = false;
            TBJEgrt.Enabled = false;
            TBJEarhr1.Enabled = false;
            TBJEarhr2.Enabled = false;
        }
    }

    protected void DLDaricay_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //acente bilgisi
            Label Lblimonokop = (Label)e.Item.FindControl("Lblimonoy");
            string imonobul = Lblimonokop.Text;
            LinkButton Lblgemiadikop = (LinkButton)e.Item.FindControl("Lblgemiadiy");
            Label Lblacentekop = (Label)e.Item.FindControl("Lblacente");

            if (Lblgemiadikop.Text.ToLower() == "takviye")
            { Lblgemiadikop.Enabled = false; }

            if (string.IsNullOrEmpty(Lblgemiadikop.Text) != true && Lblgemiadikop.Text.ToLower() != "takviye")
            {
                string acente = "";
                string reqno = "";
                string loa = "";
                string bowt = "";
                string strnt = "";
                string draft = "";
                string bilgi = "";
                string notlar = "";
                string dest = "";
                string destrih = "";
                string bayrak = "";

                SqlCommand cmdisokuup = new SqlCommand("SP_Isliste_AcenteReqnoal", baglanti);
                cmdisokuup.CommandType = CommandType.StoredProcedure;
                cmdisokuup.Parameters.AddWithValue("@imono", Convert.ToInt32(imonobul));
                SqlDataReader dr = cmdisokuup.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dest = dr["yanasmalimani"].ToString();
                        destrih = dr["yanasmarihtimi"].ToString();
                        acente = dr["acente"].ToString();
                        reqno = dr["talepno"].ToString();
                        loa = dr["loa"].ToString();
                        bowt = dr["bowt"].ToString();
                        strnt = dr["strnt"].ToString();
                        draft = dr["draft"].ToString();
                        bilgi = dr["bilgi"].ToString();
                        notlar = dr["notlar"].ToString();
                        bayrak = dr["bayrak"].ToString();
                    }
                }
                dr.Close();
                cmdisokuup.Dispose();

                if (acente.Length > 9)
                { Lblacentekop.Text = acente.Substring(0, 9) + "."; }
                else
                { Lblacentekop.Text = acente; }
                Lblacentekop.ToolTip = acente;

                Lblgemiadikop.ToolTip = "Req.no: " + reqno + " Dest: " + dest + "/" + destrih + " Flag: " + bayrak + " Loa: " + loa + " Bow-Strn Th: " + bowt + "-" + strnt + " Tpp: " + bilgi + " Tug: " + draft + " Notes: " + notlar;
            }




            //Fatique last 24 açık gri ve total de koyu gri font color değiş

            Label lblcolor = (Label)e.Item.FindControl("LBpgecmisy");
            Label lblyorul = (Label)e.Item.FindControl("Lblyorulmay");
            Label lbllastdy = (Label)e.Item.FindControl("Lbllastdayy");
            if (float.Parse(lblyorul.Text) > 0.46) // toplamdaki yorulma hesabı
            { lblcolor.Style.Add("color", "#999"); }
            if (float.Parse(lbllastdy.Text) < 10) // last24 yorulma hesabı
            { lblcolor.Style.Add("color", "#999"); }


            //darıcaya ait gemi mavi
            Label Lblinisyerigizcolor = (Label)e.Item.FindControl("Lblinisyerigizy");

            SqlCommand cmdRespistal = new SqlCommand("SP_RespistFmPort", baglanti);
            cmdRespistal.CommandType = CommandType.StoredProcedure;
            cmdRespistal.Parameters.AddWithValue("@seciliport", Lblinisyerigizcolor.Text);
            cmdRespistal.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
            cmdRespistal.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
            cmdRespistal.ExecuteNonQuery();
            string bagliist = cmdRespistal.Parameters["@bagliistasyon"].Value.ToString().Trim();
            cmdRespistal.Dispose();

            if (Lblinisyerigizcolor.Text.ToLower() == "yelkenkaya")
            { Lblgemiadikop.Style.Add("color", "#6632FF"); }// yelkenkaya gemi rengi farklı
            else if (bagliist == "1")
            { Lblgemiadikop.Style.Add("color", "#11255E"); }

            //terste kalan pillot renkli
            if (Convert.ToInt16(DateTime.Now.Day.ToString()) == Convert.ToInt16(varbiter.Text.Substring(0, 2)))
            {
                Label LblKapnocolor = (Label)e.Item.FindControl("LblKapnoy");
                SqlCommand cmdgiristal = new SqlCommand("SP_Pilotgirisist", baglanti);
                cmdgiristal.CommandType = CommandType.StoredProcedure;
                cmdgiristal.Parameters.AddWithValue("@kapno", LblKapnocolor.Text.Trim());
                cmdgiristal.Parameters.Add("@girisist", SqlDbType.Char, 1);
                cmdgiristal.Parameters["@girisist"].Direction = ParameterDirection.Output;
                cmdgiristal.ExecuteNonQuery();
                string girisist = cmdgiristal.Parameters["@girisist"].Value.ToString().Trim();
                cmdgiristal.Dispose();
                Label LBpgecmiscolor = (Label)e.Item.FindControl("LBpgecmisy");
                if (girisist == "1")
                { LBpgecmiscolor.Style.Add("color", "#11255E"); }
            }

            // tuş göster gizle ayarlanıyor
            Label mylabel = (Label)e.Item.FindControl("lblDurumy");
            String puan = mylabel.Text;
            if (Session["yetki"].ToString() != "2") // operator yetki 2 değilse
            {
                e.Item.FindControl("BtnProcessiptaly").Visible = (false);
                e.Item.FindControl("BtnProcessChgStny").Visible = (false);
                e.Item.FindControl("BtnProcessChgDesty").Visible = (false);

                e.Item.FindControl("BtnPoby").Visible = (false);
                e.Item.FindControl("BtnPoffy").Visible = (false);
                e.Item.FindControl("BtnIstasyoncikisy").Visible = (false);
                e.Item.FindControl("BtnIstasyongelisy").Visible = (false);

                e.Item.FindControl("LblIstasyoncikisy").Visible = (false);
                e.Item.FindControl("LblPoby").Visible = (false);
                e.Item.FindControl("LblPoffy").Visible = (false);
                e.Item.FindControl("LblIstasyongelisy").Visible = (false);

                e.Item.FindControl("LblIstasyoncikisetay").Visible = (false);
                e.Item.FindControl("LblPobetay").Visible = (false);
                e.Item.FindControl("LblPoffetay").Visible = (false);
                e.Item.FindControl("LblIstasyongelisetay").Visible = (false);

                if (puan == "1")
                {
                    e.Item.FindControl("LblIstasyoncikisetay").Visible = (true);
                    e.Item.FindControl("LblPobetay").Visible = (true);
                    e.Item.FindControl("LblPoffetay").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);
                }

                else if (puan == "2")
                {
                    e.Item.FindControl("LblIstasyoncikisy").Visible = (true);
                    e.Item.FindControl("LblPobetay").Visible = (true);
                    e.Item.FindControl("LblPoffetay").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);

                }

                else if (puan == "3")
                {
                    e.Item.FindControl("LblIstasyoncikisy").Visible = (true);
                    e.Item.FindControl("LblPoby").Visible = (true);
                    e.Item.FindControl("LblPoffetay").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);
                }

                else if (puan == "4")
                {
                    e.Item.FindControl("LblIstasyoncikisy").Visible = (true);
                    e.Item.FindControl("LblPoby").Visible = (true);
                    e.Item.FindControl("LblPoffy").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);
                }


            }
            else // giren opr ise
            {

                if (puan == "0")
                {
                    e.Item.FindControl("BtnProcessiptaly").Visible = (false);
                    e.Item.FindControl("BtnProcessChgStny").Visible = (true);
                    e.Item.FindControl("BtnProcessChgDesty").Visible = (false);

                    e.Item.FindControl("BtnPoby").Visible = (false);
                    e.Item.FindControl("BtnPoffy").Visible = (false);
                    e.Item.FindControl("BtnIstasyoncikisy").Visible = (false);
                    e.Item.FindControl("BtnIstasyongelisy").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikisy").Visible = (false);
                    e.Item.FindControl("LblPoby").Visible = (false);
                    e.Item.FindControl("LblPoffy").Visible = (false);
                    e.Item.FindControl("LblIstasyongelisy").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikisetay").Visible = (false);
                    e.Item.FindControl("LblPobetay").Visible = (false);
                    e.Item.FindControl("LblPoffetay").Visible = (false);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (false);

                }

                else if (puan == "1")
                {
                    e.Item.FindControl("BtnProcessiptaly").Visible = (true);
                    e.Item.FindControl("BtnProcessChgStny").Visible = (false);
                    e.Item.FindControl("BtnProcessChgDesty").Visible = (false);

                    e.Item.FindControl("BtnIstasyoncikisy").Visible = (true);
                    e.Item.FindControl("BtnPoby").Visible = (false);
                    e.Item.FindControl("BtnPoffy").Visible = (false);
                    e.Item.FindControl("BtnIstasyongelisy").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikisetay").Visible = (false);
                    e.Item.FindControl("LblIstasyoncikisy").Visible = (false);
                    e.Item.FindControl("LblPobetay").Visible = (true);
                    e.Item.FindControl("LblPoby").Visible = (false);
                    e.Item.FindControl("LblPoffetay").Visible = (true);
                    e.Item.FindControl("LblPoffy").Visible = (false);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisy").Visible = (false);
                }

                else if (puan == "2")
                {
                    e.Item.FindControl("BtnProcessiptaly").Visible = (false);
                    e.Item.FindControl("BtnProcessChgStny").Visible = (false);
                    e.Item.FindControl("BtnProcessChgDesty").Visible = (true);

                    e.Item.FindControl("BtnIstasyoncikisy").Visible = (false);
                    e.Item.FindControl("BtnPoby").Visible = (true);
                    e.Item.FindControl("BtnPoffy").Visible = (false);
                    e.Item.FindControl("BtnIstasyongelisy").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikisetay").Visible = (false);
                    e.Item.FindControl("LblIstasyoncikisy").Visible = (true);
                    e.Item.FindControl("LblPobetay").Visible = (false);
                    e.Item.FindControl("LblPoby").Visible = (false);
                    e.Item.FindControl("LblPoffetay").Visible = (true);
                    e.Item.FindControl("LblPoffy").Visible = (false);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisy").Visible = (false);

                }

                else if (puan == "3")
                {
                    e.Item.FindControl("BtnProcessiptaly").Visible = (false);
                    e.Item.FindControl("BtnProcessChgStny").Visible = (false);
                    e.Item.FindControl("BtnProcessChgDesty").Visible = (true);

                    e.Item.FindControl("BtnIstasyoncikisy").Visible = (false);
                    e.Item.FindControl("BtnPoby").Visible = (false);
                    e.Item.FindControl("BtnPoffy").Visible = (true);
                    e.Item.FindControl("BtnIstasyongelisy").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikisetay").Visible = (false);
                    e.Item.FindControl("LblIstasyoncikisy").Visible = (true);
                    e.Item.FindControl("LblPobetay").Visible = (false);
                    e.Item.FindControl("LblPoby").Visible = (true);
                    e.Item.FindControl("LblPoffetay").Visible = (false);
                    e.Item.FindControl("LblPoffy").Visible = (false);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisy").Visible = (false);
                }

                else if (puan == "4")
                {
                    if (Lblgemiadikop.Text.ToLower() == "takviye")   //takviyede takviye yolda iptali mümkün
                    {
                        e.Item.FindControl("BtnProcessiptaly").Visible = (true);
                        e.Item.FindControl("BtnProcessChgStny").Visible = (false);
                    }
                    else
                    {
                        e.Item.FindControl("BtnProcessiptaly").Visible = (false);
                        e.Item.FindControl("BtnProcessChgStny").Visible = (true);
                    }

                    e.Item.FindControl("BtnProcessChgDesty").Visible = (false);

                    e.Item.FindControl("BtnIstasyoncikisy").Visible = (false);
                    e.Item.FindControl("BtnPoby").Visible = (false);
                    e.Item.FindControl("BtnPoffy").Visible = (false);
                    e.Item.FindControl("BtnIstasyongelisy").Visible = (true);

                    e.Item.FindControl("LblIstasyoncikisetay").Visible = (false);
                    e.Item.FindControl("LblIstasyoncikisy").Visible = (true);
                    e.Item.FindControl("LblPobetay").Visible = (false);
                    e.Item.FindControl("LblPoby").Visible = (true);
                    e.Item.FindControl("LblPoffetay").Visible = (false);
                    e.Item.FindControl("LblPoffy").Visible = (true);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (false);
                    e.Item.FindControl("LblIstasyongelisy").Visible = (false);
                }

                else
                {
                    e.Item.FindControl("BtnProcessiptaly").Visible = (false);
                    e.Item.FindControl("BtnProcessChgStny").Visible = (false);
                    e.Item.FindControl("BtnProcessChgDesty").Visible = (false);

                    e.Item.FindControl("BtnPoby").Visible = (false);
                    e.Item.FindControl("BtnPoffy").Visible = (false);
                    e.Item.FindControl("BtnIstasyoncikisy").Visible = (false);
                    e.Item.FindControl("BtnIstasyongelisy").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikisy").Visible = (false);
                    e.Item.FindControl("LblPoby").Visible = (false);
                    e.Item.FindControl("LblPoffy").Visible = (false);
                    e.Item.FindControl("LblIstasyongelisy").Visible = (false);

                    e.Item.FindControl("LblIstasyoncikisetay").Visible = (false);
                    e.Item.FindControl("LblPobetay").Visible = (false);
                    e.Item.FindControl("LblPoffetay").Visible = (false);
                    e.Item.FindControl("LblIstasyongelisetay").Visible = (false);

                }
            }
            //cs tuşu 3 saat sonra gizleniyor 
            if (puan != "4")
            {
                if (Convert.ToDateTime(varbaslar.Text).AddHours(3) < (DateTime.Now))
                {
                    e.Item.FindControl("BtnProcessChgStny").Visible = (false);
                }
            }

            if (puan == "0")
            {
                Label LblNokop = (Label)e.Item.FindControl("LblNoy");
                LblNokop.Style.Add("color", "#ee1111");
                LblNokop.Style.Add("font-weight", "bold");
            }


            // DLYarimcaitemsay = DLDaricay.Items.Count.ToString(); 

            // port plan için yelkenkaya bold degil
            LinkButton Lblportcolorb = (LinkButton)e.Item.FindControl("binisport");
            if (Lblportcolorb.Text.ToLower() == "yelkenkaya")
            {
                Lblportcolorb.Style.Add("font-weight", "normal");
                Lblportcolorb.Style.Add("cursor", "default");
            }
            LinkButton Lblportcolori = (LinkButton)e.Item.FindControl("inisport");
            if (Lblportcolori.Text.ToLower() == "yelkenkaya")
            {
                Lblportcolori.Style.Add("font-weight", "normal");
                Lblportcolori.Style.Add("cursor", "default");
            }
        }
        baglanti.Close();
    }

    protected void DLDaricay_ItemCommand(object source, DataListCommandEventArgs e) // DURUM VE DATETIME GÜNCELLEME İLE DETAY KAYIT SONDA
    {

        SqlConnection baglanti = AnaKlas.baglan();
        LinkButton BtnProcessiptalkopya = (LinkButton)e.Item.FindControl("BtnProcessiptaly");
        String secilikapno = (BtnProcessiptalkopya.CommandArgument).ToString();

        SqlCommand cmdPilotDegisismial = new SqlCommand("SP_PilotDegismeciismial", baglanti);
        cmdPilotDegisismial.CommandType = CommandType.StoredProcedure;
        cmdPilotDegisismial.Parameters.AddWithValue("@secilikapno", secilikapno);
        string degismeciadisoyadi = "";
        SqlDataReader dr = cmdPilotDegisismial.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                degismeciadisoyadi = dr["degismeciadisoyadi"].ToString();
            }
        }
        dr.Close();
        cmdPilotDegisismial.Dispose();

        SqlCommand cmdPilotGemisiismial = new SqlCommand("SP_PilotGemisiismial", baglanti);
        cmdPilotGemisiismial.CommandType = CommandType.StoredProcedure;
        cmdPilotGemisiismial.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdPilotGemisiismial.Parameters.Add("@pilotgemisiismi", SqlDbType.Char, 60);
        cmdPilotGemisiismial.Parameters["@pilotgemisiismi"].Direction = ParameterDirection.Output;
        cmdPilotGemisiismial.ExecuteNonQuery();
        string shipadi = cmdPilotGemisiismial.Parameters["@pilotgemisiismi"].Value.ToString().Trim();
        cmdPilotGemisiismial.Dispose();

        string islemzamani = TarihSaatYaziYapDMYhm(DateTime.Now);
        Lblprocesstime.Text = islemzamani;
        Lblprocesstime.BorderColor = System.Drawing.Color.Gray;
        Lblprocesstime.BorderWidth = 1;

        DDCjob.Visible = false;
        DDCjob.Items.Clear();
        DDCjob.Items.Add("No");
        DDCjob.Items.Add("Yes");
        DDCjob.SelectedItem.Selected = false;
        DDCjob.Items.FindByText("No").Selected = true;
        LtCjobText.Visible = false;
        DDCjobReason.Visible = false;

        if (e.CommandName == "Istasyoncikis")
        {
            BacpCSok.CommandName = "istcik";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/> Off-Station </span>";
            this.ModalPopupCSonayMessage.Show();
        }
        else if (e.CommandName == "Gemiyebinis")
        {
            BacpCSok.CommandName = "pobbin";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/> POB </span>";
            this.ModalPopupCSonayMessage.Show();
        }
        else if (e.CommandName == "Gemideninis")
        {
            //string calsaat1 = "";
            //string calsaat2 = "";
            //string destinat = "";

            //SqlCommand cmdpilotokufull = new SqlCommand("SP_PilotOkuFull_fmKapno", baglanti);
            //cmdpilotokufull.CommandType = CommandType.StoredProcedure;
            //cmdpilotokufull.Parameters.AddWithValue("@secilikapno", secilikapno);
            //SqlDataReader readpilotfull = cmdpilotokufull.ExecuteReader();
            //if (readpilotfull.HasRows)
            //{
            //    while (readpilotfull.Read())
            //    {
            //        calsaat1 = readpilotfull["calsaat1"].ToString();
            //        calsaat2 = readpilotfull["calsaat2"].ToString();
            //        destinat = readpilotfull["inisyeri"].ToString();
            //    }
            //}
            //readpilotfull.Close();
            //cmdpilotokufull.Dispose();


            //if (calsaat1 == "" || calsaat2 == "")
            //{
            //    if (destinat.Substring(0, 5) != "Demir")
            //    {
            //        TBJEgrt.Enabled = true;
            //        TBJEetadt.Enabled = true;
            //    }
            //    jurnotfill(secilikapno, baglanti);
            //    this.ModalPopupjurnot.Show();

            //}
            //else
            //{ 
            BacpCSok.CommandName = "poffin";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/> POFF </span>";
            this.ModalPopupCSonayMessage.Show();

        }

        else if (e.CommandName == "ProcessChgStn")
        {
            BacpCSok.CommandName = "ChgStn";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Lblprocesstime.Enabled = false;

            SqlCommand cmdRespistal = new SqlCommand("SP_ResponsibleStation", baglanti);
            cmdRespistal.CommandType = CommandType.StoredProcedure;
            cmdRespistal.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdRespistal.Parameters.Add("@sonuc", SqlDbType.VarChar, 1);
            cmdRespistal.Parameters["@sonuc"].Direction = ParameterDirection.Output;
            cmdRespistal.ExecuteNonQuery();
            string reskontrol = cmdRespistal.Parameters["@sonuc"].Value.ToString().Trim();
            cmdRespistal.Dispose();


            if (reskontrol == "1")
            {
                Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/>  Change Station </span>";
            }
            else
            {
                Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/>  Change Station </span>";
            }

            this.ModalPopupCSonayMessage.Show();

        }

        else if (e.CommandName == "BtnProcessChgDest")
        {
            SqlCommand cmdPilotGemisiVarisyeri = new SqlCommand("SP_PilotGemisiVarisyeri", baglanti);
            cmdPilotGemisiVarisyeri.CommandType = CommandType.StoredProcedure;
            cmdPilotGemisiVarisyeri.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdPilotGemisiVarisyeri.Parameters.Add("@pilotvarisyeri", SqlDbType.VarChar, 20);
            cmdPilotGemisiVarisyeri.Parameters["@pilotvarisyeri"].Direction = ParameterDirection.Output;
            cmdPilotGemisiVarisyeri.ExecuteNonQuery();
            string kayitliliman = cmdPilotGemisiVarisyeri.Parameters["@pilotvarisyeri"].Value.ToString().Trim();
            cmdPilotGemisiVarisyeri.Dispose();

            SqlCommand cmdPilotGemisiVarisrihtimi = new SqlCommand("SP_PilotGemisiVarisRihtimi", baglanti);
            cmdPilotGemisiVarisrihtimi.CommandType = CommandType.StoredProcedure;
            cmdPilotGemisiVarisrihtimi.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdPilotGemisiVarisrihtimi.Parameters.Add("@pilotvarisrihtimi", SqlDbType.VarChar, 16);
            cmdPilotGemisiVarisrihtimi.Parameters["@pilotvarisrihtimi"].Direction = ParameterDirection.Output;
            cmdPilotGemisiVarisrihtimi.ExecuteNonQuery();
            string kayitlirihtim = cmdPilotGemisiVarisrihtimi.Parameters["@pilotvarisrihtimi"].Value.ToString().Trim();
            cmdPilotGemisiVarisrihtimi.Dispose();

            BacpCSokcd.CommandArgument = secilikapno;
            Litmodstnmescd.Text = "New Destination of " + shipadi.ToUpper();


            SqlCommand cmdDDLlim = new SqlCommand("SP_DDLlimanal_notorder", baglanti);
            cmdDDLlim.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmdDDLlim;
            DataSet ds = new DataSet();
            adapter.Fill(ds, "limanlar");
            DDLdesplace.Items.Clear();
            DDLdesplace.DataValueField = "limanno";
            DDLdesplace.DataTextField = "limanadi";
            DDLdesplace.DataSource = ds;
            DDLdesplace.DataBind();
            DDLdesplace.ClearSelection();
            cmdDDLlim.Dispose();

            ListItem cdp = DDLdesplace.Items.FindByText(kayitliliman);
            if (cdp != null)
            {
                DDLdesplace.Items.FindByText(kayitliliman).Selected = true;
            }


            SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
            cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
            cmdDDLrihtim.Parameters.AddWithValue("@limanadi", kayitliliman);
            SqlDataAdapter adapterr = new SqlDataAdapter();
            adapterr.SelectCommand = cmdDDLrihtim;
            DataSet dsr = new DataSet();
            adapterr.Fill(dsr, "limanlarr");
            DDLdesplaceno.Items.Clear();
            DDLdesplaceno.DataValueField = "id";
            DDLdesplaceno.DataTextField = "rihtimadi";
            DDLdesplaceno.DataSource = dsr;
            DDLdesplaceno.DataBind();
            DDLdesplaceno.ClearSelection();
            cmdDDLrihtim.Dispose();

            if (kayitlirihtim == "0")
            {
                DDLdesplaceno.Visible = false;
            }

            ListItem cdpn = DDLdesplaceno.Items.FindByText(kayitlirihtim);
            if (cdpn != null)
            {
                DDLdesplaceno.Items.FindByText(kayitlirihtim).Selected = true;
            }

            this.MPCDonay.Show();
        }

        else if (e.CommandName == "Istasyongelis")
        {
            BacpCSok.CommandName = "istgir";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/> On-Station </span>";

            DDCjob.Visible = true;
            DDCjob.Items.Clear();
            DDCjob.Items.Add("No");
            DDCjob.Items.Add("Yes");
            DDCjob.SelectedItem.Selected = false;
            DDCjob.Items.FindByText("No").Selected = true;

            LtCjobText.Visible = true;
            DDCjobReason.Visible = false;

            this.ModalPopupCSonayMessage.Show();
        }
        else if (e.CommandName == "Processiptal")
        {
            BacpCSok.CommandName = "isiptal";
            BacpCSok.CommandArgument = secilikapno.ToString();
            Litmodstnmes.Text = "<span style='font-size:18px; color:red;'>" + degismeciadisoyadi.ToUpper() + " <br/> Cancel Job </span>";
            this.ModalPopupCSonayMessage.Show();
        }

        else if (e.CommandName == "jurnot")
        {

            jurnotfill(secilikapno, baglanti);

            //byd bak
            SqlCommand cmdPilotGemisijoblist = new SqlCommand("SP_PilotGemisijoblist", baglanti);
            cmdPilotGemisijoblist.CommandType = CommandType.StoredProcedure;
            cmdPilotGemisijoblist.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdPilotGemisijoblist.Parameters.Add("@gemijoblistid", SqlDbType.Int);
            cmdPilotGemisijoblist.Parameters["@gemijoblistid"].Direction = ParameterDirection.Output;
            cmdPilotGemisijoblist.ExecuteNonQuery();
            string gemijoblistid = cmdPilotGemisijoblist.Parameters["@gemijoblistid"].Value.ToString().Trim();
            cmdPilotGemisijoblist.Dispose();

            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(gemijoblistid));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            string lybyol = lybbak.Parameters["@lybyol"].Value.ToString().Trim();
            lybbak.Dispose();

            Bshowbyd.Enabled = false;
            Bshowbyd.CommandName = "";
            Bshowbyd.ForeColor = System.Drawing.Color.Gray;
            if (string.IsNullOrEmpty(lybyol) != true)
            {
                Bshowbyd.Enabled = true;
                Bshowbyd.ForeColor = System.Drawing.Color.Blue;
                Bshowbyd.CommandName = gemijoblistid;
            }
            //byd biter


            this.ModalPopupjurnot.Show();



        }

        String Btnbinisportadi = "Yelkenkaya";
        String Btninisportadi = "Yelkenkaya";

        LinkButton Btnbinisport = (LinkButton)e.Item.FindControl("binisport");
        if (string.IsNullOrEmpty(Btnbinisport.Text) != true)
        {
            Btnbinisportadi = (Btnbinisport.Text).ToString();
        }

        LinkButton Btninisport = (LinkButton)e.Item.FindControl("inisport");
        if (string.IsNullOrEmpty(Btninisport.Text) != true)
        {
            Btninisportadi = (Btninisport.Text).ToString();
        }

        if (e.CommandName == "linkleb")
        {

            if (dogrulinkyap(Btnbinisportadi.ToLower()) != "yelkenkaya")
            {
                Buttonlbaddnotes.CommandName = Btnbinisportadi;
                Image1.ImageUrl = "../images/limanplan/" + dogrulinkyap(Btnbinisportadi.ToLower()) + ".jpg";
                Image1.Width = 747;
                this.MPEdrawing.Show();
            }
            //Response.Redirect(maplink);
            //< asp:image runat = "server" src = "images/mapicon.png" />
        }
        if (e.CommandName == "linklei")
        {

            if (dogrulinkyap(Btninisportadi.ToLower()) != "yelkenkaya")
            {
                Buttonlbaddnotes.CommandName = Btninisportadi;
                Image1.ImageUrl = "../images/limanplan/" + dogrulinkyap(Btninisportadi.ToLower()) + ".jpg";
                Image1.Width = 747;
                this.MPEdrawing.Show();
            }
            //Response.Redirect(maplink);
            //< asp:image runat = "server" src = "images/mapicon.png" />
        }



        baglanti.Close();

    }

    protected void Buttonlbaddcancel_Click(object sender, EventArgs e)
    {
        MPEdrawing.Controls.Clear();
        MPEdrawing2.Controls.Clear();


    }
    protected void Buttonlbaddnotes_Click(object sender, EventArgs e)
    {
        MPEdrawing.Controls.Clear();

        string yetkili = "";
        string telno = "";
        string cepno = "";
        string uyari = "";
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdisokuup = new SqlCommand("SP_LimanBilgilerSec", baglanti);
        cmdisokuup.CommandType = CommandType.StoredProcedure;
        cmdisokuup.Parameters.AddWithValue("@limanadi", Buttonlbaddnotes.CommandName);
        SqlDataReader dr = cmdisokuup.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                yetkili = dr["yetkili"].ToString();
                telno = dr["telno"].ToString();
                cepno = dr["cepno"].ToString();
                uyari = dr["uyari"].ToString();
            }
        }
        dr.Close();
        cmdisokuup.Dispose();
        baglanti.Close();

        Lblnote0.Text = Buttonlbaddnotes.CommandName;
        Lblnote1.Text = yetkili;
        Lblnote2.Text = telno;
        Lblnote3.Text = cepno;
        Lblnote4.Text = uyari;

        this.MPEdrawing2.Show();
    }
    protected void Buttonlbaddplan_Click(object sender, EventArgs e)
    {

        MPEdrawing2.Controls.Clear();
        this.MPEdrawing.Show();
    }

    public string dogrulinkyap(string Metin)
    {
        string dogruad = Metin;
        dogruad = Temizle(dogruad);
        dogruad = TemizleINGoldu(dogruad);

        if (dogruad == "topcularferibot" || dogruad == "yalovaroro") { dogruad = "topcuroro"; }
        else if (dogruad == "tuprasfaz1") { dogruad = "tuprasf1"; }
        else if (dogruad == "tuprasfaz2") { dogruad = "tuprasf2"; }
        else if (dogruad == "tuprasfaz3") { dogruad = "tuprasf3"; }
        else if (dogruad == "tuprasplatform") { dogruad = "tuprasplt"; }
        else if (dogruad == "safiport") { dogruad = "safi"; }
        else if (dogruad == "opay") { dogruad = "opayopet"; }
        else if (dogruad == "petline") { dogruad = "camar"; }
        else if (dogruad == "gubretas" || dogruad == "turkuaz" || dogruad == "marmaratersanesi" || dogruad == "rota") { dogruad = "gubreturkirrota"; }
        else if (dogruad == "shell" || dogruad == "koruma" || dogruad == "aktas") { dogruad = "shelkorumaaktas"; }
        else if (dogruad == "efesan" || dogruad == "total") { dogruad = "efesantotal"; }
        else if (dogruad.Substring(0, 3) == "kos") { dogruad = "kosbas"; }



        return dogruad;

    }
    public string Temizle(string Metin)
    {
        string deger = Metin;
        deger = deger.Replace(" ", "");
        deger = deger.Replace("'", "");
        deger = deger.Replace("<", "");
        deger = deger.Replace(">", "");
        deger = deger.Replace("&", "");
        deger = deger.Replace("(", "");
        deger = deger.Replace(")", "");
        deger = deger.Replace(";", "");
        deger = deger.Replace("?", "");
        deger = deger.Replace("-", "");
        deger = deger.Replace(".", "");
        deger = deger.Replace("_", "");
        deger = deger.Replace(":", "");

        return deger;
    }
    public string TemizleINGoldu(string Metin)
    {
        string deger = Metin;

        deger = deger.Replace("ç", "c");
        deger = deger.Replace("ğ", "g");
        deger = deger.Replace("ı", "i");
        deger = deger.Replace("ö", "o");
        deger = deger.Replace("ş", "s");
        deger = deger.Replace("ü", "u");

        deger = deger.Trim();
        return deger;
    }


    protected void BacpCSok_Click(object sender, EventArgs e)
    {

        SqlConnection baglanti = AnaKlas.baglan();

        //hatalı satırlar sil        
        SqlCommand cmd = new SqlCommand("delete from vardiyadetay where imono=0", baglanti);
        cmd.Parameters.AddWithValue("imono", 0);
        cmd.ExecuteNonQuery();

        int secilikapno = Convert.ToInt32(BacpCSok.CommandArgument.ToString());
        string islemzamani = Lblprocesstime.Text;
        bool tarihcek = KayitTarihCek(Lblprocesstime.Text);

        SqlCommand cmdPilotGemisiismial = new SqlCommand("SP_PilotGemisiismial", baglanti);
        cmdPilotGemisiismial.CommandType = CommandType.StoredProcedure;
        cmdPilotGemisiismial.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdPilotGemisiismial.Parameters.Add("@pilotgemisiismi", SqlDbType.Char, 30);
        cmdPilotGemisiismial.Parameters["@pilotgemisiismi"].Direction = ParameterDirection.Output;
        cmdPilotGemisiismial.ExecuteNonQuery();
        string shipadi = cmdPilotGemisiismial.Parameters["@pilotgemisiismi"].Value.ToString().Trim();
        cmdPilotGemisiismial.Dispose();

        SqlCommand cmdPilotGemisiimoal = new SqlCommand("SP_PilotGemisiimoal", baglanti);
        cmdPilotGemisiimoal.CommandType = CommandType.StoredProcedure;
        cmdPilotGemisiimoal.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdPilotGemisiimoal.Parameters.Add("@imono", SqlDbType.Int);
        cmdPilotGemisiimoal.Parameters["@imono"].Direction = ParameterDirection.Output;
        cmdPilotGemisiimoal.ExecuteNonQuery();
        string shipadimos = cmdPilotGemisiimoal.Parameters["@imono"].Value.ToString();
        cmdPilotGemisiimoal.Dispose();

        int shipadimo = 8888888;
        if (string.IsNullOrEmpty(shipadimos) != true)
        { shipadimo = Convert.ToInt32(shipadimos); }



        //pilot hangi vardiyaya aitse varno bul gecen süre seçiliyor
        SqlCommand cmdvarbilgivarnookue = new SqlCommand("SP_varvarnooku", baglanti);
        cmdvarbilgivarnookue.CommandType = CommandType.StoredProcedure;
        cmdvarbilgivarnookue.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdvarbilgivarnookue.Parameters.Add("@varnooku", SqlDbType.Char, 6);
        cmdvarbilgivarnookue.Parameters["@varnooku"].Direction = ParameterDirection.Output;
        cmdvarbilgivarnookue.ExecuteNonQuery();
        string varbilvarnoe = cmdvarbilgivarnookue.Parameters["@varnooku"].Value.ToString().Trim();
        cmdvarbilgivarnookue.Dispose();


        decimal gecenzaman = 0;


        if (BacpCSok.CommandName.ToString() == "ChgStn")
        {


            SqlCommand cmdRespistal = new SqlCommand("SP_ResponsibleStation", baglanti);
            cmdRespistal.CommandType = CommandType.StoredProcedure;
            cmdRespistal.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdRespistal.Parameters.Add("@sonuc", SqlDbType.VarChar, 1);
            cmdRespistal.Parameters["@sonuc"].Direction = ParameterDirection.Output;
            cmdRespistal.ExecuteNonQuery();
            string reskontrol = cmdRespistal.Parameters["@sonuc"].Value.ToString().Trim();
            cmdRespistal.Dispose();

            if (reskontrol == "1") { reskontrol = "2"; }
            else { reskontrol = "1"; }

            SqlCommand cmdRespistup = new SqlCommand("SP_UpRespistFmKapno", baglanti);
            cmdRespistup.CommandType = CommandType.StoredProcedure;
            cmdRespistup.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdRespistup.Parameters.AddWithValue("@reskontrol", reskontrol);
            cmdRespistup.ExecuteNonQuery();
            cmdRespistup.Dispose();

            if (shipadi.Length > 0 && shipadi.ToLower() != "takviye")
            {
                //değişen zamanlar update ediliyor
                SqlCommand cmdsonistkontrol = new SqlCommand("SP_ResponsibleStation", baglanti);
                cmdsonistkontrol.CommandType = CommandType.StoredProcedure;
                cmdsonistkontrol.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdsonistkontrol.Parameters.Add("@sonuc", SqlDbType.VarChar, 1);
                cmdsonistkontrol.Parameters["@sonuc"].Direction = ParameterDirection.Output;
                cmdsonistkontrol.ExecuteNonQuery();
                string ressonistkontrol = cmdsonistkontrol.Parameters["@sonuc"].Value.ToString().Trim();
                cmdsonistkontrol.Dispose();

                SqlCommand cmdpoffreal = new SqlCommand("SP_PilotGemisi_POff", baglanti);
                cmdpoffreal.CommandType = CommandType.StoredProcedure;
                cmdpoffreal.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdpoffreal.Parameters.Add("@realpoff", SqlDbType.VarChar, 16);
                cmdpoffreal.Parameters["@realpoff"].Direction = ParameterDirection.Output;
                cmdpoffreal.ExecuteNonQuery();
                string poffreal = cmdpoffreal.Parameters["@realpoff"].Value.ToString().Trim();
                cmdpoffreal.Dispose();

                DateTime poffd = Convert.ToDateTime(poffreal);
                int timeInisToDar = pilotzamanhesapla(secilikapno)[3];
                int timeInisToYar = pilotzamanhesapla(secilikapno)[4];

                string istasyongelis = "";

                if (ressonistkontrol == "1")
                {
                    istasyongelis = TarihSaatYaziYapDMYhm(poffd.AddMinutes(timeInisToDar));
                }
                else if (ressonistkontrol == "2")
                {
                    istasyongelis = TarihSaatYaziYapDMYhm(poffd.AddMinutes(timeInisToYar));
                }

                SqlCommand cmdistgelup = new SqlCommand("SP_UpIstasyongelisFmKapno", baglanti);
                cmdistgelup.CommandType = CommandType.StoredProcedure;
                cmdistgelup.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdistgelup.Parameters.AddWithValue("@istasyongelis", istasyongelis);
                cmdistgelup.ExecuteNonQuery();
                cmdistgelup.Dispose();

            }

        }

        else if (BacpCSok.CommandName.ToString() == "isiptal")
        {
            //songeliş alınıyor

            string sonistasyongelis = varbaslar.Text;
            string idsecim = "0";
            SqlCommand cmdlast24 = new SqlCommand("SP_DTDaricaYarimcaCanliGecmisSonGelis", baglanti);
            cmdlast24.CommandType = CommandType.StoredProcedure;
            cmdlast24.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdlast24.Parameters.AddWithValue("@varbilvarno", varbilvarnoe);
            SqlDataReader varisreaderupher = cmdlast24.ExecuteReader();
            if (varisreaderupher.HasRows)
            {
                while (varisreaderupher.Read())
                {
                    idsecim = varisreaderupher["id"].ToString();
                    sonistasyongelis = varisreaderupher["istasyongelis"].ToString();
                }
            }
            varisreaderupher.Close();

            decimal last7 = 0;
            TimeSpan tslast7 = DateTime.Now - Convert.ToDateTime(sonistasyongelis);
            last7 = Convert.ToDecimal(tslast7.TotalHours);
            last7 = Math.Round(last7, 2);
            if (last7 > 24) { last7 = Convert.ToDecimal("23"); }
            int sevensay = 0;
            if (last7 > 8) { sevensay = Convert.ToInt32(last7 * 100); }
            else if (last7 < 0) { last7 = 0; }

            //last7 ve sevensay eskihaline cevriliyor
            SqlCommand cmdvardup = new SqlCommand("SP_Pilotvardiya_Uplast7say", baglanti);
            cmdvardup.CommandType = CommandType.StoredProcedure;
            cmdvardup.Parameters.AddWithValue("@idsecim", idsecim);
            cmdvardup.Parameters.AddWithValue("@lastseven", last7);
            cmdvardup.Parameters.AddWithValue("@sevensayi", sevensay);
            cmdvardup.ExecuteNonQuery();
            cmdvardup.Dispose();

            //gemi nedurumda eski haline çevriliyor
            if (shipadi.ToLower().Trim() != "takviye")
            {
                SqlCommand cmdistipup = new SqlCommand("SP_UpPilotlarIsiptalFmKapno", baglanti);
                cmdistipup.CommandType = CommandType.StoredProcedure;
                cmdistipup.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdistipup.Parameters.AddWithValue("@durum", "0");
                cmdistipup.Parameters.AddWithValue("@imono", 0);
                cmdistipup.Parameters.AddWithValue("@gemiadi", "");
                cmdistipup.Parameters.AddWithValue("@bayrak", "");
                cmdistipup.Parameters.AddWithValue("@grt", "");
                cmdistipup.Parameters.AddWithValue("@tip", "");
                cmdistipup.Parameters.AddWithValue("@binisyeri", "");
                cmdistipup.Parameters.AddWithValue("@binisrihtim", "");
                cmdistipup.Parameters.AddWithValue("@inisyeri", "");
                cmdistipup.Parameters.AddWithValue("@inisrihtim", "");
                cmdistipup.Parameters.AddWithValue("@istasyoncikis", "");
                cmdistipup.Parameters.AddWithValue("@pob", "");
                cmdistipup.Parameters.AddWithValue("@poff", "");
                cmdistipup.Parameters.AddWithValue("@istasyongelis", sonistasyongelis);
                cmdistipup.Parameters.AddWithValue("@gemiatamazamani", "");
                cmdistipup.Parameters.AddWithValue("@jnot", "");
                cmdistipup.Parameters.AddWithValue("@jnotdaily", "");
                cmdistipup.Parameters.AddWithValue("@rom1", "");
                cmdistipup.Parameters.AddWithValue("@rom2", "");
                cmdistipup.Parameters.AddWithValue("@rom3", "");
                cmdistipup.Parameters.AddWithValue("@rom4", "");
                cmdistipup.Parameters.AddWithValue("@rom5", "");
                cmdistipup.Parameters.AddWithValue("@mboat", "");
                cmdistipup.Parameters.AddWithValue("@calsaat1", "");
                cmdistipup.Parameters.AddWithValue("@calsaat2", "");
                cmdistipup.Parameters.AddWithValue("@calsaat3", "");
                cmdistipup.Parameters.AddWithValue("@calsaat4", "");
                cmdistipup.Parameters.AddWithValue("@acente", "");
                cmdistipup.Parameters.AddWithValue("@oper", "");

                cmdistipup.ExecuteNonQuery();
                cmdistipup.Dispose();

                SqlCommand cmdisupbak = new SqlCommand("SP_Isliste_NedurumdaoprFmimo", baglanti);
                cmdisupbak.CommandType = CommandType.StoredProcedure;
                cmdisupbak.Parameters.AddWithValue("@imono", shipadimo);
                cmdisupbak.Parameters.Add("@nedurumdaopr", SqlDbType.Char, 1);
                cmdisupbak.Parameters["@nedurumdaopr"].Direction = ParameterDirection.Output;
                cmdisupbak.ExecuteNonQuery();
                string nedurumdaopr = cmdisupbak.Parameters["@nedurumdaopr"].Value.ToString().Trim();
                cmdisupbak.Dispose();

                SqlCommand cmdisnedup = new SqlCommand("SP_Up_Isliste_NedurumdaFmimo", baglanti);
                cmdisnedup.CommandType = CommandType.StoredProcedure;
                cmdisnedup.Parameters.AddWithValue("@imono", shipadimo);
                cmdisnedup.Parameters.AddWithValue("@nedurumda", nedurumdaopr);
                cmdisnedup.ExecuteNonQuery();
                cmdisnedup.Dispose();

                SqlCommand cmdisetbup = new SqlCommand("SP_Up_Isliste_EtbEtdFmimo", baglanti);
                cmdisetbup.CommandType = CommandType.StoredProcedure;
                cmdisetbup.Parameters.AddWithValue("@imono", shipadimo);
                cmdisetbup.Parameters.AddWithValue("@etb", "");
                cmdisetbup.Parameters.AddWithValue("@etd", "");
                cmdisetbup.ExecuteNonQuery();
                cmdisetbup.Dispose();

            }
            else
            {
                SqlCommand cmddurumbak = new SqlCommand("SP_PilotDurumBak", baglanti);
                cmddurumbak.CommandType = CommandType.StoredProcedure;
                cmddurumbak.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmddurumbak.Parameters.Add("@durum", SqlDbType.Char, 1);
                cmddurumbak.Parameters["@durum"].Direction = ParameterDirection.Output;
                cmddurumbak.ExecuteNonQuery();
                string durum = cmddurumbak.Parameters["@durum"].Value.ToString().Trim();
                cmddurumbak.Dispose();

                if (durum == "0") // takviye çıkmadan iptal edilmiş
                {
                    //SqlCommand cmdtaklogsil = new SqlCommand("SP_DEL_TakviyelogFmId", baglanti);
                    //cmdtaklogsil.CommandType = CommandType.StoredProcedure;
                    //cmdtaklogsil.ExecuteNonQuery();
                    //cmdtaklogsil.Dispose();
                }
                else if (durum == "4") // takviye çıkınca karşıdan iptal edilmiş
                {
                    SqlCommand cmdsonistkontrol = new SqlCommand("SP_ResponsibleStation", baglanti);
                    cmdsonistkontrol.CommandType = CommandType.StoredProcedure;
                    cmdsonistkontrol.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdsonistkontrol.Parameters.Add("@sonuc", SqlDbType.VarChar, 1);
                    cmdsonistkontrol.Parameters["@sonuc"].Direction = ParameterDirection.Output;
                    cmdsonistkontrol.ExecuteNonQuery();
                    string respistal = cmdsonistkontrol.Parameters["@sonuc"].Value.ToString().Trim();
                    cmdsonistkontrol.Dispose();

                    if (respistal == "1") { respistal = "2"; }
                    else { respistal = "1"; }

                    SqlCommand cmdistciktakup = new SqlCommand("SP_UpIstasyoncikisFmKapno", baglanti);
                    cmdistciktakup.CommandType = CommandType.StoredProcedure;
                    cmdistciktakup.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdistciktakup.Parameters.AddWithValue("@durum", "0");
                    cmdistciktakup.Parameters.AddWithValue("@istasyoncikis", "");
                    cmdistciktakup.Parameters.AddWithValue("@istasyongelis", sonistasyongelis);
                    cmdistciktakup.Parameters.AddWithValue("@respist", respistal);
                    cmdistciktakup.Parameters.AddWithValue("@Pob", "");
                    cmdistciktakup.Parameters.AddWithValue("@Poff", "");
                    cmdistciktakup.ExecuteNonQuery();
                    cmdistciktakup.Dispose();
                }

                SqlCommand cmdistipup = new SqlCommand("SP_UpPilotlarIsiptalFmKapno", baglanti);
                cmdistipup.CommandType = CommandType.StoredProcedure;
                cmdistipup.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdistipup.Parameters.AddWithValue("@durum", "0");
                cmdistipup.Parameters.AddWithValue("@imono", 0);
                cmdistipup.Parameters.AddWithValue("@gemiadi", "");
                cmdistipup.Parameters.AddWithValue("@bayrak", "");
                cmdistipup.Parameters.AddWithValue("@grt", "");
                cmdistipup.Parameters.AddWithValue("@tip", "");
                cmdistipup.Parameters.AddWithValue("@binisyeri", "");
                cmdistipup.Parameters.AddWithValue("@binisrihtim", "");
                cmdistipup.Parameters.AddWithValue("@inisyeri", "");
                cmdistipup.Parameters.AddWithValue("@inisrihtim", "");
                cmdistipup.Parameters.AddWithValue("@istasyoncikis", "");
                cmdistipup.Parameters.AddWithValue("@pob", "");
                cmdistipup.Parameters.AddWithValue("@poff", "");
                cmdistipup.Parameters.AddWithValue("@istasyongelis", sonistasyongelis);
                cmdistipup.Parameters.AddWithValue("@gemiatamazamani", "");
                cmdistipup.Parameters.AddWithValue("@jnot", "");
                cmdistipup.Parameters.AddWithValue("@jnotdaily", "");
                cmdistipup.Parameters.AddWithValue("@rom1", "");
                cmdistipup.Parameters.AddWithValue("@rom2", "");
                cmdistipup.Parameters.AddWithValue("@rom3", "");
                cmdistipup.Parameters.AddWithValue("@rom4", "");
                cmdistipup.Parameters.AddWithValue("@rom5", "");
                cmdistipup.Parameters.AddWithValue("@mboat", "");
                cmdistipup.Parameters.AddWithValue("@calsaat1", "");
                cmdistipup.Parameters.AddWithValue("@calsaat2", "");
                cmdistipup.Parameters.AddWithValue("@calsaat3", "");
                cmdistipup.Parameters.AddWithValue("@calsaat4", "");
                cmdistipup.Parameters.AddWithValue("@acente", "");
                cmdistipup.Parameters.AddWithValue("@oper", "");

                cmdistipup.ExecuteNonQuery();
                cmdistipup.Dispose();

            }

            DTloading(baglanti);
        }


        else if (BacpCSok.CommandName.ToString() == "istcik")
        {
            if (tarihcek == false)
            {
                Lblprocesstime.BorderColor = System.Drawing.Color.Red;
                Lblprocesstime.BorderWidth = 1;
                this.ModalPopupCSonayMessage.Show();
            }
            else
            {
                if (shipadi.ToLower().Trim() == "takviye")
                {
                    SqlCommand cmdVarisBagliistasyon = new SqlCommand("SP_VarisBagliistasyon", baglanti);
                    cmdVarisBagliistasyon.CommandType = CommandType.StoredProcedure;
                    cmdVarisBagliistasyon.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdVarisBagliistasyon.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
                    cmdVarisBagliistasyon.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
                    cmdVarisBagliistasyon.ExecuteNonQuery();
                    string respistal = cmdVarisBagliistasyon.Parameters["@bagliistasyon"].Value.ToString().Trim();
                    cmdVarisBagliistasyon.Dispose();

                    SqlCommand cmdistciktakup = new SqlCommand("SP_UpIstasyoncikisFmKapno", baglanti);
                    cmdistciktakup.CommandType = CommandType.StoredProcedure;
                    cmdistciktakup.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdistciktakup.Parameters.AddWithValue("@durum", "4");
                    cmdistciktakup.Parameters.AddWithValue("@istasyoncikis", islemzamani);
                    cmdistciktakup.Parameters.AddWithValue("@istasyongelis", TarihSaatYaziYapDMYhm(DateTime.Now.AddMinutes(50)));
                    cmdistciktakup.Parameters.AddWithValue("@respist", respistal);
                    cmdistciktakup.Parameters.AddWithValue("@Pob", islemzamani);
                    cmdistciktakup.Parameters.AddWithValue("@Poff", TarihSaatYaziYapDMYhm(DateTime.Now.AddMinutes(50)));
                    cmdistciktakup.ExecuteNonQuery();
                    cmdistciktakup.Dispose();

                    //taklog güncelleme

                    SqlCommand cmdPilotDegisismial = new SqlCommand("SP_PilotDegismeciismial", baglanti);
                    cmdPilotDegisismial.CommandType = CommandType.StoredProcedure;
                    cmdPilotDegisismial.Parameters.AddWithValue("@secilikapno", secilikapno);
                    string degismeciadisoyadi = "";
                    SqlDataReader dr = cmdPilotDegisismial.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            degismeciadisoyadi = dr["degismeciadisoyadi"].ToString();
                        }
                    }
                    dr.Close();
                    cmdPilotDegisismial.ExecuteNonQuery();
                    cmdPilotDegisismial.Dispose();

                    SqlCommand takkimokucmd = new SqlCommand("Select kimler from takviyelog where id =(select max(id) from takviyelog) ", baglanti);
                    string takkimoku = takkimokucmd.ExecuteScalar().ToString();
                    takkimokucmd.Dispose();
                    if (takkimoku == "" || takkimoku == null || takkimoku == "NULL")
                    { takkimoku = degismeciadisoyadi; }
                    else
                    { takkimoku = takkimoku + ", " + degismeciadisoyadi; }

                    SqlCommand takcikisonay = new SqlCommand("update takviyelog set cikissaati=@cikissaati,kimler=@kimler,durum=@durum where id =(select max(id) from takviyelog)", baglanti);
                    takcikisonay.Parameters.AddWithValue("cikissaati", islemzamani);
                    takcikisonay.Parameters.AddWithValue("kimler", takkimoku);
                    takcikisonay.Parameters.AddWithValue("durum", "2");
                    takcikisonay.ExecuteNonQuery();
                    takcikisonay.Dispose();

                }
                else
                {
                    SqlCommand cmdistcikdurup = new SqlCommand("SP_UpDurumIstasyoncikisFmKapno", baglanti);
                    cmdistcikdurup.CommandType = CommandType.StoredProcedure;
                    cmdistcikdurup.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdistcikdurup.Parameters.AddWithValue("@durum", "2");
                    cmdistcikdurup.Parameters.AddWithValue("@istasyoncikis", islemzamani);
                    cmdistcikdurup.ExecuteNonQuery();
                    cmdistcikdurup.Dispose();
                }
            }
        }

        else if (BacpCSok.CommandName.ToString() == "pobbin")
        {
            if (tarihcek == false)
            {
                Lblprocesstime.BorderColor = System.Drawing.Color.Red;
                Lblprocesstime.BorderWidth = 1;
                this.ModalPopupCSonayMessage.Show();
            }
            else
            {
                SqlCommand cmdVarisBagliistasyon = new SqlCommand("SP_VarisBagliistasyon", baglanti);
                cmdVarisBagliistasyon.CommandType = CommandType.StoredProcedure;
                cmdVarisBagliistasyon.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdVarisBagliistasyon.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
                cmdVarisBagliistasyon.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
                cmdVarisBagliistasyon.ExecuteNonQuery();
                string istvar = cmdVarisBagliistasyon.Parameters["@bagliistasyon"].Value.ToString().Trim();
                cmdVarisBagliistasyon.Dispose();


                SqlCommand cmdKalkisBagliistasyon = new SqlCommand("SP_VKalkisBagliistasyon", baglanti);
                cmdKalkisBagliistasyon.CommandType = CommandType.StoredProcedure;
                cmdKalkisBagliistasyon.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdKalkisBagliistasyon.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
                cmdKalkisBagliistasyon.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
                cmdKalkisBagliistasyon.ExecuteNonQuery();
                string istcik = cmdKalkisBagliistasyon.Parameters["@bagliistasyon"].Value.ToString().Trim();
                cmdKalkisBagliistasyon.Dispose();

                SqlCommand cmddurpoprestup = new SqlCommand("SP_UpDurumpobrestFmKapno", baglanti);
                cmddurpoprestup.CommandType = CommandType.StoredProcedure;
                cmddurpoprestup.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmddurpoprestup.Parameters.AddWithValue("@durum", "3");
                cmddurpoprestup.Parameters.AddWithValue("@respist", istvar);
                cmddurpoprestup.Parameters.AddWithValue("@pob", islemzamani);
                cmddurpoprestup.ExecuteNonQuery();
                cmddurpoprestup.Dispose();


                //değişen zamanlar update ediliyor
                DateTime pobd = DateTime.Now;
                int timeDarToBinis = pilotzamanhesapla(secilikapno)[0];
                int timeYarToBinis = pilotzamanhesapla(secilikapno)[1];
                int timeBinisToInis = pilotzamanhesapla(secilikapno)[2];
                int timeInisToDar = pilotzamanhesapla(secilikapno)[3];
                int timeInisToYar = pilotzamanhesapla(secilikapno)[4];
                int timeYanasSure = pilotzamanhesapla(secilikapno)[5];
                int timeKalkSure = pilotzamanhesapla(secilikapno)[6];

                //string istasyoncikic = "";
                //string pob = "";
                string poff = "";
                string istasyongelis = "";

                if (istcik == "1")
                {
                    if (istvar == "1")
                    {
                        poff = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure));
                        istasyongelis = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure).AddMinutes(timeInisToDar));
                    }
                    else if (istvar == "2")
                    {
                        poff = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure));
                        istasyongelis = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure).AddMinutes(timeInisToYar));
                    }
                }
                else if (istcik == "2")
                {
                    if (istvar == "1")
                    {
                        poff = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure));
                        istasyongelis = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure).AddMinutes(timeInisToDar));

                    }
                    else if (istvar == "2")
                    {
                        poff = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure));
                        istasyongelis = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure).AddMinutes(timeInisToYar));
                    }
                }


                SqlCommand cmdpoffistgelup = new SqlCommand("SP_UpPoffistgelFmKapno", baglanti);
                cmdpoffistgelup.CommandType = CommandType.StoredProcedure;
                cmdpoffistgelup.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdpoffistgelup.Parameters.AddWithValue("@poff", poff);
                cmdpoffistgelup.Parameters.AddWithValue("@istasyongelis", istasyongelis);
                cmdpoffistgelup.ExecuteNonQuery();
                cmdpoffistgelup.Dispose();
            }
        }
        else if (BacpCSok.CommandName.ToString() == "poffin")
        {


            if (tarihcek == false)
            {
                Lblprocesstime.BorderColor = System.Drawing.Color.Red;
                Lblprocesstime.BorderWidth = 1;
                this.ModalPopupCSonayMessage.Show();
            }
            else
            {
                SqlCommand cmddurpoffup = new SqlCommand("SP_UpDurumpoffFmKapno", baglanti);
                cmddurpoffup.CommandType = CommandType.StoredProcedure;
                cmddurpoffup.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmddurpoffup.Parameters.AddWithValue("@durum", "4");
                cmddurpoffup.Parameters.AddWithValue("@poff", islemzamani);
                cmddurpoffup.ExecuteNonQuery();
                cmddurpoffup.Dispose();


                //İŞLİSTE GEMİ NEDURUMDA AYARLA

                SqlCommand cmdPilotGemisiVarisyeri = new SqlCommand("SP_PilotGemisiVarisyeri", baglanti);
                cmdPilotGemisiVarisyeri.CommandType = CommandType.StoredProcedure;
                cmdPilotGemisiVarisyeri.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdPilotGemisiVarisyeri.Parameters.Add("@pilotvarisyeri", SqlDbType.VarChar, 20);
                cmdPilotGemisiVarisyeri.Parameters["@pilotvarisyeri"].Direction = ParameterDirection.Output;
                cmdPilotGemisiVarisyeri.ExecuteNonQuery();
                string kayitliliman = cmdPilotGemisiVarisyeri.Parameters["@pilotvarisyeri"].Value.ToString().Trim();
                cmdPilotGemisiVarisyeri.Dispose();


                SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
                cmdLimannoal.CommandType = CommandType.StoredProcedure;
                cmdLimannoal.Parameters.AddWithValue("@limanadi", kayitliliman);
                cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int); // 
                cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
                cmdLimannoal.ExecuteNonQuery();
                int portno = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString().Trim());
                cmdLimannoal.Dispose();


                string eta = TarihSaatYaziYapDMYhm((DateTime.Now).AddHours(24));
                string kalkislimani = "Yelkenkaya";
                string kalkisrihtimi = "0";
                string yanasmalimani = "Yelkenkaya";
                string yanasmarihtimi = "0";

                SqlCommand cmdisokuup = new SqlCommand("SP_PilotGemisiKalkisVaris4lu", baglanti);
                cmdisokuup.CommandType = CommandType.StoredProcedure;
                cmdisokuup.Parameters.AddWithValue("@secilikapno", secilikapno);
                SqlDataReader varisreaderup = cmdisokuup.ExecuteReader();
                if (varisreaderup.HasRows)
                {
                    while (varisreaderup.Read())
                    {
                        kalkislimani = varisreaderup["binisyeri"].ToString();
                        kalkisrihtimi = varisreaderup["binisrihtim"].ToString();
                        yanasmalimani = varisreaderup["inisyeri"].ToString();
                        yanasmarihtimi = varisreaderup["inisrihtim"].ToString();
                    }
                }
                varisreaderup.Close();
                cmdisokuup.Dispose();

                if (portno > 0 && portno < 900) // limanda
                {
                    SqlCommand cmdisnedup = new SqlCommand("SP_Up_Isliste_LimandaFmimo", baglanti);
                    cmdisnedup.CommandType = CommandType.StoredProcedure;
                    cmdisnedup.Parameters.AddWithValue("@shipadimo", shipadimo);
                    cmdisnedup.Parameters.AddWithValue("@nedurumda", "2");
                    cmdisnedup.Parameters.AddWithValue("@nedurumdaopr", "2");
                    cmdisnedup.Parameters.AddWithValue("@eta", eta);
                    cmdisnedup.Parameters.AddWithValue("@kalkislimani", yanasmalimani);
                    cmdisnedup.Parameters.AddWithValue("@kalkisrihtimi", yanasmarihtimi);
                    cmdisnedup.Parameters.AddWithValue("@yanasmalimani", "Yelkenkaya");
                    cmdisnedup.Parameters.AddWithValue("@yanasmarihtimi", "0");
                    cmdisnedup.ExecuteNonQuery();
                    cmdisnedup.Dispose();
                }

                else if (portno > 1000 && portno < 1099) // demirde
                {
                    SqlCommand cmdisnedup = new SqlCommand("SP_Up_Isliste_DemirdeFmimo", baglanti);
                    cmdisnedup.CommandType = CommandType.StoredProcedure;
                    cmdisnedup.Parameters.AddWithValue("@shipadimo", shipadimo);
                    cmdisnedup.Parameters.AddWithValue("@nedurumda", "1");
                    cmdisnedup.Parameters.AddWithValue("@nedurumdaopr", "1");
                    cmdisnedup.Parameters.AddWithValue("@eta", eta);
                    cmdisnedup.Parameters.AddWithValue("@kalkislimani", yanasmalimani);
                    cmdisnedup.Parameters.AddWithValue("@kalkisrihtimi", yanasmarihtimi);
                    cmdisnedup.ExecuteNonQuery();
                    cmdisnedup.Dispose();
                }

                else if (portno == 998) // çıkışa gitmiş
                {
                    SqlCommand cmdisnedup = new SqlCommand("SP_Up_Isliste_CikistaFmimo", baglanti);
                    cmdisnedup.CommandType = CommandType.StoredProcedure;
                    cmdisnedup.Parameters.AddWithValue("@shipadimo", shipadimo);
                    cmdisnedup.Parameters.AddWithValue("@nedurumda", "9");
                    cmdisnedup.Parameters.AddWithValue("@nedurumdaopr", "9");
                    cmdisnedup.ExecuteNonQuery();
                    cmdisnedup.Dispose();

                    //ordino sil
                    SqlCommand orbak = new SqlCommand("SP_orbakimo", baglanti);
                    orbak.CommandType = CommandType.StoredProcedure;
                    orbak.Parameters.AddWithValue("@imono", Convert.ToInt32(shipadimo));
                    orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
                    orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
                    orbak.ExecuteNonQuery();
                    orbak.Dispose();

                    SqlCommand cmdor = new SqlCommand("SP_Up_Isliste_imo_or", baglanti);
                    cmdor.CommandType = CommandType.StoredProcedure;
                    cmdor.Parameters.AddWithValue("@imono", Convert.ToInt32(shipadimo));
                    cmdor.Parameters.AddWithValue("oryol", "");

                    cmdor.ExecuteNonQuery();
                    cmdor.Dispose();

                    FileInfo fisilor = new FileInfo(Server.MapPath(orbak.Parameters["@oryol"].Value.ToString()));
                    try
                    {
                        fisilor.Delete();
                    }
                    catch { }

                }

                else if (portno == 997) // deneme seyri
                {
                    SqlCommand cmdisnedup = new SqlCommand("SP_Up_Isliste_CikistaFmimo", baglanti);
                    cmdisnedup.CommandType = CommandType.StoredProcedure;
                    cmdisnedup.Parameters.AddWithValue("@shipadimo", shipadimo);
                    cmdisnedup.Parameters.AddWithValue("@nedurumda", "4");
                    cmdisnedup.Parameters.AddWithValue("@nedurumdaopr", "4");
                    cmdisnedup.ExecuteNonQuery();
                    cmdisnedup.Dispose();
                }

                //geminin etb etd si silinip update ediliyor
                SqlCommand cmdisetbup = new SqlCommand("SP_Up_Isliste_EtbEtdFmimo", baglanti);
                cmdisetbup.CommandType = CommandType.StoredProcedure;
                cmdisetbup.Parameters.AddWithValue("@imono", shipadimo);
                cmdisetbup.Parameters.AddWithValue("@etb", "");
                cmdisetbup.Parameters.AddWithValue("@etd", "");
                cmdisetbup.ExecuteNonQuery();
                cmdisetbup.Dispose();

                //değişen zamanlar update ediliyor

                SqlCommand cmdVarisBagliistasyon = new SqlCommand("SP_VarisBagliistasyon", baglanti);
                cmdVarisBagliistasyon.CommandType = CommandType.StoredProcedure;
                cmdVarisBagliistasyon.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdVarisBagliistasyon.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
                cmdVarisBagliistasyon.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
                cmdVarisBagliistasyon.ExecuteNonQuery();
                string istvar = cmdVarisBagliistasyon.Parameters["@bagliistasyon"].Value.ToString().Trim();
                cmdVarisBagliistasyon.Dispose();


                SqlCommand cmdKalkisBagliistasyon = new SqlCommand("SP_VKalkisBagliistasyon", baglanti);
                cmdKalkisBagliistasyon.CommandType = CommandType.StoredProcedure;
                cmdKalkisBagliistasyon.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdKalkisBagliistasyon.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
                cmdKalkisBagliistasyon.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
                cmdKalkisBagliistasyon.ExecuteNonQuery();
                string istcik = cmdKalkisBagliistasyon.Parameters["@bagliistasyon"].Value.ToString().Trim();
                cmdKalkisBagliistasyon.Dispose();

                SqlCommand cmdpoffreal = new SqlCommand("SP_PilotGemisi_POff", baglanti);
                cmdpoffreal.CommandType = CommandType.StoredProcedure;
                cmdpoffreal.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdpoffreal.Parameters.Add("@realpoff", SqlDbType.VarChar, 16);
                cmdpoffreal.Parameters["@realpoff"].Direction = ParameterDirection.Output;
                cmdpoffreal.ExecuteNonQuery();
                string poffreal = cmdpoffreal.Parameters["@realpoff"].Value.ToString().Trim();
                cmdpoffreal.Dispose();


                DateTime poffd = Convert.ToDateTime(poffreal);
                int timeInisToDar = pilotzamanhesapla(secilikapno)[3];
                int timeInisToYar = pilotzamanhesapla(secilikapno)[4];
                string istasyongelis = "";

                if (istcik == "1")
                {
                    if (istvar == "1")
                    {
                        istasyongelis = TarihSaatYaziYapDMYhm(poffd.AddMinutes(timeInisToDar));
                    }
                    else if (istvar == "2")
                    {
                        istasyongelis = TarihSaatYaziYapDMYhm(poffd.AddMinutes(timeInisToYar));
                    }
                }
                else if (istcik == "2")
                {
                    if (istvar == "1")
                    {
                        istasyongelis = TarihSaatYaziYapDMYhm(poffd.AddMinutes(timeInisToDar));

                    }
                    else if (istvar == "2")
                    {
                        istasyongelis = TarihSaatYaziYapDMYhm(poffd.AddMinutes(timeInisToYar));
                    }
                }

                SqlCommand cmdistgelup = new SqlCommand("SP_UpIstasyongelisFmKapno", baglanti);
                cmdistgelup.CommandType = CommandType.StoredProcedure;
                cmdistgelup.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdistgelup.Parameters.AddWithValue("@istasyongelis", istasyongelis);
                cmdistgelup.ExecuteNonQuery();
                cmdistgelup.Dispose();



                //lyb sil
                SqlCommand lybbak = new SqlCommand("SP_lybbakimo", baglanti);
                lybbak.CommandType = CommandType.StoredProcedure;
                lybbak.Parameters.AddWithValue("@imono", Convert.ToInt32(shipadimo));
                lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
                lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
                lybbak.ExecuteNonQuery();
                lybbak.Dispose();

                SqlCommand cmdlyb = new SqlCommand("SP_Up_Isliste_imo_lyb", baglanti);
                cmdlyb.CommandType = CommandType.StoredProcedure;
                cmdlyb.Parameters.AddWithValue("@imono", Convert.ToInt32(shipadimo));
                cmdlyb.Parameters.AddWithValue("lybyol", "");

                cmdlyb.ExecuteNonQuery();
                cmdlyb.Dispose();
                //DTLoading(baglanti);


                FileInfo fisil = new FileInfo(Server.MapPath(lybbak.Parameters["@lybyol"].Value.ToString()));
                try
                {
                    fisil.Delete();
                }
                catch { }


            }

        }

        else if (BacpCSok.CommandName.ToString() == "istgir")
        {
            if (tarihcek == false)
            {
                Lblprocesstime.BorderColor = System.Drawing.Color.Red;
                Lblprocesstime.BorderWidth = 1;
                this.ModalPopupCSonayMessage.Show();
            }
            else
            {
                if (shipadi.ToLower().Trim() == "takviye")
                {
                    SqlCommand cmdpoffistgelup = new SqlCommand("SP_UpPoffistgelFmKapno", baglanti);
                    cmdpoffistgelup.CommandType = CommandType.StoredProcedure;
                    cmdpoffistgelup.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdpoffistgelup.Parameters.AddWithValue("@istasyongelis", islemzamani);
                    cmdpoffistgelup.Parameters.AddWithValue("@poff", islemzamani);
                    cmdpoffistgelup.ExecuteNonQuery();
                    cmdpoffistgelup.Dispose();

                    SqlCommand cmdTakvarsaatup = new SqlCommand("SP_UP_Tak_varsaat", baglanti);
                    cmdTakvarsaatup.CommandType = CommandType.StoredProcedure;
                    cmdTakvarsaatup.Parameters.AddWithValue("@varissaati", islemzamani);
                    cmdTakvarsaatup.ExecuteNonQuery();
                    cmdTakvarsaatup.Dispose();

                }
                else
                {
                    SqlCommand cmdistgelup = new SqlCommand("SP_UpIstasyongelisFmKapno", baglanti);
                    cmdistgelup.CommandType = CommandType.StoredProcedure;
                    cmdistgelup.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdistgelup.Parameters.AddWithValue("@istasyongelis", islemzamani);
                    cmdistgelup.ExecuteNonQuery();
                    cmdistgelup.Dispose();

                }

                //yaşanan is bilgisi okuma ve issüresi hesabı
                string istasyoncikis = "";
                string istasyongelis = "";
                decimal issuresi = 0;
                float issuresif = 0;


                gecenzaman = 0; // pilot hangi vardiyanınsa gecensuresi 

                DateTime varbaslard = Convert.ToDateTime(varbaslar.Text);

                if (varbilvarno.Text == varbilvarnoe)
                {
                    TimeSpan ts2 = DateTime.Now - varbaslard;
                    gecenzaman = Convert.ToDecimal(ts2.TotalHours);
                }
                else // pilot önceki vardiyadan kalma demektir.
                {
                    TimeSpan ts2 = DateTime.Now - AnaKlas.varbaslangiconceki();
                    gecenzaman = Convert.ToDecimal(ts2.TotalHours);

                    varbaslard = AnaKlas.varbaslangiconceki();
                }

                //===========================iş süressi gerçek başladı
                SqlCommand cmdisoku = new SqlCommand("SP_PilotGemisiIstCikisGelis2li", baglanti);
                cmdisoku.CommandType = CommandType.StoredProcedure;
                cmdisoku.Parameters.AddWithValue("@secilikapno", secilikapno);
                SqlDataReader varisreader = cmdisoku.ExecuteReader();
                if (varisreader.HasRows)
                {
                    while (varisreader.Read())
                    {
                        istasyoncikis = varisreader["istasyoncikis"].ToString();
                        istasyongelis = varisreader["istasyongelis"].ToString();
                    }
                }
                varisreader.Close();
                cmdisoku.Dispose();

                TimeSpan ts = Convert.ToDateTime(istasyongelis) - Convert.ToDateTime(istasyoncikis);
                issuresif = float.Parse(ts.TotalHours.ToString());
                issuresi = Convert.ToDecimal(ts.TotalHours);

                //==================================================iş süressi gerçek bitti

                //yaşanan vardiya kap bilgisi okuma 
                SqlCommand cmdvardegeroku = new SqlCommand("SP_Pilotvardiya_TopisTopsurTopdin", baglanti);
                cmdvardegeroku.CommandType = CommandType.StoredProcedure;
                cmdvardegeroku.Parameters.AddWithValue("@kapno", secilikapno);
                cmdvardegeroku.Parameters.AddWithValue("@varno", varbilvarnoe);
                SqlDataReader vardr = cmdvardegeroku.ExecuteReader();

                int toplamissayisii = 0;
                decimal toplamissuresid = 0;
                decimal toplamdinlenmed = 0;

                if (vardr.HasRows)
                {
                    while (vardr.Read())
                    {
                        string toplamissayisi = vardr["toplamissayisi"].ToString();
                        string toplamissuresi = vardr["toplamissuresi"].ToString();
                        string toplamdinlenme = vardr["toplamdinlenme"].ToString();
                        toplamissayisii = Convert.ToInt32(toplamissayisi);
                        toplamissuresid = Convert.ToDecimal(toplamissuresi);
                        toplamdinlenmed = Convert.ToDecimal(toplamdinlenme);
                    }
                }
                vardr.Close();
                cmdvardegeroku.Dispose();



                /////******yorulma hesabı
                string kalkislimani = "Yelkenkaya";
                string yanasmalimani = "Yelkenkaya";
                string kalkisrihtimi = "0";
                string yanasmarihtimi = "0";

                SqlCommand cmdisokuup = new SqlCommand("SP_PilotGemisiKalkisVaris4lu", baglanti);
                cmdisokuup.CommandType = CommandType.StoredProcedure;
                cmdisokuup.Parameters.AddWithValue("@secilikapno", secilikapno);
                SqlDataReader varisreaderup = cmdisokuup.ExecuteReader();
                if (varisreaderup.HasRows)
                {
                    while (varisreaderup.Read())
                    {
                        kalkislimani = varisreaderup["binisyeri"].ToString();
                        yanasmalimani = varisreaderup["inisyeri"].ToString();
                        kalkisrihtimi = varisreaderup["binisrihtim"].ToString();
                        yanasmarihtimi = varisreaderup["inisrihtim"].ToString();
                    }
                }
                varisreaderup.Close();
                cmdisokuup.Dispose();

                if (kalkislimani.Substring(0, 4) == "Ters")
                { kalkislimani = "Tersane Beşiktaş"; }
                else if (kalkislimani.Substring(0, 4) == "Kosb")
                { kalkislimani = "Kosbaş"; }
                else if (kalkislimani == "Trial Voyage")
                { kalkislimani = "Yelkenkaya"; }
                else if (kalkislimani == "SafiPort")
                { kalkislimani = "Demir-İzmit"; }


                if (yanasmalimani.Substring(0, 4) == "Ters")
                { yanasmalimani = "Tersane Beşiktaş"; }
                else if (yanasmalimani.Substring(0, 4) == "Kosb")
                { yanasmalimani = "Kosbaş"; }
                else if (yanasmalimani == "Trial Voyage")
                { yanasmalimani = "Yelkenkaya"; }
                else if (yanasmalimani == "SafiPort")
                { yanasmalimani = "Demir-İzmit"; }

                SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
                cmdLimannoal.CommandType = CommandType.StoredProcedure;
                cmdLimannoal.Parameters.AddWithValue("@limanadi", kalkislimani);
                cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int); // 
                cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
                cmdLimannoal.ExecuteNonQuery();
                int portnokalkis = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString().Trim());
                cmdLimannoal.Dispose();

                SqlCommand cmdLimannoal2 = new SqlCommand("SP_Lim_Limannoal", baglanti);
                cmdLimannoal2.CommandType = CommandType.StoredProcedure;
                cmdLimannoal2.Parameters.AddWithValue("@limanadi", yanasmalimani);
                cmdLimannoal2.Parameters.Add("@limanno", SqlDbType.Int); // 
                cmdLimannoal2.Parameters["@limanno"].Direction = ParameterDirection.Output;
                cmdLimannoal2.ExecuteNonQuery();
                int portnokalvar = Convert.ToInt32(cmdLimannoal2.Parameters["@limanno"].Value.ToString().Trim());
                cmdLimannoal2.Dispose();

                double fatikzihin = 0;   // manevra farkı hesabı: 
                                        // yerler: main:2950 - stations:655,1151 - averdetup:290,515

                if (portnokalkis > 0 && portnokalkis < 900) // limandan kalkis
                {
                    if (portnokalvar > 0 && portnokalvar < 900) // limana yanaş
                    { fatikzihin = 1.9; }
                    else if (portnokalvar > 1000 && portnokalvar < 1099) // demir at
                    { fatikzihin = 1.9; }
                    else // sadece kalk
                    { fatikzihin = 1.9; }
                }

                else if (portnokalkis > 1000 && portnokalkis < 1099) // demirden kalkis
                {
                    if (portnokalvar > 0 && portnokalvar < 900) // limana yanaş
                    { fatikzihin = 1.9; }
                    else if (portnokalvar > 1000 && portnokalvar < 1099) // demir at
                    { fatikzihin = 1.9; }
                    else // sadece demir al
                    { fatikzihin = 1.9; }
                }

                else if (portnokalvar > 0 && portnokalvar < 900) // ykaya-limana yanaşma
                { fatikzihin = 1.9; }

                else if (portnokalvar > 1000 && portnokalvar < 1099) // ykaya-demir at
                { fatikzihin = 1.9; }

                if (yanasmarihtimi.IndexOf("Dbl.Anc") != -1)
                { fatikzihin = 1.9; }

                if (yanasmarihtimi.IndexOf("Dock") != -1)
                { fatikzihin = 1.9; }

                if (kalkisrihtimi.IndexOf("Dock") != -1)
                { fatikzihin = 1.9; }

                //gece fatiği
                // 1. istasyoncikis 0 den önce ve istasyongelis 6 den sonra
                double gecefatiq = 0;
                float issuresigf = 0;

                // 2. istasyoncikis 0 den önce ve istasyongelis 0 ile 6 arası

                if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) > 12) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 24))
                {
                    if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 6))
                    {
                        TimeSpan tsgf = Convert.ToDateTime(istasyongelis) - (Convert.ToDateTime(istasyoncikis).Date.AddDays(1).AddMinutes(-1));
                        issuresigf = float.Parse(tsgf.TotalHours.ToString());
                        gecefatiq = (issuresigf * 35) / 100;
                    }
                }
                // 3. istasyoncikis 0 ile 6 arası ve istasyongelis 0 ile 6 arası
                if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 6))
                {
                    if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 6))
                    {
                        gecefatiq = (issuresif * 35) / 100;
                    }
                }
                // 4. istasyoncikis 0 ile 6 arası ve istasyongelis 6 den sonra
                if ((Convert.ToInt32(istasyoncikis.Substring(11, 2)) >= 0) && (Convert.ToInt32(istasyoncikis.Substring(11, 2)) < 6))
                {
                    if ((Convert.ToInt32(istasyongelis.Substring(11, 2)) > 5) && (Convert.ToInt32(istasyongelis.Substring(11, 2)) < 18))
                    {
                        TimeSpan tsgf = Convert.ToDateTime(istasyongelis).Date.AddDays(1).AddMinutes(-1).AddHours(-18) - (Convert.ToDateTime(istasyoncikis));
                        issuresigf = float.Parse(tsgf.TotalHours.ToString());
                        gecefatiq = (issuresigf * 35) / 100;
                    }
                }

                fatikzihin = fatikzihin + gecefatiq;

                /////**********************************




                //vardiya toplamlarını güncelleme
                if (issuresif > 0.16) // iş süresi 10dk dan çoksa sisteme girer
                {

                    ////rest ve work range ler hesabı
                    SqlCommand cmdsongelal = new SqlCommand("SP_Vardiyadetay_songelal", baglanti);
                    cmdsongelal.CommandType = CommandType.StoredProcedure;
                    cmdsongelal.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdsongelal.Parameters.Add("@istasyongelis", SqlDbType.VarChar, 16); // 
                    cmdsongelal.Parameters["@istasyongelis"].Direction = ParameterDirection.Output;
                    cmdsongelal.ExecuteNonQuery();
                    string sonistgelal = cmdsongelal.Parameters["@istasyongelis"].Value.ToString().Trim();
                    cmdsongelal.Dispose();

                    if (sonistgelal == "")
                    { sonistgelal = varbaslar.Text.ToString(); }

                    DateTime sonistgelald = Convert.ToDateTime(sonistgelal);
                    DateTime istasyoncikisd = Convert.ToDateTime(istasyoncikis);

                    if (sonistgelald < varbaslard.AddHours(-2))
                    { sonistgelald = varbaslard; }

                    if (istasyoncikisd < sonistgelald)
                    { istasyoncikisd = sonistgelald; }


                    decimal lastrest = 0;
                    TimeSpan tslastgel = istasyoncikisd - sonistgelald;
                    lastrest = Convert.ToDecimal(tslastgel.TotalHours);

                    //bilgiler vardiyadetaya kopyalanıyor
                    SqlCommand cmdvarkayit = new SqlCommand("SP_Ekle_VardiyadetayAllCopy", baglanti);
                    cmdvarkayit.CommandType = CommandType.StoredProcedure;
                    cmdvarkayit.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdvarkayit.ExecuteNonQuery();
                    cmdvarkayit.Dispose();

                    //üstteki son giriş range işleniyor
                    SqlCommand cmdrannup = new SqlCommand("SP_UP_Vardiyadetay_Ranges", baglanti);
                    cmdrannup.CommandType = CommandType.StoredProcedure;
                    cmdrannup.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdrannup.Parameters.AddWithValue("@rangerest", lastrest);
                    cmdrannup.Parameters.AddWithValue("@rangework", issuresi);
                    cmdrannup.ExecuteNonQuery();
                    cmdrannup.Dispose();

                    //İŞ İPTAL EDİLMİŞSE
                    //====================================================
                    string iptal = "0";
                    if (DDCjob.SelectedItem.Text == "Yes")
                    {
                        iptal = "1";

                        //İŞLİSTE GEMİ NEDURUMDA ESKİ HALİNE AYARLA
                        //===================================

                        string etageri = TarihSaatYaziYapDMYhm((DateTime.Now).AddHours(6));
                        string kliman = "Yelkenkaya";
                        string krihtimi = "0";
                        string ylimani = "Yelkenkaya";
                        string yrihtimi = "0";
                        string nedurum = "4";
                        string nedurumop = "4";

                        SqlCommand cmdlim = new SqlCommand("SP_PilotGemisiKalkisVaris4lu", baglanti);
                        cmdlim.CommandType = CommandType.StoredProcedure;
                        cmdlim.Parameters.AddWithValue("@secilikapno", secilikapno);
                        SqlDataReader limread = cmdlim.ExecuteReader();
                        if (limread.HasRows)
                        {
                            while (limread.Read())
                            {
                                kliman = limread["binisyeri"].ToString();
                                krihtimi = limread["binisrihtim"].ToString();
                                ylimani = limread["inisyeri"].ToString();
                                yrihtimi = limread["inisrihtim"].ToString();
                            }
                        }
                        limread.Close();
                        cmdlim.Dispose();


                        SqlCommand cmdLimannoalk = new SqlCommand("SP_Lim_Limannoal", baglanti);
                        cmdLimannoalk.CommandType = CommandType.StoredProcedure;
                        cmdLimannoalk.Parameters.AddWithValue("@limanadi", kliman);
                        cmdLimannoalk.Parameters.Add("@limanno", SqlDbType.Int); // 
                        cmdLimannoalk.Parameters["@limanno"].Direction = ParameterDirection.Output;
                        cmdLimannoalk.ExecuteNonQuery();
                        int portnok = Convert.ToInt32(cmdLimannoalk.Parameters["@limanno"].Value.ToString().Trim());
                        cmdLimannoalk.Dispose();


                        if (portnok == 998) // yelkenkaya
                        {
                            nedurum = "0";
                            nedurumop = "0";
                        }

                        else if (portnok > 0 && portnok < 900) // limandaymış
                        {
                            nedurum = "2";
                            nedurumop = "2";
                        }

                        else if (portnok > 1000 && portnok < 1099) //demirdeymış 
                        {
                            nedurum = "1";
                            nedurumop = "1";
                        }

                        SqlCommand cmdisnedup = new SqlCommand("SP_Up_Isliste_LimandaFmimo", baglanti);
                        cmdisnedup.CommandType = CommandType.StoredProcedure;
                        cmdisnedup.Parameters.AddWithValue("@shipadimo", shipadimo);
                        cmdisnedup.Parameters.AddWithValue("@nedurumda", nedurum);
                        cmdisnedup.Parameters.AddWithValue("@nedurumdaopr", nedurumop);
                        cmdisnedup.Parameters.AddWithValue("@eta", etageri);
                        cmdisnedup.Parameters.AddWithValue("@kalkislimani", kliman);
                        cmdisnedup.Parameters.AddWithValue("@kalkisrihtimi", krihtimi);
                        cmdisnedup.Parameters.AddWithValue("@yanasmalimani", ylimani);
                        cmdisnedup.Parameters.AddWithValue("@yanasmarihtimi", yrihtimi);
                        cmdisnedup.ExecuteNonQuery();
                        cmdisnedup.Dispose();

                        // İPTAL REASON KAYDI işliste notun başına.

                        SqlCommand cmdPilotGemisijoblist = new SqlCommand("SP_PilotGemisijoblist", baglanti);
                        cmdPilotGemisijoblist.CommandType = CommandType.StoredProcedure;
                        cmdPilotGemisijoblist.Parameters.AddWithValue("@secilikapno", secilikapno);
                        cmdPilotGemisijoblist.Parameters.Add("@gemijoblistid", SqlDbType.Int);
                        cmdPilotGemisijoblist.Parameters["@gemijoblistid"].Direction = ParameterDirection.Output;
                        cmdPilotGemisijoblist.ExecuteNonQuery();
                        string gemijoblistid = cmdPilotGemisijoblist.Parameters["@gemijoblistid"].Value.ToString().Trim();
                        cmdPilotGemisijoblist.Dispose();


                        SqlCommand cmdPilotGemisinot = new SqlCommand("SP_Isliste_geminot", baglanti);
                        cmdPilotGemisinot.CommandType = CommandType.StoredProcedure;
                        cmdPilotGemisinot.Parameters.AddWithValue("@id", gemijoblistid);
                        cmdPilotGemisinot.Parameters.Add("@notlar", SqlDbType.NVarChar, 200);
                        cmdPilotGemisinot.Parameters["@notlar"].Direction = ParameterDirection.Output;
                        cmdPilotGemisinot.ExecuteNonQuery();
                        string notu = cmdPilotGemisinot.Parameters["@notlar"].Value.ToString().Trim();
                        cmdPilotGemisinot.Dispose();

                        if (DDCjobReason.Text == "") { DDCjobReason.Text = "POB Canceled"; }

                        SqlCommand cmdisimbul = new SqlCommand("Select kapadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
                        string isimbul = cmdisimbul.ExecuteScalar().ToString();
                        cmdisimbul.Dispose();
                        SqlCommand cmdsoyisimbul = new SqlCommand("Select kapsoyadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
                        string soyisimbul = cmdsoyisimbul.ExecuteScalar().ToString();
                        if (soyisimbul.ToLower() == "yüksel") { soyisimbul = "Küksel"; }
                        cmdsoyisimbul.Dispose();
                        string isimkisalt = isimbul.Substring(0, 1) + "." + soyisimbul.Substring(0, 1);

                        if (notu.Length > 13 && notu.EndsWith(")") && notu.Substring(notu.Length - 5, 1) == "-")
                        { notu = notu.Substring(0, notu.Length - 14); }

                        SqlCommand cmdisetbup = new SqlCommand("SP_Up_Isliste_notlar", baglanti);
                        cmdisetbup.CommandType = CommandType.StoredProcedure;
                        cmdisetbup.Parameters.AddWithValue("@id", gemijoblistid);
                        cmdisetbup.Parameters.AddWithValue("@notlar", notu + " " + DDCjobReason.Text + " (" + DateTime.Now.ToShortDateString().Substring(0, 2) + "/" + DateTime.Now.ToShortTimeString() + "-" + isimkisalt + ")");
                        cmdisetbup.ExecuteNonQuery();
                        cmdisetbup.Dispose();

                        toplamissayisii = toplamissayisii - 1; //toplam işsayısı 
                        toplamdinlenmed = toplamdinlenmed - Convert.ToDecimal(fatikzihin); //toplam zihinsel yorgunluk olrak değişti


                        //// iş iptal bitti sonrası için dbox secimini No ya ayarla
                        DDCjob.Items.Clear();
                        DDCjob.Items.Add("No");
                        DDCjob.Items.Add("Yes");
                        DDCjob.SelectedItem.Selected = false;
                        DDCjob.Items.FindByText("No").Selected = true;

                    }
                    // İŞ İPTAL İŞLEMİ BİTTİ

                    SqlCommand cmdmaniptal = new SqlCommand("SP_UP_Vardiyadetay_isiptalFmKapno", baglanti);
                    cmdmaniptal.CommandType = CommandType.StoredProcedure;
                    cmdmaniptal.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdmaniptal.Parameters.AddWithValue("@manevraiptal", iptal);
                    cmdmaniptal.ExecuteNonQuery();
                    cmdmaniptal.Dispose();

                    toplamissuresid = toplamissuresid + issuresi; //toplam bedensel yorgunluk
                    toplamdinlenmed = toplamdinlenmed + Convert.ToDecimal(fatikzihin); //toplam zihinsel yorgunluk olrak değişti
                    if (shipadi.ToString().ToLower() != "takviye")
                    {
                        toplamissayisii = toplamissayisii + 1; //toplam işsayısı 
                    }
                }
                else
                {
                    toplamissuresid = toplamissuresid + issuresi;

                    SqlCommand cmdlast24 = new SqlCommand("SP_DTDaricaYarimcaCanliGecmisSonGelis", baglanti);
                    cmdlast24.CommandType = CommandType.StoredProcedure;
                    cmdlast24.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdlast24.Parameters.AddWithValue("@varbilvarno", varbilvarnoe);
                    SqlDataReader varisreaderupher = cmdlast24.ExecuteReader();
                    if (varisreaderupher.HasRows)
                    {
                        while (varisreaderupher.Read())
                        {
                            istasyongelis = varisreaderupher["istasyongelis"].ToString();
                        }
                    }
                    else
                    { istasyongelis = varbaslar.Text; }

                    varisreaderupher.Close();

                }


                decimal yorulmad = ((toplamissuresid + toplamdinlenmed) / gecenzaman);
                yorulmad = (yorulmad * 3) / 5;

                SqlCommand cmdvardetayup = new SqlCommand("SP_Pilotvardiya_UpToplamlar", baglanti);
                cmdvardetayup.CommandType = CommandType.StoredProcedure;
                cmdvardetayup.Parameters.AddWithValue("@kapno", secilikapno);
                cmdvardetayup.Parameters.AddWithValue("@varno", varbilvarnoe);
                cmdvardetayup.Parameters.AddWithValue("toplamissayisi", toplamissayisii);
                cmdvardetayup.Parameters.AddWithValue("toplamissuresi", toplamissuresid);
                cmdvardetayup.Parameters.AddWithValue("toplamdinlenme", toplamdinlenmed);
                cmdvardetayup.Parameters.AddWithValue("yorulma", yorulmad);
                cmdvardetayup.ExecuteNonQuery();
                cmdvardetayup.Dispose();



                if (shipadi.ToString().ToLower() == "takviye") // takviyede songeliş değişmez
                {
                    string sonistasyongelistak = varbaslar.Text;
                    SqlCommand cmdlasttak = new SqlCommand("SP_DTDaricaYarimcaCanliGecmisSonGelis", baglanti);
                    cmdlasttak.CommandType = CommandType.StoredProcedure;
                    cmdlasttak.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdlasttak.Parameters.AddWithValue("@varbilvarno", varbilvarnoe);
                    SqlDataReader varisreaderuphertak = cmdlasttak.ExecuteReader();
                    if (varisreaderuphertak.HasRows)
                    {
                        while (varisreaderuphertak.Read())
                        {
                            sonistasyongelistak = varisreaderuphertak["istasyongelis"].ToString();
                        }
                    }
                    varisreaderuphertak.Close();


                    istasyongelis = sonistasyongelistak;

                }


                //kaydı temizleme
                SqlCommand cmdistipup = new SqlCommand("SP_UpPilotlarIsiptalFmKapno", baglanti);
                cmdistipup.CommandType = CommandType.StoredProcedure;
                cmdistipup.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmdistipup.Parameters.AddWithValue("@durum", "0");
                cmdistipup.Parameters.AddWithValue("@imono", 0);
                cmdistipup.Parameters.AddWithValue("@gemiadi", "");
                cmdistipup.Parameters.AddWithValue("@bayrak", "");
                cmdistipup.Parameters.AddWithValue("@grt", "");
                cmdistipup.Parameters.AddWithValue("@tip", "");
                cmdistipup.Parameters.AddWithValue("@binisyeri", "");
                cmdistipup.Parameters.AddWithValue("@binisrihtim", "");
                cmdistipup.Parameters.AddWithValue("@inisyeri", "");
                cmdistipup.Parameters.AddWithValue("@inisrihtim", "");
                cmdistipup.Parameters.AddWithValue("@istasyoncikis", "");
                cmdistipup.Parameters.AddWithValue("@pob", "");
                cmdistipup.Parameters.AddWithValue("@poff", "");
                cmdistipup.Parameters.AddWithValue("@istasyongelis", istasyongelis); // lastseven hesabı için silme
                cmdistipup.Parameters.AddWithValue("@gemiatamazamani", "");
                cmdistipup.Parameters.AddWithValue("@jnot", "");
                cmdistipup.Parameters.AddWithValue("@jnotdaily", "");
                cmdistipup.Parameters.AddWithValue("@rom1", "");
                cmdistipup.Parameters.AddWithValue("@rom2", "");
                cmdistipup.Parameters.AddWithValue("@rom3", "");
                cmdistipup.Parameters.AddWithValue("@rom4", "");
                cmdistipup.Parameters.AddWithValue("@rom5", "");
                cmdistipup.Parameters.AddWithValue("@mboat", "");
                cmdistipup.Parameters.AddWithValue("@calsaat1", "");
                cmdistipup.Parameters.AddWithValue("@calsaat2", "");
                cmdistipup.Parameters.AddWithValue("@calsaat3", "");
                cmdistipup.Parameters.AddWithValue("@calsaat4", "");
                cmdistipup.Parameters.AddWithValue("@acente", "");
                cmdistipup.Parameters.AddWithValue("@oper", "");
                cmdistipup.ExecuteNonQuery();
                cmdistipup.Dispose();

                // eklenen pilot için vardiya değişince pilotlar izin gorev sifirlama. kod başlar
                //pilot gorev bul
                SqlCommand cmddurumbak = new SqlCommand("SP_PilotGorevBak", baglanti);
                cmddurumbak.CommandType = CommandType.StoredProcedure;
                cmddurumbak.Parameters.AddWithValue("@secilikapno", secilikapno);
                cmddurumbak.Parameters.Add("@gorevde", SqlDbType.Char, 1);
                cmddurumbak.Parameters["@gorevde"].Direction = ParameterDirection.Output;
                cmddurumbak.ExecuteNonQuery();
                string gorev = cmddurumbak.Parameters["@gorevde"].Value.ToString().Trim();
                cmddurumbak.Dispose();

                if (gorev == "3")
                {
                    //pilot hangi vardiyaya aitse pilotalardan varno bul
                    SqlCommand cmdpilvarnooku = new SqlCommand("SP_varvarnooku", baglanti);
                    cmdpilvarnooku.CommandType = CommandType.StoredProcedure;
                    cmdpilvarnooku.Parameters.AddWithValue("@secilikapno", secilikapno);
                    cmdpilvarnooku.Parameters.Add("@varnooku", SqlDbType.Char, 6);
                    cmdpilvarnooku.Parameters["@varnooku"].Direction = ParameterDirection.Output;
                    cmdpilvarnooku.ExecuteNonQuery();
                    string varnook = cmdpilvarnooku.Parameters["@varnooku"].Value.ToString().Trim();
                    cmdpilvarnooku.Dispose();

                    if (AnaKlas.varnohesapla() != varnook) // kendi vardiyanın pilotu ise izin ve gorev sıfırlar varno yeniler
                        {
                        SqlCommand cmdiziniptali = new SqlCommand("Update pilotlar set izinde=@izinde, gorevde=@gorevde, istasyongelis=@istasyongelis ,varno=@varno where kapno =" + secilikapno, baglanti);
                        cmdiziniptali.Parameters.AddWithValue("izinde", '0');
                        cmdiziniptali.Parameters.AddWithValue("gorevde", '0');
                        cmdiziniptali.Parameters.AddWithValue("varno", varbilvarno.Text);
                        cmdiziniptali.Parameters.AddWithValue("istasyongelis", varbaslar.Text);

                        cmdiziniptali.ExecuteNonQuery();
                        cmdiziniptali.Dispose();
                    }
                }
                // eklenen pilot için vardiya değişince pilotlar izin gorev sifirlama. kod bitti

                //duyuru tekrar oku
                SqlCommand duyurubak = new SqlCommand("SP_duyurubak", baglanti);
                duyurubak.CommandType = CommandType.StoredProcedure;
                duyurubak.Parameters.Add("@duyurusonuc", SqlDbType.NVarChar, 700);
                duyurubak.Parameters["@duyurusonuc"].Direction = ParameterDirection.Output;
                duyurubak.ExecuteNonQuery();
                LblDuyuru.Text = "Yeni Duyuru Yok";
                if (string.IsNullOrEmpty(duyurubak.Parameters["@duyurusonuc"].Value.ToString().Trim()) != true)
                { LblDuyuru.Text = duyurubak.Parameters["@duyurusonuc"].Value.ToString().Trim(); }
                duyurubak.Dispose();

                //duyuru daily
                string jnotdaily = "";
                SqlCommand cmdduydayoku = new SqlCommand("SP_duydailyoku", baglanti); //enson duyuru oku
                cmdduydayoku.CommandType = CommandType.StoredProcedure;
                cmdduydayoku.Parameters.AddWithValue("@aktif", "2");
                SqlDataReader limread2 = cmdduydayoku.ExecuteReader();

                string kayittarihi = "";
                string iptaltarihi = "";

                if (limread2.HasRows)
                {
                    while (limread2.Read())
                    {
                        jnotdaily = limread2["duyuru"].ToString();
                        kayittarihi = limread2["kayittarihi"].ToString();
                        iptaltarihi = limread2["iptaltarihi"].ToString();
                    }
                }
                limread2.Close();
                cmdduydayoku.Dispose();


                if (iptaltarihi.Trim() == null || iptaltarihi.Trim() == "" || iptaltarihi.Trim().Length == 0)
                {
                }
                else
                {
                    jnotdaily = "";
                }
                LBLjdaily.Text = jnotdaily;


                DTloading(baglanti);
            }

        }



        //*************** Herkesin degerleri update ediliyor*********4 tuş da burayı çalıştırır***************
        if (varbilvarno.Text == varbilvarnoe)
        {
            TimeSpan ts2 = DateTime.Now - Convert.ToDateTime(varbaslar.Text);
            gecenzaman = Convert.ToDecimal(ts2.TotalHours);
        }
        else // pilot önceki vardiyadan kalma demektir.
        {
            TimeSpan ts2 = DateTime.Now - AnaKlas.varbaslangiconceki();
            gecenzaman = Convert.ToDecimal(ts2.TotalHours);
        }


        // herkesin yorulması geçen zamana göre update ediliyor
        SqlCommand cmdfatoku = new SqlCommand("SP_Pilotvardiya_TopisTopsurTopdinIdYor", baglanti);
        cmdfatoku.CommandType = CommandType.StoredProcedure;
        cmdfatoku.Parameters.AddWithValue("@varno", varbilvarnoe);
        SqlDataReader fatrdr = cmdfatoku.ExecuteReader();

        if (fatrdr.HasRows)
        {
            while (fatrdr.Read())
            {
                int kapnoi = Convert.ToInt32(fatrdr["kapno"].ToString());
                string id = fatrdr["id"].ToString();
                int idi = Convert.ToInt32(id);
                string olantoplamissuresi = fatrdr["toplamissuresi"].ToString();
                string olantoplamdinlenme = fatrdr["toplamdinlenme"].ToString();

                decimal olantoplamissuresid = Convert.ToDecimal(olantoplamissuresi);
                decimal olantoplamdinlenmed = Convert.ToDecimal(olantoplamdinlenme);

                decimal yorulmadall = ((olantoplamissuresid + olantoplamdinlenmed) / gecenzaman);
                yorulmadall = (yorulmadall * 3) / 5;

                SqlConnection baglanti2 = AnaKlas.baglan2();

                //son gelis oku ve last7 hesapla ///////////////////////////
                decimal last7 = 0;
                string songelis = "";
                SqlCommand cmdsongelisoku = new SqlCommand("SP_PilotGemisiIstCikisGelis2li", baglanti2);
                cmdsongelisoku.CommandType = CommandType.StoredProcedure;
                cmdsongelisoku.Parameters.AddWithValue("@secilikapno", kapnoi);
                SqlDataReader varissonreader = cmdsongelisoku.ExecuteReader();
                if (varissonreader.HasRows)
                {
                    while (varissonreader.Read())
                    {
                        songelis = varissonreader["istasyongelis"].ToString();
                    }
                }
                varissonreader.Close();
                cmdsongelisoku.Dispose();


                if (shipadi.ToString().ToLower() == "takviye") // takviyede eski songelişi bul
                {
                    string sonistasyongelistak = varbaslar.Text;
                    SqlCommand cmdlasttak = new SqlCommand("SP_DTDaricaYarimcaCanliGecmisSonGelis", baglanti2);
                    cmdlasttak.CommandType = CommandType.StoredProcedure;
                    cmdlasttak.Parameters.AddWithValue("@secilikapno", kapnoi);
                    cmdlasttak.Parameters.AddWithValue("@varbilvarno", varbilvarnoe);
                    SqlDataReader varisreaderuphertak = cmdlasttak.ExecuteReader();
                    if (varisreaderuphertak.HasRows)
                    {
                        while (varisreaderuphertak.Read())
                        {
                            sonistasyongelistak = varisreaderuphertak["istasyongelis"].ToString();
                        }
                    }
                    varisreaderuphertak.Close();
                    cmdlasttak.Dispose();

                    songelis = sonistasyongelistak;
                }


                TimeSpan tslast7 = DateTime.Now - Convert.ToDateTime(songelis);
                last7 = Convert.ToDecimal(tslast7.TotalHours);
                last7 = Math.Round(last7, 2);
                if (last7 > 24) { last7 = Convert.ToDecimal("23"); }

                int sevensay = 0;
                int daysay = 0;

                if (last7 > 8) { sevensay = Convert.ToInt32(last7 * 100); }
                else if (last7 < 0) { last7 = 0; }
                // son okuma kayıt bitti

                // son 24 fatik hesap ve kayıt baslar
                string istasyoncikisher = "";
                string istasyongelisher = "";
                decimal issuresitoplamher = 0;

                SqlCommand cmdlast24 = new SqlCommand("SP_DTDaricaYarimcaCanliGecmis", baglanti2);
                cmdlast24.CommandType = CommandType.StoredProcedure;
                cmdlast24.Parameters.AddWithValue("@secilikapno", kapnoi);
                cmdlast24.Parameters.AddWithValue("@varbilvarno", varbilvarnoe);

                SqlDataReader varisreaderupher = cmdlast24.ExecuteReader();
                if (varisreaderupher.HasRows)
                {
                    while (varisreaderupher.Read())
                    {
                        decimal issuresiher = 0;
                        istasyoncikisher = varisreaderupher["istasyoncikis"].ToString();
                        istasyongelisher = varisreaderupher["istasyongelis"].ToString();

                        DateTime istasyoncikisherd = Convert.ToDateTime(istasyoncikisher);
                        DateTime istasyongelisherd = Convert.ToDateTime(istasyongelisher);

                        if (istasyongelisherd > DateTime.Now.AddHours(-24) && istasyoncikisherd > DateTime.Now.AddHours(-24))
                        {
                            TimeSpan tsher1 = istasyongelisherd - istasyoncikisherd;
                            issuresiher = Convert.ToDecimal(tsher1.TotalHours.ToString());
                            issuresitoplamher = issuresitoplamher + issuresiher;
                        }
                        else if (istasyongelisherd > DateTime.Now.AddHours(-24) && istasyoncikisherd < DateTime.Now.AddHours(-24))
                        {
                            TimeSpan tsher2 = istasyongelisherd - DateTime.Now.AddHours(-24);
                            issuresiher = Convert.ToDecimal(tsher2.TotalHours.ToString());
                            issuresitoplamher = issuresitoplamher + issuresiher;
                        }
                    }
                }
                varisreaderupher.Close();


                if (issuresitoplamher > 14) { daysay = 1; }

                //----------l24 bitti

                //sırayla güncelleme
                SqlCommand cmdvarfatup = new SqlCommand("SP_Pilotvardiya_UpToplamlarHerkes", baglanti2);
                cmdvarfatup.CommandType = CommandType.StoredProcedure;
                cmdvarfatup.Parameters.AddWithValue("@idi", idi);
                cmdvarfatup.Parameters.AddWithValue("@varno", varbilvarnoe);
                cmdvarfatup.Parameters.AddWithValue("yorulma", yorulmadall);
                cmdvarfatup.Parameters.AddWithValue("lastseven", last7);
                cmdvarfatup.Parameters.AddWithValue("lastday", 24 - issuresitoplamher); // son Rest/24
                cmdvarfatup.Parameters.AddWithValue("sevensayi", sevensay);
                cmdvarfatup.Parameters.AddWithValue("daysayi", daysay);
                cmdvarfatup.ExecuteNonQuery();
                cmdvarfatup.Dispose();


                baglanti2.Close();
            }
        }
        fatrdr.Close();

        //******************* Herkesin degerleri update edildi ********************

        DTloading(baglanti);


        baglanti.Close();
    }
    protected void BacpCSokcd_Click(object sender, EventArgs e)// dest. değişti onayı
    {
        SqlConnection baglanti = AnaKlas.baglan();
        int secilikapno = Convert.ToInt32(BacpCSokcd.CommandArgument.ToString());

        SqlCommand cmdRespistal = new SqlCommand("SP_RespistFmPort", baglanti);
        cmdRespistal.CommandType = CommandType.StoredProcedure;
        cmdRespistal.Parameters.AddWithValue("@seciliport", DDLdesplace.SelectedItem.Text);
        cmdRespistal.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
        cmdRespistal.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
        cmdRespistal.ExecuteNonQuery();
        string respistal = cmdRespistal.Parameters["@bagliistasyon"].Value.ToString().Trim();
        cmdRespistal.Dispose();

        // pilot durum ve ilkvaris kontrol
        SqlCommand cmddurumbak = new SqlCommand("SP_PilotDurumBak", baglanti);
        cmddurumbak.CommandType = CommandType.StoredProcedure;
        cmddurumbak.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmddurumbak.Parameters.Add("@durum", SqlDbType.Char, 1);
        cmddurumbak.Parameters["@durum"].Direction = ParameterDirection.Output;
        cmddurumbak.ExecuteNonQuery();
        string durum = cmddurumbak.Parameters["@durum"].Value.ToString().Trim();
        cmddurumbak.Dispose();

        //SqlCommand cmdVarisBagliistasyonilk = new SqlCommand("SP_VarisBagliistasyon", baglanti);
        //cmdVarisBagliistasyonilk.CommandType = CommandType.StoredProcedure;
        //cmdVarisBagliistasyonilk.Parameters.AddWithValue("@secilikapno", secilikapno);
        //cmdVarisBagliistasyonilk.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
        //cmdVarisBagliistasyonilk.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
        //cmdVarisBagliistasyonilk.ExecuteNonQuery();
        //string istvarilk = cmdVarisBagliistasyonilk.Parameters["@bagliistasyon"].Value.ToString().Trim();
        //cmdVarisBagliistasyonilk.Dispose();

        if (durum == "2") // pob olmadan cd yapılmışsa respisti değiştirme
        {
            respistal = Session["yetki"].ToString();
        }


        // pilotlar canlı gemileri güncellendi
        SqlCommand cmdpilinup = new SqlCommand("SP_UpVarPortRihResFmKapno", baglanti);
        cmdpilinup.CommandType = CommandType.StoredProcedure;
        cmdpilinup.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdpilinup.Parameters.AddWithValue("@inisyeri", DDLdesplace.SelectedItem.Text);
        cmdpilinup.Parameters.AddWithValue("@inisrihtim", DDLdesplaceno.SelectedItem.Text);
        cmdpilinup.Parameters.AddWithValue("@respist", respistal);
        cmdpilinup.ExecuteNonQuery();
        cmdpilinup.Dispose();
        string islemzamani = TarihSaatYaziYapDMYhm(DateTime.Now);

        SqlCommand cmdVarisBagliistasyon = new SqlCommand("SP_VarisBagliistasyon", baglanti);
        cmdVarisBagliistasyon.CommandType = CommandType.StoredProcedure;
        cmdVarisBagliistasyon.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdVarisBagliistasyon.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
        cmdVarisBagliistasyon.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
        cmdVarisBagliistasyon.ExecuteNonQuery();
        string istvar = cmdVarisBagliistasyon.Parameters["@bagliistasyon"].Value.ToString().Trim();
        cmdVarisBagliistasyon.Dispose();

        SqlCommand cmdKalkisBagliistasyon = new SqlCommand("SP_VKalkisBagliistasyon", baglanti);
        cmdKalkisBagliistasyon.CommandType = CommandType.StoredProcedure;
        cmdKalkisBagliistasyon.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdKalkisBagliistasyon.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
        cmdKalkisBagliistasyon.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
        cmdKalkisBagliistasyon.ExecuteNonQuery();
        string istcik = cmdKalkisBagliistasyon.Parameters["@bagliistasyon"].Value.ToString().Trim();
        cmdKalkisBagliistasyon.Dispose();

        //değişen zamanlar update ediliyor
        SqlCommand cmdpobreal = new SqlCommand("SP_PilotGemisi_Pob", baglanti);//son gercek pob oku
        cmdpobreal.CommandType = CommandType.StoredProcedure;
        cmdpobreal.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdpobreal.Parameters.Add("@realpob", SqlDbType.VarChar, 16);
        cmdpobreal.Parameters["@realpob"].Direction = ParameterDirection.Output;
        cmdpobreal.ExecuteNonQuery();
        string pobreal = cmdpobreal.Parameters["@realpob"].Value.ToString().Trim();
        cmdpobreal.Dispose();

        DateTime pobd = AnaKlas.TarihSaatYapDMYhm(pobreal);

        int timeDarToBinis = pilotzamanhesapla(secilikapno)[0];
        int timeYarToBinis = pilotzamanhesapla(secilikapno)[1];
        int timeBinisToInis = pilotzamanhesapla(secilikapno)[2];
        int timeInisToDar = pilotzamanhesapla(secilikapno)[3];
        int timeInisToYar = pilotzamanhesapla(secilikapno)[4];
        int timeYanasSure = pilotzamanhesapla(secilikapno)[5];
        int timeKalkSure = pilotzamanhesapla(secilikapno)[6];

        //string istasyoncikic = "";
        //string pob = "";
        string poff = "";
        string istasyongelis = "";

        if (istcik == "1")
        {
            if (istvar == "1")
            {
                poff = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure));
                istasyongelis = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure).AddMinutes(timeInisToDar));
            }
            else if (istvar == "2")
            {
                poff = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure));
                istasyongelis = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure).AddMinutes(timeInisToYar));
            }
        }
        else if (istcik == "2")
        {
            if (istvar == "1")
            {
                poff = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure));
                istasyongelis = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure).AddMinutes(timeInisToDar));

            }
            else if (istvar == "2")
            {
                poff = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure));
                istasyongelis = TarihSaatYaziYapDMYhm(pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure).AddMinutes(timeInisToYar));
            }
        }

        SqlCommand cmdpoffistgelup = new SqlCommand("SP_UpPoffistgelFmKapno", baglanti);
        cmdpoffistgelup.CommandType = CommandType.StoredProcedure;
        cmdpoffistgelup.Parameters.AddWithValue("@secilikapno", secilikapno);
        cmdpoffistgelup.Parameters.AddWithValue("@poff", poff);
        cmdpoffistgelup.Parameters.AddWithValue("@istasyongelis", istasyongelis);
        cmdpoffistgelup.ExecuteNonQuery();
        cmdpoffistgelup.Dispose();


        DTloading(baglanti);
        baglanti.Close();
    }
    public int[] pilotzamanhesapla(int secilikapno)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        string islemzamani = TarihSaatYaziYapDMYhm(DateTime.Now);

        // mesafeler hesapları 
        // bilgi toplama başlar============================================================================
        // darıca istasyon bolge = 90    yarımca istasyon bolge = 91
        string binisyeri = "";
        string binisrihtim = "";
        int binisbolge = 0;
        string inisyeri = "";
        string inisrihtim = "";
        int inisbolge = 0;

        SqlCommand cmdisokuup = new SqlCommand("SP_PilotGemisiKalkisVaris4lu", baglanti);
        cmdisokuup.CommandType = CommandType.StoredProcedure;
        cmdisokuup.Parameters.AddWithValue("@secilikapno", secilikapno);
        SqlDataReader varisreaderup = cmdisokuup.ExecuteReader();
        if (varisreaderup.HasRows)
        {
            while (varisreaderup.Read())
            {
                binisyeri = varisreaderup["binisyeri"].ToString();
                binisrihtim = varisreaderup["binisrihtim"].ToString();
                inisyeri = varisreaderup["inisyeri"].ToString();
                inisrihtim = varisreaderup["inisrihtim"].ToString();
            }
        }
        varisreaderup.Close();
        cmdisokuup.Dispose();

        SqlCommand cmdlimanbolgeal = new SqlCommand("SP_Lim_limanbolgeal", baglanti);
        cmdlimanbolgeal.CommandType = CommandType.StoredProcedure;
        cmdlimanbolgeal.Parameters.AddWithValue("@limanadi", binisyeri);
        cmdlimanbolgeal.Parameters.Add("@limanbolge", SqlDbType.VarChar, 2);
        cmdlimanbolgeal.Parameters["@limanbolge"].Direction = ParameterDirection.Output;
        cmdlimanbolgeal.ExecuteNonQuery();
        binisbolge = Convert.ToInt32(cmdlimanbolgeal.Parameters["@limanbolge"].Value.ToString());
        cmdlimanbolgeal.Dispose();

        SqlCommand cmdlimanbolgeali = new SqlCommand("SP_Lim_limanbolgeal", baglanti);
        cmdlimanbolgeali.CommandType = CommandType.StoredProcedure;
        cmdlimanbolgeali.Parameters.AddWithValue("@limanadi", inisyeri);
        cmdlimanbolgeali.Parameters.Add("@limanbolge", SqlDbType.VarChar, 2);
        cmdlimanbolgeali.Parameters["@limanbolge"].Direction = ParameterDirection.Output;
        cmdlimanbolgeali.ExecuteNonQuery();
        inisbolge = Convert.ToInt32(cmdlimanbolgeali.Parameters["@limanbolge"].Value.ToString());
        cmdlimanbolgeali.Dispose();

        // mesafeler alınıyor

        SqlCommand cmdarasure1 = new SqlCommand("SP_LM_arasurefm_limaneta", baglanti);
        cmdarasure1.CommandType = CommandType.StoredProcedure;
        cmdarasure1.Parameters.AddWithValue("@liman1", "90");
        cmdarasure1.Parameters.AddWithValue("@liman2", binisbolge);
        cmdarasure1.Parameters.Add("@arasure", SqlDbType.VarChar, 3);
        cmdarasure1.Parameters["@arasure"].Direction = ParameterDirection.Output;
        cmdarasure1.ExecuteNonQuery();
        int timeDarToBinis = Convert.ToInt32(cmdarasure1.Parameters["@arasure"].Value.ToString());
        cmdarasure1.Dispose();

        SqlCommand cmdarasure2 = new SqlCommand("SP_LM_arasurefm_limaneta", baglanti);
        cmdarasure2.CommandType = CommandType.StoredProcedure;
        cmdarasure2.Parameters.AddWithValue("@liman1", "91");
        cmdarasure2.Parameters.AddWithValue("@liman2", binisbolge);
        cmdarasure2.Parameters.Add("@arasure", SqlDbType.VarChar, 3);
        cmdarasure2.Parameters["@arasure"].Direction = ParameterDirection.Output;
        cmdarasure2.ExecuteNonQuery();
        int timeYarToBinis = Convert.ToInt32(cmdarasure2.Parameters["@arasure"].Value.ToString());
        cmdarasure2.Dispose();

        SqlCommand cmdarasure3 = new SqlCommand("SP_LM_arasurefm_limaneta", baglanti);
        cmdarasure3.CommandType = CommandType.StoredProcedure;
        cmdarasure3.Parameters.AddWithValue("@liman1", binisbolge);
        cmdarasure3.Parameters.AddWithValue("@liman2", inisbolge);
        cmdarasure3.Parameters.Add("@arasure", SqlDbType.VarChar, 3);
        cmdarasure3.Parameters["@arasure"].Direction = ParameterDirection.Output;
        cmdarasure3.ExecuteNonQuery();
        int timeBinisToInis = Convert.ToInt32(cmdarasure3.Parameters["@arasure"].Value.ToString());
        cmdarasure3.Dispose();

        SqlCommand cmdarasure4 = new SqlCommand("SP_LM_arasurefm_limaneta", baglanti);
        cmdarasure4.CommandType = CommandType.StoredProcedure;
        cmdarasure4.Parameters.AddWithValue("@liman1", inisbolge);
        cmdarasure4.Parameters.AddWithValue("@liman2", "90");
        cmdarasure4.Parameters.Add("@arasure", SqlDbType.VarChar, 3);
        cmdarasure4.Parameters["@arasure"].Direction = ParameterDirection.Output;
        cmdarasure4.ExecuteNonQuery();
        int timeInisToDar = Convert.ToInt32(cmdarasure4.Parameters["@arasure"].Value.ToString());
        cmdarasure4.Dispose();

        SqlCommand cmdarasure5 = new SqlCommand("SP_LM_arasurefm_limaneta", baglanti);
        cmdarasure5.CommandType = CommandType.StoredProcedure;
        cmdarasure5.Parameters.AddWithValue("@liman1", inisbolge);
        cmdarasure5.Parameters.AddWithValue("@liman2", "91");
        cmdarasure5.Parameters.Add("@arasure", SqlDbType.VarChar, 3);
        cmdarasure5.Parameters["@arasure"].Direction = ParameterDirection.Output;
        cmdarasure5.ExecuteNonQuery();
        int timeInisToYar = Convert.ToInt32(cmdarasure5.Parameters["@arasure"].Value.ToString());
        cmdarasure5.Dispose();

        // mamevra süreleri al

        SqlCommand cmdyanas = new SqlCommand("SP_Lim_yanasmasure", baglanti);
        cmdyanas.CommandType = CommandType.StoredProcedure;
        cmdyanas.Parameters.AddWithValue("@limanadi", inisyeri);
        cmdyanas.Parameters.AddWithValue("@rihtimadi", inisrihtim);
        cmdyanas.Parameters.Add("@yanasmasuresi", SqlDbType.Int);
        cmdyanas.Parameters["@yanasmasuresi"].Direction = ParameterDirection.Output;
        cmdyanas.ExecuteNonQuery();
        int timeYanasSure = Convert.ToInt32(cmdyanas.Parameters["@yanasmasuresi"].Value.ToString());
        cmdyanas.Dispose();

        SqlCommand cmdkalk = new SqlCommand("SP_Lim_kalkissure", baglanti);
        cmdkalk.CommandType = CommandType.StoredProcedure;
        cmdkalk.Parameters.AddWithValue("@limanadi", binisyeri);
        cmdkalk.Parameters.AddWithValue("@rihtimadi", binisrihtim);
        cmdkalk.Parameters.Add("@kalkissuresi", SqlDbType.Int);
        cmdkalk.Parameters["@kalkissuresi"].Direction = ParameterDirection.Output;
        cmdkalk.ExecuteNonQuery();
        int timeKalkSure = Convert.ToInt32(cmdkalk.Parameters["@kalkissuresi"].Value.ToString());
        cmdkalk.Dispose();


        baglanti.Close();
        return new int[7] { timeDarToBinis, timeYarToBinis, timeBinisToInis, timeInisToDar, timeInisToYar, timeYanasSure, timeKalkSure };
    }
    protected void DDLdesplace_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLdesplaceno.Visible = true;
        DDLdesplaceno.Items.Clear();
        this.MPCDonay.Show();
        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim.Parameters.AddWithValue("@limanadi", DDLdesplace.SelectedItem.Text);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdDDLrihtim;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "limanlar");
        DDLdesplaceno.Items.Clear();
        DDLdesplaceno.DataValueField = "id";
        DDLdesplaceno.DataTextField = "rihtimadi";
        DDLdesplaceno.DataSource = ds;
        DDLdesplaceno.DataBind();
        baglanti.Close();
        if (DDLdesplaceno.SelectedItem.Text == "0") { DDLdesplaceno.Visible = false; }


    }

    //------ izin durumları baslar
    protected void ButtonVacation_Click(object sender, EventArgs e)
    {
        izinliyenile();
    }
    private void izinliyenile()
    {
        CBaddpilot.Checked = false;
        izinliyenileic();
        this.ModalPopupizinli.Show();
    }
    private void izinliyenileic()
    {
        SqlConnection baglanti = AnaKlas.baglan();
        wac1.Enabled = true;
        wac2.Enabled = true;
        wac3.Enabled = true;

        DDLizinlifull.Enabled = true;
        DDLizinlifull.SelectedIndex = -1;
        DDLdegisfull.Enabled = false;
        DDLdegisfull.SelectedIndex = -1;

        wac1.Checked = false;
        wac2.Checked = false;
        wac3.Checked = false;

        if (varbilvarid.Text == "1") { wac1.Checked = true; }
        else if (varbilvarid.Text == "2") { wac2.Checked = true; }
        else if (varbilvarid.Text == "3") { wac3.Checked = true; }

        DDLizinlifull.Items.Clear();
        DataTable DTizinlifull = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where emekli = 'No' and kapsirano<'1000' and izinde =0 and varid='" + varbilvarid.Text + "'  order by kapadisoyadi");
        DDLizinlifull.DataValueField = "kapno";
        DDLizinlifull.DataTextField = "kapadisoyadi";
        DDLizinlifull.DataSource = DTizinlifull;
        DDLizinlifull.DataBind();
        DDLizinlifull.Items.Insert(0, new ListItem("Select?", String.Empty));
        DDLizinlifull.SelectedIndex = 0;

        DDLdegisfull.Items.Clear();
        DataTable DTdegisfull = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where emekli = 'No' and kapsirano<'1000' order by kapadisoyadi");
        DDLdegisfull.DataValueField = "kapno";
        DDLdegisfull.DataTextField = "kapadisoyadi";
        DDLdegisfull.DataSource = DTdegisfull;
        DDLdegisfull.DataBind();
        DDLdegisfull.Items.Insert(0, new ListItem("Select?", String.Empty));
        DDLdegisfull.SelectedIndex = 0;

        ListBizin.Items.Clear();
        DataTable DTizin = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where izinde='1' and emekli = 'No' and kapsirano<'1000'  and varno='" + varbilvarno.Text + "' order by kapno");
        ListBizin.DataTextField = "kapadisoyadi";
        ListBizin.DataValueField = "kapno";
        ListBizin.DataSource = DTizin;
        ListBizin.DataBind();

        ListBdegis.Items.Clear();
        DataTable DTdegis = AnaKlas.GetDataTable("Select degismecikapno, degismeciadi +' '+ degismecisoyadi as degismeciadisoyadi from pilotlar where izinde='1' and emekli = 'No' and kapsirano<'1000' and (varno='" + varbilvarno.Text + "') order by kapno");
        ListBdegis.DataTextField = "degismeciadisoyadi";
        ListBdegis.DataValueField = "degismecikapno";
        ListBdegis.DataSource = DTdegis;
        ListBdegis.DataBind();

        for (int i = 0; i < ListBizin.Items.Count; i++)
        {

            SqlCommand cmddurumbak = new SqlCommand("SP_PilotGorevBak", baglanti);
            cmddurumbak.CommandType = CommandType.StoredProcedure;
            cmddurumbak.Parameters.AddWithValue("@secilikapno", ListBizin.Items[i].Value);
            cmddurumbak.Parameters.Add("@gorevde", SqlDbType.Char, 1);
            cmddurumbak.Parameters["@gorevde"].Direction = ParameterDirection.Output;
            cmddurumbak.ExecuteNonQuery();
            string gorev = cmddurumbak.Parameters["@gorevde"].Value.ToString().Trim();
            cmddurumbak.Dispose();

            if (gorev == "0") // değişmeci gelmiş (izinde=1, gorevde=0 olur)
            {
                ListBdegis.Items[i].Attributes.Add("style", "background-color: Yellow !important;");
                ListBizin.Items[i].Attributes.Add("style", "background-color: Yellow !important;");

                // bunları sağ sol sarı yap
            }
            else if (gorev == "1") // direkt izindedir (izinde=1, gorevde=1 olur)
            {
                ListBdegis.Items[i].Text = "";
            }
            else if (gorev == "2") // hasta olmuş (izinde=1, gorevde=2 olur)
            {
                ListBdegis.Items[i].Text = "";
                ListBizin.Items[i].Attributes.Add("style", "background-color: Red !important;");
            }
            else if (gorev == "3") // add pilot (izinde=1, gorevde=3 olur) diğer vardiyadan gelmiş 
            {
                ListBizin.Items[i].Text = "";
                ListBdegis.Items[i].Attributes.Add("style", "background-color: Green !important;");
            }
        }

        DDLdegisfull.Enabled = false;
        DDLdegisfull.SelectedIndex = -1;
        ListBizin.SelectedIndex = -1;
        ListBdegis.SelectedIndex = -1;

        Buttonizinver.Enabled = false;
        Buttonizinver.Visible = true;
        Buttonizinyok.Visible = true;
        Buttonizinsil.Visible = false;
        baglanti.Close();
        baglanti.Dispose();

    }
    protected void DDLizinlifull_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBizin.SelectedIndex = -1;
        ListBdegis.SelectedIndex = -1;

        Buttonizinver.Visible = true;
        Buttonizinyok.Visible = true;
        Buttonizinsil.Visible = false;

        if (DDLizinlifull.SelectedItem.Value != "0")
        {
            Buttonizinver.Enabled = true;
            DDLdegisfull.SelectedIndex = -1;
            DDLdegisfull.Enabled = true;
        }


        this.ModalPopupizinli.Show();
    }
    protected void DDLdegisfull_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBizin.SelectedIndex = -1;
        ListBdegis.SelectedIndex = -1;

        Buttonizinver.Visible = true;
        Buttonizinyok.Visible = true;
        Buttonizinsil.Visible = false;


        if (CBaddpilot.Checked == true)
        {
            Buttonizinver.Enabled = true;
        }


        this.ModalPopupizinli.Show();
    }
    protected void ListBdegis_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBizin.SelectedIndex = ListBdegis.SelectedIndex; 

        Buttonizinver.Visible = false;
        Buttonizinsil.Visible = true;
        this.ModalPopupizinli.Show();
    }
    protected void ListBizin_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListBdegis.SelectedIndex = ListBizin.SelectedIndex;
        Buttonizinver.Visible = false;
        Buttonizinsil.Visible = true;

        this.ModalPopupizinli.Show();
    }
    protected void Buttonizinver_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (CBaddpilot.Checked == true)
        {
            Buttonizinver.Visible = true;

            int kapnoaldeg = Convert.ToInt32(DDLdegisfull.SelectedItem.Value);

            SqlCommand cmddegisen = new SqlCommand("Select kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where kapno =" + kapnoaldeg, baglanti);
            string degisenadd = cmddegisen.ExecuteScalar().ToString();
            cmddegisen.Dispose();

            Litmodalmesssage.Text = "</br></br><span style='font-size:16px; color:red;'>" + degisenadd + "</span></br> will be added to watch.</br></br> His fatique will be as average fatique.";

            ModalPopupMessage.Show();
        }
        else
        {

            if (DDLdegisfull.SelectedItem.Value == "0" || DDLdegisfull.SelectedItem.Value == "" || DDLdegisfull.SelectedItem.Value == "-1" || DDLdegisfull.SelectedItem.Value == null)
            {//secılmemış

                if (DDLizinlifull.SelectedItem.Value != "0" && DDLizinlifull.SelectedItem.Value != "" && DDLizinlifull.SelectedItem.Value != "-1" && DDLizinlifull.SelectedItem.Value != null)
                {//secılmış
                    int kapnoaliz = Convert.ToInt32(DDLizinlifull.SelectedItem.Value);

                    SqlCommand cmdizinalan = new SqlCommand("Select kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where kapno =" + kapnoaliz, baglanti);
                    string izinalan = cmdizinalan.ExecuteScalar().ToString();
                    cmdizinalan.Dispose();
                    Litmodalmesssage.Text = "<br/>Onleave Pilot is <span style='font-size:18px; color:red;'><br/><br/>" + izinalan.ToUpper() + " </span><br/><br/> Please confirm !";
                    ModalPopupMessage.Show();
                }


            }
            else
            {
                if (DDLizinlifull.SelectedItem.Value != "0" && DDLizinlifull.SelectedItem.Value != "" && DDLizinlifull.SelectedItem.Value != "-1" && DDLizinlifull.SelectedItem.Value != null)
                {
                    int kapnoaldeg = Convert.ToInt32(DDLdegisfull.SelectedItem.Value);
                    int kapnoaliz = Convert.ToInt32(DDLizinlifull.SelectedItem.Value);

                    SqlCommand cmdizinalan = new SqlCommand("Select kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where kapno =" + kapnoaliz, baglanti);
                    string izinalan = cmdizinalan.ExecuteScalar().ToString();
                    cmdizinalan.Dispose();

                    SqlCommand cmddegisen = new SqlCommand("Select kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where kapno =" + kapnoaldeg, baglanti);
                    string degisen = cmddegisen.ExecuteScalar().ToString();
                    cmddegisen.Dispose();

                    if (kapnoaldeg == kapnoaliz)
                    {
                        Litmodalmesssage.Text = "<span style='font-size:16px; color:red;'></br>" + izinalan + "</span></br></br> is patient or on duty by boss/company.</br> When he return, his fatique will change as average fatique. Are you Sure?";
                    }
                    else
                    {
                        Litmodalmesssage.Text = "<span style='font-size:16px; color:red;'></br>" + izinalan + "</span></br></br> is changing his watch with </br></br><span style='font-size:16px; color:red;'>" + degisen + ".</span>";
                    }
                    ModalPopupMessage.Show();

                }
            }
        }
        DTloading(baglanti);
        baglanti.Close();

    }
    protected void Buttonizinsil_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        if (ListBizin.SelectedItem.Value != null)
        {

            int secilikapno = Convert.ToInt32(ListBizin.SelectedItem.Value);
            SqlCommand cmdgorevbak = new SqlCommand("Select gorevde from pilotlar where kapno =" + secilikapno, baglanti);
            string gorevbak = cmdgorevbak.ExecuteScalar().ToString();
            cmdgorevbak.Dispose();

            //pilot hangi vardiyaya aitse pilotalrdan varno bul
            SqlCommand cmdvarbilgivarnookue = new SqlCommand("SP_varvarnooku", baglanti);
            cmdvarbilgivarnookue.CommandType = CommandType.StoredProcedure;
            cmdvarbilgivarnookue.Parameters.AddWithValue("@secilikapno", secilikapno);
            cmdvarbilgivarnookue.Parameters.Add("@varnooku", SqlDbType.Char, 6);
            cmdvarbilgivarnookue.Parameters["@varnooku"].Direction = ParameterDirection.Output;
            cmdvarbilgivarnookue.ExecuteNonQuery();
            string varnook = cmdvarbilgivarnookue.Parameters["@varnooku"].Value.ToString().Trim();
            cmdvarbilgivarnookue.Dispose();

                if (gorevbak == "1" || gorevbak == "2")
            {
                SqlCommand cmdiziniptali = new SqlCommand("Update pilotlar set degismecikapno = kapno,  degismeciadi = kapadi, degismecisoyadi = kapsoyadi, kidem = degismeciorgkidem, izinde='0', gorevde='0' where kapno =" + secilikapno, baglanti);
                cmdiziniptali.ExecuteNonQuery();
                cmdiziniptali.Dispose();

                //pilotun olan bilgisi okuma 
                SqlCommand cmdvardegeroku = new SqlCommand("SP_Pilotvardiya_TopisTopsurTopdin", baglanti);
                cmdvardegeroku.CommandType = CommandType.StoredProcedure;
                cmdvardegeroku.Parameters.AddWithValue("@kapno", secilikapno);
                cmdvardegeroku.Parameters.AddWithValue("@varno", varnook);
                SqlDataReader vardr = cmdvardegeroku.ExecuteReader();

                int toplamissayisio = 0;
                decimal toplamissuresio = 0;
                decimal toplamdinlenmeo = 0;

                if (vardr.HasRows)
                {
                    while (vardr.Read())
                    {
                        toplamissayisio = Convert.ToInt32(vardr["toplamissayisi"].ToString());
                        toplamissuresio = Convert.ToDecimal(vardr["toplamissuresi"].ToString());
                        toplamdinlenmeo = Convert.ToDecimal(vardr["toplamdinlenme"].ToString());
                    }
                }
                vardr.Close();
                cmdvardegeroku.Dispose();




                // 9.pilot okunuyor
                SqlCommand cmddokuzuncu = new SqlCommand("SP_pilotvardiya_dokuzuncupilot", baglanti);
                cmddokuzuncu.CommandType = CommandType.StoredProcedure;
                cmddokuzuncu.Parameters.AddWithValue("@varbilvarno", varnook);
                SqlDataReader fatrdrhas = cmddokuzuncu.ExecuteReader();

                decimal toplamissuresi = 0;
                decimal toplamdinlenme = 0;
                float toplamzihyor = 0;
                decimal lastday = 24;
                decimal lastseven = 0;
                int sevensayi = 0;
                int daysayi = 0;
                int yorgungitteni = 0;
                int yorgungitalli = 0;
                //float yedekbir = 0;
                //float yedekiki = 0;
                decimal yorulma = 0;

                if (fatrdrhas.HasRows)
                {
                    while (fatrdrhas.Read())
                    {
                        toplamissuresi = Convert.ToDecimal(fatrdrhas["toplamissuresi"].ToString());
                        toplamdinlenme = Convert.ToDecimal(fatrdrhas["toplamdinlenme"].ToString());
                        toplamzihyor = float.Parse(fatrdrhas["toplamzihyor"].ToString());
                        lastday = Convert.ToDecimal(fatrdrhas["lastday"].ToString());
                        lastseven = Convert.ToDecimal(fatrdrhas["lastseven"].ToString());
                        sevensayi = Convert.ToInt32(fatrdrhas["sevensayi"].ToString());
                        daysayi = Convert.ToInt32(fatrdrhas["daysayi"].ToString());
                        yorgungitteni = Convert.ToInt32(fatrdrhas["yorgungitteni"].ToString());
                        yorgungitalli = Convert.ToInt32(fatrdrhas["yorgungitalli"].ToString());
                        //yedekbir = float.Parse(fatrdrhas["yedekbir"].ToString());
                        //yedekiki = float.Parse(fatrdrhas["yedekiki"].ToString());
                        yorulma = Convert.ToDecimal(fatrdrhas["yorulma"].ToString());
                    }
                }
                fatrdrhas.Close();

                //izinliye 9.pilot yazılıyor
                SqlCommand cmdpilvarup = new SqlCommand("SP_Pilotvardiya_UpAll", baglanti);
                cmdpilvarup.CommandType = CommandType.StoredProcedure;
                cmdpilvarup.Parameters.AddWithValue("@kapno", secilikapno);
                cmdpilvarup.Parameters.AddWithValue("@varno", varnook);
                cmdpilvarup.Parameters.AddWithValue("@toplamissayisi", toplamissayisio);
                cmdpilvarup.Parameters.AddWithValue("@toplamissuresi", toplamissuresio);
                cmdpilvarup.Parameters.AddWithValue("@toplamdinlenme", toplamdinlenme + toplamissuresi - toplamissuresio);
                cmdpilvarup.Parameters.AddWithValue("@toplamzihyor", toplamzihyor);
                cmdpilvarup.Parameters.AddWithValue("@lastday", lastday);
                cmdpilvarup.Parameters.AddWithValue("@lastseven", lastseven);
                cmdpilvarup.Parameters.AddWithValue("@sevensayi", sevensayi);
                cmdpilvarup.Parameters.AddWithValue("@daysayi", daysayi);
                cmdpilvarup.Parameters.AddWithValue("@yorgungitteni", yorgungitteni);
                cmdpilvarup.Parameters.AddWithValue("@yorgungitalli", yorgungitalli);
                cmdpilvarup.Parameters.AddWithValue("@yorulma", yorulma);

                cmdpilvarup.ExecuteNonQuery();
                cmdpilvarup.Dispose();


            }
            else if (gorevbak == "0")
            {
                SqlCommand cmdiziniptali = new SqlCommand("Update pilotlar set degismecikapno = kapno,  degismeciadi = kapadi, degismecisoyadi = kapsoyadi, kidem = degismeciorgkidem, izinde='0', gorevde='0' where kapno =" + secilikapno, baglanti);
                cmdiziniptali.ExecuteNonQuery();
                cmdiziniptali.Dispose();
            }
            else if (gorevbak == "3")
            {
                SqlCommand cmdiziniptali = new SqlCommand("Update pilotlar set degismecikapno = kapno,  degismeciadi = kapadi, degismecisoyadi = kapsoyadi, kidem = degismeciorgkidem, izinde='0', gorevde='0', varno='100000' where kapno =" + secilikapno, baglanti);
                cmdiziniptali.ExecuteNonQuery();
                cmdiziniptali.Dispose();
            }
        }

        else
        { }
        DTloading(baglanti);
        baglanti.Close();
        izinliyenile();

    }

    protected void wac1_CheckedChanged(object sender, EventArgs e)
    {
        if (varbilvarid.Text == "1") { izinliyenile(); }
        else
        {
            SqlConnection baglanti = AnaKlas.baglan();

            DDLizinlifull.Items.Clear();
            DataTable DTizinlifull = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where emekli = 'No' and kapsirano<'1000' and izinde =0 and varid='1'  order by kapadisoyadi");
            DDLizinlifull.DataValueField = "kapno";
            DDLizinlifull.DataTextField = "kapadisoyadi";
            DDLizinlifull.DataSource = DTizinlifull;
            DDLizinlifull.DataBind();
            DDLizinlifull.Items.Insert(0, new ListItem("Select?", String.Empty));
            DDLizinlifull.SelectedIndex = 0;

            DDLdegisfull.Items.Clear();
            DataTable DTdegisfull = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where emekli = 'No' and kapsirano<'1000' order by kapadisoyadi");
            DDLdegisfull.DataValueField = "kapno";
            DDLdegisfull.DataTextField = "kapadisoyadi";
            DDLdegisfull.DataSource = DTdegisfull;
            DDLdegisfull.DataBind();
            DDLdegisfull.Items.Insert(0, new ListItem("Select?", String.Empty));
            DDLdegisfull.SelectedIndex = 0;

            ListBizin.Items.Clear();
            DataTable DTizin = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where izinde='1' and emekli = 'No' and kapsirano<'1000'  and varid='1' order by kapno");
            ListBizin.DataTextField = "kapadisoyadi";
            ListBizin.DataValueField = "kapno";
            ListBizin.DataSource = DTizin;
            ListBizin.DataBind();

            ListBdegis.Items.Clear();
            DataTable DTdegis = AnaKlas.GetDataTable("Select degismecikapno, degismeciadi +' '+ degismecisoyadi as degismeciadisoyadi from pilotlar where (izinde='1' and emekli = 'No' and kapsirano<'1000' and varid='1')   order by kapno");
            ListBdegis.DataTextField = "degismeciadisoyadi";
            ListBdegis.DataValueField = "degismecikapno";
            ListBdegis.DataSource = DTdegis;
            ListBdegis.DataBind();


            for (int i = 0; i < ListBizin.Items.Count; i++)
            {

                SqlCommand cmddurumbak = new SqlCommand("SP_PilotGorevBak", baglanti);
                cmddurumbak.CommandType = CommandType.StoredProcedure;
                cmddurumbak.Parameters.AddWithValue("@secilikapno", ListBizin.Items[i].Value);
                cmddurumbak.Parameters.Add("@gorevde", SqlDbType.Char, 1);
                cmddurumbak.Parameters["@gorevde"].Direction = ParameterDirection.Output;
                cmddurumbak.ExecuteNonQuery();
                string gorev = cmddurumbak.Parameters["@gorevde"].Value.ToString().Trim();
                cmddurumbak.Dispose();

                if (gorev == "0") // değişmeci gelmiş (izinde=1, gorevde=1 olur)
                {
                    ListBdegis.Items[i].Attributes.Add("style", "background-color: Yellow !important;");
                    ListBizin.Items[i].Attributes.Add("style", "background-color: Yellow !important;");

                    // bunları sağ sol sarı yap
                }
                else if (gorev == "1") // direkt izindedir (izinde=1, gorevde=1 olur)
                {
                    ListBdegis.Items[i].Text = "";
                }
                else if (gorev == "2") // hasta olmuş (izinde=1, gorevde=2 olur)
                {
                    ListBdegis.Items[i].Text = "";
                    ListBizin.Items[i].Attributes.Add("style", "background-color: Red !important;");
                }
                else if (gorev == "3") // add pilot diğer vardiyadan gelmiş (izinde=1, gorevde=3 olur)
                {
                    ListBizin.Items[i].Text = "";
                    ListBdegis.Items[i].Attributes.Add("style", "background-color: Green !important;");
                }
            }

            DDLdegisfull.Enabled = false;
            DDLdegisfull.SelectedIndex = -1;
            ListBizin.SelectedIndex = -1;
            ListBdegis.SelectedIndex = -1;

            Buttonizinver.Enabled = false;
            Buttonizinver.Visible = true;
            Buttonizinyok.Visible = true;
            Buttonizinsil.Visible = false;
            baglanti.Close();
            baglanti.Dispose();
            this.ModalPopupizinli.Show();
        }
    }
    protected void wac2_CheckedChanged(object sender, EventArgs e)
    {
        if (varbilvarid.Text == "2") { izinliyenile(); }
        else
        {
            SqlConnection baglanti = AnaKlas.baglan();

            DDLizinlifull.Items.Clear();
            DataTable DTizinlifull = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where emekli = 'No' and kapsirano<'1000' and izinde =0 and varid='2'  order by kapadisoyadi");
            DDLizinlifull.DataValueField = "kapno";
            DDLizinlifull.DataTextField = "kapadisoyadi";
            DDLizinlifull.DataSource = DTizinlifull;
            DDLizinlifull.DataBind();
            DDLizinlifull.Items.Insert(0, new ListItem("Select?", String.Empty));
            DDLizinlifull.SelectedIndex = 0;

            DDLdegisfull.Items.Clear();
            DataTable DTdegisfull = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where emekli = 'No' and kapsirano<'1000' order by kapadisoyadi");
            DDLdegisfull.DataValueField = "kapno";
            DDLdegisfull.DataTextField = "kapadisoyadi";
            DDLdegisfull.DataSource = DTdegisfull;
            DDLdegisfull.DataBind();
            DDLdegisfull.Items.Insert(0, new ListItem("Select?", String.Empty));
            DDLdegisfull.SelectedIndex = 0;

            ListBizin.Items.Clear();
            DataTable DTizin = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where izinde='1' and emekli = 'No' and kapsirano<'1000'  and varid='2' order by kapno");
            ListBizin.DataTextField = "kapadisoyadi";
            ListBizin.DataValueField = "kapno";
            ListBizin.DataSource = DTizin;
            ListBizin.DataBind();

            ListBdegis.Items.Clear();
            DataTable DTdegis = AnaKlas.GetDataTable("Select degismecikapno, degismeciadi +' '+ degismecisoyadi as degismeciadisoyadi from pilotlar where (izinde='1' and emekli = 'No' and kapsirano<'1000' and varid='2')  order by kapno");
            ListBdegis.DataTextField = "degismeciadisoyadi";
            ListBdegis.DataValueField = "degismecikapno";
            ListBdegis.DataSource = DTdegis;
            ListBdegis.DataBind();


            for (int i = 0; i < ListBizin.Items.Count; i++)
            {

                SqlCommand cmddurumbak = new SqlCommand("SP_PilotGorevBak", baglanti);
                cmddurumbak.CommandType = CommandType.StoredProcedure;
                cmddurumbak.Parameters.AddWithValue("@secilikapno", ListBizin.Items[i].Value);
                cmddurumbak.Parameters.Add("@gorevde", SqlDbType.Char, 1);
                cmddurumbak.Parameters["@gorevde"].Direction = ParameterDirection.Output;
                cmddurumbak.ExecuteNonQuery();
                string gorev = cmddurumbak.Parameters["@gorevde"].Value.ToString().Trim();
                cmddurumbak.Dispose();

                if (gorev == "0") // değişmeci gelmiş (izinde=1, gorevde=1 olur)
                {
                    ListBdegis.Items[i].Attributes.Add("style", "background-color: Yellow !important;");
                    ListBizin.Items[i].Attributes.Add("style", "background-color: Yellow !important;");

                    // bunları sağ sol sarı yap
                }
                else if (gorev == "1") // direkt izindedir (izinde=1, gorevde=1 olur)
                {
                    ListBdegis.Items[i].Text = "";
                }
                else if (gorev == "2") // hasta olmuş (izinde=1, gorevde=2 olur)
                {
                    ListBdegis.Items[i].Text = "";
                    ListBizin.Items[i].Attributes.Add("style", "background-color: Red !important;");
                }
                else if (gorev == "3") // add pilot diğer vardiyadan gelmiş (izinde=1, gorevde=3 olur)
                {
                    ListBizin.Items[i].Text = "";
                    ListBdegis.Items[i].Attributes.Add("style", "background-color: Green !important;");
                }
            }

            DDLdegisfull.Enabled = false;
            DDLdegisfull.SelectedIndex = -1;
            ListBizin.SelectedIndex = -1;
            ListBdegis.SelectedIndex = -1;

            Buttonizinver.Enabled = false;
            Buttonizinver.Visible = true;
            Buttonizinyok.Visible = true;
            Buttonizinsil.Visible = false;
            baglanti.Close();
            baglanti.Dispose();
            this.ModalPopupizinli.Show();
        }
    }
    protected void wac3_CheckedChanged(object sender, EventArgs e)
    {
        if (varbilvarid.Text == "3") { izinliyenile(); }
        else
        {
            SqlConnection baglanti = AnaKlas.baglan();

            DDLizinlifull.Items.Clear();
            DataTable DTizinlifull = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where emekli = 'No' and kapsirano<'1000' and izinde =0 and varid='3'  order by kapadisoyadi");
            DDLizinlifull.DataValueField = "kapno";
            DDLizinlifull.DataTextField = "kapadisoyadi";
            DDLizinlifull.DataSource = DTizinlifull;
            DDLizinlifull.DataBind();
            DDLizinlifull.Items.Insert(0, new ListItem("Select?", String.Empty));
            DDLizinlifull.SelectedIndex = 0;

            DDLdegisfull.Items.Clear();
            DataTable DTdegisfull = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where emekli = 'No' and kapsirano<'1000' order by kapadisoyadi");
            DDLdegisfull.DataValueField = "kapno";
            DDLdegisfull.DataTextField = "kapadisoyadi";
            DDLdegisfull.DataSource = DTdegisfull;
            DDLdegisfull.DataBind();
            DDLdegisfull.Items.Insert(0, new ListItem("Select?", String.Empty));
            DDLdegisfull.SelectedIndex = 0;

            ListBizin.Items.Clear();
            DataTable DTizin = AnaKlas.GetDataTable("Select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi from pilotlar where izinde='1' and emekli = 'No' and kapsirano<'1000'  and varid='3' order by kapno");
            ListBizin.DataTextField = "kapadisoyadi";
            ListBizin.DataValueField = "kapno";
            ListBizin.DataSource = DTizin;
            ListBizin.DataBind();

            ListBdegis.Items.Clear();
            DataTable DTdegis = AnaKlas.GetDataTable("Select degismecikapno, degismeciadi +' '+ degismecisoyadi as degismeciadisoyadi from pilotlar where (izinde='1' and emekli = 'No' and kapsirano<'1000' and varid='3') order by kapno");
            ListBdegis.DataTextField = "degismeciadisoyadi";
            ListBdegis.DataValueField = "degismecikapno";
            ListBdegis.DataSource = DTdegis;
            ListBdegis.DataBind();


            for (int i = 0; i < ListBizin.Items.Count; i++)
            {

                SqlCommand cmddurumbak = new SqlCommand("SP_PilotGorevBak", baglanti);
                cmddurumbak.CommandType = CommandType.StoredProcedure;
                cmddurumbak.Parameters.AddWithValue("@secilikapno", ListBizin.Items[i].Value);
                cmddurumbak.Parameters.Add("@gorevde", SqlDbType.Char, 1);
                cmddurumbak.Parameters["@gorevde"].Direction = ParameterDirection.Output;
                cmddurumbak.ExecuteNonQuery();
                string gorev = cmddurumbak.Parameters["@gorevde"].Value.ToString().Trim();
                cmddurumbak.Dispose();

                if (gorev == "0") // değişmeci gelmiş (izinde=1, gorevde=1 olur)
                {
                    ListBdegis.Items[i].Attributes.Add("style", "background-color: Yellow !important;");
                    ListBizin.Items[i].Attributes.Add("style", "background-color: Yellow !important;");

                    // bunları sağ sol sarı yap
                }
                else if (gorev == "1") // direkt izindedir (izinde=1, gorevde=1 olur)
                {
                    ListBdegis.Items[i].Text = "";
                }
                else if (gorev == "2") // hasta olmuş (izinde=1, gorevde=2 olur)
                {
                    ListBdegis.Items[i].Text = "";
                    ListBizin.Items[i].Attributes.Add("style", "background-color: Red !important;");
                }
                else if (gorev == "3") // add pilot diğer vardiyadan gelmiş (izinde=1, gorevde=3 olur)
                {
                    ListBizin.Items[i].Text = "";
                    ListBdegis.Items[i].Attributes.Add("style", "background-color: Green !important;");
                }
            }

            DDLdegisfull.Enabled = false;
            DDLdegisfull.SelectedIndex = -1;
            ListBizin.SelectedIndex = -1;
            ListBdegis.SelectedIndex = -1;

            Buttonizinver.Enabled = false;
            Buttonizinver.Visible = true;
            Buttonizinyok.Visible = true;
            Buttonizinsil.Visible = false;
            baglanti.Close();
            baglanti.Dispose();
            this.ModalPopupizinli.Show();

        }
    }
    protected void Baccepted_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (CBaddpilot.Checked == true)
        {
            if (DDLdegisfull.SelectedItem.Value != null && DDLdegisfull.SelectedItem.Value != "" && DDLdegisfull.SelectedItem.Value != "-1" && DDLdegisfull.SelectedItem.Value != "0")
            {
                int kapnoaladd = Convert.ToInt32(DDLdegisfull.SelectedItem.Value);

                //pilotvardiya tan max varno bul
                SqlCommand cmdmaxvarnobul = new SqlCommand("Select varno from pilotvardiya where id =(select max(id) from pilotvardiya where kapno='"+kapnoaladd+"') ", baglanti);
                string maxvarno = cmdmaxvarnobul.ExecuteScalar().ToString();
                cmdmaxvarnobul.Dispose();



                //pilot hangi vardiyaya aitse pilotalrdan varid bul
                SqlCommand cmdpilotlarvaridbak = new SqlCommand("SP_varvaridoku", baglanti);
                cmdpilotlarvaridbak.CommandType = CommandType.StoredProcedure;
                cmdpilotlarvaridbak.Parameters.AddWithValue("@secilikapno", kapnoaladd);
                cmdpilotlarvaridbak.Parameters.Add("@varidoku", SqlDbType.Char, 1);
                cmdpilotlarvaridbak.Parameters["@varidoku"].Direction = ParameterDirection.Output;
                cmdpilotlarvaridbak.ExecuteNonQuery();
                string varidok = cmdpilotlarvaridbak.Parameters["@varidoku"].Value.ToString().Trim();
                cmdpilotlarvaridbak.Dispose();

                if (AnaKlas.varidhesapla() != varidok ) // kendi vardiyanın pilotu değilse ekler
                { 

                SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
                cmdPilotismial.CommandType = CommandType.StoredProcedure;
                cmdPilotismial.Parameters.AddWithValue("@secilikapno", kapnoaladd);
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

                //değişen,izinlileri sıfırla
                SqlCommand cmdiziniptali = new SqlCommand("Update pilotlar set degismecikapno = kapno,  degismeciadi = kapadi, degismecisoyadi = kapsoyadi, kidem = degismeciorgkidem, izinde='1', gorevde='3' where kapsirano <1000 and kapno ='" + kapnoaladd + "' ", baglanti);
                cmdiziniptali.ExecuteNonQuery();
                cmdiziniptali.Dispose();

                //pilotlar için varno güncelle 
                SqlCommand cmdvarno = new SqlCommand("update pilotlar set varno=@varno, istasyongelis=@istasyongelis where emekli='No' and kapsirano<'1000' and kapno=" + kapnoaladd, baglanti);
                cmdvarno.Parameters.AddWithValue("varno", varbilvarno.Text);
                cmdvarno.Parameters.AddWithValue("istasyongelis", varbaslar.Text);
                cmdvarno.ExecuteNonQuery();
                cmdvarno.Dispose();

                //girişistasyon/respist,  eşitleme
                SqlCommand cmddegismeci = new SqlCommand("update pilotlar set  respist=(Select girisistasyon from pilotlar where kapno='" + kapnoaladd + "')  where kapno='" + kapnoaladd + "'", baglanti);
                cmddegismeci.ExecuteNonQuery();
                cmddegismeci.Dispose();


                //pilot vardiyaya isim ekleme

                if(varbilvarno.Text != maxvarno) // pilot daha önce eklenmemişse ekler
                    { 
                SqlCommand cmdkapnoadd = new SqlCommand("insert into pilotvardiya (kapno,kapadisoyadi,varno,toplamissayisi,toplamissuresi,toplamdinlenme,toplamzihyor,lastday,lastseven,sevensayi,daysayi,yorgungitteni,yorgungitalli,yorulma,yorulmalast) values (@kapno,@kapadisoyadi,@varno,@toplamissayisi,@toplamissuresi,@toplamdinlenme,@toplamzihyor,@lastday,@lastseven,@sevensayi,@daysayi,@yorgungitteni,@yorgungitalli,@yorulma,@yorulmalast)", baglanti);
                cmdkapnoadd.Parameters.AddWithValue("kapno", kapnoaladd);
                cmdkapnoadd.Parameters.AddWithValue("kapadisoyadi", kapadisoyadi);
                cmdkapnoadd.Parameters.AddWithValue("varno", varbilvarno.Text);
                cmdkapnoadd.Parameters.AddWithValue("toplamissayisi", 0);
                cmdkapnoadd.Parameters.AddWithValue("toplamissuresi", 0);
                cmdkapnoadd.Parameters.AddWithValue("toplamdinlenme", 0); //toplam zihinsel yorulma
                cmdkapnoadd.Parameters.AddWithValue("toplamzihyor", 0); // kullanılmıyor
                cmdkapnoadd.Parameters.AddWithValue("lastday", 24);
                cmdkapnoadd.Parameters.AddWithValue("lastseven", 0);
                cmdkapnoadd.Parameters.AddWithValue("sevensayi", 0);
                cmdkapnoadd.Parameters.AddWithValue("daysayi", 0);
                cmdkapnoadd.Parameters.AddWithValue("yorgungitteni", 0);
                cmdkapnoadd.Parameters.AddWithValue("yorgungitalli", 0);
                cmdkapnoadd.Parameters.AddWithValue("yorulma", 0);
                cmdkapnoadd.Parameters.AddWithValue("yorulmalast", 0);
                cmdkapnoadd.ExecuteNonQuery();
                cmdkapnoadd.Dispose();
                    }

                    // 9.pilot okunuyor // eklenen pilot için 2. ikinci pilot bilgisi alındı
                    SqlCommand cmddokuzuncu = new SqlCommand("SP_pilotvardiya_do_ikincipilot", baglanti);
                cmddokuzuncu.CommandType = CommandType.StoredProcedure;
                cmddokuzuncu.Parameters.AddWithValue("@varbilvarno", varbilvarno.Text);
                SqlDataReader fatrdrhas = cmddokuzuncu.ExecuteReader();

                decimal toplamissuresi = 0;
                decimal toplamdinlenme = 0;
                float toplamzihyor = 0;
                decimal lastday = 24;
                decimal lastseven = 0;
                int sevensayi = 0;
                int daysayi = 0;
                int yorgungitteni = 0;
                int yorgungitalli = 0;
                decimal yorulma = 0;

                if (fatrdrhas.HasRows)
                {
                    while (fatrdrhas.Read())
                    {
                        toplamissuresi = Convert.ToDecimal(fatrdrhas["toplamissuresi"].ToString());
                        toplamdinlenme = Convert.ToDecimal(fatrdrhas["toplamdinlenme"].ToString());
                        toplamzihyor = float.Parse(fatrdrhas["toplamzihyor"].ToString());
                        lastday = Convert.ToDecimal(fatrdrhas["lastday"].ToString());
                        lastseven = Convert.ToDecimal(fatrdrhas["lastseven"].ToString());
                        sevensayi = Convert.ToInt32(fatrdrhas["sevensayi"].ToString());
                        daysayi = Convert.ToInt32(fatrdrhas["daysayi"].ToString());
                        yorgungitteni = Convert.ToInt32(fatrdrhas["yorgungitteni"].ToString());
                        yorgungitalli = Convert.ToInt32(fatrdrhas["yorgungitalli"].ToString());
                        yorulma = Convert.ToDecimal(fatrdrhas["yorulma"].ToString());
                    }
                }
                fatrdrhas.Close();

                //add pilota  9.pilot bilgileri yazılıyor
                SqlCommand cmdpilvarup = new SqlCommand("SP_Pilotvardiya_UpAll", baglanti);
                cmdpilvarup.CommandType = CommandType.StoredProcedure;
                cmdpilvarup.Parameters.AddWithValue("@kapno", kapnoaladd);
                cmdpilvarup.Parameters.AddWithValue("@varno", varbilvarno.Text);
                cmdpilvarup.Parameters.AddWithValue("@toplamissayisi", 0);
                cmdpilvarup.Parameters.AddWithValue("@toplamissuresi", 0);
                cmdpilvarup.Parameters.AddWithValue("@toplamdinlenme", toplamdinlenme + toplamissuresi);
                cmdpilvarup.Parameters.AddWithValue("@toplamzihyor", toplamzihyor);
                cmdpilvarup.Parameters.AddWithValue("@lastday", lastday);
                cmdpilvarup.Parameters.AddWithValue("@lastseven", lastseven);
                cmdpilvarup.Parameters.AddWithValue("@sevensayi", sevensayi);
                cmdpilvarup.Parameters.AddWithValue("@daysayi", daysayi);
                cmdpilvarup.Parameters.AddWithValue("@yorgungitteni", yorgungitteni);
                cmdpilvarup.Parameters.AddWithValue("@yorgungitalli", yorgungitalli);
                cmdpilvarup.Parameters.AddWithValue("@yorulma", yorulma);

                cmdpilvarup.ExecuteNonQuery();
                cmdpilvarup.Dispose();
                }
            }



        }

        else
        {

            if (DDLdegisfull.SelectedItem.Value == "0" || DDLdegisfull.SelectedItem.Value == "" || DDLdegisfull.SelectedItem.Value == "-1" || DDLdegisfull.SelectedItem.Value == null)
            {//direkt izindedir (izinde=1, gorevde=1 olur)

                if (DDLizinlifull.SelectedItem.Value != "0" && DDLizinlifull.SelectedItem.Value != "" && DDLizinlifull.SelectedItem.Value != "-1" && DDLizinlifull.SelectedItem.Value != null)
                {
                    int kapnoaliz = Convert.ToInt32(DDLizinlifull.SelectedItem.Value);

                    SqlCommand cmdizinsave = new SqlCommand("Update pilotlar set izinde='1', gorevde='1'  where kapno =" + kapnoaliz, baglanti);
                    cmdizinsave.ExecuteNonQuery();
                    cmdizinsave.Dispose();
                }

            }
            else//değişmeci seçilmiş
            {
                if (DDLizinlifull.SelectedItem.Value != "0" && DDLizinlifull.SelectedItem.Value != "" && DDLizinlifull.SelectedItem.Value != "-1" && DDLizinlifull.SelectedItem.Value != null)
                {
                    int kapnoaldeg = Convert.ToInt32(DDLdegisfull.SelectedItem.Value);
                    int kapnoaliz = Convert.ToInt32(DDLizinlifull.SelectedItem.Value);

                    if (kapnoaldeg == kapnoaliz)// pilot kendi ismine değişmeci seçilirse hasta olur (izinde=1, gorevde=2 olur)
                    {
                        SqlCommand cmdizinsaved = new SqlCommand("Update pilotlar set izinde='1', gorevde='2' where kapno =" + kapnoaliz, baglanti);
                        cmdizinsaved.ExecuteNonQuery();
                        cmdizinsaved.Dispose();
                    }
                    else // normal değişmeci (izinde=1, gorevde=0 olur)
                    {
                        SqlCommand cmdizinkidsave = new SqlCommand("Update pilotlar set degismeciorgkidem = kidem where kapno =" + kapnoaliz, baglanti);
                        cmdizinkidsave.ExecuteNonQuery();
                        cmdizinkidsave.Dispose();

                        SqlCommand cmdizinsave = new SqlCommand("Update pilotlar set degismecikapno = ('" + kapnoaldeg + "'),  degismeciadi = (Select kapadi from pilotlar where kapno = '" + kapnoaldeg + "'), degismecisoyadi = (Select kapsoyadi from pilotlar where kapno = '" + kapnoaldeg + "'), kidem = (Select kidem from pilotlar where kapno = '" + kapnoaldeg + "'), izinde='1', gorevde='0'  where kapno =" + kapnoaliz, baglanti);
                        cmdizinsave.ExecuteNonQuery();
                        cmdizinsave.Dispose();
                    }
                }
            }
        }
        DTloading(baglanti);
        baglanti.Close();
        izinliyenile();
    }
    protected void CBaddpilot_CheckedChanged(object sender, EventArgs e)
    {
        if (CBaddpilot.Checked == true)
        {
            izinliyenileic();

            wac1.Enabled = false;
            wac2.Enabled = false;
            wac3.Enabled = false;
            wac1.Checked = false;
            wac2.Checked = false;
            wac3.Checked = false;

            DDLizinlifull.Enabled = false;
            DDLizinlifull.SelectedIndex = -1;
            DDLdegisfull.Enabled = true;
            DDLdegisfull.SelectedIndex = -1;

        }
        else if (CBaddpilot.Checked == false)
        {
            izinliyenileic();
            wac1.Enabled = true;
            wac2.Enabled = true;
            wac3.Enabled = true;
            Buttonizinver.Enabled = false;

            if (varbilvarid.Text == "1") { wac1.Checked = true; }
            else if (varbilvarid.Text == "2") { wac2.Checked = true; }
            else if (varbilvarid.Text == "3") { wac3.Checked = true; }

            DDLizinlifull.Enabled = true;
            DDLizinlifull.SelectedIndex = -1;
            DDLdegisfull.Enabled = false;
            DDLdegisfull.SelectedIndex = -1;
        }


        this.ModalPopupizinli.Show();
    }

    //------ izin durumları bitti

    protected void DDCjob_SelectedIndexChanged(object sender, EventArgs e)
    {
        LtCjobText.Visible = true;

        if (DDCjob.SelectedItem.Text == "Yes")
        {
            DDCjobReason.Visible = true;
            DDCjobReason.Text = "";
        }
        else
        {
            DDCjobReason.Visible = false;
        }


        this.ModalPopupCSonayMessage.Show();
    }

    //------ takviye durumları baslar
    protected void ButtonTKVYsp_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        TBtaktaleptime.Text = TarihSaatYaziYapDMYhm(DateTime.Now);
        TBtaktaleptime.BorderColor = System.Drawing.Color.Gray;
        TBtaktaleptime.BorderWidth = 1;
        LitTakdeptime.Text = "Required Dep.Time : ";
        ListBoxDarica.Enabled = false;
        ListBoxYarimca.Enabled = false;
        ButtonTistekiptal.Visible = false;
        TBtaktaleptime.Enabled = true;

        ListBoxDarica.Items.Clear();
        ListBoxYarimca.Items.Clear();

        DataTable DTDaricaTak = AnaKlas.GetDataTable("Select pilotlar.kapno, degismeciadi +' '+ degismecisoyadi as degismeciadisoyadi from pilotlar join pilotvardiya on pilotlar.kapno=pilotvardiya.kapno and  pilotlar.varno=pilotvardiya.varno where (pilotlar.kapsirano<'1000' and pilotlar.izinde='0' and pilotlar.respist='1' and pilotlar.durum='0' and pilotlar.varno='" + varbilvarno.Text + "') or (pilotlar.kapsirano<'1000' and (pilotlar.izinde='1') and pilotlar.durum='0'  and pilotlar.kapno!=pilotlar.degismecikapno and pilotlar.respist='1'  and pilotlar.varno='" + varbilvarno.Text + "') or (pilotlar.kapsirano<'1000' and pilotlar.izinde='1' and pilotlar.gorevde = '3' and pilotlar.kapno=pilotlar.degismecikapno and pilotlar.durum='0'   and pilotlar.respist='1' and pilotlar.varno='" + varbilvarno.Text + "')  order by [durum] asc, [daysayi] asc, [sevensayi] desc, [yorulma] asc, [yorulmalast] asc  ");

        ListBoxDarica.DataTextField = "degismeciadisoyadi";
        ListBoxDarica.DataValueField = "kapno";
        ListBoxDarica.DataSource = DTDaricaTak;
        ListBoxDarica.DataBind();

        DataTable DTYarimcaTak = AnaKlas.GetDataTable("Select pilotlar.kapno, degismeciadi +' '+ degismecisoyadi as degismeciadisoyadi from pilotlar join pilotvardiya on pilotlar.kapno=pilotvardiya.kapno and  pilotlar.varno=pilotvardiya.varno  where (pilotlar.kapsirano<'1000' and pilotlar.izinde='0' and pilotlar.respist='2' and pilotlar.durum='0' and pilotlar.varno='" + varbilvarno.Text + "') or (pilotlar.kapsirano<'1000' and pilotlar.izinde='1' and  pilotlar.durum='0' and pilotlar.kapno!=pilotlar.degismecikapno   and pilotlar.respist='2' and pilotlar.varno='" + varbilvarno.Text + "') or (pilotlar.kapsirano<'1000' and pilotlar.izinde='1' and pilotlar.gorevde = '3' and pilotlar.kapno=pilotlar.degismecikapno and pilotlar.durum='0'   and pilotlar.respist='2' and pilotlar.varno='" + varbilvarno.Text + "') order by [durum] asc, [daysayi] asc, [sevensayi] desc, [yorulma] asc, [yorulmalast] asc  ");
        ListBoxYarimca.DataTextField = "degismeciadisoyadi";
        ListBoxYarimca.DataValueField = "kapno";
        ListBoxYarimca.DataSource = DTYarimcaTak;
        ListBoxYarimca.DataBind();

        if (ListBoxDarica.Items.Count.ToString() == "0") { ButtonTgonder.CommandArgument = "0"; }
        else { ButtonTgonder.CommandArgument = ListBoxDarica.Items.Count.ToString(); }

        SqlCommand takistekokucmd = new SqlCommand("Select durum from takviyelog where id =(select max(id) from takviyelog) ", baglanti);
        string takistekoku = takistekokucmd.ExecuteScalar().ToString();
        takistekokucmd.Dispose();
        if (takistekoku == "2") // durum 1 ise istek yapılmış 2 ise kabul edilmiştir
        {
            ButtonTgonder.Visible = false;
            ButtonTiste.Visible = true;
        }
        else if (takistekoku == "1")
        {
            SqlCommand taktimebakcmd = new SqlCommand("Select istenensaat from takviyelog where id =(select max(id) from takviyelog) ", baglanti);
            string taktimebak = taktimebakcmd.ExecuteScalar().ToString();
            taktimebakcmd.Dispose();

            SqlCommand takbakvercmd = new SqlCommand("Select isteyenistyetki from takviyelog where id =(select max(id) from takviyelog) ", baglanti);
            string takbakver = takbakvercmd.ExecuteScalar().ToString();
            takbakvercmd.Dispose();

            ButtonTgonder.Visible = true;
            ButtonTiste.Visible = false;
            ButtonTistekiptal.Visible = false;
            TBtaktaleptime.Text = taktimebak;
            ListBoxDarica.Enabled = true;
            ListBoxYarimca.Enabled = true;

            if (takbakver == "1") // durum 1 ise istek yapan darıca
            {
                if (Session["yetki"].ToString() == "1")
                {
                    ListBoxDarica.Enabled = false;
                    ListBoxYarimca.Enabled = false;
                    ButtonTgonder.Visible = false;
                    ButtonTistekiptal.Visible = true;
                    ButtonTiste.Visible = false;
                    TBtaktaleptime.Enabled = false;
                }
            }
            else if (takbakver == "2") // durum 2 ise istek yapan yarımca
            {
                if (Session["yetki"].ToString() == "2")
                {
                    ButtonTgonder.Visible = false;
                    ButtonTistekiptal.Visible = true;
                    ButtonTiste.Enabled = false;
                    TBtaktaleptime.Enabled = false;
                }
            }
        }

        baglanti.Close();
        this.ModalPopupTakviye.Show();
    }
    protected void ButtonTgonder_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        LitTakuyari.Visible = false;

        if (TBtaktaleptime.Text == "" || TBtaktaleptime.Text == null || TBtaktaleptime.Text == "__.__.____ __:__")
        {
            TBtaktaleptime.BorderColor = System.Drawing.Color.Red;
            TBtaktaleptime.BorderWidth = 1;
            this.ModalPopupTakviye.Show();
        }
        else if (IsDate2(TBtaktaleptime.Text) != true)
        {
            TBtaktaleptime.BorderColor = System.Drawing.Color.Red;
            TBtaktaleptime.BorderWidth = 1;
            this.ModalPopupTakviye.Show();
        }
        else if (AnaKlas.TarihSaatYapDMYhm(TBtaktaleptime.Text) < DateTime.Now || AnaKlas.TarihSaatYapDMYhm(TBtaktaleptime.Text) > DateTime.Now.AddHours(6))
        {
            TBtaktaleptime.BorderColor = System.Drawing.Color.Red;
            TBtaktaleptime.BorderWidth = 1;
            this.ModalPopupTakviye.Show();
        }
        else if (ListBoxDarica.Items.Count.ToString() == ButtonTgonder.CommandArgument.ToString())
        {
            LitTakuyari.Visible = true;
            LitTakuyari.Text = "Please select and send pilots first in the list.";
            this.ModalPopupTakviye.Show();
        }
        else
        {
            DateTime istasyoncikisd = AnaKlas.TarihSaatYapDMYhm(TBtaktaleptime.Text.ToString());
            DateTime istasyongelisd = istasyoncikisd.AddMinutes(50); //timei 50 idi direk değerini verdim. 1090.satırdada iki tane var

            if (ListBoxDarica.Items.Count.ToString() != ButtonTgonder.CommandArgument.ToString())
            {      // Darıca listboax.
                for (int i = 0; i < ListBoxDarica.Items.Count; i++)
                {
                    String kapnoal = ListBoxDarica.Items[i].Value.ToString();
                    int kapnoali = Convert.ToInt32(kapnoal);
                    SqlCommand cmdkontrol = new SqlCommand("Select respist from pilotlar where kapno =" + kapnoali, baglanti);
                    string cikiskontrol = cmdkontrol.ExecuteScalar().ToString();

                    if (cikiskontrol != "1")
                    {
                        string istasyoncikis = TarihSaatYaziYapDMYhm(istasyoncikisd);
                        string Pob = istasyoncikis;
                        string istasyongelis = TarihSaatYaziYapDMYhm(istasyongelisd);
                        string Poff = istasyongelis;
                        string durum = "1";
                        string gemiadi = "TAKVİYE";
                        string binisyeri = "Yarımca İstasyon";
                        string binisrihtim = "0";
                        string inisyeri = "Darıca İstasyon";
                        string inisrihtim = "0";
                        //string respist = "1";

                        SqlCommand cmdpason = new SqlCommand("update pilotlar set durum=@durum,imono=@imono,gemiadi=@gemiadi,binisyeri=@binisyeri,binisrihtim=@binisrihtim,inisyeri=@inisyeri,inisrihtim=@inisrihtim,istasyoncikis=@istasyoncikis,istasyongelis=@istasyongelis,Pob=@Pob,Poff=@Poff,gemiatamazamani=@gemiatamazamani where kapno=" + kapnoali, baglanti);
                        cmdpason.Parameters.AddWithValue("durum", durum);
                        cmdpason.Parameters.AddWithValue("imono", "9999999");
                        cmdpason.Parameters.AddWithValue("gemiadi", gemiadi);
                        cmdpason.Parameters.AddWithValue("binisyeri", binisyeri);
                        cmdpason.Parameters.AddWithValue("binisrihtim", binisrihtim);
                        cmdpason.Parameters.AddWithValue("inisyeri", inisyeri);
                        cmdpason.Parameters.AddWithValue("inisrihtim", inisrihtim);
                        cmdpason.Parameters.AddWithValue("istasyoncikis", istasyoncikis);
                        cmdpason.Parameters.AddWithValue("Pob", Pob);
                        cmdpason.Parameters.AddWithValue("istasyongelis", istasyongelis);
                        cmdpason.Parameters.AddWithValue("Poff", Poff);
                        cmdpason.Parameters.AddWithValue("gemiatamazamani", Pob);
                        cmdpason.ExecuteNonQuery();
                    }
                }

                // YArımca listbox.
                for (int i = 0; i < ListBoxYarimca.Items.Count; i++)
                {
                    String kapnoal = ListBoxYarimca.Items[i].Value.ToString();
                    int kapnoali = Convert.ToInt32(kapnoal);
                    SqlCommand cmdkontrol = new SqlCommand("Select respist from pilotlar where kapno =" + kapnoali, baglanti);
                    string cikiskontrol = cmdkontrol.ExecuteScalar().ToString();

                    if (cikiskontrol != "2")
                    {
                        string istasyoncikis = TarihSaatYaziYapDMYhm(istasyoncikisd);
                        string Pob = istasyoncikis;
                        string istasyongelis = TarihSaatYaziYapDMYhm(istasyongelisd);
                        string Poff = istasyongelis;
                        string durum = "1";
                        string gemiadi = "TAKVİYE";
                        string binisyeri = "Darıca İstasyon";
                        string binisrihtim = "0";
                        string inisyeri = "Yarımca İstasyon";
                        string inisrihtim = "0";
                        //string respist = "2";

                        SqlCommand cmdpason = new SqlCommand("update pilotlar set durum=@durum,imono=@imono,gemiadi=@gemiadi,binisyeri=@binisyeri,binisrihtim=@binisrihtim,inisyeri=@inisyeri,inisrihtim=@inisrihtim,istasyoncikis=@istasyoncikis,istasyongelis=@istasyongelis,Pob=@Pob,Poff=@Poff,gemiatamazamani=@gemiatamazamani  where kapno=" + kapnoali, baglanti);
                        cmdpason.Parameters.AddWithValue("durum", durum);
                        cmdpason.Parameters.AddWithValue("imono", "9999999");
                        cmdpason.Parameters.AddWithValue("gemiadi", gemiadi);
                        cmdpason.Parameters.AddWithValue("binisyeri", binisyeri);
                        cmdpason.Parameters.AddWithValue("binisrihtim", binisrihtim);
                        cmdpason.Parameters.AddWithValue("inisyeri", inisyeri);
                        cmdpason.Parameters.AddWithValue("inisrihtim", inisrihtim);
                        cmdpason.Parameters.AddWithValue("istasyoncikis", istasyoncikis);
                        cmdpason.Parameters.AddWithValue("Pob", Pob);
                        cmdpason.Parameters.AddWithValue("istasyongelis", istasyongelis);
                        cmdpason.Parameters.AddWithValue("Poff", Poff);
                        cmdpason.Parameters.AddWithValue("gemiatamazamani", Pob);
                        cmdpason.ExecuteNonQuery();
                    }
                }
            }

            SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
            cmdPilotismial.CommandType = CommandType.StoredProcedure;
            cmdPilotismial.Parameters.AddWithValue("@secilikapno", Convert.ToInt32(Session["kapno"]));
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

            SqlCommand takcikisonay = new SqlCommand("update takviyelog set verilensaat=@verilensaat,onaylayanopr=@onaylayanopr where id =(select max(id) from takviyelog)", baglanti);
            takcikisonay.Parameters.AddWithValue("onaylayanopr", kapadisoyadi);
            takcikisonay.Parameters.AddWithValue("verilensaat", TBtaktaleptime.Text.ToString());
            takcikisonay.ExecuteNonQuery();
            takcikisonay.Dispose();

            ButtonTKVYsp.ForeColor = System.Drawing.Color.Red; //System.Drawing.Color.FromName("gray");
        }

        DTloading(baglanti);
        baglanti.Close();
    }
    protected void ButtonTiste_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        if (TBtaktaleptime.Text == "" || TBtaktaleptime.Text == null || TBtaktaleptime.Text == "__.__.____ __:__")
        {
            TBtaktaleptime.BorderColor = System.Drawing.Color.Red;
            TBtaktaleptime.BorderWidth = 1;
            this.ModalPopupTakviye.Show();
        }
        else if (IsDate2(TBtaktaleptime.Text) != true)
        {
            TBtaktaleptime.BorderColor = System.Drawing.Color.Red;
            TBtaktaleptime.BorderWidth = 1;
            this.ModalPopupTakviye.Show();
        }
        else if (AnaKlas.TarihSaatYapDMYhm(TBtaktaleptime.Text) < DateTime.Now || AnaKlas.TarihSaatYapDMYhm(TBtaktaleptime.Text) > DateTime.Now.AddHours(6))
        {
            TBtaktaleptime.BorderColor = System.Drawing.Color.Red;
            TBtaktaleptime.BorderWidth = 1;
            this.ModalPopupTakviye.Show();
        }
        else
        {
            SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
            cmdPilotismial.CommandType = CommandType.StoredProcedure;
            cmdPilotismial.Parameters.AddWithValue("@secilikapno", Convert.ToInt32(Session["kapno"]));
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

            SqlCommand takistekonay = new SqlCommand("insert into takviyelog (isteyenistyetki,isteyenopr,istenensaat,durum) values (@isteyenistyetki,@isteyenopr,@istenensaat,@durum)", baglanti);
            takistekonay.Parameters.AddWithValue("isteyenistyetki", Session["yetki"].ToString());
            takistekonay.Parameters.AddWithValue("isteyenopr", kapadisoyadi);
            takistekonay.Parameters.AddWithValue("istenensaat", TBtaktaleptime.Text);
            takistekonay.Parameters.AddWithValue("durum", "1");
            takistekonay.ExecuteNonQuery();
            takistekonay.Dispose();

            ButtonTKVYsp.ForeColor = System.Drawing.Color.Red; //System.Drawing.Color.FromName("gray");
        }
        baglanti.Close();
    }
    protected void ButtonTistekiptal_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        string islemzamani = TarihSaatYaziYapDMYhm(DateTime.Now);

        SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
        cmdPilotismial.CommandType = CommandType.StoredProcedure;
        cmdPilotismial.Parameters.AddWithValue("@secilikapno", Convert.ToInt32(Session["kapno"]));
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

        SqlCommand takistekonay = new SqlCommand("update takviyelog set onaylayanopr=@onaylayanopr,verilensaat=@verilensaat,cikissaati=@cikissaati,varissaati=@varissaati,kimler=@kimler,durum=@durum where durum = '1' ", baglanti);
        takistekonay.Parameters.AddWithValue("onaylayanopr", kapadisoyadi);
        takistekonay.Parameters.AddWithValue("verilensaat", islemzamani);
        takistekonay.Parameters.AddWithValue("cikissaati", islemzamani);
        takistekonay.Parameters.AddWithValue("varissaati", islemzamani);
        takistekonay.Parameters.AddWithValue("kimler", "istek iptal");
        takistekonay.Parameters.AddWithValue("durum", "2");
        takistekonay.ExecuteNonQuery();
        takistekonay.Dispose();

        ButtonTKVYsp.ForeColor = System.Drawing.Color.Red; //System.Drawing.Color.FromName("gray");

        baglanti.Close();
    }
    protected void LBtakmover_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "1")
        {
            for (int k = ListBoxDarica.Items.Count - 1; k >= 0; k--)
            {
                if (ListBoxDarica.Items[k].Selected)
                {
                    ListBoxYarimca.Items.Add(ListBoxDarica.Items[k]);
                    ListBoxYarimca.SelectedValue = ListBoxDarica.Items[k].Value;
                    ListBoxDarica.Items.RemoveAt(k);
                }
            }
        }
        this.ModalPopupTakviye.Show();
    }
    protected void LBtakmovel_Click(object sender, EventArgs e)
    {
        if (Session["yetki"].ToString() == "2")
        {
            for (int k = ListBoxYarimca.Items.Count - 1; k >= 0; k--)
            {
                if (ListBoxYarimca.Items[k].Selected)
                {
                    ListBoxDarica.Items.Add(ListBoxYarimca.Items[k]);
                    ListBoxDarica.SelectedValue = ListBoxYarimca.Items[k].Value;
                    ListBoxYarimca.Items.RemoveAt(k);
                }
            }
        }
        this.ModalPopupTakviye.Show();
    }
    
    //-------takviye durumları bitti

    protected void ButtonChangeWatch_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["yetki"]) == 1)
        {
            if (AnaKlas.TarihSaatYapDMYhm(varbiter.Text) < (DateTime.Now))
            {
                varSummary();
                this.ModalPopupWatch.Show();
            }
            else
            {
                Litmodalmesssage.Text = "The Watch can be changed after 11:00 on the date of watch.";
                Baccepted.Visible = false;
                this.ModalPopupMessage.Show();
            }
        }
        else
        {
            Litmodalmesssage.Text = "The Watch can be changed only by Darıca Station.";
            Baccepted.Visible = false;
            this.ModalPopupMessage.Show();
        }
        SqlConnection baglanti = AnaKlas.baglan();
        DTloading(baglanti);
        baglanti.Close();
    }

    private void varSummary()
    {
        Lwid.Text = varbilvarid.Text;
        Lwfinish.Text = varbiter.Text;
    }


    protected void BWCacceptedok_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        //çıkan vard. değişen,izinlileri sil sıfırla
        string cikanvarid = AnaKlas.varidhesaplaonceki();
        SqlCommand cmdiziniptali = new SqlCommand("Update pilotlar set degismecikapno = kapno,  degismeciadi = kapadi, degismecisoyadi = kapsoyadi, kidem = degismeciorgkidem, izinde='0', gorevde='0' where kapsirano <1000 and varid ='" + cikanvarid + "' ", baglanti);
        cmdiziniptali.ExecuteNonQuery();
        cmdiziniptali.Dispose();

        //çıkan vard. eklenen pilotları değişen,izinlileri sil sıfırla
        SqlCommand cmdaddpilotiptali = new SqlCommand("Update pilotlar set degismecikapno = kapno,  degismeciadi = kapadi, degismecisoyadi = kapsoyadi, kidem = degismeciorgkidem, izinde='0', gorevde='0' where kapsirano <1000 and izinde='1' and gorevde='3' and imono='0' ", baglanti);
        cmdaddpilotiptali.ExecuteNonQuery();
        cmdaddpilotiptali.Dispose();

        //yeni vard.canli pilotlar için varno güncelle 
        SqlCommand cmdvarno = new SqlCommand("update pilotlar set varno=@varno, istasyongelis=@istasyongelis where emekli='No' and kapsirano<'1000' and gorevde!='3' and varid=" + AnaKlas.varidhesapla(), baglanti);
        cmdvarno.Parameters.AddWithValue("varno", AnaKlas.varnohesapla());
        cmdvarno.Parameters.AddWithValue("istasyongelis", TarihSaatYaziYapDMYhm(AnaKlas.varbaslangic()));
        cmdvarno.ExecuteNonQuery();
        cmdvarno.Dispose();

        //sabit vardiyabilgi guncelleme
        SqlCommand cmdvarsabit = new SqlCommand("update vardiyabilgi set varno=@varno,varid=@varid,varbaslar=@varbaslar,varbiter=@varbiter where id='1'", baglanti);
        cmdvarsabit.Parameters.AddWithValue("varno", AnaKlas.varnohesapla());
        cmdvarsabit.Parameters.AddWithValue("varid", AnaKlas.varidhesapla());
        cmdvarsabit.Parameters.AddWithValue("varbaslar", TarihSaatYaziYapDMYhm(AnaKlas.varbaslangic()));
        cmdvarsabit.Parameters.AddWithValue("varbiter", TarihSaatYaziYapDMYhm(AnaKlas.varbitis()));
        cmdvarsabit.ExecuteNonQuery();
        cmdvarsabit.Dispose();

        //litleride guncelle
        varbilvarid.Text = AnaKlas.varidhesapla();
        varbilvarno.Text = AnaKlas.varnohesapla();
        varbaslar.Text = TarihSaatYaziYapDMYhm(AnaKlas.varbaslangic());
        varbiter.Text = TarihSaatYaziYapDMYhm(AnaKlas.varbitis());


        //yeni pılot vardıya kapno ve varno ekleme
        SqlCommand cmdkapnodize = new SqlCommand("Select kapno from pilotlar where emekli='No' and kapsirano<'1000' and varid ='" + AnaKlas.varidhesapla() + "' ", baglanti);
        SqlDataReader kaprdr = cmdkapnodize.ExecuteReader();
        if (kaprdr.HasRows)
        {
            while (kaprdr.Read())
            {
                string kapnoal = kaprdr["kapno"].ToString();

                //sonyorulma al
                SqlConnection baglanti2 = AnaKlas.baglan2();
                SqlCommand cmdsonyorulmaal = new SqlCommand("Select yorulma from pilotvardiya where kapno ='" + kapnoal + "' and varno='" + AnaKlas.varnohesaplaonceki3() + "' ", baglanti2);
                decimal sonyorulma = 0;
                if (cmdsonyorulmaal.ExecuteScalar() != null)
                {
                    sonyorulma = Convert.ToDecimal(cmdsonyorulmaal.ExecuteScalar().ToString());
                }
                cmdsonyorulmaal.Dispose();


                //kapadi al
                SqlCommand cmdkapadial = new SqlCommand("Select kapadi from pilotlar where kapno ='" + kapnoal + "' ", baglanti2);
                string kapadi = "";
                if (cmdkapadial.ExecuteScalar() != null)
                {
                    kapadi = cmdkapadial.ExecuteScalar().ToString();
                }
                cmdkapadial.Dispose();

                //kapsoyadi al
                SqlCommand cmdkapsoyadial = new SqlCommand("Select kapsoyadi from pilotlar where kapno ='" + kapnoal + "' ", baglanti2);
                string kapsoyadi = "";
                if (cmdkapsoyadial.ExecuteScalar() != null)
                {
                    kapsoyadi = cmdkapsoyadial.ExecuteScalar().ToString();
                }
                cmdkapsoyadial.Dispose();

                //sonyorulma güncelleniyor
                SqlCommand cmdkayitkontrol = new SqlCommand("Select varno from pilotvardiya where kapno ='" + kapnoal + "' and varno='" + AnaKlas.varnohesapla() + "' ", baglanti2);
                if (cmdkayitkontrol.ExecuteScalar() == null)
                {
                    SqlCommand cmdkapnoadd = new SqlCommand("insert into pilotvardiya (kapno,kapadisoyadi,varno,toplamissayisi,toplamissuresi,toplamdinlenme,toplamzihyor,lastday,lastseven,sevensayi,daysayi,yorgungitteni,yorgungitalli,yorulma,yorulmalast) values (@kapno,@kapadisoyadi,@varno,@toplamissayisi,@toplamissuresi,@toplamdinlenme,@toplamzihyor,@lastday,@lastseven,@sevensayi,@daysayi,@yorgungitteni,@yorgungitalli,@yorulma,@yorulmalast)", baglanti2);
                    cmdkapnoadd.Parameters.AddWithValue("kapno", kapnoal);
                    cmdkapnoadd.Parameters.AddWithValue("kapadisoyadi", kapadi + ' ' + kapsoyadi);
                    cmdkapnoadd.Parameters.AddWithValue("varno", AnaKlas.varnohesapla());
                    cmdkapnoadd.Parameters.AddWithValue("toplamissayisi", 0);
                    cmdkapnoadd.Parameters.AddWithValue("toplamissuresi", 0);
                    cmdkapnoadd.Parameters.AddWithValue("toplamdinlenme", 0); //toplam zihinsel yorulma
                    cmdkapnoadd.Parameters.AddWithValue("toplamzihyor", 0); // kullanılmıyor
                    cmdkapnoadd.Parameters.AddWithValue("lastday", 24);
                    cmdkapnoadd.Parameters.AddWithValue("lastseven", 0);
                    cmdkapnoadd.Parameters.AddWithValue("sevensayi", 0);
                    cmdkapnoadd.Parameters.AddWithValue("daysayi", 0);
                    cmdkapnoadd.Parameters.AddWithValue("yorgungitteni", 0);
                    cmdkapnoadd.Parameters.AddWithValue("yorgungitalli", 0);
                    cmdkapnoadd.Parameters.AddWithValue("yorulma", 0);
                    cmdkapnoadd.Parameters.AddWithValue("yorulmalast", sonyorulma);
                    cmdkapnoadd.ExecuteNonQuery();
                    cmdkapnoadd.Dispose();
                }

                //yeni vardiya pilotları girişistasyon/respist,  eşitleme
                SqlCommand cmddegismeci = new SqlCommand("update pilotlar set  respist=(Select girisistasyon from pilotlar where kapno='" + kapnoal + "')  where kapno='" + kapnoal + "' and gorevde!='3' ", baglanti2);
                cmddegismeci.ExecuteNonQuery();
                cmddegismeci.Dispose();

                baglanti2.Close();
            }
        }

        kaprdr.Close();

        //çıkan vardiyaya servis time ekle
        string varnocikan = AnaKlas.varnohesaplaonceki();

        SqlCommand sercmdkapnodize = new SqlCommand("Select kapno from pilotlar where  varno='" + varnocikan + "' and respist!=girisistasyon ", baglanti);
        SqlDataReader serkaprdr = sercmdkapnodize.ExecuteReader();
        if (serkaprdr.HasRows)
        {
            while (serkaprdr.Read())
            {
                string kapnoal = serkaprdr["kapno"].ToString();

                //toplamdinlenme al
                decimal toplamdinlenme = 0;

                SqlConnection baglanti2 = AnaKlas.baglan2();
                SqlCommand cmdtopdinal = new SqlCommand("Select toplamdinlenme from pilotvardiya where kapno ='" + kapnoal + "' and varno='" + varnocikan + "' ", baglanti2);
                if (cmdtopdinal.ExecuteScalar() != null)
                {
                    toplamdinlenme = Convert.ToDecimal(cmdtopdinal.ExecuteScalar().ToString());
                }
                cmdtopdinal.Dispose();

                toplamdinlenme = toplamdinlenme + Convert.ToDecimal(0.75);

                SqlCommand sercmddegismeci = new SqlCommand("update pilotvardiya set toplamdinlenme=@toplamdinlenme  where kapno='" + kapnoal + "'  and varno='" + varnocikan + "'  ", baglanti2);
                sercmddegismeci.Parameters.AddWithValue("toplamdinlenme", toplamdinlenme);
                sercmddegismeci.ExecuteNonQuery();
                sercmddegismeci.Dispose();

                baglanti2.Close();
            }
        }

        serkaprdr.Close();


        DTloading(baglanti);
        baglanti.Close();
    }


    protected void LBDuyuru_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand cmdduydayoku = new SqlCommand("SP_duydailyoku", baglanti); //enson duyuru oku
        cmdduydayoku.CommandType = CommandType.StoredProcedure;
        cmdduydayoku.Parameters.AddWithValue("@aktif", "1");
        SqlDataReader limread = cmdduydayoku.ExecuteReader();
        string duyuru = "";

        if (limread.HasRows)
        {
            while (limread.Read())
            {
                duyuru = limread["duyuru"].ToString();
            }
        }
        limread.Close();
        cmdduydayoku.Dispose();

        if (duyuru == null || duyuru == "")
        {
            TBDuyuru.Text = "";
        }
        else
        {
            TBDuyuru.Text = duyuru;
        }

        baglanti.Close();


        LblDuyuru.Visible = false;
        TBDuyuru.Visible = true;
        LBDkaydet.Visible = true;
        LBDCancel.Visible = true;
        LBLleftch.Visible = true;
        remLen.Visible = true;

    }
    protected void LBDkaydet_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand cmdPilotismial = new SqlCommand("SP_Pilotismial", baglanti);
        cmdPilotismial.CommandType = CommandType.StoredProcedure;
        cmdPilotismial.Parameters.AddWithValue("@secilikapno", Convert.ToInt32(Session["kapno"]));
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


        SqlCommand cmdduydayoku = new SqlCommand("SP_duydailyoku", baglanti); //enson duyuru oku
        cmdduydayoku.CommandType = CommandType.StoredProcedure;
        cmdduydayoku.Parameters.AddWithValue("@aktif", "1");
        SqlDataReader limread = cmdduydayoku.ExecuteReader();
        string duyuru = "";

        if (limread.HasRows)
        {
            while (limread.Read())
            {
                duyuru = limread["duyuru"].ToString();
            }
        }
        limread.Close();
        cmdduydayoku.Dispose();

        if (duyuru == null || duyuru == "")
        {
            if (TBDuyuru.Text == "")
            {
            }
            else
            {
                string TBDuyuruYazi = "";
                if (TBDuyuru.Text.Length > 5)
                {
                    TBDuyuruYazi = TBDuyuru.Text;
                }
                else
                {
                    TBDuyuruYazi = "";
                }
                SqlCommand cmdistgelup = new SqlCommand("SP_duyuru_Yaz", baglanti); //insert
                cmdistgelup.CommandType = CommandType.StoredProcedure;
                cmdistgelup.Parameters.AddWithValue("@aktif", "1");
                cmdistgelup.Parameters.AddWithValue("@duyuru", TBDuyuruYazi);
                cmdistgelup.Parameters.AddWithValue("@kaydeden", kapadisoyadi);
                cmdistgelup.Parameters.AddWithValue("@kayittarihi", TarihSaatYaziYapDMYhm(DateTime.Now));
                cmdistgelup.Parameters.AddWithValue("@iptaltarihi", "");
                cmdistgelup.ExecuteNonQuery();
                cmdistgelup.Dispose();
            }
        }
        else
        {
            if (TBDuyuru.Text == "" || TBDuyuru.Text.Length < 5)
            {
                SqlCommand cmdTakvarsaatup = new SqlCommand("SP_duyuru_UP_ipt2li", baglanti);
                cmdTakvarsaatup.CommandType = CommandType.StoredProcedure;
                cmdTakvarsaatup.Parameters.AddWithValue("@iptaltarihi", TarihSaatYaziYapDMYhm(DateTime.Now));
                cmdTakvarsaatup.Parameters.AddWithValue("@aktif", "0");
                cmdTakvarsaatup.ExecuteNonQuery();
                cmdTakvarsaatup.Dispose();
            }
            else
            {
                if (TBDuyuru.Text == duyuru)
                {
                }

                else
                {
                    SqlCommand cmdTakvarsaatup = new SqlCommand("SP_duyuru_UP_ipt2li", baglanti);
                    cmdTakvarsaatup.CommandType = CommandType.StoredProcedure;
                    cmdTakvarsaatup.Parameters.AddWithValue("@iptaltarihi", TarihSaatYaziYapDMYhm(DateTime.Now));
                    cmdTakvarsaatup.Parameters.AddWithValue("@aktif", "0");
                    cmdTakvarsaatup.ExecuteNonQuery();
                    cmdTakvarsaatup.Dispose();

                    SqlCommand cmdistgelup = new SqlCommand("SP_duyuru_Yaz", baglanti); //insert
                    cmdistgelup.CommandType = CommandType.StoredProcedure;
                    cmdistgelup.Parameters.AddWithValue("@aktif", "1");
                    cmdistgelup.Parameters.AddWithValue("@duyuru", TBDuyuru.Text);
                    cmdistgelup.Parameters.AddWithValue("@kaydeden", kapadisoyadi);
                    cmdistgelup.Parameters.AddWithValue("@kayittarihi", TarihSaatYaziYapDMYhm(DateTime.Now));
                    cmdistgelup.Parameters.AddWithValue("@iptaltarihi", "");
                    cmdistgelup.ExecuteNonQuery();
                    cmdistgelup.Dispose();
                }
            }
        }


        LblDuyuru.Visible = true;
        TBDuyuru.Visible = false;
        LBDkaydet.Visible = false;
        LBDCancel.Visible = false;
        LBLleftch.Visible = false;
        remLen.Visible = false;

        DTloading(baglanti);
        baglanti.Close();

    }
    protected void LBDCancel_Click(object sender, EventArgs e)
    {
        LblDuyuru.Visible = true;
        TBDuyuru.Visible = false;
        LBDkaydet.Visible = false;
        LBDCancel.Visible = false;
        LBLleftch.Visible = false;
        remLen.Visible = false;
    }


    protected void LBjdaily_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand cmdduydayoku = new SqlCommand("SP_duydailyoku", baglanti); //enson duyuru oku
        cmdduydayoku.CommandType = CommandType.StoredProcedure;
        cmdduydayoku.Parameters.AddWithValue("@aktif", "2");
        SqlDataReader limread = cmdduydayoku.ExecuteReader();

        string jnotdaily = "";
        string iptaltarihi = "";

        if (limread.HasRows)
        {
            while (limread.Read())
            {
                jnotdaily = limread["duyuru"].ToString();
                iptaltarihi = limread["iptaltarihi"].ToString();
            }
        }
        limread.Close();
        cmdduydayoku.Dispose();


        if (iptaltarihi.Trim() == null || iptaltarihi.Trim() == "" || iptaltarihi.Trim().Length == 0)
        {
            TBjurnotdaily.Text = jnotdaily;
        }
        else
        {
            TBjurnotdaily.Text = "";
        }



        baglanti.Close();

        LBLjdaily.Visible = false;      //LblDuyuru
        TBjurnotdaily.Visible = true; //TBDuyuru
        LBjkaydet.Visible = true;     //LBDkaydet
        LBjCancel.Visible = true;     //LBDCancel
        LBLleftchdaily.Visible = true;//LBLleftch
        remLen2.Visible = true;
    }
    protected void LBjkaydet_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        //==================jnotdaily yi jurnota kaydetmek

        SqlCommand cmdisimbul = new SqlCommand("Select kapadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
        string isimbul = cmdisimbul.ExecuteScalar().ToString();
        cmdisimbul.Dispose();
        SqlCommand cmdsoyisimbul = new SqlCommand("Select kapsoyadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
        string soyisimbul = cmdsoyisimbul.ExecuteScalar().ToString();
        if (soyisimbul.ToLower() == "yüksel") { soyisimbul = "Küksel"; }
        cmdsoyisimbul.Dispose();
        string operkisa = isimbul.Substring(0, 2) + "." + soyisimbul.Substring(0, 2);

        string jnotdaily = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TBjurnotdaily.Text.ToString().Trim().ToLower());

        SqlCommand cmdduydayoku = new SqlCommand("SP_duydailyoku", baglanti); //enson duyuru oku
        cmdduydayoku.CommandType = CommandType.StoredProcedure;
        cmdduydayoku.Parameters.AddWithValue("@aktif", "2");
        SqlDataReader limread = cmdduydayoku.ExecuteReader();
        string id = "";
        string aktif = "";
        string duyuru = "";
        string kaydeden = "";
        string kayittarihi = "";
        string iptaltarihi = "";

        if (limread.HasRows)
        {
            while (limread.Read())
            {
                id = limread["id"].ToString();
                aktif = limread["aktif"].ToString();
                duyuru = limread["duyuru"].ToString();
                kaydeden = limread["kaydeden"].ToString();
                kayittarihi = limread["kayittarihi"].ToString();
                iptaltarihi = limread["iptaltarihi"].ToString();
            }
        }
        limread.Close();
        cmdduydayoku.Dispose();

        if (jnotdaily != "" && jnotdaily.Length > 5)
        {
            if (jnotdaily == duyuru)
            {
            }

            else
            {
                jnotdaily = jnotdaily + " [" + DateTime.Now.ToShortTimeString().Substring(0, 5) + "-" + operkisa + "]";

                if (iptaltarihi.Trim() == null || iptaltarihi.Trim() == "" || iptaltarihi.Trim().Length == 0)
                {
                    SqlCommand cmdTakvarsaatup = new SqlCommand("SP_duyuru_UP_ipt", baglanti);
                    cmdTakvarsaatup.CommandType = CommandType.StoredProcedure;
                    cmdTakvarsaatup.Parameters.AddWithValue("@iptaltarihi", TarihSaatYaziYapDMYhm(DateTime.Now));
                    cmdTakvarsaatup.ExecuteNonQuery();
                    cmdTakvarsaatup.Dispose();
                }

                SqlCommand cmdistgelup = new SqlCommand("SP_duyuru_Yaz", baglanti); //insert
                cmdistgelup.CommandType = CommandType.StoredProcedure;
                cmdistgelup.Parameters.AddWithValue("@aktif", "2");
                cmdistgelup.Parameters.AddWithValue("@duyuru", jnotdaily);
                cmdistgelup.Parameters.AddWithValue("@kaydeden", operkisa);
                cmdistgelup.Parameters.AddWithValue("@kayittarihi", TarihSaatYaziYapDMYhm(DateTime.Now));
                cmdistgelup.Parameters.AddWithValue("@iptaltarihi", "");
                cmdistgelup.ExecuteNonQuery();
                cmdistgelup.Dispose();
            }
        }
        else
        {
            if (iptaltarihi.Trim() == null || iptaltarihi.Trim() == "" || iptaltarihi.Trim().Length == 0)
            {
                SqlCommand cmdTakvarsaatup = new SqlCommand("SP_duyuru_UP_ipt", baglanti);
                cmdTakvarsaatup.CommandType = CommandType.StoredProcedure;
                cmdTakvarsaatup.Parameters.AddWithValue("@iptaltarihi", TarihSaatYaziYapDMYhm(DateTime.Now));
                cmdTakvarsaatup.ExecuteNonQuery();
                cmdTakvarsaatup.Dispose();
            }
        }

        //====================jnot duyuru ya bitti

        LBLjdaily.Visible = true;      //LblDuyuru
        TBjurnotdaily.Visible = false; //TBDuyuru
        LBjkaydet.Visible = false;     //LBDkaydet
        LBjCancel.Visible = false;     //LBDCancel
        LBLleftchdaily.Visible = false;//LBLleftch
        remLen2.Visible = false;

        DTloading(baglanti);
        baglanti.Close();

    }
    protected void LBjCancel_Click(object sender, EventArgs e)
    {
        LBLjdaily.Visible = true;
        TBjurnotdaily.Visible = false;
        LBjkaydet.Visible = false;
        LBjCancel.Visible = false;
        LBLleftchdaily.Visible = false;
        remLen2.Visible = false;
    }


    protected void LBonlineoff_Click(object sender, EventArgs e)
    {
        if (Session["kapno"].ToString() == "" || (Session["kapno"] == null))
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else
        {

            AnaKlas.logrecup(Session["kapno"].ToString());
            Session.Abandon();
            Response.Redirect("https://www.monitoringpilot.com");
        }
    }
    protected void LBonline_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["yetki"]) == 1 || Convert.ToInt32(Session["yetki"]) == 2)
        {
            //               Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Convert.ToInt32(Session["yetki"]) == 0)
        {
            //              Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonet/yonetim.aspx");
        }
    }
    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        Lblsaat.Text = DateTime.Now.ToShortTimeString();

        SqlConnection baglanti = AnaKlas.baglan();

        //******************* Herkesin degerleri update ediliyor********************
        string varbilvarnoe = varbilvarno.Text;

        TimeSpan ts2 = DateTime.Now - Convert.ToDateTime(varbaslar.Text);
        decimal gecenzaman = Convert.ToDecimal(ts2.TotalHours);



        // herkesin yorulması geçen zamana göre update ediliyor
        SqlCommand cmdfatoku = new SqlCommand("SP_Pilotvardiya_TopisTopsurTopdinIdYor", baglanti);
        cmdfatoku.CommandType = CommandType.StoredProcedure;
        cmdfatoku.Parameters.AddWithValue("@varno", varbilvarnoe);
        SqlDataReader fatrdr = cmdfatoku.ExecuteReader();

        if (fatrdr.HasRows)
        {
            while (fatrdr.Read())
            {
                int kapnoi = Convert.ToInt32(fatrdr["kapno"].ToString());
                string id = fatrdr["id"].ToString();
                int idi = Convert.ToInt32(id);
                string olantoplamissuresi = fatrdr["toplamissuresi"].ToString();
                string olantoplamdinlenme = fatrdr["toplamdinlenme"].ToString();

                decimal olantoplamissuresid = Convert.ToDecimal(olantoplamissuresi);
                decimal olantoplamdinlenmed = Convert.ToDecimal(olantoplamdinlenme);

                decimal yorulmadall = ((olantoplamissuresid + olantoplamdinlenmed) / gecenzaman);
                yorulmadall = (yorulmadall * 3) / 5;

                SqlConnection baglanti2 = AnaKlas.baglan2();

                //son gelis oku

                decimal last7 = 0;
                string songelis = "";
                SqlCommand cmdsongelisoku = new SqlCommand("SP_PilotGemisiIstCikisGelis2li", baglanti2);
                cmdsongelisoku.CommandType = CommandType.StoredProcedure;
                cmdsongelisoku.Parameters.AddWithValue("@secilikapno", kapnoi);
                SqlDataReader varissonreader = cmdsongelisoku.ExecuteReader();
                if (varissonreader.HasRows)
                {
                    while (varissonreader.Read())
                    {
                        songelis = varissonreader["istasyongelis"].ToString();
                    }
                }
                varissonreader.Close();
                cmdsongelisoku.Dispose();

                // takviye r8 sabitleme  için önce gemi adını bul
                SqlCommand cmdPilotGemisiismial = new SqlCommand("SP_PilotGemisiismial", baglanti2);
                cmdPilotGemisiismial.CommandType = CommandType.StoredProcedure;
                cmdPilotGemisiismial.Parameters.AddWithValue("@secilikapno", kapnoi);
                cmdPilotGemisiismial.Parameters.Add("@pilotgemisiismi", SqlDbType.Char, 30);
                cmdPilotGemisiismial.Parameters["@pilotgemisiismi"].Direction = ParameterDirection.Output;
                cmdPilotGemisiismial.ExecuteNonQuery();
                string shipadi = cmdPilotGemisiismial.Parameters["@pilotgemisiismi"].Value.ToString().Trim();
                cmdPilotGemisiismial.Dispose();

                if (shipadi.ToString().ToLower() == "takviye") // takviyelerin son gemi songelişini bul
                {
                    string sonistasyongelistak = varbaslar.Text;
                    SqlCommand cmdlasttak = new SqlCommand("SP_DTDaricaYarimcaCanliGecmisSonGelis", baglanti2);
                    cmdlasttak.CommandType = CommandType.StoredProcedure;
                    cmdlasttak.Parameters.AddWithValue("@secilikapno", kapnoi);
                    cmdlasttak.Parameters.AddWithValue("@varbilvarno", varbilvarnoe);
                    SqlDataReader varisreaderuphertak = cmdlasttak.ExecuteReader();
                    if (varisreaderuphertak.HasRows)
                    {
                        while (varisreaderuphertak.Read())
                        {
                            sonistasyongelistak = varisreaderuphertak["istasyongelis"].ToString();
                        }
                    }
                    varisreaderuphertak.Close();
                    cmdlasttak.Dispose();

                    songelis = sonistasyongelistak;
                }


                TimeSpan tslast7 = DateTime.Now - Convert.ToDateTime(songelis);
                last7 = Convert.ToDecimal(tslast7.TotalHours);

                int sevensay = 0;
                int daysay = 0;

                if (last7 > 8) { sevensay = Convert.ToInt32(last7 * 100); }
                else if (last7 < 0) { last7 = 0; }
                // son okuma kayıt bitti

                // son 24 fatik hesap ve kayıt baslar
                string istasyoncikisher = "";
                string istasyongelisher = "";
                decimal issuresitoplamher = 0;

                SqlCommand cmdlast24 = new SqlCommand("SP_DTDaricaYarimcaCanliGecmis", baglanti2);
                cmdlast24.CommandType = CommandType.StoredProcedure;
                cmdlast24.Parameters.AddWithValue("@secilikapno", kapnoi);
                cmdlast24.Parameters.AddWithValue("@varbilvarno", varbilvarnoe);

                SqlDataReader varisreaderupher = cmdlast24.ExecuteReader();
                if (varisreaderupher.HasRows)
                {
                    while (varisreaderupher.Read())
                    {
                        decimal issuresiher = 0;
                        istasyoncikisher = varisreaderupher["istasyoncikis"].ToString();
                        istasyongelisher = varisreaderupher["istasyongelis"].ToString();

                        DateTime istasyoncikisherd = Convert.ToDateTime(istasyoncikisher);
                        DateTime istasyongelisherd = Convert.ToDateTime(istasyongelisher);

                        if (istasyongelisherd > DateTime.Now.AddHours(-24) && istasyoncikisherd > DateTime.Now.AddHours(-24))
                        {
                            TimeSpan tsher1 = istasyongelisherd - istasyoncikisherd;
                            issuresiher = Convert.ToDecimal(tsher1.TotalHours.ToString());
                            issuresitoplamher = issuresitoplamher + issuresiher;
                        }
                        else if (istasyongelisherd > DateTime.Now.AddHours(-24) && istasyoncikisherd < DateTime.Now.AddHours(-24))
                        {
                            TimeSpan tsher2 = istasyongelisherd - DateTime.Now.AddHours(-24);
                            issuresiher = Convert.ToDecimal(tsher2.TotalHours.ToString());
                            issuresitoplamher = issuresitoplamher + issuresiher;
                        }
                    }
                }
                varisreaderupher.Close();


                if (issuresitoplamher > 14) { daysay = 1; }

                //----------l24 bitti

                //sırayla güncelleme
                SqlCommand cmdvarfatup = new SqlCommand("SP_Pilotvardiya_UpToplamlarHerkes", baglanti2);
                cmdvarfatup.CommandType = CommandType.StoredProcedure;
                cmdvarfatup.Parameters.AddWithValue("@idi", idi);
                cmdvarfatup.Parameters.AddWithValue("@varno", varbilvarnoe);
                cmdvarfatup.Parameters.AddWithValue("yorulma", yorulmadall);
                cmdvarfatup.Parameters.AddWithValue("lastseven", last7);
                cmdvarfatup.Parameters.AddWithValue("lastday", 24 - issuresitoplamher); // son Rest/24
                cmdvarfatup.Parameters.AddWithValue("sevensayi", sevensay);
                cmdvarfatup.Parameters.AddWithValue("daysayi", daysay);
                cmdvarfatup.ExecuteNonQuery();
                cmdvarfatup.Dispose();


                baglanti2.Close();
            }
        }
        fatrdr.Close();

        //******************* Herkesin degerleri update edildi ********************


        DTloading(baglanti);
        baglanti.Close();
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
        else if (Convert.ToDateTime(eta) > DateTime.Now.AddMinutes(31))
        {
            return false;
        }


        else
        {
            return true;
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

    private bool IsDate2(string tarihyazi)
    {
        DateTime Temp;
        IFormatProvider culture = new System.Globalization.CultureInfo("tr-TR", true);
        if (DateTime.TryParse(tarihyazi, culture, System.Globalization.DateTimeStyles.AssumeLocal, out Temp) == true)
            return true;
        else
            return false;
    }


    protected void Bshowbyd_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        //yol oku
        SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
        lybbak.CommandType = CommandType.StoredProcedure;
        lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(Bshowbyd.CommandName));
        lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
        lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
        lybbak.ExecuteNonQuery();
        string lybyol = lybbak.Parameters["@lybyol"].Value.ToString().Trim();
        lybbak.Dispose();
        baglanti.Close();

        Response.Redirect(lybyol, false);
    }

    protected void Bacceptedjur_Click(object sender, EventArgs e)
    {

        TBJEagency.BorderColor = System.Drawing.Color.Gray;
        TBJEagency.BorderWidth = 1;

        TBJEetadt.BorderColor = System.Drawing.Color.Gray;
        TBJEetadt.BorderWidth = 1;

        TBJEgrt.BorderColor = System.Drawing.Color.Gray;
        TBJEgrt.BorderWidth = 1;

        TBJEarhr1.BorderColor = System.Drawing.Color.Gray;
        TBJEarhr1.BorderWidth = 1;

        TBJEarhr2.BorderColor = System.Drawing.Color.Gray;
        TBJEarhr2.BorderWidth = 1;

        string kapno = LJEid.Text;
        string gemiadi = LBJEsn.Text;
        string rom1 = HttpUtility.HtmlDecode(DDLJEap.SelectedItem.Text);
        string rom2 = HttpUtility.HtmlDecode(DDLJEdp.SelectedItem.Text);
        string rom3 = HttpUtility.HtmlDecode(DDLJEdb.SelectedItem.Text);
        string rom4 = HttpUtility.HtmlDecode(DDLJEflag.SelectedItem.Text);
        string rom5 = HttpUtility.HtmlDecode(DDLJEextra.SelectedItem.Text);
        string mboat = HttpUtility.HtmlDecode(DDLJEtip.SelectedItem.Text);
        string wh1 = TBJEetadt.Text; // wh1
        string wh2 = TBJEgrt.Text;  // wh2
        string wh3 = TBJEarhr1.Text; // wh1
        string wh4 = TBJEarhr2.Text;  // wh2

        //string oper = TBJEloa.Text;
        string acente = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TBJEagency.Text.ToString().Trim().ToLower());
        string jnot = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TBjurnot.Text.ToString().Trim().ToLower());
        string jnotdaily = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TBjurnotdaily.Text.ToString().Trim().ToLower());
        string jnotlast = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TBjurnotfalse.Text.ToString().Trim().ToLower());

        string hata = "0";

        if (jnot == "") { jnot = "-"; }
        if (jnotdaily == "") { jnotdaily = "-"; }
        if (rom1 == "...") { rom1 = ""; }
        if (rom2 == "...") { rom2 = ""; }
        if (rom3 == "...") { rom3 = ""; }
        if (rom4 == "...") { rom4 = ""; }
        if (rom5 == "...") { rom5 = ""; }
        if (mboat == "...") { mboat = ""; }

        if (acente == "" || acente == null)
        {
            acente = "";
            TBJEagency.BorderColor = System.Drawing.Color.Red;
            TBJEagency.BorderWidth = 1;
            hata = "1";
            this.ModalPopupjurnot.Show();
        }



        if (TBJEetadt.Enabled == true && TBJEgrt.Enabled == true)
        {
            if (wh1 != "" && wh1 != null && wh1 != "__:__")
            {
                if (IsDate2(wh1) != true)
                {
                    TBJEetadt.BorderColor = System.Drawing.Color.Red;
                    TBJEetadt.BorderWidth = 1;
                    hata = "1";
                    this.ModalPopupjurnot.Show();
                }
            }
            else
            {
                wh1 = "";
            }


            if (wh2 != "" && wh2 != null && wh2 != "__:__")
            {

                if (IsDate2(wh2) != true)
                {
                    TBJEgrt.BorderColor = System.Drawing.Color.Red;
                    TBJEgrt.BorderWidth = 1;
                    hata = "1";
                    this.ModalPopupjurnot.Show();
                }
            }
            else
            {
                wh2 = "";
            }

        }
        else
        {
            wh1 = "";
            wh2 = "";
        }

        if (TBJEarhr1.Enabled == true && TBJEarhr2.Enabled == true)
        {

            if (wh3 != "" && wh3 != null && wh3 != "__:__")
            {
                if (IsDate2(wh3) != true)
                {
                    TBJEarhr1.BorderColor = System.Drawing.Color.Red;
                    TBJEarhr1.BorderWidth = 1;
                    hata = "1";
                    this.ModalPopupjurnot.Show();
                }
            }
            else
            {
                wh3 = "";
            }



            if (wh4 != "" && wh4 != null && wh4 != "__:__")
            {

                if (IsDate2(wh4) != true)
                {
                    TBJEarhr2.BorderColor = System.Drawing.Color.Red;
                    TBJEarhr2.BorderWidth = 1;
                    hata = "1";
                    this.ModalPopupjurnot.Show();
                }
            }
            else
            {
                wh4 = "";
            }

        }
        else
        {
            wh3 = "";
            wh4 = "";
        }

        if (wh1 != "" && wh2 == "")
        {
            TBJEgrt.BorderColor = System.Drawing.Color.Red;
            TBJEgrt.BorderWidth = 1;
            hata = "1";
            this.ModalPopupjurnot.Show();
        }

        if (wh3 != "" && wh4 == "")
        {
            TBJEarhr2.BorderColor = System.Drawing.Color.Red;
            TBJEarhr2.BorderWidth = 1;
            hata = "1";
            this.ModalPopupjurnot.Show();
        }



        if (hata == "0")
        {
            SqlConnection baglanti = AnaKlas.baglan();

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


            //SqlCommand cmdisimbul = new SqlCommand("Select kapadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
            //string isimbul = cmdisimbul.ExecuteScalar().ToString();
            //cmdisimbul.Dispose();
            //SqlCommand cmdsoyisimbul = new SqlCommand("Select kapsoyadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
            //string soyisimbul = cmdsoyisimbul.ExecuteScalar().ToString();
            //if (soyisimbul.ToLower() == "yüksel") { soyisimbul = "Küksel"; }
            //cmdsoyisimbul.Dispose();

            string operkisa = seskapadis.Substring(0, 2) + "." + seskapsoyadis.Substring(0, 2);

            //if (jnot.Length > 6 && jnot.EndsWith(")") && jnot.Substring(jnot.Length - 4, 1) == ":" && jnot.Substring(jnot.Length - 7, 1) == "(")
            //{ jnot = jnot.Substring(0, jnot.Length - 7); }

            if (jnot != "-")
            {
                jnot = jnotlast + jnot + " [" + DateTime.Now.ToShortTimeString().Substring(0, 5) + "-" + operkisa + "]|";
            }
            else { jnot = jnotlast; }

            TBjurnot.Text = "";

            SqlCommand cmd = new SqlCommand("SP_Up_Pilotlar_jurnal", baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("kapno", kapno);
            cmd.Parameters.AddWithValue("rom1", rom1);
            cmd.Parameters.AddWithValue("rom2", rom2);
            cmd.Parameters.AddWithValue("rom3", rom3);
            cmd.Parameters.AddWithValue("rom4", rom4);
            cmd.Parameters.AddWithValue("rom5", rom5);
            cmd.Parameters.AddWithValue("mboat", mboat);
            cmd.Parameters.AddWithValue("calsaat1", wh1);
            cmd.Parameters.AddWithValue("calsaat2", wh2);
            cmd.Parameters.AddWithValue("calsaat3", wh3);
            cmd.Parameters.AddWithValue("calsaat4", wh4);
            cmd.Parameters.AddWithValue("acente", acente);
            cmd.Parameters.AddWithValue("oper", operkisa);
            cmd.Parameters.AddWithValue("jnot", jnot);
            cmd.Parameters.AddWithValue("jnotdaily", "");
            cmd.ExecuteNonQuery();
            cmd.Dispose();


            DTloading(baglanti);
            //jurnotfill(kapno, baglanti);
            //this.ModalPopupjurnot.Show();
            baglanti.Close();



        }

    }

    protected void DDLJEap_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLJEap.SelectedItem.Text == "...")
        {
            DDLJEdp.ClearSelection();
            DDLJEdp.Items.FindByText("...").Selected = true;
            DDLJEdp.Enabled = false;

            //DDLJEdb.ClearSelection();
            //DDLJEdb.Items.FindByText("...").Selected = true;
            //DDLJEdb.Enabled = false;

            //DDLJEflag.ClearSelection();
            //DDLJEflag.Items.FindByText("...").Selected = true;
            //DDLJEflag.Enabled = false;

        }
        else
        {
            DDLJEdp.Enabled = true;

            //if (DDLJEdb.SelectedItem.Text != "...")
            //{
            //    DDLJEdb.Enabled = true;
            //    DDLJEdb.SelectedIndex = -1;
            //}

            //if (DDLJEflag.SelectedItem.Text != "...")
            //{
            //    DDLJEflag.Enabled = true;
            //    DDLJEflag.SelectedIndex = -1;
            //}
        }

        /// Romorkör satır renklendirme
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdtdoku = new SqlCommand("SP_tugs_oku", baglanti);
        cmdtdoku.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdt = cmdtdoku.ExecuteReader();
        string bolge = "";
        int i = 1;

        if (drdt.HasRows)
        {
            while (drdt.Read())
            {
                bolge = drdt["bolge"].ToString();
                if (bolge == "2")
                {
                    DDLJEap.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdp.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdb.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEflag.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEextra.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                }
                i++;
            }
        }
        drdt.Close();
        cmdtdoku.Dispose();


        /// Palamar satır renklendirme
        SqlCommand cmdtdokumoor = new SqlCommand("SP_tugs_okumoor", baglanti);
        cmdtdokumoor.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdtm = cmdtdokumoor.ExecuteReader();
        string bolgem = "";
        int im = 1;

        if (drdtm.HasRows)
        {
            while (drdtm.Read())
            {
                bolgem = drdtm["bolge"].ToString();
                if (bolgem == "2")
                {
                    DDLJEtip.Items[im].Attributes.Add("style", "background-color: Orange !important;");
                }
                im++;
            }
        }
        drdtm.Close();
        cmdtdokumoor.Dispose();


        baglanti.Close();

        this.ModalPopupjurnot.Show();
    }

    //protected void DDLJEdp_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (DDLJEdp.SelectedItem.Text == "...")
    //    {
    //        DDLJEdb.ClearSelection();
    //        DDLJEdb.Items.FindByText("...").Selected = true;
    //        DDLJEdb.Enabled = false;

    //        DDLJEflag.ClearSelection();
    //        DDLJEflag.Items.FindByText("...").Selected = true;
    //        DDLJEflag.Enabled = false;

    //    }
    //    else
    //    {DDLJEdb.Enabled = true;
    //        //if (DDLJEdb.SelectedItem.Text != "...")
    //        //{
    //        //    
    //        //    DDLJEdb.SelectedIndex = -1;
    //        //}

    //        if (DDLJEflag.SelectedItem.Text != "...")
    //        {
    //            DDLJEflag.Enabled = true;
    //            DDLJEflag.SelectedIndex = -1;
    //        }
    //    }


    //    /// Romorkör satır renklendirme
    //    SqlConnection baglanti = AnaKlas.baglan();
    //    SqlCommand cmdtdoku = new SqlCommand("SP_tugs_oku", baglanti);
    //    cmdtdoku.CommandType = CommandType.StoredProcedure;
    //    SqlDataReader drdt = cmdtdoku.ExecuteReader();
    //    string bolge = "";
    //    int i = 1;

    //    if (drdt.HasRows)
    //    {
    //        while (drdt.Read())
    //        {
    //            bolge = drdt["bolge"].ToString();
    //            if (bolge == "2")
    //            {
    //                DDLJEap.Items[i].Attributes.Add("style", "background-color: Orange !important;");
    //                DDLJEdp.Items[i].Attributes.Add("style", "background-color: Orange !important;");
    //                DDLJEdb.Items[i].Attributes.Add("style", "background-color: Orange !important;");
    //                DDLJEflag.Items[i].Attributes.Add("style", "background-color: Orange !important;");
    //                DDLJEextra.Items[i].Attributes.Add("style", "background-color: Orange !important;");
    //            }
    //            i++;
    //        }
    //    }
    //    drdt.Close();
    //    cmdtdoku.Dispose();


    //    /// Palamar satır renklendirme
    //    SqlCommand cmdtdokumoor = new SqlCommand("SP_tugs_okumoor", baglanti);
    //    cmdtdokumoor.CommandType = CommandType.StoredProcedure;
    //    SqlDataReader drdtm = cmdtdokumoor.ExecuteReader();
    //    string bolgem = "";
    //    int im = 1;

    //    if (drdtm.HasRows)
    //    {
    //        while (drdtm.Read())
    //        {
    //            bolgem = drdtm["bolge"].ToString();
    //            if (bolgem == "2")
    //            {
    //                DDLJEtip.Items[im].Attributes.Add("style", "background-color: Orange !important;");
    //            }
    //            im++;
    //        }
    //    }
    //    drdtm.Close();
    //    cmdtdokumoor.Dispose();


    //    baglanti.Close();



    //    this.ModalPopupjurnot.Show();
    //}

    protected void DDLJEdb_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLJEdb.SelectedItem.Text == "...")
        {
            DDLJEflag.ClearSelection();
            DDLJEflag.Items.FindByText("...").Selected = true;
            DDLJEflag.Enabled = false;
        }
        else
        {
            DDLJEflag.Enabled = true;

        }



        /// Romorkör satır renklendirme
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdtdoku = new SqlCommand("SP_tugs_oku", baglanti);
        cmdtdoku.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdt = cmdtdoku.ExecuteReader();
        string bolge = "";
        int i = 1;

        if (drdt.HasRows)
        {
            while (drdt.Read())
            {
                bolge = drdt["bolge"].ToString();
                if (bolge == "2")
                {
                    DDLJEap.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdp.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEdb.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEflag.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                    DDLJEextra.Items[i].Attributes.Add("style", "background-color: Orange !important;");
                }
                i++;
            }
        }
        drdt.Close();
        cmdtdoku.Dispose();


        /// Palamar satır renklendirme
        SqlCommand cmdtdokumoor = new SqlCommand("SP_tugs_okumoor", baglanti);
        cmdtdokumoor.CommandType = CommandType.StoredProcedure;
        SqlDataReader drdtm = cmdtdokumoor.ExecuteReader();
        string bolgem = "";
        int im = 1;

        if (drdtm.HasRows)
        {
            while (drdtm.Read())
            {
                bolgem = drdtm["bolge"].ToString();
                if (bolgem == "2")
                {
                    DDLJEtip.Items[im].Attributes.Add("style", "background-color: Orange !important;");
                }
                im++;
            }
        }
        drdtm.Close();
        cmdtdokumoor.Dispose();


        baglanti.Close();


        this.ModalPopupjurnot.Show();
    }






}


