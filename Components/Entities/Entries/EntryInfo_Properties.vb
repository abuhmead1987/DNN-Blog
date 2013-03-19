Imports System
Imports System.Runtime.Serialization

Namespace Entities.Entries
  Partial Public Class EntryInfo
  Inherits DotNetNuke.Entities.Content.ContentItem

#Region " Private Members "
#End Region

#Region " Constructors "
  Public Sub New()
  End Sub
#End Region

#Region " Public Properties "
  <DataMember()>
  Public Property BlogID As Int32 = -1
  <DataMember()>
  Public Property Title As String = ""
  <DataMember()>
  Public Property Summary As String = ""
  <DataMember()>
  Public Property Image As String = ""
  <DataMember()>
  Public Property Published As Boolean = True
  <DataMember()>
  Public Property PublishedOnDate As Date = Date.Now
  <DataMember()>
  Public Property AllowComments As Boolean = True
  <DataMember()>
  Public Property DisplayCopyright As Boolean = False
  <DataMember()>
  Public Property Copyright As String = ""
  <DataMember()>
  Public Property ViewCount As Int32 = 0
  <DataMember()>
  Public Property Username As String = ""
  <DataMember()>
  Public Property Email As String = ""
  <DataMember()>
  Public Property DisplayName As String = ""
#End Region

 End Class
End Namespace


