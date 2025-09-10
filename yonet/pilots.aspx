<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pilots.aspx.cs" Inherits="yonet_pilots"  MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />

            <style type="text/css">
.panelpilotaddeadit{
width: 280px;
height: 500px;
border: 1px  groove #111;
background-color:#cccccc;
}

.panelmessage {
width: 300px;
height: 100px;
border: 1px  groove #111;
background-color:white;
margin:15px;
font-family:'Trebuchet MS';
font-size:small;
text-align:center;
}

.modalbodyarka{
background-color: #333333;
filter: alpha(opacity:70);
opacity: 0.6;
z-index: 10000;
}

</style>

</head>

<body>
    <form id="form1" runat="server">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

   <div id="main">
  <div id="header"> <a href="#" class="logo"><img src="img/logo.png" width="46" height="48" alt="" /></a>
<div id="header-right"><asp:Literal ID="Litpagebaslik" runat="server" ></asp:Literal></div>
    <ul id="top-navigation">
      <li class="active" ><span><span>Settings</span></span></li>
      <li><span><span><a href="Statistics.aspx">Statistics</a></span></span></li>
      <li><span><span><a href="vacations.aspx">Vacations</a></span></span></li>
      <li><span><span><a href="onlineusers.aspx">Online Users</a></span></span></li>
      <li><span><span><a href="../main.aspx">Pilot Monitor</a></span></span></li>
      <li><span><span> <asp:LinkButton ID="LinkButton1" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>
    </ul>
  </div>
  <div id="middle">

    <div id="left-column"> 
      <h3>Users</h3>
      <ul class="nav">
                <asp:Literal ID="Litmenu1" Text="" runat="server"> 
                <li><a href="pilots.aspx">User List</a></li></asp:Literal>

      </ul>

      <h3>Places</h3>
      <ul class="nav">
        <li><a href="portsandberths.aspx">Ports and Berths</a></li>
        <li><a href="anchorageplaces.aspx">Anchorage Places</a></li>

      </ul>

      <h3>Distances</h3><ul class="nav">
        <li><a href="distances.aspx">Distances</a></li></ul>

      <asp:Literal ID="Litmenu4" Text="" runat="server"> 
      <h3>Flags</h3> <ul class="nav">        
        <li><a href="flags.aspx">Flags</a></li></ul></asp:Literal>

      <asp:Literal ID="Litmenu5" Text="" runat="server"> 
      <h3>Ship Types</h3><ul class="nav">
        <li><a href="shiptypes.aspx">Ship Types</a></li></ul></asp:Literal>

      <asp:Literal ID="Litmenu6" Text="" runat="server"> 
      <h3>Pilot Stations</h3><ul class="nav">
        <li><a href="pilotstations.aspx">Pilot Stations</a></li></ul></asp:Literal>

</div>


      <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
 
    <div id="center-column">
      <div class="top-bar">

          <ul>
  <li class="sagusttuslist">  <asp:LinkButton ID="Baddnewpilot" CssClass="sagusttuslista" runat="server" OnClick="Baddnewpilot_Click" >Add New User</asp:LinkButton></li>
</ul>

        <h1><a href="yonetim.aspx">Settings</a> > User List </h1>
          
      <div class="breadcrumbs"></div>
      </div>
      <br />
      <div class="select-bar">
        <label>
            <asp:DropDownList ID="DropDownListports"  runat="server"   Height="21px" Width="250px" OnSelectedIndexChanged="DropDownListports_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
        </label>

      </div>


              <div class="breadcrumbs" runat="server" id="yetkiaciklama">

 <u>Yetkiler: </u><br />
                  
 "0" > Kýlavuz Kaptan <br />
 "1" > Gözcü Darýca <br />
 "2" > Gözcü Yarýmca <br />
 "3" > Þirket Müdürü <br />
 "4" > Þirket Çalýþaný <br />
 "5" > Dýþ Firmalar <br />
 "6" > Stajer Pilot <br />
 "7" > Stajer Gözcü <br />
 "8" > Pilot Botu Kaptaný <br />
 "9" > Admins <br />

      </div>



      <div >

       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="747px" DataKeyNames="kapno"   OnSelectedIndexChanging="GridView1_SelectedIndexChanging"  CellPadding="2" ForeColor="#333333"  GridLines="Vertical"  >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/arrowr.png"  HeaderText=""  SelectText="" ItemStyle-Width="30px"   ShowSelectButton="True" />

<asp:TemplateField HeaderText="No"  ControlStyle-Width="25px" >
                    <ItemTemplate><asp:Label ID="Litemportlimanno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>

<asp:TemplateField HeaderText="Name-Surname"  ControlStyle-Width="135px" >
                    <ItemTemplate><asp:Label ID="Litemportlimanadi" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>

<asp:TemplateField HeaderText="E-Mail"   ControlStyle-Width="120px">
                     <ItemTemplate><asp:Label ID="Litemportrihtimadi" runat="server" Text='<%# Bind("eposta") %>'></asp:Label> </ItemTemplate>
                 </asp:TemplateField>

<asp:TemplateField HeaderText="Gsm"   ControlStyle-Width="85px">
                     <ItemTemplate> <asp:Label ID="Litemportlimanbolge" runat="server" Text='<%# Bind("tel1") %>'></asp:Label> </ItemTemplate>
                </asp:TemplateField>

<asp:TemplateField HeaderText="Lvl."  ControlStyle-Width="20px">
                    <ItemTemplate><asp:Label ID="Litemportzorluk" runat="server" Text='<%# Bind("yetki") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>

<asp:TemplateField HeaderText="Rtrd"   ControlStyle-Width="20px">
                    <ItemTemplate><asp:Label ID="Litemportresp" runat="server" Text='<%# Bind("emekli") %>'></asp:Label></ItemTemplate>
                 </asp:TemplateField>

<asp:TemplateField HeaderText="Stn." ControlStyle-Width="20px">
                      <ItemTemplate><asp:Label ID="Litemportyanasmasuresi" runat="server" Text='<%# Bind("girisistasyon") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>

<asp:TemplateField HeaderText="Edit"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle">
<ItemTemplate><asp:ImageButton ID="IBPilotEdit"  CommandArgument='<%#Eval("kapno")%>'  runat="server" ImageUrl="~/images/edit.png" Width="16px"  OnClick="IBPilotEdit_Click"/></ItemTemplate></asp:TemplateField>

             
            </Columns>

            <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1"  ForeColor="#cc0000" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>


        <asp:Button id="buttonshowpopuppadd" runat="server" style="display:none;" />
        <asp:Panel ID="panellbadd"   runat="server"> 

<table  id="PilotAdd"  class="panellbadd">                        
            <tr><td colspan="2"  class="tdorta"  ><div style="border:1px solid white; font-weight:bold; height:40px ; text-align:center" ><br /><strong>Add New User</strong></div></td></tr>      

            <tr><td >Worker Number : </td><td  class="tdsag" ><div><asp:TextBox ID="TBPADDkapsirano" Width="50px" runat="server" MaxLength="4"  ReadOnly="true" ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="TBPADDkapsirano" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></div>  </td></tr>
            <tr><td >Name: </td><td  class="tdsag" ><asp:TextBox ID="TBPADDkapadi" Width="150px" runat="server"  Visible="true" CssClass="kucukharf"  MaxLength="30">     </asp:TextBox>  </td></tr>
            <tr><td >Surname : </td><td  class="tdsag" ><asp:TextBox ID="TBPADDkapsoyadi" Width="150px" runat="server"  CssClass="kucukharf"  MaxLength="30"></asp:TextBox></td></tr>
            <tr><td >E-Mail : </td><td  class="tdsag" ><asp:TextBox ID="TBPADDeposta"  runat="server" Width="150px" MaxLength="30"  CssClass="kucukharf" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red" ErrorMessage="**" ControlToValidate="TBPADDeposta" ValidationExpression="\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator></td></tr>

            <tr><td >Password : </td><td  class="tdsag" ><asp:TextBox ID="TBPADDsifre"  runat="server" Width="150px" MaxLength="30" TextMode="Password"   BorderStyle="NotSet" BorderWidth="1px" ></asp:TextBox></td></tr>
            <tr><td >Gsm: </td><td  class="tdsag" >
                <asp:TextBox ID="TBPADDtel1"  runat="server" Width="120px" MaxLength="11"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBPADDtel1"  CultureName="tr-TR"  ID="MaskedEditExtender11" runat="server" ErrorTooltipEnabled="True" MaskType="None" DisplayMoney="Left"  AcceptNegative="Left"  Mask="9999-999-99-99" ClearMaskOnLostFocus="false"></asp:MaskedEditExtender>
                <asp:TextBox ID="TBPADDtel2"  runat="server" Width="120px" MaxLength="11"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBPADDtel2"  CultureName="tr-TR"  ID="MaskedEditExtender12" runat="server" ErrorTooltipEnabled="True" MaskType="None" DisplayMoney="Left"  AcceptNegative="Left"  Mask="9999-999-99-99" ClearMaskOnLostFocus="false"></asp:MaskedEditExtender></td></tr>
            <tr><td >Address :</td><td class="tdsag" ><asp:TextBox    ID="TBPADDaddress" runat="server" Visible="True" MaxLength="100"  TextMode="MultiLine" Rows="2" Width="244px"  CssClass="kucukharf" ></asp:TextBox></td></tr >
            <tr><td >Date of Birth :</td><td class="tdsag" ><asp:TextBox  ID="TBPADDdogumtar" runat="server" Visible="True"  MaxLength="10" Width="90px"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBPADDdogumtar"  CultureName="tr-TR"  ID="MaskedEditExtender8" runat="server" ErrorTooltipEnabled="True" MaskType="Date" DisplayMoney="Left"  AcceptNegative="Left"  Mask="99/99/9999"  ClearMaskOnLostFocus="false"></asp:MaskedEditExtender>       
                <asp:DropDownList ID="TBPADDdogumyer" runat="server" Width="110px" Height="19px">
            <asp:ListItem Value="01">Adana</asp:ListItem>
            <asp:ListItem Value="02">Adýyaman</asp:ListItem>
            <asp:ListItem Value="03">Afyonkarahisar</asp:ListItem>
            <asp:ListItem Value="04">Aðrý</asp:ListItem>
            <asp:ListItem Value="05">Amasya</asp:ListItem>
            <asp:ListItem Value="06">Ankara</asp:ListItem>
            <asp:ListItem Value="07">Antalya</asp:ListItem>
            <asp:ListItem Value="08">Artvin</asp:ListItem>
            <asp:ListItem Value="09">Aydýn</asp:ListItem>
            <asp:ListItem Value="10">Balýkesir</asp:ListItem>
            <asp:ListItem Value="11">Bilecik</asp:ListItem>
            <asp:ListItem Value="12">Bingöl</asp:ListItem>
            <asp:ListItem Value="13">Bitlis</asp:ListItem>
            <asp:ListItem Value="14">Bolu</asp:ListItem>
            <asp:ListItem Value="15">Burdur</asp:ListItem>
            <asp:ListItem Value="16">Bursa</asp:ListItem>
            <asp:ListItem Value="17">Çanakkale</asp:ListItem>
            <asp:ListItem Value="18">Çankýrý</asp:ListItem>
            <asp:ListItem Value="19">Çorum</asp:ListItem>
            <asp:ListItem Value="20">Denizli</asp:ListItem>
            <asp:ListItem Value="21">Diyarbakýr</asp:ListItem>
            <asp:ListItem Value="22">Edirne</asp:ListItem>
            <asp:ListItem Value="23">Elazýð</asp:ListItem>
            <asp:ListItem Value="24">Erzincan</asp:ListItem>
            <asp:ListItem Value="25">Erzurum</asp:ListItem>
            <asp:ListItem Value="26">Eskiþehir</asp:ListItem>
            <asp:ListItem Value="27">Gaziantep</asp:ListItem>
            <asp:ListItem Value="28">Giresun</asp:ListItem>
            <asp:ListItem Value="29">Gümüþhane</asp:ListItem>
            <asp:ListItem Value="30">Hakkari</asp:ListItem>
            <asp:ListItem Value="31">Hatay</asp:ListItem>
            <asp:ListItem Value="32">Isparta</asp:ListItem>
            <asp:ListItem Value="33">Ýçel</asp:ListItem>
            <asp:ListItem Value="34" Selected="True">Ýstanbul</asp:ListItem>
            <asp:ListItem Value="35">Ýzmir</asp:ListItem>
            <asp:ListItem Value="46">Kahramanmaraþ</asp:ListItem>
            <asp:ListItem Value="36">Kars</asp:ListItem>
            <asp:ListItem Value="37">Kastamonu</asp:ListItem>
            <asp:ListItem Value="38">Kayseri</asp:ListItem>
            <asp:ListItem Value="39">Kýrklareli</asp:ListItem>
            <asp:ListItem Value="40">Kýrþehir</asp:ListItem>
            <asp:ListItem Value="41">Kocaeli</asp:ListItem>
            <asp:ListItem Value="42">Konya</asp:ListItem>
            <asp:ListItem Value="43">Kütahya</asp:ListItem>
            <asp:ListItem Value="44">Malatya</asp:ListItem>
            <asp:ListItem Value="45">Manisa</asp:ListItem>
            <asp:ListItem Value="47">Mardin</asp:ListItem>
            <asp:ListItem Value="48">Muðla</asp:ListItem>
            <asp:ListItem Value="49">Muþ</asp:ListItem>
            <asp:ListItem Value="50">Nevþehir</asp:ListItem>
            <asp:ListItem Value="51">Niðde</asp:ListItem>
            <asp:ListItem Value="52">Ordu</asp:ListItem>
            <asp:ListItem Value="53">Rize</asp:ListItem>
            <asp:ListItem Value="54">Sakarya</asp:ListItem>
            <asp:ListItem Value="55">Samsun</asp:ListItem>
            <asp:ListItem Value="56">Siirt</asp:ListItem>
            <asp:ListItem Value="57">Sinop</asp:ListItem>
            <asp:ListItem Value="58">Sivas</asp:ListItem>
            <asp:ListItem Value="59">Tekirdað</asp:ListItem>
            <asp:ListItem Value="60">Tokat</asp:ListItem>
            <asp:ListItem Value="61">Trabzon</asp:ListItem>
            <asp:ListItem Value="62">Tunceli</asp:ListItem>
            <asp:ListItem Value="63">Þanlýurfa</asp:ListItem>
            <asp:ListItem Value="64">Uþak</asp:ListItem>
            <asp:ListItem Value="65">Van</asp:ListItem>
            <asp:ListItem Value="66">Yozgat</asp:ListItem>
            <asp:ListItem Value="67">Zonguldak</asp:ListItem>
            <asp:ListItem Value="68">Aksaray</asp:ListItem>
            <asp:ListItem Value="69">Bayburt</asp:ListItem>
            <asp:ListItem Value="70">Karaman</asp:ListItem>
            <asp:ListItem Value="71">Kýrýkkale</asp:ListItem>
            <asp:ListItem Value="72">Batman</asp:ListItem>
            <asp:ListItem Value="73">Þýrnak</asp:ListItem>
            <asp:ListItem Value="74">Bartýn</asp:ListItem>
            <asp:ListItem Value="75">Ardahan</asp:ListItem>
            <asp:ListItem Value="76">Iðdýr</asp:ListItem>
            <asp:ListItem Value="77">Yalova</asp:ListItem>
            <asp:ListItem Value="78">Karabük</asp:ListItem>
            <asp:ListItem Value="79">Kilis</asp:ListItem>
            <asp:ListItem Value="80">Osmaniye</asp:ListItem>
            <asp:ListItem Value="81">Düzce</asp:ListItem>
        </asp:DropDownList> </td></tr >
            <tr><td >Authority Level : </td><td  class="tdsag" ><asp:DropDownList ID="TBPADDyetki" runat="server" Width="130px" Height="19px"  AutoPostBack="true" OnSelectedIndexChanged="TBPADDyetki_SelectedIndexChanged"> 
                    <asp:ListItem Value="0" Selected="True">Kýlavuz Kaptan</asp:ListItem><asp:ListItem Value="1">Gözcü Darýca</asp:ListItem><asp:ListItem Value="2">Gözcü Yarýmca</asp:ListItem><asp:ListItem Value="3">Þirket Müdürü</asp:ListItem><asp:ListItem Value="4">Þirket Çalýþaný</asp:ListItem><asp:ListItem Value="5">Dýþ Firmalar</asp:ListItem><asp:ListItem Value="6">Stajer Pilot</asp:ListItem><asp:ListItem Value="7">Stajer Gözcü</asp:ListItem><asp:ListItem Value="8">Pilot Botu Kaptaný</asp:ListItem></asp:DropDownList>
                <asp:DropDownList ID="TBPADDkidem" runat="server" Width="50px" Height="19px"  Visible="false"> </asp:DropDownList> </td></tr>

            <tr><td colspan="2"  class="tdorta"  style="height:50px; text-align:center; font-size:20px; font-weight:bold; font-family:'Trebuchet MS'">
                       <asp:Button ID="Baddnewpilotsave" runat="server" style="height:30px; Width:80px" Text="Save"  OnClick="Baddnewpilotsave_Click"  />&nbsp;&nbsp; 
                       <asp:Button ID="Baddnewpilotcancel" runat="server" style="height:30px; Width:80px" Text="Cancel"  OnClick="Baddnewpilotcancel_Click"  /></td></tr>         
           </table>
 </asp:Panel>
            <asp:ModalPopupExtender 
            ID="ModalPopupExtenderpilotadd" runat="server" 
            TargetControlID="buttonshowpopuppadd" 
            PopupControlID="panellbadd"
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender> <br />
        <asp:Button  id="buttonshowpopuppe" runat="server" style="display:none;"/>
        <asp:Panel ID="paneleditpilot"  runat="server"> 

<table id="PilotEdit"  class="panellbadd"> 
               <tr ><td colspan="2" class="tdorta"  ><div style="border:1px solid white; font-weight:bold; height:40px ; text-align:center" ><br /><strong>Pilot's Informations</strong></div><asp:Label Visible="false" ID="PilotEid" runat="server"></asp:Label></td></tr>
            <tr><td >Worker Number : </td><td  class="tdsag" ><asp:TextBox ID="TBPEkapsirano" Width="50px" Enabled="false" runat="server" MaxLength="4"  ></asp:TextBox>                  Kapsirano : <asp:TextBox ID="TBEkapsirano" Width="50px" runat="server" MaxLength="4"  ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="TBEkapsirano" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender></td></tr>
            <tr><td >Name: </td><td  class="tdsag" ><asp:TextBox ID="TBPEkapadi" Width="150px" runat="server"  Visible="true" CssClass="kucukharf"  MaxLength="30">     </asp:TextBox>  </td></tr>
            <tr><td >Surname : </td><td  class="tdsag" ><asp:TextBox ID="TBPEkapsoyadi" Width="150px" runat="server"  CssClass="kucukharf"  MaxLength="30"></asp:TextBox></td></tr>
            <tr><td >E-Mail : </td><td  class="tdsag" ><asp:TextBox ID="TBPEeposta"  runat="server" Width="150px" MaxLength="30"  CssClass="kucukharf" ></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor="Red" ErrorMessage="**" ControlToValidate="TBPEeposta" ValidationExpression="\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                Onay: <asp:DropDownList ID="DDLpostasmsal" runat="server" Width="40px" Height="19px"> 
                    <asp:ListItem Value="0">0</asp:ListItem><asp:ListItem Value="1" >1</asp:ListItem><asp:ListItem Value="2">2</asp:ListItem><asp:ListItem Value="3">3</asp:ListItem><asp:ListItem Value="4">4</asp:ListItem><asp:ListItem Value="5">5</asp:ListItem><asp:ListItem Value="6">6</asp:ListItem><asp:ListItem Value="7">7</asp:ListItem><asp:ListItem Value="8">8</asp:ListItem><asp:ListItem Value="9">9</asp:ListItem></asp:DropDownList></td></tr>
            <tr><td ><asp:Literal ID="LitPasstext" runat="server">Password : </asp:Literal></td><td  class="tdsag" ><asp:TextBox ID="TBPEsifre"  runat="server" Width="150px" MaxLength="30" ></asp:TextBox></td></tr>
            <tr><td >Gsm : </td><td  class="tdsag" >
                <asp:TextBox ID="TBPEtel1"  runat="server" Width="120px" MaxLength="11"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBPEtel1"  CultureName="tr-TR"  ID="MaskedEditExtender9" runat="server" ErrorTooltipEnabled="True" MaskType="None" DisplayMoney="Left"  AcceptNegative="Left"  Mask="9999-999-99-99" ClearMaskOnLostFocus="false"></asp:MaskedEditExtender> 
                <asp:TextBox ID="TBPEtel2"  runat="server" Width="120px" MaxLength="11"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBPEtel2"  CultureName="tr-TR"  ID="MaskedEditExtender10" runat="server" ErrorTooltipEnabled="True" MaskType="None" DisplayMoney="Left"  AcceptNegative="Left"  Mask="9999-999-99-99" ClearMaskOnLostFocus="false"></asp:MaskedEditExtender></td></tr>
            <tr><td >Address :</td><td class="tdsag" ><asp:TextBox    ID="TBPEaddress" runat="server" Visible="True" MaxLength="100"  TextMode="MultiLine" Rows="2" Width="244px"  CssClass="kucukharf" ></asp:TextBox></td></tr >
            <tr><td >Date of Birth :</td><td class="tdsag" ><asp:TextBox  ID="TBPEdogumtar" runat="server" Visible="True"  MaxLength="10" Width="90px"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBPEdogumtar"  CultureName="tr-TR"  ID="MaskedEditExtender1" runat="server" ErrorTooltipEnabled="True" MaskType="Date" DisplayMoney="Left"  AcceptNegative="Left"  Mask="99/99/9999" ClearMaskOnLostFocus="false"></asp:MaskedEditExtender>      
                <asp:DropDownList ID="TBPEdogumyer" runat="server" Width="110px" Height="19px">
            <asp:ListItem Value="01">Adana</asp:ListItem>
            <asp:ListItem Value="02">Adýyaman</asp:ListItem>
            <asp:ListItem Value="03">Afyonkarahisar</asp:ListItem>
            <asp:ListItem Value="04">Aðrý</asp:ListItem>
            <asp:ListItem Value="05">Amasya</asp:ListItem>
            <asp:ListItem Value="06">Ankara</asp:ListItem>
            <asp:ListItem Value="07">Antalya</asp:ListItem>
            <asp:ListItem Value="08">Artvin</asp:ListItem>
            <asp:ListItem Value="09">Aydýn</asp:ListItem>
            <asp:ListItem Value="10">Balýkesir</asp:ListItem>
            <asp:ListItem Value="11">Bilecik</asp:ListItem>
            <asp:ListItem Value="12">Bingöl</asp:ListItem>
            <asp:ListItem Value="13">Bitlis</asp:ListItem>
            <asp:ListItem Value="14">Bolu</asp:ListItem>
            <asp:ListItem Value="15">Burdur</asp:ListItem>
            <asp:ListItem Value="16">Bursa</asp:ListItem>
            <asp:ListItem Value="17">Çanakkale</asp:ListItem>
            <asp:ListItem Value="18">Çankýrý</asp:ListItem>
            <asp:ListItem Value="19">Çorum</asp:ListItem>
            <asp:ListItem Value="20">Denizli</asp:ListItem>
            <asp:ListItem Value="21">Diyarbakýr</asp:ListItem>
            <asp:ListItem Value="22">Edirne</asp:ListItem>
            <asp:ListItem Value="23">Elazýð</asp:ListItem>
            <asp:ListItem Value="24">Erzincan</asp:ListItem>
            <asp:ListItem Value="25">Erzurum</asp:ListItem>
            <asp:ListItem Value="26">Eskiþehir</asp:ListItem>
            <asp:ListItem Value="27">Gaziantep</asp:ListItem>
            <asp:ListItem Value="28">Giresun</asp:ListItem>
            <asp:ListItem Value="29">Gümüþhane</asp:ListItem>
            <asp:ListItem Value="30">Hakkari</asp:ListItem>
            <asp:ListItem Value="31">Hatay</asp:ListItem>
            <asp:ListItem Value="32">Isparta</asp:ListItem>
            <asp:ListItem Value="33">Ýçel</asp:ListItem>
            <asp:ListItem Value="34">Ýstanbul</asp:ListItem>
            <asp:ListItem Value="35">Ýzmir</asp:ListItem>
            <asp:ListItem Value="46">Kahramanmaraþ</asp:ListItem>
            <asp:ListItem Value="36">Kars</asp:ListItem>
            <asp:ListItem Value="37">Kastamonu</asp:ListItem>
            <asp:ListItem Value="38">Kayseri</asp:ListItem>
            <asp:ListItem Value="39">Kýrklareli</asp:ListItem>
            <asp:ListItem Value="40">Kýrþehir</asp:ListItem>
            <asp:ListItem Value="41">Kocaeli</asp:ListItem>
            <asp:ListItem Value="42">Konya</asp:ListItem>
            <asp:ListItem Value="43">Kütahya</asp:ListItem>
            <asp:ListItem Value="44">Malatya</asp:ListItem>
            <asp:ListItem Value="45">Manisa</asp:ListItem>
            <asp:ListItem Value="47">Mardin</asp:ListItem>
            <asp:ListItem Value="48">Muðla</asp:ListItem>
            <asp:ListItem Value="49">Muþ</asp:ListItem>
            <asp:ListItem Value="50">Nevþehir</asp:ListItem>
            <asp:ListItem Value="51">Niðde</asp:ListItem>
            <asp:ListItem Value="52">Ordu</asp:ListItem>
            <asp:ListItem Value="53">Rize</asp:ListItem>
            <asp:ListItem Value="54">Sakarya</asp:ListItem>
            <asp:ListItem Value="55">Samsun</asp:ListItem>
            <asp:ListItem Value="56">Siirt</asp:ListItem>
            <asp:ListItem Value="57">Sinop</asp:ListItem>
            <asp:ListItem Value="58">Sivas</asp:ListItem>
            <asp:ListItem Value="59">Tekirdað</asp:ListItem>
            <asp:ListItem Value="60">Tokat</asp:ListItem>
            <asp:ListItem Value="61">Trabzon</asp:ListItem>
            <asp:ListItem Value="62">Tunceli</asp:ListItem>
            <asp:ListItem Value="63">Þanlýurfa</asp:ListItem>
            <asp:ListItem Value="64">Uþak</asp:ListItem>
            <asp:ListItem Value="65">Van</asp:ListItem>
            <asp:ListItem Value="66">Yozgat</asp:ListItem>
            <asp:ListItem Value="67">Zonguldak</asp:ListItem>
            <asp:ListItem Value="68">Aksaray</asp:ListItem>
            <asp:ListItem Value="69">Bayburt</asp:ListItem>
            <asp:ListItem Value="70">Karaman</asp:ListItem>
            <asp:ListItem Value="71">Kýrýkkale</asp:ListItem>
            <asp:ListItem Value="72">Batman</asp:ListItem>
            <asp:ListItem Value="73">Þýrnak</asp:ListItem>
            <asp:ListItem Value="74">Bartýn</asp:ListItem>
            <asp:ListItem Value="75">Ardahan</asp:ListItem>
            <asp:ListItem Value="76">Iðdýr</asp:ListItem>
            <asp:ListItem Value="77">Yalova</asp:ListItem>
            <asp:ListItem Value="78">Karabük</asp:ListItem>
            <asp:ListItem Value="79">Kilis</asp:ListItem>
            <asp:ListItem Value="80">Osmaniye</asp:ListItem>
            <asp:ListItem Value="81">Düzce</asp:ListItem>
        </asp:DropDownList> </td></tr >
            <tr><td >Authority Level : </td><td  class="tdsag" ><asp:DropDownList ID="TBPEyetki" runat="server" Width="130px" Height="19px"> 
                    <asp:ListItem Value="0" Selected="True">Kýlavuz Kaptan</asp:ListItem><asp:ListItem Value="1">Gözcü Darýca</asp:ListItem><asp:ListItem Value="2">Gözcü Yarýmca</asp:ListItem><asp:ListItem Value="3">Þirket Müdürü</asp:ListItem><asp:ListItem Value="4">Þirket Çalýþaný</asp:ListItem><asp:ListItem Value="5">Dýþ Firmalar</asp:ListItem><asp:ListItem Value="6">Stajer Pilot</asp:ListItem><asp:ListItem Value="7">Stajer Gözcü</asp:ListItem><asp:ListItem Value="8">Pilot Botu Kaptaný</asp:ListItem><asp:ListItem Value="9">Admins</asp:ListItem></asp:DropDownList>
                Seniority : <asp:DropDownList ID="TBPEkidem" runat="server" Width="50px" Height="19px">
                    <asp:ListItem Value="0" >1</asp:ListItem><asp:ListItem Value="1">2</asp:ListItem><asp:ListItem Value="2">3</asp:ListItem><asp:ListItem Value="3">4</asp:ListItem><asp:ListItem Value="4">5</asp:ListItem><asp:ListItem Value="5">6</asp:ListItem>
                            </asp:DropDownList></td></tr>
            <tr><td >Work Start: </td><td  class="tdsag" ><asp:TextBox ID="TBPEisegiristarihi" Width="85px" runat="server" MaxLength="10"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBPEisegiristarihi"  CultureName="tr-TR"  ID="MaskedEditExtender2" runat="server" ErrorTooltipEnabled="True" MaskType="Date" DisplayMoney="Left"  AcceptNegative="Left"  Mask="99/99/9999" ClearMaskOnLostFocus="false"></asp:MaskedEditExtender>
                Seniority D:<asp:TextBox ID="TBPEkidemliolmatarihi" Width="85px" runat="server" MaxLength="10"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBPEkidemliolmatarihi"  CultureName="tr-TR"  ID="MaskedEditExtender3" runat="server" ErrorTooltipEnabled="True" MaskType="Date" DisplayMoney="Left"  AcceptNegative="Left"  Mask="99/99/9999" ClearMaskOnLostFocus="false"></asp:MaskedEditExtender></td></tr>

            <tr><td >Retired-Date : </td><td  class="tdsag" > <asp:DropDownList ID="TBPEemekli" runat="server" Width="50px" Height="19px"  OnSelectedIndexChanged="TBPEemekli_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Value="0">No</asp:ListItem><asp:ListItem Value="1">Yes</asp:ListItem> </asp:DropDownList> 
                  <asp:TextBox ID="TBPEemeklitarihi" Width="85px" runat="server" MaxLength="10"></asp:TextBox><asp:MaskedEditExtender TargetControlID="TBPEemeklitarihi"  CultureName="tr-TR"  ID="MaskedEditExtender7" runat="server" ErrorTooltipEnabled="True" MaskType="Date" DisplayMoney="Left"  AcceptNegative="Left"  Mask="99/99/9999"  ClearMaskOnLostFocus="false"></asp:MaskedEditExtender></td></tr>
 
               <tr><td >Watch Id : </td><td  class="tdsag" > <asp:DropDownList ID="TBPEvarid" runat="server" Width="50px" Height="19px">  </asp:DropDownList> Entry-Exit:<asp:DropDownList ID="TBPEgirisistasyon" runat="server" Width="50px" Height="19px"></asp:DropDownList>    <asp:DropDownList ID="TBPEcikisistasyon" runat="server" Width="50px" Height="19px"></asp:DropDownList></td></tr>

            <tr><td >D.Adi: </td><td  class="tdsag" ><asp:TextBox ID="TBEdegismeciadi" Width="120px" runat="server"  Visible="true" CssClass="kucukharf"  MaxLength="30">     </asp:TextBox>  
                D.Kapno:<asp:TextBox ID="TBEdegismecikapno" Width="50px" runat="server" MaxLength="4"  ></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="TBEdegismecikapno" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender> </td></tr>

            <tr><td >D.Soyadi: </td><td  class="tdsag" ><asp:TextBox ID="TBEdegismecisoyadi" Width="120px" runat="server"  CssClass="kucukharf"  MaxLength="30"></asp:TextBox>
                     D.Kidem: <asp:DropDownList ID="DDLdegismeciorgkidem" runat="server" Width="50px" Height="19px">
                         <asp:ListItem Value="0" >1</asp:ListItem><asp:ListItem Value="1">2</asp:ListItem><asp:ListItem Value="2">3</asp:ListItem><asp:ListItem Value="3">4</asp:ListItem><asp:ListItem Value="4">5</asp:ListItem><asp:ListItem Value="5">6</asp:ListItem>
                              </asp:DropDownList></td></tr></td></tr>
    
            <tr><td >Varno: </td><td  class="tdsag" ><asp:TextBox ID="TBEvarno" Width="50px" runat="server" MaxLength="6"  ></asp:TextBox><asp:FilteredTextBoxExtender TargetControlID="TBEvarno" ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"></asp:FilteredTextBoxExtender> 
                Respist:<asp:DropDownList ID="DDLrespist" runat="server" Width="50px" Height="19px"><asp:ListItem Value="0">1</asp:ListItem><asp:ListItem Value="1">2</asp:ListItem></asp:DropDownList>    
                  Durum:<asp:DropDownList ID="DDLdurum" runat="server" Width="50px" Height="19px"> <asp:ListItem Value="0">0</asp:ListItem><asp:ListItem Value="1" >1</asp:ListItem><asp:ListItem Value="2">2</asp:ListItem><asp:ListItem Value="3">3</asp:ListItem><asp:ListItem Value="4">4</asp:ListItem><asp:ListItem Value="5">5</asp:ListItem><asp:ListItem Value="6">6</asp:ListItem><asp:ListItem Value="7">7</asp:ListItem><asp:ListItem Value="8">8</asp:ListItem><asp:ListItem Value="9">9</asp:ListItem></asp:DropDownList></td></tr>

            <tr><td >Son Ýst.Geliþ: </td><td class="tdsag"  >
                <asp:TextBox ID="TBEistasyongelis" runat="server" Width="150px"></asp:TextBox><asp:MaskedEditExtender CultureName="tr-TR" ID="MaskedEditExtender13" runat="server" TargetControlID="TBEistasyongelis" ErrorTooltipEnabled="true" MaskType="DateTime" DisplayMoney="Left" AcceptNegative="Left" Mask="99/99/9999 99:99"></asp:MaskedEditExtender>
                            </td></tr>

               <tr ><td colspan="2" class="tdorta" style="height:50px; text-align:center; font-size:20px; font-weight:bold; font-family:'Trebuchet MS'">
                                <asp:Button ID="LBPEditmode" runat="server" style="height:30px; Width:80px"  OnClick="LBPEditmode_Click" Text="Edit Mode" BorderWidth="1px" BorderColor="#FF6600" BorderStyle="Solid"></asp:Button>
                                <asp:Button ID="ButtonPilotEDT" runat="server" style="height:30px; Width:80px" Text="Save"  CssClass="btn"  OnClick="ButtonPilotEDT_Click"/>&nbsp;&nbsp; 
                                <asp:Button ID="ButtonPilotEDTcancel" runat="server" style="height:30px; Width:80px" Text="Cancel"  CssClass="btn"/>   
                           </td></tr></table>
 </asp:Panel>
            <asp:ModalPopupExtender   
            ID="ModalPopupExtenderPilotEdit" runat="server"  
            CancelControlID="ButtonPilotEDTcancel" 
            TargetControlID="buttonshowpopuppe"  
            PopupControlID="paneleditpilot" 
            BackgroundCssClass="modalbodyarka" ></asp:ModalPopupExtender>
  

            </ContentTemplate></asp:UpdatePanel>
                   

      
      </div>
    </div>
    
</div>
  <div id="footer"></div>    
        <asp:LinkButton ID="LBmainpage" runat="server" Text="Pilot Monitor"  OnClick="LBmainpage_Click"></asp:LinkButton> <br />
        <br />

        <asp:LinkButton ID="LBguvcik" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton>
    
</div>





    
  
    </form>
</body>
</html>
