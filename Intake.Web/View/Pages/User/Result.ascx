<%@ Control Language="C#" Inherits="Intake.Web.View.Pages.User.Result" %>
<article class="user">
	<div class="primary">
		<img src="about:blank" alt="Avatar" class="avatar"/>
		<h2>
			<a href="/users/<%: User.Handle %>"><%: User.Handle %></a>
		</h2>
	</div>
	<div class="secondary">
		<h3>
			<%: User.Name %>
		</h3>
	</div>
</article>