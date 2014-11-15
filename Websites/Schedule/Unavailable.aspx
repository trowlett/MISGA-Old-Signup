<%@ Page Title="" Language="C#" MasterPageFile="~/mrmisga.master" AutoEventWireup="true" CodeFile="Unavailable.aspx.cs" Inherits="Unavailable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2 style="text-align: center">
        <br />
        Sorry but Signups are temporarily unavailable.&nbsp; 
        <br />
        <br />
        Please check back after 
        <asp:Label ID="lblCheckbackTime" runat="server" Text="Midnight"></asp:Label>
        .</h2>
</asp:Content>

