<%@ Page Title="" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="notification_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<title>Manage Notifications</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="10000" />

        <asp:UpdatePanel ID="StockPricePanel" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" />
        </Triggers>
        <ContentTemplate>
            Stock price is <asp:Label id="StockPrice" runat="server"></asp:Label><br />
            as of <asp:Label id="TimeOfPrice" runat="server"></asp:Label>  
        </ContentTemplate>
        </asp:UpdatePanel>
        <div>
        Page originally created at <asp:Label ID="OriginalTime" runat="server"></asp:Label>
            <br />
            <asp:Calendar ID="Calendar1" runat="server" CellPadding="10" CellSpacing="15" 
                onselectionchanged="Calendar1_SelectionChanged"></asp:Calendar>
        </div>
</asp:Content>

