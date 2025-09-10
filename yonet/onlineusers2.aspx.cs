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
using System.Web.Security;


public partial class yonet_onlineusers2 : System.Web.UI.Page
{
    MembershipUserCollection users;


    Metodlar AnaKlas = new Metodlar();

    public class Employee
    {
        private int _id;
        private string _FName;
        private string _Pcip;
        private string _Login;
        private string _Kapno;
        private string _Kapsirano;
        private string _Times;


        public int ID { get { return _id; } set { _id = value; } }
        public string kapadisoyadi { get { return _FName; } set { _FName = value; } }
        public string Pcip { get { return _Pcip; } set { _Pcip = value; } }
        public string Login { get { return _Login; } set { _Login = value; } }
        public string Kapno { get { return _Kapno; } set { _Kapno = value; } }
        public string Kapsirano { get { return _Kapsirano; } set { _Kapsirano = value; } }
        public string Times { get { return _Times; } set { _Times = value; } }


        public Employee(int id, string fname, string pcip, string login, string kapno, string kapsirano, string times)
        {
            ID = id;
            kapadisoyadi = fname;
            Pcip = pcip;
            Login = login;
            Kapno = kapno;
            Kapsirano = kapsirano;
            Times = times;
        }


    }


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
        else if (Convert.ToInt32(Session["kapno"]) < 0 || Convert.ToInt32(Session["kapno"]) > 999)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }
        else if (cmdlogofbak.ExecuteScalar() == null)
        {
            Response.Redirect("http://www.monitoringpilot.com");
        }

        else
        {

            if (!IsPostBack)
            {
                databagla();
            }

        }
        cmdlogofbak.Dispose();
        baglanti.Close();
    }

    private void databagla()  
    {
   
        //DataTable DTlrsetgrid = AnaKlas.GetDataTable("Select DISTINCT top 100 kapno, id, kapadi +' '+ kapsoyadi as kapadisoyadi, pcip, login from onoffline  order by id desc");
        //GridView1.DataSource = DTlrsetgrid;
        //GridView1.DataBind();
        List<Employee> empList = new List<Employee>();
        int i = 1;

        SqlConnection baglanti = AnaKlas.baglan();
        SqlCommand cmdfatoku = new SqlCommand("select kapno from onoffline  where yetki!='8'  group by kapno", baglanti);

        SqlDataReader fatrdr = cmdfatoku.ExecuteReader();

        if (fatrdr.HasRows)
        {
            while (fatrdr.Read())
            {
                string kapno= fatrdr["kapno"].ToString();

                SqlConnection baglanti2 = AnaKlas.baglan2();
                SqlCommand cmdfatoku2 = new SqlCommand("select kapno, kapadi +' '+ kapsoyadi as kapadisoyadi, count(kapno) as say, id, pcip, login from onoffline where id =(select max(id) from onoffline where kapno= '" + kapno + "'  and yetki != '8') and DateTime.Parse(login)>'"+DateTime.Now.AddDays(-100)+"' and kapno='" + kapno+"'  " , baglanti2);

                cmdfatoku2.ExecuteNonQuery();
                cmdfatoku2.Dispose();

                SqlDataReader fatrdralt = cmdfatoku2.ExecuteReader();
                if (fatrdralt.HasRows)
                {
                    while (fatrdralt.Read())
                    {





                        empList.Add(new Employee(i,fatrdralt["kapadisoyadi"].ToString(), fatrdralt["pcip"].ToString(), fatrdralt["login"].ToString(), fatrdralt["kapsirano"].ToString(), fatrdralt["say"].ToString(), kapno));
                    }
                }
                fatrdralt.Close();
                baglanti2.Close();
                i = i + 1;
            }
        }
        fatrdr.Close();
        cmdfatoku.Dispose();

        GridView1.DataSource = empList.Take(100).OrderByDescending(x=> DateTime.Parse(x.Login));
        GridView1.DataBind();

        baglanti.Close();



    }





    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (e.NewSelectedIndex == GridView1.SelectedIndex)
        {
            e.Cancel = true;
            GridView1.SelectedIndex = -1;
            databagla();
        }



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
    protected void LBmainpage_Click(object sender, EventArgs e)
    {
        Response.Redirect("../main.aspx");
    }








    protected void kapadi_Click(object sender, EventArgs e)
    {

        SqlConnection baglanti = AnaKlas.baglan();
        LinkButton LinkButtonisler = (LinkButton)sender;
        PilotEid.Text = LinkButtonisler.CommandArgument.ToString();
        int Pilotidi = Convert.ToInt32(PilotEid.Text);

        string kapadidetay = HttpUtility.HtmlDecode(LinkButtonisler.Text).ToString();

        Lblpilotname.Text = kapadidetay;



        SqlCommand cmdfatoku2a = new SqlCommand("select kapadi +' '+ kapsoyadi as kapadisoyadi, id, pcip, login, logof from onoffline where kapno ='" + Pilotidi + "' and yetki != '8'  order by id desc", baglanti);

        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmdfatoku2a;
        DataSet ds = new DataSet();
        adapter.Fill(ds, "onlinelar");
        ListView1.DataSource = ds;
        ListView1.DataBind();

        cmdfatoku2a.Dispose();

        baglanti.Close();

        this.ModalPopupExtenderPilotEdit.Show();
    }
}