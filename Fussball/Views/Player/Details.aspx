<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Fussball.Models.Player>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Spillerdetaljer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a class="backBtn"></a>
    <div id="PlayerCard">
        <h1><%= Model.Name %></h1>
        <img src="<%= Model.Image_Large %>" alt="Bilde av <%= Model.Name %>">
        <dl>
            <dt>Antall kamper</dt>
            <dd><%= Model.TotalGames() %></dd>
            <dt>Antall mål</dt>
            <dd><%= Model.TotalGoals() %></dd>
            <dt>Antall selvmål</dt>
            <dd><%= Model.TotalSelfGoals() %></dd>
            <dt>Sluppet inn</dt>
            <dd><%= Model.LetInGoals() %></dd>
            <dt>Gjennomsnittsmål pr kamp</dt>
            <dd><%= Math.Round(Model.AverageGoals(), 2) %></dd>
            <dt>Beste farge</dt>
            <dd><%= Model.BestColor() %></dd>
            <dt>Beste posisjon</dt>
            <dd><%= Model.BestPosition() %></dd>
            <dt>Spilt mest med</dt>
            <dd><%= Model.PlayedMostWith() %></dd>
            <dt>Vunnet mest med</dt>
            <dd><%= Model.BestTeamPlayer() %></dd>
        </dl>
    </div>

</asp:Content>