Imports System.ComponentModel.DataAnnotations

Public Class Book

    <Required>
    <ScaffoldColumn(False)>
    Public Property Id() As Integer

    <Required>
    Public Property Title() As String

    <Required>
    Public Property Price() As Integer

    <Required>
    Public Property Isbn() As String

End Class
