<%@ Page Language="C#" AutoEventWireup="true" CodeFile="statisticsg.aspx.cs" Inherits="yonet_statisticsg" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />
    <style type="text/css">
        .panelmessage {
            width: 300px;
            height: 150px;
            border: 1px groove #111;
            background-color: white;
            margin: 15px;
            font-family: 'Trebuchet MS';
            font-size: x-small;
            text-align: center;
            color: black;
        }

        .modalbodyarka {
            background-color: #333333;
            filter: alpha(opacity:70);
            opacity: 0.6;
            z-index: 10000;
        }
    </style>

<script type="text/javascript">

    function printpage() {
var getpanel = document.getElementById("<%= Panel2.ClientID%>");
var MainWindow = window.open();
MainWindow.document.write("<html><body>");
MainWindow.document.write(getpanel.innerHTML);
MainWindow.document.write("</body></html>");
MainWindow.document.close();
setTimeout(function () {
MainWindow.print();
}, 500);
return false;
}

        function shouldCancelbackspace(e) {
            var key;
            if (e) {
                key = e.which ? e.which : e.keyCode;
                if (key == null || (key != 8 && key != 13)) { // return when the key is not backspace key.
                    return false;
                }
            }
            else {
                return false;
            }


            if (e.srcElement) { // in IE
                tag = e.srcElement.tagName.toUpperCase();
                type = e.srcElement.type;
                readOnly = e.srcElement.readOnly;
                if (type == null) { // Type is null means the mouse focus on a non-form field. disable backspace button
                    return true;
                } else {
                    type = e.srcElement.type.toUpperCase();
                }


            } else { // in FF
                tag = e.target.nodeName.toUpperCase();
                type = (e.target.type) ? e.target.type.toUpperCase() : "";
            }


            // we don't want to cancel the keypress (ever) if we are in an input/text area
            if (tag == 'INPUT' || type == 'TEXT' || type == 'TEXTAREA') {
                if (readOnly == true) // if the field has been dsabled, disbale the back space button
                    return true;
                if (((tag == 'INPUT' && type == 'RADIO') || (tag == 'INPUT' && type == 'CHECKBOX'))
                && (key == 8 || key == 13)) {
                    return true; // the mouse is on the radio button/checkbox, disbale the backspace button
                }
                return false;
            }


            // if we are not in one of the above things, then we want to cancel (true) if backspace
            return (key == 8 || key == 13);
        }


        // check the browser type
        function whichBrs() {
            var agt = navigator.userAgent.toLowerCase();
            if (agt.indexOf("opera") != -1) return 'Opera';
            if (agt.indexOf("staroffice") != -1) return 'Star Office';
            if (agt.indexOf("webtv") != -1) return 'WebTV';
            if (agt.indexOf("beonex") != -1) return 'Beonex';
            if (agt.indexOf("chimera") != -1) return 'Chimera';
            if (agt.indexOf("netpositive") != -1) return 'NetPositive';
            if (agt.indexOf("phoenix") != -1) return 'Phoenix';
            if (agt.indexOf("firefox") != -1) return 'Firefox';
            if (agt.indexOf("safari") != -1) return 'Safari';
            if (agt.indexOf("skipstone") != -1) return 'SkipStone';
            if (agt.indexOf("msie") != -1) return 'Internet Explorer';
            if (agt.indexOf("netscape") != -1) return 'Netscape';
            if (agt.indexOf("mozilla/5.0") != -1) return 'Mozilla';


            if (agt.indexOf('\/') != -1) {
                if (agt.substr(0, agt.indexOf('\/')) != 'mozilla') {
                    return navigator.userAgent.substr(0, agt.indexOf('\/'));
                } else
                    return 'Netscape';
            } else if (agt.indexOf(' ') != -1)
                return navigator.userAgent.substr(0, agt.indexOf(' '));
            else
                return navigator.userAgent;
        }


        // Global events (every key press)


        var browser = whichBrs();
        if (browser == 'Internet Explorer') {
            document.onkeydown = function () { return !shouldCancelbackspace(event); }
        } else if (browser == 'Firefox') {
            document.onkeypress = function (e) { return !shouldCancelbackspace(e); }
        }


<%--        window.onload = SetScroll;

        function SetScroll() {
            var objDiv = document.getElementById("<%=panel2.ClientID%>");
            objDiv.scrollTop = objDiv.scrollHeight;
        }--%>

</script>


</head>

   

<body  onkeydown="return (event.keyCode!=8 || event.keyCode!=13 )">
    <form id="form1" runat="server">
   <div id="main">
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
  <div id="header"> <a href="#" class="logo"><img src="img/logo.png" width="46" height="48" alt="" /></a>
<div id="header-right"><asp:Literal ID="Litpagebaslik" runat="server" ></asp:Literal></div>
   <ul id="top-navigation">
      <li><span><span><a href="yonetim.aspx">Settings</a></span></span></li>
      <li  class="active" ><span><span>Statistics</span></span></li>
      <li><span><span><a href="vacations.aspx">Vacations</a></span></span></li>
      <li><span><span><a href="onlineusers.aspx">Online Users</a></span></span></li>
      <li><span><span><a href="../main.aspx">Pilot Monitor</a></span></span></li>
      <li><span><span> <asp:LinkButton ID="LBguvcik" runat="server" Text="SignOff" OnClick="LBguvcik_Click"></asp:LinkButton></span></span></li>

    </ul>
  </div>
  <div id="middle">

    <div id="left-column-aralik"> <!--  id="ulnavlihover" -->
     
   
</div>

    <div id="center-column-full">       
      <div class="top-bar-full"> 
 <h1><a href="statistics.aspx">Statistics</a></h1>
        <div class="breadcrumbs"></div>
      </div>
      <br />
      <div class="select-bar"> </div>
 <div class="clear"> <!--  id="ulnavlihover" -->
      <ul class="navfull">
          
            <li class="liistatik1">  <asp:LinkButton ID="liistatik1" runat="server"  OnClick="liistatik1_Click">L3 Summary</asp:LinkButton></li>
<%--            <li class="liistatik1">  <asp:LinkButton ID="liistatik2" runat="server"  OnClick="liistatik2_Click">2M Summary</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik3" runat="server"  OnClick="liistatik3_Click">1Y Summary</asp:LinkButton></li>--%>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik4" runat="server"  OnClick="liistatik4_Click">Per Pilot</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik5" runat="server"  OnClick="liistatik5_Click">Per Ship</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik6" runat="server"  OnClick="liistatik6_Click">Per Terminal</asp:LinkButton></li>
            <li class="liistatik1a">  <asp:Label ID="liistatik7" runat="server"  >One Day</asp:Label></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik8" runat="server"  OnClick="liistatik8_Click">One Month</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik9" runat="server"  OnClick="liistatik9_Click">One Year</asp:LinkButton></li>
</ul>  </div>

        <div class="clearup">
     
       
          <br />


            <%--arama radio no.7- One Day--%>
    <asp:TextBox ID="TextBox7" runat="server" MaxLength="10"    Font-Size="Small" Width="90px" Height="22px" ></asp:TextBox>
<asp:MaskedEditExtender  CultureName="tr-TR"  ID="MaskedEditExtender7" runat="server" TargetControlID="TextBox7" ErrorTooltipEnabled="true" MaskType="Date" DisplayMoney="Left"  AcceptNegative="Left" Mask="99/99/9999"></asp:MaskedEditExtender>
  

           <asp:Button ID="LBgetist3" runat="server" OnClick="LBgetist3_Click" Text="Jobs Details" Width="100px" Height="24px"></asp:Button>
           <asp:Button ID="LBjsr" runat="server" OnClick="LBjsr_Click" Text="Status Report" Width="100px" Height="24px"></asp:Button>
<div style="float:right;">           <asp:Button ID="LBworkrest" runat="server"   OnClick="LBworkrest_Click" Text="W/R Hours" Width="100px" Height="24px"></asp:Button>
    </div>
      <asp:Panel ID="Panel1" runat="server" DefaultButton="LBgetist3" Visible="false">
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
               <ContentTemplate>

                         <asp:Label ID="baslik7" runat="server" > <div  class="yazibigbold"><p></p>
                             <asp:Label ID="Lwoidgunluk" runat="server"  ></asp:Label> <asp:Label ID="Lbosluk" runat="server" Text=" "></asp:Label> 
                             
                             <asp:LinkButton ID="LBgeri" runat="server" OnClick="LBgeri_Click" Font-Size="10px" Text="<<" BackColor="#9fc0c1" BorderStyle="Solid" BorderWidth="1px" Font-Underline="false"></asp:LinkButton> 
                             <asp:Label ID="Ldikbol" runat="server" Width="11px" Text=" "></asp:Label> 
                             <asp:LinkButton ID="LBileri" runat="server" OnClick="LBileri_Click"  Font-Size="10px"  Text=">>" BackColor="#c9dcdc"  BorderStyle="Solid" BorderWidth="1px" Font-Underline="false" ></asp:LinkButton> </div></asp:Label>
                       
                         <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView7_SelectedIndexChanging"  Width="911px" AllowPaging="True" PageSize="50" OnPageIndexChanging="GridView7_PageIndexChanging">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="24px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                
                                  <asp:TemplateField ControlStyle-Width="32px" HeaderText="No">
                                     <ItemTemplate>
                                         <asp:Label ID="siraod" runat="server" Text='<%# Container.DataItemIndex +1   %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="130px" HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <asp:Label ID="pilotod" runat="server" Text='<%# Bind("pilotismi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="125px" HeaderText="Ship Name">
                                     <ItemTemplate>
                                         <asp:Label ID="shipod" runat="server"  Text='<%# Bind("gemiadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="60px" HeaderText="Type/Grt">
                                     <ItemTemplate>
                                         <asp:Label ID="tgrtod" runat="server" ><%#Eval("tipi")%>/<%#Eval("grt")%></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="125px" HeaderText="Departure">
                                     <ItemTemplate>
                                         <asp:Label ID="depod" runat="server" ><%#Eval("binisyeri") %> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="125px" HeaderText="Destination">
                                     <ItemTemplate>
                                         <asp:Label ID="destod" runat="server" ><%#Eval("inisyeri") %> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="118px" HeaderText="Off Station ">
                                     <ItemTemplate>
                                         <asp:Label ID="sofod" runat="server" ><%#Eval("istasyoncikis") %> </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="117px" HeaderText="On Station">
                                     <ItemTemplate>
                                         <asp:Label ID="sonod" runat="server" ><%#Eval("istasyongelis")%>  </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                   <asp:TemplateField ControlStyle-Width="20px" HeaderText="X" >
                                     <ItemTemplate>
                                         <asp:Label ToolTip='<%#Eval("manevraiptal") as string == "1"  ? "" : "Cancelled" %>' ID="cancelod" runat="server" ><%#Eval("manevraiptal") %></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>
                             <HeaderStyle BackColor="#507CD1" Font-Names="Trebuchet MS" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Font-Names="Trebuchet MS" Font-Size="11px" />
                             <SelectedRowStyle BackColor="#c1cDe1"  ForeColor="#333333" />
                         </asp:GridView>



                 </ContentTemplate>

                     <Triggers>
                         <asp:PostBackTrigger ControlID="LBgeri" />
                          <asp:PostBackTrigger ControlID="LBileri" />
                     </Triggers>
                 </asp:UpdatePanel>
          </asp:Panel>

       <asp:Panel runat="server" ID="Panel2" Visible= "false" >
             
                         <div  class="yazibigbold"  ><p></p>
                             <asp:Label ID="Lwoidgunluk2" runat="server"  ></asp:Label> 
                             <asp:Label ID="Lbosluk2" runat="server" Text=" "></asp:Label> 
                             
                             <asp:LinkButton ID="LBgeri2" runat="server" OnClick="LBgeri2_Click" Font-Size="10px" Text="<<" BackColor="#afc0c1" BorderStyle="Solid" BorderWidth="1px" Font-Underline="false"></asp:LinkButton> 
                             <asp:Label ID="Ldikbol2" runat="server" Width="11px" Text=" "></asp:Label> 
                             <asp:LinkButton ID="LBileri2" runat="server" OnClick="LBileri2_Click"  Font-Size="10px"  Text=">>" BackColor="#d9dcdc"  BorderStyle="Solid" BorderWidth="1px" Font-Underline="false" ></asp:LinkButton> 

                             <asp:Label ID="Ldikbol5" runat="server" Width="60px" Text=" "></asp:Label> 
                             <asp:Button ID="TBcancel" runat="server" Width="70px"  OnClick="TBcancel_Click" Text="Cancel" Visible="false"  Font-Size="12px" ></asp:Button>
                             <asp:Button ID="LBupdate" runat="server"  Width="70px"  Font-Size="12px"  OnClick="LBupdate_Click"  Text="&nbsp;&nbsp;Update&nbsp;&nbsp;" ></asp:Button>


                             <asp:Label ID="Ldikbol3" runat="server" Width="7px" Text=" "></asp:Label> 
                             <asp:Button ID="LBsendemail" runat="server"  Font-Size="12px"  Text="&nbsp;&nbsp;Send E-mail&nbsp;&nbsp;"  OnClick="LBsendemail_Click"  ></asp:Button>

                             <asp:Label ID="Ldikbol4" runat="server" Width="7px" Text=" "></asp:Label> 
                             <asp:Button ID="LBjsrprint" runat="server"  Font-Size="12px"  OnClick="LBjsrprint_Click" Text="&nbsp;&nbsp;PDF/Print&nbsp;&nbsp;" ></asp:Button>
<%--OnClientClick="return printpage();" --%>
</div>
                       <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
               <ContentTemplate>
                   <br />

                                <table  style="width: 620px; height:400px; padding:2px; border:1px solid blue; background-color:white;" >
                 <tr>
                     <td  colspan="5" style="text-align:right"><asp:Label ID="TBSatir1" runat="server" Height="25px"  Width="90px" CssClass="textbox"></asp:Label></td>
                 </tr>
                 <tr>
                     <td colspan="5" style="text-align:right;  font-weight:bold; font-style:italic;">Darýca / Yarýmca</td>
                 </tr>
                 <tr>
                     <td colspan="5" style="text-align:center; font-weight:bold; font-size:14px; text-decoration:underline;">ÝÞ DURUMU RAPORU</td>
                 </tr>
                 <tr>
                     <td colspan="5" style="text-align:center; font-weight:bold;"><asp:Label ID="Labelsatir4" runat="server" ></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="text-align:left">Toplam Ýþ</td>
                     <td style="text-align:center; " >&nbsp;:&nbsp;</td>
                     <td  colspan="3" style="text-align:left;  Height:25px " >
                         <asp:Label ID="TBsatir5" runat="server"  Width="150px"   ></asp:Label>
                         <asp:TextBox ID="TTBsatir5" runat="server"  Width="150px" CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td colspan="5" style="text-align:center; font-weight:bold;  Height:25px "><asp:Label ID="Labelsatir6" runat="server" ></asp:Label></td>
                 </tr>
                 <tr>
                     <td style="text-align:left">Yanaþýk gemilerin sayýsý</td>
                     <td style="text-align:center; " >&nbsp;:&nbsp;</td>
                     <td  colspan="3" style="text-align:left;  Height:25px ">
                         <asp:Label ID="TBsatir7" runat="server" Width="150px"   ></asp:Label>
                         <asp:TextBox ID="TTBsatir7" runat="server" Width="150px" CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style="text-align:left">Eskihisar ve Yalova demirdeki gemi sayýsý  </td>
                     <td style="text-align:center; " >&nbsp;:&nbsp;</td>
                     <td  colspan="3" style="text-align:left;  Height:25px ">
                         <asp:Label ID="TBsatir8" runat="server" Width="150px"   ></asp:Label>
                         <asp:TextBox ID="TTBsatir8" runat="server" Width="150px" CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style="text-align:left">Hereke demirdeki gemi sayýsý</td>
                     <td style="text-align:center; " >&nbsp;:&nbsp;</td>
                     <td  colspan="3" style="text-align:left;  Height:25px ">
                         <asp:Label ID="TBsatir9" runat="server" Width="150px"  ></asp:Label>
                         <asp:TextBox ID="TTBsatir9" runat="server" Width="150px"  CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>                         
                     </td>
                 </tr>
                 <tr>
                     <td style="text-align:left">Yarýmca ve Ýzmit  demirdeki gemilerin sayýsý  </td>
                     <td style="text-align:center; " >&nbsp;:&nbsp;</td>
                     <td  colspan="3" style="text-align:left;  Height:25px ">
                         <asp:Label ID="TBsatir10" runat="server" Width="150px"   ></asp:Label>
                         <asp:TextBox ID="TTBsatir10" runat="server" Width="150px"  CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style="text-align:left">Beklenen gemi sayýsý</td>
                     <td style="text-align:center; " >&nbsp;:&nbsp;</td>
                     <td  colspan="3" style="text-align:left;  Height:25px ">
                         <asp:Label ID="TBsatir11" runat="server" Width="150px"   ></asp:Label>
                         <asp:TextBox ID="TTBsatir11" runat="server" Width="150px"  CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style="text-align:left">Son talep numarasý</td>
                     <td style="text-align:center; " >:</td>
                     <td style="text-align:left;  Height:25px " >
                         <asp:Label ID="TBsatir12a" runat="server" Width="70px"  ></asp:Label>
                         <asp:TextBox ID="TTBsatir12a" runat="server" Width="70px" CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>
                         <asp:Label ID="Label1" runat="server" Text=" / " Width="10px" ></asp:Label>
                     </td>
                     <td style="text-align:left; " >
                         <asp:Label ID="TBsatir12b" runat="server" Width="70px"  ></asp:Label>
                         <asp:TextBox ID="TTBsatir12b" runat="server" Width="70px" CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>
                         <asp:Label ID="Label2" runat="server" Text=" / " Width="10px" ></asp:Label>

                     </td>
                     <td style="text-align:left; " >
                         <asp:Label ID="TBsatir12c" runat="server" Width="70px" ></asp:Label>
                         <asp:TextBox ID="TTBsatir12c" runat="server" Width="70px" CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>                         

                     </td>
                 </tr>
                 <tr>
                     <td  style="text-align:center; width:300px;" >&nbsp;</td>
                     <td  style="text-align:center; width:20px;" >&nbsp;</td>
                     <td  style="text-align:center; width:100px;" >&nbsp;</td>
                     <td  style="text-align:center; width:100px;" >&nbsp;</td>
                     <td  style="text-align:center; width:100px;" >&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="text-align:left">Dekaþ</td>
                     <td style="text-align:center; " >&nbsp;:&nbsp;</td>
                     <td  colspan="3" style="text-align:left;  Height:25px ">
                         <asp:Label ID="TBsatir14" runat="server" Width="150px"   ></asp:Label>
                         <asp:TextBox ID="TTBsatir14" runat="server" Width="150px" CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>
                 </tr>
                 <tr>
                     <td style="text-align:left">Derince</td>
                     <td style="text-align:center; " >&nbsp;:&nbsp;</td>
                     <td  colspan="3" style="text-align:left;  Height:25px ">
                         <asp:Label ID="TBsatir15" runat="server" Width="150px" ></asp:Label>
                         <asp:TextBox ID="TTBsatir15" runat="server" Width="150px" CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>                         
</td>
                 </tr>
                 <tr>
                     <td style="text-align:left">Çalýþan Pilot Sayýsý</td>
                     <td style="text-align:center; " >&nbsp;:&nbsp;</td>
                     <td  colspan="3" style="text-align:left;  Height:25px ">
                         <asp:Label ID="TBsatir16" runat="server" Width="150px"  ></asp:Label>
                         <asp:TextBox ID="TTBsatir16" runat="server" Width="150px" CssClass="textbox" Visible="false" Height="20px" ></asp:TextBox>
                         <asp:Label ID="Label3" runat="server" Width="50px"  ></asp:Label>
                             <asp:Button ID="LBsave" runat="server"  Width="70px"  Font-Size="12px"  ForeColor="Red" OnClick="LBsave_Click" Text="&nbsp;&nbsp;Save&nbsp;&nbsp;"  Visible="false"></asp:Button>
</td>
                 </tr>
             </table>



</ContentTemplate>

                                      <Triggers>
                         <asp:PostBackTrigger ControlID="LBgeri2" />
                          <asp:PostBackTrigger ControlID="LBileri2" />
                     </Triggers>

             </asp:UpdatePanel>
 </asp:Panel>


                                    <%--  update modal  baþlar    --%>
                        <asp:Button ID="buttonMessagePanelok1" runat="server" Style="display: none;" />
                        <asp:Panel ID="panelMessagePanelok1" Style="display: none;" CssClass="panelmessage" runat="server">
                            <div style="text-align: center; font-weight: bold;">
                                <br />
                                <img src="../images/sending.gif" /> 
                            </div>
                           
                             <br /><br />

                        </asp:Panel>
                        <asp:ModalPopupExtender ID="ModalPopupMessageok1" runat="server"
                            TargetControlID="buttonMessagePanelok1"
                            PopupControlID="panelMessagePanelok1"
                            BackgroundCssClass="modalbodyarka">
                        </asp:ModalPopupExtender>
                        <%-- modal message ok biter --%>

                        <%--   modal message ok baþlar    --%>
                        <asp:Button ID="buttonMessagePanelok" runat="server" Style="display: none;" />
                        <asp:Panel ID="panelMessagePanelok" Style="display: none;" CssClass="panelmessage" runat="server">
                            <div style="text-align: center; font-weight: bold;">
                                <br /><br /><asp:Label ID="mailmes" Text="" runat="server"></asp:Label><br /><br /><br />
                            </div>
                            <asp:Button ID="Bclosedok" runat="server" Style="height: 30px; Width: 80px"  OnClick="Bclosedok_Click" Text="Close" />
                            <br /><br />

                        </asp:Panel>
                        <asp:ModalPopupExtender ID="ModalPopupMessageok" runat="server"
                            TargetControlID="buttonMessagePanelok"
                            PopupControlID="panelMessagePanelok"
                            BackgroundCssClass="modalbodyarka">
                        </asp:ModalPopupExtender>
                        <%-- modal message ok biter --%>

      </div>
       
  </div>
  
  <div id="footer"></div>   


        </div>

    </div>
    </form>

      <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(showPopup); 
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(hidePopup);    

        function showPopup(sender, args){
         var ModalControl ='<%= ModalPopupMessageok1.ClientID %>';   
            $find(ModalControl).show();        
        } 

        function hidePopup(sender, args) { 
            var ModalControl ='<%= ModalPopupMessageok1.ClientID %>'; 
            $find(ModalControl).hide(); 
        } 
    </script>

</body>
</html>
