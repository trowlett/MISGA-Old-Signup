<%@ Page Title="" Language="C#" Debug="true" MasterPageFile="~/MRAdmin.master" AutoEventWireup="true" CodeFile="editevents.aspx.cs" Inherits="Schedule_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        Edit Schedule in the Events Database</h2>
     <asp:Panel ID="Panel1" runat="server">
             <p>
                 The Events Database is displayed below.<br />If you want to reload the database from 
                 the &#39;schedule.txt&#39; file, click on &quot;Manage Events&quot; above, then &quot;Load Events&quot;.<br />
             </p>
        <hr />
       
    </asp:Panel>

    <asp:Panel ID="Panel2" runat="server">
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:MRMISGADBConnect %>" 
            DeleteCommand="DELETE FROM Events WHERE (EventID = @EventID)" 
            SelectCommand="SELECT EventID, Date, Type, HostID, Title, Cost, Time, Deadline, SpecialRule, Guest, PlayerLimit, PostDate, HostPhone FROM Events" 
            
            
            
            
            
            UpdateCommand="UPDATE Events SET Date = @Date, Type = @Type, Title = @Title, Cost = @Cost, Deadline = @Deadline, SpecialRule = @SpecialRule, Guest = @Guest, PlayerLimit = @PlayerLimit, PostDate = @PostDate, HostPhone = @HostPhone WHERE (EventID = @EventID)">
            <DeleteParameters>
                <asp:Parameter Name="EventID" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Date" Type="DateTime" />
                <asp:Parameter Name="Type" Type="String" />
                <asp:Parameter Name="Title" Type="String" />
                <asp:Parameter Name="Cost" Type="String" />
                <asp:Parameter Name="Deadline" Type="DateTime" />
                <asp:Parameter Name="SpecialRule" Type="String" DefaultValue=" " />
                <asp:Parameter Name="Guest" Type="String" DefaultValue=" " />
                <asp:Parameter Name="PlayerLimit" Type="String" />
                <asp:Parameter Name="PostDate" />
                <asp:Parameter Name="HostPhone" Type='String' DefaultValue=" " />
                <asp:Parameter Name="EventID" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
    <br />
  
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="EventID" 
            DataSourceID="SqlDataSource2" EmptyDataText="Events Database is Empty" 
            HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" 
            GridLines="None" PageSize="15" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="EventID" HeaderText="Event ID" ReadOnly="True" 
                    SortExpression="EventID">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Date" DataFormatString="{0:M/d h:mm}" 
                    HeaderText="Date" SortExpression="Date">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" Width="230px" />
                </asp:BoundField>
                <asp:BoundField DataField="Cost" HeaderText="Cost" SortExpression="Cost">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="Deadline" DataFormatString="{0:M/d h:mm}" 
                    HeaderText="Deadline" SortExpression="Deadline">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="SpecialRule" HeaderText="Special Rule" 
                    SortExpression="SpecialRule">
                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Guest" HeaderText="Guest" SortExpression="Guest">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PlayerLimit" HeaderText="Player Limit" 
                    SortExpression="PlayerLimit">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="PostDate" DataFormatString="{0:M/d h:mm}" 
                    HeaderText="Post Date" SortExpression="PostDate">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerSettings FirstPageText="First" LastPageText="&amp;gt;&amp;gt;Last" 
                Mode="NumericFirstLast" NextPageText="Next" PageButtonCount="15" 
                PreviousPageText="&amp;lt;Prev" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
 
    </asp:Panel>
    <p>

    </p>

</asp:Content>

