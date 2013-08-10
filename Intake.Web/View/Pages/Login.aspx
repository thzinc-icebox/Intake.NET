<%@ Page Language="C#" MasterPageFile="~/Default.master" %>
<asp:Content ContentPlaceHolderID="ArticleHeader" ID="ArticleHeaderContent" runat="server">
	<h1>Log in to <a href="/">Intake</a></h1>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleBody" ID="ArticleBodyContent" runat="server">
	<form method="post" action="/login">
		<fieldset>
			<input type="text" name="handle" placeholder="Handle"/>
			<input type="password" name="password" placeholder="Password"/>
			<input type="submit" value="Log in"/>
		</fieldset>
	</form>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleFooter" ID="ArticleFooterContent" runat="server">
</asp:Content>


