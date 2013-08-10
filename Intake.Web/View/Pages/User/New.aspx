<%@ Page Language="C#" MasterPageFile="~/Default.master" %>
<asp:Content ContentPlaceHolderID="ArticleHeader" ID="ArticleHeaderContent" runat="server">
	<h1>Sign up for <a href="/">Intake</a></h1>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleBody" ID="ArticleBodyContent" runat="server">
	<% if (Context.Items.Contains("CreateUser.Error")) { %>
		<div class="error">
			<%: Context.Items["CreateUser.Error"] %>
		</div>
	<% } %>
	<form method="post" action="/users/new">
		<fieldset>
			<input type="text" name="handle" value="<%: Context.Items["Handle"] %>" placeholder="Handle"/>
			<input type="text" name="name" value="<%: Context.Items["Name"] %>" placeholder="Display Name"/>
			<input type="password" name="password" placeholder="Password"/>
			<input type="password" name="passwordConfirm" placeholder="Confirm Password"/>
			<input type="submit" value="Sign up"/>
		</fieldset>
	</form>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleFooter" ID="ArticleFooterContent" runat="server">
	<h2>Terms of Use</h2>
	<p>
		There are no guarantees here. Use at your own risk.
	</p>
</asp:Content>


