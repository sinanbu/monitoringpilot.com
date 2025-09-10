<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - PILOT INFO</title>
 <link href="css/stil.css" rel="stylesheet" />
    <style type="text/css">
   
      
        .auto-style1 {
            width: 100%; 
                 }

        .selected_row
    {
        background-color:#252a44;
        border-bottom-color:gray;
    }



    </style>

    <script type="text/javascript" src="js/jquery-1.11.2.js"></script>
<script type="text/javascript">
    $(function () {
        $("[id*=DLDarica] td").bind("click", function () {
            var row = $(this).parent();
            $("[id*=DLDarica] tr").each(function () {
                if ($(this)[0] != row[0]) {
                    $("td", this).removeClass("selected_row");
                }
            });
            $("td", row).each(function () {
                if (!$(this).hasClass("selected_row")) {
                    $(this).addClass("selected_row");
                } else {
                    $(this).removeClass("selected_row");
                }
            });
        });
    });


   


</script>

</head>
<body onkeydown = "return (event.keyCode!=13)" >
    <form id="form1" runat="server" >

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

<asp:UpdatePanel ID="UpdatePanelHeader" runat="server" UpdateMode="Conditional" ><ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
           <table  style="width:1340px; height:25px; color:white; background-image:url(images/boslukaltcizgi.png)"><tr  style="height:25px">
            <td > 
          <asp:Label ID="LblVarid" ForeColor="Red" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarid" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarno" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarno" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarbasla" runat="server" Text=""></asp:Label><asp:Literal ID="varbaslar" runat="server" Text="" Visible="false"></asp:Literal> 
          <asp:Label ID="LblVarbit" runat="server" Text=""></asp:Label> <asp:Literal ID="varbiter" runat="server" Text="" Visible="false"></asp:Literal> 
                          
</td></tr>
</table>   </asp:Panel> </ContentTemplate></asp:UpdatePanel>
 <div >
 <asp:Panel ID="summariall" runat="server" >


<table  style="width:1340px; float:right;   color:white">
 <tr  style="height:22px;  border-color:red; ">
     <td style="text-align:left;">
                      <asp:Button ID="ButtonLiveScreen" style="height:20px; Width:110px; font-size:x-small;  text-align:center;" runat="server" Text="Live Screen" OnClick="ButtonLiveScreen_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;&nbsp;</td>
        <td style="text-align:right">

               <asp:ImageButton ID="btnExport" Visible="false"  ImageUrl="~/images/excellogo.png"   style="vertical-align:text-bottom;"   runat="server" ToolTip="Save to Excel"   OnClick="btnExport_Click"  /> &nbsp;
               <asp:ImageButton ID="btnExportdown"  Visible="false" runat="server" ImageUrl="~/images/arrowdown.png" OnClick="btnExportdown_Click" ToolTip="Download the Excel"  />&nbsp;&nbsp;&nbsp;
 

                <asp:LinkButton ID="LBonline" runat="server"  ForeColor="white" OnClick="LBonline_Click"  OnClientClick="this.disabled='true'; "  ></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="white"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "  ></asp:LinkButton>&nbsp;&nbsp;
 </td></tr></table>  



<table  style="width:100% ;"> <tr><td style="text-align:left; width:100% ; height:25px"></td></tr>
<tr><td style="text-align:left; width:100% ;"><span style="font-family:'Trebuchet MS'; color:orange; font-size:small; margin:0; "><b>DAILY JOBS LOG </b><br /><br /></span></td>
</tr></table>  


     
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>

   
      <div class="clearup"> </div>
   <div  style="clear:both; text-align:left;"> 


        


       </div>
                    <div style="height:150px; clear:both; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                 </ContentTemplate></asp:UpdatePanel>

           </asp:Panel>
  

    </div>

    </form>
</body>
</html>
