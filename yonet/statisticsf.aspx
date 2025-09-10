<%@ Page Language="C#" AutoEventWireup="true" CodeFile="statisticsf.aspx.cs" Inherits="yonet_statisticsf" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>PMTR</title>

    <link href="css/all.css" rel="stylesheet" />

<%--<script type="text/javascript">
        function shouldCancelbackspace(e) {
            var key;
            if (e) {
                key = e.which ? e.which : e.keyCode;
                if (key == null || (key != 8 && key != 13)) { // return when the key is not backspace key.
                    return false;
                }
            } else {
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


</script>--%>
<script type="text/javascript">
    //var status = document.getElementById("TextBox8");
    //status.style.visibility = 'hidden';
    // document.getElementById("TextBox8").focus();

    //function cancelBack() {
    //    if ((event.keyCode == 8 ||
    //       (event.keyCode == 37 && event.altKey) ||
    //       (event.keyCode == 39 && event.altKey))
    //        &&
    //       (event.srcElement.form == null || event.srcElement.isTextEdit == false)
    //      ) {
    //        event.cancelBubble = true;
    //        event.returnValue = false;
    //    }
    //}


    //$(document).on("keydown", function (event) {
    //    // Chrome & Firefox || Internet Explorer
    //    if (document.activeElement === document.body || document.activeElement === document.body.parentElement) {
    //        // SPACE (32) o BACKSPACE (8)
    //        if (event.keyCode === 32 || event.keyCode === 8) {
    //            event.preventDefault();
    //        }
    //    }
    //});

    //$(document).on("keydown", function (e) {
    //    if (e.which === 8 && !$(e.target).is("input[type='text']:not([readonly]), textarea")) {
    //        e.preventDefault();
    //    }
    //});

</script>


</head>

   

<body >
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

                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>

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
            <li class="liistatik1a">  <asp:label ID="liistatik6" runat="server"  >Per Terminal</asp:label></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik7" runat="server"  OnClick="liistatik7_Click">One Day</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik8" runat="server"  OnClick="liistatik8_Click">One Month</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik9" runat="server"  OnClick="liistatik9_Click">One Year</asp:LinkButton></li></ul>  </div>

        <div class="clearup">
     
              <br />

            <asp:DropDownList ID="DDLPilots"   runat="server" Height="24px" Width="200px"   ></asp:DropDownList>

            <asp:Button ID="LBgetist3" runat="server" OnClick="LBgetist3_Click" Text="Get Data" Width="90px" Height="24px"></asp:Button>
 
        
                    <asp:Label ID="baslik6" runat="server"> <div  class="yazibigbold"><p></p>
                             <asp:Label ID="Lwoidpp" runat="server" ></asp:Label> </div></asp:Label> 
                        
                         <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView6_SelectedIndexChanging" Width="911px" AllowPaging="True" PageSize="50" OnPageIndexChanging="GridView6_PageIndexChanging">
 
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="31px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                
                                  <asp:TemplateField ControlStyle-Width="130px" HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <asp:Label ID="shippp" runat="server" Text='<%# Bind("pilotismi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="110px" HeaderText="Ship Name">
                                     <ItemTemplate>
                                         <asp:Label ID="flagpp" runat="server"  Text='<%# Bind("gemiadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="80px" HeaderText="Type/Grt">
                                     <ItemTemplate>
                                         <asp:Label ID="tgrtpp" runat="server" ><%#Eval("tipi")%>/<%#Eval("grt")%></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="140px" HeaderText="Departure">
                                     <ItemTemplate>
                                         <asp:Label ID="deppp" runat="server" ><%#Eval("binisyeri") %> <%#Eval("binisrihtim") as string == "0" || Eval("binisrihtim") as string == ""  ? "" : "/" + Eval("binisrihtim")   %></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="140px" HeaderText="Destination">
                                     <ItemTemplate>
                                         <asp:Label ID="destpp" runat="server" ><%#Eval("inisyeri") %> <%#Eval("inisrihtim")  as string == "0" || Eval("inisrihtim") as string == ""  ? "" : "/" +  Eval("inisrihtim")  %></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="118px" HeaderText="Pob">
                                     <ItemTemplate>
                                         <asp:Label ID="sofpp" runat="server" ><%#Eval("pob") %> </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="117px" HeaderText="Poff">
                                     <ItemTemplate>
                                         <asp:Label ID="sonpp" runat="server" ><%#Eval("poff")%>  </asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                   <asp:TemplateField ControlStyle-Width="20px" HeaderText="X" >
                                     <ItemTemplate>
                                         <asp:Label  ID="cancelpp" runat="server" ><%#Eval("manevraiptal") %></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                             </Columns>
                             <HeaderStyle BackColor="#507CD1" Font-Names="Trebuchet MS" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Font-Names="Trebuchet MS" Font-Size="11px" />
                             <SelectedRowStyle BackColor="#c1cDe1"  ForeColor="#333333" />
                         </asp:GridView>

            </div>
                 </ContentTemplate></asp:UpdatePanel>

 
   
   
    </div>  
  </div>
  <div id="footer"></div>   


 
    </div>





    
  
    </form>
</body>
</html>
