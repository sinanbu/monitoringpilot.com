using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Data;
using System.Globalization;
using System.Web;
using System.Windows.Forms;
using AjaxControlToolkit;
using System.Web.UI;

public partial class sipy: System.Web.UI.Page
{

    Metodlar AnaKlas = new Metodlar();

    string ww = "";
    string hh = "";
 
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection baglanti = AnaKlas.baglan();
        //SqlCommand cmdlogofbak = new SqlCommand("Select id from onoffline where kapno='" + Session["kapno"] + "' and time = 0.001 ", baglanti);

        //string height = HttpContext.Current.Request.Params["clientScreenHeight"];
        //string width = HttpContext.Current.Request.Params["clientScreenWidth"];

        //int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        //int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        //this.Location = new Point(0, 0);
        //this.Size = new Size(screenWidth, screenHeight);
        string pcip = AnaKlas.Pcipal();

        if (pcip == "78.189.22.74" || pcip == "195.175.33.210" || pcip == "78.186.197.207" || pcip == "212.156.49.226" || Session["kapno"] != null)
        {
            if (!IsPostBack)
            {
                DTloading();

                // Register JavaScript which will collect the value and assign to HiddenField and trigger a postback
                //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SetHiddenField", "SetHiddenField();", true);

                //hh = height.Value;
                //ww = width.Value;
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SetHiddenField", "SetHiddenField();", true);
                //hh = height.Value;
                //ww = width.Value;
                ////Also, I would add other checking to make sure that this is posted back by our script
                //string ControlID = string.Empty;
                //if (!String.IsNullOrEmpty(Request.Form["height"]))
                //{
                //    ControlID = Request.Form["height"];
                //}
                //if (ControlID == height.ClientID)
                //{
                //    //On postback do our operation
                //    hh = height.Value;
                //    //etc...
                //}
                //if (ControlID == width.ClientID)
                //{
                //    //On postback do our operation
                //    ww = width.Value;
                //    //etc...
                //}
            }
        }
        else  //if (cmdlogofbak.ExecuteScalar() != null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }



        baglanti.Close();

    }

    protected void gerisayimtik_Tick(object sender, EventArgs e)
    {
        DTloading();


    }


    private void DTloading()
    {
        kroman1.Visible = false;
        kroman2.Visible = false;
        kroman3.Visible = false;
        kroman4.Visible = false;
        kroman5.Visible = false;

        diler1.Visible = false;
        diler2.Visible = false;
        diler3.Visible = false;
        diler4.Visible = false;
        diler5.Visible = false;
        diler6.Visible = false;
        diler7.Visible = false;
        diler8.Visible = false;

        nuh1.Visible = false;
        nuh2.Visible = false;
        nuh3.Visible = false;
        nuh4.Visible = false;

        evyap1.Visible = false;
        evyap2.Visible = false;
        evyap3.Visible = false;
        evyap4.Visible = false;
        evyap5.Visible = false;
        evyap6.Visible = false;
        evyap7.Visible = false;
        evyap8.Visible = false;
        evyap9.Visible = false;
        evyap10.Visible = false;
        evyap11.Visible = false;

        gubre1.Visible = false;
        gubre2.Visible = false;
        gubre3.Visible = false;

        turkuaz.Visible = false;

        marmara.Visible = false;

        rota1.Visible = false;
        rota2.Visible = false;
        rota3.Visible = false;
        rota4.Visible = false;
        rota5.Visible = false;
        rota6.Visible = false;

        milangaz.Visible = false;

        igsas1.Visible = false;
        igsas2.Visible = false;
        igsas3.Visible = false;
        igsas4.Visible = false;
        igsas5.Visible = false;
        igsas6.Visible = false;
        igsas7.Visible = false;

        dp1.Visible = false;
        dp2.Visible = false;
        dp3.Visible = false;
        dp4.Visible = false;
        dp5.Visible = false;
        dp6.Visible = false;

        oyak1.Visible = false;
        oyak2.Visible = false;
        oyak3.Visible = false;

        tupf11.Visible = false;
        tupf12.Visible = false;
        tupf13.Visible = false;
        tupf14.Visible = false;
        tupf15.Visible = false;
        tupf16.Visible = false;

        tupf21.Visible = false;
        tupf22.Visible = false;

        tupf31.Visible = false;
        tupf32.Visible = false;
        tupf33.Visible = false;
        tupf34.Visible = false;
        tupf35.Visible = false;
        tupf36.Visible = false;
        tupf37.Visible = false;

        tupp.Visible = false;

        poas1.Visible = false;
        poas2.Visible = false;
        poas3.Visible = false;

        shell.Visible = false;

        koruma1.Visible = false;
        koruma2.Visible = false;
        koruma3.Visible = false;

        aktas.Visible = false;

        ford1.Visible = false;
        ford2.Visible = false;

        auto1.Visible = false;
        auto2.Visible = false;
        auto3.Visible = false;
        auto4.Visible = false;

        limas1.Visible = false;
        limas2.Visible = false;
        limas3.Visible = false;
        limas4.Visible = false;
        limas5.Visible = false;
        limas6.Visible = false;
        limas7.Visible = false;
        limas8.Visible = false;

        kosbas1.Visible = false;
        kosbas2.Visible = false;
        kosbas3.Visible = false;
        kosbas4.Visible = false;
        //kosbas5.Visible = false;
        //kosbas6.Visible = false;
        //kosbas7.Visible = false;
        //kosbas8.Visible = false;
        //kosbas9.Visible = false;
        //kosbas10.Visible = false;
        //kosbas11.Visible = false;
        //kosbas12.Visible = false;
        //kosbas13.Visible = false;
        //kosbas14.Visible = false;
        //kosbas15.Visible = false;
        //kosbas16.Visible = false;
        //kosbas17.Visible = false;
        //kosbas18.Visible = false;
        //kosbas19.Visible = false;
        //kosbas20.Visible = false;
        //kosbas11.Visible = false;
        //kosbas22.Visible = false;
        //kosbas23.Visible = false;
        //kosbas24.Visible = false;
        //kosbas25.Visible = false;
        //kosbas26.Visible = false;



        //pagestyle.Attributes.Add


        SqlConnection baglanti = AnaKlas.baglan();

        int inportsay = 0;


        SqlCommand cmdfatoku = new SqlCommand("SP_Isliste_NSL_VipOnly", baglanti);
        cmdfatoku.CommandType = CommandType.StoredProcedure;
        SqlDataReader fatrdr = cmdfatoku.ExecuteReader();

        if (fatrdr.HasRows)
        {
            while (fatrdr.Read())
            {
                string id = fatrdr["id"].ToString();
                string imono = fatrdr["imono"].ToString();
                string gemiadi = fatrdr["gemiadi"].ToString();
                string tip = fatrdr["tip"].ToString();
                string kalkislimani = fatrdr["kalkislimani"].ToString();
                string kalkisrihtimi = fatrdr["kalkisrihtimi"].ToString();
                double loa = Convert.ToInt32(fatrdr["loa"].ToString());
                string nedurumdaopr = fatrdr["nedurumdaopr"].ToString();
                string kayitzamani = fatrdr["kayitzamani"].ToString();


                if (kalkislimani == "Kroman")
                {
                    if (kalkisrihtimi == "Q.1")
                    {
                        if (tip == "Tanker") { kroman1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { kroman1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { kroman1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { kroman1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { kroman1.ImageUrl = "images/vesb.png"; }
                        else { kroman1.ImageUrl = "images/veso.png"; }
                        kroman1.Visible = true; inportsay++;
                        kroman1.Style.Add("height", "auto");
                        kroman1.AlternateText = gemiadi + "/" + loa;
                        kroman1.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kroman1.Style.Add("top", ((33 + ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kroman1.Style.Add("left", ((84 - ((loa - 70) * 31 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        //kroman1.Style.Add("left", (((Convert.ToDouble(12400 / 1920) - ((loa - 70) * 0 / 100))).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        //kroman1.Style.Add("top",  (((Convert.ToDouble(30000 / 1080) - ((loa - 70) * 0 / 100))).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                    }
                    else if (kalkisrihtimi == "Q.2")
                    {
                        if (tip == "Tanker") { kroman2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { kroman2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { kroman2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { kroman2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { kroman2.ImageUrl = "images/vesb.png"; }
                        else { kroman2.ImageUrl = "images/veso.png"; }
                        kroman2.Visible = true; inportsay++;
                        kroman2.Style.Add("height", "auto");
                        kroman2.AlternateText = gemiadi + "/" + loa;
                        kroman2.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kroman2.Style.Add("top", ((52 + ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kroman2.Style.Add("left", ((58 - ((loa - 70) * 30 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.3")
                    {
                        if (tip == "Tanker") { kroman3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { kroman3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { kroman3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { kroman3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { kroman3.ImageUrl = "images/vesb.png"; }
                        else { kroman3.ImageUrl = "images/veso.png"; }
                        kroman3.Visible = true; inportsay++;
                        kroman3.Style.Add("height", "auto");
                        kroman3.AlternateText = gemiadi + "/" + loa;
                        kroman3.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kroman3.Style.Add("top", ((70 + ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kroman3.Style.Add("left", ((33 - ((loa - 70) * 30 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.4")
                    {
                        if (tip == "Tanker") { kroman4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { kroman4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { kroman4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { kroman4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { kroman4.ImageUrl = "images/vesb.png"; }
                        else { kroman4.ImageUrl = "images/veso.png"; }
                        kroman4.Visible = true; inportsay++;
                        kroman4.Style.Add("height", "auto");
                        kroman4.AlternateText = gemiadi + "/" + loa;
                        kroman4.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kroman4.Style.Add("top", ((92 - ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kroman4.Style.Add("left", ((3 - ((loa - 70) * 5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.Btwn")
                    {
                        if (tip == "Tanker") { kroman5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { kroman5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { kroman5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { kroman5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { kroman5.ImageUrl = "images/vesb.png"; }
                        else { kroman5.ImageUrl = "images/veso.png"; }
                        kroman5.Visible = true; inportsay++;
                        kroman5.Style.Add("height", "auto");
                        kroman5.AlternateText = gemiadi + "/" + loa;
                        kroman5.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kroman5.Style.Add("top", ((81 - ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kroman5.Style.Add("left", ((18 - ((loa - 70) * 5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "Diler")
                {
                    if (kalkisrihtimi == "Q.1")
                    {
                        if (tip == "Tanker") { diler1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { diler1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { diler1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { diler1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { diler1.ImageUrl = "images/vesb.png"; }
                        else { diler1.ImageUrl = "images/veso.png"; }
                        diler1.Visible = true; inportsay++;
                        diler1.Style.Add("height", "auto");
                        diler1.AlternateText = gemiadi + "/" + loa;
                        diler1.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler1.Style.Add("top", ((25 + ((loa - 70) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler1.Style.Add("left", ((275 - ((loa - 70) * 21 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.2")
                    {
                        if (tip == "Tanker") { diler2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { diler2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { diler2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { diler2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { diler2.ImageUrl = "images/vesb.png"; }
                        else { diler2.ImageUrl = "images/veso.png"; }
                        diler2.Visible = true; inportsay++;
                        diler2.Style.Add("height", "auto");
                        diler2.AlternateText = gemiadi + "/" + loa;
                        diler2.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler2.Style.Add("top", ((45 - ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler2.Style.Add("left", ((240 - ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.3")
                    {
                        if (tip == "Tanker") { diler3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { diler3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { diler3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { diler3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { diler3.ImageUrl = "images/vesb.png"; }
                        else { diler3.ImageUrl = "images/veso.png"; }
                        diler3.Visible = true; inportsay++;
                        diler3.Style.Add("height", "auto");
                        diler3.AlternateText = gemiadi + "/" + loa;
                        diler3.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler3.Style.Add("top", ((66 - ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler3.Style.Add("left", ((203 - ((loa - 70) * 7 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.4.in")
                    {
                        if (tip == "Tanker") { diler4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { diler4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { diler4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { diler4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { diler4.ImageUrl = "images/vesb.png"; }
                        else { diler4.ImageUrl = "images/veso.png"; }
                        diler4.Visible = true; inportsay++;
                        diler4.Style.Add("height", "auto");
                        diler4.AlternateText = gemiadi + "/" + loa;
                        diler4.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler4.Style.Add("top", ((56 - ((loa - 70) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler4.Style.Add("left", ((198 - ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.5.in")
                    {
                        if (tip == "Tanker") { diler5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { diler5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { diler5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { diler5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { diler5.ImageUrl = "images/vesb.png"; }
                        else { diler5.ImageUrl = "images/veso.png"; }
                        diler5.Visible = true; inportsay++;
                        diler5.Style.Add("height", "auto");
                        diler5.AlternateText = gemiadi + "/" + loa;
                        diler5.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler5.Style.Add("top", ((47 + ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler5.Style.Add("left", ((200 - ((loa - 70) * 41 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.6")
                    {
                        if (tip == "Tanker") { diler6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { diler6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { diler6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { diler6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { diler6.ImageUrl = "images/vesb.png"; }
                        else { diler6.ImageUrl = "images/veso.png"; }
                        diler6.Visible = true; inportsay++;
                        diler6.Style.Add("height", "auto");
                        diler6.AlternateText = gemiadi + "/" + loa;
                        diler6.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler6.Style.Add("top", ((60 + ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler6.Style.Add("left", ((157 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.7")
                    {
                        if (tip == "Tanker") { diler7.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { diler7.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { diler7.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { diler7.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { diler7.ImageUrl = "images/vesb.png"; }
                        else { diler7.ImageUrl = "images/veso.png"; }
                        diler7.Visible = true; inportsay++;
                        diler7.Style.Add("height", "auto");
                        diler7.AlternateText = gemiadi + "/" + loa;
                        diler7.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler7.Style.Add("top", ((91 - ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler7.Style.Add("left", ((105 - ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.6,5")
                    {
                        if (tip == "Tanker") { diler8.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { diler8.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { diler8.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { diler8.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { diler8.ImageUrl = "images/vesb.png"; }
                        else { diler8.ImageUrl = "images/veso.png"; }
                        diler8.Visible = true; inportsay++;
                        diler8.Style.Add("height", "auto");
                        diler8.AlternateText = gemiadi + "/" + loa;
                        diler8.Style.Add("max-width", ((loa / 45).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler8.Style.Add("top", ((76 - ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler8.Style.Add("left", ((130 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                }
                if (kalkislimani == "Nuh Çimento")
                {
                    if (kalkisrihtimi == "Q.1")
                    {
                        if (tip == "Tanker") { nuh1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { nuh1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { nuh1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { nuh1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { nuh1.ImageUrl = "images/vesb.png"; }
                        else { nuh1.ImageUrl = "images/veso.png"; }
                        nuh1.Visible = true; inportsay++;
                        nuh1.Style.Add("height", "auto");
                        nuh1.AlternateText = gemiadi + "/" + loa;
                        nuh1.Style.Add("max-width", ((loa / 50).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        nuh1.Style.Add("top", ((111 - ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        nuh1.Style.Add("left", ((320 - ((loa - 70) * 5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.2")
                    {
                        if (tip == "Tanker") { nuh2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { nuh2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { nuh2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { nuh2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { nuh2.ImageUrl = "images/vesb.png"; }
                        else { nuh2.ImageUrl = "images/veso.png"; }
                        nuh2.Visible = true; inportsay++;
                        nuh2.Style.Add("height", "auto");
                        nuh2.AlternateText = gemiadi + "/" + loa;
                        nuh2.Style.Add("max-width", ((loa / 50).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        nuh2.Style.Add("top", ((58 - ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        nuh2.Style.Add("left", ((387 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.3")
                    {
                        if (tip == "Tanker") { nuh3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { nuh3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { nuh3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { nuh3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { nuh3.ImageUrl = "images/vesb.png"; }
                        else { nuh3.ImageUrl = "images/veso.png"; }
                        nuh3.Visible = true; inportsay++;
                        nuh3.Style.Add("height", "auto");
                        nuh3.AlternateText = gemiadi + "/" + loa;
                        nuh3.Style.Add("max-width", ((loa / 50).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        nuh3.Style.Add("top", ((26 + ((loa - 70) * 5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        nuh3.Style.Add("left", ((428 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.Btwn")
                    {
                        if (tip == "Tanker") { nuh4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { nuh4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { nuh4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { nuh4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { nuh4.ImageUrl = "images/vesb.png"; }
                        else { nuh4.ImageUrl = "images/veso.png"; }
                        nuh4.Visible = true; inportsay++;
                        nuh4.Style.Add("height", "auto");
                        nuh4.AlternateText = gemiadi + "/" + loa;
                        nuh4.Style.Add("max-width", ((loa / 50).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        nuh4.Style.Add("top", ((85 - ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        nuh4.Style.Add("left", ((352 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "Evyap")
                {
                    if (kalkisrihtimi == "P.1")
                    {
                        if (tip == "Tanker") { evyap1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap1.ImageUrl = "images/vesb.png"; }
                        else { evyap1.ImageUrl = "images/veso.png"; }
                        evyap1.Visible = true; inportsay++;
                        evyap1.Style.Add("height", "auto");
                        evyap1.AlternateText = gemiadi + "/" + loa;
                        evyap1.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap1.Style.Add("top", ((60 + ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap1.Style.Add("left", ((641 - ((loa - 70) * 21 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2A.iz")
                    {
                        if (tip == "Tanker") { evyap2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap2.ImageUrl = "images/vesb.png"; }
                        else { evyap2.ImageUrl = "images/veso.png"; }
                        evyap2.Visible = true; inportsay++;
                        evyap2.Style.Add("height", "auto");
                        evyap2.AlternateText = gemiadi + "/" + loa;
                        evyap2.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap2.Style.Add("top", ((63 + ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap2.Style.Add("left", ((633 - ((loa - 70) * 23.7 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2B.iz")
                    {
                        if (tip == "Tanker") { evyap3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap3.ImageUrl = "images/vesb.png"; }
                        else { evyap3.ImageUrl = "images/veso.png"; }
                        evyap3.Visible = true; inportsay++;
                        evyap3.Style.Add("height", "auto");
                        evyap3.AlternateText = gemiadi + "/" + loa;
                        evyap3.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap3.Style.Add("top", ((93 - ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap3.Style.Add("left", ((602 - ((loa - 70) * 16 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2C.iz")
                    {
                        if (tip == "Tanker") { evyap4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap4.ImageUrl = "images/vesb.png"; }
                        else { evyap4.ImageUrl = "images/veso.png"; }
                        evyap4.Visible = true; inportsay++;
                        evyap4.Style.Add("height", "auto");
                        evyap4.AlternateText = gemiadi + "/" + loa;
                        evyap4.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap4.Style.Add("top", ((131 - ((loa - 70) * 13 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap4.Style.Add("left", ((563 - ((loa - 70) * 2.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.3A.ist")
                    {
                        if (tip == "Tanker") { evyap5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap5.ImageUrl = "images/vesb.png"; }
                        else { evyap5.ImageUrl = "images/veso.png"; }
                        evyap5.Visible = true; inportsay++;
                        evyap5.Style.Add("height", "auto");
                        evyap5.AlternateText = gemiadi + "/" + loa;
                        evyap5.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap5.Style.Add("top", ((49 + ((loa - 70) * 4.6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap5.Style.Add("left", ((619 - ((loa - 70) * 27 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.3B.ist")
                    {
                        if (tip == "Tanker") { evyap6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap6.ImageUrl = "images/vesb.png"; }
                        else { evyap6.ImageUrl = "images/veso.png"; }
                        evyap6.Visible = true; inportsay++;
                        evyap6.Style.Add("height", "auto");
                        evyap6.AlternateText = gemiadi + "/" + loa;
                        evyap6.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap6.Style.Add("top", ((79 - ((loa - 70) * 1.3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap6.Style.Add("left", ((588 - ((loa - 70) * 21 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.3C.ist")
                    {
                        if (tip == "Tanker") { evyap7.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap7.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap7.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap7.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap7.ImageUrl = "images/vesb.png"; }
                        else { evyap7.ImageUrl = "images/veso.png"; }
                        evyap7.Visible = true; inportsay++;
                        evyap7.Style.Add("height", "auto");
                        evyap7.AlternateText = gemiadi + "/" + loa;
                        evyap7.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap7.Style.Add("top", ((116 - ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap7.Style.Add("left", ((549 - ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.4")
                    {
                        if (tip == "Tanker") { evyap8.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap8.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap8.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap8.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap8.ImageUrl = "images/vesb.png"; }
                        else { evyap8.ImageUrl = "images/veso.png"; }
                        evyap8.Visible = true; inportsay++;
                        evyap8.Style.Add("height", "auto");
                        evyap8.AlternateText = gemiadi + "/" + loa;
                        evyap8.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap8.Style.Add("top", ((28 + ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap8.Style.Add("left", ((602 - ((loa - 70) * 27 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.5")
                    {
                        if (tip == "Tanker") { evyap9.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap9.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap9.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap9.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap9.ImageUrl = "images/vesb.png"; }
                        else { evyap9.ImageUrl = "images/veso.png"; }
                        evyap9.Visible = true; inportsay++;
                        evyap9.Style.Add("height", "auto");
                        evyap9.AlternateText = gemiadi + "/" + loa;
                        evyap9.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap9.Style.Add("top", ((40 - ((loa - 70) * 0.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap9.Style.Add("left", ((550 - ((loa - 70) * 16 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.6")
                    {
                        if (tip == "Tanker") { evyap10.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap10.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap10.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap10.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap10.ImageUrl = "images/vesb.png"; }
                        else { evyap10.ImageUrl = "images/veso.png"; }
                        evyap10.Visible = true; inportsay++;
                        evyap10.Style.Add("height", "auto");
                        evyap10.AlternateText = gemiadi + "/" + loa;
                        evyap10.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap10.Style.Add("top", ((55 - ((loa - 70) * 3.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap10.Style.Add("left", ((480 - ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.Btwn")
                    {
                        if (tip == "Tanker") { evyap11.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { evyap11.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { evyap11.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { evyap11.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { evyap11.ImageUrl = "images/vesb.png"; }
                        else { evyap11.ImageUrl = "images/veso.png"; }
                        evyap11.Visible = true; inportsay++;
                        evyap11.Style.Add("height", "auto");
                        evyap11.AlternateText = gemiadi + "/" + loa;
                        evyap11.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        evyap11.Style.Add("top", ((47 - ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        evyap11.Style.Add("left", ((516 - ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Gübretaş")
                {
                    if (kalkisrihtimi == "Q.1")
                    {
                        if (tip == "Tanker") { gubre1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { gubre1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { gubre1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { gubre1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { gubre1.ImageUrl = "images/vesb.png"; }
                        else { gubre1.ImageUrl = "images/veso.png"; }
                        gubre1.Visible = true; inportsay++;
                        gubre1.Style.Add("height", "auto");
                        gubre1.AlternateText = gemiadi + "/" + loa;
                        gubre1.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        gubre1.Style.Add("top", ((53 - ((loa - 70) * 0.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        gubre1.Style.Add("left", ((690 - ((loa - 70) * 16 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        gubre1.Style.Add("top", ((67 - ((loa - 70) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        gubre1.Style.Add("left", ((754 - ((loa - 70) * 22 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                    else if (kalkisrihtimi == "Q.2")
                    {
                        if (tip == "Tanker") { gubre2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { gubre2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { gubre2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { gubre2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { gubre2.ImageUrl = "images/vesb.png"; }
                        else { gubre2.ImageUrl = "images/veso.png"; }
                        gubre2.Visible = true; inportsay++;
                        gubre2.Style.Add("height", "auto");
                        gubre2.AlternateText = gemiadi + "/" + loa;
                        gubre2.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        gubre2.Style.Add("top", ((56 - ((loa - 70) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        gubre2.Style.Add("left", ((720 - ((loa - 70) * 28 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.3")
                    {
                        if (tip == "Tanker") { gubre3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { gubre3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { gubre3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { gubre3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { gubre3.ImageUrl = "images/vesb.png"; }
                        else { gubre3.ImageUrl = "images/veso.png"; }
                        gubre3.Visible = true; inportsay++;
                        gubre3.Style.Add("height", "auto");
                        gubre3.AlternateText = gemiadi + "/" + loa;
                        gubre3.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        gubre3.Style.Add("top", ((45 + ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        gubre3.Style.Add("left", ((684 - ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                }
                if (kalkislimani == "Turkuaz")
                {
                    if (kalkisrihtimi == "P.1.Dbl.Anc")
                    {
                        if (tip == "Tanker") { turkuaz.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { turkuaz.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { turkuaz.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { turkuaz.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { turkuaz.ImageUrl = "images/vesb.png"; }
                        else { turkuaz.ImageUrl = "images/veso.png"; }
                        turkuaz.Visible = true; inportsay++;
                        turkuaz.Style.Add("height", "auto");
                        turkuaz.AlternateText = gemiadi + "/" + loa;
                        turkuaz.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        turkuaz.Style.Add("top", ((97 + ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        turkuaz.Style.Add("left", ((776 - ((loa - 70) * 23 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "Marmara Tersanesi")
                {
                    if (kalkisrihtimi == "P.1.Dbl.Anc")
                    {
                        if (tip == "Tanker") { marmara.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { marmara.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { marmara.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { marmara.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { marmara.ImageUrl = "images/vesb.png"; }
                        else { marmara.ImageUrl = "images/veso.png"; }
                        marmara.Visible = true; inportsay++;
                        marmara.Style.Add("height", "auto");
                        marmara.AlternateText = gemiadi + "/" + loa;
                        marmara.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        marmara.Style.Add("top", ((104 + ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        marmara.Style.Add("left", ((798 - ((loa - 70) * 22 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "Rota")
                {
                    if (kalkisrihtimi == "Q.1")
                    {
                        if (tip == "Tanker") { rota1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { rota1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { rota1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { rota1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { rota1.ImageUrl = "images/vesb.png"; }
                        else { rota1.ImageUrl = "images/veso.png"; }
                        rota1.Visible = true; inportsay++;
                        rota1.Style.Add("height", "auto");
                        rota1.AlternateText = gemiadi + "/" + loa;
                        rota1.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        rota1.Style.Add("top", ((44 + ((loa - 70) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        rota1.Style.Add("left", ((948 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2")
                    {
                        if (tip == "Tanker") { rota2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { rota2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { rota2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { rota2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { rota2.ImageUrl = "images/vesb.png"; }
                        else { rota2.ImageUrl = "images/veso.png"; }
                        rota2.Visible = true; inportsay++;
                        rota2.Style.Add("height", "auto");
                        rota2.AlternateText = gemiadi + "/" + loa;
                        rota2.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        rota2.Style.Add("top", ((68 + ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        rota2.Style.Add("left", ((923 - ((loa - 70) * 14 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.3")
                    {
                        if (tip == "Tanker") { rota3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { rota3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { rota3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { rota3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { rota3.ImageUrl = "images/vesb.png"; }
                        else { rota3.ImageUrl = "images/veso.png"; }
                        rota3.Visible = true; inportsay++;
                        rota3.Style.Add("height", "auto");
                        rota3.AlternateText = gemiadi + "/" + loa;
                        rota3.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        rota3.Style.Add("top", ((110 - ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        rota3.Style.Add("left", ((929 - ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.4")
                    {
                        if (tip == "Tanker") { rota4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { rota4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { rota4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { rota4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { rota4.ImageUrl = "images/vesb.png"; }
                        else { rota4.ImageUrl = "images/veso.png"; }
                        rota4.Visible = true; inportsay++;
                        rota4.Style.Add("height", "auto");
                        rota4.AlternateText = gemiadi + "/" + loa;
                        rota4.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        rota4.Style.Add("top", ((114 - ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        rota4.Style.Add("left", ((909 - ((loa - 70) * 24 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.5")
                    {
                        if (tip == "Tanker") { rota5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { rota5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { rota5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { rota5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { rota5.ImageUrl = "images/vesb.png"; }
                        else { rota5.ImageUrl = "images/veso.png"; }
                        rota5.Visible = true; inportsay++;
                        rota5.Style.Add("height", "auto");
                        rota5.AlternateText = gemiadi + "/" + loa;
                        rota5.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        rota5.Style.Add("top", ((74 + ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        rota5.Style.Add("left", ((904 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.6")
                    {
                        if (tip == "Tanker") { rota6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { rota6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { rota6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { rota6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { rota6.ImageUrl = "images/vesb.png"; }
                        else { rota6.ImageUrl = "images/veso.png"; }
                        rota6.Visible = true; inportsay++;
                        rota6.Style.Add("height", "auto");
                        rota6.AlternateText = gemiadi + "/" + loa;
                        rota6.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        rota6.Style.Add("top", ((46 + ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        rota6.Style.Add("left", ((882 - ((loa - 70) * 21 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                }
                if (kalkislimani == "Milangaz")
                {
                    if (kalkisrihtimi == "Buoys")
                    {
                        if (tip == "Tanker") { milangaz.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { milangaz.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { milangaz.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { milangaz.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { milangaz.ImageUrl = "images/vesb.png"; }
                        else { milangaz.ImageUrl = "images/veso.png"; }
                        milangaz.Visible = true; inportsay++;
                        milangaz.Style.Add("height", "auto");
                        milangaz.AlternateText = gemiadi + "/" + loa;
                        milangaz.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        milangaz.Style.Add("top", ((72 + ((loa - 70) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        milangaz.Style.Add("left", ((1038 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "İgsaş")
                {
                    if (kalkisrihtimi == "P.1")
                    {
                        if (tip == "Tanker") { igsas1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { igsas1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { igsas1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { igsas1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { igsas1.ImageUrl = "images/vesb.png"; }
                        else { igsas1.ImageUrl = "images/veso.png"; }
                        igsas1.Visible = true; inportsay++;
                        igsas1.Style.Add("height", "auto");
                        igsas1.AlternateText = gemiadi + "/" + loa;
                        igsas1.Style.Add("max-width", ((loa / 36).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        igsas1.Style.Add("top", ((117 - ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        igsas1.Style.Add("left", ((1220 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2")
                    {
                        if (tip == "Tanker") { igsas2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { igsas2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { igsas2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { igsas2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { igsas2.ImageUrl = "images/vesb.png"; }
                        else { igsas2.ImageUrl = "images/veso.png"; }
                        igsas2.Visible = true; inportsay++;
                        igsas2.Style.Add("height", "auto");
                        igsas2.AlternateText = gemiadi + "/" + loa;
                        igsas2.Style.Add("max-width", ((loa / 36).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        igsas2.Style.Add("top", ((117 + ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        igsas2.Style.Add("left", ((1175 - ((loa - 70) * 32 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.3")
                    {
                        if (tip == "Tanker") { igsas3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { igsas3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { igsas3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { igsas3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { igsas3.ImageUrl = "images/vesb.png"; }
                        else { igsas3.ImageUrl = "images/veso.png"; }
                        igsas3.Visible = true; inportsay++;
                        igsas3.Style.Add("height", "auto");
                        igsas3.AlternateText = gemiadi + "/" + loa;
                        igsas3.Style.Add("max-width", ((loa / 36).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        igsas3.Style.Add("top", ((117 - ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        igsas3.Style.Add("left", ((1128 - ((loa - 70) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.4")
                    {
                        if (tip == "Tanker") { igsas4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { igsas4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { igsas4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { igsas4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { igsas4.ImageUrl = "images/vesb.png"; }
                        else { igsas4.ImageUrl = "images/veso.png"; }
                        igsas4.Visible = true; inportsay++;
                        igsas4.Style.Add("height", "auto");
                        igsas4.AlternateText = gemiadi + "/" + loa;
                        igsas4.Style.Add("max-width", ((loa / 36).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        igsas4.Style.Add("top", ((97 - ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        igsas4.Style.Add("left", ((1130 - ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.5")
                    {
                        if (tip == "Tanker") { igsas5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { igsas5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { igsas5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { igsas5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { igsas5.ImageUrl = "images/vesb.png"; }
                        else { igsas5.ImageUrl = "images/veso.png"; }
                        igsas5.Visible = true; inportsay++;
                        igsas5.Style.Add("height", "auto");
                        igsas5.AlternateText = gemiadi + "/" + loa;
                        igsas5.Style.Add("max-width", ((loa / 36).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        igsas5.Style.Add("top", ((97 - ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        igsas5.Style.Add("left", ((1200 - ((loa - 70) * 25 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.6")
                    {
                        if (tip == "Tanker") { igsas6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { igsas6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { igsas6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { igsas6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { igsas6.ImageUrl = "images/vesb.png"; }
                        else { igsas6.ImageUrl = "images/veso.png"; }
                        igsas6.Visible = true; inportsay++;
                        igsas6.Style.Add("height", "auto");
                        igsas6.AlternateText = gemiadi + "/" + loa;
                        igsas6.Style.Add("max-width", ((loa / 36).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        igsas6.Style.Add("top", ((72 - ((loa - 70) * 14 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        igsas6.Style.Add("left", ((1231 - ((loa - 70) * 25 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.7")
                    {
                        if (tip == "Tanker") { igsas7.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { igsas7.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { igsas7.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { igsas7.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { igsas7.ImageUrl = "images/vesb.png"; }
                        else { igsas7.ImageUrl = "images/veso.png"; }
                        igsas7.Visible = true; inportsay++;
                        igsas7.Style.Add("height", "auto");
                        igsas7.AlternateText = gemiadi + "/" + loa;
                        igsas7.Style.Add("max-width", ((loa / 36).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        igsas7.Style.Add("top", ((22 + ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        igsas7.Style.Add("left", ((1212 - ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "DubaiPort")
                {
                    if (kalkisrihtimi == "Q.1.ist")
                    {
                        if (tip == "Tanker") { dp1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { dp1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { dp1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { dp1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { dp1.ImageUrl = "images/vesb.png"; }
                        else { dp1.ImageUrl = "images/veso.png"; }
                        dp1.Visible = true; inportsay++;
                        dp1.Style.Add("height", "auto");
                        dp1.AlternateText = gemiadi + "/" + loa;
                        dp1.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        dp1.Style.Add("top", ((259 + ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        dp1.Style.Add("left", ((21 - ((loa - 70) * 9.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.1.iz")
                    {
                        if (tip == "Tanker") { dp2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { dp2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { dp2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { dp2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { dp2.ImageUrl = "images/vesb.png"; }
                        else { dp2.ImageUrl = "images/veso.png"; }
                        dp2.Visible = true; inportsay++;
                        dp2.Style.Add("height", "auto");
                        dp2.AlternateText = gemiadi + "/" + loa;
                        dp2.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        dp2.Style.Add("top", ((341 - ((loa - 70) * 14.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        dp2.Style.Add("left", ((77 - ((loa - 70) * 27 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.1.Btwn")
                    {
                        if (tip == "Tanker") { dp3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { dp3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { dp3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { dp3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { dp3.ImageUrl = "images/vesb.png"; }
                        else { dp3.ImageUrl = "images/veso.png"; }
                        dp3.Visible = true; inportsay++;
                        dp3.Style.Add("height", "auto");
                        dp3.AlternateText = gemiadi + "/" + loa;
                        dp3.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        dp3.Style.Add("top", ((300 - ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        dp3.Style.Add("left", ((49 - ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.2.A")
                    {
                        if (tip == "Tanker") { dp4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { dp4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { dp4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { dp4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { dp4.ImageUrl = "images/vesb.png"; }
                        else { dp4.ImageUrl = "images/veso.png"; }
                        dp4.Visible = true; inportsay++;
                        dp4.Style.Add("height", "auto");
                        dp4.AlternateText = gemiadi + "/" + loa;
                        dp4.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        dp4.Style.Add("top", ((180 + ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        dp4.Style.Add("left", ((156 - ((loa - 70) * 33 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.2.Btwn")
                    {
                        if (tip == "Tanker") { dp5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { dp5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { dp5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { dp5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { dp5.ImageUrl = "images/vesb.png"; }
                        else { dp5.ImageUrl = "images/veso.png"; }
                        dp5.Visible = true; inportsay++;
                        dp5.Style.Add("height", "auto");
                        dp5.AlternateText = gemiadi + "/" + loa;
                        dp5.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        dp5.Style.Add("top", ((207 - ((loa - 70) * 5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        dp5.Style.Add("left", ((94 - ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.2.B")
                    {
                        if (tip == "Tanker") { dp6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { dp6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { dp6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { dp6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { dp6.ImageUrl = "images/vesb.png"; }
                        else { dp6.ImageUrl = "images/veso.png"; }
                        dp6.Visible = true; inportsay++;
                        dp6.Style.Add("height", "auto");
                        dp6.AlternateText = gemiadi + "/" + loa;
                        dp6.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        dp6.Style.Add("top", ((237 - ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        dp6.Style.Add("left", ((28 - ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                }

                if (kalkislimani == "Oyakport")
                {
                    if (kalkisrihtimi == "Q.1")
                    {
                        if (tip == "Tanker") { oyak1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { oyak1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { oyak1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { oyak1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { oyak1.ImageUrl = "images/vesb.png"; }
                        else { oyak1.ImageUrl = "images/veso.png"; }
                        oyak1.Visible = true; inportsay++;
                        oyak1.Style.Add("height", "auto");
                        oyak1.AlternateText = gemiadi + "/" + loa;
                        oyak1.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        oyak1.Style.Add("top", ((316 + ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        oyak1.Style.Add("left", ((220 - ((loa - 70) * 29 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.2")
                    {
                        if (tip == "Tanker") { oyak2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { oyak2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { oyak2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { oyak2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { oyak2.ImageUrl = "images/vesb.png"; }
                        else { oyak2.ImageUrl = "images/veso.png"; }
                        oyak2.Visible = true; inportsay++;
                        oyak2.Style.Add("height", "auto");
                        oyak2.AlternateText = gemiadi + "/" + loa;
                        oyak2.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        oyak2.Style.Add("top", ((341 + ((loa - 70) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        oyak2.Style.Add("left", ((167 - ((loa - 70) * 25 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.3")
                    {
                        if (tip == "Tanker") { oyak3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { oyak3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { oyak3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { oyak3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { oyak3.ImageUrl = "images/vesb.png"; }
                        else { oyak3.ImageUrl = "images/veso.png"; }
                        oyak3.Visible = true; inportsay++;
                        oyak3.Style.Add("height", "auto");
                        oyak3.AlternateText = gemiadi + "/" + loa;
                        oyak3.Style.Add("max-width", ((loa / 40).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        oyak3.Style.Add("top", ((367 - ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        oyak3.Style.Add("left", ((112 - ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Tüpraş Faz1")
                {
                    if (kalkisrihtimi == "P.A")
                    {
                        if (tip == "Tanker") { tupf11.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf11.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf11.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf11.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf11.ImageUrl = "images/vesb.png"; }
                        else { tupf11.ImageUrl = "images/veso.png"; }
                        tupf11.Visible = true; inportsay++;
                        tupf11.Style.Add("height", "auto");
                        tupf11.AlternateText = gemiadi + "/" + loa;
                        tupf11.Style.Add("max-width", ((loa / 35).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf11.Style.Add("top", ((222 - ((loa - 70) * 7 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf11.Style.Add("left", ((352 - ((loa - 70) * 24 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.B")
                    {
                        if (tip == "Tanker") { tupf12.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf12.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf12.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf12.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf12.ImageUrl = "images/vesb.png"; }
                        else { tupf12.ImageUrl = "images/veso.png"; }
                        tupf12.Visible = true; inportsay++;
                        tupf12.Style.Add("height", "auto");
                        tupf12.AlternateText = gemiadi + "/" + loa;
                        tupf12.Style.Add("max-width", ((loa / 35).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf12.Style.Add("top", ((213 - ((loa - 70) * 7 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf12.Style.Add("left", ((310 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.C")
                    {
                        if (tip == "Tanker") { tupf13.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf13.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf13.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf13.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf13.ImageUrl = "images/vesb.png"; }
                        else { tupf13.ImageUrl = "images/veso.png"; }
                        tupf13.Visible = true; inportsay++;
                        tupf13.Style.Add("height", "auto");
                        tupf13.AlternateText = gemiadi + "/" + loa;
                        tupf13.Style.Add("max-width", ((loa / 35).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf13.Style.Add("top", ((204 - ((loa - 70) * 7 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf13.Style.Add("left", ((269 - ((loa - 70) * 22 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.D")
                    {
                        if (tip == "Tanker") { tupf14.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf14.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf14.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf14.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf14.ImageUrl = "images/vesb.png"; }
                        else { tupf14.ImageUrl = "images/veso.png"; }
                        tupf14.Visible = true; inportsay++;
                        tupf14.Style.Add("height", "auto");
                        tupf14.AlternateText = gemiadi + "/" + loa;
                        tupf14.Style.Add("max-width", ((loa / 35).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf14.Style.Add("top", ((227 + ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf14.Style.Add("left", ((266 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.in")
                    {
                        if (tip == "Tanker") { tupf15.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf15.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf15.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf15.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf15.ImageUrl = "images/vesb.png"; }
                        else { tupf15.ImageUrl = "images/veso.png"; }
                        tupf15.Visible = true; inportsay++;
                        tupf15.Style.Add("height", "auto");
                        tupf15.AlternateText = gemiadi + "/" + loa;
                        tupf15.Style.Add("max-width", ((loa / 35).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf15.Style.Add("top", ((236 - ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf15.Style.Add("left", ((404 - ((loa - 70) * 5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.out")
                    {
                        if (tip == "Tanker") { tupf16.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf16.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf16.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf16.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf16.ImageUrl = "images/vesb.png"; }
                        else { tupf16.ImageUrl = "images/veso.png"; }
                        tupf16.Visible = true; inportsay++;
                        tupf16.Style.Add("height", "auto");
                        tupf16.AlternateText = gemiadi + "/" + loa;
                        tupf16.Style.Add("max-width", ((loa / 35).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf16.Style.Add("top", ((256 + ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf16.Style.Add("left", ((390 - ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Tüpraş Faz2")
                {

                    if (kalkisrihtimi == "P.N")
                    {
                        if (tip == "Tanker") { tupf21.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf21.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf21.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf21.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf21.ImageUrl = "images/vesb.png"; }
                        else { tupf21.ImageUrl = "images/veso.png"; }
                        tupf21.Visible = true; inportsay++;
                        tupf21.Style.Add("height", "auto");
                        tupf21.AlternateText = gemiadi + "/" + loa;
                        tupf21.Style.Add("max-width", ((loa / 35).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf21.Style.Add("top", ((252 - ((loa - 70) * 7 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf21.Style.Add("left", ((511 - ((loa - 70) * 22 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.S")
                    {
                        if (tip == "Tanker") { tupf22.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf22.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf22.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf22.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf22.ImageUrl = "images/vesb.png"; }
                        else { tupf22.ImageUrl = "images/veso.png"; }
                        tupf22.Visible = true; inportsay++;
                        tupf22.Style.Add("height", "auto");
                        tupf22.AlternateText = gemiadi + "/" + loa;
                        tupf22.Style.Add("max-width", ((loa / 35).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf22.Style.Add("top", ((277 + ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf22.Style.Add("left", ((504 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "Tüpraş Platform")
                {
                    if (kalkisrihtimi == "P.1")
                    {
                        if (tip == "Tanker") { tupp.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupp.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupp.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupp.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupp.ImageUrl = "images/vesb.png"; }
                        else { tupp.ImageUrl = "images/veso.png"; }
                        tupp.Visible = true; inportsay++;
                        tupp.Style.Add("height", "auto");
                        tupp.AlternateText = gemiadi + "/" + loa;
                        tupp.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupp.Style.Add("top", ((349 - ((loa - 70) * 0.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupp.Style.Add("left", ((328 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "Tüpraş Faz3")
                {
                    if (kalkisrihtimi == "P.A")
                    {
                        if (tip == "Tanker") { tupf31.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf31.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf31.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf31.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf31.ImageUrl = "images/vesb.png"; }
                        else { tupf31.ImageUrl = "images/veso.png"; }
                        tupf31.Visible = true; inportsay++;
                        tupf31.Style.Add("height", "auto");
                        tupf31.AlternateText = gemiadi + "/" + loa;
                        tupf31.Style.Add("max-width", ((loa / 34).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf31.Style.Add("top", ((235 + ((loa - 70) * 0.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf31.Style.Add("left", ((830 - ((loa - 70) * 35 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.B")
                    {
                        if (tip == "Tanker") { tupf32.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf32.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf32.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf32.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf32.ImageUrl = "images/vesb.png"; }
                        else { tupf32.ImageUrl = "images/veso.png"; }
                        tupf32.Visible = true; inportsay++;
                        tupf32.Style.Add("height", "auto");
                        tupf32.AlternateText = gemiadi + "/" + loa;
                        tupf32.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf32.Style.Add("top", ((261 + ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf32.Style.Add("left", ((845 - ((loa - 70) * 33 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.C")
                    {
                        if (tip == "Tanker") { tupf33.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf33.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf33.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf33.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf33.ImageUrl = "images/vesb.png"; }
                        else { tupf33.ImageUrl = "images/veso.png"; }
                        tupf33.Visible = true; inportsay++;
                        tupf33.Style.Add("height", "auto");
                        tupf33.AlternateText = gemiadi + "/" + loa;
                        tupf33.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf33.Style.Add("top", ((270 + ((loa - 70) * 0.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf33.Style.Add("left", ((755 - ((loa - 70) * 35 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.D")
                    {
                        if (tip == "Tanker") { tupf34.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf34.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf34.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf34.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf34.ImageUrl = "images/vesb.png"; }
                        else { tupf34.ImageUrl = "images/veso.png"; }
                        tupf34.Visible = true; inportsay++;
                        tupf34.Style.Add("height", "auto");
                        tupf34.AlternateText = gemiadi + "/" + loa;
                        tupf34.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf34.Style.Add("top", ((295 + ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf34.Style.Add("left", ((770 - ((loa - 70) * 33 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.E")
                    {
                        if (tip == "Tanker") { tupf35.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf35.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf35.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf35.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf35.ImageUrl = "images/vesb.png"; }
                        else { tupf35.ImageUrl = "images/veso.png"; }
                        tupf35.Visible = true; inportsay++;
                        tupf35.Style.Add("height", "auto");
                        tupf35.AlternateText = gemiadi + "/" + loa;
                        tupf35.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf35.Style.Add("top", ((315 + ((loa - 70) * 0.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf35.Style.Add("left", ((670 - ((loa - 70) * 35 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.F")
                    {
                        if (tip == "Tanker") { tupf36.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf36.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf36.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf36.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf36.ImageUrl = "images/vesb.png"; }
                        else { tupf36.ImageUrl = "images/veso.png"; }
                        tupf36.Visible = true; inportsay++;
                        tupf36.Style.Add("height", "auto");
                        tupf36.AlternateText = gemiadi + "/" + loa;
                        tupf36.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf36.Style.Add("top", ((335 + ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf36.Style.Add("left", ((680 - ((loa - 70) * 25 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.G")
                    {
                        if (tip == "Tanker") { tupf37.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { tupf37.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { tupf37.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { tupf37.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { tupf37.ImageUrl = "images/vesb.png"; }
                        else { tupf37.ImageUrl = "images/veso.png"; }
                        tupf37.Visible = true; inportsay++;
                        tupf37.Style.Add("height", "auto");
                        tupf37.AlternateText = gemiadi + "/" + loa;
                        tupf37.Style.Add("max-width", ((loa / 33).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        tupf37.Style.Add("top", ((205 - ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        tupf37.Style.Add("left", ((839 - ((loa - 70) * 32 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "Poaş")
                {
                    if (kalkisrihtimi == "P.1.Beam.İst")
                    {
                        if (tip == "Tanker") { poas1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poas1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poas1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poas1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poas1.ImageUrl = "images/vesb.png"; }
                        else { poas1.ImageUrl = "images/veso.png"; }
                        poas1.Visible = true; inportsay++;
                        poas1.Style.Add("height", "auto");
                        poas1.AlternateText = gemiadi + "/" + loa;
                        poas1.Style.Add("max-width", ((loa / 34).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poas1.Style.Add("top", ((266 - ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poas1.Style.Add("left", ((973 - ((loa - 70) * 22 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.1.Beam.İzm")
                    {
                        if (tip == "Tanker") { poas2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poas2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poas2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poas2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poas2.ImageUrl = "images/vesb.png"; }
                        else { poas2.ImageUrl = "images/veso.png"; }
                        poas2.Visible = true; inportsay++;
                        poas2.Style.Add("height", "auto");
                        poas2.AlternateText = gemiadi + "/" + loa;
                        poas2.Style.Add("max-width", ((loa / 34).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poas2.Style.Add("top", ((240 - ((loa - 70) * 7 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poas2.Style.Add("left", ((983 - ((loa - 70) * 22 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.1.Dbl.Anc")
                    {
                        if (tip == "Tanker") { poas3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poas3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poas3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poas3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poas3.ImageUrl = "images/vesb.png"; }
                        else { poas3.ImageUrl = "images/veso.png"; }
                        poas3.Visible = true; inportsay++;
                        poas3.Style.Add("height", "auto");
                        poas3.AlternateText = gemiadi + "/" + loa;
                        poas3.Style.Add("max-width", ((loa / 34).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poas3.Style.Add("top", ((259 + ((loa - 70) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poas3.Style.Add("left", ((1000 + ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "Shell")
                {
                    if (kalkisrihtimi == "P.1.Dbl.Anc")
                    {
                        if (tip == "Tanker") { shell.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { shell.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { shell.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { shell.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { shell.ImageUrl = "images/vesb.png"; }
                        else { shell.ImageUrl = "images/veso.png"; }
                        shell.Visible = true; inportsay++;
                        shell.Style.Add("height", "auto");
                        shell.AlternateText = gemiadi + "/" + loa;
                        shell.Style.Add("max-width", ((loa / 38).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        shell.Style.Add("top", ((306 + ((loa - 70) * 14 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        shell.Style.Add("left", ((1120 - ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "Koruma")
                {
                    if (kalkisrihtimi == "P.1.ist")
                    {
                        if (tip == "Tanker") { koruma1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { koruma1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { koruma1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { koruma1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { koruma1.ImageUrl = "images/vesb.png"; }
                        else { koruma1.ImageUrl = "images/veso.png"; }
                        koruma1.Visible = true; inportsay++;
                        koruma1.Style.Add("height", "auto");
                        koruma1.AlternateText = gemiadi + "/" + loa;
                        koruma1.Style.Add("max-width", ((loa / 38).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        koruma1.Style.Add("top", ((245 - ((loa - 70) * 24 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        koruma1.Style.Add("left", ((1151 - ((loa - 70) * 31 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.1.iz")
                    {
                        if (tip == "Tanker") { koruma2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { koruma2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { koruma2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { koruma2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { koruma2.ImageUrl = "images/vesb.png"; }
                        else { koruma2.ImageUrl = "images/veso.png"; }
                        koruma2.Visible = true; inportsay++;
                        koruma2.Style.Add("height", "auto");
                        koruma2.AlternateText = gemiadi + "/" + loa;
                        koruma2.Style.Add("max-width", ((loa / 38).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        koruma2.Style.Add("top", ((237 - ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        koruma2.Style.Add("left", ((1167 - ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.1.Dbl.Anc")
                    {
                        if (tip == "Tanker") { koruma3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { koruma3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { koruma3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { koruma3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { koruma3.ImageUrl = "images/vesb.png"; }
                        else { koruma3.ImageUrl = "images/veso.png"; }
                        koruma3.Visible = true; inportsay++;
                        koruma3.Style.Add("height", "auto");
                        koruma3.AlternateText = gemiadi + "/" + loa;
                        koruma3.Style.Add("max-width", ((loa / 38).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        koruma3.Style.Add("top", ((280 + ((loa - 70) * 14 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        koruma3.Style.Add("left", ((1175 - ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Aktaş")
                {
                    if (kalkisrihtimi == "P.1.Dbl.Anc")
                    {
                        if (tip == "Tanker") { aktas.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { aktas.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { aktas.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { aktas.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { aktas.ImageUrl = "images/vesb.png"; }
                        else { aktas.ImageUrl = "images/veso.png"; }
                        aktas.Visible = true; inportsay++;
                        aktas.Style.Add("height", "auto");
                        aktas.AlternateText = gemiadi + "/" + loa;
                        aktas.Style.Add("max-width", ((loa / 38).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        aktas.Style.Add("top", ((240 + ((loa - 70) * 14 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        aktas.Style.Add("left", ((1245 - ((loa - 70) * 14 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Ford Otosan")
                {
                    if (kalkisrihtimi == "P.iz")
                    {
                        if (tip == "Tanker") { ford1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { ford1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { ford1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { ford1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { ford1.ImageUrl = "images/vesb.png"; }
                        else { ford1.ImageUrl = "images/veso.png"; }
                        ford1.Visible = true; inportsay++;
                        ford1.Style.Add("height", "auto");
                        ford1.AlternateText = gemiadi + "/" + loa;
                        ford1.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        ford1.Style.Add("top", ((488 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        ford1.Style.Add("left", ((932 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.ist")
                    {
                        if (tip == "Tanker") { ford2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { ford2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { ford2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { ford2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { ford2.ImageUrl = "images/vesb.png"; }
                        else { ford2.ImageUrl = "images/veso.png"; }
                        ford2.Visible = true; inportsay++;
                        ford2.Style.Add("height", "auto");
                        ford2.AlternateText = gemiadi + "/" + loa;
                        ford2.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        ford2.Style.Add("top", ((500 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        ford2.Style.Add("left", ((912 - ((loa - 70) * 27 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Autoport")
                {
                   if (kalkisrihtimi == "P.A.iz")
                    {
                        if (tip == "Tanker") { auto1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { auto1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { auto1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { auto1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { auto1.ImageUrl = "images/vesb.png"; }
                        else { auto1.ImageUrl = "images/veso.png"; }
                        auto1.Visible = true; inportsay++;
                        auto1.Style.Add("height", "auto");
                        auto1.AlternateText = gemiadi + "/" + loa;
                        auto1.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        auto1.Style.Add("top", ((552 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        auto1.Style.Add("left", ((1008 - ((loa - 70) * 16 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.B.iz")
                    {
                        if (tip == "Tanker") { auto2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { auto2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { auto2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { auto2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { auto2.ImageUrl = "images/vesb.png"; }
                        else { auto2.ImageUrl = "images/veso.png"; }
                        auto2.Visible = true; inportsay++;
                        auto2.Style.Add("height", "auto");
                        auto2.AlternateText = gemiadi + "/" + loa;
                        auto2.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        auto2.Style.Add("top", ((452 + ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        auto2.Style.Add("left", ((1018 - ((loa - 70) * 19.5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.A.ist")
                    {
                        if (tip == "Tanker") { auto3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { auto3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { auto3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { auto3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { auto3.ImageUrl = "images/vesb.png"; }
                        else { auto3.ImageUrl = "images/veso.png"; }
                        auto3.Visible = true; inportsay++;
                        auto3.Style.Add("height", "auto");
                        auto3.AlternateText = gemiadi + "/" + loa;
                        auto3.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        auto3.Style.Add("top", ((550 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        auto3.Style.Add("left", ((977 - ((loa - 70) * 23 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.B.ist")
                    {
                        if (tip == "Tanker") { auto4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { auto4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { auto4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { auto4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { auto4.ImageUrl = "images/vesb.png"; }
                        else { auto4.ImageUrl = "images/veso.png"; }
                        auto4.Visible = true; inportsay++;
                        auto4.Style.Add("height", "auto");
                        auto4.AlternateText = gemiadi + "/" + loa;
                        auto4.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        auto4.Style.Add("top", ((448 + ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        auto4.Style.Add("left", ((988 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Limaş")
                {
                    if (kalkisrihtimi == "T.A.iz")
                    {
                        if (tip == "Tanker") { limas1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { limas1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { limas1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { limas1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { limas1.ImageUrl = "images/vesb.png"; }
                        else { limas1.ImageUrl = "images/veso.png"; }
                        limas1.Visible = true; inportsay++;
                        limas1.Style.Add("height", "auto");
                        limas1.AlternateText = gemiadi + "/" + loa;
                        limas1.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        limas1.Style.Add("top", ((484 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        limas1.Style.Add("left", ((1073 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "T.B.iz")
                    {
                        if (tip == "Tanker") { limas2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { limas2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { limas2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { limas2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { limas2.ImageUrl = "images/vesb.png"; }
                        else { limas2.ImageUrl = "images/veso.png"; }
                        limas2.Visible = true; inportsay++;
                        limas2.Style.Add("height", "auto");
                        limas2.AlternateText = gemiadi + "/" + loa;
                        limas2.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        limas2.Style.Add("top", ((442 + ((loa - 70) * 5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        limas2.Style.Add("left", ((1100 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "T.A.ist")
                    {
                        if (tip == "Tanker") { limas3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { limas3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { limas3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { limas3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { limas3.ImageUrl = "images/vesb.png"; }
                        else { limas3.ImageUrl = "images/veso.png"; }
                        limas3.Visible = true; inportsay++;
                        limas3.Style.Add("height", "auto");
                        limas3.AlternateText = gemiadi + "/" + loa;
                        limas3.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        limas3.Style.Add("top", ((473 - ((loa - 70) * 27 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        limas3.Style.Add("left", ((1056 - ((loa - 70) * 17 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "T.B.ist")
                    {
                        if (tip == "Tanker") { limas4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { limas4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { limas4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { limas4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { limas4.ImageUrl = "images/vesb.png"; }
                        else { limas4.ImageUrl = "images/veso.png"; }
                        limas4.Visible = true; inportsay++;
                        limas4.Style.Add("height", "auto");
                        limas4.AlternateText = gemiadi + "/" + loa;
                        limas4.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        limas4.Style.Add("top", ((430 + ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        limas4.Style.Add("left", ((1082 - ((loa - 70) * 34 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                    if (kalkisrihtimi == "B.A.iz")
                    {
                        if (tip == "Tanker") { limas5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { limas5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { limas5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { limas5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { limas5.ImageUrl = "images/vesb.png"; }
                        else { limas5.ImageUrl = "images/veso.png"; }
                        limas5.Visible = true; inportsay++;
                        limas5.Style.Add("height", "auto");
                        limas5.AlternateText = gemiadi + "/" + loa;
                        limas5.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        limas5.Style.Add("top", ((530 - ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        limas5.Style.Add("left", ((1124 - ((loa - 70) * 14 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "B.B.iz")
                    {
                        if (tip == "Tanker") { limas6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { limas6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { limas6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { limas6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { limas6.ImageUrl = "images/vesb.png"; }
                        else { limas6.ImageUrl = "images/veso.png"; }
                        limas6.Visible = true; inportsay++;
                        limas6.Style.Add("height", "auto");
                        limas6.AlternateText = gemiadi + "/" + loa;
                        limas6.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        limas6.Style.Add("top", ((470 + ((loa - 70) * 5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        limas6.Style.Add("left", ((1160 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "B.B.ist")
                    {
                        if (tip == "Tanker") { limas7.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { limas7.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { limas7.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { limas7.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { limas7.ImageUrl = "images/vesb.png"; }
                        else { limas7.ImageUrl = "images/veso.png"; }
                        limas7.Visible = true; inportsay++;
                        limas7.Style.Add("height", "auto");
                        limas7.AlternateText = gemiadi + "/" + loa;
                        limas7.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        limas7.Style.Add("top", ((453 + ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        limas7.Style.Add("left", ((1136 - ((loa - 70) * 34 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "B.A.ist")
                    {
                        if (tip == "Tanker") { limas8.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { limas8.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { limas8.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { limas8.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { limas8.ImageUrl = "images/vesb.png"; }
                        else { limas8.ImageUrl = "images/veso.png"; }
                        limas8.Visible = true; inportsay++;
                        limas8.Style.Add("height", "auto");
                        limas8.AlternateText = gemiadi + "/" + loa;
                        limas8.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        limas8.Style.Add("top", ((513 - ((loa - 70) * 17 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        limas8.Style.Add("left", ((1100 - ((loa - 70) * 23 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Kosbaş-Edgemar")
                {
                    if (kalkisrihtimi == "P.1")
                    {
                        if (tip == "Tanker") { kosbas1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { kosbas1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { kosbas1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { kosbas1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { kosbas1.ImageUrl = "images/vesb.png"; }
                        else { kosbas1.ImageUrl = "images/veso.png"; }
                        kosbas1.Visible = true; inportsay++;
                        kosbas1.Style.Add("height", "auto");
                        kosbas1.AlternateText = gemiadi + "/" + loa;
                        kosbas1.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kosbas1.Style.Add("top", ((488 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kosbas1.Style.Add("left", ((545 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2")
                    {
                        if (tip == "Tanker") { kosbas2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { kosbas2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { kosbas2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { kosbas2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { kosbas2.ImageUrl = "images/vesb.png"; }
                        else { kosbas2.ImageUrl = "images/veso.png"; }
                        kosbas2.Visible = true; inportsay++;
                        kosbas2.Style.Add("height", "auto");
                        kosbas2.AlternateText = gemiadi + "/" + loa;
                        kosbas2.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kosbas2.Style.Add("top", ((488 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kosbas2.Style.Add("left", ((560 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Kosbaş-Aemeda")
                {
                    if (kalkisrihtimi == "P.1")
                    {
                        if (tip == "Tanker") { kosbas3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { kosbas3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { kosbas3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { kosbas3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { kosbas3.ImageUrl = "images/vesb.png"; }
                        else { kosbas3.ImageUrl = "images/veso.png"; }
                        kosbas3.Visible = true; inportsay++;
                        kosbas3.Style.Add("height", "auto");
                        kosbas3.AlternateText = gemiadi + "/" + loa;
                        kosbas3.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kosbas3.Style.Add("top", ((488 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kosbas3.Style.Add("left", ((575 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2")
                    {
                        if (tip == "Tanker") { kosbas4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { kosbas4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { kosbas4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { kosbas4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { kosbas4.ImageUrl = "images/vesb.png"; }
                        else { kosbas4.ImageUrl = "images/veso.png"; }
                        kosbas4.Visible = true; inportsay++;
                        kosbas4.Style.Add("height", "auto");
                        kosbas4.AlternateText = gemiadi + "/" + loa;
                        kosbas4.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kosbas4.Style.Add("top", ((488 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kosbas4.Style.Add("left", ((590 - ((loa - 70) * 19 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }



//Kosbaş - Ak Yatçılık
//Kosbaş - Atlas
//Kosbaş - Çimtaş

//Kosbaş - Proteksan
//Kosbaş - Selah
//Kosbaş - Turquoise
//Kosbaş - TVK
//Kosbaş - Uzmar
//Kosbaş - Yatchley

//LİMANLARI 2 RIHTIMLO YAP

            }

        }
        fatrdr.Close();


        SqlCommand cmdmansayHerSay = new SqlCommand("SP_Isliste_NSL_HerSay", baglanti);
        cmdmansayHerSay.CommandType = CommandType.StoredProcedure;
        cmdmansayHerSay.Parameters.Add("@HerDemSay", SqlDbType.Int);
        cmdmansayHerSay.Parameters["@HerDemSay"].Direction = ParameterDirection.Output;
        cmdmansayHerSay.ExecuteNonQuery();
        string HerDemSay = cmdmansayHerSay.Parameters["@HerDemSay"].Value.ToString();
        cmdmansayHerSay.Dispose();

        SqlCommand cmdmansayYarSay = new SqlCommand("SP_Isliste_NSL_YarSay", baglanti);
        cmdmansayYarSay.CommandType = CommandType.StoredProcedure;
        cmdmansayYarSay.Parameters.Add("@YarSay", SqlDbType.Int);
        cmdmansayYarSay.Parameters["@YarSay"].Direction = ParameterDirection.Output;
        cmdmansayYarSay.ExecuteNonQuery();
        string YarSay = cmdmansayYarSay.Parameters["@YarSay"].Value.ToString();
        cmdmansayYarSay.Dispose();

        SqlCommand cmdmansayEskiSay = new SqlCommand("SP_Isliste_NSL_EskiSay", baglanti);
        cmdmansayEskiSay.CommandType = CommandType.StoredProcedure;
        cmdmansayEskiSay.Parameters.Add("@EskiSay", SqlDbType.Int);
        cmdmansayEskiSay.Parameters["@EskiSay"].Direction = ParameterDirection.Output;
        cmdmansayEskiSay.ExecuteNonQuery();
        string EskiSay = cmdmansayEskiSay.Parameters["@EskiSay"].Value.ToString();
        cmdmansayEskiSay.Dispose();

        SqlCommand cmdmansayizmitSay = new SqlCommand("SP_Isliste_NSL_izmitSay", baglanti);
        cmdmansayizmitSay.CommandType = CommandType.StoredProcedure;
        cmdmansayizmitSay.Parameters.Add("@izmitSay", SqlDbType.Int);
        cmdmansayizmitSay.Parameters["@izmitSay"].Direction = ParameterDirection.Output;
        cmdmansayizmitSay.ExecuteNonQuery();
        string izmitSay = cmdmansayizmitSay.Parameters["@izmitSay"].Value.ToString();
        cmdmansayizmitSay.Dispose();

        SqlCommand cmdmansaysipdSay = new SqlCommand("SP_Isliste_NSL_sipdsay", baglanti);
        cmdmansaysipdSay.CommandType = CommandType.StoredProcedure;
        cmdmansaysipdSay.Parameters.Add("@sipdSay", SqlDbType.Int);
        cmdmansaysipdSay.Parameters["@sipdSay"].Direction = ParameterDirection.Output;
        cmdmansaysipdSay.ExecuteNonQuery();
        string sipdSay = cmdmansaysipdSay.Parameters["@sipdSay"].Value.ToString();
        cmdmansaysipdSay.Dispose();

        SqlCommand cmdmansaysipySay = new SqlCommand("SP_Isliste_NSL_sipysay", baglanti);
        cmdmansaysipySay.CommandType = CommandType.StoredProcedure;
        cmdmansaysipySay.Parameters.Add("@sipySay", SqlDbType.Int);
        cmdmansaysipySay.Parameters["@sipySay"].Direction = ParameterDirection.Output;
        cmdmansaysipySay.ExecuteNonQuery();
        string sipySay = cmdmansaysipySay.Parameters["@sipySay"].Value.ToString();
        cmdmansaysipySay.Dispose();

        label1.Text = "In Ports: " + sipdSay;
        label2.Text = "Esk.Anch: " + EskiSay;

        label3.Text = "In Ports: " + sipySay;
        label4.Text = "Her.Anch: " + HerDemSay;
        label5.Text = "Yar.Anch: " + YarSay;
        label6.Text = "Izm.Anch: " + izmitSay;



        baglanti.Close();


    


}




protected void LBmapyarimca_Click(object sender, EventArgs e)
    {
        Response.Redirect("sipy.aspx");
    }



    protected void LBmapdarica_Click(object sender, EventArgs e)
    {
        Response.Redirect("sipd.aspx");
    }

   
}




