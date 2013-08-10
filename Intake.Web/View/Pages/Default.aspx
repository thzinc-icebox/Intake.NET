<%@ Page Language="C#" MasterPageFile="~/Default.master" %>
<%@ Import Namespace="Intake.Web" %>
<asp:Content ContentPlaceHolderID="ArticleHeader" runat="server">
	<h1>Intake</h1>
</asp:Content>
<asp:Content ContentPlaceHolderID="ArticleBody" runat="server">
	<p>
		Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla ut ligula enim. Nulla vestibulum blandit lobortis. Curabitur eget dolor eget quam ornare aliquam eu eu lacus. Suspendisse luctus mollis commodo. Phasellus et venenatis eros. Donec sagittis cursus pulvinar. Duis at orci ut leo convallis condimentum vitae tempor metus. Maecenas eget malesuada velit. Suspendisse potenti. Sed id porta leo. Ut congue varius tellus sed ullamcorper. Sed quis sem eget metus fermentum tincidunt.
	</p>
	<p>
		Etiam pharetra dolor vitae mollis tincidunt. Aenean eget urna orci. Proin tortor risus, viverra et ullamcorper at, gravida eget enim. Quisque fermentum adipiscing commodo. Nunc viverra orci ac libero posuere pretium. Aliquam felis dui, aliquam id vestibulum sed, pulvinar sit amet lorem. Duis et vestibulum elit. Mauris at arcu consequat, scelerisque lorem a, sodales lectus. Suspendisse nec nulla eget nulla facilisis sagittis a vel tortor. Nunc non enim at tortor dignissim mollis venenatis eget diam. Vivamus eros odio, vehicula quis scelerisque non, tristique ac nulla. Nunc eu tempor diam. Duis interdum ipsum adipiscing mauris rhoncus vulputate. 
	</p>
	
	<div class="promotion">
		<% if ((Master as Default).IsLoggedIn) { %>
			<a href="/users/<%: (Master as Default).CurrentHandle %>/data" class="data">
				View my data
			</a>
		<% } else { %>
			<a href="/users/new" class="new user">
				Sign up
			</a>
			
			<a href="/login" class="login">
				Log in
			</a>
		<% } %>
	</div>
</asp:Content>