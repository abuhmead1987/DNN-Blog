﻿Imports DotNetNuke.Web.Client.ClientResourceManagement
Imports DotNetNuke.Services.Localization
Imports System.Globalization

Public Class TermsEditML
 Inherits BlogModuleBase

 Public Property VocabularyId As Integer = 1
 Public Property ColumnHeaders As String = "[]"
 Public Property Columns As String = ""

 Private Sub Page_Init1(sender As Object, e As System.EventArgs) Handles Me.Init
  AddBlogService()
  ClientResourceManager.RegisterScript(Page, ResolveUrl("~/DesktopModules/Blog/js/jquery.handsontable.js"))
  ClientResourceManager.RegisterStyleSheet(Page, ResolveUrl("~/DesktopModules/Blog/css/jquery.handsontable.css"), Web.Client.FileOrder.Css.ModuleCss)
  Me.Request.Params.ReadValue("VocabularyId", VocabularyId)
  If VocabularyId <> Settings.VocabularyId AndAlso VocabularyId <> 1 Then ' prevent users from editing another vocabulary
   VocabularyId = 1
  End If
  ColumnHeaders = "['" & (New CultureInfo(PortalSettings.DefaultLanguage)).EnglishName & "'"
  Columns = "[{data: 'DefaultName'}"
  For Each l As Locale In LocaleController.Instance.GetLocales(PortalId).Values
   If l.Code <> PortalSettings.DefaultLanguage Then
    ColumnHeaders &= ", '" & l.EnglishName & "'"
    Columns &= ", {data: 'LocNames." & l.Code & "'}"
   End If
  Next
  ColumnHeaders &= "]"
  Columns &= "]"
 End Sub

 Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

 End Sub

 Private Sub cmdCancel_Click(sender As Object, e As System.EventArgs) Handles cmdCancel.Click
  Response.Redirect(EditUrl("Manage"), False)
 End Sub

 Private Sub cmdUpdate_Click(sender As Object, e As System.EventArgs) Handles cmdUpdate.Click
  Dim vocab As List(Of Services.TermsController.TermML) = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of Services.TermsController.TermML))(Storage.Value)
  Dim currentVocabulary As Dictionary(Of Integer, Entities.Terms.TermInfo) = Entities.Terms.TermsController.GetTermsByVocabulary(ModuleId, VocabularyId)

  For Each t As Services.TermsController.TermML In vocab
   Dim crtTerm As Entities.Terms.TermInfo = currentVocabulary(t.TermID)
   If crtTerm IsNot Nothing Then
    If crtTerm.Name <> t.DefaultName Then
     crtTerm.Name = t.DefaultName
     DotNetNuke.Entities.Content.Common.Util.GetTermController().UpdateTerm(crtTerm)
    End If
   End If
   For Each l As String In t.LocNames.Keys
    Data.DataProvider.Instance.SetTermLocalization(t.TermID, l, t.LocNames(l), "")
   Next
  Next

  Response.Redirect(EditUrl("Manage"), False)
 End Sub
End Class