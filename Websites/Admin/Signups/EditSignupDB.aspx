<%@ Page Title="" Language="C#" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="EditSignupDB.aspx.cs" Inherits="Signups_EditSignupDB" %>

<script runat="server">

    void GridView1_RowDeleting
        (Object sender, GridViewDeleteEventArgs e)
    {
        TableCell cell = GridView1.Rows[e.RowIndex].Cells[2];
        
        if (cell.Text.Trim() == "120225003")
        {
            e.Cancel = true;
            Message.Text = "Not deleteing the selected item.";
        }
        else
        {
            e.Cancel = true;
            Message.Text = "not deleteing " + cell.Text + " anyway.";
        }
    }  

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>
    Edit Signup Database
    </h1>
    <asp:Button ID="btnDeleteAll" runat="server" Text="Delete All" 
        onclick="btnDeleteAll_Click" Enabled="False" Visible="False" />
    
    <p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MRMISGADBConnect %>" 
            DeleteCommand="DELETE FROM PlayersList WHERE (TransDate = @TransDate)" 
            SelectCommand="SELECT PlayersList.TransDate, PlayersList.EventID, PlayersList.PlayerID, PlayersList.Action, PlayersList.Carpool, PlayersList.Marked, PlayersList.SpecialRule, Players.Name FROM PlayersList INNER JOIN Players ON PlayersList.PlayerID = Players.PlayerID ORDER BY PlayersList.EventID, PlayersList.TransDate, Players.Name"            

            
            
            UpdateCommand="UPDATE PlayersList SET Action = @Action, Carpool = @Carpool, Marked = @Marked, SpecialRule = @SpecialRule, GuestID = @GuestID, EventID = @EventID, PlayerID = @PlayerID WHERE (TransDate = @TransDate)">
            <DeleteParameters>
                <asp:Parameter Name="TransDate" Type="DateTime" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Action" Type="String" />
                <asp:Parameter Name="Carpool" Type="String" />
                <asp:Parameter Name="Marked" Type="Int32" />
                <asp:Parameter Name="SpecialRule" Type="String" DefaultValue=" " />
                <asp:Parameter Name="GuestID" Type="Int32" DefaultValue="0" />
                <asp:Parameter Name="EventID" Type="String" />
                <asp:Parameter Name="PlayerID" />
                <asp:Parameter Name="TransDate" Type="DateTime" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <asp:Label ID="Message" ForeColor="Red" runat="server" />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
             OnRowDeleting="GridView1_RowDeleting"
            DataSourceID="SqlDataSource1" PageSize="25" CellPadding="4" 
            ForeColor="#333333" GridLines="None" DataKeyNames="TransDate">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="TransDate" HeaderText="TransDate" 
                    SortExpression="TransDate" ReadOnly="True" DataFormatString="{0:d}" >
                </asp:BoundField>
                <asp:BoundField DataField="EventID" HeaderText="EventID" 
                    SortExpression="EventID" >
                </asp:BoundField>
                <asp:BoundField DataField="PlayerID" HeaderText="PlayerID" 
                    SortExpression="PlayerID" >
                </asp:BoundField>
                <asp:BoundField DataField="Action" HeaderText="Action" 
                    SortExpression="Action" >
                </asp:BoundField>
                <asp:BoundField DataField="Carpool" HeaderText="Carpool" 
                    SortExpression="Carpool" >
                </asp:BoundField>
                <asp:BoundField DataField="Marked" HeaderText="Marked" 
                    SortExpression="Marked" >
                </asp:BoundField>
                <asp:BoundField DataField="SpecialRule" HeaderText="SpecialRule" 
                    SortExpression="SpecialRule" >
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" >
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </p>
</asp:Content>

