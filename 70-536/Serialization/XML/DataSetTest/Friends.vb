﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.42
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System.Xml.Serialization

'
'This source code was auto-generated by xsd, Version=2.0.50727.42.
'

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true),  _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=false)>  _
Partial Public Class Friends
    
    Private itemsField() As FriendsPeople
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("People", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
    Public Property Items() As FriendsPeople()
        Get
            Return Me.itemsField
        End Get
        Set
            Me.itemsField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42"),  _
 System.SerializableAttribute(),  _
 System.Diagnostics.DebuggerStepThroughAttribute(),  _
 System.ComponentModel.DesignerCategoryAttribute("code"),  _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=true)>  _
Partial Public Class FriendsPeople
    
    Private nameField As String
    
    Private doBField As String
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
    Public Property Name() As String
        Get
            Return Me.nameField
        End Get
        Set
            Me.nameField = value
        End Set
    End Property
    
    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
    Public Property DoB() As String
        Get
            Return Me.doBField
        End Get
        Set
            Me.doBField = value
        End Set
    End Property
End Class
