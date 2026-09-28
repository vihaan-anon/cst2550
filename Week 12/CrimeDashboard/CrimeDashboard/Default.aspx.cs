using CrimeDashboard.Models;
using CrimeDashboard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CrimeDashboard
{
    public partial class _Default : Page
    {
        private PoliceApiServices apiService = new PoliceApiServices();
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    var forces = await apiService.GetForcesAsync();
                    ddlForce.DataSource = forces;
                    ddlForce.DataTextField = "Name";
                    ddlForce.DataValueField = "Id";
                    ddlForce.DataBind();
                    ddlForce.Items.Insert(0,
                        new System.Web.UI.WebControls.ListItem(
                            "-- Select a Force --", ""));
                }
                catch (HttpRequestException ex)
                {
                    lblError.Text = "Could not reach the Police API. " +
                        "Please check your internet connection.";
                    lblError.Visible = true;
                }
                catch (Exception ex)
                {
                    lblError.Text = $"An error occurred: {ex.Message}";
                    lblError.Visible = true;
                }

            }

        }

        // ViewState for Stop & Search data
        private List<StopAndSearch> CurrentSSData
        {
            get { return ViewState["SSData"] as List<StopAndSearch>; }
            set { ViewState["SSData"] = value; }
        }

        protected async void btnSearchSS_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlForce.SelectedValue)) return;

            try
            {
                string forceId = ddlForce.SelectedValue;
                string date = txtSSDate.Text.Trim();

                var data = await apiService.GetStopAndSearchAsync(forceId, date);
                CurrentSSData = data;

                lblTotalSS.Text = data.Count.ToString("N0");

                // Bind to GridView
                gvStopSearch.DataSource = data;
                gvStopSearch.DataBind();

                // Generate Stop & Search chart (by Object of Search)
                GenerateSSChart(data);

                pnlSS.Visible = true;
            }
            catch (Exception ex)
            {
                lblTotalSS.Text = "Error: " + ex.Message;
                pnlSS.Visible = true;
            }
        }

        private void GenerateSSChart(List<StopAndSearch> data)
        {
            var groups = data
                .GroupBy(s => s.ObjectOfSearch ?? "Unknown")
                .OrderByDescending(g => g.Count())
                .Take(8)
                .ToList();

            string labels = string.Join(",",
                groups.Select(g => $"'{g.Key.Replace("'", "\\\'")}'"));
            string values = string.Join(",",
                groups.Select(g => g.Count().ToString()));

            string script = $@"
                    <script>
                        if (window.ssChartInstance) window.ssChartInstance.destroy();
                        var ctxSS = document.getElementById('ssChart').getContext('2d');
                        window.ssChartInstance = new Chart(ctxSS, {{
                            type: 'doughnut',
                            data: {{
                                labels: [{labels}],
                                datasets: [{{
                                    data: [{values}],
                                    backgroundColor: [
                                        '#2ecc71','#e74c3c','#3498db','#f39c12',
                                        '#9b59b6','#1abc9c','#e67e22','#34495e'
                                    ]
                                }}]
                            }},
                            options: {{ responsive: true }}
                        }});
                    </script>";

            // Append to existing chart scripts
            litChartScript.Text += script;
        }

        protected void gvStopSearch_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gvStopSearch.PageIndex = e.NewPageIndex;
            gvStopSearch.DataSource = CurrentSSData;
            gvStopSearch.DataBind();
        }

        protected async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                double lat = double.Parse(txtLat.Text);
                double lng = double.Parse(txtLng.Text); 
                string date = txtDate.Text; // Expecting format YYYY-MM
                List<Crime> crimes = await apiService.GetCrimesAsync(lat, lng, date);

                lblTotalCrimes.Text = crimes.Count.ToString("N0");
                lblCategories.Text = crimes.Select(cr => cr.Category).Distinct().Count().ToString();
                lblLocations.Text = crimes.Select(cr => cr.StreetName).Distinct().Count().ToString();

                
                
                GenerateCharts(crimes);

                CurrentCrimes = crimes;
                gvCrimes.DataSource = crimes;   // Bind directly from the local list
                gvCrimes.DataBind();
                BindGrid();

                pnlResults.Visible = true;


            }
            catch (Exception ex)
            {

                lblTotalCrimes.Text = "Error: " + ex.Message;
                pnlResults.Visible = true;

            }
        }

        private void BindGrid()
        {
            gvCrimes.DataSource = CurrentCrimes;
            gvCrimes.DataBind();
        }


        protected async void gvCrimes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCrimes.PageIndex = e.NewPageIndex;
            
            gvCrimes.DataSource = CurrentCrimes;  // ViewState works here
            gvCrimes.DataBind();

            BindGrid();
        }

        // Store crimes in ViewState so paging works without re-calling the API
        private List<Crime> CurrentCrimes
        {
            get { return ViewState["Crimes"] as List<Crime>; }
            set { ViewState["Crimes"] = value; }
        }


        private void GenerateCharts(List<Crime> crimes)
        {
            // --- PIE CHART: Crimes by Category ---
            var categoryGroups = crimes
                .GroupBy(cr => cr.CategoryDisplay)
                .OrderByDescending(g => g.Count())
                .Take(10)
                .ToList();

            string pieLabels = string.Join(",",
                categoryGroups.Select(g => $"'{g.Key}'"));
            string pieData = string.Join(",",
                categoryGroups.Select(g => g.Count().ToString()));

            // --- BAR CHART: Crimes by Location (Top 10) ---
            var locationGroups = crimes
                .GroupBy(cr => cr.StreetName)
                .OrderByDescending(g => g.Count())
                .Take(10)
                .ToList();

            string barLabels = string.Join(",",
                locationGroups.Select(g =>
                    $"'{g.Key.Replace("'", "\\\'")}'"));
            string barData = string.Join(",",
                locationGroups.Select(g => g.Count().ToString()));

            // Build the JavaScript and inject it into the page
            string script = $@"
                <script>
                    // Destroy previous charts if they exist
                    if (window.pieChartInstance) window.pieChartInstance.destroy();
                    if (window.barChartInstance) window.barChartInstance.destroy();
 
                    // PIE CHART
                    var ctxPie = document.getElementById('pieChart').getContext('2d');
                    window.pieChartInstance = new Chart(ctxPie, {{
                        type: 'pie',
                        data: {{
                            labels: [{pieLabels}],
                            datasets: [{{
                                data: [{pieData}],
                                backgroundColor: [
                                    '#3498db','#e74c3c','#2ecc71','#f39c12','#9b59b6',
                                    '#1abc9c','#e67e22','#34495e','#16a085','#c0392b'
                                ]
                            }}]
                        }},
                        options: {{ responsive: true }}
                    }});
 
                    // BAR CHART
                    var ctxBar = document.getElementById('barChart').getContext('2d');
                    window.barChartInstance = new Chart(ctxBar, {{
                        type: 'bar',
                        data: {{
                            labels: [{barLabels}],
                            datasets: [{{
                                label: 'Number of Crimes',
                                data: [{barData}],
                                backgroundColor: '#3498db'
                            }}]
                        }},
                        options: {{
                            responsive: true,
                            plugins: {{ legend: {{ display: false }} }},
                            scales: {{ y: {{ beginAtZero: true }} }}
                        }}
                    }});
                </script>";

            litChartScript.Text = script;
        }

    }
}
