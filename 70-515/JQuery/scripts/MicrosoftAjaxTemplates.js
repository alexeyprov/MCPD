//----------------------------------------------------------
// Copyright (C) Microsoft Corporation. All rights reserved.
//----------------------------------------------------------
// MicrosoftAjaxTemplates.js
Type.registerNamespace("Sys.Preview.UI");Sys.Application.disposeElement=function(a,b){this._disposeElementRecursive(a);if(!b)this._disposeElementInternal(a)};Sys.Application._disposeElementRecursive=function(d){if(d.nodeType===1){var c=d.childNodes;for(var b=c.length-1;b>=0;b--){var a=c[b];if(a.nodeType===1){Sys.Application._disposeElementInternal(a);this._disposeElementRecursive(a)}}}};Sys.Application._disposeElementInternal=function(a){if(a.dispose&&typeof a.dispose==="function")a.dispose();else if(a.control&&typeof a.control.dispose==="function")a.control.dispose();var c=Sys.UI.Behavior.getBehaviors(a);for(var b=c.length-1;b>=0;b--)c[b].dispose()};Sys.UI.DomElement._oldGetElementById=Sys.UI.DomElement.getElementById;Sys.UI.DomElement.getElementById=function(c,h){var a=Sys.UI.DomElement._oldGetElementById(c,h);if(!a&&!h&&Sys.Preview.UI.Template._contexts.length){var f=Sys.Preview.UI.Template._contexts;for(var d=0,i=f.length;d<i;d++){var g=f[d];for(var e=0,j=g.length;e<j;e++){var b=g[e];if(b.nodeType===1){if(b.id===c)return b;a=Sys.UI.DomElement._oldGetElementById(c,b);if(a)return a}}}}return a};if($get===Sys.UI.DomElement._oldGetElementById)$get=Sys.UI.DomElement.getElementById;Sys.UI.DomElement.isDomElement=function(a){var c=false;if(typeof a.nodeType!=="number"){var b=a.ownerDocument||a.document||a;if(b!=a){var d=b.defaultView||b.parentWindow;c=d!=a&&!(d.document&&a.document&&d.document===a.document)}else c=typeof b.body==="undefined"}return !c};Sys.Application.registerMarkupExtension=function(c,b,a){var d=Object.getType(b);if(!d.implementsInterface(Sys.Preview.UI.IMarkupExtension)){a=typeof a==="undefined"||a===true;b=new Sys.Preview.UI.GenericMarkupExtension(b,a)}if(!this._extensions)this._extensions={};this._extensions[c]=b};Sys.Application._getMarkupExtension=function(b){var a=this._extensions?this._extensions[b]:null;if(!a)throw Error.invalidOperation("A markup extension with the name '"+b+"' could not be found.");return a};Sys.Preview.UI.DOMProcessor={};Sys.Preview.UI.DOMProcessor.processNode=function(a,c,b){return Sys.Preview.UI.DOMProcessor._processNodeWithMappings(Sys.Preview.UI.DOMProcessor._getNamespaceMappings(null,[a]),a,c,b)};Sys.Preview.UI.DOMProcessor.processNodes=function(c,e,d){var b=[];for(var a=0,f=c.length;a<f;a++)Array.addRange(b,Sys.Preview.UI.DOMProcessor.processNode(c[a],e,d));return b};Sys.Preview.UI.DOMProcessor._processNodeWithMappings=function(d,h,e,f){var g={userContext:e,localContext:{}},a=[];Sys.Preview.UI.DOMProcessor._processNodeInternal(h,d,a,g,f);for(var b=0,i=a.length;b<i;b++){var c=a[b];if(Sys.Component.isInstanceOfType(c))c.endUpdate()}return a};Sys.Preview.UI.DOMProcessor._processNodeInternal=function(b,f,s,i,t){if(b.__msajaxactivated)return;var a,d,c,p=null,q=null;try{p=b.getAttribute(f.types)}catch(z){}try{q=b.getAttribute(f.sysKey)}catch(z){}if(q)i.localContext[q]=b;if(p){b.__msajaxactivated=true;var u=p.split(","),o={},l=[];for(a=0,d=u.length;a<d;a++){var h=u[a].trim();if(o[h])continue;var e=f.namespaces[h];if(e){var r=e.inheritsFrom(Sys.Component);c=r&&(e.inheritsFrom(Sys.UI.Behavior)||e.inheritsFrom(Sys.UI.Control))?new e(b):new e;if(r){l.push(c);c.beginUpdate()}o[h]={instance:c,typeName:h,type:e};s.push(c)}}for(a=0,d=b.attributes.length;a<d;a++){var g=b.attributes[a];if(!g.specified)continue;var m=g.nodeName;if(m===f.sysKey||m===f.types)continue;var n=Sys.Preview.UI.DOMProcessor._splitAttribute(m),w=n.ns;if(!w)continue;var j=o[w];if(!j)continue;if(n.name==="sys-key")i.localContext[g.nodeValue]=j.instance;else Sys.Preview.UI.DOMProcessor._setProperty(j.instance,j.type,n.name,g.nodeValue,i)}var k=Sys.Application,x=k.get_isCreatingComponents();for(a=0,d=l.length;a<d;a++){c=l[a];if(c.get_id())k.addComponent(c);if(x)k._createdComponents[k._createdComponents.length]=c}}if(t||typeof t==="undefined"){var y=b.className;if(!Sys.Preview.UI.Template._isTemplate(b))for(a=0,d=b.childNodes.length;a<d;a++){var v=b.childNodes[a];if(v.nodeType!==3)Sys.Preview.UI.DOMProcessor._processNodeInternal(v,f,s,i,true)}}};Sys.Preview.UI.DOMProcessor._splitAttribute=function(c){var a=c.split(":"),b=a.length>1?a[0]:null,d=a[b?1:0];return {ns:b,name:d}};Sys.Preview.UI.DOMProcessor._getBodyNamespaceMapping=function(){if(Sys.Preview.UI.DOMProcessor._bodyNamespaceMapping)return Sys.Preview.UI.DOMProcessor._bodyNamespaceMapping;var a={sysNamespace:"sys",types:"sys:attach",sysId:"sys:id",sysKey:"sys:key",sysActivate:"sys:activate",sysChecked:"sys:checked",styleNamespace:"style",classNamespace:"class",namespaces:{}};Sys.Preview.UI.DOMProcessor._getNamespaceMapping(a,document.body);Sys.Preview.UI.DOMProcessor._bodyNamespaceMapping=a;return a};Sys.Preview.UI.DOMProcessor._getNamespaceMappings=function(d,c){var b=d||Sys.Preview.UI.DOMProcessor._getBodyNamespaceMapping();for(var a=0,e=c.length;a<e;a++)Sys.Preview.UI.DOMProcessor._getNamespaceMapping(b,c[a]);return b};Sys.Preview.UI.DOMProcessor._getNamespaceMapping=function(namespaceMapping,element){var attributes=element.attributes;for(var i=0,l=attributes.length;i<l;i++){var attribute=attributes[i];if(!attribute.specified)continue;var attrib=Sys.Preview.UI.DOMProcessor._splitAttribute(attribute.nodeName);if(attrib.ns!=="xmlns")continue;var name=attrib.name,value=attribute.nodeValue.trim();if(value.toLowerCase().startsWith("javascript:")){value=value.substr(11).trimStart();if(value==="Sys")with(namespaceMapping){sysNamespace=name;types=name+":attach";sysId=name+":id";sysChecked=name+":checked";sysActivate=name+":activate";sysKey=name+":key"}else{var type=Type.parse(value);if(type)namespaceMapping.namespaces[name]=type}}else if(value==="http://schemas.microsoft.com/aspnet/style")namespaceMapping.styleNamespace=name;else if(value==="http://schemas.microsoft.com/aspnet/class")namespaceMapping.classNamespace=name}};Sys.Preview.UI.DOMProcessor._getExtensionValue=function(c,j){var d,a,g=c.indexOf(" ");if(g!==-1){d=c.substr(0,g);a=c.substr(g+1).trim()}else{d=c;a=null}if(a){a=a.replace(/\\,/g,"\x00").split(/,/g);var e={};for(var h=0,l=a.length;h<l;h++){var b=a[h].replace(/\u0000/g,","),f=b.indexOf("=");if(f!==-1)e[b.substr(0,f).trim()]=b.substr(f+1).trim();else e["$default"]=b.trim()}}var i=Sys.Application._getMarkupExtension(d),k=i.provideValue(j,e);return {code:k,isExpression:i.get_isExpression()}};Sys.Preview.UI.DOMProcessor._setProperty=function(f,g,c,a,j){var b=a,d,i=true;if(a.startsWith("{{")&&a.endsWith("}}"))d=a.slice(2,-2);else if(a.startsWith("{")&&a.endsWith("}")){var h=this._getExtensionValue(a.slice(1,-1),c);d=h.code;i=h.isExpression}if(d){b=this._evaluateExpression(d,j);if(!i||typeof b==="undefined")return}if(!Sys.Preview.UI.DOMProcessor._trySet(f,g,c,b)){var e=Sys.Preview.UI.DOMProcessor._mapToPrototype(c,g);if(e&&e!==c)Sys.Preview.UI.DOMProcessor._trySet(f,g,e,b);else f[c]=b}};Sys.Preview.UI.DOMProcessor._trySet=function(a,f,b,c){var d=a["set_"+b];if(typeof d==="function"){d.call(a,c);return true}var e=a["add_"+b];if(typeof e==="function"){e.call(a,new Function("sender","args",c));return true}if(typeof a[b]!=="undefined"){a[b]=c;return true}return false};Sys.Preview.UI.DOMProcessor._mapToPrototype=function(d,c){var b=Sys.Preview.UI.DOMProcessor._caseIndex[c.__typeName];if(!b){b={};c.resolveInheritance();for(var a in c.prototype){if(a.startsWith("get_")||a.startsWith("set_")||a.startsWith("add_"))a=a.substr(4);else if(a.startsWith("remove_"))a=a.substr(7);b[a.toLowerCase()]=a}Sys.Preview.UI.DOMProcessor._caseIndex[c.__typeName]=b}return b[d.toLowerCase()]};Sys.Preview.UI.DOMProcessor._doEval=function($expression,$context){with($context.localContext)with($context.userContext||{})return eval("("+$expression+")")};Sys.Preview.UI.DOMProcessor._evaluateExpression=function(b,a){return Sys.Preview.UI.DOMProcessor._doEval.call(a.userContext,b,a)};Sys.Preview.UI.DOMProcessor._caseIndex={};Sys.Application.add_init(function(){var e=Sys.Preview.UI.DOMProcessor._getBodyNamespaceMapping(),a=document.body.getAttribute(e.sysActivate);if(!a)return;if(a==="*")Sys.Preview.UI.DOMProcessor.processNode(document.body);else{var c=a.split(",");for(var b=0,f=c.length;b<f;b++){var d=document.getElementById(c[b].trim());if(d)Sys.Preview.UI.DOMProcessor.processNode(d)}}});Sys.Preview.UI.Template=function(a){a._msajaxtemplate=this;this._element=a;this._instanceId=0;this._nestedTemplates=null;this._createInstance=this._compile()};Sys.Preview.UI.Template.prototype={get_element:function(){return this._element},dispose:function(){var b=this.get_element();if(b)b._msajaxtemplate=null;if(this._nestedTemplates){for(var a=0,c=this._nestedTemplates.length;a<c;a++)this._nestedTemplates[a].dispose();this._nestedTemplates=null}},_appendTextNode:function(b,a,c){b.push(a+"document.createTextNode("+Sys.Serialization.JavaScriptSerializer.serialize(c)+"));\n")},_trySet:function(d,f,a,b){f.resolveInheritance();var c=f.prototype,e="set_"+a,h=c[e];if(typeof h==="function"){d.push("  $component."+e+"("+b+");\n");return true}var g="add_"+a,i=c["add_"+a];if(typeof i==="function"){d.push("  $component."+g+'(new Function("sender", "args", '+b+"));\n");return true}if(typeof c[a]!=="undefined"){d.push("  $component."+a+" = "+b+";\n");return true}return false},_appendAttributeSetter:function(g,b,l,k,c,i,h){var d=k.ns,a=k.name;if(d)if(d===g.classNamespace){a=Sys.Serialization.JavaScriptSerializer.serialize(a);b.push("  ("+c+") ? Sys.UI.DomElement.addCssClass($element, "+a+") : Sys.UI.DomElement.removeCssClass($element, "+a+");\n")}else if(d===g.styleNamespace)b.push("  $component = $element;\n  $element.style."+this._translateStyleName(a)+" = "+c+";\n;");else{var e=l[d];if(e)b.push("  $component = __componentIndex['"+d+"'];\n");if(a==="sys-key")b.push("  __context["+c+"] = $component;\n");else if(i){if(!this._trySet(b,e.type,a,c)){var f=Sys.Preview.UI.DOMProcessor._mapToPrototype(a,e.type);if(f&&f!==a)this._trySet(b,e.type,f,c);else b.push("  $component."+a+" = "+c+";\n")}}else b.push("  "+c+";\n")}else if(i){var j=a.toLowerCase();if(j.startsWith("on"))b.push("  $component = $element;\n  __f = new Function("+c+");\n  $element."+a+" = __f;\n");else if(j==="style")b.push("  $component = $element;\n  $element.style.cssText = "+c+";\n");else if(h)b.push("  $component = $element;\n  if ("+c+") {\n    __e = document.createAttribute('"+a+"');\n    __e.nodeValue = \""+h+'";\n    $element.setAttributeNode(__e);\n  }\n');else b.push("  $component = $element;\n  __e = document.createAttribute('"+a+"');\n  __e.nodeValue = "+c+";\n  $element.setAttributeNode(__e);\n")}else b.push("  $component = $element;\n  "+c+";\n")},_translateStyleName:function(b){if(b.indexOf("-")===-1)return b;var a=b.toLowerCase().split("-"),d=a[0];for(var c=1,f=a.length;c<f;c++){var e=a[c];d+=e.substr(0,1).toUpperCase()+e.substr(1)}return d},_processAttribute:function(c,f,e,b,a,d){a=this._getAttributeExpression(b,a);if(a!==null)this._appendAttributeSetter(c,f,e,b,a.code,a.isExpression,d)},_getAttributeExpression:function(c,a){var b=typeof a;if(b==="undefined")return null;if(a===null)return {isExpression:true,code:"null"};if(b==="string")if(a.startsWith("{{")&&a.endsWith("}}"))return {isExpression:true,code:a.slice(2,-2).trim()};else if(a.startsWith("{")&&a.endsWith("}"))return Sys.Preview.UI.DOMProcessor._getExtensionValue(a.slice(1,-1),c.name);return {isExpression:true,code:Sys.Serialization.JavaScriptSerializer.serialize(a)}},_processBooleanAttribute:function(e,d,g,f,c){var a,b=e.getAttributeNode(d.sysNamespace+":"+c);if(!b){b=e.getAttributeNode(c);if(b&&(b.specified||b.nodeValue===true))a=true;else return}else{a=b.nodeValue;if(a==="true")a=true;else if(a==="false")return}this._processAttribute(d,g,f,{name:c},a,c)},_processBooleanAttributes:function(e,c,f,d,b){var i,j,h;for(var a=0,g=b.length;a<g;a++)this._processBooleanAttribute(e,c,f,d,b[a])},_getExplicitAttribute:function(e,h,f,c,d,b){var a;try{a=c.getAttributeNode(d)}catch(i){return null}if(!a||!a.specified)return null;if(b){var g=d==="style"?c.style.cssText:a.nodeValue;this._processAttribute(e,h,f,{name:b},g)}return a.nodeValue},_buildTemplateCode:function(c,D,a,y){var t,g,H,u,f,n,A=Sys.Preview.UI.Template.expressionRegExp,h="  "+(y?"__p["+y+"].appendChild(":"__topElements.push(");for(t=0,H=D.childNodes.length;t<H;t++){var b=D.childNodes[t],d=b.nodeValue;if(b.nodeType===8)if(d.startsWith("*")&&d.endsWith("*"))a.push("  "+d.slice(1,-1)+"\n");else a.push(h+"document.createComment("+Sys.Serialization.JavaScriptSerializer.serialize(d)+"));\n");else if(b.nodeType===3)if(d.startsWith("{")&&d.endsWith("}")&&!d.startsWith("{{")&&!d.startsWith("}}")){var z=this._getAttributeExpression({name:"nodeValue"},d);if(z.isExpression)a.push(h+"document.createTextNode("+z.code+"));\n");else a.push(h+'$element=$component=document.createTextNode(""));\n'+"  "+z.code+";\n")}else{var k=A.exec(d),p=0;while(k){var B=d.substring(p,k.index);if(B)this._appendTextNode(a,h,B);a.push(h+"document.createTextNode("+k[1]+"));\n");p=k.index+k[0].length;k=A.exec(d)}if(p<d.length)this._appendTextNode(a,h,d.substr(p))}else{var C=b.attributes,j=null,v=null,e={},E=b.tagName.toLowerCase(),l,s=y+1;n=E==="input";if(n){var K=this._getAttributeExpression({name:"type"},b.getAttribute("type")),J=this._getAttributeExpression({name:"name"},b.getAttribute("name"));a.push("  $element=__p["+s+"]=Sys.Preview.UI.Template._createInput("+K.code+", "+J.code+");\n");l=Sys.Preview.UI.Template._inputBooleanAttributes;this._processBooleanAttributes(b,c,a,e,l)}else a.push("  $element=__p["+s+"]=document.createElement('"+b.nodeName+"');\n");j=this._getExplicitAttribute(c,a,e,b,c.types);if(j){j=j.split(",");a.push("  __componentIndex = {}\n");for(g=0,u=j.length;g<u;g++){f=j[g].trim();if(e[f])continue;var i=c.namespaces[f];if(i){var w=i.inheritsFrom(Sys.Component),I=w&&(i.inheritsFrom(Sys.UI.Behavior)||i.inheritsFrom(Sys.UI.Control)),L=i.implementsInterface(Sys.Preview.UI.ITemplateContext);e[f]={type:i,isComponent:w};a.push("  __components.push(__componentIndex['"+f+"'] = $component = new "+i.getName());if(I)a.push("($element));\n");else a.push("());\n");if(w)a.push("  $component.beginUpdate();\n");if(L)a.push("  $component.set_parentContext({ dataItem: $dataItem || window, parentContext: $parentContext });\n")}}}v=this._getExplicitAttribute(c,a,e,b,c.sysKey);if(v)a.push("  __context["+Sys.Serialization.JavaScriptSerializer.serialize(v)+"] = $element;\n");this._getExplicitAttribute(c,a,e,b,c.sysId,"id");this._getExplicitAttribute(c,a,e,b,"style","style");this._getExplicitAttribute(c,a,e,b,"class","class");if(!n){l=Sys.Preview.UI.Template._booleanAttributes[E]||Sys.Preview.UI.Template._commonBooleanAttributes;this._processBooleanAttributes(b,c,a,e,l)}for(g=0,u=C.length;g<u;g++){var x=C[g],r=x.nodeName,m=r.toLowerCase();if(!x.specified&&(!n||m!=="value"))continue;if(m==="class"||m==="style")continue;if(Array.indexOf(l,m)!==-1)continue;if(n&&Array.indexOf(Sys.Preview.UI.Template._inputRequiredAttributes,m)!==-1)continue;var q=Sys.Preview.UI.DOMProcessor._splitAttribute(r),G=q.ns,M=x.nodeValue,F=false;r=q.name;if(G){F=G===c.sysNamespace;if(F){if(Array.indexOf(Sys.Preview.UI.Template._sysAttributes,r)!==-1)continue;q.ns=null}}this._processAttribute(c,a,e,q,M)}a.push(h+"$element);\n");for(f in e){index=e[f];if(index.isComponent)a.push("  if (($component=__componentIndex['"+f+"']).get_id()) __app.addComponent($component);\nif (__creatingComponents) __app._createdComponents[__app._createdComponents.length] = $component;\n")}if(Sys.Preview.UI.Template._isTemplate(b)){var o=this._nestedTemplates;if(!o)this._nestedTemplates=o=[];o.push(new Sys.Preview.UI.Template(b));a.push("  $element._msajaxtemplate = this._nestedTemplates["+(o.length-1)+"];\n")}else{this._buildTemplateCode(c,b,a,s);a.push("  $element=__p["+s+"];\n")}}}},_compile:function(){var b=this.get_element(),a=[" var __context = {}, $component, __app = Sys.Application, __creatingComponents = __app.get_isCreatingComponents(), __components = [], __componentIndex, __e, __f, __topElements = [], __p = [__containerElement], $index = __instanceId, $id = Sys.Preview.UI.Template._getIdFunction(__instanceId), $element = __containerElement;\n Sys.Preview.UI.Template._contexts.push(__topElements);\n with(__context) { with($dataItem || {}) {\n"],c=Sys.Preview.UI.DOMProcessor._getNamespaceMappings(null,[b]);this._buildTemplateCode(c,b,a,0);a.push("} }\n  for (var __i = 0, __l = __topElements.length; __i < __l; __i++) {\n  __containerElement.appendChild(__topElements[__i]);\n }\n");a.push(" Sys.Preview.UI.Template._contexts.pop();\n");a.push(" return new Sys.Preview.UI.TemplateResult(this, __containerElement, __topElements, __components);");a=a.join("");return new Function("__containerElement","$dataItem","$parentContext","__instanceId",a)},createInstance:function(b,c,a){return this._createInstance(b,c,a,this._instanceId++)}};Sys.Preview.UI.Template.getTemplate=function(a){var b=a._msajaxtemplate;if(b)return b;return a._msajaxtemplate=new Sys.Preview.UI.Template(a)};Sys.Preview.UI.Template._getIdFunction=function(a){return function(b){return b+a}};Sys.Preview.UI.Template._createInput=function(c,b){var a,e=Sys.Preview.UI.Template._dynamicInputs;if(e===true){a=document.createElement("input");if(c)a.type=c;if(b)a.name=b}else{var d="<input ";if(c)d+="type='"+c+"' ";if(b)d+="name='"+b+"' ";d+="/>";try{a=document.createElement(d)}catch(f){Sys.Preview.UI.Template._dynamicInputs=true;return Sys.Preview.UI.Template._createInput(c,b)}if(e!==false)if(a.tagName.toLowerCase()==="input")Sys.Preview.UI.Template._dynamicInputs=false;else{Sys.Preview.UI.Template._dynamicInputs=true;return Sys.Preview.UI.Template._createInput(c,b)}}return a};Sys.Preview.UI.Template._isTemplate=function(b){var a=b.className;return a&&(a==="sys-template"||Array.contains(a.split(" "),"sys-template"))};Sys.Preview.UI.Template._contexts=[];Sys.Preview.UI.Template._inputRequiredAttributes=["type","name"];Sys.Preview.UI.Template._commonBooleanAttributes=["disabled"];Sys.Preview.UI.Template._inputBooleanAttributes=["disabled","checked","readonly"];Sys.Preview.UI.Template._booleanAttributes={"input":Sys.Preview.UI.Template._inputBooleanAttributes,"select":["disabled","multiple"],"option":["disabled","selected"],"img":["disabled","ismap"],"textarea":["disabled","readonly"]};Sys.Preview.UI.Template._sysAttributes=["attach","id","key","disabled","checked","readonly","ismap","multiple","selected"];Sys.Preview.UI.Template.expressionRegExp=/\{\{\s*([\w\W]*?)\s*\}\}/g;Sys.Preview.UI.Template.registerClass("Sys.Preview.UI.Template",null,Sys.IDisposable);Sys.Preview.UI.TemplateResult=function(d,b,c,a){this._template=d;this._container=b;this._elements=c;this._components=a};Sys.Preview.UI.TemplateResult.prototype={get_container:function(){return this._container||null},get_components:function(){return this._components||[]},get_elements:function(){return this._elements||[]},get_template:function(){return this._template||null},dispose:function(){var a=this.get_elements();if(a)for(var b=0,d=a.length;b<d;b++){var c=a[b];if(c.nodeType===1)Sys.Application.disposeElement(c,false)}this._template=null;this._elements=null;this._components=null;this._container=null},initializeComponents:function(){var b=this.get_components();if(b)for(var c=0,d=b.length;c<d;c++){var a=b[c];if(Sys.Component.isInstanceOfType(a))if(a.get_isUpdating())a.endUpdate();else if(!a.get_isInitialized())a.initialize()}}};Sys.Preview.UI.TemplateResult.registerClass("Sys.Preview.UI.TemplateResult",null,Sys.IDisposable);Sys.Preview.UI.ITemplateContext=function(){};Sys.Preview.UI.ITemplateContext.prototype={get_parentContext:function(){throw Error.notImplemented()},set_parentContext:function(){throw Error.notImplemented()}};Sys.Preview.UI.ITemplateContext.registerInterface("Sys.Preview.UI.ITemplateContext");Sys.Preview.UI.IMarkupExtension=function(){};Sys.Preview.UI.IMarkupExtension.prototype={get_isExpression:function(){throw Error.notImplemented()},provideValue:function(){throw Error.notImplemented()}};Sys.Preview.UI.IMarkupExtension.registerInterface("Sys.Preview.UI.IMarkupExtension");Sys.Preview.UI.GenericMarkupExtension=function(a,b){this._provideValue=a;this._isExpression=b};Sys.Preview.UI.GenericMarkupExtension.prototype={get_isExpression:function(){return this._isExpression},provideValue:function(a,b){return this._provideValue(a,b)}};Sys.Preview.UI.GenericMarkupExtension.registerClass("Sys.Preview.UI.GenericMarkupExtension",null,Sys.Preview.UI.IMarkupExtension);Sys.Preview.BindingMode=function(){};Sys.Preview.BindingMode.prototype={oneTime:0,oneWay:1,twoWay:2,oneWayToSource:3};Sys.Preview.BindingMode.registerEnum("Sys.Preview.BindingMode");Sys.Preview.Binding=function(){Sys.Preview.Binding.initializeBase(this)};Sys.Preview.Binding.prototype={_mode:Sys.Preview.BindingMode.oneWay,_path:null,_propertyName:null,_source:null,_sourceHandlers:null,_target:null,_targetHandlers:null,_updateSource:false,_updateTarget:false,get_mode:function(){return this._mode||null},set_mode:function(a){this._mode=a},get_source:function(){return this._source||null},set_source:function(a){this._source=a},get_path:function(){return this._path||null},set_path:function(a){if(a!==this._path){this._path=a;this._pathArray=a?a.split("."):null}},get_target:function(){return this._target||null},set_target:function(a){this._target=a},get_propertyName:function(){return this._propertyName||null},set_propertyName:function(a){if(a!==this._propertyName){this._propertyName=a;this._propertyNameArray=a?a.split("."):null}},_addBinding:function(a){if(a.nodeType===3){a=a.parentNode;if(!a)return}var b=a._msajaxBindings;if(!b)a._msajaxBindings=[this];else b.push(this);if(typeof a.dispose!=="function")a.dispose=Sys.Preview.Binding._disposeBindings},_disposeHandlers:function(b,a){if(a[0]){Sys.UI.DomEvent.removeHandler(b,"click",a[0]);a[0]=null}if(a[1]){Sys.UI.DomEvent.removeHandler(b,"keyup",a[1]);a[1]=null}if(a[2]){Sys.UI.DomEvent.removeHandler(b,"change",a[2]);a[2]=null}if(a[3]){b.remove_propertyChanged(a[3]);a[3]=null}if(a[4]){b.remove_disposing(a[4]);a[4]=null}},dispose:function(){if(this._sourceHandlers){this._disposeHandlers(this.get_source(),this._sourceHandlers);delete this._sourceHandlers}if(this._targetHandlers){this._disposeHandlers(this.get_target(),this._targetHandlers);delete this._targetHandlers}Sys.Preview.Binding.callBaseMethod(this,"dispose")},_getPropertyFromIndex:function(a,d,e){for(var c=0;c<=e;c++){a=this._getPropertyData(a,d[c]);var b=typeof a;if(c<d.length-1&&(a===null||b==="undefined"||b==="number"||b==="date"||b==="string"||a instanceof Array))throw Error.invalidOperation(Sys.Preview.TemplateRes.nullReferenceInPath)}return a},_getPropertyData:function(b,a){if(typeof b["get_"+a]==="function")return b["get_"+a]();else return b[a]},_onDisposing:function(){this.dispose()},_setPropertyData:function(a,b,c){if(typeof a["set_"+b]==="function")a["set_"+b](c);else a[b]=c},update:function(a){a=a||this.get_mode();if(a===Sys.Preview.BindingMode.oneWayToSource){delete this._lastTarget;this._onTargetPropertyChanged()}else{delete this._lastSource;this._onSourcePropertyChanged()}},_hookEvent:function(a,b,d,f){if(Sys.UI.DomElement.isDomElement(a)){if(a.nodeType===1){var c=a.tagName.toLowerCase();if(c==="input"||c==="select"||c==="textarea"){var e=a.type;if(c==="input"&&e&&(e.toLowerCase()==="checkbox"||e.toLowerCase()==="radio")){b[0]=Function.createDelegate(this,d);Sys.UI.DomEvent.addHandler(a,"click",b[0])}if(c==="select"){b[1]=Function.createDelegate(this,d);Sys.UI.DomEvent.addHandler(a,"keyup",b[1])}b[2]=Function.createDelegate(this,d);Sys.UI.DomEvent.addHandler(a,"change",b[2]);this._addBinding(a)}}}else{if(Sys.INotifyPropertyChange.isImplementedBy(a)){b[3]=Function.createDelegate(this,f);a.add_propertyChanged(b[3])}if(Sys.INotifyDisposing.isImplementedBy(a)){b[4]=Function.createDelegate(this,this._onDisposing);a.add_disposing(b[4])}}},initialize:function(){var a=this.get_source(),b=this.get_target(),c=this.get_mode();Sys.Preview.Binding.callBaseMethod(this,"initialize");this.update(c);if(c!==Sys.Preview.BindingMode.oneWayToSource){this._sourceHandlers=[];this._hookEvent(a,this._sourceHandlers,this._onSourcePropertyChanged,this._onComponentSourceChanged)}else if(Sys.UI.DomElement.isDomElement(a))this._addBinding(a);if(c!==Sys.Preview.BindingMode.oneWay){this._targetHandlers=[];this._hookEvent(b,this._targetHandlers,this._onTargetPropertyChanged,this._onComponentTargetChanged)}else if(Sys.UI.DomElement.isDomElement(b))this._addBinding(b)},_onComponentSourceChanged:function(b,a){if(a.get_propertyName()===this._pathArray[0])this._onSourcePropertyChanged()},_onComponentTargetChanged:function(b,a){if(a.get_propertyName()===this._propertyNameArray[0])this._onTargetPropertyChanged()},_onSourcePropertyChanged:function(){var a=this._getPropertyFromIndex(this.get_source(),this._pathArray,this._pathArray.length-1);if(!this._updateSource&&a!==this._lastSource)try{this._updateTarget=true;var b=this._propertyNameArray.length,c=this._getPropertyFromIndex(this.get_target(),this._propertyNameArray,b-2);this._setPropertyData(c,this._propertyNameArray[b-1],a);this._lastSource=a;this._lastTarget=a}finally{this._updateTarget=false}},_onTargetPropertyChanged:function(){var a=this._getPropertyFromIndex(this.get_target(),this._propertyNameArray,this._propertyNameArray.length-1);if(!this._updateTarget&&a!==this._lastTarget)try{this._updateSource=true;var b=this._pathArray.length,c=this._getPropertyFromIndex(this.get_source(),this._pathArray,b-2);this._setPropertyData(c,this._pathArray[b-1],a);this._lastTarget=this._lastSource=a}finally{this._updateSource=false}}};Sys.Preview.Binding.bind=function(d,b,c,f,e){var a=new Sys.Preview.Binding;a.set_source(c);a.set_path(f);a.set_target(d);a.set_propertyName(b);a.set_mode(e);a.initialize();return a};Sys.Preview.Binding._disposeBindings=function(){var a=this._msajaxBindings;if(a)for(var b=0,c=a.length;b<c;b++)a[b].dispose();this._msajaxBindings=null;if(this.control&&typeof this.control.dispose==="function")this.control.dispose();if(this.dispose===Sys.Preview.Binding._disposeBindings)this.dispose=null};Sys.Preview.Binding.registerClass("Sys.Preview.Binding",Sys.Component);Sys.Application.registerMarkupExtension("binding",function(b,a){var d=a["$default"]||a["path"]||null,c=a["mode"]||"twoWay";return "Sys.Preview.Binding.bind($component, "+Sys.Serialization.JavaScriptSerializer.serialize(b)+", "+(a["source"]||"$dataItem")+", "+Sys.Serialization.JavaScriptSerializer.serialize(d)+", Sys.Preview.BindingMode."+c+");"},false);Sys.Preview.UI.DataView=function(a){Sys.Preview.UI.DataView.initializeBase(this,[a])};Sys.Preview.UI.DataView.prototype={_data:null,_template:null,_parentContext:null,_results:null,_changed:false,_dirty:false,add_itemCreated:function(a){this.get_events().addHandler("itemCreated",a)},remove_itemCreated:function(a){this.get_events().removeHandler("itemCreated",a)},get_data:function(){return this._data},set_data:function(a){if(this._data!==a){this._data=a;this._dirty=true;if(this._isActive()){this.raisePropertyChanged("data");this.render()}else this._changed=this._dirty=true}},get_itemPlaceholder:function(){return this._placeholder||null},set_itemPlaceholder:function(a){if(this._placeholder!==a){this._placeholder=a;this._dirty=true;this.raisePropertyChanged("itemPlaceholder")}},get_parentContext:function(){return this._parentContext||null},set_parentContext:function(a){if(this._parentContext!==a){this._parentContext=a;this._dirty=true;this.raisePropertyChanged("parentContext")}},get_template:function(){return this._template||null},set_template:function(a){if(this._template!==a){this._template=a;this._dirty=true;if(this._isActive()){this.raisePropertyChanged("template");this.render()}else this._changed=this._dirty=true}},_elementContains:function(b,a){while(a){if(a===b)return true;a=a.parentNode}return false},_getTemplateAndPlaceholder:function(){var a=this.get_template(),b=this.get_itemPlaceholder(),c=this.get_element();if(!a)return {template:Sys.Preview.UI.Template.getTemplate(c),placeholder:c};if(!Sys.Preview.UI.Template.isInstanceOfType(a))a=Sys.Preview.UI.Template.getTemplate(a);if(!b)if(this._elementContains(c,a.get_element()))b=a.get_element();else{var d=this.get_id();if(d)b=Sys.UI.DomElement.getElementById(d+"_item",this.get_element())}return {template:a,placeholder:b||c}},_initializeResults:function(){for(var a=0,b=this._results.length;a<b;a++)this._results[a].initializeComponents()},_isActive:function(){return this.get_isInitialized()&&!this.get_isUpdating()},_raiseItemCreated:function(b){this.onItemCreated(b);var a=this.get_events().getHandler("itemCreated");if(a)a(this,b)},initialize:function(){Sys.Preview.UI.DataView.callBaseMethod(this,"initialize");this.render()},onItemCreated:function(){},render:function(){this._dirty=false;var h=this._getTemplateAndPlaceholder(),d=h.template,a=h.placeholder,b=this.get_data(),g=this.get_parentContext(),c;if(!d||!a)return;Sys.Application.disposeElement(a,true);a.innerHTML="";if(d.get_element()===a)Sys.UI.DomElement.removeCssClass(a,"sys-template");if(b===null||typeof b==="undefined")this._results=[];else if(b instanceof Array){var i=b.length;this._results=new Array(i);for(var e=0,j=i;e<j;e++){var f=b[e];c=d.createInstance(a,f,g);this._raiseItemCreated(new Sys.Preview.UI.DataViewItemEventArgs(f,c));this._results[e]=c}}else{c=d.createInstance(a,b,g);this.onItemCreated(new Sys.Preview.UI.DataViewItemEventArgs(b,c));this._results=[c]}this._initializeResults()},updated:function(){if(this._changed){this.raisePropertyChanged("");this._changed=false}if(this._dirty)this.render()}};Sys.Preview.UI.DataView.registerClass("Sys.Preview.UI.DataView",Sys.UI.Control,Sys.Preview.UI.ITemplateContext);Sys.Preview.UI.DataViewItemEventArgs=function(b,a){Sys.Preview.UI.DataViewItemEventArgs.initializeBase(this);this._result=a||null;this._data=b||null};Sys.Preview.UI.DataViewItemEventArgs.prototype={get_dataItem:function(){return this._data},get_templateResult:function(){return this._result}};Sys.Preview.UI.DataViewItemEventArgs.registerClass("Sys.Preview.UI.DataViewItemEventArgs",Sys.EventArgs);Sys.Preview.TemplateRes={"nullReferenceInPath":"Null reference while evaluating data path."};