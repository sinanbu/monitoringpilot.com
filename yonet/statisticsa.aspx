<%@ Page Language="C#" AutoEventWireup="true" CodeFile="statisticsa.aspx.cs" Inherits="yonet_statisticsa" %>

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

            <li class="liistatik1a">  <asp:label ID="liistatik1" runat="server"  >L3 Summary</asp:label></li>
<%--            <li class="liistatik1">  <asp:LinkButton ID="liistatik2" runat="server"  OnClick="liistatik2_Click">2M Summary</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik3" runat="server"  OnClick="liistatik3_Click">1Y Summary</asp:LinkButton></li>--%>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik4" runat="server"  OnClick="liistatik4_Click">Per Pilot</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik5" runat="server"  OnClick="liistatik5_Click">Per Ship</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik6" runat="server"  OnClick="liistatik6_Click">Per Terminal</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik7" runat="server"  OnClick="liistatik7_Click">One Day</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik8" runat="server"  OnClick="liistatik8_Click">One Month</asp:LinkButton></li>
            <li class="liistatik1">  <asp:LinkButton ID="liistatik9" runat="server"  OnClick="liistatik9_Click">One Year</asp:LinkButton></li>
</ul>  </div>

        <div class="clearup">
     
          &nbsp;&nbsp;&nbsp;

               <asp:Label ID="baslik1" runat="server">  <div  class="yazibigbold"> <p></p>
                         Watch:<asp:Label ID="Lwid" runat="server" Text=""></asp:Label>
                         &nbsp;&nbsp;/&nbsp;&nbsp; Start: <asp:Label ID="Lwstart" runat="server" Text=""></asp:Label>
                                                              / Finish: <asp:Label ID="Lwfinish" runat="server" Text=""></asp:Label>
                         &nbsp;&nbsp;/&nbsp;&nbsp; Total Jobs: <asp:Label ID="Ljobs" runat="server"  ForeColor="Red"  Text=""></asp:Label>
                         &nbsp;&nbsp;/&nbsp;&nbsp; Total Work: <asp:Label ID="Lwork" runat="server" ForeColor="Red"  Text=""></asp:Label>
                         &nbsp;&nbsp;/&nbsp;&nbsp; OPAwt: <asp:Label ID="Lowa" runat="server" ForeColor="Red"  Text=""></asp:Label>
                     </div></asp:Label>
                         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" PageSize="200" Width="911px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="31px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                 <asp:TemplateField ControlStyle-Width="200px" HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <asp:Label ID="kapadi" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Jobs Count">
                                     <ItemTemplate>
                                         <asp:Label ID="toj" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Work Hours">
                                     <ItemTemplate>
                                         <asp:Label ID="twh" runat="server" Text='<%# Bind("toplamissuresi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Rest Hours">
                                     <ItemTemplate>
                                         <asp:Label ID="trh" runat="server" Text='<%# Bind("toplamdinlenme") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="One Work Time">
                                     <ItemTemplate>
                                         <asp:Label ID="owa" runat="server" Text='<%# Bind("owa") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="280px" HeaderText="Fatique">
                                     <ItemTemplate>
                                         <div class="container">
                                             <div class="graph">
                                                 <div class="databar" style='width:<%#Eval("Percentage")%>%;'></div>
                                             </div>
                                             <div class="datavalue">
                                                 <%#Eval("yorulma") %>
                                             </div>
                                         </div>
                                     </ItemTemplate>
                                     <HeaderStyle Font-Names="Trebuchet MS" Font-Size="Smaller" HorizontalAlign="Left" VerticalAlign="Middle" />
                                     <ItemStyle />
                                 </asp:TemplateField>


                             </Columns>
                             <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Font-Names="Trebuchet MS" Font-Size="11px"/>
                             <SelectedRowStyle BackColor="#c1cDe1" ForeColor="#333333" />
                         </asp:GridView>

                    
                     <asp:Label ID="baslik2" runat="server"><div  class="yazibigbold"><p>&nbsp;</p>
                             Watch.<asp:Label ID="Lwoid" runat="server" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Start: <asp:Label ID="Lwstartonceki" runat="server" Text=""></asp:Label>
                                                                  / Finish: <asp:Label ID="Lwfinishonceki" runat="server" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Total Jobs: <asp:Label ID="Lojobs" runat="server" ForeColor="Red" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Total Work: <asp:Label ID="Lowork" runat="server" ForeColor="Red" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; OPAwt: <asp:Label ID="Lowao" runat="server" ForeColor="Red"  Text=""></asp:Label>
                         </div></asp:Label>
                         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView2_SelectedIndexChanging" PageSize="200" Width="911px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="31px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                 <asp:TemplateField ControlStyle-Width="200px" HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <asp:Label ID="kapadio" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Jobs Count">
                                     <ItemTemplate>
                                         <asp:Label ID="tojo" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Work Hours">
                                     <ItemTemplate>
                                         <asp:Label ID="twho" runat="server" Text='<%# Bind("toplamissuresi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Rest Hours">
                                     <ItemTemplate>
                                         <asp:Label ID="trho" runat="server" Text='<%# Bind("toplamdinlenme") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderText="One Work Time">
                                     <ItemTemplate>
                                         <asp:Label ID="owao" runat="server" Text='<%# Bind("owa") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                 <asp:TemplateField ControlStyle-Width="280px" HeaderText="Fatique">
                                     <ItemTemplate>
                                         <div class="container">
                                             <div class="graph">
                                                 <div class="databar" style='width:<%#Eval("Percentage")%>%;'></div>
                                             </div>
                                             <div class="datavalue">
                                                 <%#Eval("yorulma") %>
                                             </div>
                                         </div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             </Columns>
                             <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Font-Names="Trebuchet MS" Font-Size="11px"/>
                             <SelectedRowStyle BackColor="#c1cDe1" ForeColor="#333333" />
                         </asp:GridView>
                
                   
                     <asp:Label ID="baslik3" runat="server"><div  class="yazibigbold"> <p>&nbsp;</p>
                             Watch.<asp:Label ID="Lwo2id" runat="server" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Start: <asp:Label ID="Lwstartonceki2" runat="server" Text=""></asp:Label>
                                                                  / Finish: <asp:Label ID="Lwfinishonceki2" runat="server" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Total Jobs: <asp:Label ID="Lo2jobs" runat="server" ForeColor="Red" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; Total Work: <asp:Label ID="Lo2work" runat="server" ForeColor="Red" Text=""></asp:Label>
                             &nbsp;&nbsp;/&nbsp;&nbsp; OPAwt: <asp:Label ID="Lowao2" runat="server" ForeColor="Red"  Text=""></asp:Label>
                         </div></asp:Label>
                         <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="2" DataKeyNames="id" ForeColor="#333333" GridLines="Vertical" OnSelectedIndexChanging="GridView3_SelectedIndexChanging" PageSize="200" Width="911px">
                             <AlternatingRowStyle BackColor="White" />
                             <Columns>
                                 <asp:CommandField ButtonType="Image" HeaderText="" ItemStyle-Width="31px" SelectImageUrl="~/images/arrowr.png" SelectText="" ShowSelectButton="True" />
                                 <asp:TemplateField ControlStyle-Width="200px" HeaderText="Pilot Name">
                                     <ItemTemplate>
                                         <asp:Label ID="kapadio2" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Jobs Count">
                                     <ItemTemplate>
                                         <asp:Label ID="tojo2" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Work Hours">
                                     <ItemTemplate>
                                         <asp:Label ID="twho2" runat="server" Text='<%# Bind("toplamissuresi") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField ControlStyle-Width="100px" HeaderText="Rest Hours">
                                     <ItemTemplate>
                                         <asp:Label ID="trho2" runat="server" Text='<%# Bind("toplamdinlenme") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                  <asp:TemplateField ControlStyle-Width="100px" HeaderText="One Work Time">
                                     <ItemTemplate>
                                         <asp:Label ID="owao2" runat="server" Text='<%# Bind("owa") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                 <asp:TemplateField ControlStyle-Width="280px" HeaderText="Fatique">
                                     <ItemTemplate>
                                         <div class="container">
                                             <div class="graph">
                                                 <div class="databar" style='width:<%#Eval("Percentage")%>%;'></div>
                                             </div>
                                             <div class="datavalue">
                                                 <%#Eval("yorulma") %>
                                             </div>
                                         </div>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                             </Columns>
                             <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="11px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                             <RowStyle BackColor="#EFF3FB"  Font-Names="Trebuchet MS" Font-Size="11px"/>
                             <SelectedRowStyle BackColor="#c1cDe1" ForeColor="#333333" />
                         </asp:GridView>

             

                 </ContentTemplate></asp:UpdatePanel>

      </div>
       
    </div>
    
 
  <div id="footer"></div>   






    
  
    </form>
</body>
</html>
