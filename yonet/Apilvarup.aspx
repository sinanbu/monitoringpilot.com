<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Apilvarup.aspx.cs" Inherits="Apilvarup" EnableEventValidation="false" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot - PORT INFO</title>
 <link href="../css/stil.css" rel="stylesheet" />
    <style type="text/css">
   
      
        .auto-style1 {
            width: 100%; 
                 }

        .selected_row
    {
        background-color:#252a44;
        border-bottom-color:gray;
    }


.kucukharf {
    text-transform:lowercase;
    direction:ltr;
    overflow:auto;
}

    </style>

    <script type="text/javascript" src="../js/jquery-1.11.2.js"></script>
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
           <table  style="width:1340px; height:25px; color:white; background-image:url(../images/boslukaltcizgi.png)"><tr  style="height:25px">
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
       <asp:Button ID="ButtonLiveScreen" style="height:25px; Width:80px; font-size:x-small;  text-align:center;" runat="server" Text="Live Screen" OnClick="ButtonLiveScreen_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;&nbsp;
        <asp:Button ID="adminsayfalar" style="height:25px; Width:120px;  font-size:x-small; text-align:center" runat="server" Text="ADMIN SAYFALAR"   OnClick="adminsayfalar_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;
     </td>
        <td style="text-align:right">
                <asp:LinkButton ID="LBonline" runat="server"  ForeColor="white" OnClick="LBonline_Click"  OnClientClick="this.disabled='true'; "  ></asp:LinkButton> &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="white"  OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "  ></asp:LinkButton>&nbsp;&nbsp;
 </td></tr></table>  



<table  style="width:100% ;"> <tr><td style="text-align:left; width:100% ; height:25px"></td></tr>
<tr><td style="text-align:left; width:100% ;"><span style="font-family:'Trebuchet MS'; color:#222; font-size:small; margin:0; "><b>VARDİYA İÇİ SAATLER TOPLAMLARI GÜNCELLE</b><br /><br /></span></td>
</tr></table>  


     
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>

   
      <div class="clearup"> </div>
   <div  style="clear:both; text-align:left;"> 
                 <asp:DropDownList ID="DDLPilots"   runat="server" Height="24px" Width="200px"  AutoPostBack="true" OnSelectedIndexChanged="DDLPilots_SelectedIndexChanged"  ></asp:DropDownList> </br></br>


       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit"  DataKeyNames="id" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  OnSelectedIndexChanging="GridView1_SelectedIndexChanging"  CellPadding="2" ForeColor="#333333"  GridLines="Vertical"  >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/arrowr.png"  HeaderText=""  SelectText="" ItemStyle-Width="20px"   ShowSelectButton="True" />

                                <asp:TemplateField ControlStyle-Width="40px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                                     <ItemTemplate >
                                         <%# Container.DataItemIndex + 1 %>
                                     </ItemTemplate>
                                 </asp:TemplateField>

<asp:TemplateField HeaderText="Pilot Name"   ControlStyle-Width="255px">
                    <ItemTemplate> <asp:Label ID="Litgemi" runat="server" Text='<%# Bind("kapadisoyadi") %>'></asp:Label> </ItemTemplate>
                </asp:TemplateField>

<asp:TemplateField HeaderText="Watch"  ControlStyle-Width="50px" >
                    <ItemTemplate><asp:Label ID="Litemportlimanadi" runat="server" Text='<%# Bind("varno") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Total Jobs Count" ControlStyle-Width="120px">
                    <EditItemTemplate><asp:TextBox ID="TBciktime" runat="server" Width="200px"  Text='<%# Bind("toplamissayisi") %>'  MaxLength="2"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="TBciktime" runat="server" FilterType="Custom" ValidChars="0123456789"></asp:FilteredTextBoxExtender> </EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litcik" runat="server" Text='<%# Bind("toplamissayisi") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>

               <asp:TemplateField HeaderText="Total Body Fatigue" ControlStyle-Width="120px">
                    <EditItemTemplate><asp:TextBox ID="TBpobtime" runat="server" Width="200px"  Text='<%# Bind("toplamissuresi") %>'  MaxLength="6"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="TBpobtime" runat="server" FilterType="Custom" ValidChars="0123456789,"></asp:FilteredTextBoxExtender> </EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litpob" runat="server" Text='<%# Bind("toplamissuresi") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>

               <asp:TemplateField HeaderText="Total Mind Fatigue" ControlStyle-Width="120px">
                    <EditItemTemplate><asp:TextBox ID="TBpofftime" runat="server" Width="200px"  Text='<%# Bind("toplamdinlenme") %>'  MaxLength="6"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="TBpofftime" runat="server" FilterType="Custom" ValidChars="0123456789,"></asp:FilteredTextBoxExtender> </EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litoff" runat="server" Text='<%# Bind("toplamdinlenme") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>

               <asp:TemplateField HeaderText="Fatigue Percentage" ControlStyle-Width="120px">
                    <EditItemTemplate><asp:TextBox ID="TBontime" runat="server" Width="200px"  Text='<%# Bind("yorulma") %>'  MaxLength="6"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="TBontime" runat="server" FilterType="Custom" ValidChars="0123456789,"></asp:FilteredTextBoxExtender> </EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litgel" runat="server" Text='<%# Bind("yorulma") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>

               <asp:TemplateField HeaderText="Last Watch Fatigue" ControlStyle-Width="120px">
                    <EditItemTemplate><asp:TextBox ID="TBiptal" runat="server" Width="200px"  Text='<%# Bind("yorulmalast") %>'  MaxLength="6"></asp:TextBox><asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="TBiptal" runat="server" FilterType="Custom" ValidChars="0123456789,"></asp:FilteredTextBoxExtender> </EditItemTemplate>
                    <ItemTemplate><asp:Label ID="Litip" runat="server" Text='<%# Bind("yorulmalast") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>


<asp:CommandField  ItemStyle-Width="70px" ShowEditButton="True"  EditImageUrl="../images/edit.png"  EditText=""   UpdateText="" UpdateImageUrl="../images/save.png"  CancelImageUrl="../images/cancel.png"   CancelText=""  ButtonType="Image" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:CommandField>

                
            </Columns>

           
            <EditRowStyle BorderStyle="None" BackColor="#77bbdF"/>
            <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB"   Font-Size="12px"/>
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

        


       </div>
                    <div style="height:150px; clear:both; float:left;">  <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                 </ContentTemplate></asp:UpdatePanel>

           </asp:Panel>
  

    </div>

    </form>
</body>
</html>
