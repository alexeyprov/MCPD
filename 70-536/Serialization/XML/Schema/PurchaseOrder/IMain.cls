VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "IMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Public Function Validate(ByVal POXml As String) As String

Dim oPOSchema       As MSXML2.DOMDocument40
Dim oSchemaCache    As MSXML2.XMLSchemaCache40
Dim oPODoc          As MSXML2.DOMDocument40
        
Set oPOSchema = New MSXML2.DOMDocument40
Set oSchemaCache = New MSXML2.XMLSchemaCache40

oPOSchema.async = False
oPOSchema.Load App.Path + "\po.xsd"

oSchemaCache.Add "", oPOSchema

Set oPODoc = New MSXML2.DOMDocument40

With oPODoc
    .async = False
    .validateOnParse = False
    Set .schemas = oSchemaCache
    .loadXML POXml
End With

If oPODoc.parseError <> 0 Then
    Err.Raise vbObjectError + 1, "PO Validation", oPODoc.parseError.reason & " Line:" & oPODoc.parseError.Line & " Pos:" & oPODoc.parseError.linepos
Else
    Validate = oPODoc.xml
End If
    
End Function
