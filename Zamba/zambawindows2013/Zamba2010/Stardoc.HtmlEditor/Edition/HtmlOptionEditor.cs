﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mshtml;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;
namespace Stardoc.HtmlEditor
{
    internal partial class HtmlOptionEditor : Form
    {
        #region Constantes
        private const String TITLE_STATE_VIEW = "Resumen elemento OPTION";
        private const String TITLE_STATE_UPDATE = "Edición elemento OPTION";
        private const String TITLE_STATE_INSERT = "Insertar elemento OPTION"; 
        #endregion

        #region Atributos
        private HtmlFormState _state = HtmlFormState.Insert ;
        #endregion

        #region Propiedades
        public OptionItem Option
        {
            get
            {
                return new OptionItem(tbName.Text, tbValue.Text, chkSelected.Checked, chkEnabled.Checked);
            }
            set
            {
                tbName.Text = value.Name;
                tbValue.Text = value.Value;
                chkEnabled.Checked = value.Enabled;
                chkSelected.Checked = value.Default;
            }
        }

        public HtmlFormState State
        {
            get { return _state; }
            set
            {
                _state = value;

                switch (_state)
                {
                    case HtmlFormState.View:
                        this.Text = TITLE_STATE_VIEW;
                        break;
                    case HtmlFormState.Update:
                        this.Text = TITLE_STATE_UPDATE;
                        break;
                    case HtmlFormState.Insert:
                        this.Text = TITLE_STATE_INSERT;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Constructores
        public HtmlOptionEditor()
        {
            InitializeComponent();
        }

        public HtmlOptionEditor(OptionItem item)
        {
            InitializeComponent();

            tbName.Text = item.Name;
            tbValue.Text = item.Value ;
            chkSelected.Checked = item.Default;
        }

        #endregion

        #region Eventos
        private void btAccept_Click(object sender, EventArgs e)
        {
                DialogResult = DialogResult.OK;
                Close();
        }
        private void AddSelectItem_Load(object sender, EventArgs e)
        {
            ActiveControl = tbName;

            //Pongo opciones por default
            if (_state == HtmlFormState.Insert) 
                chkEnabled.Checked = true;
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}