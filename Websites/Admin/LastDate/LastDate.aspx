<%@ Page Title="" Language="C#" Debug="true" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="LastDate.aspx.cs" Inherits="Administration_ChangeDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
#last 
{
    width: 45%;
    float: right;
    border: 1px solid silver;
    padding: 10px 20px 10px 20px;
}
#last h3 { text-align: center; font-weight: bold;}

#first
{
    width: 45%;
    float: left;
    border: 1px solid silver;
    padding: 10px 20px 10px 20px;
}
#first h3 { text-align: center; font-weight: bold;}

#bottom
{
    clear: both;
    padding: 10px 20px 10px 20px;
    text-align: center;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <h2>Administrative Task:  Change First &amp; Last Date</h2>
    <p>Change the date of the first Signup allowed and the last date of the season</p>
    <p>
    <asp:Label ID="lblDateLastChanged" runat="server" Text="Date Last Changed:  "></asp:Label></p>
<div id="first">
<h3>First Date Parameter</h3>
    <p>
        <asp:Label ID="lblCurrentFirstDate" runat="server" Text="Current First Date: "></asp:Label>
        <asp:TextBox ID="tbFirstDate" runat="server" ReadOnly="true"></asp:TextBox>
        </p>
    <p>
        <asp:Label ID="lblNewFirstDate" runat="server" Text="New First Date: "></asp:Label>
        <asp:TextBox ID="tbNewFirstDate" runat="server"></asp:TextBox>

        </p>

        <asp:Calendar ID="CalFirstDate" runat="server" 
            onselectionchanged="CalFirstDate_SelectionChanged" 
            Caption="Select First Date" CaptionAlign="Top"></asp:Calendar>

    </div>
<div id="last">
<h3>Last Date Parameter</h3>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Current Last Date: "></asp:Label>
        <asp:TextBox ID="tbLastDate" runat="server" ReadOnly="True"></asp:TextBox>
        </p>
    <p>
        <asp:Label ID="lblNewLastDate" runat="server" Text="New Last Date: "></asp:Label>
        <asp:TextBox ID="tbNewLastDate" runat="server"></asp:TextBox>
        </p>

            <asp:Calendar ID="CalLastDate" runat="server" 
                onselectionchanged="CalLastDate_SelectionChanged" Caption="Select Last Date"></asp:Calendar>

</div>
<div id="bottom">
         <p>

        <asp:Button ID="btnChangeDate" runat="server" onclick="btnChangeDate_Click" 
            Text="Change" />
            </p>
        <p>
            <asp:Label ID="lblDateChanged" runat="server"></asp:Label>
            </p>
        <br />
        <br />
        <br />
    <asp:Panel ID="AdminPanelReturn" BackColor="AntiqueWhite" runat="server">
            <p style="font-size: .9em; color: Maroon; text-align: center"><a href="../Default.aspx">Back to Administration Main Page</a></p>
    </asp:Panel>
    </div>
</asp:Content>

