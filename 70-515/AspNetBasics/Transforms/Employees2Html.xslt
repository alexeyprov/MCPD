<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
	<xsl:output method="xml" indent="yes"/>

	<xsl:template match="/">
		<div>
			<xsl:apply-templates select="Employees/Employee"/>
		</div>
	</xsl:template>

	<xsl:template match="Employee" >
		<br/>
		<h4>
			<u>
				Employee 
				<xsl:value-of select="ID"/>
			</u>
		</h4>
		<b>
			<xsl:value-of select="Name"/>
		</b>
		<br />
		<b>
			<xsl:value-of select="Position"/>
		</b>
	</xsl:template>
	
</xsl:stylesheet>
