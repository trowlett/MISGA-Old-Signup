<%@ Page Title="" Language="C#" Debug="true" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Maintenance_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table style="width: 100%;">
    <tr>
    <td style="width: 70%;">
        <h2>Site Maintenace Main Page</h2></td>
    <td>
        <h4 style="text-align: right;"> 
        Jan. 25, 2014
        Update
        </h4></td></tr></table>
    <div class="items">
    <ol>
        <li>
        <asp:HyperLink ID="hlPlayersList" runat="server" Text="View Players Signed Up for an Event" NavigateUrl="~/Signups/Default.aspx"></asp:HyperLink>
        </li>
        <li>
        <asp:HyperLink ID="hlManageEvents" runat="server" Text="Manage Events Database" NavigateUrl="~/Events/Default.aspx"></asp:HyperLink>
        </li>
        <li>
        <asp:HyperLink ID="hlMembersDatabase" runat="server" Text="Edit Members Database" NavigateUrl="~/Members/editmember.aspx"></asp:HyperLink>
        </li>
        <li>
        <asp:HyperLink ID="hlLastDate" runat="server" Text="Set the First and Last Dates of the current year's season" NavigateUrl="~/LastDate/LastDate.aspx"></asp:HyperLink>
        </li>
        <li>
        <asp:HyperLink ID="hlSelectDelete" runat="server" 
                Text="Selectively Delete Members with No Current Signups" 
                NavigateUrl="~/InactiveMembers.aspx"></asp:HyperLink>
        </li>
        <li>
        <asp:HyperLink ID="hlClearSignups" runat="server" Text="Purge Marked Records in Signup List (PlayersList) Database." NavigateUrl="~/Signups/Clear.aspx"></asp:HyperLink>
        </li>
        <li>
        <asp:HyperLink ID="hlUpdateHandicaps" runat="server" NavigateUrl="~/Members/updatehandicaps.aspx" Text="Update Member's Handicaps."></asp:HyperLink>
        </li>
<!--        <li>
            <asp:HyperLink ID="hlNotification" runat="server" NavigateUrl="~/notification/Default.aspx" Visible="True">Manage Notifications</asp:HyperLink>
        </li> -->

<!--        <li>more Things To Do</li> -->
    </ol>
        <p>
            Go to the 
            <asp:HyperLink ID="hlOrgURL" runat="server" 
                NavigateUrl="<%$ AppSettings:OrgURL %>" 
                Text="<%$ AppSettings:WebSiteName %>"></asp:HyperLink>
             
        </p>
    </div>
    <br />
    <br />
    
    <br />

    </asp:Content>

