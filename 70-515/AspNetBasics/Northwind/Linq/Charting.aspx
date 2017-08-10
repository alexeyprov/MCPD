<%@ Page MasterPageFile="~/Northwind/Master/Northwind.master" Language="C#"
	AutoEventWireup="true" CodeFile="Charting.aspx.cs" Inherits="Northwind.UI.Linq.ChartingPage" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NorthwindContentPlaceHolder" runat="Server">
	<asp:Chart ID="dataChart" runat="server" BackColor="Silver" 
		BackGradientStyle="TopBottom" BackSecondaryColor="WhiteSmoke" 
		BorderlineColor="Gray" BorderlineDashStyle="Solid" Width="400px">
		<series>
			<asp:Series ChartArea="mainArea" ChartType="Line" Name="ProductPrice" BorderWidth="3">
			</asp:Series>
			<asp:Series ChartArea="mainArea" ChartType="Spline" Name="ProductStock" Color="Violet" BorderWidth="3">
			</asp:Series>
		</series>
		<chartareas>
			<asp:ChartArea BackColor="Wheat" Name="mainArea">
			</asp:ChartArea>
		</chartareas>
		<Titles>
			<asp:Title Font="Times New Roman, 14pt" Name="Products" Text="Product Stock">
			</asp:Title>
		</Titles>
		<BorderSkin SkinStyle="Emboss" />
	</asp:Chart>
</asp:Content>

