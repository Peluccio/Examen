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
    public partial class Reporte : Form
    {
        public Reporte(DataTable dt)
        {
            InitializeComponent();
            //Crear objeto del reporte(.rpt).
            CrystalReport1 crProducto = new CrystalReport1();

            //Agsignar los datos contenidos en el DataSet que recibe como parámetros.
            crProducto.SetDataSource(dt);

            //Asignar al visor de reportes el contenido del reporte.
            crystalReportViewer1.ReportSource = crProducto;
        }
    }
}
