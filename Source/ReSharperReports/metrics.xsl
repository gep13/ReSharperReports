<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8" indent="yes" />

  <xsl:template match="/">
    <xsl:text disable-output-escaping='yes'>&lt;!DOCTYPE html&gt;</xsl:text>

    <html>
      <head>
        <title>Metrics Report</title>
        <style>
          tr.summary {
            background-color: #B1D1F9;
          }

          tr.details {
            background-color: #B2F7C0;
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
      <body>
        <h1>Metrics Report</h1>
        <xsl:apply-templates select="CodeMetricsReport/Targets/Target/Modules/Module" />
      </body>
    </html>
  </xsl:template>

  <xsl:template match="Module">
    <h2><xsl:apply-templates select="@Name" /></h2>
    <h3>Summary</h3>
    <table>
      <tr class="summary">
        <th>MaintainabilityIndex</th>
        <th>CyclomaticComplexity</th>
        <th>ClassCoupling</th>
        <th>DepthOfInheritance</th>
        <th>LinesOfCode</th>
      </tr>
      <tr>
        <xsl:apply-templates select="Metrics/Metric" />
      </tr>
    </table>

    <xsl:apply-templates select="Namespaces" />
  </xsl:template>

  <xsl:template match="Namespaces">
    <h3>Details</h3>
    <table>
      <tr class="details">
        <th>Namespace</th>
        <th>MaintainabilityIndex</th>
        <th>CyclomaticComplexity</th>
        <th>ClassCoupling</th>
        <th>DepthOfInheritance</th>
        <th>LinesOfCode</th>
      </tr>
      <xsl:apply-templates select="Namespace" />
    </table>
  </xsl:template>

  <xsl:template match="Namespace">
    <tr>
      <td><xsl:apply-templates select="@Name" /></td>
      <xsl:apply-templates select="Metrics/Metric" />
    </tr>
  </xsl:template>

  <xsl:template match="Metrics/Metric">
    <td>
      <xsl:apply-templates select="@Value" />
    </td>
  </xsl:template>

</xsl:stylesheet>
