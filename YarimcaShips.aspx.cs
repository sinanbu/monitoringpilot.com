using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Globalization;
using System.Net.Mail;

public partial class YarimcaShips : System.Web.UI.Page
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
        if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2" && Session["kapno"].ToString() != "28")
        {
            Response.Redirect("https://www.monitoringpilot.com");//
        }
        else //if (cmdlogofbak.ExecuteScalar() != null)
        {

            if (!IsPostBack)
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

                LBonline.Text = kapadisoyadi;

                LiteralYaz(baglanti);

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
                LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();
                DTLoading(baglanti);
            }
             
        }
        baglanti.Close();

    }

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();
    }

    private void LiteralYaz(SqlConnection baglanti)
    {
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


    private void DTLoading(SqlConnection baglanti)
    {
       
        NewShipListvipC(baglanti); //contacted 
        VDLher(baglanti);           //hereke ports
        VDLyaranc(baglanti);        //hereke izmit yarımca demir
        VDLyarport(baglanti);       //yarımca ports
        VIPortKosb(baglanti);       //kosbaş ports
    }


    private void NewShipListvipC(SqlConnection baglanti)
    {

        using (PilotdbEntities2 isliste = new PilotdbEntities2())
        {
            DLNewShipListvipC.DataSource = NewShipListvipCBind(baglanti);
            DLNewShipListvipC.DataBind();
        }
    }
    public List<isliste> NewShipListvipCBind(SqlConnection baglanti)
    {

        SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_vipCyarimca", baglanti);
        cmdisokunsl.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmdisokunsl.ExecuteReader();

        List<isliste> NSLvipC = new List<isliste>();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                NSLvipC.Add(new isliste()
                {
                    id = Convert.ToInt32(dr["id"].ToString()),
                    imono = Convert.ToInt32(dr["imono"].ToString()),
                    gemiadi = dr["gemiadi"].ToString(),
                    kalkislimani = dr["kalkislimani"].ToString(),
                    kalkisrihtimi = dr["kalkisrihtimi"].ToString(),
                    yanasmalimani = dr["yanasmalimani"].ToString(),
                    yanasmarihtimi = dr["yanasmarihtimi"].ToString(),
                    demiryeri = dr["demiryeri"].ToString(),
                    bayrak = dr["bayrak"].ToString(),
                    tip = dr["tip"].ToString(),
                    grt = dr["grt"].ToString(),
                    acente = dr["acente"].ToString(),
                    fatura = dr["fatura"].ToString(),
                    bowt = dr["bowt"].ToString(),
                    strnt = dr["strnt"].ToString(),
                    loa = dr["loa"].ToString(),
                    tehlikeliyuk = dr["tehlikeliyuk"].ToString(),
                    draft = dr["draft"].ToString(),
                    bilgi = dr["bilgi"].ToString(),
                    eta = dr["eta"].ToString(),
                    notlar = dr["notlar"].ToString(),
                    nedurumda = dr["nedurumda"].ToString(),
                    nedurumdaopr = dr["nedurumdaopr"].ToString(), 
                    talepno = dr["talepno"].ToString(),
                    pratikano = dr["pratikano"].ToString(),
                    kaydeden = dr["kaydeden"].ToString(),
                    bowok = dr["bowok"].ToString(),
                    lcbno = dr["lcbno"].ToString(),
                    lcbdest = dr["lcbdest"].ToString(),
                    lcbdate = dr["lcbdate"].ToString(),
                    lybyol = dr["lybyol"].ToString()


                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();

        return NSLvipC;

    }

    private void VDLher(SqlConnection baglanti)
    {
        using (PilotdbEntities2 isliste = new PilotdbEntities2())
        {
            DLher.DataSource = VDLherBind(baglanti);
            DLher.DataBind();
        }
    }
    public List<isliste> VDLherBind(SqlConnection baglanti)
    {

        SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_her", baglanti);
        cmdisokunsl.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmdisokunsl.ExecuteReader();

        List<isliste> NSLher = new List<isliste>();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                NSLher.Add(new isliste()
                {
                    id = Convert.ToInt32(dr["id"].ToString()),
                    imono = Convert.ToInt32(dr["imono"].ToString()),
                    gemiadi = dr["gemiadi"].ToString(),
                    kalkislimani = dr["kalkislimani"].ToString(),
                    kalkisrihtimi = dr["kalkisrihtimi"].ToString(),
                    yanasmalimani = dr["yanasmalimani"].ToString(),
                    yanasmarihtimi = dr["yanasmarihtimi"].ToString(),
                    demiryeri = dr["demiryeri"].ToString(),
                    bayrak = dr["bayrak"].ToString(),
                    tip = dr["tip"].ToString(),
                    grt = dr["grt"].ToString(),
                    acente = dr["acente"].ToString(),
                    fatura = dr["fatura"].ToString(),
                    bowt = dr["bowt"].ToString(),
                    strnt = dr["strnt"].ToString(),
                    loa = dr["loa"].ToString(),
                    tehlikeliyuk = dr["tehlikeliyuk"].ToString(),
                    draft = dr["draft"].ToString(),
                    bilgi = dr["bilgi"].ToString(),
                    eta = dr["eta"].ToString(),
                    notlar = dr["notlar"].ToString(),
                    talepno = dr["talepno"].ToString(),
                    pratikano = dr["pratikano"].ToString(),
                    kaydeden = dr["kaydeden"].ToString(),
                    bowok = dr["bowok"].ToString(),
                    lcbno = dr["lcbno"].ToString(),
                    lcbdest = dr["lcbdest"].ToString(),
                    lcbdate = dr["lcbdate"].ToString(),
                    lybyol = dr["lybyol"].ToString()



                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();


        return NSLher;

    }

    private void VDLyaranc(SqlConnection baglanti)
    {
        using (PilotdbEntities2 isliste = new PilotdbEntities2())
        {
            DLyaranc.DataSource = VDLyarancBind(baglanti);
            DLyaranc.DataBind();
        }
    }
    public List<isliste> VDLyarancBind(SqlConnection baglanti)
    {

        SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_YA", baglanti);
        cmdisokunsl.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmdisokunsl.ExecuteReader();

        List<isliste> NSLYA = new List<isliste>();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                NSLYA.Add(new isliste()
                {
                    id = Convert.ToInt32(dr["id"].ToString()),
                    imono = Convert.ToInt32(dr["imono"].ToString()),
                    gemiadi = dr["gemiadi"].ToString(),
                    kalkislimani = dr["kalkislimani"].ToString(),
                    kalkisrihtimi = dr["kalkisrihtimi"].ToString(),
                    yanasmalimani = dr["yanasmalimani"].ToString(),
                    yanasmarihtimi = dr["yanasmarihtimi"].ToString(),
                    demiryeri = dr["demiryeri"].ToString(),
                    bayrak = dr["bayrak"].ToString(),
                    tip = dr["tip"].ToString(),
                    grt = dr["grt"].ToString(),
                    acente = dr["acente"].ToString(),
                    fatura = dr["fatura"].ToString(),
                    bowt = dr["bowt"].ToString(),
                    strnt = dr["strnt"].ToString(),
                    loa = dr["loa"].ToString(),
                    tehlikeliyuk = dr["tehlikeliyuk"].ToString(),
                    draft = dr["draft"].ToString(),
                    bilgi = dr["bilgi"].ToString(),
                    eta = dr["eta"].ToString(),
                    notlar = dr["notlar"].ToString(),
                    talepno = dr["talepno"].ToString(),
                    pratikano = dr["pratikano"].ToString(),
                    kaydeden = dr["kaydeden"].ToString(),
                    bowok = dr["bowok"].ToString(),
                    lcbno = dr["lcbno"].ToString(),
                    lcbdest = dr["lcbdest"].ToString(),
                    lcbdate = dr["lcbdate"].ToString(),
                    lybyol = dr["lybyol"].ToString()



                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();


        return NSLYA;

    }

    private void VDLyarport(SqlConnection baglanti)
    {
        using (PilotdbEntities2 isliste = new PilotdbEntities2())
        {
            DLyarport.DataSource = VDLyarportBind(baglanti);
            DLyarport.DataBind();
        }
    }
    public List<isliste> VDLyarportBind(SqlConnection baglanti)
    {


        SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_YP", baglanti);
        cmdisokunsl.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmdisokunsl.ExecuteReader();

        List<isliste> NSLYP = new List<isliste>();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                NSLYP.Add(new isliste()
                {
                    id = Convert.ToInt32(dr["id"].ToString()),
                    imono = Convert.ToInt32(dr["imono"].ToString()),
                    gemiadi = dr["gemiadi"].ToString(),
                    kalkislimani = dr["kalkislimani"].ToString(),
                    kalkisrihtimi = dr["kalkisrihtimi"].ToString(),
                    yanasmalimani = dr["yanasmalimani"].ToString(),
                    yanasmarihtimi = dr["yanasmarihtimi"].ToString(),
                    demiryeri = dr["demiryeri"].ToString(),
                    bayrak = dr["bayrak"].ToString(),
                    tip = dr["tip"].ToString(),
                    grt = dr["grt"].ToString(),
                    acente = dr["acente"].ToString(),
                    fatura = dr["fatura"].ToString(),
                    bowt = dr["bowt"].ToString(),
                    strnt = dr["strnt"].ToString(),
                    loa = dr["loa"].ToString(),
                    tehlikeliyuk = dr["tehlikeliyuk"].ToString(),
                    draft = dr["draft"].ToString(),
                    bilgi = dr["bilgi"].ToString(),
                    eta = dr["eta"].ToString(),
                    notlar = dr["notlar"].ToString(),
                    talepno = dr["talepno"].ToString(),
                    pratikano = dr["pratikano"].ToString(),
                    kaydeden = dr["kaydeden"].ToString(),
                    bowok = dr["bowok"].ToString(),
                    lcbno = dr["lcbno"].ToString(),
                    lcbdest = dr["lcbdest"].ToString(),
                    lcbdate = dr["lcbdate"].ToString(),
                    lybyol = dr["lybyol"].ToString()



                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();


        return NSLYP;

    }

    private void VIPortKosb(SqlConnection baglanti)
    {
        using (PilotdbEntities2 isliste = new PilotdbEntities2())
        {
            DLVIPortKosb.DataSource = VIPortKosbBind(baglanti);
            DLVIPortKosb.DataBind();
        }
    }
    public List<isliste> VIPortKosbBind(SqlConnection baglanti)
    {


        SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_Yk", baglanti);
        cmdisokunsl.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmdisokunsl.ExecuteReader();

        List<isliste> NSLYk = new List<isliste>();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                NSLYk.Add(new isliste()
                {
                    id = Convert.ToInt32(dr["id"].ToString()),
                    imono = Convert.ToInt32(dr["imono"].ToString()),
                    gemiadi = dr["gemiadi"].ToString(),
                    kalkislimani = dr["kalkislimani"].ToString(),
                    kalkisrihtimi = dr["kalkisrihtimi"].ToString(),
                    yanasmalimani = dr["yanasmalimani"].ToString(),
                    yanasmarihtimi = dr["yanasmarihtimi"].ToString(),
                    demiryeri = dr["demiryeri"].ToString(),
                    bayrak = dr["bayrak"].ToString(),
                    tip = dr["tip"].ToString(),
                    grt = dr["grt"].ToString(),
                    acente = dr["acente"].ToString(),
                    fatura = dr["fatura"].ToString(),
                    bowt = dr["bowt"].ToString(),
                    strnt = dr["strnt"].ToString(),
                    loa = dr["loa"].ToString(),
                    tehlikeliyuk = dr["tehlikeliyuk"].ToString(),
                    draft = dr["draft"].ToString(),
                    bilgi = dr["bilgi"].ToString(),
                    eta = dr["eta"].ToString(),
                    notlar = dr["notlar"].ToString(),
                    talepno = dr["talepno"].ToString(),
                    pratikano = dr["pratikano"].ToString(),
                    kaydeden = dr["kaydeden"].ToString(),
                    bowok = dr["bowok"].ToString(),
                    lcbno = dr["lcbno"].ToString(),
                    lcbdest = dr["lcbdest"].ToString(),
                    lcbdate = dr["lcbdate"].ToString(),
                    lybyol = dr["lybyol"].ToString()




                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();


        return NSLYk;

    }
   


    protected void DLNewShipListvipC_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //satır secim
            ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
            SqlConnection baglanti = AnaKlas.baglan();
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {  

            // * not rengi kırmızı
            Label notlarckop = (Label)e.Item.FindControl("notlar");
            string uzatmakelimesi = notlarckop.Text.ToLower() + "uzat";
            if (uzatmakelimesi.Substring(0, 3) == "byd")
            {
                notlarckop.Style.Add("color", "#e100a0");
            }
            else if (uzatmakelimesi.Substring(0, 1) == "*")
            {
                notlarckop.Style.Add("color", "#ee1111");
            }
            else if (uzatmakelimesi.Substring(0, 1) == "+")
            {
                notlarckop.Style.Add("color", "#1111ee");
            }
            
            if (uzatmakelimesi.Substring(0, 1) == ".")
            {   HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warntd");
                td.Attributes.Add("style", "background-color:#9FF3A1"); }

            // tan rengi farklı
            Label Lbl7kop = (Label)e.Item.FindControl("Lbl7");
            if (Lbl7kop.Text.ToLower() == "tan")
            {
                Lbl7kop.Style.Add("color", "#ee1111");
            }
            else if (Lbl7kop.Text.ToLower() == "gnr")
            {
                Lbl7kop.Style.Add("color", "#11bb11");
            }
            //uyarı olan liman rengi farklı
            Label Lbl5ckop = (Label)e.Item.FindControl("Lbl5");
            //string yanasmalimani = Lbl5ckop.Text;

            //if (yanasmalimani != "Yelkenkaya")
            //{
            //    string yetkili = "";
            //    string telno = "";
            //    string cepno = "";
            //    string uyari = "";
            //    SqlCommand cmdisokuup = new SqlCommand("SP_LimanBilgilerSec", baglanti);
            //    cmdisokuup.CommandType = CommandType.StoredProcedure;
            //    cmdisokuup.Parameters.AddWithValue("@limanadi", yanasmalimani);
            //    SqlDataReader dr = cmdisokuup.ExecuteReader();
            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            yetkili = dr["yetkili"].ToString();
            //            telno = dr["telno"].ToString();
            //            cepno = dr["cepno"].ToString();
            //            uyari = dr["uyari"].ToString();
            //        }
            //    }
            //    dr.Close();
            //    cmdisokuup.Dispose();

            //    if (uyari != "")
            //    {
            //        Lbl5ckop.Style.Add("font-weight", "bold");
            //        Lbl5ckop.ToolTip = yetkili + " " + telno + " " + cepno + " " + uyari;
            //    }
            //    else
            //    { Lbl5ckop.ToolTip = yetkili + " " + telno + " " + cepno; }
            //}
            // demirler kısaltma
            Label Lbl5rihtimkop = (Label)e.Item.FindControl("Lbl5rih");
            Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
            if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
            else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
            else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
            else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
            else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
            else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
            {
                demirkisakop.Text = "OTY";
                Lbl5ckop.Text = "Ordino-Talep Yok";
                Lbl5ckop.Style.Add("font-style", "normal");
                Lbl5ckop.Style.Add("color", "#ee1111");
                Lbl5rihtimkop.Text = "";
            }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }


            Label Lbl15dkop = (Label)e.Item.FindControl("Lbl15d");
            if (string.IsNullOrEmpty(Lbl15dkop.Text) == true)
            {
                
                HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warngemitd");
                td.Attributes.Add("style", "background-color:#ffaaaa");
            }

            Label Lbl14dkop = (Label)e.Item.FindControl("Lbl14d");
            Label Lbl14ekop = (Label)e.Item.FindControl("Lbl14e");
            Label Lbl14fkop = (Label)e.Item.FindControl("Lbl14f");
            if (Lbl14dkop.Text != "0")
            {
                if (Lbl14fkop.Text.ToLower() == "no")
                {
                    Lbl14dkop.Style.Add("font-style", "normal");
                    Lbl14dkop.Style.Add("color", "#ee1111");
                }
                else if (Lbl14fkop.Text.ToLower() == "yes")
                {
                    Lbl14dkop.Style.Add("font-style", "normal");
                    Lbl14dkop.Style.Add("color", "#11bb11");
                }
            }
            if (Lbl14ekop.Text != "0")
            {
                if (Lbl14fkop.Text.ToLower() == "no")
                {
                    Lbl14ekop.Style.Add("font-style", "normal");
                    Lbl14ekop.Style.Add("color", "#ee1111");
                }
                else if (Lbl14fkop.Text.ToLower() == "yes")
                {
                    Lbl14ekop.Style.Add("font-style", "normal");
                    Lbl14ekop.Style.Add("color", "#11bb11");
                }
            }


            HtmlTableCell td1 = (HtmlTableCell)e.Item.FindControl("tdlcb");
            Label Labellcbdatekop = (Label)e.Item.FindControl("Labellcbdate");
            if (Labellcbdatekop.Text != "")
            {
                if (Convert.ToDateTime(Labellcbdatekop.Text).AddHours(6) < DateTime.Now)
                {
                    td1.Attributes.Add("style", "background-color:#00fffa");
                }
                if (Labellcbdatekop.Text != "" && Labellcbdatekop.Text != null)
                {
                    Labellcbdatekop.Style.Add("color", "#1111ee");
                   // Labellcbdatekop.Style.Add("font-weight", "normal");
                }
            }

            // lybyol varsa koyu pembe

            Label Lbllybkop = (Label)e.Item.FindControl("Lbllyb");
            if (Lbllybkop.Text != "" && Lbllybkop.Text != null)
            {
                Lbl5ckop.Style.Add("color", "#e100a0");
                Lbl5ckop.Style.Add("font-weight", "bold");
            }

            //or bak            
            Button LBpatakop = (Button)e.Item.FindControl("LBpata");
            Label Lbl11kop = (Label)e.Item.FindControl("Lbl11");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(LBpatakop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl11kop.Style.Add("color", "#e100a0");
                Lbl11kop.Style.Add("font-weight", "bold");
            }
            else
            {
                Lbl11kop.Style.Add("color", "#333333");
            }
            orbak.Dispose();

        }
            baglanti.Close();
    }
    protected void DLher_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        //satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
            // tan rengi farklı
            Label Lbl7kop = (Label)e.Item.FindControl("Lbl7");
            if (Lbl7kop.Text.ToLower() == "tan")
            {
                Lbl7kop.Style.Add("color", "#ee1111");
            }
            else if (Lbl7kop.Text.ToLower() == "gnr")
            {
                Lbl7kop.Style.Add("color", "#11bb11");
            }

            // * not rengi kırmızı
            Label notlarckop = (Label)e.Item.FindControl("notlar");
            string uzatmakelimesi = notlarckop.Text.ToLower() + "uzat";
            if (uzatmakelimesi.Substring(0, 3) == "byd")
            {
                notlarckop.Style.Add("color", "#e100a0");
            }
            else if (uzatmakelimesi.Substring(0, 1) == "*")
            {
                notlarckop.Style.Add("color", "#ee1111");
            }
            else if (uzatmakelimesi.Substring(0, 1) == "+")
            {
                notlarckop.Style.Add("color", "#1111ee");
            }
           
            if (uzatmakelimesi.Substring(0, 1) == ".")
            { HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warntd");
              td.Attributes.Add("style", "background-color:#9FF3A1"); }

            //uyarı olan liman rengi farklı
            Label Lbl5ckop = (Label)e.Item.FindControl("Lbl5");
            //string yanasmalimani = Lbl5ckop.Text;

            //if (yanasmalimani != "Yelkenkaya")
            //{
            //    string yetkili = "";
            //    string telno = "";
            //    string cepno = "";
            //    string uyari = "";
            //    SqlCommand cmdisokuup = new SqlCommand("SP_LimanBilgilerSec", baglanti);
            //    cmdisokuup.CommandType = CommandType.StoredProcedure;
            //    cmdisokuup.Parameters.AddWithValue("@limanadi", yanasmalimani);
            //    SqlDataReader dr = cmdisokuup.ExecuteReader();
            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            yetkili = dr["yetkili"].ToString();
            //            telno = dr["telno"].ToString();
            //            cepno = dr["cepno"].ToString();
            //            uyari = dr["uyari"].ToString();
            //        }
            //    }
            //    dr.Close();
            //    cmdisokuup.Dispose();
            //    if (uyari != "")
            //    {
            //        Lbl5ckop.Style.Add("font-weight", "bold");
            //        Lbl5ckop.ToolTip = yetkili + " " + telno + " " + cepno + " " + uyari;
            //    }
            //    else
            //    { Lbl5ckop.ToolTip = yetkili + " " + telno + " " + cepno; }
            //}
            // demirler kısaltma
            Label Lbl5rihtimkop = (Label)e.Item.FindControl("Lbl5rih");
            Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
            if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
            else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
            else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
            else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
            else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
            else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
            {
                demirkisakop.Text = "OTY";
                Lbl5ckop.Text = "Ordino-Talep Yok";
                Lbl5ckop.Style.Add("font-style", "normal");
                Lbl5ckop.Style.Add("color", "#ee1111");
                Lbl5rihtimkop.Text = "";
            }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }

            Label Lbl15dkop = (Label)e.Item.FindControl("Lbl15d");
            if (string.IsNullOrEmpty(Lbl15dkop.Text) == true)
            {
                
                HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warngemitd");
                td.Attributes.Add("style", "background-color:#ffaaaa");
            }

            //2 saat kala eta kırmızı
            Label etakop = (Label)e.Item.FindControl("Lbl4full");
            Label etasaatkop = (Label)e.Item.FindControl("Lbl4hour");
            if (Convert.ToDateTime(etakop.Text) > DateTime.Now.AddHours(-2))
            {
                if (Convert.ToDateTime(etakop.Text).AddHours(-2) <= DateTime.Now)
                {
                    etasaatkop.Style.Add("color", "#ee1111");
                }
            }

            Label Lbl14dkop = (Label)e.Item.FindControl("Lbl14d");
            Label Lbl14ekop = (Label)e.Item.FindControl("Lbl14e");
            Label Lbl14fkop = (Label)e.Item.FindControl("Lbl14f");
            if (Lbl14dkop.Text != "0")
            {
                if (Lbl14fkop.Text.ToLower() == "no")
                {
                    Lbl14dkop.Style.Add("font-style", "normal");
                    Lbl14dkop.Style.Add("color", "#ee1111");
                }
                else if (Lbl14fkop.Text.ToLower() == "yes")
                {
                    Lbl14dkop.Style.Add("font-style", "normal");
                    Lbl14dkop.Style.Add("color", "#11bb11");
                }
            }
            if (Lbl14ekop.Text != "0")
            {
                if (Lbl14fkop.Text.ToLower() == "no")
                {
                    Lbl14ekop.Style.Add("font-style", "normal");
                    Lbl14ekop.Style.Add("color", "#ee1111");
                }
                else if (Lbl14fkop.Text.ToLower() == "yes")
                {
                    Lbl14ekop.Style.Add("font-style", "normal");
                    Lbl14ekop.Style.Add("color", "#11bb11");
                }
            }


            HtmlTableCell td1 = (HtmlTableCell)e.Item.FindControl("tdlcb");
            Label Labellcbdatekop = (Label)e.Item.FindControl("Labellcbdate");
            if (Labellcbdatekop.Text != "")
            {
                if (Convert.ToDateTime(Labellcbdatekop.Text).AddHours(6) < DateTime.Now)
                {
                    td1.Attributes.Add("style", "background-color:#00fffa");
                }
                if (Labellcbdatekop.Text != "" && Labellcbdatekop.Text != null)
                {
                    Labellcbdatekop.Style.Add("color", "#1111ee");
                    // Labellcbdatekop.Style.Add("font-weight", "normal");
                }
            }

            // lybyol varsa koyu pembe

            Label Lbllybkop = (Label)e.Item.FindControl("Lbllyb");
            if (Lbllybkop.Text != "" && Lbllybkop.Text != null)
            {
                Lbl5ckop.Style.Add("color", "#e100a0");
                Lbl5ckop.Style.Add("font-weight", "bold");
            }

            //or bak            
            Button LBpatakop = (Button)e.Item.FindControl("LBpata");
            Label Lbl11kop = (Label)e.Item.FindControl("Lbl11");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(LBpatakop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl11kop.Style.Add("color", "#e100a0");
                Lbl11kop.Style.Add("font-weight", "bold");
            }
            else
            {
                Lbl11kop.Style.Add("color", "#333333");
            }
            orbak.Dispose();

        }
        baglanti.Close();
    }
    protected void DLyaranc_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        //satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {           
            
            // * not rengi kırmızı
            Label notlarckop = (Label)e.Item.FindControl("notlar");
            string uzatmakelimesi = notlarckop.Text.ToLower() + "uzat";
            if (uzatmakelimesi.Substring(0, 3) == "byd")
            {
                notlarckop.Style.Add("color", "#e100a0");
            }
            else if (uzatmakelimesi.Substring(0, 1) == "*")
            {
                notlarckop.Style.Add("color", "#ee1111");
            }
            else if (uzatmakelimesi.Substring(0, 1) == "+")
            {
                notlarckop.Style.Add("color", "#1111ee");
            }
            
            if (uzatmakelimesi.Substring(0, 1) == ".")
            {HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warntd");
                td.Attributes.Add("style", "background-color:#9FF3A1"); }

            // tan rengi farklı
            Label Lbl7kop = (Label)e.Item.FindControl("Lbl7");
            if (Lbl7kop.Text.ToLower() == "tan")
            {
                Lbl7kop.Style.Add("color", "#ee1111");
            }
            else if (Lbl7kop.Text.ToLower() == "gnr")
            {
                Lbl7kop.Style.Add("color", "#11bb11");
            }

            //uyarı olan liman rengi farklı
            Label Lbl5ckop = (Label)e.Item.FindControl("Lbl5");
            //string yanasmalimani = Lbl5ckop.Text;
            //if (yanasmalimani != "Yelkenkaya")
            //{
            //    string yetkili = "";
            //    string telno = "";
            //    string cepno = "";
            //    string uyari = "";
            //    SqlCommand cmdisokuup = new SqlCommand("SP_LimanBilgilerSec", baglanti);
            //    cmdisokuup.CommandType = CommandType.StoredProcedure;
            //    cmdisokuup.Parameters.AddWithValue("@limanadi", yanasmalimani);
            //    SqlDataReader dr = cmdisokuup.ExecuteReader();
            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            yetkili = dr["yetkili"].ToString();
            //            telno = dr["telno"].ToString();
            //            cepno = dr["cepno"].ToString();
            //            uyari = dr["uyari"].ToString();
            //        }
            //    }
            //    dr.Close();
            //    cmdisokuup.Dispose();

            //    if (uyari != "")
            //    {
            //        Lbl5ckop.Style.Add("font-weight", "bold");
            //        Lbl5ckop.ToolTip = yetkili + " " + telno + " " + cepno + " " + uyari;
            //    }
            //    else
            //    { Lbl5ckop.ToolTip = yetkili + " " + telno + " " + cepno; }
            //}
            // demirler kısaltma
            Label Lbl5rihtimkop = (Label)e.Item.FindControl("Lbl5rih");
            Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
            if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
            else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
            else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
            else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
            else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
            else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
            {
                demirkisakop.Text = "OTY";
                Lbl5ckop.Text = "Ordino-Talep Yok";
                Lbl5ckop.Style.Add("font-style", "normal");
                Lbl5ckop.Style.Add("color", "#ee1111");
                Lbl5rihtimkop.Text = "";
            }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }

            Label Lbl15dkop = (Label)e.Item.FindControl("Lbl15d");
            if (string.IsNullOrEmpty(Lbl15dkop.Text) == true)
            {
                
                HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warngemitd");
                td.Attributes.Add("style", "background-color:#ffaaaa");
            }

            //satır boya giriş 
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("Row");
            Label Lblkalkislimanikop = (Label)e.Item.FindControl("Lblkalkislimanigiz");

            if (Lblkalkislimanikop.Text.ToString() == "Demir-İzmit")
            { tr.Attributes.Add("style", "background-color:#Ffffe5"); }

            //2 saat kala eta kırmızı
            Label etakop = (Label)e.Item.FindControl("Lbl4full");
            Label etasaatkop = (Label)e.Item.FindControl("Lbl4hour");
            if (Convert.ToDateTime(etakop.Text) > DateTime.Now.AddHours(-2))
            {
                if (Convert.ToDateTime(etakop.Text).AddHours(-2) <= DateTime.Now)
                {
                    etasaatkop.Style.Add("color", "#ee1111");
                }
            }

            Label Lbl14dkop = (Label)e.Item.FindControl("Lbl14d");
            Label Lbl14ekop = (Label)e.Item.FindControl("Lbl14e");
            Label Lbl14fkop = (Label)e.Item.FindControl("Lbl14f");
            if (Lbl14dkop.Text != "0")
            {
                if (Lbl14fkop.Text.ToLower() == "no")
                {
                    Lbl14dkop.Style.Add("font-style", "normal");
                    Lbl14dkop.Style.Add("color", "#ee1111");
                }
                else if (Lbl14fkop.Text.ToLower() == "yes")
                {
                    Lbl14dkop.Style.Add("font-style", "normal");
                    Lbl14dkop.Style.Add("color", "#11bb11");
                }
            }
            if (Lbl14ekop.Text != "0")
            {
                if (Lbl14fkop.Text.ToLower() == "no")
                {
                    Lbl14ekop.Style.Add("font-style", "normal");
                    Lbl14ekop.Style.Add("color", "#ee1111");
                }
                else if (Lbl14fkop.Text.ToLower() == "yes")
                {
                    Lbl14ekop.Style.Add("font-style", "normal");
                    Lbl14ekop.Style.Add("color", "#11bb11");
                }
            }


            HtmlTableCell td1 = (HtmlTableCell)e.Item.FindControl("tdlcb");
            Label Labellcbdatekop = (Label)e.Item.FindControl("Labellcbdate");
            if (Labellcbdatekop.Text != "")
            {
                if (Convert.ToDateTime(Labellcbdatekop.Text).AddHours(6) < DateTime.Now)
                {
                    td1.Attributes.Add("style", "background-color:#00fffa");
                }
                if (Labellcbdatekop.Text != "" && Labellcbdatekop.Text != null)
                {
                    Labellcbdatekop.Style.Add("color", "#1111ee");
                    // Labellcbdatekop.Style.Add("font-weight", "normal");
                }
            }

            // lybyol varsa koyu pembe

            Label Lbllybkop = (Label)e.Item.FindControl("Lbllyb");
            if (Lbllybkop.Text != "" && Lbllybkop.Text != null)
            {
                Lbl5ckop.Style.Add("color", "#e100a0");
                Lbl5ckop.Style.Add("font-weight", "bold");
            }

            //or bak            
            Button LBpatakop = (Button)e.Item.FindControl("LBpata");
            Label Lbl11kop = (Label)e.Item.FindControl("Lbl11");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(LBpatakop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl11kop.Style.Add("color", "#e100a0");
                Lbl11kop.Style.Add("font-weight", "bold");
            }
            else
            {
                Lbl11kop.Style.Add("color", "#333333");
            }
            orbak.Dispose();

        }
        baglanti.Close();
    }
    protected void DLyarport_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        //satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {          
            // tan rengi farklı
            Label Lbl7kop = (Label)e.Item.FindControl("Lbl7");
            if (Lbl7kop.Text.ToLower() == "tan")
            {
                Lbl7kop.Style.Add("color", "#ee1111");
            }
            else if (Lbl7kop.Text.ToLower() == "gnr")
            {
                Lbl7kop.Style.Add("color", "#11bb11");
            }

            // * not rengi kırmızı
            Label notlarckop = (Label)e.Item.FindControl("notlar");
            string uzatmakelimesi = notlarckop.Text.ToLower() + "uzat";
            if (uzatmakelimesi.Substring(0, 3) == "byd")
            {
                notlarckop.Style.Add("color", "#e100a0");
            }
            else if (uzatmakelimesi.Substring(0, 1) == "*")
            {
                notlarckop.Style.Add("color", "#ee1111");
            }
            else if (uzatmakelimesi.Substring(0, 1) == "+")
            {
                notlarckop.Style.Add("color", "#1111ee");
            }
            
            if (uzatmakelimesi.Substring(0, 1) == ".")
            {HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warntd");
                td.Attributes.Add("style", "background-color:#9FF3A1"); }

            //uyarı olan liman rengi farklı
            Label Lbl5ckop = (Label)e.Item.FindControl("Lbl5");
            //string yanasmalimani = Lbl5ckop.Text;

            //if (yanasmalimani != "Yelkenkaya")
            //{
            //    string yetkili = "";
            //    string telno = "";
            //    string cepno = "";
            //    string uyari = "";
            //    SqlCommand cmdisokuup = new SqlCommand("SP_LimanBilgilerSec", baglanti);
            //    cmdisokuup.CommandType = CommandType.StoredProcedure;
            //    cmdisokuup.Parameters.AddWithValue("@limanadi", yanasmalimani);
            //    SqlDataReader dr = cmdisokuup.ExecuteReader();
            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            yetkili = dr["yetkili"].ToString();
            //            telno = dr["telno"].ToString();
            //            cepno = dr["cepno"].ToString();
            //            uyari = dr["uyari"].ToString();
            //        }
            //    }
            //    dr.Close();
            //    cmdisokuup.Dispose();

            //    if (uyari != "")
            //    {
            //        Lbl5ckop.Style.Add("font-weight", "bold");
            //        Lbl5ckop.ToolTip = yetkili + " " + telno + " " + cepno + " " + uyari;
            //    }
            //    else
            //    { Lbl5ckop.ToolTip = yetkili + " " + telno + " " + cepno; }
            //}
            // demirler kısaltma
            Label Lbl5rihtimkop = (Label)e.Item.FindControl("Lbl5rih");
            Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
            if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
            else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
            else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
            else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
            else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
            else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
            {
                demirkisakop.Text = "OTY";
                Lbl5ckop.Text = "Ordino-Talep Yok";
                Lbl5ckop.Style.Add("font-style", "normal");
                Lbl5ckop.Style.Add("color", "#ee1111");
                Lbl5rihtimkop.Text = "";
            }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }

            Label Lbl15dkop = (Label)e.Item.FindControl("Lbl15d");
            if (string.IsNullOrEmpty(Lbl15dkop.Text) == true)
            {
                
                HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warngemitd");
                td.Attributes.Add("style", "background-color:#ffaaaa");
            }

            //2 saat kala eta kırmızı
            Label etakop = (Label)e.Item.FindControl("Lbl4full");
            Label etasaatkop = (Label)e.Item.FindControl("Lbl4hour");
            if (Convert.ToDateTime(etakop.Text) > DateTime.Now.AddHours(-2))
            {
                if (Convert.ToDateTime(etakop.Text).AddHours(-2) <= DateTime.Now)
                {
                    etasaatkop.Style.Add("color", "#ee1111");
                }
            }


            Label Lbl14dkop = (Label)e.Item.FindControl("Lbl14d");
            Label Lbl14ekop = (Label)e.Item.FindControl("Lbl14e");
            Label Lbl14fkop = (Label)e.Item.FindControl("Lbl14f");
            if (Lbl14dkop.Text != "0")
            {
                if (Lbl14fkop.Text.ToLower() == "no")
                {
                    Lbl14dkop.Style.Add("font-style", "normal");
                    Lbl14dkop.Style.Add("color", "#ee1111");
                }
                else if (Lbl14fkop.Text.ToLower() == "yes")
                {
                    Lbl14dkop.Style.Add("font-style", "normal");
                    Lbl14dkop.Style.Add("color", "#11bb11");
                }
            }
            if (Lbl14ekop.Text != "0")
            {
                if (Lbl14fkop.Text.ToLower() == "no")
                {
                    Lbl14ekop.Style.Add("font-style", "normal");
                    Lbl14ekop.Style.Add("color", "#ee1111");
                }
                else if (Lbl14fkop.Text.ToLower() == "yes")
                {
                    Lbl14ekop.Style.Add("font-style", "normal");
                    Lbl14ekop.Style.Add("color", "#11bb11");
                }
            }

            HtmlTableCell td1 = (HtmlTableCell)e.Item.FindControl("tdlcb");
            Label Labellcbdatekop = (Label)e.Item.FindControl("Labellcbdate");
            if (Labellcbdatekop.Text != "")
            {
                if (Convert.ToDateTime(Labellcbdatekop.Text).AddHours(6) < DateTime.Now)
                {
                    td1.Attributes.Add("style", "background-color:#00fffa");
                }
                if (Labellcbdatekop.Text != "" && Labellcbdatekop.Text != null)
                {
                    Labellcbdatekop.Style.Add("color", "#1111ee");
                    // Labellcbdatekop.Style.Add("font-weight", "normal");
                }
            }


            // lybyol varsa koyu pembe

            Label Lbllybkop = (Label)e.Item.FindControl("Lbllyb");
            if (Lbllybkop.Text != "" && Lbllybkop.Text != null)
            {
                Lbl5ckop.Style.Add("color", "#e100a0");
                Lbl5ckop.Style.Add("font-weight", "bold");
            }

            //or bak            
            Button LBpatakop = (Button)e.Item.FindControl("LBpata");
            Label Lbl11kop = (Label)e.Item.FindControl("Lbl11");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(LBpatakop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl11kop.Style.Add("color", "#e100a0");
                Lbl11kop.Style.Add("font-weight", "bold");
            }
            else
            {
                Lbl11kop.Style.Add("color", "#333333");
            }
            orbak.Dispose();

        }
        baglanti.Close();
    }
    protected void DLVIPortKosb_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        //satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
 
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            // tan rengi farklı
            Label Lbl7kop = (Label)e.Item.FindControl("Lbl7");
            if (Lbl7kop.Text.ToLower() == "tan")
            {
                Lbl7kop.Style.Add("color", "#ee1111");
            }
            else if (Lbl7kop.Text.ToLower() == "gnr")
            {
                Lbl7kop.Style.Add("color", "#11bb11");
            }
            // * not rengi kırmızı
            Label notlarckop = (Label)e.Item.FindControl("notlar");
            string uzatmakelimesi = notlarckop.Text.ToLower() + "uzat";
            if (uzatmakelimesi.Substring(0, 3) == "byd")
            {
                notlarckop.Style.Add("color", "#e100a0");
            }
            else if (uzatmakelimesi.Substring(0, 1) == "*")
            {
                notlarckop.Style.Add("color", "#ee1111");
            }
            else if (uzatmakelimesi.Substring(0, 1) == "+")
            {
                notlarckop.Style.Add("color", "#1111ee");
            }
            
            if (uzatmakelimesi.Substring(0, 1) == ".")
            {HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warntd");
                td.Attributes.Add("style", "background-color:#9FF3A1"); }

            //uyarı olan liman rengi farklı
            Label Lbl5ckop = (Label)e.Item.FindControl("Lbl5");
            //string yanasmalimani = Lbl5ckop.Text;

            //if (yanasmalimani != "Yelkenkaya")
            //{
            //    string yetkili = "";
            //    string telno = "";
            //    string cepno = "";
            //    string uyari = "";
            //    SqlCommand cmdisokuup = new SqlCommand("SP_LimanBilgilerSec", baglanti);
            //    cmdisokuup.CommandType = CommandType.StoredProcedure;
            //    cmdisokuup.Parameters.AddWithValue("@limanadi", yanasmalimani);
            //    SqlDataReader dr = cmdisokuup.ExecuteReader();
            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            yetkili = dr["yetkili"].ToString();
            //            telno = dr["telno"].ToString();
            //            cepno = dr["cepno"].ToString();
            //            uyari = dr["uyari"].ToString();
            //        }
            //    }
            //    dr.Close();
            //    cmdisokuup.Dispose();

            //    if (uyari != "")
            //    {
            //        Lbl5ckop.Style.Add("font-weight", "bold");
            //        Lbl5ckop.ToolTip = yetkili + " " + telno + " " + cepno + " " + uyari;
            //    }
            //    else
            //    { Lbl5ckop.ToolTip = yetkili + " " + telno + " " + cepno; }
            //}
            // demirler kısaltma
            Label Lbl5rihtimkop = (Label)e.Item.FindControl("Lbl5rih");
            Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
            if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
            else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
            else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
            else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
            else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
            else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
            {
                demirkisakop.Text = "OTY";
                Lbl5ckop.Text = "Ordino-Talep Yok";
                Lbl5ckop.Style.Add("font-style", "normal");
                Lbl5ckop.Style.Add("color", "#ee1111");
                Lbl5rihtimkop.Text = "";
            }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }

            Label Lbl15dkop = (Label)e.Item.FindControl("Lbl15d");
            if (string.IsNullOrEmpty(Lbl15dkop.Text) == true)
            {
                
                HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("warngemitd");
                td.Attributes.Add("style", "background-color:#ffaaaa");
            }

            //2 saat kala eta kırmızı
            Label etakop = (Label)e.Item.FindControl("Lbl4full");
            Label etasaatkop = (Label)e.Item.FindControl("Lbl4hour");
            if (Convert.ToDateTime(etakop.Text) > DateTime.Now.AddHours(-2))
            {
                if (Convert.ToDateTime(etakop.Text).AddHours(-2) <= DateTime.Now)
                {
                    etasaatkop.Style.Add("color", "#ee1111");
                }
            }

            Label Lbl14dkop = (Label)e.Item.FindControl("Lbl14d");
            Label Lbl14ekop = (Label)e.Item.FindControl("Lbl14e");
            Label Lbl14fkop = (Label)e.Item.FindControl("Lbl14f");
            if (Lbl14dkop.Text != "0")
            {
                if (Lbl14fkop.Text.ToLower() == "no")
                {
                    Lbl14dkop.Style.Add("font-style", "normal");
                    Lbl14dkop.Style.Add("color", "#ee1111");
                }
                else if (Lbl14fkop.Text.ToLower() == "yes")
                {
                    Lbl14dkop.Style.Add("font-style", "normal");
                    Lbl14dkop.Style.Add("color", "#11bb11");
                }
            }
            if (Lbl14ekop.Text != "0")
            {
                if (Lbl14fkop.Text.ToLower() == "no")
                {
                    Lbl14ekop.Style.Add("font-style", "normal");
                    Lbl14ekop.Style.Add("color", "#ee1111");
                }
                else if (Lbl14fkop.Text.ToLower() == "yes")
                {
                    Lbl14ekop.Style.Add("font-style", "normal");
                    Lbl14ekop.Style.Add("color", "#11bb11");
                }
            }

            HtmlTableCell td1 = (HtmlTableCell)e.Item.FindControl("tdlcb");
            Label Labellcbdatekop = (Label)e.Item.FindControl("Labellcbdate");
            if (Labellcbdatekop.Text != "")
            {
                if (Convert.ToDateTime(Labellcbdatekop.Text).AddHours(6) < DateTime.Now)
                {
                    td1.Attributes.Add("style", "background-color:#00fffa");
                }
                if (Labellcbdatekop.Text != "" && Labellcbdatekop.Text != null)
                {
                    Labellcbdatekop.Style.Add("color", "#1111ee");
                    // Labellcbdatekop.Style.Add("font-weight", "normal");
                }
            }

            // lybyol varsa koyu pembe

            Label Lbllybkop = (Label)e.Item.FindControl("Lbllyb");
            if (Lbllybkop.Text != "" && Lbllybkop.Text != null)
            {
                Lbl5ckop.Style.Add("color", "#e100a0");
                Lbl5ckop.Style.Add("font-weight", "bold");
            }

            //or bak            
            Button LBpatakop = (Button)e.Item.FindControl("LBpata");
            Label Lbl11kop = (Label)e.Item.FindControl("Lbl11");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(LBpatakop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl11kop.Style.Add("color", "#e100a0");
                Lbl11kop.Style.Add("font-weight", "bold");
            }
            else
            {
                Lbl11kop.Style.Add("color", "#333333");
            }
            orbak.Dispose();
        }
        
        baglanti.Close();
    }




    protected void ImageButtonJobEdit_Click(object sender, ImageClickEventArgs e)
    {
        TBJEetadt.BorderColor = System.Drawing.Color.Gray;
        TBJEetadt.BorderWidth = 1;
        TBJEbt.BorderColor = System.Drawing.Color.Gray;
        TBJEbt.BorderWidth = 1;
        TBJEst.BorderColor = System.Drawing.Color.Gray;
        TBJEst.BorderWidth = 1;
        TBJEgrt.BorderColor = System.Drawing.Color.Gray;
        TBJEgrt.BorderWidth = 1;
        TBJEloa.BorderColor = System.Drawing.Color.Gray;
        TBJEloa.BorderWidth = 1;
        TBJEagency.BorderColor = System.Drawing.Color.Gray;
        TBJEagency.BorderWidth = 1;
        TBJEinvoice.BorderColor = System.Drawing.Color.Gray;
        TBJEinvoice.BorderWidth = 1;
        TBJEnotes.BorderColor = System.Drawing.Color.Gray;
        TBJEnotes.BorderWidth = 1;
        TBJEreqno.BorderColor = System.Drawing.Color.Gray;
        TBJEreqno.BorderWidth = 1;
        DDLJEap.BorderColor = System.Drawing.Color.Gray;
        DDLJEdp.BorderColor = System.Drawing.Color.Gray;
        DDLJEflag.BorderColor = System.Drawing.Color.Gray;
        DDLJEtip.BorderColor = System.Drawing.Color.Gray;
        DDLJEdb.BorderColor = System.Drawing.Color.Gray;
        DDLJEdc.BorderColor = System.Drawing.Color.Gray;
        DDLJEdc.BorderWidth = 1;
        DDLJEap.BorderWidth = 1;
        DDLJEdp.BorderWidth = 1;
        DDLJEflag.BorderWidth = 1;
        DDLJEtip.BorderWidth = 1;
        DDLJEdb.BorderWidth = 1;
        TBJEpratikano.BorderColor = System.Drawing.Color.Gray;
        TBJEpratikano.BorderWidth = 1;
        DDLJEpratika.BorderColor = System.Drawing.Color.Gray;
        DDLJEpratika.BorderWidth = 1;

        DDLJEdb.Visible = true;

        ImageButton ImageButtonJobEditkopya = (ImageButton)sender;
        string shipid = HttpUtility.HtmlDecode(ImageButtonJobEditkopya.CommandArgument).ToString();
        LJEid.Text = shipid;
        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdgemibul = new SqlCommand("SP_Isliste_gemibilgi", baglanti);
        cmdgemibul.CommandType = CommandType.StoredProcedure;
        cmdgemibul.Parameters.AddWithValue("@id", Convert.ToInt32(shipid));
        SqlDataReader gemireader = cmdgemibul.ExecuteReader();

        string demiryeri = "";
        string bayrak = "";
        string tip = "";
        string tehlikeliyuk = "";
        string yanasmalimani = "";
        string yanasmarihtimi = "";
        string pratika = "";

        if (gemireader.HasRows)
        {
            while (gemireader.Read())
            {
                LBJEimo.Text = gemireader["imono"].ToString();
                LBJEsn.Text = gemireader["gemiadi"].ToString();
                demiryeri = gemireader["demiryeri"].ToString();
                bayrak = gemireader["bayrak"].ToString();
                tip = gemireader["tip"].ToString();
                TBJEetadt.Text = gemireader["eta"].ToString();
                LblDDLJEdepp.Text = gemireader["kalkislimani"].ToString();
                LblJEdepb.Text = gemireader["kalkisrihtimi"].ToString(); 
                yanasmalimani = gemireader["yanasmalimani"].ToString();
                yanasmarihtimi = gemireader["yanasmarihtimi"].ToString();
                tehlikeliyuk = gemireader["tehlikeliyuk"].ToString();
                TBJEbt.Text = gemireader["bowt"].ToString();
                TBJEst.Text = gemireader["strnt"].ToString();
                TBJEgrt.Text = gemireader["grt"].ToString();
                TBJEloa.Text = gemireader["loa"].ToString();
                LblJEdrft.Text = gemireader["draft"].ToString();
                Labeltpp.Text = gemireader["bilgi"].ToString();
                TBJEagency.Text = gemireader["acente"].ToString();
                TBJEinvoice.Text = gemireader["fatura"].ToString();
                TBJEnotes.Text = gemireader["notlar"].ToString();
                TBJEreqno.Text = gemireader["talepno"].ToString();
                Lblkayitzamani.Text = gemireader["kayitzamani"].ToString();
                Lblnedurumda.Text = gemireader["nedurumda"].ToString();
                Lblnedurumdaopr.Text = gemireader["nedurumdaopr"].ToString();
                pratika = gemireader["pratika"].ToString();
                TBJEpratikano.Text = gemireader["pratikano"].ToString();

            }
        }
        gemireader.Close();

        if (Convert.ToDateTime(TBJEetadt.Text) < DateTime.Now)
        {
            TBJEetadt.Text = TarihSaatYaziYapDMYhm(DateTime.Now);
        }

        if (LblJEdepb.Text == "0")
        { LblJEdepb.Visible = false; }
        else
        { LblJEdepb.Visible = true; }

        SqlCommand cmdDDLliman = new SqlCommand("SP_DDLlimanal", baglanti);
        cmdDDLliman.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterp = new SqlDataAdapter();
        adapterp.SelectCommand = cmdDDLliman;
        DataSet dsp = new DataSet();
        adapterp.Fill(dsp, "limanlar");
        DDLJEdp.Items.Clear();
        DDLJEdp.DataValueField = "limanno";
        DDLJEdp.DataTextField = "limanadi";
        DDLJEdp.DataSource = dsp;
        DDLJEdp.DataBind();
        DDLJEdp.ClearSelection();
        if (yanasmalimani != "") { DDLJEdp.Items.FindByText(yanasmalimani).Selected = true; }

        //varış rihtim
        SqlCommand cmdDDLrihtim2 = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim2.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim2.Parameters.AddWithValue("@limanadi", DDLJEdp.SelectedItem.Text);
        SqlDataAdapter adapterr2 = new SqlDataAdapter();
        adapterr2.SelectCommand = cmdDDLrihtim2;
        DataSet dsr2 = new DataSet();
        adapterr2.Fill(dsr2, "limanlar");
        DDLJEdb.Items.Clear();
        DDLJEdb.DataValueField = "id";
        DDLJEdb.DataTextField = "rihtimadi";
        DDLJEdb.DataSource = dsr2;
        DDLJEdb.DataBind();
        DDLJEdb.Items.FindByText(yanasmarihtimi).Selected = true;
        if (yanasmarihtimi == "0") { DDLJEdb.Visible = false; }

        SqlCommand cmdDDLdemir = new SqlCommand("SP_DDLdemiral", baglanti);
        cmdDDLdemir.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterdem = new SqlDataAdapter();
        adapterdem.SelectCommand = cmdDDLdemir;
        DataSet dsdem = new DataSet();
        adapterdem.Fill(dsdem, "limanlar");
        DDLJEap.Items.Clear(); //edit
        DDLJEap.DataValueField = "limanno";
        DDLJEap.DataTextField = "limanadi";
        DDLJEap.DataSource = dsdem;
        DDLJEap.DataBind();
        DDLJEap.ClearSelection();
        if (demiryeri != "") { DDLJEap.Items.FindByText(demiryeri).Selected = true; }

        SqlCommand cmdDDLflag = new SqlCommand("SP_DDLflagal", baglanti);
        cmdDDLflag.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterflag = new SqlDataAdapter();
        adapterflag.SelectCommand = cmdDDLflag;
        DataSet dsflag = new DataSet();
        adapterflag.Fill(dsflag, "flaglist");
        DDLJEflag.Items.Clear();//edit
        DDLJEflag.DataValueField = "id";
        DDLJEflag.DataTextField = "flag";
        DDLJEflag.DataSource = dsflag;
        DDLJEflag.DataBind();
        DDLJEflag.ClearSelection();
        if (bayrak != "") { DDLJEflag.Items.FindByText(bayrak).Selected = true; }

        SqlCommand cmdDDLtip = new SqlCommand("SP_DDLshiptip", baglanti);
        cmdDDLtip.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adaptertip = new SqlDataAdapter();
        adaptertip.SelectCommand = cmdDDLtip;
        DataSet dstip = new DataSet();
        adaptertip.Fill(dstip, "shiptip");
        DDLJEtip.Items.Clear();
        DDLJEtip.DataValueField = "id";
        DDLJEtip.DataTextField = "tip";
        DDLJEtip.DataSource = dstip;
        DDLJEtip.DataBind();
        DDLJEtip.ClearSelection();
        if (tip != "") { DDLJEtip.Items.FindByText(tip).Selected = true; }

        DDLJEdc.ClearSelection();
        DDLJEdc.Items.FindByText(tehlikeliyuk).Selected = true;

        baglanti.Close();


        if (string.IsNullOrEmpty(pratika) != true)
        {
            DDLJEpratika.ClearSelection();
            DDLJEpratika.Items.FindByText(pratika).Selected = true;
        }

        baglanti.Close();
        if (DDLJEpratika.SelectedItem.Text == "Yes")
        {
            TBJEpratikano.Visible = true;

        }
        else
        {
            TBJEpratikano.Visible = false;
        }


        this.ModalPopupExtenderjobedit.Show();
    }
    protected void Buttonisedit_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        TBJEetadt.BorderColor = System.Drawing.Color.Gray;
        TBJEetadt.BorderWidth = 1;
        TBJEbt.BorderColor = System.Drawing.Color.Gray;
        TBJEbt.BorderWidth = 1;
        TBJEst.BorderColor = System.Drawing.Color.Gray;
        TBJEst.BorderWidth = 1;
        TBJEgrt.BorderColor = System.Drawing.Color.Gray;
        TBJEgrt.BorderWidth = 1;
        TBJEloa.BorderColor = System.Drawing.Color.Gray;
        TBJEloa.BorderWidth = 1;
        TBJEagency.BorderColor = System.Drawing.Color.Gray;
        TBJEagency.BorderWidth = 1;
        TBJEinvoice.BorderColor = System.Drawing.Color.Gray;
        TBJEinvoice.BorderWidth = 1;
        TBJEnotes.BorderColor = System.Drawing.Color.Gray;
        TBJEnotes.BorderWidth = 1;
        TBJEreqno.BorderColor = System.Drawing.Color.Gray;
        TBJEreqno.BorderWidth = 1;
        DDLJEap.BorderColor = System.Drawing.Color.Gray;
        DDLJEdp.BorderColor = System.Drawing.Color.Gray;
        DDLJEflag.BorderColor = System.Drawing.Color.Gray;
        DDLJEtip.BorderColor = System.Drawing.Color.Gray;
        DDLJEdb.BorderColor = System.Drawing.Color.Gray;
        DDLJEdc.BorderColor = System.Drawing.Color.Gray;
        TBJEpratikano.BorderColor = System.Drawing.Color.Gray;
        TBJEpratikano.BorderWidth = 1;
        DDLJEpratika.BorderColor = System.Drawing.Color.Gray;
        DDLJEpratika.BorderWidth = 1;
        DDLJEdc.BorderWidth = 1;
        DDLJEap.BorderWidth = 1;
        DDLJEdp.BorderWidth = 1;
        DDLJEflag.BorderWidth = 1;
        DDLJEtip.BorderWidth = 1;
        DDLJEdb.BorderWidth = 1;

        string imono = LBJEimo.Text;
        string gemiadi = LBJEsn.Text;
        string demiryeri = HttpUtility.HtmlDecode(DDLJEap.SelectedItem.Text);
        string kalkislimani = LblDDLJEdepp.Text;
        string kalkisrihtimi = LblJEdepb.Text;
        string yanasmalimani = HttpUtility.HtmlDecode(DDLJEdp.SelectedItem.Text);
        string yanasmarihtimi = HttpUtility.HtmlDecode(DDLJEdb.SelectedItem.Text);
        string eta = TBJEetadt.Text;
        string bowt = TBJEbt.Text;
        string strnt = TBJEst.Text;
        string bayrak = HttpUtility.HtmlDecode(DDLJEflag.SelectedItem.Text);
        string grt = TBJEgrt.Text;
        string draft = LblJEdrft.Text;
        string tip = HttpUtility.HtmlDecode(DDLJEtip.SelectedItem.Text);
        string loa = TBJEloa.Text;
        string tehlikeliyuk = HttpUtility.HtmlDecode(DDLJEdc.SelectedItem.Text);
        string acente = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEagency.Text.ToString().Trim().ToLower()));
        string fatura = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEinvoice.Text.ToString().Trim().ToLower()));
        string notlar = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEnotes.Text.ToString().Trim().ToLower()));
        string talepno = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEreqno.Text.ToString().Trim().ToLower()));
        string kayitzamani = TarihSaatYaziYapDMYhm(DateTime.Now);
        string pratika = HttpUtility.HtmlDecode(DDLJEpratika.SelectedItem.Text);
        string pratikano = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEpratikano.Text.ToString().Trim().ToLower()));

        if (eta == "" || eta == null || eta == "__.__.____ __:__")
        {
            eta = "";
            TBJEetadt.BorderColor = System.Drawing.Color.Red;
            TBJEetadt.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        else if (IsDate2(eta) != true)
        {
            eta = "";
            TBJEetadt.BorderColor = System.Drawing.Color.Red;
            TBJEetadt.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }

        if (bowt == "" || bowt == null)
        {
            bowt = "";
            TBJEbt.BorderColor = System.Drawing.Color.Red;
            TBJEbt.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (strnt == "" || strnt == null)
        {
            strnt = "";
            TBJEst.BorderColor = System.Drawing.Color.Red;
            TBJEst.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (grt == "" || grt == null)
        {
            grt = "";
            TBJEgrt.BorderColor = System.Drawing.Color.Red;
            TBJEgrt.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (loa == "" || loa == null)
        {
            loa = "";
            TBJEloa.BorderColor = System.Drawing.Color.Red;
            TBJEloa.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }

        if (acente == "" || acente == null)
        {
            acente = "";
            TBJEagency.BorderColor = System.Drawing.Color.Red;
            TBJEagency.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (fatura == "" || fatura == null)
        {
            fatura = "";
            TBJEinvoice.BorderColor = System.Drawing.Color.Red;
            TBJEinvoice.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }

        if (notlar == "" || notlar == null)
        {
            notlar = "";
            TBJEnotes.BorderColor = System.Drawing.Color.Red;
            TBJEnotes.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        if (talepno == "" || talepno == null)
        {
            talepno = "";
            TBJEreqno.BorderColor = System.Drawing.Color.Red;
            TBJEreqno.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }

        if (pratika == "Yes")
        {
            if (pratikano == "" || pratikano == null)
            {
                pratikano = "";
                TBJEpratikano.BorderColor = System.Drawing.Color.Red;
                TBJEpratikano.BorderWidth = 1;
                this.ModalPopupExtenderjobedit.Show();
            }

        }

        if (demiryeri != "" && yanasmalimani != "" && yanasmarihtimi != "" && eta != "" && bowt != "" && strnt != "" && bayrak != "" && grt != "" && tip != "" && loa != "" && tehlikeliyuk != "" && acente != "" && fatura != "" && notlar != "" && talepno != "")
        {
  
                if (Convert.ToDateTime(eta) < DateTime.Now)
                {
                    eta = kayitzamani;
                }


            string bilgi = "0";
            bilgi = AnaKlas.tpphesapla(grt, tip, loa, bowt, strnt, tehlikeliyuk, kalkisrihtimi, yanasmarihtimi)[0];
            draft = AnaKlas.tpphesapla(grt, tip, loa, bowt, strnt, tehlikeliyuk, kalkisrihtimi, yanasmarihtimi)[1];

            string isimkisalt = "";
   
                SqlCommand cmdisimbul = new SqlCommand("Select kapadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
                string isimbul = cmdisimbul.ExecuteScalar().ToString();
                cmdisimbul.Dispose();
                SqlCommand cmdsoyisimbul = new SqlCommand("Select kapsoyadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
                string soyisimbul = cmdsoyisimbul.ExecuteScalar().ToString();
                //if (soyisimbul.ToLower() == "yüksel") { soyisimbul = "Küksel"; }
                cmdsoyisimbul.Dispose();
                isimkisalt = isimbul.Substring(0, 1) + soyisimbul.Substring(0, 1);

                if(notlar.Length > 13 && notlar.EndsWith(")") && notlar.Substring(notlar.Length-5,1)=="-")
                { notlar = notlar.Substring(0,notlar.Length - 14); }
 
            

            int secimedit = Convert.ToInt32(LJEid.Text);

                SqlCommand cmd = new SqlCommand("SP_Up_Isliste_full", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", secimedit);
                cmd.Parameters.AddWithValue("imono", imono);
                cmd.Parameters.AddWithValue("gemiadi", gemiadi);
                cmd.Parameters.AddWithValue("demiryeri", demiryeri);
                cmd.Parameters.AddWithValue("kalkislimani", kalkislimani);
                cmd.Parameters.AddWithValue("kalkisrihtimi", kalkisrihtimi);
                cmd.Parameters.AddWithValue("yanasmalimani", yanasmalimani);
                cmd.Parameters.AddWithValue("yanasmarihtimi", yanasmarihtimi);
                cmd.Parameters.AddWithValue("eta", eta);
                cmd.Parameters.AddWithValue("bowt", bowt);
                cmd.Parameters.AddWithValue("strnt", strnt);
                cmd.Parameters.AddWithValue("bayrak", bayrak);
                cmd.Parameters.AddWithValue("grt", grt);
                cmd.Parameters.AddWithValue("tip", tip);
                cmd.Parameters.AddWithValue("loa", loa);
                cmd.Parameters.AddWithValue("draft", draft);
                cmd.Parameters.AddWithValue("bilgi", bilgi);
                cmd.Parameters.AddWithValue("tehlikeliyuk", tehlikeliyuk);
                cmd.Parameters.AddWithValue("acente", acente);
                cmd.Parameters.AddWithValue("fatura", fatura);
                cmd.Parameters.AddWithValue("notlar", notlar);
                cmd.Parameters.AddWithValue("nedurumda", Lblnedurumda.Text);
                cmd.Parameters.AddWithValue("nedurumdaopr", Lblnedurumdaopr.Text);
                cmd.Parameters.AddWithValue("talepno", talepno);
            cmd.Parameters.AddWithValue("pratika", pratika);
            cmd.Parameters.AddWithValue("pratikano", pratikano);
                cmd.Parameters.AddWithValue("kayitzamani", Lblkayitzamani.Text);
                cmd.Parameters.AddWithValue("kaydeden", isimkisalt + " " + DateTime.Now.ToShortDateString().Substring(0, 2) + "|" + DateTime.Now.ToShortTimeString().Substring(0, 2));
                cmd.ExecuteNonQuery();
                cmd.Dispose();

            //log_newjob

            SqlCommand cmdlognj = new SqlCommand("SP_log_newjob_ekle", baglanti);
            cmdlognj.CommandType = CommandType.StoredProcedure;
            cmdlognj.Parameters.AddWithValue("imono", imono);
            cmdlognj.Parameters.AddWithValue("gemiadi", gemiadi);
            cmdlognj.Parameters.AddWithValue("demiryeri", demiryeri);
            cmdlognj.Parameters.AddWithValue("kalkislimani", kalkislimani);
            cmdlognj.Parameters.AddWithValue("kalkisrihtimi", kalkisrihtimi);
            cmdlognj.Parameters.AddWithValue("yanasmalimani", yanasmalimani);
            cmdlognj.Parameters.AddWithValue("yanasmarihtimi", yanasmarihtimi);
            cmdlognj.Parameters.AddWithValue("eta", eta);
            cmdlognj.Parameters.AddWithValue("bowt", bowt);
            cmdlognj.Parameters.AddWithValue("strnt", strnt);
            cmdlognj.Parameters.AddWithValue("bayrak", bayrak);
            cmdlognj.Parameters.AddWithValue("grt", grt);
            cmdlognj.Parameters.AddWithValue("tip", tip);
            cmdlognj.Parameters.AddWithValue("loa", loa);
            cmdlognj.Parameters.AddWithValue("draft", draft);
            cmdlognj.Parameters.AddWithValue("bilgi", bilgi);
            cmdlognj.Parameters.AddWithValue("tehlikeliyuk", tehlikeliyuk);
            cmdlognj.Parameters.AddWithValue("acente", acente);
            cmdlognj.Parameters.AddWithValue("fatura", fatura);
            cmdlognj.Parameters.AddWithValue("notlar", notlar);
            cmdlognj.Parameters.AddWithValue("pratika", pratika);
            cmdlognj.Parameters.AddWithValue("pratikano", pratikano);
            cmdlognj.Parameters.AddWithValue("lcbno", "");
            cmdlognj.Parameters.AddWithValue("lcbdest", "");
            cmdlognj.Parameters.AddWithValue("lcbdate", "");
            cmdlognj.Parameters.AddWithValue("talepno", talepno);
            cmdlognj.Parameters.AddWithValue("nedurumda", Lblnedurumda.Text);
            cmdlognj.Parameters.AddWithValue("nedurumdaopr", Lblnedurumdaopr.Text);
            cmdlognj.Parameters.AddWithValue("gizlegoster", "1");
            cmdlognj.Parameters.AddWithValue("kayitzamani", kayitzamani);
            cmdlognj.Parameters.AddWithValue("kaydeden", isimbul + " " + soyisimbul);
            cmdlognj.ExecuteNonQuery();
            cmdlognj.Dispose();

        }
        DTLoading(baglanti);
        baglanti.Close();




    }


   protected void DDLJEdp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLJEdb.Visible = true;
        DDLJEdb.Items.Clear();
        this.ModalPopupExtenderjobedit.Show();

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim.Parameters.AddWithValue("@limanadi", DDLJEdp.SelectedItem.Text);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdDDLrihtim;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "limanlar");
        DDLJEdb.DataValueField = "id";
        DDLJEdb.DataTextField = "rihtimadi";
        DDLJEdb.DataSource = ds;
        DDLJEdb.DataBind();
        baglanti.Close();

        if (DDLJEdb.SelectedItem.Text == "0") { DDLJEdb.Visible = false; }
    }

    protected void DDLdesplace_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLdesplaceno.Enabled = false;
        Buttonpilotata.Enabled = false;
        DDLnompilot.Items.Clear();
        DDLnompilot.Enabled = false;
        CBdeletepob.Checked = false;
        Buttonpilotata.Text = "Nominate";
        patauyari.Text = "";

        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
        cmdLimannoal.CommandType = CommandType.StoredProcedure;
        cmdLimannoal.Parameters.AddWithValue("@limanadi", DDLdesplace.SelectedItem.Text);
        cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int);
        cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
        cmdLimannoal.ExecuteNonQuery();
        int limannobul = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString());
        cmdLimannoal.Dispose();


        if ((limannobul > 1000 && limannobul < 1050) || limannobul == 998 || limannobul == 999 || limannobul == 997)
        {
            DDLdesplaceno.Items.Insert(0, new ListItem("0", String.Empty));
            DDLdesplaceno.SelectedIndex = 0;
            DDLdesplaceno.Visible = false;
            DDLnompilot.Enabled = true;
            pilotsecpro(Lblgrt.Text);
        }
        else
        {
            SqlCommand cmdDDLrihtimk = new SqlCommand("SP_DDLrihtimal", baglanti);
            cmdDDLrihtimk.CommandType = CommandType.StoredProcedure;
            cmdDDLrihtimk.Parameters.AddWithValue("@limanadi", DDLdesplace.SelectedItem.Text);
            SqlDataAdapter adapterrk = new SqlDataAdapter();
            adapterrk.SelectCommand = cmdDDLrihtimk;
            DataSet dsrk = new DataSet();
            adapterrk.Fill(dsrk, "limanlarrk");
            DDLdesplaceno.Items.Clear();
            DDLdesplaceno.DataValueField = "id";
            DDLdesplaceno.DataTextField = "rihtimadi";
            DDLdesplaceno.DataSource = dsrk;
            DDLdesplaceno.DataBind();
            DDLdesplaceno.Items.Insert(0, new ListItem("Select", String.Empty));
            DDLdesplaceno.SelectedIndex = 0;
            DDLdesplaceno.Visible = true;
            DDLdesplaceno.Enabled = true;
			DDLnompilot.Enabled = false;
        }
        baglanti.Close();
        this.ModalPopupExtenderpilotata.Show();
    }
    protected void DDLdesplaceno_SelectedIndexChanged(object sender, EventArgs e)
    {
            Buttonpilotata.Text = "Nominate";
        patauyari.Text = "";
        if (DDLdesplaceno.SelectedItem.Text == "Select")
        {
            DDLnompilot.Items.Clear();
            DDLnompilot.Enabled = false;
            Buttonpilotata.Enabled = false;
        }

        else
        {
            CBdeletepob.Checked = false;
            DDLnompilot.Enabled = true;
            pilotsecpro(Lblgrt.Text);
        }
        this.ModalPopupExtenderpilotata.Show();
    }
    protected void DDLnompilot_SelectedIndexChanged(object sender, EventArgs e)
    {
        CBdeletepob.Checked = false;
        Buttonpilotata.Enabled = true;
        Buttonpilotata.Text = "Nominate";
        patauyari.Text = "";
        this.ModalPopupExtenderpilotata.Show();
    }

    protected void LBpata_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        Button LBpatakopya = (Button)sender;
        String seciliislistids = (LBpatakopya.CommandArgument).ToString();
        int seciliislistid = Convert.ToInt32(seciliislistids);

        Buttonpilotatac.CommandArgument = seciliislistids;
        CBdeletepob.Checked = false;

        string gemiadi = "";
        string kalkislimani = "";
        string kalkisrihtimi = "";
        string demiryeri = "";
        string yanasmalimani = "";
        string yanasmarihtimi = "";
        string eta = "";
        string grt = "";
        string nedurumda = "";
        string bowt = "0";
        string strnt = "0";

        SqlCommand cmdisokuup = new SqlCommand("SP_Isliste_gemibilgi", baglanti);
        cmdisokuup.CommandType = CommandType.StoredProcedure;
        cmdisokuup.Parameters.AddWithValue("@id", seciliislistid);
        SqlDataReader dr = cmdisokuup.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                gemiadi = dr["gemiadi"].ToString();
                kalkislimani = dr["kalkislimani"].ToString();
                kalkisrihtimi = dr["kalkisrihtimi"].ToString();
                demiryeri = dr["demiryeri"].ToString();
                yanasmalimani = dr["yanasmalimani"].ToString();
                yanasmarihtimi = dr["yanasmarihtimi"].ToString();
                eta = dr["eta"].ToString();
                grt = dr["grt"].ToString();
                nedurumda = dr["nedurumda"].ToString();
                bowt = dr["bowt"].ToString();
                strnt = dr["strnt"].ToString();
            }
        }
        dr.Close();
        cmdisokuup.Dispose();


        Buttonpilotatac.Enabled = true;

        if (nedurumda != "7" && nedurumda != "6") // durum 67 değilse kontact açılır
        {
            patauyaric.Text = "";
            Buttonpilotata.CommandArgument = seciliislistids;

            TBgemiadic.Text = gemiadi;

            if (yanasmalimani.Trim() == "To Order")
            {
                patauyaric.Text = "Destination place cannot be 'To Order'. ";
                Buttonpilotatac.Enabled = false;
            }
            if (demiryeri.ToLower() == "ordino-talep yok")
            {
                patauyaric.Text = "This ship has not 'Ordino', Cannot give a pilot. ";
                Buttonpilotatac.Enabled = false;
            }

            TBetadatetimec.Text = TarihSaatYaziYapDMYhm(DateTime.Now.AddHours(1));

            baglanti.Close();
            this.ModalPopupExtenderpilotatac.Show();
        }



        else  // yoksa pilot atama açılıyor
        {
            Buttonpilotatac.Visible = true;
            DDLdesplace.Enabled = true;
            DDLdesplaceno.Enabled = true;
            Buttonpilotata.Enabled = false;
            Buttonpilotata.Text = "Nominate";
            DDLnompilot.Items.Clear();
            patauyari.Text = "";
            DDLbowok.Enabled = true;
            if (bowt == "0" && strnt == "0")
            { DDLbowok.Enabled = false; }

            Buttonpilotata.CommandArgument = seciliislistids;
            TBgemiadi.Text = gemiadi;
            Lblgrt.Text = grt;
            if (kalkisrihtimi == "0") { Lblpatadep.Text = kalkislimani; }
            else { Lblpatadep.Text = kalkislimani + " / " + kalkisrihtimi; }
            TBetadatetime.Text = TarihSaatYaziYapDMYhm(DateTime.Now.AddMinutes(40));

            //variş liman 
            SqlCommand cmdDDLlim = new SqlCommand("SP_DDLlimanal_notorder", baglanti);
            cmdDDLlim.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmdDDLlim;
            DataSet ds = new DataSet();
            adapter.Fill(ds, "limanlar");
            DDLdesplace.Items.Clear();//p.ata
            DDLdesplace.DataValueField = "limanno";
            DDLdesplace.DataTextField = "limanadi";
            DDLdesplace.DataSource = ds;
            DDLdesplace.DataBind();
            DDLdesplace.ClearSelection();
            cmdDDLlim.Dispose();

            if (yanasmalimani != "")
            {
                DDLdesplace.Items.FindByText(yanasmalimani).Selected = true;
            }

            //variş rıhtım seçim zorunlu
            SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
            cmdLimannoal.CommandType = CommandType.StoredProcedure;
            cmdLimannoal.Parameters.AddWithValue("@limanadi", yanasmalimani);
            cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int);
            cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
            cmdLimannoal.ExecuteNonQuery();
            int limannobul = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString());
            cmdLimannoal.Dispose();


            if ((limannobul > 1000 && limannobul < 1050) || limannobul == 998 || limannobul == 999 || limannobul == 997)
            {
                DDLdesplaceno.Items.Insert(0, new ListItem("0", String.Empty));
                DDLdesplaceno.SelectedIndex = 0;
                DDLdesplaceno.Visible = false;
                DDLnompilot.Enabled = true;
                pilotsecpro(Lblgrt.Text);
            }
            else
            {
                SqlCommand cmdDDLrihtimk = new SqlCommand("SP_DDLrihtimal", baglanti);
                cmdDDLrihtimk.CommandType = CommandType.StoredProcedure;
                cmdDDLrihtimk.Parameters.AddWithValue("@limanadi", yanasmalimani);
                SqlDataAdapter adapterrk = new SqlDataAdapter();
                adapterrk.SelectCommand = cmdDDLrihtimk;
                DataSet dsrk = new DataSet();
                adapterrk.Fill(dsrk, "limanlarrk");
                DDLdesplaceno.Items.Clear();
                DDLdesplaceno.DataValueField = "id";
                DDLdesplaceno.DataTextField = "rihtimadi";
                DDLdesplaceno.DataSource = dsrk;
                DDLdesplaceno.DataBind();
                DDLdesplaceno.Items.Insert(0, new ListItem("Select", String.Empty));
                DDLdesplaceno.SelectedIndex = 0;
                DDLdesplaceno.Visible = true;
                DDLdesplaceno.Enabled = true;
				DDLnompilot.Enabled = false;
            }


            ////variş rihtim
            //SqlCommand cmdDDLrihtimk = new SqlCommand("SP_DDLrihtimal", baglanti);
            //cmdDDLrihtimk.CommandType = CommandType.StoredProcedure;
            //cmdDDLrihtimk.Parameters.AddWithValue("@limanadi", DDLdesplace.SelectedItem.Text);
            //SqlDataAdapter adapterrk = new SqlDataAdapter();
            //adapterrk.SelectCommand = cmdDDLrihtimk;
            //DataSet dsrk = new DataSet();
            //adapterrk.Fill(dsrk, "limanlarrk");
            //DDLdesplaceno.Items.Clear();
            //DDLdesplaceno.DataValueField = "id";
            //DDLdesplaceno.DataTextField = "rihtimadi";
            //DDLdesplaceno.DataSource = dsrk;
            //DDLdesplaceno.DataBind();
            //DDLdesplaceno.Items.FindByText(yanasmarihtimi).Selected = true;
            //cmdDDLrihtimk.Dispose();

            if (yanasmarihtimi == "0")
            {
                DDLdesplaceno.Visible = false;
            }



            TBetadatetime.BorderColor = System.Drawing.Color.Gray;
            TBetadatetime.BorderWidth = 1;

            DDLdesplace.BorderColor = System.Drawing.Color.Gray;
            DDLdesplace.BorderWidth = 1;


            pilotsecpro(grt);
            baglanti.Close();

            this.ModalPopupExtenderpilotata.Show();
        }
    }
    protected void Buttonpilotata_Click(object sender, EventArgs e)
    {
        if (CBdeletepob.Checked == false)
        {
            if (DDLnompilot.SelectedItem.Text == "" || DDLnompilot.SelectedItem.Text == "Select Pilot?" || DDLnompilot.SelectedItem == null || DDLnompilot.SelectedItem.Value == "0")
        {
            this.ModalPopupExtenderpilotata.Show();
            patauyari.Text = "Please Select a Suitable Pilot.";
        }
        else
        {
            PilotAtaSonIslem();
        }
        }
        else // pob cancel tikli ise
        {


            SqlConnection baglanti = AnaKlas.baglan();

            //İŞLİSTE GEMİ NEDURUMDA ESKİ HALİNE AYARLA
            string seciliislistid = Buttonpilotata.CommandArgument;

            string kliman = "";
            string nedurum = "0";
            string nedurumop = "0";

            SqlCommand cmdisokuup = new SqlCommand("SP_Isliste_gemibilgi", baglanti);
            cmdisokuup.CommandType = CommandType.StoredProcedure;
            cmdisokuup.Parameters.AddWithValue("@id", Convert.ToInt32(seciliislistid));
            SqlDataReader dr = cmdisokuup.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    kliman = dr["kalkislimani"].ToString();
                }
            }
            dr.Close();
            cmdisokuup.Dispose();

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


            SqlCommand cmdistnedurumup = new SqlCommand("SP_Up_Isliste_nedurumda_opr", baglanti);
            cmdistnedurumup.CommandType = CommandType.StoredProcedure;
            cmdistnedurumup.Parameters.AddWithValue("@id", seciliislistid);
            cmdistnedurumup.Parameters.AddWithValue("@nedurumda", nedurum);
            cmdistnedurumup.Parameters.AddWithValue("@nedurumdaopr", nedurumop);
            cmdistnedurumup.ExecuteNonQuery();
            cmdistnedurumup.Dispose();

            DTLoading(baglanti);
            baglanti.Close();
        }
    }
    protected void Buttonpilotatac_Click(object sender, EventArgs e)
    {
        TBetadatetimec.BorderColor = System.Drawing.Color.Gray;
        TBetadatetimec.BorderWidth = 1;

        string kayitzamani = TarihSaatYaziYapDMYhm(DateTime.Now);
        string secimedits = Buttonpilotatac.CommandArgument.ToString();
        int secimedit = Convert.ToInt32(secimedits);

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdgemibul = new SqlCommand("SP_Isliste_gemibilgi", baglanti);
        cmdgemibul.CommandType = CommandType.StoredProcedure;
        cmdgemibul.Parameters.AddWithValue("@id", secimedit);
        SqlDataReader gemireader = cmdgemibul.ExecuteReader();

        string kalkislimani = "";
        string bagliistasyon = "1";
        string nedurumda = "";
        string nedurumdaopr = "";

        if (gemireader.HasRows)
        {
            while (gemireader.Read())
            {
                kalkislimani = gemireader["kalkislimani"].ToString();
                nedurumda = gemireader["nedurumda"].ToString();
                nedurumdaopr = gemireader["nedurumdaopr"].ToString();
                Lblpatailkkayit.Text = gemireader["kayitzamani"].ToString();
            }
        }
        gemireader.Close();
        cmdgemibul.Dispose();

        if (TBetadatetimec.Text == "" || TBetadatetimec.Text == null || TBetadatetimec.Text == "__.__.____ __:__")
        {
            TBetadatetimec.BorderColor = System.Drawing.Color.Red;
            TBetadatetimec.BorderWidth = 1;
            this.ModalPopupExtenderpilotatac.Show();
        }

        else if (IsDate2(TBetadatetimec.Text) != true)
        {
            TBetadatetimec.BorderColor = System.Drawing.Color.Red;
            TBetadatetimec.BorderWidth = 1;
            this.ModalPopupExtenderpilotatac.Show();
        }
        else
        {
            if (Convert.ToDateTime(TBetadatetimec.Text) < DateTime.Now)
            { TBetadatetimec.Text = kayitzamani; }

            if (nedurumdaopr == "4")
            {
                SqlCommand cmdistmakeconup = new SqlCommand("SP_Up_Isliste_makecontact", baglanti);
                cmdistmakeconup.CommandType = CommandType.StoredProcedure;
                cmdistmakeconup.Parameters.AddWithValue("@id", secimedit);
                cmdistmakeconup.Parameters.AddWithValue("@eta", TBetadatetimec.Text);
                cmdistmakeconup.Parameters.AddWithValue("@etb", TBetadatetimec.Text);// ilk pilot istek saaati 
                cmdistmakeconup.Parameters.AddWithValue("@etd", kayitzamani);// pilot isteğinin yapıldığının yazıldığı an
                cmdistmakeconup.Parameters.AddWithValue("@nedurumda", "0");
                cmdistmakeconup.Parameters.AddWithValue("@nedurumdaopr", "0");
                cmdistmakeconup.Parameters.AddWithValue("@kayitzamani", Lblpatailkkayit.Text); // pilot isteğinin yapıldığının yazıldığı an
                cmdistmakeconup.ExecuteNonQuery();
                cmdistmakeconup.Dispose();
            }
            else if (nedurumdaopr == "0")
            {
                SqlCommand cmdistmakeconup = new SqlCommand("SP_Up_Isliste_makecontact", baglanti);
                cmdistmakeconup.CommandType = CommandType.StoredProcedure;
                cmdistmakeconup.Parameters.AddWithValue("@id", secimedit);
                cmdistmakeconup.Parameters.AddWithValue("@eta", TBetadatetimec.Text);
                cmdistmakeconup.Parameters.AddWithValue("@etb", TBetadatetimec.Text);// ilk pilot istek saaati 
                cmdistmakeconup.Parameters.AddWithValue("@etd", kayitzamani);// pilot isteğinin yapıldığının yazıldığı an
                cmdistmakeconup.Parameters.AddWithValue("@nedurumda", "7");
                cmdistmakeconup.Parameters.AddWithValue("@nedurumdaopr", "0");
                cmdistmakeconup.Parameters.AddWithValue("@kayitzamani", Lblpatailkkayit.Text); // pilot isteğinin yapıldığının yazıldığı an
                cmdistmakeconup.ExecuteNonQuery();
                cmdistmakeconup.Dispose();
            }

            else
            {
                string nedurumdabagli = "";
                //
                SqlCommand cmdisupbak = new SqlCommand("SP_Lim_Bagliistal", baglanti);
                cmdisupbak.CommandType = CommandType.StoredProcedure;
                cmdisupbak.Parameters.AddWithValue("@limanadi", kalkislimani);
                cmdisupbak.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
                cmdisupbak.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
                cmdisupbak.ExecuteNonQuery();
                bagliistasyon = cmdisupbak.Parameters["@bagliistasyon"].Value.ToString().Trim();
                cmdisupbak.Dispose();

                if (bagliistasyon == "1") { nedurumdabagli = "7"; }
                else { nedurumdabagli = "6"; }

                SqlCommand cmdistmakeconup = new SqlCommand("SP_Up_Isliste_makecontact", baglanti);
                cmdistmakeconup.CommandType = CommandType.StoredProcedure;
                cmdistmakeconup.Parameters.AddWithValue("@id", secimedit);
                cmdistmakeconup.Parameters.AddWithValue("@eta", TBetadatetimec.Text);
                cmdistmakeconup.Parameters.AddWithValue("@etb", TBetadatetimec.Text);// ilk pilot istek saaati 
                cmdistmakeconup.Parameters.AddWithValue("@etd", kayitzamani);// pilot isteğinin yapıldığının yazıldığı an
                cmdistmakeconup.Parameters.AddWithValue("@nedurumda", nedurumdabagli);
                cmdistmakeconup.Parameters.AddWithValue("@nedurumdaopr", nedurumdaopr);
                cmdistmakeconup.Parameters.AddWithValue("@kayitzamani", Lblpatailkkayit.Text); // pilot isteğinin yapıldığının yazıldığı an
                cmdistmakeconup.ExecuteNonQuery();
                cmdistmakeconup.Dispose();

            }

        }
        DTLoading(baglanti);
        baglanti.Close();



    }

    private void pilotsecpro(string TBgrtal)
    {
        SqlConnection baglanti = AnaKlas.baglan();

        SqlCommand cmdvarbilvarid = new SqlCommand("SP_varbilgivaridoku", baglanti);
        cmdvarbilvarid.CommandType = CommandType.StoredProcedure;
        cmdvarbilvarid.Parameters.Add("@bilgivaridoku", SqlDbType.Char, 1);
        cmdvarbilvarid.Parameters["@bilgivaridoku"].Direction = ParameterDirection.Output;
        cmdvarbilvarid.ExecuteNonQuery();
        string varbilvarid = cmdvarbilvarid.Parameters["@bilgivaridoku"].Value.ToString().Trim();
        cmdvarbilvarid.Dispose();

        SqlCommand cmdvarbilvarno = new SqlCommand("SP_varbilgivarnooku", baglanti);
        cmdvarbilvarno.CommandType = CommandType.StoredProcedure;
        cmdvarbilvarno.Parameters.Add("@bilgivarnooku", SqlDbType.Char, 6);
        cmdvarbilvarno.Parameters["@bilgivarnooku"].Direction = ParameterDirection.Output;
        cmdvarbilvarno.ExecuteNonQuery();
        string varbilvarno = cmdvarbilvarno.Parameters["@bilgivarnooku"].Value.ToString().Trim();
        cmdvarbilvarno.Dispose();


        string respist = ""; // respist kaç ise nompilot ddl o pilotlarla dolar.
        if ((Session["yetki"]).ToString() == "1")
        { respist = "1"; }
        else if ((Session["yetki"]).ToString() == "2")
        { respist = "2"; }

        string kidem = "0";


        if (Convert.ToInt32(TBgrtal) < 20000)
        {

            SqlCommand cmdDDLpilnom = new SqlCommand("SP_DDL_PilotNomine", baglanti);
            cmdDDLpilnom.CommandType = CommandType.StoredProcedure;
            cmdDDLpilnom.Parameters.AddWithValue("@respist", respist);
            cmdDDLpilnom.Parameters.AddWithValue("@varbilvarid", varbilvarid);
            cmdDDLpilnom.Parameters.AddWithValue("@varbilvarno", varbilvarno);
            cmdDDLpilnom.Parameters.AddWithValue("@kidem", kidem);
            SqlDataAdapter adapterpn = new SqlDataAdapter();
            adapterpn.SelectCommand = cmdDDLpilnom;
            DataSet dspn = new DataSet();
            adapterpn.Fill(dspn, "pilotlar");
            DDLnompilot.Items.Clear();
            DDLnompilot.DataValueField = "kapno";
            DDLnompilot.DataTextField = "degismeciadisoyadi";
            DDLnompilot.DataSource = dspn;
            DDLnompilot.DataBind();

            patauyari.Text = "";
            int say = DDLnompilot.Items.Count;
            if (say == 0)
            {
                patauyari.Text = "There is no suitable Pilot !";
            }
            else
            {
                DDLnompilot.Items.Insert(0, new ListItem("Select Pilot?", String.Empty));
                DDLnompilot.SelectedIndex = 0;
            }
        }

        else if (Convert.ToInt32(TBgrtal) >= 20000)
        {
            kidem = "2";


            SqlCommand cmdDDLpilnom = new SqlCommand("SP_DDL_PilotNomine", baglanti);
            cmdDDLpilnom.CommandType = CommandType.StoredProcedure;
            cmdDDLpilnom.Parameters.AddWithValue("@respist", respist);
            cmdDDLpilnom.Parameters.AddWithValue("@varbilvarid", varbilvarid);
            cmdDDLpilnom.Parameters.AddWithValue("@varbilvarno", varbilvarno);
            cmdDDLpilnom.Parameters.AddWithValue("@kidem", kidem);
            SqlDataAdapter adapterpn = new SqlDataAdapter();
            adapterpn.SelectCommand = cmdDDLpilnom;
            DataSet dspn = new DataSet();
            adapterpn.Fill(dspn, "pilotlar");
            DDLnompilot.Items.Clear();
            DDLnompilot.DataValueField = "kapno";
            DDLnompilot.DataTextField = "degismeciadisoyadi";
            DDLnompilot.DataSource = dspn;
            DDLnompilot.DataBind();

            patauyari.Text = "";
            int say = DDLnompilot.Items.Count;
            if (say == 0)
            {
                patauyari.Text = "There is no suitable Pilot !";
            }
            else
            {
                DDLnompilot.Items.Insert(0, new ListItem("Select Pilot?", String.Empty));
                DDLnompilot.SelectedIndex = 0;
            }
        }

        baglanti.Close();
    }
    private void PilotAtaSonIslem()
    {
        if (TBetadatetime.Text == "" || TBetadatetime.Text == null || TBetadatetime.Text == "__.__.____ __:__")
        {
            TBetadatetime.BorderColor = System.Drawing.Color.Red;
            TBetadatetime.BorderWidth = 1;
            this.ModalPopupExtenderpilotata.Show();
        }
        else if (IsDate2(TBetadatetime.Text) != true)
        {
            TBetadatetime.BorderColor = System.Drawing.Color.Red;
            TBetadatetime.BorderWidth = 1;
            this.ModalPopupExtenderpilotata.Show();
        }
        else if (Convert.ToDateTime(TBetadatetime.Text) < DateTime.Now)
        {
            TBetadatetime.BorderColor = System.Drawing.Color.Red;
            TBetadatetime.BorderWidth = 1;
            this.ModalPopupExtenderpilotata.Show();
        }
        else
        {
            SqlConnection baglanti = AnaKlas.baglan();

            string seciliislistid = Buttonpilotata.CommandArgument;

            string imonos = "";
            string gemiadi = "";
            string binisyeri = "";
            string binisrihtim = "";
            string demiryeri = "";
            string bayrak = "";
            string grt = "";
            string tip = "";
            string bowt = "0";
            string strnt = "0";
            string notlar = "";
            string acente = "";

            int binisbolge = 0;
            int inisbolge = 0;

            SqlCommand cmdisokuup = new SqlCommand("SP_Isliste_gemibilgi", baglanti);
            cmdisokuup.CommandType = CommandType.StoredProcedure;
            cmdisokuup.Parameters.AddWithValue("@id", Convert.ToInt32(seciliislistid));
            SqlDataReader dr = cmdisokuup.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    imonos = dr["imono"].ToString();
                    gemiadi = dr["gemiadi"].ToString();
                    binisyeri = dr["kalkislimani"].ToString();
                    binisrihtim = dr["kalkisrihtimi"].ToString();
                    demiryeri = dr["demiryeri"].ToString();
                    bayrak = dr["bayrak"].ToString();
                    grt = dr["grt"].ToString();
                    tip = dr["tip"].ToString();
                    bowt = dr["bowt"].ToString();
                    strnt = dr["strnt"].ToString();
                    acente = dr["acente"].ToString();
                    notlar = dr["notlar"].ToString();
                }
            }
            dr.Close();
            cmdisokuup.Dispose();

            if (demiryeri.ToLower() == "demir-izni yok" && (DDLdesplace.SelectedItem.Text == "Demir-Eskihisar" || DDLdesplace.SelectedItem.Text == "Demir-Hereke" || DDLdesplace.SelectedItem.Text == "Demir-İzmit" || DDLdesplace.SelectedItem.Text == "Demir-Yarımca"))
            {
                DDLdesplace.BorderColor = System.Drawing.Color.Red;
                DDLdesplace.BorderWidth = 1;
                this.ModalPopupExtenderpilotata.Show();
            }

            else
            {

                string durum = "1";
                int imono = Convert.ToInt32(imonos);
                string inisyeri = DDLdesplace.SelectedItem.Text;
                string inisrihtim = DDLdesplaceno.SelectedItem.Text;

                string pataid = DDLnompilot.SelectedItem.Value;


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



                string islemzamani = TarihSaatYaziYapDMYhm(DateTime.Now);

                //yeni dest. a ait ist bulundu
                SqlCommand cmdRespistal = new SqlCommand("SP_RespistFmPort", baglanti);
                cmdRespistal.CommandType = CommandType.StoredProcedure;
                cmdRespistal.Parameters.AddWithValue("@seciliport", inisyeri);
                cmdRespistal.Parameters.Add("@bagliistasyon", SqlDbType.Char, 1);
                cmdRespistal.Parameters["@bagliistasyon"].Direction = ParameterDirection.Output;
                cmdRespistal.ExecuteNonQuery();
                string respistal = cmdRespistal.Parameters["@bagliistasyon"].Value.ToString().Trim();
                cmdRespistal.Dispose();

                if (Session["yetki"].ToString() == "1") // gemi bilgisi live e aktarılıyor
                {
                    string Pob = TBetadatetime.Text;
                    DateTime Pobd = Convert.ToDateTime(Pob);
                    DateTime istasyoncikisdarica = Pobd.AddMinutes(-timeDarToBinis);
                    string istasyoncikis = TarihSaatYaziYapDMYhm(istasyoncikisdarica);
                    DateTime Poffd = Pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure);
                    string Poff = TarihSaatYaziYapDMYhm(Poffd);
                    DateTime istasyongelisd = Poffd;
                    if (respistal == "1") { istasyongelisd = Poffd.AddMinutes(timeInisToDar); }
                    else if (respistal == "2") { istasyongelisd = Poffd.AddMinutes(timeInisToYar); }
                    string istasyongelis = TarihSaatYaziYapDMYhm(istasyongelisd);

                    SqlCommand cmdistipup = new SqlCommand("SP_UpPilotlarIsiptalFmKapno", baglanti);
                    cmdistipup.CommandType = CommandType.StoredProcedure;
                    cmdistipup.Parameters.AddWithValue("@secilikapno", pataid);
                    cmdistipup.Parameters.AddWithValue("@durum", durum);
                    cmdistipup.Parameters.AddWithValue("@imono", imono);
                    cmdistipup.Parameters.AddWithValue("@gemiadi", gemiadi);
                    cmdistipup.Parameters.AddWithValue("@bayrak", bayrak);
                    cmdistipup.Parameters.AddWithValue("@grt", grt);
                    cmdistipup.Parameters.AddWithValue("@tip", tip);
                    cmdistipup.Parameters.AddWithValue("@binisyeri", binisyeri);
                    cmdistipup.Parameters.AddWithValue("@binisrihtim", binisrihtim);
                    cmdistipup.Parameters.AddWithValue("@inisyeri", inisyeri);
                    cmdistipup.Parameters.AddWithValue("@inisrihtim", inisrihtim);
                    cmdistipup.Parameters.AddWithValue("@istasyoncikis", istasyoncikis);
                    cmdistipup.Parameters.AddWithValue("@pob", Pob);
                    cmdistipup.Parameters.AddWithValue("@poff", Poff);
                    cmdistipup.Parameters.AddWithValue("@istasyongelis", istasyongelis);
                    cmdistipup.Parameters.AddWithValue("@gemiatamazamani", islemzamani);
                    cmdistipup.Parameters.AddWithValue("@jnot", notlar);
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
                    cmdistipup.Parameters.AddWithValue("@acente", acente);
                    cmdistipup.Parameters.AddWithValue("@oper", "");
                    cmdistipup.ExecuteNonQuery();
                    cmdistipup.Dispose();

                    //işliste update ediliyor
                    SqlCommand cmdisnedup = new SqlCommand("SP_Up_Isliste_NedurumdaFmimo", baglanti);
                    cmdisnedup.CommandType = CommandType.StoredProcedure;
                    cmdisnedup.Parameters.AddWithValue("@imono", imono);
                    cmdisnedup.Parameters.AddWithValue("@nedurumda", "8");// yeni işleme alınmış pilot elinde 9 dışarı cıkmış
                    cmdisnedup.ExecuteNonQuery();
                    cmdisnedup.Dispose();

                    

                    //bow işlemi
                    if (bowt != "0" || strnt != "0")
                    {
                        SqlCommand cmdistbowok = new SqlCommand("SP_Up_Isliste_bowok", baglanti);
                        cmdistbowok.CommandType = CommandType.StoredProcedure;
                        cmdistbowok.Parameters.AddWithValue("@imono", imono);
                        cmdistbowok.Parameters.AddWithValue("@bowok", DDLbowok.SelectedItem.Text);
                        cmdistbowok.ExecuteNonQuery();
                        cmdistbowok.Dispose();
                    }


                    //pilota mail sms göndermek  // postasmsal 1 ise eposta, 2 ise sms, 3 ise ikisinide almak istiyordur.

                    string postasmsal = "";
                    string eposta = "";

                    SqlCommand cmdpilotokufull = new SqlCommand("SP_PilotOkuFull_fmKapno", baglanti);
                    cmdpilotokufull.CommandType = CommandType.StoredProcedure;
                    cmdpilotokufull.Parameters.AddWithValue("@secilikapno", pataid);
                    SqlDataReader readpilotfull = cmdpilotokufull.ExecuteReader();
                    if (readpilotfull.HasRows)
                    {
                        while (readpilotfull.Read())
                        {
                            postasmsal = readpilotfull["postasmsal"].ToString();
                            eposta = readpilotfull["eposta"].ToString();
                        }
                    }
                    readpilotfull.Close();
                    cmdpilotokufull.Dispose();

                    if (postasmsal == "1")
                    {
                        string Subject = "NewJOB|" + istasyoncikis.Substring(11, 5) + "/" + gemiadi + "/grt: " + grt;
                        string Body = "";
                        Body = Body + "<table  style='border:1px solid black; background-color:lightyellow; font-family:Arial; font-size:10px;' ><tr><td style='border:1px;' colspan=2> <font color='red'> SHIP INFO </font ></td></tr>";
                        Body = Body + "<tr><td >imo No</td><td>" + imono + "</td></tr>";
                        Body = Body + "<tr><td>Gemi adi</td><td>" + gemiadi + "</td></tr>";
                        Body = Body + "<tr><td>Bayrak</td><td>" + bayrak + "</td></tr>";
                        Body = Body + "<tr><td>Grt</td><td>" + grt + "</td></tr>";
                        Body = Body + "<tr><td>Tipi</td><td>" + tip + "</td></tr>";
                        Body = Body + "<tr><td>Kalkış</td><td>" + binisyeri + " / " + binisrihtim + "</td></tr>";
                        Body = Body + "<tr><td>Varış</td><td>" + inisyeri + " / " + inisrihtim + "</td></tr>";
                        Body = Body + "<tr><td>Acente</td><td>" + acente + "</td></tr>";
                        Body = Body + "<tr><td>Not</td><td>" + notlar + "</td></tr></table>";
                        Body = Body + "</br></br>Send by PilotMonitoring System " + DateTime.Now.ToString();
                        Mailtoone(eposta, Subject, Body, imono);
                    }
                    DTLoading(baglanti);
                }


                else if (Session["yetki"].ToString() == "2") // gemi bilgisi live e aktarılıyor
                {
                    string Pob = TBetadatetime.Text;
                    DateTime Pobd = Convert.ToDateTime(Pob);
                    DateTime istasyoncikisyarimca = Pobd.AddMinutes(-timeYarToBinis);
                    string istasyoncikis = TarihSaatYaziYapDMYhm(istasyoncikisyarimca);
                    DateTime Poffd = Pobd.AddMinutes(timeBinisToInis).AddMinutes(timeYanasSure).AddMinutes(timeKalkSure);
                    string Poff = TarihSaatYaziYapDMYhm(Poffd);
                    DateTime istasyongelisd = Poffd;
                    if (respistal == "1") { istasyongelisd = Poffd.AddMinutes(timeInisToDar); }
                    else if (respistal == "2") { istasyongelisd = Poffd.AddMinutes(timeInisToYar); }
                    string istasyongelis = TarihSaatYaziYapDMYhm(istasyongelisd);

                    SqlCommand cmdistipup = new SqlCommand("SP_UpPilotlarIsiptalFmKapno", baglanti);
                    cmdistipup.CommandType = CommandType.StoredProcedure;
                    cmdistipup.Parameters.AddWithValue("@secilikapno", pataid);
                    cmdistipup.Parameters.AddWithValue("@durum", durum);
                    cmdistipup.Parameters.AddWithValue("@imono", imono);
                    cmdistipup.Parameters.AddWithValue("@gemiadi", gemiadi);
                    cmdistipup.Parameters.AddWithValue("@bayrak", bayrak);
                    cmdistipup.Parameters.AddWithValue("@grt", grt);
                    cmdistipup.Parameters.AddWithValue("@tip", tip);
                    cmdistipup.Parameters.AddWithValue("@binisyeri", binisyeri);
                    cmdistipup.Parameters.AddWithValue("@binisrihtim", binisrihtim);
                    cmdistipup.Parameters.AddWithValue("@inisyeri", inisyeri);
                    cmdistipup.Parameters.AddWithValue("@inisrihtim", inisrihtim);
                    cmdistipup.Parameters.AddWithValue("@istasyoncikis", istasyoncikis);
                    cmdistipup.Parameters.AddWithValue("@pob", Pob);
                    cmdistipup.Parameters.AddWithValue("@poff", Poff);
                    cmdistipup.Parameters.AddWithValue("@istasyongelis", istasyongelis);
                    cmdistipup.Parameters.AddWithValue("@gemiatamazamani", islemzamani);
                    cmdistipup.Parameters.AddWithValue("@jnot", notlar);
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
                    cmdistipup.Parameters.AddWithValue("@acente", acente);
                    cmdistipup.Parameters.AddWithValue("@oper", "");
                    cmdistipup.ExecuteNonQuery();
                    cmdistipup.Dispose();

                    //işliste update ediliyor
                    SqlCommand cmdisnedup = new SqlCommand("SP_Up_Isliste_NedurumdaFmimo", baglanti);
                    cmdisnedup.CommandType = CommandType.StoredProcedure;
                    cmdisnedup.Parameters.AddWithValue("@imono", imono);
                    cmdisnedup.Parameters.AddWithValue("@nedurumda", "8");// yeni işleme alınmış pilot elinde
                    cmdisnedup.ExecuteNonQuery();
                    cmdisnedup.Dispose();

                  

                    //bow işlemi
                    if (bowt != "0" || strnt != "0")
                    {
                        SqlCommand cmdistbowok = new SqlCommand("SP_Up_Isliste_bowok", baglanti);
                        cmdistbowok.CommandType = CommandType.StoredProcedure;
                        cmdistbowok.Parameters.AddWithValue("@imono", imono);
                        cmdistbowok.Parameters.AddWithValue("@bowok", DDLbowok.SelectedItem.Text);
                        cmdistbowok.ExecuteNonQuery();
                        cmdistbowok.Dispose();
                    }


                    //pilota mail sms göndermek  // postasmsal 1 ise eposta, 2 ise sms, 3 ise ikisinide almak istiyordur.

                    string postasmsal = "";
                    string eposta = "";

                    SqlCommand cmdpilotokufull = new SqlCommand("SP_PilotOkuFull_fmKapno", baglanti);
                    cmdpilotokufull.CommandType = CommandType.StoredProcedure;
                    cmdpilotokufull.Parameters.AddWithValue("@secilikapno", pataid);
                    SqlDataReader readpilotfull = cmdpilotokufull.ExecuteReader();
                    if (readpilotfull.HasRows)
                    {
                        while (readpilotfull.Read())
                        {
                            postasmsal = readpilotfull["postasmsal"].ToString();
                            eposta = readpilotfull["eposta"].ToString();
                        }
                    }
                    readpilotfull.Close();
                    cmdpilotokufull.Dispose();

                    if (postasmsal == "1")
                    {
                        string Subject = "NewJOB|" + istasyoncikis.Substring(11, 5) + "/" + gemiadi + "/grt: " + grt;
                        string Body = "";
                        Body = Body + "<table  style='border:1px solid black; background-color:lightyellow; font-family:Arial; font-size:10px;' ><tr><td style='border:1px;' colspan=2> <font color='red'> SHIP INFO </font ></td></tr>";
                        Body = Body + "<tr><td >imo No</td><td>" + imono + "</td></tr>";
                        Body = Body + "<tr><td>Gemi adi</td><td>" + gemiadi + "</td></tr>";
                        Body = Body + "<tr><td>Bayrak</td><td>" + bayrak + "</td></tr>";
                        Body = Body + "<tr><td>Grt</td><td>" + grt + "</td></tr>";
                        Body = Body + "<tr><td>Tipi</td><td>" + tip + "</td></tr>";
                        Body = Body + "<tr><td>Kalkış</td><td>" + binisyeri + " / " + binisrihtim + "</td></tr>";
                        Body = Body + "<tr><td>Varış</td><td>" + inisyeri + " / " + inisrihtim + "</td></tr>";
                        Body = Body + "<tr><td>Acente</td><td>" + acente + "</td></tr>";
                        Body = Body + "<tr><td>Not</td><td>" + notlar + "</td></tr></table>";
                        Body = Body + "</br></br>Send by PilotMonitoring System " + DateTime.Now.ToString();
                        Mailtoone(eposta, Subject, Body, imono);
                    }
                    DTLoading(baglanti);
                }


                baglanti.Close();
            }
        }
    }

    protected void LBonlineoff_Click(object sender, EventArgs e)
    {
        if (Session["kapno"] == null)
        {
            Response.Redirect("https://www.monitoringpilot.com");
        }
        else if (Session["kapno"].ToString() == "")
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
        if (Session["yetki"].ToString() == "9")
        {
            Response.Redirect("yonet/yonetim.aspx");
        }
    }


    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        DTLoading(baglanti);
        baglanti.Close();
        baglanti.Dispose();
    }

    private bool Mailtoone(string toadres, string Subject, string Body, int imono)
    {
        try
        {
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand lybbak = new SqlCommand("SP_lybbakimo", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@imono", Convert.ToInt32(imono));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            string Resim = lybbak.Parameters["@lybyol"].Value.ToString();
            lybbak.Dispose();
            baglanti.Close();
            baglanti.Dispose();

            AlternateView plainView = AlternateView.CreateAlternateViewFromString("alternate", null, "text/plain");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<img src=cid:companylogo></br><p>" + Body + "</p>", null, "text/html");
            LinkedResource logo = new LinkedResource(Server.MapPath(Resim));
            logo.ContentId = "companylogo";

            MailMessage mesaj = new MailMessage();

            mesaj.From = new MailAddress("info@monitoringpilot.com");
            mesaj.To.Add(new MailAddress(toadres));
            mesaj.Subject = Subject;// Mailinizin Konusunu, başlığını giriyorsunuz			
            //mesaj.Body = Body;  // Göndereceğiniz mailin içeriğini girin, IsBodyHtml = true yaptıysanız html etiketleri ok
            mesaj.IsBodyHtml = true;// Mail içeriğinde html kullanılacaksa true, mail içereğinde htmli engellemek için false giriniz.
            mesaj.BodyEncoding = System.Text.Encoding.UTF8;//bu da olabilir : mesaj.BodyEncoding = UTF8Encoding.UTF8;
            mesaj.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            htmlView.LinkedResources.Add(logo);
            mesaj.AlternateViews.Add(plainView);
            mesaj.AlternateViews.Add(htmlView);


            //if (Resim != null && Resim != "")
            //{
            //    Attachment dosya = new Attachment(Server.MapPath(Resim));
                //mesaj.Attachments.Add(dosya);
            //}

            string host = "smtp.gmail.com";
            string smtpUser = "info@monitoringpilot.com";
            string smtpPassword = "Dekas1996!!";

            SmtpClient smtp = new SmtpClient(host, 25);// Genelde mail.domain.com şeklinde olan smtp mail sunucu adresinizi girmelisiniz.
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


        //______________ resmin html gibi direk göstermek
        //AlternateView plainView = AlternateView.CreateAlternateViewFromString("alternate", null, "text/plain");
        //AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<img src=cid:companylogo><p>Mail içeriği buraya yazılabilir.</p>", null, "text/html");
        //LinkedResource logo = new LinkedResource("mail gövdesine eklenecek resim yolu");

        //logo.ContentId ="companylogo";

        //htmlView.LinkedResources.Add(logo);
        //mesaj.AlternateViews.Add(plainView);
        //mesaj.AlternateViews.Add(htmlView);
        //__________________




        //    forumlardan bilgi:    sp.Nette mail gönderen kod bloğumda hata alıyorum.Kodlarım aşağıdadır.

        //SmtpClient istemci = new SmtpClient("mail.domain.com", 587);

        //        NetworkCredential bascCredential = new NetworkCredential("info@domain.com", "password");
        //        istemci.UseDefaultCredentials = true;
        //        istemci.Credentials = bascCredential;
        //        istemci.EnableSsl = false;


        //        MailMessage mesaj = new MailMessage();
        //        mesaj.From = new MailAddress(email.Text);
        //        mesaj.To.Add(new MailAddress("info@domain.com"));
        //        mesaj.Subject = "Mesaj Bildirimi";
        //        mesaj.IsBodyHtml = true;
        //        mesaj.Body = String.Format("<table><tr><td>" + phone.Text + "</td><tr><tr><td>" + comments.Text + "</td></tr></table>");
        //        mesaj.Priority = MailPriority.Normal;


        //        if (Resim.HasFile)
        //        {
        //            Resim.SaveAs(Server.MapPath("images/mail/") + Resim.FileName);

        //            Attachment dosya = new Attachment(Server.MapPath("images/mail/") + Resim.FileName);
        //            mesaj.Attachments.Add(dosya);

        //        }
        //        istemci.Send(mesaj);
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(System.Web.UI.Page), "MyJSFunction", "tamam();", true);

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

    private string Temizle(string Metin)
    {
        string deger = Metin;
        deger = deger.Replace("'", "");
        deger = deger.Replace("<", "");
        deger = deger.Replace(">", "");
        deger = deger.Replace("&", "");
        deger = deger.Replace("[", "");
        deger = deger.Replace("]", "");
        deger = deger.Replace(";", "");
        deger = deger.Replace("?", "");
        deger = deger.Replace("%", "");
        deger = deger.Replace("!", "");
        return deger;
    }


    protected void CBdeletepob_CheckedChanged(object sender, EventArgs e)
    {
        if (CBdeletepob.Checked == true)
        {
            patauyari.Visible = true;
            patauyari.Text = "POB Contact Canceled.";
            Buttonpilotata.Enabled = true;
            Buttonpilotata.Text = "Yes";
            this.ModalPopupExtenderpilotata.Show();
        }
        else
        {
            patauyari.Visible = false;
            patauyari.Text = "";
            Buttonpilotata.Text = "Nominate";

            if (DDLdesplaceno.SelectedItem.Text == "Select" || DDLnompilot.SelectedItem.Text == "Select Pilot?")
            {
                DDLnompilot.Items.Clear();
                DDLnompilot.Enabled = false;
                Buttonpilotata.Enabled = false;
            }

            this.ModalPopupExtenderpilotata.Show();

        }
    }

    protected void DDLJEpratika_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLJEpratika.SelectedItem.Text == "Yes")
        {
            TBJEpratikano.Visible = true;
            TBJEpratikano.Text = "";
        }
        else if (DDLJEpratika.SelectedItem.Text == "No")
        {
            TBJEpratikano.Visible = false;
            TBJEpratikano.Text = "";
        }
        this.ModalPopupExtenderjobedit.Show();
    }


    protected void Lbl20_Click(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        Button LBiskopya = (Button)sender;
        String seciliislistids = (LBiskopya.CommandArgument).ToString();
        int islisteid = Convert.ToInt32(seciliislistids);
        String seciligemiadi = (LBiskopya.CommandName).ToString();
        shipnamelcb.Text = seciligemiadi.ToUpper();
        shipnamelcb.ForeColor = System.Drawing.Color.Red;

        string lcbno = "";
        string lcbdest = "";
        string lcbdate = "";
        TBlcbnoilk.Text = "";

        TBlcbdate.BorderColor = System.Drawing.Color.Gray;
        TBlcbdate.BorderWidth = 1;
        TBlcbdest.BorderColor = System.Drawing.Color.Gray;
        TBlcbdest.BorderWidth = 1;
        TBlcbno.BorderColor = System.Drawing.Color.Gray;
        TBlcbno.BorderWidth = 1;

        SqlCommand cmdlcboku = new SqlCommand("SP_Isliste_lcboku", baglanti);
        cmdlcboku.CommandType = CommandType.StoredProcedure;
        cmdlcboku.Parameters.AddWithValue("@id", islisteid);
        SqlDataReader cmdlcbokureader = cmdlcboku.ExecuteReader();
        if (cmdlcbokureader.HasRows)
        {
            while (cmdlcbokureader.Read())
            {
                lcbno = cmdlcbokureader["lcbno"].ToString();
                lcbdest = cmdlcbokureader["lcbdest"].ToString();
                lcbdate = cmdlcbokureader["lcbdate"].ToString();
            }
        }
        cmdlcbokureader.Close();
        cmdlcboku.Dispose();

        TBlcbno.Text = lcbno;
        TBlcbdest.Text = lcbdest;
        TBlcbdate.Text = lcbdate;
        TBlcbnoilk.Text = lcbno;

        Bacceptedlcb.CommandArgument = seciliislistids;


        DTLoading(baglanti);
        baglanti.Close();

        this.ModalPopuplcbnot.Show();


    }
    protected void Bacceptedlcb_Click(object sender, EventArgs e)
    {

        TBlcbdate.BorderColor = System.Drawing.Color.Gray;
        TBlcbdate.BorderWidth = 1;
        TBlcbdest.BorderColor = System.Drawing.Color.Gray;
        TBlcbdest.BorderWidth = 1;
        TBlcbno.BorderColor = System.Drawing.Color.Gray;
        TBlcbno.BorderWidth = 1;

        if (TBlcbno.Text.Trim() == "" && TBlcbdest.Text.Trim() == "" && (TBlcbdate.Text.Trim() == "" || TBlcbdate.Text.Trim() == "__.__.____ __:__"))
        {
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand cmdisetbup = new SqlCommand("SP_Up_Isliste_lcb", baglanti);
            cmdisetbup.CommandType = CommandType.StoredProcedure;
            cmdisetbup.Parameters.AddWithValue("@id", Bacceptedlcb.CommandArgument);
            cmdisetbup.Parameters.AddWithValue("@lcbno", "");
            cmdisetbup.Parameters.AddWithValue("@lcbdest", "");
            cmdisetbup.Parameters.AddWithValue("@lcbdate", "");
            cmdisetbup.ExecuteNonQuery();
            cmdisetbup.Dispose();

            if (TBlcbnoilk.Text != "" && TBlcbno.Text == "")
            { 
            //log_newjob
            SqlCommand cmdisimbul = new SqlCommand("Select kapadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
            string isimbul = cmdisimbul.ExecuteScalar().ToString();
            cmdisimbul.Dispose();
            SqlCommand cmdsoyisimbul = new SqlCommand("Select kapsoyadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
            string soyisimbul = cmdsoyisimbul.ExecuteScalar().ToString();
            cmdsoyisimbul.Dispose();

            SqlCommand cmdlognj = new SqlCommand("SP_log_newjob_ekle", baglanti);
            cmdlognj.CommandType = CommandType.StoredProcedure;
            cmdlognj.Parameters.AddWithValue("imono", Bacceptedlcb.CommandArgument);
            cmdlognj.Parameters.AddWithValue("gemiadi", shipnamelcb.Text);
            cmdlognj.Parameters.AddWithValue("demiryeri", "");
            cmdlognj.Parameters.AddWithValue("kalkislimani", "");
            cmdlognj.Parameters.AddWithValue("kalkisrihtimi", "");
            cmdlognj.Parameters.AddWithValue("yanasmalimani", "");
            cmdlognj.Parameters.AddWithValue("yanasmarihtimi", "");
            cmdlognj.Parameters.AddWithValue("eta", "");
            cmdlognj.Parameters.AddWithValue("bowt", "");
            cmdlognj.Parameters.AddWithValue("strnt", "");
            cmdlognj.Parameters.AddWithValue("bayrak", "");
            cmdlognj.Parameters.AddWithValue("grt", "");
            cmdlognj.Parameters.AddWithValue("tip", "");
            cmdlognj.Parameters.AddWithValue("loa", "");
            cmdlognj.Parameters.AddWithValue("draft", "");
            cmdlognj.Parameters.AddWithValue("bilgi", "");
            cmdlognj.Parameters.AddWithValue("tehlikeliyuk", "");
            cmdlognj.Parameters.AddWithValue("acente", "");
            cmdlognj.Parameters.AddWithValue("fatura", "");
            cmdlognj.Parameters.AddWithValue("notlar", "");
            cmdlognj.Parameters.AddWithValue("pratika", "");
            cmdlognj.Parameters.AddWithValue("pratikano", "");
            cmdlognj.Parameters.AddWithValue("lcbno", "");
            cmdlognj.Parameters.AddWithValue("lcbdest", "silindi");
            cmdlognj.Parameters.AddWithValue("lcbdate", "");
            cmdlognj.Parameters.AddWithValue("talepno", "");
            cmdlognj.Parameters.AddWithValue("nedurumda", "");
            cmdlognj.Parameters.AddWithValue("nedurumdaopr", "");
            cmdlognj.Parameters.AddWithValue("gizlegoster", "");
            cmdlognj.Parameters.AddWithValue("kayitzamani", TarihSaatYaziYapDMYhm(DateTime.Now));
            cmdlognj.Parameters.AddWithValue("kaydeden", isimbul + " " + soyisimbul);
            cmdlognj.ExecuteNonQuery();
            cmdlognj.Dispose();
            }
            DTLoading(baglanti);
            baglanti.Close();
        }
      


        else
        {
            if (TBlcbno.Text == "")
            {
                TBlcbno.BorderColor = System.Drawing.Color.Red;
                TBlcbno.BorderWidth = 1;
                this.ModalPopuplcbnot.Show();
            }
            if (TBlcbdest.Text == "")
            {
                TBlcbdest.BorderColor = System.Drawing.Color.Red;
                TBlcbdest.BorderWidth = 1;
                this.ModalPopuplcbnot.Show();
            }

            if (TBlcbdate.Text == "" || TBlcbdate.Text == null || TBlcbdate.Text == "__.__.____ __:__")
            {
                TBlcbdate.BorderColor = System.Drawing.Color.Red;
                TBlcbdate.BorderWidth = 1;
                this.ModalPopuplcbnot.Show();
            }
            else if (IsDate2(TBlcbdate.Text) != true)
            {
                TBlcbdate.BorderColor = System.Drawing.Color.Red;
                TBlcbdate.BorderWidth = 1;
                this.ModalPopuplcbnot.Show();
            }
            else if (Convert.ToDateTime(TBlcbdate.Text) < DateTime.Now.AddHours(-6))
            {
                TBlcbdate.BorderColor = System.Drawing.Color.Red;
                TBlcbdate.BorderWidth = 1;
                this.ModalPopuplcbnot.Show();
            }
            else if (TBlcbno.Text.Trim() != "" && TBlcbdest.Text.Trim() != "" && TBlcbdate.Text.Trim() != "")
            {
                SqlConnection baglanti = AnaKlas.baglan();
                SqlCommand cmdisetbup = new SqlCommand("SP_Up_Isliste_lcb", baglanti);
                cmdisetbup.CommandType = CommandType.StoredProcedure;
                cmdisetbup.Parameters.AddWithValue("@id", Bacceptedlcb.CommandArgument);
                cmdisetbup.Parameters.AddWithValue("@lcbno", TBlcbno.Text);
                cmdisetbup.Parameters.AddWithValue("@lcbdest", TBlcbdest.Text);
                cmdisetbup.Parameters.AddWithValue("@lcbdate", TBlcbdate.Text);
                cmdisetbup.ExecuteNonQuery();
                cmdisetbup.Dispose();



                //log_newjob
                SqlCommand cmdisimbul = new SqlCommand("Select kapadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
                string isimbul = cmdisimbul.ExecuteScalar().ToString();
                cmdisimbul.Dispose();
                SqlCommand cmdsoyisimbul = new SqlCommand("Select kapsoyadi  from pilotlar where kapno =" + Session["kapno"], baglanti);
                string soyisimbul = cmdsoyisimbul.ExecuteScalar().ToString();
                cmdsoyisimbul.Dispose();

                SqlCommand cmdlognj = new SqlCommand("SP_log_newjob_ekle", baglanti);
                cmdlognj.CommandType = CommandType.StoredProcedure;
                cmdlognj.Parameters.AddWithValue("imono", Bacceptedlcb.CommandArgument);
                cmdlognj.Parameters.AddWithValue("gemiadi", shipnamelcb.Text);
                cmdlognj.Parameters.AddWithValue("demiryeri", "");
                cmdlognj.Parameters.AddWithValue("kalkislimani", "");
                cmdlognj.Parameters.AddWithValue("kalkisrihtimi", "");
                cmdlognj.Parameters.AddWithValue("yanasmalimani", "");
                cmdlognj.Parameters.AddWithValue("yanasmarihtimi", "");
                cmdlognj.Parameters.AddWithValue("eta", "");
                cmdlognj.Parameters.AddWithValue("bowt", "");
                cmdlognj.Parameters.AddWithValue("strnt", "");
                cmdlognj.Parameters.AddWithValue("bayrak", "");
                cmdlognj.Parameters.AddWithValue("grt", "");
                cmdlognj.Parameters.AddWithValue("tip", "");
                cmdlognj.Parameters.AddWithValue("loa", "");
                cmdlognj.Parameters.AddWithValue("draft", "");
                cmdlognj.Parameters.AddWithValue("bilgi", "");
                cmdlognj.Parameters.AddWithValue("tehlikeliyuk", "");
                cmdlognj.Parameters.AddWithValue("acente", "");
                cmdlognj.Parameters.AddWithValue("fatura", "");
                cmdlognj.Parameters.AddWithValue("notlar", "");
                cmdlognj.Parameters.AddWithValue("pratika", "");
                cmdlognj.Parameters.AddWithValue("pratikano", "");
                cmdlognj.Parameters.AddWithValue("lcbno", TBlcbno.Text);
                cmdlognj.Parameters.AddWithValue("lcbdest", TBlcbdest.Text);
                cmdlognj.Parameters.AddWithValue("lcbdate", TBlcbdate.Text);
                cmdlognj.Parameters.AddWithValue("talepno", "");
                cmdlognj.Parameters.AddWithValue("nedurumda", "");
                cmdlognj.Parameters.AddWithValue("nedurumdaopr", "");
                cmdlognj.Parameters.AddWithValue("gizlegoster", "");
                cmdlognj.Parameters.AddWithValue("kayitzamani", TarihSaatYaziYapDMYhm(DateTime.Now));
                cmdlognj.Parameters.AddWithValue("kaydeden", isimbul + " " + soyisimbul);
                cmdlognj.ExecuteNonQuery();
                cmdlognj.Dispose();


                DTLoading(baglanti);
                baglanti.Close();
            }
            
        }
    }


}