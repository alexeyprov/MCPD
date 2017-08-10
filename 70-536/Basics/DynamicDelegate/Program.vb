Imports System
Imports System.IO
Imports System.Data
Imports System.Security
Imports System.Threading
Imports System.Reflection
Imports System.Data.Common
Imports System.Security.Policy
Imports System.Xml.Serialization

Imports DynamicDelegate.Common

Public Module Program

	Private Const ADAPTER_SEND_METHOD As String = "Send"
	Private Const ADAPTER_ARGUMENTS_METHOD As String = "get_Arguments"

	Private ReadOnly _sendMethod As MethodInfo
	Private ReadOnly _argumentsMethod As MethodInfo

	Private Delegate Function AdapterSendDelegate(ByVal source As Message, ByVal configuration As ParametersCollection) As Boolean
	Private Delegate Function AdapterArgumentsDelegate() As List(Of AdapterArgumentDescription)



	Sub New()

		Dim t As Type = GetType(IAdapter)

		_sendMethod = t.GetMethod(ADAPTER_SEND_METHOD)

		_argumentsMethod = t.GetMethod(ADAPTER_ARGUMENTS_METHOD)

	End Sub

	Sub Test()

		Dim m As New Message()
		Dim p As New ParametersCollection()

		ExecuteAdapter(m, p)

		Dim args As List(Of AdapterArgumentDescription) = GetListOfAdapterArguments()

	End Sub

	Private Sub ExecuteAdapter(ByVal source As Message, ByVal parameters As ParametersCollection)

		If source Is Nothing Then Throw New ArgumentNullException("source")

		ExecuteAdapterHelper(Of Boolean)(_sendMethod, _
		 GetType(AdapterSendDelegate), source, parameters)

	End Sub

	Public Function GetListOfAdapterArguments() As List(Of AdapterArgumentDescription)

		Return ExecuteAdapterHelper(Of List(Of AdapterArgumentDescription))(_argumentsMethod, _
		 GetType(AdapterArgumentsDelegate))

	End Function


	Private Function ExecuteAdapterHelper(Of T)(ByVal method As MethodInfo, _
	 ByVal delegateType As Type, _
	 ByVal ParamArray params() As Object) As T


		Dim adapterType As KeyValuePair(Of String, String) = New KeyValuePair(Of String, String)("DynamicDelegate", "DynamicDelegate.TestAdapter")

		Dim domainName As String = String.Concat("Adapter.", adapterType.Value, ".", Guid.NewGuid().ToString("N"))

		Dim evidience As New Evidence(AppDomain.CurrentDomain.Evidence)

		Dim domain As AppDomain = AppDomain.CreateDomain(domainName, evidience)

		Try

			Dim adapter As IAdapter = DirectCast(domain.CreateInstanceAndUnwrap(adapterType.Key, adapterType.Value), IAdapter)
			Dim d As [Delegate] = [Delegate].CreateDelegate(delegateType, adapter, method)

			Using executeAdapterMethod As New AsyncInvoke(d)

				executeAdapterMethod.BeginInvoke(params)

				Dim timeout As Integer = -1

				If timeout <= 0 Then

					timeout = -1

				End If

				executeAdapterMethod.EndInvoke(timeout)

				If Not executeAdapterMethod.IsCompleted Then

					executeAdapterMethod.CancelInvoke(timeout)

				End If

				If executeAdapterMethod.Exception IsNot Nothing Then

					Throw executeAdapterMethod.Exception

				End If

				Return DirectCast(executeAdapterMethod.ReturnValue, T)

			End Using

		Finally

			AppDomain.Unload(domain)

		End Try


	End Function


End Module
