using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.Globalization;
using System.IO;
using System.Drawing;

public partial class joblist : System.Web.UI.Page
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
        else if (Session["kapno"].ToString() != "18" && Session["kapno"].ToString() != "70" && Session["kapno"].ToString() != "107"  && Session["kapno"].ToString() != "15"  && Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2" && Session["yetki"].ToString() != "9" && Session["kapno"].ToString() != "92" && Session["kapno"].ToString() != "97" && Session["kapno"].ToString() != "98") 
            {
                Response.Redirect("https://www.monitoringpilot.com");
            }

        else 
        {
            if (!Page.IsPostBack)
            {
                if (Session["yetki"].ToString() == "9") { LBonline.Enabled = true; }
                else { LBonline.Enabled = false; }

                if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2" && Session["kapno"].ToString() != "28")
                {
                    ButtonANJshowpopup.Visible = false;
                    Bdaricaships.Visible = false;
                    Byarimcaships.Visible = false;
                    //Byalovaships.Visible = false;
                    lybload.Visible = false;
                    orload.Visible = false;
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

                LBonline.Text = kapadisoyadi;

                LiteralYaz(baglanti);

                LblVarid.Text = "Watch:" + varbilvarid.Text;
                LblVarno.Text = " / WatchNumber:" + ' ' + varbilvarno.Text;
                LblVarbasla.Text = " / Entry:" + ' ' + varbaslar.Text;
                LblVarbit.Text = " / Exit:" + ' ' + varbiter.Text;
                LblReeltime.Text = " / Passed Time: " + Math.Round((DateTime.Now - Convert.ToDateTime(varbaslar.Text)).TotalHours, 2).ToString();


                if (Session["yetki"].ToString() == "0")
                {
                    mainmanu2.Visible = false;
                    mainmanu3.Visible = false;
                    mainmanu4.Visible = false;
                    mainmanu6.Visible = false;
                }

                if (Session["kapno"].ToString() == "18" || Session["kapno"].ToString() == "70" || Session["kapno"].ToString() == "107" || Session["kapno"].ToString() == "15" || Session["kapno"].ToString() == "92" || Session["kapno"].ToString() == "97" || Session["kapno"].ToString() == "98")
                {
                    mainmanu2.Visible = true;
                    mainmanu3.Visible = true;
                    mainmanu6.Visible = true;
                }
                else if (Session["kapno"].ToString() == "96")
                {
                    mainmanu2.Visible = true;
                }


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

        NewShipListc(baglanti);
        NewShipListvipC(baglanti);
        NewShipListManeuvering(baglanti);
        AnchoredShipList(baglanti);
        VIPort(baglanti);
        //VIPortTers(baglanti);
        //VIPortyalo(baglanti);
        VDLyaranc(baglanti);
        VDLyarport(baglanti);
        VDLher(baglanti);
        VIPortKosb(baglanti);
    }

    private void NewShipListc(SqlConnection baglanti)
    {

        using (PilotdbEntities2 isliste = new PilotdbEntities2())
        {
            DLNewShipListc.DataSource = NewShipListcBind(baglanti);
            DLNewShipListc.DataBind();
        }
    }
    public List<isliste> NewShipListcBind(SqlConnection baglanti)
    {

        SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL", baglanti);
        cmdisokunsl.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmdisokunsl.ExecuteReader();

        List<isliste> NSL = new List<isliste>();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                NSL.Add(new isliste()
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
                    kaydeden = dr["kaydeden"].ToString()


                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();


        return NSL;

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

        SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_vipC", baglanti);
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
                    talepno = dr["talepno"].ToString(),
                    pratikano = dr["pratikano"].ToString(),
                    kaydeden = dr["kaydeden"].ToString()


                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();


        return NSLvipC;

    }


    private void NewShipListManeuvering(SqlConnection baglanti)
    {

        using (PilotdbEntities2 isliste = new PilotdbEntities2())
        {
            DLNewShipManeuvering.DataSource = NewShipListManeuveringBind(baglanti);
            DLNewShipManeuvering.DataBind();
        }
    }
    public List<isliste> NewShipListManeuveringBind(SqlConnection baglanti)
    {

        SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_Canli_Maneuvering", baglanti);
        cmdisokunsl.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmdisokunsl.ExecuteReader();

        List<isliste> NSLvipCM = new List<isliste>();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                NSLvipCM.Add(new isliste()
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
                    kaydeden = dr["kaydeden"].ToString()


                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();


        return NSLvipCM;

    }



    private void AnchoredShipList(SqlConnection baglanti)
    {
        using (PilotdbEntities2 isliste = new PilotdbEntities2())
        {
            DLAnchoredShipList.DataSource = AnchoredShipListBind(baglanti);
            DLAnchoredShipList.DataBind();
        }
    }
    public List<isliste> AnchoredShipListBind(SqlConnection baglanti)
    {


        SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_ASL", baglanti);
        cmdisokunsl.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmdisokunsl.ExecuteReader();

        List<isliste> NSLasl = new List<isliste>();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                NSLasl.Add(new isliste()
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
                    kaydeden = dr["kaydeden"].ToString()


                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();


        return NSLasl;

    }

    private void VIPort(SqlConnection baglanti)
    {
        using (PilotdbEntities2 isliste = new PilotdbEntities2())
        {

            DLVIPort.DataSource = VIPortBind(baglanti);
            DLVIPort.DataBind();
        }
    }
    public List<isliste> VIPortBind(SqlConnection baglanti)
    {

        SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_VIP", baglanti);
        cmdisokunsl.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = cmdisokunsl.ExecuteReader();

        List<isliste> NSLvip = new List<isliste>();

        if (dr.HasRows)
        {
            while (dr.Read())
            {
                NSLvip.Add(new isliste()
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
                    kaydeden = dr["kaydeden"].ToString()


                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();


        return NSLvip;

    }

    //private void VIPortTers(SqlConnection baglanti)
    //{
    //    using (PilotdbEntities2 isliste = new PilotdbEntities2())
    //    {
    //        DLVIPortTers.DataSource = VIPortTersBind(baglanti);
    //        DLVIPortTers.DataBind();
    //    }
    //}
    //public List<isliste> VIPortTersBind(SqlConnection baglanti)
    //{


    //    SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_vipT", baglanti);
    //    cmdisokunsl.CommandType = CommandType.StoredProcedure;
    //    SqlDataReader dr = cmdisokunsl.ExecuteReader();

    //    List<isliste> NSLvipT = new List<isliste>();

    //    if (dr.HasRows)
    //    {
    //        while (dr.Read())
    //        {
    //            NSLvipT.Add(new isliste()
    //            {
    //                id = Convert.ToInt32(dr["id"].ToString()),
    //                imono = Convert.ToInt32(dr["imono"].ToString()),
    //                gemiadi = dr["gemiadi"].ToString(),
    //                kalkislimani = dr["kalkislimani"].ToString(),
    //                kalkisrihtimi = dr["kalkisrihtimi"].ToString(),
    //                yanasmalimani = dr["yanasmalimani"].ToString(),
    //                yanasmarihtimi = dr["yanasmarihtimi"].ToString(),
    //                demiryeri = dr["demiryeri"].ToString(),
    //                bayrak = dr["bayrak"].ToString(),
    //                tip = dr["tip"].ToString(),
    //                grt = dr["grt"].ToString(),
    //                acente = dr["acente"].ToString(),
    //                fatura = dr["fatura"].ToString(),
    //                bowt = dr["bowt"].ToString(),
    //                strnt = dr["strnt"].ToString(),
    //                loa = dr["loa"].ToString(),
    //                tehlikeliyuk = dr["tehlikeliyuk"].ToString(),
    //                draft = dr["draft"].ToString(),
    //                bilgi = dr["bilgi"].ToString(),
    //                eta = dr["eta"].ToString(),
    //                notlar = dr["notlar"].ToString(),
    //                talepno = dr["talepno"].ToString(),
    //pratikano = dr["pratikano"].ToString(),
    //                kaydeden = dr["kaydeden"].ToString()


    //            });

    //        }
    //    }
    //    dr.Close();
    //    cmdisokunsl.Dispose();


    //    return NSLvipT;

    //}

    //private void VIPortyalo(SqlConnection baglanti)
    //{
    //    using (PilotdbEntities2 isliste = new PilotdbEntities2())
    //    {
    //        DLVIPortyalo.DataSource = VIPortyaloBind(baglanti);
    //        DLVIPortyalo.DataBind();
    //    }
    //}
    //public List<isliste> VIPortyaloBind(SqlConnection baglanti)
    //{


    //    SqlCommand cmdisokunsl = new SqlCommand("SP_Isliste_NSL_YalPort", baglanti);
    //    cmdisokunsl.CommandType = CommandType.StoredProcedure;
    //    SqlDataReader dr = cmdisokunsl.ExecuteReader();

    //    List<isliste> NSLvipTyalo = new List<isliste>();

    //    if (dr.HasRows)
    //    {
    //        while (dr.Read())
    //        {
    //            NSLvipTyalo.Add(new isliste()
    //            {
    //                id = Convert.ToInt32(dr["id"].ToString()),
    //                imono = Convert.ToInt32(dr["imono"].ToString()),
    //                gemiadi = dr["gemiadi"].ToString(),
    //                kalkislimani = dr["kalkislimani"].ToString(),
    //                kalkisrihtimi = dr["kalkisrihtimi"].ToString(),
    //                yanasmalimani = dr["yanasmalimani"].ToString(),
    //                yanasmarihtimi = dr["yanasmarihtimi"].ToString(),
    //                demiryeri = dr["demiryeri"].ToString(),
    //                bayrak = dr["bayrak"].ToString(),
    //                tip = dr["tip"].ToString(),
    //                grt = dr["grt"].ToString(),
    //                acente = dr["acente"].ToString(),
    //                fatura = dr["fatura"].ToString(),
    //                bowt = dr["bowt"].ToString(),
    //                strnt = dr["strnt"].ToString(),
    //                loa = dr["loa"].ToString(),
    //                tehlikeliyuk = dr["tehlikeliyuk"].ToString(),
    //                draft = dr["draft"].ToString(),
    //                bilgi = dr["bilgi"].ToString(),
    //                eta = dr["eta"].ToString(),
    //                notlar = dr["notlar"].ToString(),
    //                talepno = dr["talepno"].ToString(),
    //                kaydeden = dr["kaydeden"].ToString()


    //            });

    //        }
    //    }
    //    dr.Close();
    //    cmdisokunsl.Dispose();


    //    return NSLvipTyalo;

    //}


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
                    kaydeden = dr["kaydeden"].ToString()


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
                    kaydeden = dr["kaydeden"].ToString()


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
                    kaydeden = dr["kaydeden"].ToString()


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
                    kaydeden = dr["kaydeden"].ToString()

                });

            }
        }
        dr.Close();
        cmdisokunsl.Dispose();


        return NSLYk;

    }


    protected void DLNewShipListc_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {//satır sec
            ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
            {
                e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
            }

            LinkButton Lbl5kop = (LinkButton)e.Item.FindControl("Lbl5");
            Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
            if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
            else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
            else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
            else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
            else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
            else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
            {
                demirkisakop.Text = "OTY";
                Lbl5kop.Text = "Ordino-Talep Yok";
                Lbl5kop.Style.Add("font-style", "normal");
                Lbl5kop.Style.Add("color", "#ee1111");

            }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }

            else if (demirkisakop.Text.ToLower() == "pratika yok")
            {
                demirkisakop.Text = "PRY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }

            //lyb bak
            ImageButton ImageButtonJobEdit1kop = (ImageButton)e.Item.FindControl("ImageButtonJobEdit1");  
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(lybbak.Parameters["@lybyol"].Value.ToString().Trim()) != true)
            {
                Lbl5kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl5kop.Style.Add("color", "#333333");
                Lbl5kop.Enabled = false;
            }
            lybbak.Dispose();


            //or bak
            LinkButton Lbl3kop = (LinkButton)e.Item.FindControl("Lbl3");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl3kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl3kop.Style.Add("color", "#333333");
                Lbl3kop.Enabled = false;
            }
            orbak.Dispose();
            baglanti.Close();

        }
    }
    protected void DLNewShipListvipC_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {//satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
            {
                e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
            }
            LinkButton Lbl5kop = (LinkButton)e.Item.FindControl("Lbl5");

            // demirler kısaltma

            Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
            if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
            else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
            else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
            else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
            else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
            else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
            {
                demirkisakop.Text = "OTY";
                Lbl5kop.Text = "Ordino-Talep Yok";
                Lbl5kop.Style.Add("font-style", "normal");
                Lbl5kop.Style.Add("color", "#ee1111");

            }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            else if (demirkisakop.Text.ToLower() == "pratika yok")
            {
                demirkisakop.Text = "PRY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            //lyb bak
            ImageButton ImageButtonJobEdit1kop = (ImageButton)e.Item.FindControl("ImageButtonJobEdit1");
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(lybbak.Parameters["@lybyol"].Value.ToString().Trim()) != true)
            {
                Lbl5kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl5kop.Style.Add("color", "#333333");
                Lbl5kop.Enabled = false;
            }
            lybbak.Dispose();

            //or bak
            LinkButton Lbl3kop = (LinkButton)e.Item.FindControl("Lbl3");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl3kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl3kop.Style.Add("color", "#333333");
                Lbl3kop.Enabled = false;
            }
            orbak.Dispose();
            baglanti.Close();
        }
       
    }

    protected void DLNewShipManeuvering_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
   
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
            {
                e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
            }
            LinkButton Lbl5kop = (LinkButton)e.Item.FindControl("Lbl5");

            // demirler kısaltma

            Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
            if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
            else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
            else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
            else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
            else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
            else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
            {
                demirkisakop.Text = "OTY";
                Lbl5kop.Text = "Ordino-Talep Yok";
                Lbl5kop.Style.Add("font-style", "normal");
                Lbl5kop.Style.Add("color", "#ee1111");

            }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            else if (demirkisakop.Text.ToLower() == "pratika yok")
            {
                demirkisakop.Text = "PRY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            //lyb bak
            ImageButton ImageButtonJobEdit1kop = (ImageButton)e.Item.FindControl("ImageButtonJobEdit1");
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(lybbak.Parameters["@lybyol"].Value.ToString().Trim()) != true)
            {
                Lbl5kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl5kop.Style.Add("color", "#333333");
                Lbl5kop.Enabled = false;
            }
            lybbak.Dispose();

            //or bak
            LinkButton Lbl3kop = (LinkButton)e.Item.FindControl("Lbl3");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl3kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl3kop.Style.Add("color", "#333333");
                Lbl3kop.Enabled = false;
            }
            orbak.Dispose();
            baglanti.Close();


        }

    }


    protected void DLAnchoredShipList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {//satır sec
       ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
           
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
            {
                e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
            }

            LinkButton Lbl5kop = (LinkButton)e.Item.FindControl("Lbl5");
        Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
        if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
        else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
        else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
        else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
        else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
        else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
        {
            demirkisakop.Text = "OTY";
            Lbl5kop.Text = "Ordino-Talep Yok";
            Lbl5kop.Style.Add("font-style", "normal");
            Lbl5kop.Style.Add("color", "#ee1111");

        }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            else if (demirkisakop.Text.ToLower() == "pratika yok")
            {
                demirkisakop.Text = "PRY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            //lyb bak
            ImageButton ImageButtonJobEdit1kop = (ImageButton)e.Item.FindControl("ImageButtonJobEdit1");
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(lybbak.Parameters["@lybyol"].Value.ToString().Trim()) != true)
            {
                Lbl5kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl5kop.Style.Add("color", "#333333");
                Lbl5kop.Enabled = false;
            }
            lybbak.Dispose();

            //or bak
            LinkButton Lbl3kop = (LinkButton)e.Item.FindControl("Lbl3");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl3kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl3kop.Style.Add("color", "#333333");
                Lbl3kop.Enabled = false;
            }
            orbak.Dispose();
            baglanti.Close();


        }
    }
    protected void DLVIPort_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {  //satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
            {
                e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
            }

            LinkButton Lbl5kop = (LinkButton)e.Item.FindControl("Lbl5");
        Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
        if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
        else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
        else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
        else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
        else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
        else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
        {
            demirkisakop.Text = "OTY";
            Lbl5kop.Text = "Ordino-Talep Yok";
            Lbl5kop.Style.Add("font-style", "normal");
            Lbl5kop.Style.Add("color", "#ee1111");

        }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            else if (demirkisakop.Text.ToLower() == "pratika yok")
            {
                demirkisakop.Text = "PRY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            //lyb bak
            ImageButton ImageButtonJobEdit1kop = (ImageButton)e.Item.FindControl("ImageButtonJobEdit1");
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(lybbak.Parameters["@lybyol"].Value.ToString().Trim()) != true)
            {
                Lbl5kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl5kop.Style.Add("color", "#333333");
                Lbl5kop.Enabled = false;
            }
            lybbak.Dispose();

            //or bak
            LinkButton Lbl3kop = (LinkButton)e.Item.FindControl("Lbl3");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl3kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl3kop.Style.Add("color", "#333333");
                Lbl3kop.Enabled = false;
            }
            orbak.Dispose();
            baglanti.Close();


        }
    }
    //protected void DLVIPortTers_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{  //satır sec
    //    ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
    //    if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
    //        {
    //            e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
    //        }
 
    //    Label Lbl5ckop = (Label)e.Item.FindControl("Lbl5");
    //    Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
    //    if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
    //    else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
    //    else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
    //    else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
    //    else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
    //    else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
    //    {
    //        demirkisakop.Text = "OTY";
    //        Lbl5ckop.Text = "Ordino-Talep Yok";
    //        Lbl5ckop.Style.Add("font-style", "normal");
    //        Lbl5ckop.Style.Add("color", "#ee1111");

    //    }
    //        else if (demirkisakop.Text.ToLower() == "demir-izni yok")
    //        {
    //            demirkisakop.Text = "DİY";
    //            demirkisakop.Style.Add("font-style", "normal");
    //            demirkisakop.Style.Add("color", "#ee1111");
    //        }
            //    else if (demirkisakop.Text.ToLower() == "pratika yok")
            //{
            //    demirkisakop.Text = "PRY";
            //    demirkisakop.Style.Add("font-style", "normal");
            //    demirkisakop.Style.Add("color", "#ee1111");
            //}
//    }
//}

//protected void DLVIPortyalo_ItemDataBound(object sender, RepeaterItemEventArgs e)
//{  //satır sec
//    ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
//    if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
//    {
//        if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
//        {
//            e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
//        }

//        Label Lbl5ckop = (Label)e.Item.FindControl("Lbl5");
//        Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
//        if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
//        else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
//        else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
//        else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
//        else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
//        else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
//        {
//            demirkisakop.Text = "OTY";
//            Lbl5ckop.Text = "Ordino-Talep Yok";
//            Lbl5ckop.Style.Add("font-style", "normal");
//            Lbl5ckop.Style.Add("color", "#ee1111");

//        }
//        else if (demirkisakop.Text.ToLower() == "demir-izni yok")
//        {
//            demirkisakop.Text = "DİY";
//            demirkisakop.Style.Add("font-style", "normal");
//            demirkisakop.Style.Add("color", "#ee1111");
//        }
//    }
//}
protected void DLher_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {  //satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
            {
                e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
            }

            LinkButton Lbl5kop = (LinkButton)e.Item.FindControl("Lbl5");
        Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
        if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
        else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
        else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
        else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
        else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
        else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
        {
            demirkisakop.Text = "OTY";
            Lbl5kop.Text = "Ordino-Talep Yok";
            Lbl5kop.Style.Add("font-style", "normal");
            Lbl5kop.Style.Add("color", "#ee1111");

        }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            else if (demirkisakop.Text.ToLower() == "pratika yok")
            {
                demirkisakop.Text = "PRY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            //lyb bak
            ImageButton ImageButtonJobEdit1kop = (ImageButton)e.Item.FindControl("ImageButtonJobEdit1");
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(lybbak.Parameters["@lybyol"].Value.ToString().Trim()) != true)
            {
                Lbl5kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl5kop.Style.Add("color", "#333333");
                Lbl5kop.Enabled = false;
            }
            lybbak.Dispose();

            //or bak
            LinkButton Lbl3kop = (LinkButton)e.Item.FindControl("Lbl3");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl3kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl3kop.Style.Add("color", "#333333");
                Lbl3kop.Enabled = false;
            }
            orbak.Dispose();
            baglanti.Close();


        }
    }
    protected void DLyaranc_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {  //satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
            {
                e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
            }

            LinkButton Lbl5kop = (LinkButton)e.Item.FindControl("Lbl5");
        Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
        if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
        else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
        else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
        else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
        else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
        else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
        {
            demirkisakop.Text = "OTY";
            Lbl5kop.Text = "Ordino-Talep Yok";
            Lbl5kop.Style.Add("font-style", "normal");
            Lbl5kop.Style.Add("color", "#ee1111");

        }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            else if (demirkisakop.Text.ToLower() == "pratika yok")
            {
                demirkisakop.Text = "PRY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            //lyb bak
            ImageButton ImageButtonJobEdit1kop = (ImageButton)e.Item.FindControl("ImageButtonJobEdit1");
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(lybbak.Parameters["@lybyol"].Value.ToString().Trim()) != true)
            {
                Lbl5kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl5kop.Style.Add("color", "#333333");
                Lbl5kop.Enabled = false;
            }
            lybbak.Dispose();

            //or bak
            LinkButton Lbl3kop = (LinkButton)e.Item.FindControl("Lbl3");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl3kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl3kop.Style.Add("color", "#333333");
                Lbl3kop.Enabled = false;
            }
            orbak.Dispose();
            baglanti.Close();


        }
    }
    protected void DLyarport_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {  //satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
            {
                e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
            }

            LinkButton Lbl5kop = (LinkButton)e.Item.FindControl("Lbl5");
        Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
        if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
        else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
        else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
        else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
        else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
        else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
        {
            demirkisakop.Text = "OTY";
            Lbl5kop.Text = "Ordino-Talep Yok";
            Lbl5kop.Style.Add("font-style", "normal");
            Lbl5kop.Style.Add("color", "#ee1111");

        }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            else if (demirkisakop.Text.ToLower() == "pratika yok")
            {
                demirkisakop.Text = "PRY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            //lyb bak
            ImageButton ImageButtonJobEdit1kop = (ImageButton)e.Item.FindControl("ImageButtonJobEdit1");
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(lybbak.Parameters["@lybyol"].Value.ToString().Trim()) != true)
            {
                Lbl5kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl5kop.Style.Add("color", "#333333");
                Lbl5kop.Enabled = false;
            }
            lybbak.Dispose();

            //or bak
            LinkButton Lbl3kop = (LinkButton)e.Item.FindControl("Lbl3");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl3kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl3kop.Style.Add("color", "#333333");
                Lbl3kop.Enabled = false;
            }
            orbak.Dispose();
            baglanti.Close();


        }
    }
    protected void DLVIPortKosb_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {  //satır sec
        ScriptManager.RegisterStartupScript(this, GetType(), "select", "select();", true);
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (Session["yetki"].ToString() != "1" && Session["yetki"].ToString() != "2")
            {
                e.Item.FindControl("ImageButtonJobEdit1").Visible = (false);
            }

            LinkButton Lbl5kop = (LinkButton)e.Item.FindControl("Lbl5");
        Label demirkisakop = (Label)e.Item.FindControl("demirkisa");
        if (demirkisakop.Text.ToLower() == "demir-eskihisar") { demirkisakop.Text = "EHS"; }
        else if (demirkisakop.Text.ToLower() == "demir-yalova") { demirkisakop.Text = "YLV"; }
        else if (demirkisakop.Text.ToLower() == "demir-yarımca") { demirkisakop.Text = "YRM"; }
        else if (demirkisakop.Text.ToLower() == "demir-izmit") { demirkisakop.Text = "İZM"; }
        else if (demirkisakop.Text.ToLower() == "demir-hereke") { demirkisakop.Text = "HRK"; }
        else if (demirkisakop.Text.ToLower() == "ordino-talep yok")
        {
            demirkisakop.Text = "OTY";
            Lbl5kop.Text = "Ordino-Talep Yok";
            Lbl5kop.Style.Add("font-style", "normal");
            Lbl5kop.Style.Add("color", "#ee1111");

        }
            else if (demirkisakop.Text.ToLower() == "demir-izni yok")
            {
                demirkisakop.Text = "DİY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            else if (demirkisakop.Text.ToLower() == "pratika yok")
            {
                demirkisakop.Text = "PRY";
                demirkisakop.Style.Add("font-style", "normal");
                demirkisakop.Style.Add("color", "#ee1111");
            }
            //lyb bak
            ImageButton ImageButtonJobEdit1kop = (ImageButton)e.Item.FindControl("ImageButtonJobEdit1");
            SqlConnection baglanti = AnaKlas.baglan();
            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(lybbak.Parameters["@lybyol"].Value.ToString().Trim()) != true)
            {
                Lbl5kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl5kop.Style.Add("color", "#333333");
                Lbl5kop.Enabled = false;
            }
            lybbak.Dispose();

            //or bak
            LinkButton Lbl3kop = (LinkButton)e.Item.FindControl("Lbl3");
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(ImageButtonJobEdit1kop.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
            {
                Lbl3kop.Style.Add("color", "#1111ee");
            }
            else
            {
                Lbl3kop.Style.Add("color", "#333333");
                Lbl3kop.Enabled = false;
            }
            orbak.Dispose();
            baglanti.Close();


        }
    }

    protected void ButtonANJshowpopup_Click(object sender, EventArgs e)
    {
        Response.Redirect("newjob.aspx");
    }

    protected void ImageButtonJobEdit_Click(object sender, ImageClickEventArgs e)
    {
        TBJEimo.BorderColor = System.Drawing.Color.Gray;
        TBJEimo.BorderWidth = 1;
        TBJEsn.BorderColor = System.Drawing.Color.Gray;
        TBJEsn.BorderWidth = 1;
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
        DDLJEdepp.BorderColor = System.Drawing.Color.Gray;
        DDLJEdp.BorderColor = System.Drawing.Color.Gray;
        DDLJEflag.BorderColor = System.Drawing.Color.Gray;
        DDLJEtip.BorderColor = System.Drawing.Color.Gray;
        DDLJEdepb.BorderColor = System.Drawing.Color.Gray;
        DDLJEdb.BorderColor = System.Drawing.Color.Gray;
        DDLJEdc.BorderColor = System.Drawing.Color.Gray;
        DDLJEdc.BorderWidth = 1;
        DDLJEap.BorderWidth = 1;
        DDLJEdepp.BorderWidth = 1;
        DDLJEdp.BorderWidth = 1;
        DDLJEflag.BorderWidth = 1;
        DDLJEtip.BorderWidth = 1;
        DDLJEdepb.BorderWidth = 1;
        DDLJEdb.BorderWidth = 1;
        TBJEpratikano.BorderColor = System.Drawing.Color.Gray;
        TBJEpratikano.BorderWidth = 1;
        DDLJEpratika.BorderColor = System.Drawing.Color.Gray;
        DDLJEpratika.BorderWidth = 1;

        DDLJEdepb.Visible = true;
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
        string kalkislimani = "";
        string kalkisrihtimi = "";
        string yanasmalimani = "";
        string yanasmarihtimi = "";
        string pratika = "";

        TBJEetadt.Text = TarihSaatYaziYapDMYhm(DateTime.Now.AddMinutes(30));

        if (gemireader.HasRows)
        {
            while (gemireader.Read())
            {
                TBJEimo.Text = gemireader["imono"].ToString();
                TBJEsn.Text = gemireader["gemiadi"].ToString();
                demiryeri = gemireader["demiryeri"].ToString();
                bayrak = gemireader["bayrak"].ToString();
                tip = gemireader["tip"].ToString();
                kalkislimani = gemireader["kalkislimani"].ToString();
                LblDDLJEdepp.Text = gemireader["kalkislimani"].ToString(); // gemi departure düzeltilirse doğru tabloya geçsin
                kalkisrihtimi = gemireader["kalkisrihtimi"].ToString();
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

        DDLJEdepp.Items.Clear();
        SqlCommand cmdDDLliman = new SqlCommand("SP_DDLlimanal", baglanti);
        cmdDDLliman.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adapterp = new SqlDataAdapter();
        adapterp.SelectCommand = cmdDDLliman;
        DataSet dsp = new DataSet();
        adapterp.Fill(dsp, "limanlar");
        DDLJEdepp.DataValueField = "limanno";
        DDLJEdepp.DataTextField = "limanadi";
        DDLJEdepp.DataSource = dsp;
        DDLJEdepp.DataBind();
        DDLJEdepp.ClearSelection();
        if (kalkislimani != "") { DDLJEdepp.Items.FindByText(kalkislimani).Selected = true; }

        //kalkış rihtim
        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim.Parameters.AddWithValue("@limanadi", DDLJEdepp.SelectedItem.Text);
        SqlDataAdapter adapterr = new SqlDataAdapter();
        adapterr.SelectCommand = cmdDDLrihtim;
        DataSet dsr = new DataSet();
        adapterr.Fill(dsr, "limanlar");
        DDLJEdepb.Items.Clear();
        DDLJEdepb.DataValueField = "id";
        DDLJEdepb.DataTextField = "rihtimadi";
        DDLJEdepb.DataSource = dsr;
        DDLJEdepb.DataBind();
        DDLJEdepb.Items.FindByText(kalkisrihtimi).Selected = true;
        if (kalkisrihtimi == "0") { DDLJEdepb.Visible = false; }

        //variş liman
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
        TBJEimo.BorderColor = System.Drawing.Color.Gray;
        TBJEimo.BorderWidth = 1;
        TBJEsn.BorderColor = System.Drawing.Color.Gray;
        TBJEsn.BorderWidth = 1;
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
        DDLJEdepp.BorderColor = System.Drawing.Color.Gray;
        DDLJEflag.BorderColor = System.Drawing.Color.Gray;
        DDLJEtip.BorderColor = System.Drawing.Color.Gray;
        DDLJEdb.BorderColor = System.Drawing.Color.Gray;
        DDLJEdepb.BorderColor = System.Drawing.Color.Gray;
        DDLJEdc.BorderColor = System.Drawing.Color.Gray;
        TBJEpratikano.BorderColor = System.Drawing.Color.Gray;
        TBJEpratikano.BorderWidth = 1;
        DDLJEpratika.BorderColor = System.Drawing.Color.Gray;
        DDLJEpratika.BorderWidth = 1;
        DDLJEdc.BorderWidth = 1;
        DDLJEap.BorderWidth = 1;
        DDLJEdp.BorderWidth = 1;
        DDLJEdepp.BorderWidth = 1;
        DDLJEflag.BorderWidth = 1;
        DDLJEtip.BorderWidth = 1;
        DDLJEdb.BorderWidth = 1;
        DDLJEdepb.BorderWidth = 1;

        string imono = AnaKlas.TemizleRakamoldu(TBJEimo.Text.ToLower());
        string gemiadi = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temizle(TBJEsn.Text.ToString().Trim().ToLower()));
        string demiryeri = HttpUtility.HtmlDecode(DDLJEap.SelectedItem.Text);
        string kalkislimani = HttpUtility.HtmlDecode(DDLJEdepp.SelectedItem.Text);
        string kalkisrihtimi = HttpUtility.HtmlDecode(DDLJEdepb.SelectedItem.Text);
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

        if (imono == "" || imono == "0000000" || imono == "9999999" || imono == "8888888" || imono == null || imono.Length != 7 || imono.Substring(0, 1) == "0")
        {
            imono = "";
            TBJEimo.BorderColor = System.Drawing.Color.Red;
            TBJEimo.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }

        if (gemiadi == "" || gemiadi == null)
        {
            gemiadi = "";
            TBJEsn.BorderColor = System.Drawing.Color.Red;
            TBJEsn.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }

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
        else if (Convert.ToDateTime(eta) < DateTime.Now)
        {
            eta = "";
            TBJEetadt.BorderColor = System.Drawing.Color.Red;
            TBJEetadt.BorderWidth = 1;
            this.ModalPopupExtenderjobedit.Show();
        }
        else if (Convert.ToDateTime(eta) > DateTime.Now.AddDays(15))
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

        if (imono != "" && gemiadi != "" && demiryeri != "" && yanasmalimani != "" && yanasmarihtimi != "" && kalkislimani != "" && kalkisrihtimi != "" && eta != "" && bowt != "" && strnt != "" && bayrak != "" && grt != "" && tip != "" && loa != ""  && tehlikeliyuk != "" && acente != "" && fatura != "" && notlar != "" && talepno != "")
        {
            if (imono.Length == 7)
            {

                int imonoi = Convert.ToInt32(imono);

                string nedurumda = Lblnedurumda.Text;
                string nedurumdaopr = Lblnedurumdaopr.Text;

                if (LblDDLJEdepp.Text != kalkislimani)
                {
                    SqlCommand cmdLimannoal = new SqlCommand("SP_Lim_Limannoal", baglanti);
                    cmdLimannoal.CommandType = CommandType.StoredProcedure;
                    cmdLimannoal.Parameters.AddWithValue("@limanadi", kalkislimani);
                    cmdLimannoal.Parameters.Add("@limanno", SqlDbType.Int);
                    cmdLimannoal.Parameters["@limanno"].Direction = ParameterDirection.Output;
                    cmdLimannoal.ExecuteNonQuery();
                    int portno = Convert.ToInt32(cmdLimannoal.Parameters["@limanno"].Value.ToString());
                    cmdLimannoal.Dispose();
                    if (portno > 0 && portno < 900) // limanda
                    {
                        nedurumda = "2";
                        nedurumdaopr = "2";
                    }

                    else if (portno > 1000 && portno < 1099) // demirde
                    {
                        nedurumda = "1";
                        nedurumdaopr = "1";
                    }


                    else if (portno == 998) // yelkenkaya
                    {
                        nedurumda = "4";
                        nedurumdaopr = "4";
                    }

                    //else if (portno == 999) // to order
                    //{                }

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
                    if (soyisimbul.ToLower() == "yüksel") { soyisimbul = "Küksel"; }
                    cmdsoyisimbul.Dispose();
                    isimkisalt = isimbul.Substring(0, 1) + soyisimbul.Substring(0, 1);

                    if (notlar.Length>13 && notlar.EndsWith(")") && notlar.Substring(notlar.Length - 5, 1) == "-")
                    { notlar = notlar.Substring(0, notlar.Length - 14); }

                

                int secimedit = Convert.ToInt32(LJEid.Text);

                SqlCommand cmd = new SqlCommand("SP_Up_Isliste_full", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", secimedit);
                cmd.Parameters.AddWithValue("imono", imonoi);
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
                cmd.Parameters.AddWithValue("nedurumda", nedurumda);
                cmd.Parameters.AddWithValue("nedurumdaopr", nedurumdaopr);
                cmd.Parameters.AddWithValue("talepno", talepno);
                cmd.Parameters.AddWithValue("pratika", pratika);
                cmd.Parameters.AddWithValue("pratikano", pratikano);
                cmd.Parameters.AddWithValue("kayitzamani", Lblkayitzamani.Text);
                cmd.Parameters.AddWithValue("kaydeden", isimkisalt + " " + DateTime.Now.ToShortDateString().Substring(0, 2) + "|" + DateTime.Now.ToShortTimeString().Substring(0, 2));
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

        }
    
        DTLoading(baglanti);
    baglanti.Close();


    }

    protected void DDLJEdepp_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLJEdepb.Visible = true;
        DDLJEdepb.Items.Clear();
        this.ModalPopupExtenderjobedit.Show();

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdDDLrihtim = new SqlCommand("SP_DDLrihtimal", baglanti);
        cmdDDLrihtim.CommandType = CommandType.StoredProcedure;
        cmdDDLrihtim.Parameters.AddWithValue("@limanadi", DDLJEdepp.SelectedItem.Text);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdDDLrihtim;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "limanlar");
        DDLJEdepb.DataValueField = "id";
        DDLJEdepb.DataTextField = "rihtimadi";
        DDLJEdepb.DataSource = ds;
        DDLJEdepb.DataBind();
        baglanti.Close();

        if (DDLJEdepb.SelectedItem.Text == "0") { DDLJEdepb.Visible = false; }
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
        if (Convert.ToInt32(Session["yetki"]) == 1 || Convert.ToInt32(Session["yetki"]) == 2)
        {
            //          Response.Redirect("pmtr.aspx");
        }
        else if (Convert.ToInt32(Session["yetki"]) == 0)
        {
            //       Response.Redirect("yonet/yonetim.aspx");
        }
        else if (Convert.ToInt32(Session["yetki"]) == 9)
        {
            Response.Redirect("yonet/yonetim.aspx");
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



    protected void LBLgemiadi1c_Click(object sender, EventArgs e)
    {

        LinkButton LinkButtonkopya = (LinkButton)sender;
        string shipid = HttpUtility.HtmlDecode(LinkButtonkopya.CommandArgument).ToString();
        string shipname = HttpUtility.HtmlDecode(LinkButtonkopya.CommandName).ToString();

        TBgemilyb.ForeColor = System.Drawing.Color.Black;
        TBgemilyb.Text = shipname;
        BlybEkle.CommandArgument = shipid;

        TBgemior.ForeColor = System.Drawing.Color.Black;
        TBgemior.Text = shipname;
        BorEkle.CommandArgument = shipid;

    }

    string uzanti = "";
    string resimadi = "";
    protected void BlybEkle_Click(object sender, EventArgs e)
    {

            if (fuResim.HasFile && TBgemilyb.Text !="" && BlybEkle.CommandArgument!="")
        {
            uzanti = Path.GetExtension(fuResim.PostedFile.FileName);
            if (uzanti ==".jpg")
            { 
            resimadi = "lyb_" + Path.GetFileNameWithoutExtension(fuResim.PostedFile.FileName) + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + uzanti;
            fuResim.SaveAs(Server.MapPath("LYB/sahte" + uzanti));

            int Donusturme = 1000;

            Bitmap bmp = new Bitmap(Server.MapPath("LYB/sahte" + uzanti));
            using (Bitmap OrjinalResim = bmp)
            {
                double ResYukseklik = OrjinalResim.Height;
                double ResGenislik = OrjinalResim.Width;
                double oran = 0;

                if (ResGenislik >= Donusturme)
                {
                    oran = ResGenislik / ResYukseklik;
                    ResGenislik = Donusturme;
                    ResYukseklik = Donusturme / oran;

                    Size yenidegerler = new Size(Convert.ToInt32(ResGenislik), Convert.ToInt32(ResYukseklik));
                    Bitmap yeniresim = new Bitmap(OrjinalResim, yenidegerler);
                    yeniresim.Save(Server.MapPath("LYB/" + resimadi));

                    yeniresim.Dispose();
                    OrjinalResim.Dispose();
                    bmp.Dispose();


                }
                else
                {
                    fuResim.SaveAs(Server.MapPath("LYB/" + resimadi));
                }
            }
            FileInfo figecici = new FileInfo(Server.MapPath("LYB/sahte" + uzanti));
            figecici.Delete();


                SqlConnection baglanti = AnaKlas.baglan();

                //yol oku
                SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
                lybbak.CommandType = CommandType.StoredProcedure;
                lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(BlybEkle.CommandArgument));
                lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
                lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
                lybbak.ExecuteNonQuery();
                if (string.IsNullOrEmpty(lybbak.Parameters["@lybyol"].Value.ToString().Trim()) != true)
                {   FileInfo fieski = new FileInfo(Server.MapPath(lybbak.Parameters["@lybyol"].Value.ToString()));
                    fieski.Delete();
                     }
                lybbak.Dispose();

                SqlCommand cmd = new SqlCommand("SP_Up_Isliste_lyb", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(BlybEkle.CommandArgument));
                cmd.Parameters.AddWithValue("lybyol", "LYB/" + resimadi);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                DTLoading(baglanti);
                baglanti.Close();

                BlybEkle.CommandArgument = "";
                TBgemilyb.Text = "LYB Loaded Successfully";
                TBgemilyb.ForeColor = System.Drawing.Color.Green;
                fuResim.Dispose();
                //fuResim.PostedFile.InputStream.Dispose();
                //fuResim.Attributes.Clear();
                fuResim.ID = null;
            }
            else
            { TBgemilyb.Text= "Please Select jpg File";
              BlybEkle.CommandArgument = "";
              TBgemilyb.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            TBgemilyb.Text = "Pls.Select a Ship";
            BlybEkle.CommandArgument = "";
            TBgemilyb.ForeColor = System.Drawing.Color.Red;
        }


    }

    protected void Lbl5_Click(object sender, EventArgs e)
    {
        LinkButton LBLbl5kop = (LinkButton)sender;
        string shipid = HttpUtility.HtmlDecode(LBLbl5kop.CommandArgument).ToString();

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
        lybbak.CommandType = CommandType.StoredProcedure;
        lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(shipid));
        lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
        lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
        lybbak.ExecuteNonQuery();

        string lybyol = lybbak.Parameters["@lybyol"].Value.ToString().Trim();
        lybbak.Dispose();
        baglanti.Close();
        baglanti.Dispose();

        if (string.IsNullOrEmpty(lybyol) != true)
        {
            Response.Redirect(lybyol, false);
        }

    }



    protected void Blybsil_Click(object sender, EventArgs e)
    {
        if (TBgemilyb.Text != "" && BlybEkle.CommandArgument != "")
        {
            SqlConnection baglanti = AnaKlas.baglan();

            //yol oku
            SqlCommand lybbak = new SqlCommand("SP_lybbak", baglanti);
            lybbak.CommandType = CommandType.StoredProcedure;
            lybbak.Parameters.AddWithValue("@id", Convert.ToInt32(BlybEkle.CommandArgument));
            lybbak.Parameters.Add("@lybyol", SqlDbType.NVarChar, 250);
            lybbak.Parameters["@lybyol"].Direction = ParameterDirection.Output;
            lybbak.ExecuteNonQuery();
            lybbak.Dispose();

    //        protected void yuklenenDosyalar()
    //{
    //    DirectoryInfo klasor = new DirectoryInfo(MapPath("dosyalar"));
    //    ddl.DataSource = klasor.GetFiles();
    //    ddl.DataBind();
    //    lblyd.Text = ddl.Items.Count + " adet yüklenmiş dosya ! ";
    //    if (ddl.Items.Count == 0)
    //    {
    //        ddl.Items.Add("Yüklenmiş dosya bulunmuyor.!");
    //        ddl.Enabled = false;
    //        btnsil.Visible = false;
    //    }
    //}

   

            SqlCommand cmd = new SqlCommand("SP_Up_Isliste_lyb", baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(BlybEkle.CommandArgument));
            cmd.Parameters.AddWithValue("lybyol", "");

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            DTLoading(baglanti);
            baglanti.Close();

            
            fuResim.Dispose();
            fuResim.ID = null;

            FileInfo fisil = new FileInfo(Server.MapPath(lybbak.Parameters["@lybyol"].Value.ToString()));
            try { fisil.Delete();
                BlybEkle.CommandArgument = "";
                TBgemilyb.Text = "LYB Silindi";
                TBgemilyb.ForeColor = System.Drawing.Color.Green;
            }
            catch {
                BlybEkle.CommandArgument = "";
                TBgemilyb.Text = "No File Found";
                TBgemilyb.ForeColor = System.Drawing.Color.Red;
            }

        }
        else
        {
            TBgemilyb.Text = "Pls.Select a Ship";
            BlybEkle.CommandArgument = "";
            TBgemilyb.ForeColor = System.Drawing.Color.Red;
        }

    }

    protected void BorEkle_Click(object sender, EventArgs e)
    {
        if (fuResimor.HasFile && TBgemior.Text != "" && BorEkle.CommandArgument != "")
        {
            uzanti = Path.GetExtension(fuResimor.PostedFile.FileName);
            if (uzanti == ".jpg")
            {
                resimadi = "or_" + Path.GetFileNameWithoutExtension(fuResimor.PostedFile.FileName) + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + uzanti;
                fuResimor.SaveAs(Server.MapPath("OR/sahte" + uzanti));

                int Donusturme = 1000;

                Bitmap bmp = new Bitmap(Server.MapPath("OR/sahte" + uzanti));
                using (Bitmap OrjinalResim = bmp)
                {
                    double ResYukseklik = OrjinalResim.Height;
                    double ResGenislik = OrjinalResim.Width;
                    double oran = 0;

                    if (ResGenislik >= Donusturme)
                    {
                        oran = ResGenislik / ResYukseklik;
                        ResGenislik = Donusturme;
                        ResYukseklik = Donusturme / oran;

                        Size yenidegerler = new Size(Convert.ToInt32(ResGenislik), Convert.ToInt32(ResYukseklik));
                        Bitmap yeniresim = new Bitmap(OrjinalResim, yenidegerler);
                        yeniresim.Save(Server.MapPath("OR/" + resimadi));

                        yeniresim.Dispose();
                        OrjinalResim.Dispose();
                        bmp.Dispose();


                    }
                    else
                    {
                        fuResimor.SaveAs(Server.MapPath("OR/" + resimadi));
                    }
                }
                FileInfo figecici = new FileInfo(Server.MapPath("OR/sahte" + uzanti));
                figecici.Delete();


                SqlConnection baglanti = AnaKlas.baglan();

                //yol oku
                SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
                orbak.CommandType = CommandType.StoredProcedure;
                orbak.Parameters.AddWithValue("@id", Convert.ToInt32(BorEkle.CommandArgument));
                orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
                orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
                orbak.ExecuteNonQuery();
                if (string.IsNullOrEmpty(orbak.Parameters["@oryol"].Value.ToString().Trim()) != true)
                {
                    FileInfo fieski = new FileInfo(Server.MapPath(orbak.Parameters["@oryol"].Value.ToString()));
                    fieski.Delete();
                }
                orbak.Dispose();

                SqlCommand cmd = new SqlCommand("SP_Up_Isliste_or", baglanti);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(BorEkle.CommandArgument));
                cmd.Parameters.AddWithValue("oryol", "OR/" + resimadi);

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                DTLoading(baglanti);
                baglanti.Close();

                BorEkle.CommandArgument = "";
                TBgemior.Text = "Ordino Loaded Successfully";
                TBgemior.ForeColor = System.Drawing.Color.Green;
                fuResimor.Dispose();
                //fuResimor.PostedFile.InputStream.Dispose();
                //fuResimor.Attributes.Clear();
                fuResimor.ID = null;
            }
            else
            {
                TBgemior.Text = "Please Select jpg File";
                BorEkle.CommandArgument = "";
                TBgemior.ForeColor = System.Drawing.Color.Red;
            }
        }
        else
        {
            TBgemior.Text = "Pls.Select a Ship";
            BorEkle.CommandArgument = "";
            TBgemior.ForeColor = System.Drawing.Color.Red;
        }

    }

    protected void Borsil_Click(object sender, EventArgs e)
    {
        if (TBgemior.Text != "" && BorEkle.CommandArgument != "")
        {
            SqlConnection baglanti = AnaKlas.baglan();

            //yol oku
            SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
            orbak.CommandType = CommandType.StoredProcedure;
            orbak.Parameters.AddWithValue("@id", Convert.ToInt32(BorEkle.CommandArgument));
            orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
            orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
            orbak.ExecuteNonQuery();
            orbak.Dispose();

            SqlCommand cmd = new SqlCommand("SP_Up_Isliste_or", baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(BorEkle.CommandArgument));
            cmd.Parameters.AddWithValue("oryol", "");

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            DTLoading(baglanti);
            baglanti.Close();

            fuResimor.Dispose();
            fuResimor.ID = null;

            FileInfo fisil = new FileInfo(Server.MapPath(orbak.Parameters["@oryol"].Value.ToString()));
            try
            {
                fisil.Delete();
                BorEkle.CommandArgument = "";
                TBgemior.Text = "Ordino Silindi";
                TBgemior.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {
                BorEkle.CommandArgument = "";
                TBgemior.Text = "No File Found";
                TBgemior.ForeColor = System.Drawing.Color.Red;
            }

        }
        else
        {
            TBgemior.Text = "Pls.Select a Ship";
            BorEkle.CommandArgument = "";
            TBgemior.ForeColor = System.Drawing.Color.Red;
        }

    }

    protected void Lbl3_Click(object sender, EventArgs e)
    {
        LinkButton LBLbl3kop = (LinkButton)sender;
        string shipid = HttpUtility.HtmlDecode(LBLbl3kop.CommandArgument).ToString();

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand orbak = new SqlCommand("SP_orbak", baglanti);
        orbak.CommandType = CommandType.StoredProcedure;
        orbak.Parameters.AddWithValue("@id", Convert.ToInt32(shipid));
        orbak.Parameters.Add("@oryol", SqlDbType.NVarChar, 250);
        orbak.Parameters["@oryol"].Direction = ParameterDirection.Output;
        orbak.ExecuteNonQuery();

        string oryol = orbak.Parameters["@oryol"].Value.ToString().Trim();
        orbak.Dispose();
        baglanti.Close();
        baglanti.Dispose();

        if (string.IsNullOrEmpty(oryol) != true)
        {
            Response.Redirect(oryol, false);
        }
    }
}


