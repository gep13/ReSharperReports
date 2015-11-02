<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
    <xsl:output method="html" indent="yes" />
    <xsl:template match="/">
        <html>
          <head>
            <script>
              function sendHttpGetRequest(fileName, lineNumber) {
                var url = "http://127.0.0.1:63330/file?file=" + fileName + "&amp;line=" + lineNumber;
                var xmlHttp = new XMLHttpRequest();
                xmlHttp.open('GET', url, false);
                xmlHttp.send(null);
              }
            </script>
          </head>
            <body>
                <h1>Statistics</h1>
                <p>Total codebase size: <xsl:value-of select="//CodebaseCost"/></p>
                <p>Code to analyze: <xsl:value-of select="//TotalDuplicatesCost"/></p>
                <p>Total size of duplicated fragments: <xsl:value-of select="//CodebaseCost" /></p>
                <h1>Detected Duplicates</h1>
                <xsl:for-each select="//Duplicates/Duplicate">
                    <h2>Duplicated Code. Size: <xsl:value-of  select="@Cost"/></h2>
                    <h3>Duplicated Fragments:</h3>
                    <xsl:for-each select="Fragment">
                        <xsl:variable name="i" select="position()"/>
                        <p>Fragment <xsl:value-of select="$i"/> in file <xsl:value-of select="FileName"/> at line: <xsl:element name="a">
                          <xsl:attribute name="href">
                          </xsl:attribute>
                          <xsl:attribute name="onclick">sendHttpGetRequest('<xsl:value-of select="translate(FileName, '\', '/')"/>',<xsl:value-of select="LineRange/@Start"/>); return false;</xsl:attribute>
                          <xsl:value-of select="LineRange/@Start"/>
                        </xsl:element></p>
                        <pre><xsl:value-of select="Text"/></pre>
                    </xsl:for-each>
                </xsl:for-each>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>