using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen
{
    public partial class ReportePromedio : Form
    {
        public ReportePromedio(DataTable dt)
        {
            InitializeComponent();
            //Crear objeto del reporte(.rpt).
            CrystalReport2 crProducto2 = new CrystalReport2();

            //Agsignar los datos contenidos en el DataSet que recibe como parámetros.
            crProducto2.SetDataSource(dt);

            //Asignar al visor de reportes el contenido del reporte.
            crystalReportViewer2.ReportSource = crProducto2;
        }
    }
}
