﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Stardoc.HtmlEditor.HtmlControls;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.Edition
{
    internal partial class HtmlTextBox
        : Form
    {
        #region Constantes
        /// <summary>
        /// Titulo del Form cuando esta en estado View
        /// </summary>
        private const String TITLE_STATE_VIEW = "Resumen elemento TEXTBOX";
        /// <summary>
        /// Titulo del Form cuando esta en estado Edit
        /// </summary>
        private const String TITLE_STATE_UPDATE = "Editar elemento TEXTBOX";
        /// <summary>
        /// Titulo del Form cuando esta en estado Insert
        /// </summary>
        private const String TITLE_STATE_INSERT = "Insertar elemento TEXTBOX";
        #endregion

        #region Atributos
        private HtmlFormState _state = HtmlFormState.Insert;
        #endregion

        #region Propiedades
        public HtmlFormState State
        {
            get { return _state; }
            set
            {
                _state = value;
                switch (value)
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
        public TextBoxItem TextBox
        {
            get { return new TextBoxItem(tbName.Text, tbValue.Text, chkReadOnly.Checked, chkEnabled.Checked); }
            set
            {
                tbName.Text = value.Name;
                tbValue.Text = value.Value;
                chkEnabled.Checked = value.Enabled;
                chkReadOnly.Checked = value.ReadOnly;
            }
        }
        #endregion

        #region Constructores
        internal HtmlTextBox()
        {
            InitializeComponent();
        }
        internal HtmlTextBox(TextBoxItem item, HtmlFormState state)
            : this()
        {
            State = state;
            TextBox = item;
        }
        #endregion

        #region Eventos
        private void btAccept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void HtmlComboBox_Load(object sender, EventArgs e)
        {
            ActiveControl = tbName;
        }
        #endregion
    }
}