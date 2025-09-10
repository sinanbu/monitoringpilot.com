<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Alivescreensil.aspx.cs" Inherits="Alivescreensil" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MonitoringPilot</title>
    <link href="../css/stil.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .selected_row {
            background-color: #252a44;
            border-bottom-color: gray;
        }


        .kucukharf {
            text-transform: lowercase;
            direction: ltr;
            overflow: auto;
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
<body onkeydown="return (event.keyCode!=13)">
    <form id="form1" runat="server">

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

        <asp:UpdatePanel ID="UpdatePanelHeader" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
                    <table style="width: 1340px; height: 25px; color: white; background-image: url(../images/boslukaltcizgi.png)">
                        <tr style="height: 25px">
                            <td>
                                <asp:Label ID="LblVarid" ForeColor="Red" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarid" runat="server" Text="" Visible="false"></asp:Literal>
                                <asp:Label ID="LblVarno" runat="server" Text=""></asp:Label><asp:Literal ID="varbilvarno" runat="server" Text="" Visible="false"></asp:Literal>
                                <asp:Label ID="LblVarbasla" runat="server" Text=""></asp:Label><asp:Literal ID="varbaslar" runat="server" Text="" Visible="false"></asp:Literal>
                                <asp:Label ID="LblVarbit" runat="server" Text=""></asp:Label>
                                <asp:Literal ID="varbiter" runat="server" Text="" Visible="false"></asp:Literal>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <asp:Panel ID="summariall" runat="server">


                <table style="width: 1340px; float: right; color: white">
                    <tr style="height: 22px; border-color: red;">
                        <td style="text-align: left;">
                            <asp:Button ID="ButtonLiveScreen" Style="height: 25px; Width: 80px; font-size: x-small; text-align: center;" runat="server" Text="Live Screen" OnClick="ButtonLiveScreen_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';" />
                            <asp:Button ID="adminsayfalar" style="height:25px; Width:120px;  font-size:x-small; text-align:center" runat="server" Text="ADMIN SAYFALAR"   OnClick="adminsayfalar_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Wait..';"   /> &nbsp;&nbsp;
                           &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:LinkButton ID="LBonline" runat="server" ForeColor="white" OnClick="LBonline_Click" OnClientClick="this.disabled='true'; "></asp:LinkButton>
                            &nbsp;&nbsp
                <asp:LinkButton ID="LBonlineoff" Text="SignOff" ForeColor="white" OnClick="LBonlineoff_Click" runat="server" OnClientClick="this.disabled='true'; "></asp:LinkButton>&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>



                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left; width: 100%; height: 25px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 100%;"><span style="font-family: 'Trebuchet MS'; color: #222; font-size: small; margin: 0;"><b>CANLI MANEVRA İŞ DETAYLARI GÜNCELLE</b><br />
                            <br />
                        </span></td>
                    </tr>
                </table>



                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>


                        <div class="clearup"></div>
                        <div style="clear: both; text-align: left;">


       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCancelingEdit="GridView1_RowCancelingEdit" DataKeyNames="kapno" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="2" ForeColor="#333333" GridLines="Vertical">
           <AlternatingRowStyle BackColor="White" />
           <Columns>

               <asp:CommandField ButtonType="Image" SelectImageUrl="../images/arrowr.png" HeaderText="" SelectText="" ItemStyle-Width="20px" ShowSelectButton="True" />

               <asp:TemplateField ControlStyle-Width="20px" ItemStyle-HorizontalAlign="center" HeaderText="No">
                   <ItemTemplate>
                       <%# Container.DataItemIndex + 1 %>
                   </ItemTemplate>
               </asp:TemplateField>


               <asp:TemplateField HeaderText="Pilot Adı" ControlStyle-Width="100px">
                   <ItemTemplate>
                       <asp:Label ID="Lkapadi" runat="server" Text='<%# Bind("kapadi") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Soyadı" ControlStyle-Width="100px">
                   <ItemTemplate>
                       <asp:Label ID="Lkapsoyadi" runat="server" Text='<%# Bind("kapsoyadi") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Gemi Adı" ControlStyle-Width="100px">
                   <ItemTemplate><asp:Label ID="Lgemiadi" runat="server" Text='<%# Bind("gemiadi") %>'></asp:Label></ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Durum" ControlStyle-Width="130px">
                   <EditItemTemplate><asp:TextBox ID="TBistasyoncikis" runat="server" Text='<%# Bind("durum") %>'  ></asp:TextBox></EditItemTemplate>
                   <ItemTemplate>    <asp:Label ID="Listasyoncikis" runat="server" Text='<%# Bind("durum") %>'></asp:Label></ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Respist" ControlStyle-Width="130px">
                   <EditItemTemplate><asp:TextBox ID="TBpob" runat="server" Text='<%# Bind("respist") %>'  ></asp:TextBox></EditItemTemplate>
                   <ItemTemplate>    <asp:Label ID="Lpob" runat="server" Text='<%# Bind("respist") %>'></asp:Label></ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="PoFF" ControlStyle-Width="130px">
                   <ItemTemplate>    <asp:Label ID="Lpoff" runat="server" Text='<%# Bind("poff") %>'></asp:Label></ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="İstasyon Geliş" ControlStyle-Width="130px">
                   <EditItemTemplate><asp:TextBox ID="TBistasyongelis" runat="server" Text='<%# Bind("istasyongelis") %>'  ></asp:TextBox></EditItemTemplate>
                   <ItemTemplate>    <asp:Label ID="Listasyongelis" runat="server" Text='<%# Bind("istasyongelis") %>'></asp:Label></ItemTemplate>
               </asp:TemplateField>


               <asp:CommandField ItemStyle-Width="60px" ShowEditButton="True" EditImageUrl="../images/edit.png" EditText="" UpdateText="" UpdateImageUrl="../images/save.png" CancelImageUrl="../images/cancel.png" CancelText="" ButtonType="Image">
                   <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
               </asp:CommandField>


           </Columns>


           <EditRowStyle BorderStyle="None" BackColor="#77bbdF" />
           <HeaderStyle BackColor="#507CD1" Font-Names="Arial" Font-Size="12px" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
           <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
           <RowStyle BackColor="#EFF3FB" Font-Size="12px" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
           <SortedAscendingCellStyle BackColor="#F5F7FB" />
           <SortedAscendingHeaderStyle BackColor="#6D95E1" />
           <SortedDescendingCellStyle BackColor="#E9EBEF" />
           <SortedDescendingHeaderStyle BackColor="#4870BE" />
       </asp:GridView>




                        </div>
                        <div style="height: 150px; clear: both; float: left;">
                            <asp:Label ID="Label1" runat="server" Text=" &nbsp;" ForeColor="Black" Height="50px"></asp:Label></div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>


        </div>

    </form>
</body>
</html>
