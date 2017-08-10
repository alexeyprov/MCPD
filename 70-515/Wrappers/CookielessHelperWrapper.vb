Imports System.Reflection
Imports System.Web
Imports System.Web.Security

Namespace Wrappers

	''' <summary>
	''' Wrapper around <see cref="System.Web.Security.CookielessHelperClass">CookielessHelperClass</see> class
	''' </summary>
	''' <remarks>Exposes private methods for cookie data persistence in URL's thru reflection</remarks>
	Friend NotInheritable Class CookielessHelperWrapper

#Region "Private Fields"

		Private _instance As Object

		Private Shared ReadOnly _cookielessHelperGetMethod As MethodInfo
		Private Shared ReadOnly _setCookieValueMethod As MethodInfo
		Private Shared ReadOnly _useCookielessMethod As MethodInfo

#End Region

#Region "Constructors"

		Shared Sub New()

			Const PrivateMember As BindingFlags = BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.DeclaredOnly
			Const PrivateStaticMember As BindingFlags = BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.DeclaredOnly

			Dim cookielessHelperProperty As PropertyInfo = GetType(HttpContext).GetProperty("CookielessHelper", PrivateMember)

			Dim wrappedType As Type = cookielessHelperProperty.PropertyType
			_cookielessHelperGetMethod = cookielessHelperProperty.GetGetMethod(True)

			Dim useCookielessArgs As Type() = New Type() {GetType(HttpContext), GetType(Boolean), GetType(HttpCookieMode)}
			_useCookielessMethod = wrappedType.GetMethod("UseCookieless", PrivateStaticMember, Nothing, useCookielessArgs, Nothing)

			Dim setCookieValueArgs As Type() = New Type() {GetType(Char), GetType(String)}
			_setCookieValueMethod = wrappedType.GetMethod("SetCookieValue", PrivateMember, Nothing, setCookieValueArgs, Nothing)

		End Sub

		Private Sub New(ByVal helperInstance As Object)

			_instance = helperInstance

		End Sub

#End Region

#Region "Public Properties"

		''' <summary>
		''' Replacement for the <see cref="System.Web.HttpContext.CookielessHelper">HttpContext.CookielessHelper</see> property
		''' </summary>
		''' <value>Wrapped instance of <see cref="System.Web.Security.CookielessHelperClass">CookielessHelperClass</see></value>
		Public Shared ReadOnly Property Instance As CookielessHelperWrapper
			Get
				Return New CookielessHelperWrapper(_cookielessHelperGetMethod.Invoke(HttpContext.Current, New Object() {}))
			End Get
		End Property

#End Region

#Region "Public Methods"

		''' <summary>
		''' Checks whether cookieless cookie data persistence should be used
		''' </summary>
		''' <param name="context">current HTTP context</param>
		''' <param name="doRedirect">flag whether to auto-detect cookie support via redirection</param>
		''' <param name="cookieMode">cookie mode from configuration</param>
		''' <returns>True, if cookieless persistence method should be used, False if cookies should be used</returns>
		''' <remarks>Exposes private <see cref="System.Web.Security.CookielessHelperClass.UseCookieless">CookielessHelperClass.UseCookieless</see> method</remarks>
		Public Shared Function UseCookieless(ByVal context As HttpContext, ByVal doRedirect As Boolean, ByVal cookieMode As HttpCookieMode) As Boolean

			Return CType(_useCookielessMethod.Invoke(Nothing, New Object() {context, doRedirect, cookieMode}), Boolean)

		End Function

		''' <summary>
		''' Stores cookie data in URL when cookies are not supported
		''' </summary>
		''' <param name="identifier">Cookie type</param>
		''' <param name="cookieValue">Cookie value</param>
		''' <remarks>Exposes private <see cref="System.Web.Security.CookielessHelperClass.SetCookieValue">CookielessHelperClass.SetCookieValue</see> method</remarks>
		Public Sub SetCookieValue(ByVal identifier As Char, ByVal cookieValue As String)

			_setCookieValueMethod.Invoke(_instance, New Object() {identifier, cookieValue})

		End Sub

#End Region

	End Class

End Namespace