<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=7.0.13.220, Culture=neutral, PublicKeyToken=a9d7983dfcc261be"
    Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<script runat="server">      
    public override void VerifyRenderingInServerForm(Control control)
    {
        // to avoid the server form (<form runat="server">) requirement
    }

    protected override void OnLoad(EventArgs e)
    {
        // bind the report viewer
        base.OnLoad(e);
        Bimbingan.Report = new Proyek_Informatika.Report.Bimbingan();

    }
</script>

<%--<html>
    <head title="Availibility" id="Head1" runat="server"></head>
    <body>--%>
    <asp:TextBox ID="ReportBimbingan" runat="server" />
    <div style="margin :5px;">
    
        <form clientidmode="Static" id="frep" runat="server">
        <telerik:ReportViewer ID="Bimbingan" runat="server" Width="800px" Height="800px"/>
        </form>
        
    </div>
    <%--</body>
</html>--%>