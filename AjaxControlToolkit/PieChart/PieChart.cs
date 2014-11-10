﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Drawing.Design;
using AjaxControlToolkit.Design;

namespace AjaxControlToolkit {

    [ClientCssResource(Constants.PieChartName)]
    [ClientScriptResource("Sys.Extended.UI.PieChart", Constants.PieChartName)]
    public class PieChart : ChartBase {
        PieChartValueCollection pieChartValueList = null;

        // Provide list of PieChartValue to client side. Need help from PieChartValues property 
        // for designer experience support, cause Editor always blocks the property
        // ability to provide values to client side as ExtenderControlProperty on run time.
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ExtenderControlProperty(true, true)]
        public PieChartValueCollection PieChartClientValues {
            get { return pieChartValueList; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(null)]
        [NotifyParentProperty(true)]
        [Editor(typeof(PieChartValueCollectionEditor), typeof(UITypeEditor))]
        public PieChartValueCollection PieChartValues {
            get {
                if(pieChartValueList == null)
                    pieChartValueList = new PieChartValueCollection();
                return pieChartValueList;
            }
        }

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
            if(IsDesignMode)
                return;

            foreach(PieChartValue pieChartValue in PieChartValues) {
                if(pieChartValue.Category == null || pieChartValue.Category.Trim() == String.Empty)
                    throw new Exception("Category is missing in the PieChartValue. Please provide a Category in the PieChartValue.");

                if(pieChartValue.Data == null)
                    throw new Exception("Data is missing in the PieChartValue. Please provide a Data in the PieChartValue.");
            }
        }
    }

}