Public Class TestController

    Inherits System.Web.Mvc.Controller

    Private db As New sdgdb

    '
    ' GET: /Test

    Function Index() As ActionResult
        Return View()
    End Function

    Function Search(model As Book) As ActionResult
        If Request.IsAjaxRequest() Then
            ' リクエストがAjax通信（非同期通信）である場合値を返す
            Dim Result = From b In db.Books Select b

            If Not String.IsNullOrEmpty(model.Title) Then
                Result = Result.Where(Function(a) a.Title.Contains(model.Title))
            End If

            ViewBag.Test = Result

            Return PartialView("_Part")
        Else
            ' リクエストがAjax通信以外の場合、何もしない
            Return New EmptyResult()
        End If
    End Function
End Class