Imports System.Threading
Imports System.Reflection
Imports System.Security.Permissions
Imports System.Runtime.Serialization
Imports System.Diagnostics.CodeAnalysis

Namespace Common

	Public Class AsyncInvoke
		Implements IDisposable

#Region "Constants"

		Private THREAD_ABORT_TIMEOUT As Integer = 1000

#End Region

#Region "Internal classes"

		<Serializable()> _
		Public NotInheritable Class CancelInvokeException
			Inherits Exception

#Region "Constructors"

			Public Sub New()

				MyBase.New("Thread was not aborted.")

			End Sub

			Public Sub New(ByVal message As String)

				MyBase.New(message)

			End Sub

			Public Sub New(ByVal message As String, ByVal innerException As Exception)

				MyBase.New(message, innerException)

			End Sub

			<SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.SerializationFormatter)> _
			Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)

				MyBase.New(info, context)

			End Sub

#End Region

		End Class

		Private Class Context

#Region "Private fields"

			Private _isCompleted As Boolean

			Private _exception As Exception

			Private _isCanceled As Boolean

			Private _args As Object()

			Private _delegate As [Delegate]

			Private _thread As Thread

			Private _waitObject As EventWaitHandle

			Private _returnValue As Object

#End Region

#Region "Constructors"

			Public Sub New(ByVal [delegate] As [Delegate], ByVal args As Object())

				_args = args

				_delegate = [delegate]

				_thread = New Thread(AddressOf EntryPoint)
				_thread.Name = GetThreadName(_delegate.Method.Name)

				_waitObject = New EventWaitHandle(False, EventResetMode.ManualReset)

			End Sub

#End Region

#Region "Public interface"

			Public ReadOnly Property Thread() As Thread
				Get
					Return _thread
				End Get
			End Property

			Public ReadOnly Property WaitObject() As EventWaitHandle
				Get
					Return _waitObject
				End Get
			End Property

			Public Property Exception() As Exception
				Get
					Return _exception
				End Get
				Set(ByVal value As Exception)
					_exception = value
				End Set
			End Property

			Public Property IsCompleted() As Boolean
				Get
					Return _isCompleted
				End Get
				Set(ByVal value As Boolean)
					_isCompleted = value
				End Set
			End Property

			Public Property IsCanceled() As Boolean
				Get
					Return _isCanceled
				End Get
				Set(ByVal value As Boolean)
					_isCanceled = value
				End Set
			End Property

			Public ReadOnly Property ReturnValue() As Object
				Get
					Return _returnValue
				End Get
			End Property

			Public Sub Invoke()

				If _delegate IsNot Nothing Then

					_returnValue = _delegate.DynamicInvoke(_args)

				End If

			End Sub

#End Region

#Region "Implementation"

			Private Function GetThreadName(ByVal methodName As String) As String

				Return String.Format("AsyncInvoke: {0}", methodName)

			End Function

#End Region

		End Class

#End Region

#Region "Private fields"

		Private _delegate As [Delegate]

		Private _exception As Exception

		Private _context As Context

		Private _isDisposed As Boolean

		Private _returnValue As Object

#End Region

#Region "Constructor"

		Public Sub New(ByVal [delegate] As [Delegate])

			_delegate = [delegate]

		End Sub

#End Region

#Region "Public interface"

		Public ReadOnly Property IsCompleted() As Boolean
			Get
				Me.CheckContext()

				Return _context Is Nothing
			End Get
		End Property

		Public ReadOnly Property Exception() As Exception
			Get
				Me.CheckContext()

				Return _exception
			End Get
		End Property

		Public ReadOnly Property IsDisposed() As Boolean
			Get
				Return _isDisposed
			End Get
		End Property

		Public ReadOnly Property ReturnValue() As Object
			Get
				Dim retval As Object = _returnValue

				If retval Is Nothing AndAlso _context IsNot Nothing Then
					retval = _context.ReturnValue
				End If

				Return retval
			End Get
		End Property

		Public Sub BeginInvoke(ByVal ParamArray args As Object())

			If _isDisposed Then Throw New ObjectDisposedException("AsyncInvoke")

			Me.CheckContext()

			If _context IsNot Nothing Then

				Throw New InvalidOperationException("Method has been already invoked.")

			End If

			Try

				_exception = Nothing

				_context = New Context(_delegate, args)

				_context.Thread.Start(_context)

			Catch

				If _context IsNot Nothing Then

					_context.WaitObject.Close()

					_context = Nothing

				End If

				Throw

			End Try

		End Sub

		Public Sub EndInvoke(ByVal timeout As Integer)

			If _isDisposed Then Throw New ObjectDisposedException("AsyncInvoke")

			Me.CheckContext()

			If _context Is Nothing Then Return

			_context.WaitObject.WaitOne(timeout, False)

		End Sub

		Public Sub CancelInvoke(ByVal timeout As Integer)

			If _isDisposed Then Throw New ObjectDisposedException("AsyncInvoke")

			Me.CheckContext()

			If _context Is Nothing Then Return

			_context.Thread.Abort()

			If timeout >= 0 And timeout < THREAD_ABORT_TIMEOUT Then

				timeout = THREAD_ABORT_TIMEOUT

			End If

			_context.WaitObject.WaitOne(timeout, False)

			SyncLock _context

				If Not _context.IsCompleted Then

					_context.IsCanceled = True

					_context = Nothing

					_exception = Nothing

					Throw New CancelInvokeException()

				End If

			End SyncLock

		End Sub

		Public Sub Dispose() Implements IDisposable.Dispose

			Me.Dispose(True)

			GC.SuppressFinalize(Me)

		End Sub

#End Region

#Region "Implementation"

		Private Sub CheckContext()

			If _context Is Nothing Then Return

			SyncLock _context

				If _context.IsCompleted Then

					_returnValue = _context.ReturnValue

					_context.WaitObject.Close()

					_exception = _context.Exception

					_context = Nothing

				End If

			End SyncLock

		End Sub

		<System.Diagnostics.DebuggerStepThrough()> _
		<SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
		Private Shared Sub EntryPoint(ByVal obj As Object)

			Dim context As Context = DirectCast(obj, Context)

			Try

				context.Invoke()

			Catch ex As Exception

				context.Exception = ex

			Finally

				SyncLock context

					context.IsCompleted = True

					context.WaitObject.Set()

					If context.IsCanceled Then

						context.WaitObject.Close()

					End If

				End SyncLock

			End Try

		End Sub

		<SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
		Protected Overridable Sub Dispose(ByVal isDisposing As Boolean)

			If _isDisposed Then Return

			If isDisposing Then

				Try

					Me.CancelInvoke(THREAD_ABORT_TIMEOUT)

				Catch

				End Try

				Try

					Me.CheckContext()

				Catch

				End Try

			End If

			_delegate = Nothing
			_exception = Nothing
			_context = Nothing
			_returnValue = Nothing

			_isDisposed = True

		End Sub

		Protected Overrides Sub Finalize()

			Me.Dispose(False)

		End Sub

#End Region

	End Class

End Namespace