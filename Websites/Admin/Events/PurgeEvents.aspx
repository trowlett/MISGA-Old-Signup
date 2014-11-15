<%@ Page Title="" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="PurgeEvents.aspx.cs" Inherits="Events_PurgeEvents" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

<script type="text/jscript">
    function cancelClicked() {
        var label = $get('cancelMsg');
        label.innerText = 'You hit cancel in the Confirm dialog on ' + (new Date()).localeFormat("T") + '.';      
    }
</script>


    <h2>
        Purge Events from Database
</h2>
    <p>
        <asp:Label ID="DBInfo" runat="server" Text="database info"></asp:Label>
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
            onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
            <asp:ListItem Value="All" Selected="True">All Events</asp:ListItem>
            <asp:ListItem Value="Prior">Events Prior to Date</asp:ListItem>
        </asp:RadioButtonList>
        
    </p>
    <asp:Panel ID="Panel1" runat="server" Visible="true">
        <asp:Label ID="Label1" runat="server" Text="Purge Events Prior to: "></asp:Label>
        <asp:TextBox ID="tbDate"
            runat="server"></asp:TextBox>
    </asp:Panel>
    <p>
    <asp:Button ID="btnDoIt" runat="server" Text="Do It" onclick="btnDoIt_Click" />
        <ajaxToolkit:ConfirmButtonExtender ID="cbe" runat="server" 
            ConfirmText="Are you sure you want Purge these Events?" 
            TargetControlID="btnDoIt" onclientcancel="cancelClicked" />
    </p>
    <p id="cancelMsg"></p>
    <p>
        <asp:Label ID="ChoiceLbl" runat="server" Text="Choice selection feedback"></asp:Label>
    </p>
    <div>
        <asp:Panel ID="DataPanel" runat="server" Visible="false">

        <asp:CheckBox ID="CBAll" runat="server" AutoPostBack="True" 
            oncheckedchanged="CBAll_CheckedChanged" />
        <asp:Button ID="btnDelete" runat="server" BackColor="White" BorderStyle="None" 
            Font-Names="Arial" ForeColor="#3399FF" onclick="btnDelete_Click" 
            Text="delete" />
        <asp:Label ID="lblDelete" runat="server"></asp:Label>

            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                <table>
                    <tr>
                        <th>
                            Select<br /><asp:CheckBox ID="CheckBox1" runat="server" /></th>
                        <th>
                            ID</th>
                        <th>
                            Date</th>
                        <th>
                            Time</th>
                        <th>
                            Type</th>
                        <th>
                            Title</th>
                    </tr>

                </HeaderTemplate>
                <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox2" runat="server" /></td>
                    <td>
                        123456789</td>
                    <td>
                        TODAY</td>
                    <td>
                        Now</td>
                    <td>
                        Home</td>
                    <td>
                        Title</td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
                </asp:Repeater>

         </asp:Panel>
     </div>

</asp:Content>

