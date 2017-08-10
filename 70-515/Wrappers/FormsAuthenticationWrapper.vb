Imports System.Reflection
Imports System.Web
Imports System.Web.Security

Imports OCM.Phoenix.WebToolsFramework.Common.Helpers

Imports LocalConstants = OCM.Phoenix.WebToolsFramework.Server.WebToolsPortal.Helpers.ConstantsHelper

Namespace Wrappers

	''' <summary>
	''' Wrapper around <see cref="System.Web.Security.FormsAuthentication">FormsAuthentication</see> class
	''' </summary>
	''' <remarks>Exposes private methods required for cookieless authentication thru reflection</remarks>
	Friend NotInheritable Class FormsAuthenticationWrapper

#Region "Private Constants"

		Private Const ROOT_PATH As String = "/"

#End Region

#Region "Private Fields"

		Private Shared ReadOnly _encryptMethod As MethodInfo

#End Region

#Region "Constructors"

		Shared Sub New()

			Const PrivateStaticMember As BindingFlags = BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.DeclaredOnly

			Dim encryptArgs As Type() = New Type() {GetType(FormsAuthenticationTicket), GetType(Boolean)}

			_encryptMethod = GetType(FormsAuthentication).GetMethod("Encrypt", PrivateStaticMember, Nothing, encryptArgs, Nothing)

		End Sub

		Private Sub New()

		End Sub

#End Region

#Region "Public Methods"

		''' <summary>
		''' Sets forms authentication ticket
		''' </summary>
		''' <param name="userName">login name</param>
		''' <param name="createPersistentCookie">make ticket persistent</param>
		''' <param name="userData">user-defined data to store with the ticket</param>
		Public Shared Sub SetAuthCookie(ByVal userName As String, ByVal createPersistentCookie As Boolean, ByVal userData As String)

			Dim currentContext As HttpContext = HttpContext.Current

			FormsAuthentication.Initialize()

			If Not currentContext.Request.IsSecureConnection AndAlso FormsAuthentication.RequireSSL Then

				Throw New HttpException(SystemResources.GetString(SystemResources.SecureCookiesWithNonSecureConnection))

			End If

			Dim cookieless As Boolean = CookielessHelperWrapper.UseCookieless(currentContext, False, FormsAuthentication.CookieMode)

			Dim cookie As HttpCookie = GetAuthCookie(userName, createPersistentCookie,
			 IIf(cookieless, ROOT_PATH, FormsAuthentication.FormsCookiePath), Not cookieless, userData)

			If cookieless Then

				CookielessHelperWrapper.Instance.SetCookieValue(LocalConstants.AspxCookieTypes.FORMS_AUTHENTICATION_COOKIE, cookie.Value)

			Else

				currentContext.Response.Cookies.Add(cookie)

				CookielessHelperWrapper.Instance.SetCookieValue(LocalConstants.AspxCookieTypes.FORMS_AUTHENTICATION_COOKIE, Nothing)

			End If

		End Sub

#End Region

#Region "Implementation"

		Private Shared Function GetAuthCookie(ByVal userName As String, ByVal createPersistentCookie As Boolean, ByVal cookiePath As String, ByVal hexEncodedTicket As Boolean, ByVal userData As String) As HttpCookie

			Dim ticket As New FormsAuthenticationTicket(2, userName, DateTime.Now,
			  DateTime.Now.AddMinutes(CDbl(FormsAuthentication.Timeout.Minutes)),
			  createPersistentCookie, userData, cookiePath)

			Dim encryptedTicket As String = Encrypt(ticket, hexEncodedTicket)

			If String.IsNullOrEmpty(encryptedTicket) Then

				Throw New HttpException(SystemResources.GetString(SystemResources.UnableToEncryptTicket))

			End If

			Dim cookie As New HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
			cookie.HttpOnly = True
			cookie.Path = cookiePath
			cookie.Secure = FormsAuthentication.RequireSSL

			If FormsAuthentication.CookieDomain IsNot Nothing Then

				cookie.Domain = FormsAuthentication.CookieDomain

			End If

			If ticket.IsPersistent Then

				cookie.Expires = ticket.Expiration

			End If

			Return cookie

		End Function

		Private Shared Function Encrypt(ByVal ticket As FormsAuthenticationTicket, ByVal hexEncodedTicket As Boolean) As String

			Return CType(_encryptMethod.Invoke(Nothing, New Object() {ticket, hexEncodedTicket}), String)

		End Function

#End Region

	End Class

End Namespace