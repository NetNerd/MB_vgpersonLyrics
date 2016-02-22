﻿'Copyright © 2016 NetNerd


'This file is a part of MB_vgpersonLyrics.

'MB_vgpersonLyrics is free software: you can redistribute it and/or modify
'it under the terms Of the GNU General Public License As published by
'the Free Software Foundation, either version 3 Of the License, Or
'(at your option) any later version.

'MB_vgpersonLyrics Is distributed In the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty Of
'MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License For more details.

'You should have received a copy Of the GNU General Public License
'along with MB_vgpersonLyrics.  If Not, see < http: //www.gnu.org/licenses/>.


Imports System.Windows.Forms
Imports System.Drawing
Imports MusicBeePlugin.LanguageClass
Imports MusicBeePlugin.SettingsClass

Public Class ConfigPanel
    Private Shared LblLang1 As Label
    Private Shared WithEvents LangBox1 As ListBox

    Private Shared LblLang2 As Label
    Private Shared WithEvents LangBox2 As ListBox

    Private Shared WithEvents BtnL As Button
    Private Shared WithEvents BtnR As Button

    Private Shared LblUI As Label
    Private Shared WithEvents UILangCB As ComboBox

    Private Shared LblBlanks As Label
    Private Shared WithEvents BlanksCB As ComboBox

    Private Shared WithEvents LblUpdateCheck As Label
    Private Shared WithEvents UpdateCheckCB As CheckBox

    Private Shared Border1 As Panel
    Private Shared Border2 As Panel


    Private Shared MySettings As New SettingsCollection


    Private Shared Sub DefineControls()
        LblLang1 = New Label With {.Bounds = New Rectangle(10, 8, 126, 14)}
        LangBox1 = New ListBox With {.Bounds = New Rectangle(12, 23, 124, 46), .AllowDrop = True}

        LblLang2 = New Label With {.Bounds = New Rectangle(174, 8, 126, 14)}
        LangBox2 = New ListBox With {.Bounds = New Rectangle(176, 23, 124, 46), .AllowDrop = True}

        BtnL = New Button With {.Bounds = New Rectangle(144, 23, 24, 22), .Text = "<"}
        BtnR = New Button With {.Bounds = New Rectangle(144, 47, 24, 22), .Text = ">"}

        LblUI = New Label With {.Bounds = New Rectangle(10, 94, 98, 14)}
        UILangCB = New ComboBox With {.Bounds = New Rectangle(110, 89, 100, 21), .DropDownStyle = ComboBoxStyle.DropDownList}

        LblBlanks = New Label With {.Bounds = New Rectangle(10, 119, 98, 14)}
        BlanksCB = New ComboBox With {.Bounds = New Rectangle(110, 114, 100, 21), .DropDownStyle = ComboBoxStyle.DropDownList}
        BlanksCB.Items.AddRange({1, 2, 3, 4, 5, 6, 7, 8, 9})

        LblUpdateCheck = New Label With {.Bounds = New Rectangle(336, 1, 140, 42)}
        UpdateCheckCB = New CheckBox With {.Bounds = New Rectangle(319, 2, 13, 13)}

        Border1 = New Panel With {.Bounds = New Rectangle(2, 1, 308, 170), .BackColor = Color.FromArgb(224, 224, 224)}
        Border2 = New Panel With {.Bounds = New Rectangle(3, 2, 306, 168)}
    End Sub

    Shared Sub SetupControls(ByVal Settings As SettingsCollection)
        MySettings = Settings

        DefineControls()

        For Each Lang As Language In LangList()
            UILangCB.Items.Add(Lang.Name)
        Next

        UILangCB.SelectedItem = MySettings.UILanguage.Name

        If MySettings.BlankCount > 0 And MySettings.BlankCount < 10 Then BlanksCB.SelectedIndex = MySettings.BlankCount - 1

        UpdateCheckCB.Checked = MySettings.UpdateChecking

        For Each Lang As String In MySettings.LangBox1Items
            LangBox1.Items.Add(MySettings.UILanguage.LocalizeFromString(Lang))
        Next
        For Each Lang As String In MySettings.LangBox2Items
            LangBox2.Items.Add(MySettings.UILanguage.LocalizeFromString(Lang))
        Next

        TranslateLbls()
    End Sub

    Shared Function GetControls() As Control()
        Return {LblLang1, LangBox1, LblLang2, LangBox2, BtnL, BtnR, LblUI, UILangCB, LblBlanks, BlanksCB, LblUpdateCheck, UpdateCheckCB, Border2, Border1}
    End Function

    Shared Function GetSettings() As SettingsCollection
        Dim LangOrder As String = ""

        ReDim MySettings.LangBox1Items(LangBox1.Items.Count - 1)
        ReDim MySettings.LangBox2Items(LangBox2.Items.Count - 1)
        For i = 0 To LangBox1.Items.Count - 1
            MySettings.LangBox1Items(i) = MySettings.UILanguage.UnlocalizeFromString(LangBox1.Items(i))
        Next
        For i = 0 To LangBox2.Items.Count - 1
            MySettings.LangBox2Items(i) = MySettings.UILanguage.UnlocalizeFromString(LangBox2.Items(i))
        Next

        Return MySettings
    End Function

    Private Shared Sub TranslateLbls()
        LblLang1.Text = FallbackHelper(MySettings.UILanguage.LblLang1, LangEn.LblLang1)
        LblLang2.Text = FallbackHelper(MySettings.UILanguage.LblLang2, LangEn.LblLang2)
        LblUI.Text = FallbackHelper(MySettings.UILanguage.LblUI, LangEn.LblUI)
        LblBlanks.Text = FallbackHelper(MySettings.UILanguage.LblBlanks, LangEn.LblBlanks)
        LblUpdateCheck.Text = FallbackHelper(MySettings.UILanguage.LblUpdateCheck, LangEn.LblUpdateCheck)
    End Sub


    Private Shared Sub UILangCB_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles UILangCB.SelectionChangeCommitted
        Dim NewUILang As Language = LangEn
        For Each Lang In LangList()

            If UILangCB.SelectedItem Is Lang.Name Then
                NewUILang = Lang
            End If
        Next

        For i = 0 To LangBox1.Items.Count - 1
            LangBox1.Items(i) = NewUILang.LocalizeFromString(MySettings.UILanguage.UnlocalizeFromString(LangBox1.Items(i)))
        Next
        For i = 0 To LangBox2.Items.Count - 1
            LangBox2.Items(i) = NewUILang.LocalizeFromString(MySettings.UILanguage.UnlocalizeFromString(LangBox2.Items(i)))
        Next

        MySettings.UILanguage = NewUILang
        UILangCB.SelectedItem = MySettings.UILanguage.Name

        TranslateLbls()
    End Sub

    Private Shared Sub BlanksCB_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles BlanksCB.SelectionChangeCommitted
        MySettings.BlankCount = BlanksCB.SelectedItem
    End Sub

    Private Shared Sub UpdateCheckCB_CheckedChanged(sender As Object, e As EventArgs) Handles UpdateCheckCB.CheckedChanged
        MySettings.UpdateChecking = UpdateCheckCB.Checked
    End Sub

    Private Shared Sub LblUpdateCheck_Click(sender As Object, e As EventArgs) Handles LblUpdateCheck.Click
        UpdateCheckCB.Checked = Not UpdateCheckCB.Checked
    End Sub

    'Drag/drop on the language boxes
    Private Shared Sub LangBox_DragDrop(ByVal sender As ListBox, ByVal e As System.Windows.Forms.DragEventArgs) Handles LangBox1.DragDrop, LangBox2.DragDrop
        If sender.PointToClient(New Point(e.X, e.Y)).Y < 0 Then
            sender.Items.Insert(0, e.Data.GetData(DataFormats.Text))
        ElseIf sender.PointToClient(New Point(e.X, e.Y)).Y > sender.ItemHeight * sender.Items.Count Then
            sender.Items.Add(e.Data.GetData(DataFormats.Text))
        Else
            sender.Items.Insert(sender.PointToClient(New Point(e.X, e.Y)).Y / sender.ItemHeight, e.Data.GetData(DataFormats.Text))
        End If

        'Remove the old item
        If (LangBox1.SelectedIndex > -1 AndAlso e.Data.GetData(DataFormats.Text) Is LangBox1.Items(LangBox1.SelectedIndex)) Then
            LangBox1.Items.RemoveAt(LangBox1.SelectedIndex)
        ElseIf (LangBox2.SelectedIndex > -1 AndAlso e.Data.GetData(DataFormats.Text) Is LangBox2.Items(LangBox2.SelectedIndex)) Then
            LangBox2.Items.RemoveAt(LangBox2.SelectedIndex)
        End If
    End Sub
    'More drag/drop
    Private Shared Sub LangBox_DragEnter(ByVal sender As ListBox, ByVal e As System.Windows.Forms.DragEventArgs) Handles LangBox1.DragEnter, LangBox2.DragEnter
        'Only give the effect for an item that's from one of the listboxes
        If (LangBox1.SelectedIndex > -1 AndAlso e.Data.GetData(DataFormats.Text) Is LangBox1.Items(LangBox1.SelectedIndex)) _
            OrElse (LangBox2.SelectedIndex > -1 AndAlso e.Data.GetData(DataFormats.Text) Is LangBox2.Items(LangBox2.SelectedIndex)) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    'More drag/drop
    Private Shared Sub LangBox_MouseDown(ByVal sender As ListBox, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LangBox1.MouseDown, LangBox2.MouseDown
        sender.SelectionMode = SelectionMode.One
        Dim OldSelected As Integer = sender.SelectedIndex
        Dim OldSelectedText As String = sender.Text
        Dim OldCount As Integer = sender.Items.Count

        Try
            sender.DoDragDrop(sender.Items(sender.IndexFromPoint(New Point(e.X, e.Y))), DragDropEffects.Move)
        Catch
            sender.SelectionMode = SelectionMode.None 'You can drag over items to select them. This prevents that when you drag from a blank area.
        End Try

        'If the old selected item is still in the same place, select it again because the user (hopefully) didn't want anything to change.
        If sender.Items.Count = OldCount AndAlso OldSelected > -1 AndAlso sender.Items(OldSelected) = OldSelectedText AndAlso sender.SelectionMode = SelectionMode.One Then
            sender.SelectedIndex = OldSelected
        End If
    End Sub

    'L/R btns
    Private Shared Sub BtnL_Click(sender As Object, e As EventArgs) Handles BtnL.Click
        If LangBox2.SelectedIndex > -1 Then
            LangBox1.Items.Add(LangBox2.Items(LangBox2.SelectedIndex))
            LangBox2.Items.RemoveAt(LangBox2.SelectedIndex)
        End If
    End Sub

    Private Shared Sub BtnR_Click(sender As Object, e As EventArgs) Handles BtnR.Click
        If LangBox1.SelectedIndex > -1 Then
            LangBox2.Items.Add(LangBox1.Items(LangBox1.SelectedIndex))
            LangBox1.Items.RemoveAt(LangBox1.SelectedIndex)
        End If
    End Sub
End Class
