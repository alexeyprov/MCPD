<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true"
	CodeFile="AjaxOOP.aspx.cs" Inherits="AspNetBasics.ClientProgrammingDemo.UI.AjaxOOPPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
	<asp:ScriptManager ID="smAjax" runat="server">
	</asp:ScriptManager>
	<script type="text/javascript" src="../Scripts/Garden.js" >
	</script>

	<h2>
		Interface</h2>
	<div>
		This file contains a Tree base class, and an IFruitTree interface. Apple and Banana,
		two derived classes implement that interface, whereas, Pine does not implement that
		interface.
	</div>
	<div>
		<ul>
			<li><a href="#" onclick="return OnTestNewClick()">Object Creation</a></li>
			<li><a href="#" onclick="return OnTestImplementsClick()">Implements Check</a></li>
			<li><a href="#" onclick="return OnTestInterfaceMethodClick()">Call interface method</a></li>
		</ul>
	</div>
	<script type="text/javascript" language="javascript">
		<!--
		function OnTestNewClick()
		{
			var apple = new Garden.Apple();

			apple.makeLeaves();
			alert(apple);

			return false;
		}

		function OnTestImplementsClick()
		{
			var banana = new Garden.Banana();

			if (Garden.Banana.implementsInterface(Garden.IFruitTree))
			{
				alert("Banana implements IFruitTree");
			}
			else
			{
				alert("Banana doesn't implement IFruitTree");
			}

			if (Garden.Pine.implementsInterface(Garden.IFruitTree))
			{
				alert("Pine implements IFruitTree");
			}
			else
			{
				alert("Pine doesn't implement IFruitTree");
			}

			return false;
		}

		function OnTestInterfaceMethodClick()
		{
			ProcessFruitTree(new Garden.Apple());

			ProcessFruitTree(new Garden.Banana());

			ProcessFruitTree(new Garden.Pine());

			return false;
		}

		function ProcessFruitTree(t)
		{
			alert("Current tree: " + t);

			if (Garden.IFruitTree.isImplementedBy(t))
			{
				alert(t.getName() + " is a fruit tree. Fruit is " + t.bearFruit());
			}
		}

		//-->
	</script>


</asp:Content>
