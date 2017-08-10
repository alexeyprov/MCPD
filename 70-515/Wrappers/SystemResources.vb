Imports System
Imports System.Globalization
Imports System.Resources
Imports System.Web.UI.WebControls

Namespace Common.Helpers

	''' <summary>
	''' Exposes resources owned by <see cref="System.Web.SR">internal SR class</see>
	''' </summary>
	''' <remarks></remarks>
	Public Module SystemResources

#Region "Private Constants"

		Private Const Ellipsis As String = "..."

		Private Const StringLengthThreshold As Integer = &H400

#End Region

#Region "Public Constants"

		Public ReadOnly DataObjectPropertyNotFound As String = "ObjectDataSourceView_DataObjectPropertyNotFound"
		Public ReadOnly DataObjectPropertyReadOnly As String = "ObjectDataSourceView_DataObjectPropertyReadOnly"
		Public ReadOnly Delete As String = "DataSourceView_delete"
		Public ReadOnly DeleteNotSupported As String = "ObjectDataSourceView_DeleteNotSupported"
		Public ReadOnly InsertNotSupported As String = "ObjectDataSourceView_InsertNotSupported"
		Public ReadOnly InsertRequiresValues As String = "ObjectDataSourceView_InsertRequiresValues"
		Public ReadOnly InvalidViewName As String = "DataSource_InvalidViewName"
		Public ReadOnly Pessimistic As String = "ObjectDataSourceView_Pessimistic"
		Public ReadOnly Update As String = "DataSourceView_update"
		Public ReadOnly UpdateNotSupported As String = "ObjectDataSourceView_UpdateNotSupported"
		Public ReadOnly SecureCookiesWithNonSecureConnection As String = "Connection_not_secure_creating_secure_cookie"
		Public ReadOnly UnableToEncryptTicket As String = "Unable_to_encrypt_cookie_ticket"

#End Region

#Region "Private Fields"

		Private ReadOnly _resourceManager As ResourceManager = New ResourceManager("System.Web", GetType(ObjectDataSourceView).Assembly)

#End Region

#Region "Public Methods"

		Public Function GetString(ByVal name As String) As String

			Return _resourceManager.GetString(name, Nothing)

		End Function

		Public Function GetString(ByVal name As String, ByVal ParamArray args As Object()) As String

			Dim text As String = _resourceManager.GetString(name, Nothing)

			If ((args Is Nothing) OrElse (args.Length = 0)) Then

				Return text

			End If

			' clip all the string arguments to less than 1K length
			For index As Integer = 0 To args.Length - 1

				Dim argString As String = TryCast(args(index), String)

				If argString IsNot Nothing AndAlso argString.Length > StringLengthThreshold Then

					args(index) = argString.Substring(0, StringLengthThreshold - Ellipsis.Length) & Ellipsis

				End If

			Next index

			Return String.Format(CultureInfo.CurrentCulture, text, args)

		End Function

#End Region

	End Module

End Namespace