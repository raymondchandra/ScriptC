namespace Proyek_Informatika.Report
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for NilaiSkripsi1.
    /// </summary>
    public partial class NilaiSkripsi2 : Telerik.Reporting.Report
    {
        private int id;
        public NilaiSkripsi2(int id)
        {
            //
            // Required for telerik Reporting designer support
            //

            InitializeComponent();
            this.ReportParameters["id_skripsi"].Value = id;

           
            //this.sqlDataSource2.Parameters.Add("@id", System.Data.DbType.Int32, id);


            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
    }
}