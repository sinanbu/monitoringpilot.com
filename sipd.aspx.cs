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

public partial class sipd: System.Web.UI.Page
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

        //// Register JavaScript which will collect the value and assign to HiddenField and trigger a postback
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SetHiddenField", "SetHiddenField();", true);

        //hh = height.Value;
        //ww = width.Value;

        //ww = width.Value.ToString();
        //hh = height.Value.ToString();

        //if (hh==""||hh==",,,,,,") {
        //    hh = "0";
        //    ww = "0"; }

    }


    private void DTloading()
    {
        aslan1.Visible = false;
        aslan2.Visible = false;
        aslan3.Visible = false;
        aslan4.Visible = false;

        belde1.Visible = false;
        belde2.Visible = false;
        belde3.Visible = false;
        belde4.Visible = false;

        poliport1.Visible = false;
        poliport2.Visible = false;
        poliport3.Visible = false;
        poliport4.Visible = false;
        poliport5.Visible = false;
        poliport6.Visible = false;
        poliport7.Visible = false;
        poliport8.Visible = false;
        poliport9.Visible = false;
        poliport10.Visible = false;

        colakoglu1.Visible = false;
        colakoglu2.Visible = false;
        colakoglu3.Visible = false;
        colakoglu4.Visible = false;
        colakoglu5.Visible = false;
        colakoglu6.Visible = false;
        colakoglu7.Visible = false;

        yilport1.Visible = false;
        yilport2.Visible = false;
        yilport3.Visible = false;
        yilport4.Visible = false;
        yilport5.Visible = false;
        yilport6.Visible = false;
        yilport7.Visible = false;
        yilport8.Visible = false;
        yilport9.Visible = false;
        yilport10.Visible = false;

        altintel1.Visible = false;
        altintel2.Visible = false;
        altintel3.Visible = false;

        solventas1.Visible = false;
        solventas2.Visible = false;
        solventas3.Visible = false;
        solventas4.Visible = false;
        solventas5.Visible = false;
        solventas6.Visible = false;
        solventas7.Visible = false;
        solventas8.Visible = false;

        efesan1.Visible = false;
        efesan2.Visible = false;
        efesan3.Visible = false;
        efesan4.Visible = false;
        efesan5.Visible = false;
        efesan6.Visible = false;

        generji1.Visible = false;
        generji2.Visible = false;
        generji3.Visible = false;

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



                if (kalkislimani == "Aslan Çimento")
                {

                    if (kalkisrihtimi == "P.1.out.ist")
                    {
                        if (tip == "Tanker") { aslan2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { aslan2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { aslan2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { aslan2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { aslan2.ImageUrl = "images/vesb.png"; }
                        else { aslan2.ImageUrl = "images/veso.png"; }
                        aslan2.Visible = true; inportsay++;
                        aslan2.Style.Add("height", "auto");
                        aslan2.Attributes.Add("title", gemiadi + "/" + loa);
                        aslan2.Style.Add("max-width", ((loa / 20).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        aslan2.Style.Add("top", ((585 + ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        aslan2.Style.Add("left", ((24 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");


                    }
                    else if (kalkisrihtimi == "P.1.out.iz")
                    {
                        if (tip == "Tanker") { aslan3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { aslan3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { aslan3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { aslan3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { aslan3.ImageUrl = "images/vesb.png"; }
                        else { aslan3.ImageUrl = "images/veso.png"; }
                        aslan3.Visible = true; inportsay++;
                        aslan3.Style.Add("height", "auto");
                        aslan3.Attributes.Add("title", gemiadi + "/" + loa);
                        aslan3.Style.Add("max-width", ((loa / 20).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        aslan3.Style.Add("top", ((625 - ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        aslan3.Style.Add("left", ((72 - ((loa - 70) * 54 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2.out.short")
                    {
                        if (tip == "Tanker") { aslan1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { aslan1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { aslan1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { aslan1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { aslan1.ImageUrl = "images/vesb.png"; }
                        else { aslan1.ImageUrl = "images/veso.png"; }
                        aslan1.Visible = true; inportsay++;
                        aslan1.Style.Add("height", "auto");
                        aslan1.Attributes.Add("title", gemiadi + "/" + loa);
                        aslan1.Style.Add("max-width", ((loa / 20).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        aslan1.Style.Add("top", ((545 + ((loa - 70) * 9 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        aslan1.Style.Add("left", ((20 - ((loa - 70) * 54 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                    else if (kalkisrihtimi == "P.3.in")
                    {
                        if (tip == "Tanker") { aslan4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { aslan4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { aslan4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { aslan4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { aslan4.ImageUrl = "images/vesb.png"; }
                        else { aslan4.ImageUrl = "images/veso.png"; }
                        aslan4.Visible = true; inportsay++;
                        aslan4.Style.Add("height", "auto");
                        aslan4.Attributes.Add("title", gemiadi + "/" + loa);
                        aslan4.Style.Add("max-width", ((loa / 20).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        aslan4.Style.Add("top", ((591 + ((loa - 100) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        aslan4.Style.Add("left", ((68 - ((loa - 100) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                }

                if (kalkislimani == "Belde Port")
                {
                    if (kalkisrihtimi == "Q.1")
                    {
                        if (tip == "Tanker") { belde1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { belde1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { belde1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { belde1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { belde1.ImageUrl = "images/vesb.png"; }
                        else { belde1.ImageUrl = "images/veso.png"; }
                        belde1.Visible = true; inportsay++;
                        belde1.Style.Add("height", "auto");
                        belde1.Attributes.Add("title", gemiadi + "/" + loa);
                        belde1.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        belde1.Style.Add("top", ((666 - ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        belde1.Style.Add("left", ((380 - ((loa - 70) * 41 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        //  belde1.Style.Add("top", (((Convert.ToDouble(66600/720)-((loa - 70) * 15 / 100))).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        //   belde1.Style.Add("left", (((Convert.ToDouble(38100/1280) - ((loa - 70) * 41 / 100))).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");

                    }

                    else if (kalkisrihtimi == "Q.2")
                    {
                        if (tip == "Tanker") { belde2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { belde2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { belde2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { belde2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { belde2.ImageUrl = "images/vesb.png"; }
                        else { belde2.ImageUrl = "images/veso.png"; }
                        belde2.Visible = true; inportsay++;
                        belde2.Style.Add("height", "auto");
                        belde2.Attributes.Add("title", gemiadi + "/" + loa);
                        belde2.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        belde2.Style.Add("top", ((612 - ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        belde2.Style.Add("left", ((335 - ((loa - 70) * 41 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.3")
                    {
                        if (tip == "Tanker") { belde3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { belde3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { belde3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { belde3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { belde3.ImageUrl = "images/vesb.png"; }
                        else { belde3.ImageUrl = "images/veso.png"; }
                        belde3.Visible = true; inportsay++;
                        belde3.Style.Add("height", "auto");
                        belde3.Attributes.Add("title", gemiadi + "/" + loa);
                        belde3.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        belde3.Style.Add("top", ((560 - ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        belde3.Style.Add("left", ((292 - ((loa - 70) * 41 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.4")
                    {
                        if (tip == "Tanker") { belde4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { belde4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { belde4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { belde4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { belde4.ImageUrl = "images/vesb.png"; }
                        else { belde4.ImageUrl = "images/veso.png"; }
                        belde4.Visible = true; inportsay++;
                        belde4.Style.Add("height", "auto");
                        belde4.Attributes.Add("title", gemiadi + "/" + loa);
                        belde4.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        belde4.Style.Add("top", ((500 + ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        belde4.Style.Add("left", ((242 - ((loa - 70) * 16 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }//22  17
                }
                if (kalkislimani == "Poliport")
                {
                    if (kalkisrihtimi == "P.T.1A.iz")
                    {
                        if (tip == "Tanker") { poliport1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poliport1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poliport1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poliport1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poliport1.ImageUrl = "images/vesb.png"; }
                        else { poliport1.ImageUrl = "images/veso.png"; }
                        poliport1.Visible = true; inportsay++;
                        poliport1.Style.Add("height", "auto");
                        poliport1.Attributes.Add("title", gemiadi + "/" + loa);
                        poliport1.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poliport1.Style.Add("top", ((254 + ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poliport1.Style.Add("left", ((216 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                    else if (kalkisrihtimi == "P.T.1B.iz")
                    {
                        if (tip == "Tanker") { poliport2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poliport2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poliport2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poliport2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poliport2.ImageUrl = "images/vesb.png"; }
                        else { poliport2.ImageUrl = "images/veso.png"; }
                        poliport2.Visible = true; inportsay++;
                        poliport2.Style.Add("height", "auto");
                        poliport2.Attributes.Add("title", gemiadi + "/" + loa);
                        poliport2.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poliport2.Style.Add("top", ((328 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poliport2.Style.Add("left", ((213 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                    else if (kalkisrihtimi == "P.T.2A.ist")
                    {
                        if (tip == "Tanker") { poliport3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poliport3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poliport3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poliport3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poliport3.ImageUrl = "images/vesb.png"; }
                        else { poliport3.ImageUrl = "images/veso.png"; }
                        poliport3.Visible = true; inportsay++;
                        poliport3.Style.Add("height", "auto");
                        poliport3.Attributes.Add("title", gemiadi + "/" + loa);
                        poliport3.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poliport3.Style.Add("top", ((260 + ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poliport3.Style.Add("left", ((183 - ((loa - 70) * 27 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                    else if (kalkisrihtimi == "P.T.2B.ist")
                    {
                        if (tip == "Tanker") { poliport4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poliport4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poliport4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poliport4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poliport4.ImageUrl = "images/vesb.png"; }
                        else { poliport4.ImageUrl = "images/veso.png"; }
                        poliport4.Visible = true; inportsay++;
                        poliport4.Style.Add("height", "auto");
                        poliport4.Attributes.Add("title", gemiadi + "/" + loa);
                        poliport4.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poliport4.Style.Add("top", ((322 - ((loa - 70) * 7 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poliport4.Style.Add("left", ((181 - ((loa - 70) * 27 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                    else if (kalkisrihtimi == "P.B.3A.iz")
                    {
                        if (tip == "Tanker") { poliport5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poliport5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poliport5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poliport5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poliport5.ImageUrl = "images/vesb.png"; }
                        else { poliport5.ImageUrl = "images/veso.png"; }
                        poliport5.Visible = true; inportsay++;
                        poliport5.Style.Add("height", "auto");
                        poliport5.Attributes.Add("title", gemiadi + "/" + loa);
                        poliport5.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poliport5.Style.Add("top", ((268 + ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poliport5.Style.Add("left", ((79 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                    else if (kalkisrihtimi == "P.B.3B.iz")
                    {
                        if (tip == "Tanker") { poliport6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poliport6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poliport6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poliport6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poliport6.ImageUrl = "images/vesb.png"; }
                        else { poliport6.ImageUrl = "images/veso.png"; }
                        poliport6.Visible = true; inportsay++;
                        poliport6.Style.Add("height", "auto");
                        poliport6.Attributes.Add("title", gemiadi + "/" + loa);
                        poliport6.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poliport6.Style.Add("top", ((332 - ((loa - 70) * 7 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poliport6.Style.Add("left", ((77 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                    else if (kalkisrihtimi == "P.B.4A.ist")
                    {
                        if (tip == "Tanker") { poliport7.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poliport7.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poliport7.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poliport7.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poliport7.ImageUrl = "images/vesb.png"; }
                        else { poliport7.ImageUrl = "images/veso.png"; }
                        poliport7.Visible = true; inportsay++;
                        poliport7.Style.Add("height", "auto");
                        poliport7.Attributes.Add("title", gemiadi + "/" + loa);
                        poliport7.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poliport7.Style.Add("top", ((125 + ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poliport7.Style.Add("left", ((45 - ((loa - 70) * 27 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                    else if (kalkisrihtimi == "P.B.4B.ist")
                    {
                        if (tip == "Tanker") { poliport8.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poliport8.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poliport8.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poliport8.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poliport8.ImageUrl = "images/vesb.png"; }
                        else { poliport8.ImageUrl = "images/veso.png"; }
                        poliport8.Visible = true; inportsay++;
                        poliport8.Style.Add("height", "auto");
                        poliport8.Attributes.Add("title", gemiadi + "/" + loa);
                        poliport8.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poliport8.Style.Add("top", ((197 + ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poliport8.Style.Add("left", ((42 - ((loa - 70) * 27 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                    else if (kalkisrihtimi == "P.B.4C.ist")
                    {
                        if (tip == "Tanker") { poliport9.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poliport9.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poliport9.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poliport9.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poliport9.ImageUrl = "images/vesb.png"; }
                        else { poliport9.ImageUrl = "images/veso.png"; }
                        poliport9.Visible = true; inportsay++;
                        poliport9.Style.Add("height", "auto");
                        poliport9.Attributes.Add("title", gemiadi + "/" + loa);
                        poliport9.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poliport9.Style.Add("top", ((268 + ((loa - 70) * 15 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poliport9.Style.Add("left", ((39 - ((loa - 70) * 28 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");

                    }
                    else if (kalkisrihtimi == "P.B.4D.ist")
                    {


                        if (tip == "Tanker") { poliport10.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { poliport10.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { poliport10.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { poliport10.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { poliport10.ImageUrl = "images/vesb.png"; }
                        else { poliport10.ImageUrl = "images/veso.png"; }
                        poliport10.Visible = true; inportsay++;
                        poliport10.Style.Add("height", "auto");
                        poliport10.Attributes.Add("title", gemiadi + "/" + loa);
                        poliport10.Style.Add("max-width", ((loa / 28).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        poliport10.Style.Add("top", ((339 - ((loa - 70) * 24 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        poliport10.Style.Add("left", ((35 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Çolakoğlu")
                {
                    if (kalkisrihtimi == "Q.2A")
                    {
                        if (tip == "Tanker") { colakoglu1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { colakoglu1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { colakoglu1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { colakoglu1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { colakoglu1.ImageUrl = "images/vesb.png"; }
                        else { colakoglu1.ImageUrl = "images/veso.png"; }
                        colakoglu1.Visible = true; inportsay++;
                        colakoglu1.Style.Add("height", "auto");
                        colakoglu1.Attributes.Add("title", gemiadi + "/" + loa);
                        colakoglu1.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        colakoglu1.Style.Add("top", ((248 + ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        colakoglu1.Style.Add("left", ((265 - ((loa - 70) * 31 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                    else if (kalkisrihtimi == "Q.2B")
                    {
                        if (tip == "Tanker") { colakoglu2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { colakoglu2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { colakoglu2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { colakoglu2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { colakoglu2.ImageUrl = "images/vesb.png"; }
                        else { colakoglu2.ImageUrl = "images/veso.png"; }
                        colakoglu2.Visible = true; inportsay++;
                        colakoglu2.Style.Add("height", "auto");
                        colakoglu2.Attributes.Add("title", gemiadi + "/" + loa);
                        colakoglu2.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        colakoglu2.Style.Add("top", ((305 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        colakoglu2.Style.Add("left", ((266 - ((loa - 70) * 31 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                    else if (kalkisrihtimi == "Q.2C")
                    {
                        if (tip == "Tanker") { colakoglu3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { colakoglu3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { colakoglu3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { colakoglu3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { colakoglu3.ImageUrl = "images/vesb.png"; }
                        else { colakoglu3.ImageUrl = "images/veso.png"; }
                        colakoglu3.Visible = true; inportsay++;
                        colakoglu3.Style.Add("height", "auto");
                        colakoglu3.Attributes.Add("title", gemiadi + "/" + loa);
                        colakoglu3.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        colakoglu3.Style.Add("top", ((360 - ((loa - 70) * 24 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        colakoglu3.Style.Add("left", ((268 - ((loa - 70) * 31 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.1.ist")
                    {
                        if (tip == "Tanker") { colakoglu4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { colakoglu4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { colakoglu4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { colakoglu4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { colakoglu4.ImageUrl = "images/vesb.png"; }
                        else { colakoglu4.ImageUrl = "images/veso.png"; }
                        colakoglu4.Visible = true; inportsay++;
                        colakoglu4.Style.Add("height", "auto");
                        colakoglu4.Attributes.Add("title", gemiadi + "/" + loa);
                        colakoglu4.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        colakoglu4.Style.Add("top", ((384 + ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        colakoglu4.Style.Add("left", ((290 + ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.1.Btwn.ist")
                    {
                        if (tip == "Tanker") { colakoglu5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { colakoglu5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { colakoglu5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { colakoglu5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { colakoglu5.ImageUrl = "images/vesb.png"; }
                        else { colakoglu5.ImageUrl = "images/veso.png"; }
                        colakoglu5.Visible = true; inportsay++;
                        colakoglu5.Style.Add("height", "auto");
                        colakoglu5.Attributes.Add("title", gemiadi + "/" + loa);
                        colakoglu5.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        colakoglu5.Style.Add("top", ((384 + ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        colakoglu5.Style.Add("left", ((353 - ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.1.Btwn.iz")
                    {
                        if (tip == "Tanker") { colakoglu6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { colakoglu6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { colakoglu6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { colakoglu6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { colakoglu6.ImageUrl = "images/vesb.png"; }
                        else { colakoglu6.ImageUrl = "images/veso.png"; }
                        colakoglu6.Visible = true; inportsay++;
                        colakoglu6.Style.Add("height", "auto");
                        colakoglu6.Attributes.Add("title", gemiadi + "/" + loa);
                        colakoglu6.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        colakoglu6.Style.Add("top", ((384 - ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        colakoglu6.Style.Add("left", ((416 - ((loa - 70) * 41 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.1.iz")
                    {
                        if (tip == "Tanker") { colakoglu7.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { colakoglu7.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { colakoglu7.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { colakoglu7.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { colakoglu7.ImageUrl = "images/vesb.png"; }
                        else { colakoglu7.ImageUrl = "images/veso.png"; }
                        colakoglu7.Visible = true; inportsay++;
                        colakoglu7.Style.Add("height", "auto");
                        colakoglu7.Attributes.Add("title", gemiadi + "/" + loa);
                        colakoglu7.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        colakoglu7.Style.Add("top", ((383 - ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        colakoglu7.Style.Add("left", ((479 - ((loa - 70) * 52 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Yılport")
                {
                    if (kalkisrihtimi == "Q.1A")
                    {
                        if (tip == "Tanker") { yilport1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { yilport1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { yilport1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { yilport1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { yilport1.ImageUrl = "images/vesb.png"; }
                        else { yilport1.ImageUrl = "images/veso.png"; }
                        yilport1.Visible = true; inportsay++;
                        yilport1.Style.Add("height", "auto");
                        yilport1.Attributes.Add("title", gemiadi + "/" + loa);
                        yilport1.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        yilport1.Style.Add("top", ((235 + ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        yilport1.Style.Add("left", ((501 - ((loa - 70) * 17 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                    else if (kalkisrihtimi == "Q.1B")
                    {
                        if (tip == "Tanker") { yilport2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { yilport2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { yilport2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { yilport2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { yilport2.ImageUrl = "images/vesb.png"; }
                        else { yilport2.ImageUrl = "images/veso.png"; }
                        yilport2.Visible = true; inportsay++;
                        yilport2.Style.Add("height", "auto");
                        yilport2.Attributes.Add("title", gemiadi + "/" + loa);
                        yilport2.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        yilport2.Style.Add("top", ((335 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        yilport2.Style.Add("left", ((519 - ((loa - 70) * 24 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                    else if (kalkisrihtimi == "Q.2A")
                    {
                        if (tip == "Tanker") { yilport3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { yilport3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { yilport3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { yilport3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { yilport3.ImageUrl = "images/vesb.png"; }
                        else { yilport3.ImageUrl = "images/veso.png"; }
                        yilport3.Visible = true; inportsay++;
                        yilport3.Style.Add("height", "auto");
                        yilport3.Attributes.Add("title", gemiadi + "/" + loa);
                        yilport3.Style.Add("max-width", ((loa / 26).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        yilport3.Style.Add("top", ((230 + ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        yilport3.Style.Add("left", ((572 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.2B")
                    {
                        if (tip == "Tanker") { yilport4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { yilport4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { yilport4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { yilport4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { yilport4.ImageUrl = "images/vesb.png"; }
                        else { yilport4.ImageUrl = "images/veso.png"; }
                        yilport4.Visible = true; inportsay++;
                        yilport4.Style.Add("height", "auto");
                        yilport4.Attributes.Add("title", gemiadi + "/" + loa);
                        yilport4.Style.Add("max-width", ((loa / 26).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        yilport4.Style.Add("top", ((280 - ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        yilport4.Style.Add("left", ((670 - ((loa - 70) * 37 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.3A")
                    {
                        if (tip == "Tanker") { yilport5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { yilport5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { yilport5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { yilport5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { yilport5.ImageUrl = "images/vesb.png"; }
                        else { yilport5.ImageUrl = "images/veso.png"; }
                        yilport5.Visible = true; inportsay++;
                        yilport5.Style.Add("height", "auto");
                        yilport5.Attributes.Add("title", gemiadi + "/" + loa);
                        yilport5.Style.Add("max-width", ((loa / 26).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        yilport5.Style.Add("top", ((142 + ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        yilport5.Style.Add("left", ((625 - ((loa - 70) * 6 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                    else if (kalkisrihtimi == "Q.3B")
                    {
                        if (tip == "Tanker") { yilport6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { yilport6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { yilport6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { yilport6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { yilport6.ImageUrl = "images/vesb.png"; }
                        else { yilport6.ImageUrl = "images/veso.png"; }
                        yilport6.Visible = true; inportsay++;
                        yilport6.Style.Add("height", "auto");
                        yilport6.Attributes.Add("title", gemiadi + "/" + loa);
                        yilport6.Style.Add("max-width", ((loa / 26).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        yilport6.Style.Add("top", ((198 - ((loa - 70) * 21 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        yilport6.Style.Add("left", ((674 - ((loa - 70) * 33 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.4A")
                    {
                        if (tip == "Tanker") { yilport7.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { yilport7.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { yilport7.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { yilport7.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { yilport7.ImageUrl = "images/vesb.png"; }
                        else { yilport7.ImageUrl = "images/veso.png"; }
                        yilport7.Visible = true; inportsay++;
                        yilport7.Style.Add("height", "auto");
                        yilport7.Attributes.Add("title", gemiadi + "/" + loa);
                        yilport7.Style.Add("max-width", ((loa / 26).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        yilport7.Style.Add("top", ((124 + ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        yilport7.Style.Add("left", ((630 - ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                    else if (kalkisrihtimi == "Q.4B")
                    {
                        if (tip == "Tanker") { yilport8.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { yilport8.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { yilport8.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { yilport8.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { yilport8.ImageUrl = "images/vesb.png"; }
                        else { yilport8.ImageUrl = "images/veso.png"; }
                        yilport8.Visible = true; inportsay++;
                        yilport8.Style.Add("height", "auto");
                        yilport8.Attributes.Add("title", gemiadi + "/" + loa);
                        yilport8.Style.Add("max-width", ((loa / 26).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        yilport8.Style.Add("top", ((131 - ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        yilport8.Style.Add("left", ((690 - ((loa - 70) * 40 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.5")
                    {
                        if (tip == "Tanker") { yilport9.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { yilport9.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { yilport9.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { yilport9.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { yilport9.ImageUrl = "images/vesb.png"; }
                        else { yilport9.ImageUrl = "images/veso.png"; }
                        yilport9.Visible = true; inportsay++;
                        yilport9.Style.Add("height", "auto");
                        yilport9.Attributes.Add("title", gemiadi + "/" + loa);
                        yilport9.Style.Add("max-width", ((loa / 26).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        yilport9.Style.Add("top", ((95 - ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        yilport9.Style.Add("left", ((716 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.6")
                    {
                        if (tip == "Tanker") { yilport10.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { yilport10.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { yilport10.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { yilport10.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { yilport10.ImageUrl = "images/vesb.png"; }
                        else { yilport10.ImageUrl = "images/veso.png"; }
                        yilport10.Visible = true; inportsay++;
                        yilport10.Style.Add("height", "auto");
                        yilport10.Attributes.Add("title", gemiadi + "/" + loa);
                        yilport10.Style.Add("max-width", ((loa / 26).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        yilport10.Style.Add("top", ((66 - ((loa - 70) * 0 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        yilport10.Style.Add("left", ((766 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
                if (kalkislimani == "Altıntel")
                {
                    if (kalkisrihtimi == "P.in")
                    {
                        if (tip == "Tanker") { altintel1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { altintel1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { altintel1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { altintel1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { altintel1.ImageUrl = "images/vesb.png"; }
                        else { altintel1.ImageUrl = "images/veso.png"; }
                        altintel1.Visible = true; inportsay++;
                        altintel1.Style.Add("height", "auto");
                        altintel1.Attributes.Add("title", gemiadi + "/" + loa);
                        altintel1.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        altintel1.Style.Add("top", ((111 - ((loa - 70) * 16 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        altintel1.Style.Add("left", ((797 - ((loa - 70) * 21 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.out.A")
                    {
                        if (tip == "Tanker") { altintel2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { altintel2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { altintel2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { altintel2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { altintel2.ImageUrl = "images/vesb.png"; }
                        else { altintel2.ImageUrl = "images/veso.png"; }
                        altintel2.Visible = true; inportsay++;
                        altintel2.Style.Add("height", "auto");
                        altintel2.Attributes.Add("title", gemiadi + "/" + loa);
                        altintel2.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        altintel2.Style.Add("top", ((91 + ((loa - 70) * 9 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        altintel2.Style.Add("left", ((846 - ((loa - 70) * 31 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.out.B")
                    {
                        if (tip == "Tanker") { altintel3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { altintel3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { altintel3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { altintel3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { altintel3.ImageUrl = "images/vesb.png"; }
                        else { altintel3.ImageUrl = "images/veso.png"; }
                        altintel3.Visible = true; inportsay++;
                        altintel3.Style.Add("height", "auto");
                        altintel3.Attributes.Add("title", gemiadi + "/" + loa);
                        altintel3.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        altintel3.Style.Add("top", ((142 - ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        altintel3.Style.Add("left", ((804 - ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Solventaş")
                {
                    if (kalkisrihtimi == "1A.ist")
                    {
                        if (tip == "Tanker") { solventas1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { solventas1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { solventas1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { solventas1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { solventas1.ImageUrl = "images/vesb.png"; }
                        else { solventas1.ImageUrl = "images/veso.png"; }
                        solventas1.Visible = true; inportsay++;
                        solventas1.Style.Add("height", "auto");
                        solventas1.Attributes.Add("title", gemiadi + "/" + loa);
                        solventas1.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        solventas1.Style.Add("top", ((238 + ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        solventas1.Style.Add("left", ((962 - ((loa - 70) * 29 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "1A.iz")
                    {
                        if (tip == "Tanker") { solventas2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { solventas2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { solventas2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { solventas2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { solventas2.ImageUrl = "images/vesb.png"; }
                        else { solventas2.ImageUrl = "images/veso.png"; }
                        solventas2.Visible = true; inportsay++;
                        solventas2.Style.Add("height", "auto");
                        solventas2.Attributes.Add("title", gemiadi + "/" + loa);
                        solventas2.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        solventas2.Style.Add("top", ((226 + ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        solventas2.Style.Add("left", ((988 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "1B.ist")
                    {
                        if (tip == "Tanker") { solventas3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { solventas3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { solventas3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { solventas3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { solventas3.ImageUrl = "images/vesb.png"; }
                        else { solventas3.ImageUrl = "images/veso.png"; }
                        solventas3.Visible = true; inportsay++;
                        solventas3.Style.Add("height", "auto");
                        solventas3.Attributes.Add("title", gemiadi + "/" + loa);
                        solventas3.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        solventas3.Style.Add("top", ((302 - ((loa - 70) * 22 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        solventas3.Style.Add("left", ((972 - ((loa - 70) * 34 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "1B.iz")
                    {
                        if (tip == "Tanker") { solventas4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { solventas4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { solventas4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { solventas4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { solventas4.ImageUrl = "images/vesb.png"; }
                        else { solventas4.ImageUrl = "images/veso.png"; }
                        solventas4.Visible = true; inportsay++;
                        solventas4.Style.Add("height", "auto");
                        solventas4.Attributes.Add("title", gemiadi + "/" + loa);
                        solventas4.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        solventas4.Style.Add("top", ((298 - ((loa - 70) * 26 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        solventas4.Style.Add("left", ((998 - ((loa - 70) * 25 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "2A.ist")
                    {
                        if (tip == "Tanker") { solventas5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { solventas5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { solventas5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { solventas5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { solventas5.ImageUrl = "images/vesb.png"; }
                        else { solventas5.ImageUrl = "images/veso.png"; }
                        solventas5.Visible = true; inportsay++;
                        solventas5.Style.Add("height", "auto");
                        solventas5.Attributes.Add("title", gemiadi + "/" + loa);
                        solventas5.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        solventas5.Style.Add("top", ((206 + ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        solventas5.Style.Add("left", ((1006 - ((loa - 70) * 22 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "2A.iz")
                    {
                        if (tip == "Tanker") { solventas6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { solventas6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { solventas6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { solventas6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { solventas6.ImageUrl = "images/vesb.png"; }
                        else { solventas6.ImageUrl = "images/veso.png"; }
                        solventas6.Visible = true; inportsay++;
                        solventas6.Style.Add("height", "auto");
                        solventas6.Attributes.Add("title", gemiadi + "/" + loa);
                        solventas6.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        solventas6.Style.Add("top", ((192 + ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        solventas6.Style.Add("left", ((1029 - ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "2B.ist")
                    {
                        if (tip == "Tanker") { solventas7.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { solventas7.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { solventas7.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { solventas7.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { solventas7.ImageUrl = "images/vesb.png"; }
                        else { solventas7.ImageUrl = "images/veso.png"; }
                        solventas7.Visible = true; inportsay++;
                        solventas7.Style.Add("height", "auto");
                        solventas7.Attributes.Add("title", gemiadi + "/" + loa);
                        solventas7.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        solventas7.Style.Add("top", ((269 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        solventas7.Style.Add("left", ((1029 - ((loa - 70) * 36 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "2B.iz")
                    {
                        if (tip == "Tanker") { solventas8.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { solventas8.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { solventas8.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { solventas8.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { solventas8.ImageUrl = "images/vesb.png"; }
                        else { solventas8.ImageUrl = "images/veso.png"; }
                        solventas8.Visible = true; inportsay++;
                        solventas8.Style.Add("height", "auto");
                        solventas8.Attributes.Add("title", gemiadi + "/" + loa);
                        solventas8.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        solventas8.Style.Add("top", ((255 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        solventas8.Style.Add("left", ((1054 - ((loa - 70) * 28 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }

                }

                if (kalkislimani == "Efesan")
                {
                    if (kalkisrihtimi == "P.1.ist")
                    {
                        if (tip == "Tanker") { efesan1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { efesan1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { efesan1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { efesan1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { efesan1.ImageUrl = "images/vesb.png"; }
                        else { efesan1.ImageUrl = "images/veso.png"; }
                        efesan1.Visible = true; inportsay++;
                        efesan1.Style.Add("height", "auto");
                        efesan1.Attributes.Add("title", gemiadi + "/" + loa);
                        efesan1.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        efesan1.Style.Add("top", ((177 + ((loa - 70) * 17 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        efesan1.Style.Add("left", ((1064 - ((loa - 70) * 14 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2.ist")
                    {
                        if (tip == "Tanker") { efesan2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { efesan2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { efesan2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { efesan2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { efesan2.ImageUrl = "images/vesb.png"; }
                        else { efesan2.ImageUrl = "images/veso.png"; }
                        efesan2.Visible = true; inportsay++;
                        efesan2.Style.Add("height", "auto");
                        efesan2.Attributes.Add("title", gemiadi + "/" + loa);
                        efesan2.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        efesan2.Style.Add("top", ((215 - ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        efesan2.Style.Add("left", ((1100 - ((loa - 70) * 41 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.3.iz")
                    {
                        if (tip == "Tanker") { efesan3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { efesan3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { efesan3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { efesan3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { efesan3.ImageUrl = "images/vesb.png"; }
                        else { efesan3.ImageUrl = "images/veso.png"; }
                        efesan3.Visible = true; inportsay++;
                        efesan3.Style.Add("height", "auto");
                        efesan3.Attributes.Add("title", gemiadi + "/" + loa);
                        efesan3.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        efesan3.Style.Add("top", ((163 + ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        efesan3.Style.Add("left", ((1086 - ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.4.iz")
                    {
                        if (tip == "Tanker") { efesan4.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { efesan4.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { efesan4.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { efesan4.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { efesan4.ImageUrl = "images/vesb.png"; }
                        else { efesan4.ImageUrl = "images/veso.png"; }
                        efesan4.Visible = true; inportsay++;
                        efesan4.Style.Add("height", "auto");
                        efesan4.Attributes.Add("title", gemiadi + "/" + loa);
                        efesan4.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        efesan4.Style.Add("top", ((197 - ((loa - 70) * 24 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        efesan4.Style.Add("left", ((1119 - ((loa - 70) * 39 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.5")
                    {
                        if (tip == "Tanker") { efesan5.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { efesan5.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { efesan5.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { efesan5.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { efesan5.ImageUrl = "images/vesb.png"; }
                        else { efesan5.ImageUrl = "images/veso.png"; }
                        efesan5.Visible = true; inportsay++;
                        efesan5.Style.Add("height", "auto");
                        efesan5.Attributes.Add("title", gemiadi + "/" + loa);
                        efesan5.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        efesan5.Style.Add("top", ((137 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        efesan5.Style.Add("left", ((1083 + ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "Q.6")
                    {
                        if (tip == "Tanker") { efesan6.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { efesan6.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { efesan6.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { efesan6.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { efesan6.ImageUrl = "images/vesb.png"; }
                        else { efesan6.ImageUrl = "images/veso.png"; }
                        efesan6.Visible = true; inportsay++;
                        efesan6.Style.Add("height", "auto");
                        efesan6.Attributes.Add("title", gemiadi + "/" + loa);
                        efesan6.Style.Add("max-width", ((loa / 24).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        efesan6.Style.Add("top", ((108 + ((loa - 70) * 7 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        efesan6.Style.Add("left", ((1154 - ((loa - 70) * 43 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }

                if (kalkislimani == "Güzel Enerji")
                {
                    if (kalkisrihtimi == "P.1.in")
                    {
                        if (tip == "Tanker") { generji1.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { generji1.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { generji1.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { generji1.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { generji1.ImageUrl = "images/vesb.png"; }
                        else { generji1.ImageUrl = "images/veso.png"; }
                        generji1.Visible = true; inportsay++;
                        generji1.Style.Add("height", "auto");
                        generji1.Attributes.Add("title", gemiadi + "/" + loa);
                        generji1.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        generji1.Style.Add("top", ((108 - ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        generji1.Style.Add("left", ((1206 - ((loa - 70) * 38 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2.out")
                    {
                        if (tip == "Tanker") { generji2.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { generji2.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { generji2.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { generji2.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { generji2.ImageUrl = "images/vesb.png"; }
                        else { generji2.ImageUrl = "images/veso.png"; }
                        generji2.Visible = true; inportsay++;
                        generji2.Style.Add("height", "auto");
                        generji2.Attributes.Add("title", gemiadi + "/" + loa);
                        generji2.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        generji2.Style.Add("top", ((121 - ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        generji2.Style.Add("left", ((1213 - ((loa - 70) * 21 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                    else if (kalkisrihtimi == "P.2.out.Dbl.Anc")
                    {
                        if (tip == "Tanker") { generji3.ImageUrl = "images/vest.png"; }
                        else if (tip == "Container") { generji3.ImageUrl = "images/vesc.png"; }
                        else if (tip == "Lpg" || tip == "Lng") { generji3.ImageUrl = "images/vesl.png"; }
                        else if (tip == "RoRo") { generji3.ImageUrl = "images/vesr.png"; }
                        else if (tip == "Gnr.Cargo") { generji3.ImageUrl = "images/vesb.png"; }
                        else { generji3.ImageUrl = "images/veso.png"; }
                        generji3.Visible = true; inportsay++;
                        generji3.Style.Add("height", "auto");
                        generji3.Attributes.Add("title", gemiadi + "/" + loa);
                        generji3.Style.Add("max-width", ((loa / 25).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        generji3.Style.Add("top", ((132 + ((loa - 70) * 14 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        generji3.Style.Add("left", ((1220 - ((loa - 70) * 11 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }
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
                        kroman1.Attributes.Add("title", gemiadi + "/" + loa);
                        kroman1.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kroman1.Style.Add("top", ((552 + ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kroman1.Style.Add("left", ((596 - ((loa - 70) * 38 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        kroman2.Attributes.Add("title", gemiadi + "/" + loa);
                        kroman2.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kroman2.Style.Add("top", ((577 + ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kroman2.Style.Add("left", ((562 - ((loa - 70) * 38 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        kroman3.Attributes.Add("title", gemiadi + "/" + loa);
                        kroman3.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kroman3.Style.Add("top", ((605 + ((loa - 70) * 12 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kroman3.Style.Add("left", ((524 - ((loa - 70) * 38 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        kroman4.Attributes.Add("title", gemiadi + "/" + loa);
                        kroman4.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kroman4.Style.Add("top", ((646 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kroman4.Style.Add("left", ((467 - ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        kroman5.Attributes.Add("title", gemiadi + "/" + loa);
                        kroman5.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        kroman5.Style.Add("top", ((625 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        kroman5.Style.Add("left", ((494 - ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        diler1.Attributes.Add("title", gemiadi + "/" + loa);
                        diler1.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler1.Style.Add("top", ((534 + ((loa - 70) * 9 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler1.Style.Add("left", ((902 - ((loa - 70) * 41 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        diler2.Attributes.Add("title", gemiadi + "/" + loa);
                        diler2.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler2.Style.Add("top", ((569 + ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler2.Style.Add("left", ((842 - ((loa - 70) * 36 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        diler3.Attributes.Add("title", gemiadi + "/" + loa);
                        diler3.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler3.Style.Add("top", ((598 - ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler3.Style.Add("left", ((790 - ((loa - 70) * 17 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        diler4.Attributes.Add("title", gemiadi + "/" + loa);
                        diler4.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler4.Style.Add("top", ((586 - ((loa - 70) * 4 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler4.Style.Add("left", ((777 - ((loa - 70) * 25 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        diler5.Attributes.Add("title", gemiadi + "/" + loa);
                        diler5.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler5.Style.Add("top", ((573 - ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler5.Style.Add("left", ((780 - ((loa - 70) * 41 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        diler6.Attributes.Add("title", gemiadi + "/" + loa);
                        diler6.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler6.Style.Add("top", ((590 + ((loa - 70) * 5 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler6.Style.Add("left", ((720 - ((loa - 70) * 29 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        diler7.Attributes.Add("title", gemiadi + "/" + loa);
                        diler7.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler7.Style.Add("top", ((640 - ((loa - 70) * 3 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler7.Style.Add("left", ((637 - ((loa - 70) * 17 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        diler8.Attributes.Add("title", gemiadi + "/" + loa);
                        diler8.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        diler8.Style.Add("top", ((614 - ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        diler8.Style.Add("left", ((679 - ((loa - 70) * 17 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        nuh1.Attributes.Add("title", gemiadi + "/" + loa);
                        nuh1.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        nuh1.Style.Add("top", ((625 - ((loa - 70) * 8 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        nuh1.Style.Add("left", ((963 - ((loa - 70) * 10 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        nuh2.Attributes.Add("title", gemiadi + "/" + loa);
                        nuh2.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        nuh2.Style.Add("top", ((570 + ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        nuh2.Style.Add("left", ((1033 - ((loa - 70) * 21 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        nuh3.Attributes.Add("title", gemiadi + "/" + loa);
                        nuh3.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        nuh3.Style.Add("top", ((530 - ((loa - 70) * 2 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        nuh3.Style.Add("left", ((1086 - ((loa - 70) * 18 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
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
                        nuh4.Attributes.Add("title", gemiadi + "/" + loa);
                        nuh4.Style.Add("max-width", ((loa / 30).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "%");
                        nuh4.Style.Add("top", ((596 + ((loa - 70) * 1 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                        nuh4.Style.Add("left", ((999 - ((loa - 70) * 20 / 100)).ToString(CultureInfo.CreateSpecificCulture("en-GB"))) + "px");
                    }
                }




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




