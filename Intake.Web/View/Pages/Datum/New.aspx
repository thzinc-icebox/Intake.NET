<%@ Page Language="C#" MasterPageFile="~/Default.master" %>
<%@ Import Namespace="System.Text.RegularExpressions" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register TagPrefix="user" TagName="Result" Src="~/View/Pages/User/Result.ascx" %>

<asp:Content ContentPlaceHolderID="ArticleHeader" ID="ArticleHeaderContent" runat="server">
	<user:Result runat="server"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleBody" ID="ArticleBodyContent" runat="server">
	<% if (Context.Items.Contains("CreateDatum.Error")) { %>
		<div class="error">
			<%: Context.Items["CreateDatum.Error"] %>
		</div>
	<% } %>
	<form method="post" action="/data/new" class="new datum">
		<fieldset>
			<textarea name="value" placeholder="Value"><%: Context.Items["Value"] %></textarea>
			<textarea name="description" placeholder="Description"><%: Context.Items["Description"] %></textarea>
			<fieldset>
				<legend>Tags</legend>
				<datalist class="tags" data-displaycontainer="tags" data-input="tagName">
					<% if (Context.Items.Contains("TagNames")) { %>
						<% foreach (var tagName in Context.Items["TagNames"] as IEnumerable<string>) { %>
							<option value="<%: tagName %>"/>
						<% } %>
					<% } %>
				</datalist>
				<input type="text" id="tagName"/>
				<ul id="tags"/>
			</fieldset>
			<fieldset class="location">
				<legend>Location</legend>
				<a href="#" class="get location">
					<img src="about:blank" alt="Toggle Location"/>
				</a>
				<div class="coordinates">
					<span class="latitude"></span>
					<span class="longitude"></span>
					<span class="accuracy"></span> meters
				</div>
				<div class="map"></div>
			</fieldset>
			<input type="submit" value="Save"/>
		</fieldset>
	</form>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleFooter" ID="ArticleFooterContent" runat="server">
</asp:Content>


