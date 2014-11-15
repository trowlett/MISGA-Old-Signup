<%@ Page Title="" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="Clear.aspx.cs" Inherits="Signups_Clear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        Clear Marked Entries in Signup Database</h2>
        <p>
            <asp:Label ID="lblMarkedCount" runat="server"></asp:Label>
        </p>
    <p>
        <asp:Label ID="Label1" runat="server" 
            Text="Clear marked Entries from Signup (PlayersList) Database"></asp:Label>
        <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" 
            Text="Do It!" />
            <br />
        <asp:Label ID="lblPurgeCount" runat="server" Text="Entries Purged = 0'"></asp:Label>
    </p>
</asp:Content>

