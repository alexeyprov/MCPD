Public Interface IAdapter

	ReadOnly Property Arguments() As List(Of AdapterArgumentDescription)

	Function Send(ByVal source As Message, ByVal configuration As ParametersCollection) As Boolean

End Interface
