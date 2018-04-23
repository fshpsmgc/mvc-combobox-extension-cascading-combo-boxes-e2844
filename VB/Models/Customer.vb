Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc

Public Class Customer
    Public Sub New()
        ID = -1
        Name = String.Empty
        Country = -1
        City = -1
    End Sub

    Private privateID As Integer
    Public Property ID() As Integer
        Get
            Return privateID
        End Get
        Set(ByVal value As Integer)
            privateID = value
        End Set
    End Property
    Private privateName As String
    Public Property Name() As String
        Get
            Return privateName
        End Get
        Set(ByVal value As String)
            privateName = value
        End Set
    End Property
    Private privateCountry As Integer
    Public Property Country() As Integer
        Get
            Return privateCountry
        End Get
        Set(ByVal value As Integer)
            privateCountry = value
        End Set
    End Property
    Private privateCity As Integer
    Public Property City() As Integer
        Get
            Return privateCity
        End Get
        Set(ByVal value As Integer)
            privateCity = value
        End Set
    End Property
End Class


Public Class Country
    Private privateID As Integer
    Public Property ID() As Integer
        Get
            Return privateID
        End Get
        Set(ByVal value As Integer)
            privateID = value
        End Set
    End Property
    Private privateName As String
    Public Property Name() As String
        Get
            Return privateName
        End Get
        Set(ByVal value As String)
            privateName = value
        End Set
    End Property

    Public Shared Function GetCountries() As IEnumerable(Of Country)
        Dim list As List(Of Country) = New List(Of Country)()
        For i As Integer = 0 To 99
            list.Add(New Country With {.ID = i, .Name = "Country" & i.ToString()})
        Next i
        Return list
    End Function
End Class
Public Class City
    Private privateID As Integer
    Public Property ID() As Integer
        Get
            Return privateID
        End Get
        Set(ByVal value As Integer)
            privateID = value
        End Set
    End Property
    Private privateName As String
    Public Property Name() As String
        Get
            Return privateName
        End Get
        Set(ByVal value As String)
            privateName = value
        End Set
    End Property
    Private privateCountryID As Integer
    Public Property CountryID() As Integer
        Get
            Return privateCountryID
        End Get
        Set(ByVal value As Integer)
            privateCountryID = value
        End Set
    End Property

    Public Shared Function GetCities(ByVal country As Integer) As IEnumerable(Of City)
        Dim list As List(Of City) = New List(Of City)()
        For i As Integer = 0 To 9999
            If country >= 0 AndAlso i Mod 100 = country Then
                list.Add(New City With {.ID = i, .Name = "City" & i.ToString(), .CountryID = i Mod 100})
            End If
        Next i
        Return list
    End Function
End Class