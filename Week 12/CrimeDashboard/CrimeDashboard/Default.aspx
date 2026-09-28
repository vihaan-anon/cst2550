<%@ Page Title="Crime Dashboard" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="CrimeDashboard._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblError" runat="server" CssClass="text-danger"
    style="font-size:16px; font-weight:bold;" Visible="false" />

    <h2>UK Police Crime Dashboard</h2>
    <p class="lead">Select a location and date to view street-level crime data.</p>

    <!-- FILTER PANEL -->
    <div class="well" style="background: #f0f4f8; padding: 20px; border-radius: 8px;">
        <div class="row">
            <div class="col-md-3">
                <label>Latitude:</label>
                <asp:TextBox ID="txtLat" runat="server" CssClass="form-control"
                    Text="51.5074" />
            </div>
            <div class="col-md-3">
                <label>Longitude:</label>
                <asp:TextBox ID="txtLng" runat="server" CssClass="form-control"
                    Text="-0.1278" />
            </div>
            <div class="col-md-3">
                <label>Date (YYYY-MM):</label>
                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"
                    Text="2024-01" />
            </div>
            <div class="col-md-3" style="padding-top: 24px;">
                <asp:Button ID="btnSearch" runat="server" Text="Load Crime Data"
                    CssClass="btn btn-primary btn-block"
                    OnClick="btnSearch_Click" />
            </div>
        </div>
    </div>

    <!-- SUMMARY STATS -->
    <asp:Panel ID="pnlResults" runat="server" Visible="false">
        <div class="row" style="margin-top: 20px;">
            <div class="col-md-4">
                <div class="panel panel-info">
                    <div class="panel-heading">Total Crimes</div>
                    <div class="panel-body" style="font-size: 28px; font-weight: bold;">
                        <asp:Label ID="lblTotalCrimes" runat="server" />
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-warning">
                    <div class="panel-heading">Unique Categories</div>
                    <div class="panel-body" style="font-size: 28px; font-weight: bold;">
                        <asp:Label ID="lblCategories" runat="server" />
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-danger">
                    <div class="panel-heading">Unique Locations</div>
                    <div class="panel-body" style="font-size: 28px; font-weight: bold;">
                        <asp:Label ID="lblLocations" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <!-- CHARTS ROW -->
        <div class="row" style="margin-top: 20px;">
            <div class="col-md-6">
                <h4>Crimes by Category</h4>
                <canvas id="pieChart" style="max-height: 400px;"></canvas>
            </div>
            <div class="col-md-6">
                <h4>Top 10 Crime Locations</h4>
                <canvas id="barChart" style="max-height: 400px;"></canvas>
            </div>
        </div>

        <!-- DATA TABLE -->
        <h4 style="margin-top: 30px;">Crime Records</h4>
        <asp:GridView ID="gvCrimes" runat="server" AutoGenerateColumns="false"
            CssClass="table table-striped table-bordered table-hover"
            AllowPaging="true" PageSize="15"
            OnPageIndexChanging="gvCrimes_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="CategoryDisplay" HeaderText="Crime Type" />
                <asp:BoundField DataField="Month" HeaderText="Month" />
                <asp:BoundField DataField="StreetName" HeaderText="Location" />
                <asp:BoundField DataField="OutcomeText" HeaderText="Outcome" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <!-- STOP & SEARCH SECTION -->
    <hr style="margin-top: 40px;" />
    <h3>Stop and Search Data</h3>
    <p>Select a police force to view Stop & Search records.</p>

    <div class="well" style="background: #f0f4f8; padding: 20px; border-radius: 8px;">
        <div class="row">
            <div class="col-md-4">
                <label>Police Force:</label>
                <asp:DropDownList ID="ddlForce" runat="server"
                    CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <label>Date (YYYY-MM):</label>
                <asp:TextBox ID="txtSSDate" runat="server"
                    CssClass="form-control" Text="2024-01" />
            </div>
            <div class="col-md-3" style="padding-top: 24px;">
                <asp:Button ID="btnSearchSS" runat="server"
                    Text="Load Stop & Search"
                    CssClass="btn btn-success btn-block"
                    OnClick="btnSearchSS_Click" />
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlSS" runat="server" Visible="false">
        <div class="row" style="margin-top: 20px;">
            <div class="col-md-4">
                <div class="panel panel-success">
                    <div class="panel-heading">Total Stop & Searches</div>
                    <div class="panel-body" style="font-size: 28px; font-weight: bold;">
                        <asp:Label ID="lblTotalSS" runat="server" />
                    </div>
                </div>
            </div>
            <div class="col-md-8">
                <canvas id="ssChart" style="max-height: 350px;"></canvas>
            </div>
        </div>

        <asp:GridView ID="gvStopSearch" runat="server"
            AutoGenerateColumns="false"
            CssClass="table table-striped table-bordered table-hover"
            AllowPaging="true" PageSize="15"
            OnPageIndexChanging="gvStopSearch_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="DateTime" HeaderText="Date/Time" />
                <asp:BoundField DataField="Type" HeaderText="Type" />
                <asp:BoundField DataField="AgeRange" HeaderText="Age Range" />
                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                <asp:BoundField DataField="ObjectOfSearch" HeaderText="Reason" />
                <asp:BoundField DataField="OutcomeDisplay" HeaderText="Outcome" />
            </Columns>
        </asp:GridView>
    </asp:Panel>

    <!-- CHART.JS SCRIPT PLACEHOLDER (filled from code-behind) -->
    <asp:Literal ID="litChartScript" runat="server" />

</asp:Content>
