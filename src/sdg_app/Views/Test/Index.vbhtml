@ModelType sdg_app.Book

@Code
    ViewData("Title") = "検索"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>検索条件</h2>

@Using Ajax.BeginForm("Search", New AjaxOptions With {
                     .HttpMethod = "POST",
                     .UpdateTargetId = "mainForm"})
    @Html.Label("Title", "書籍名：")
    @Html.TextBox("Title", "")
    @<input type="submit" name="btnSearch" value="検索">
End Using

<h2>検索結果</h2>

<div id="mainForm"></div>
