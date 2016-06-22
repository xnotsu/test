Imports System.Data.Entity

Public Class sdgdb : Inherits DbContext
    Public Property Books() As DbSet(Of Book)
End Class
