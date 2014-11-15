<%@ Page Title="" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Events_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        Manage Events Database</h2>
    <br />
    <div class="items">
    <ol>
    <li>
        <asp:HyperLink ID="hlLoadEvents" runat="server" NavigateUrl="~/Events/loadevents.aspx" Text='Load Events from "schedule.txt" file'></asp:HyperLink>
        </li>
    <li>
        <asp:HyperLink ID="hlEditEvents" runat="server" NavigateUrl="~/Events/editevents.aspx" Text="Edit Events"></asp:HyperLink>
        </li>
    <li>
        <asp:HyperLink ID="hlPurgeEvents" runat="server" 
            NavigateUrl="~/Events/PurgeEvents.aspx" Text="Purge Events"></asp:HyperLink>
        </li>
    </ol>
    </div>
    </asp:Content>

