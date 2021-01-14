Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc

Namespace CS.Models
	Public Class Customer
		Public Sub New()
			ID = -1
			Name = String.Empty
			Country = -1
			City = -1
		End Sub

		Public Property ID() As Integer
		Public Property Name() As String
		Public Property Country() As Integer
		Public Property City() As Integer
	End Class


	Public Class Country
		Public Property ID() As Integer
		Public Property Name() As String

		Public Shared Function GetCountries() As IEnumerable(Of Country)
			Dim list As New List(Of Country)()
			For i As Integer = 0 To 99
				list.Add(New Country With {.ID = i, .Name = "Country" & i.ToString()})
			Next i
			Return list
		End Function
	End Class
	Public Class City
		Public Property ID() As Integer
		Public Property Name() As String
		Public Property CountryID() As Integer

		Public Shared Function GetCities(ByVal c As Integer) As IEnumerable(Of City)
			Dim list As New List(Of City)()
			For i As Integer = 0 To 9999
				If c >= 0 AndAlso i Mod 100 = c Then
					list.Add(New City With {.ID = i, .Name = "City" & i.ToString(), .CountryID = i Mod 100})
				End If
			Next i
			Return list
		End Function
	End Class
End Namespace
