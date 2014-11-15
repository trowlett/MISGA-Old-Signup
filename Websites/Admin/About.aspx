<%@ Page Title="About Us" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true"
    CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About 
        <asp:Literal ID="Literal1" runat="server" Text="<%$ AppSettings:Org %>"></asp:Literal>&nbsp;Administration Web Site
    </h2>
    <p>
        Web site is used to maintain the database for the 
        <asp:Literal ID="Literal2" runat="server" Text="<%$ AppSettings:Org %>"></asp:Literal>&nbsp;Web Site 
        <asp:HyperLink ID="HyperLink1" runat="server" Text="<%$ AppSettings:OrgURL %>" 
            NavigateUrl="<%$ AppSettings:OrgURL %>"></asp:HyperLink>
        </p>
</asp:Content>
