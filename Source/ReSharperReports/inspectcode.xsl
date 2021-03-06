<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:key name="ISSUETYPES" match="/Report/Issues/Project/Issue" use="@TypeId"/>
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/" name="TopLevelReport">
    <html>
      <body>
        <head>
          <script>
            function sendHttpGetRequest(fileName, lineNumber) {
            var url = "http://127.0.0.1:63330/file?file=" + fileName + "&amp;line=" + lineNumber;
            var xmlHttp = new XMLHttpRequest();
            xmlHttp.open('GET', url, false);
            xmlHttp.send(null);
            }
          </script>
          <style>
            tr.error {
            background-color: #F8B2B2;
            }
            tr.warning {
            background-color: #B1D1F9;
            }
            tr.suggestion {
            background-color: #B2F7C0;
            }
            tr.hint {
            background-color: #D9FCB4;
            }
            table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
            }
            th, td {
            padding: 5px;
            text-align: left;
            }
          </style>
        </head>
        <h1>Code Inspection Error Report</h1>

        <h2>Errors</h2>
        <xsl:choose>
          <xsl:when test="/Report/IssueTypes/IssueType[@Severity='ERROR']">
            <xsl:for-each select="/Report/IssueTypes/IssueType[@Severity='ERROR']">
              <table style="width:100%">
                <caption>
                  <xsl:if test="@WikiUrl">
                    <xsl:element name="a">
                      <xsl:attribute name="href">
                        <xsl:value-of select="@WikiUrl"/>
                      </xsl:attribute>
                      <xsl:value-of select="@Description"/>
                    </xsl:element>
                  </xsl:if>
                  <xsl:if test="not(@WikiUrl)">
                    <xsl:value-of select="@Description"/>
                  </xsl:if>
                </caption>
                <tr class="error">
                  <th>File</th>
                  <th>Line Number</th>
                  <th>Message</th>
                </tr>
                <xsl:for-each select="key('ISSUETYPES',@Id)">
                  <tr>
                    <td>
                      <xsl:value-of select="@File"/>
                    </td>
                    <td>
                      <xsl:element name="a">
                        <xsl:attribute name="href">
                        </xsl:attribute>
                        <xsl:attribute name="onclick">sendHttpGetRequest('<xsl:value-of select="translate(@File, '\', '/')"/>',<xsl:value-of select="@Line"/>); return false;</xsl:attribute>
                        <xsl:value-of select="@Line"/>
                      </xsl:element>
                    </td>
                    <td>
                      <xsl:value-of select="@Message"/>
                    </td>
                  </tr>
                </xsl:for-each>
              </table>
              <br />
              <hr />
              <br />
            </xsl:for-each>
          </xsl:when>
          <xsl:otherwise>
            No Errors where generated by InspectCode.
          </xsl:otherwise>
        </xsl:choose>

        <h2>Warnings</h2>
        <xsl:choose>
          <xsl:when test="/Report/IssueTypes/IssueType[@Severity='WARNING']">
            <xsl:for-each select="/Report/IssueTypes/IssueType[@Severity='WARNING']">
              <table style="width:100%">
                <caption>
                  <xsl:if test="@WikiUrl">
                    <xsl:element name="a">
                      <xsl:attribute name="href">
                        <xsl:value-of select="@WikiUrl"/>
                      </xsl:attribute>
                      <xsl:value-of select="@Description"/>
                    </xsl:element>
                  </xsl:if>
                  <xsl:if test="not(@WikiUrl)">
                    <xsl:value-of select="@Description"/>
                  </xsl:if>
                </caption>
                <tr class="warning">
                  <th>File</th>
                  <th>Line Number</th>
                  <th>Message</th>
                </tr>
                <xsl:for-each select="key('ISSUETYPES',@Id)">
                  <tr>
                    <td>
                      <xsl:value-of select="@File"/>
                    </td>
                    <td>
                      <xsl:element name="a">
                        <xsl:attribute name="href">
                        </xsl:attribute>
                        <xsl:attribute name="onclick">sendHttpGetRequest('<xsl:value-of select="translate(@File, '\', '/')"/>',<xsl:value-of select="@Line"/>); return false;</xsl:attribute>
                        <xsl:value-of select="@Line"/>
                      </xsl:element>
                    </td>
                    <td>
                      <xsl:value-of select="@Message"/>
                    </td>
                  </tr>
                </xsl:for-each>
              </table>
              <br />
              <hr />
              <br />
            </xsl:for-each>
          </xsl:when>
          <xsl:otherwise>
            No Warnings where generated by InspectCode.
          </xsl:otherwise>
        </xsl:choose>

        <h2>Suggestions</h2>
        <xsl:choose>
          <xsl:when test="/Report/IssueTypes/IssueType[@Severity='SUGGESTION']">
            <xsl:for-each select="/Report/IssueTypes/IssueType[@Severity='SUGGESTION']">
              <table style="width:100%">
                <caption>
                  <xsl:if test="@WikiUrl">
                    <xsl:element name="a">
                      <xsl:attribute name="href">
                        <xsl:value-of select="@WikiUrl"/>
                      </xsl:attribute>
                      <xsl:value-of select="@Description"/>
                    </xsl:element>
                  </xsl:if>
                  <xsl:if test="not(@WikiUrl)">
                    <xsl:value-of select="@Description"/>
                  </xsl:if>
                </caption>
                <tr class="suggestion">
                  <th>File</th>
                  <th>Line Number</th>
                  <th>Message</th>
                </tr>
                <xsl:for-each select="key('ISSUETYPES',@Id)">
                  <tr>
                    <td>
                      <xsl:value-of select="@File"/>
                    </td>
                    <td>
                      <xsl:element name="a">
                        <xsl:attribute name="href">
                        </xsl:attribute>
                        <xsl:attribute name="onclick">sendHttpGetRequest('<xsl:value-of select="translate(@File, '\', '/')"/>',<xsl:value-of select="@Line"/>); return false;</xsl:attribute>
                        <xsl:value-of select="@Line"/>
                      </xsl:element>
                    </td>
                    <td>
                      <xsl:value-of select="@Message"/>
                    </td>
                  </tr>
                </xsl:for-each>
              </table>
              <br />
              <hr />
              <br />
            </xsl:for-each>
          </xsl:when>
          <xsl:otherwise>
            No Suggestions where generated by InspectCode.
          </xsl:otherwise>
        </xsl:choose>

        <h2>Hints</h2>
        <xsl:choose>
          <xsl:when test="/Report/IssueTypes/IssueType[@Severity='HINT']">
            <xsl:for-each select="/Report/IssueTypes/IssueType[@Severity='HINT']">
              <table style="width:100%">
                <caption>
                  <xsl:if test="@WikiUrl">
                    <xsl:element name="a">
                      <xsl:attribute name="href">
                        <xsl:value-of select="@WikiUrl"/>
                      </xsl:attribute>
                      <xsl:value-of select="@Description"/>
                    </xsl:element>
                  </xsl:if>
                  <xsl:if test="not(@WikiUrl)">
                    <xsl:value-of select="@Description"/>
                  </xsl:if>
                </caption>
                <tr class="hint">
                  <th>File</th>
                  <th>Line Number</th>
                  <th>Message</th>
                </tr>
                <xsl:for-each select="key('ISSUETYPES',@Id)">
                  <tr>
                    <td>
                      <xsl:value-of select="@File"/>
                    </td>
                    <td>
                      <xsl:element name="a">
                        <xsl:attribute name="href">
                        </xsl:attribute>
                        <xsl:attribute name="onclick">sendHttpGetRequest('<xsl:value-of select="translate(@File, '\', '/')"/>',<xsl:value-of select="@Line"/>); return false;</xsl:attribute>
                        <xsl:value-of select="@Line"/>
                      </xsl:element>
                    </td>
                    <td>
                      <xsl:value-of select="@Message"/>
                    </td>
                  </tr>
                </xsl:for-each>
              </table>
              <br />
              <hr />
              <br />
            </xsl:for-each>
          </xsl:when>
          <xsl:otherwise>
            No Hints where generated by InspectCode.
          </xsl:otherwise>
        </xsl:choose>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
