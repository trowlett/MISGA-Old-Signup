<%@ Page Title="" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="purge_prior_year.aspx.cs" Inherits="Signups_purge_prior_year" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

<script type="text/jscript">
    function cancelClicked() {
        var label = $get('cancelMsg');
        label.innerText = 'You canceled the purge at ' + (new Date()).localeFormat("T") + '.';
        label.style.color = "red";
    }
</script>
    <asp:Panel ID="Panel1" runat="server">

    <h2>Player's List Data Base Maintenance</h2>
<h2>
    <asp:Label ID="lblHdr1" runat="server" Text="Purge Prior Year Sign Up Entries"></asp:Label></h2>
<p>
    <asp:Button ID="btnDoIt" runat="server" onclick="btnDoIt_Click" Text="Do It" />
    <asp:Label ID="lblResult" runat="server" Text="Label" Visible="False"></asp:Label>
    </p>
                    <asp:ConfirmButtonExtender ID="cbe" runat="server" 
            ConfirmText="Are you sure you want purge prior year Sign Ups?" 
            TargetControlID="btnDoIt" onclientcancel="cancelClicked" />
    <p id="cancelMsg"><asp:Label ID="lblErrorMsg" runat="server" Text="Error" 
                    ForeColor="Red" Visible="False"></asp:Label>
             </p>


    </asp:Panel>
</asp:Content>

