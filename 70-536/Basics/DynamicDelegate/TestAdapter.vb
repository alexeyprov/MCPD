Imports System.Collections.Generic

<Serializable()> _
Public Class TestAdapter
	Implements IAdapter


	Public ReadOnly Property Arguments() As List(Of AdapterArgumentDescription) Implements IAdapter.Arguments
		Get
			Dim args As New List(Of AdapterArgumentDescription)(2)
			args.Add(New AdapterArgumentDescription())
			args.Add(New AdapterArgumentDescription())

			Return args
		End Get
	End Property

	Public Function Send(ByVal source As Message, ByVal configuration As ParametersCollection) As Boolean Implements IAdapter.Send

		Return True

	End Function
End Class
