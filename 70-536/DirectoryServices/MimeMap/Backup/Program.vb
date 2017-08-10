Imports System
Imports System.DirectoryServices
Imports System.Runtime.InteropServices

Module Program

	Private Const MIME_MAP_AD_ENTRY As String = "IIS://Localhost/MimeMap"
	Private Const MIME_MAP_AD_PROPERTY As String = "MimeMap"

	Private Const DOT As String = "."


	Sub Main()
		Using mimeMap As DirectoryEntry = New DirectoryEntry(MIME_MAP_AD_ENTRY)
			Dim propValues As PropertyValueCollection = mimeMap.Properties(MIME_MAP_AD_PROPERTY)

			For Each value As Object In propValues

				Dim mimeType As IISOle.IISMimeType = CType(value, IISOle.IISMimeType)

				Console.WriteLine(String.Format("{0},{1}", mimeType.Extension, mimeType.MimeType))

				Marshal.ReleaseComObject(mimeType)

			Next

		End Using

	End Sub

End Module
