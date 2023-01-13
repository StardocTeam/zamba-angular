using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Themes;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;
using System.IO;
using System.Diagnostics;
using Zamba.AdminControls;
using Zamba.Core;

namespace DemoAppsHub
{
    public partial class UcHome : RadControl
    {
        public const int WM_SIZE = 5;

        
        private int indexer;

        #region Initialization

        public UcHome()
        {
            InitializeComponent();

            new TelerikMetroBlueTheme();
            //       ThemeResolutionService.LoadPackageResource("DemoAppsHub.PanoramaDemo.tssp");
            //     this.ThemeName = "PanoramaDemo";

            //  this.radPanorama1.ThemeName = "PanoramaDemo";
            this.radPanorama1.ScrollingBackground = true;
            this.radPanorama1.PanelImage = Zamba._2Home.Resource.bg_pattern;
            this.radPanorama1.PanoramaElement.BackgroundImagePrimitive.ImageLayout = ImageLayout.Tile;
            this.radPanorama1.SizeChanged += new EventHandler(radTilePanel1_SizeChanged);
            this.radPanorama1.ScrollBarAlignment = HorizontalScrollAlignment.Bottom;
            this.radPanorama1.ScrollBarThickness = 5;
            this.radPanorama1.PanoramaElement.GradientStyle = GradientStyles.Solid;
            this.radPanorama1.PanoramaElement.DrawFill = true;
            this.radPanorama1.PanoramaElement.BackColor = System.Drawing.Color.FromArgb(1, 23, 117);
            GenericRuleManager GRM = new GenericRuleManager();
            var CategoryButtonsGroup = GRM.GetDynamicButtons(ButtonPlace.InicioZamba, ButtonType.Rule);
            GRM = null;
            LoadDynamicButtons(CategoryButtonsGroup);

        }

        #endregion

        #region Event Handlers        

        void radTilePanel1_SizeChanged(object sender, EventArgs e)
        {
            int width = this.radPanorama1.Width + Math.Max((this.radPanorama1.PanoramaElement.ScrollBar.Maximum - this.radPanorama1.Width) / 4, 1);
            this.radPanorama1.PanelImageSize = new Size(width, 768);
            this.radPanorama1.PanoramaElement.UpdateViewOnScroll();
        }



        public void LoadDynamicButtons(Dictionary<String, IGenericCategoryGroup> dynamicButtonsGroup)
        {
            foreach (IGenericCategoryGroup Group in dynamicButtonsGroup.Values)
            {
                TileGroupElement controlsGroups;
                controlsGroups = new Telerik.WinControls.UI.TileGroupElement();
                this.radPanorama1.Groups.AddRange(new Telerik.WinControls.RadItem[] { controlsGroups });
                controlsGroups.AccessibleDescription = "Controls";
                controlsGroups.AccessibleName = "Controls";
                controlsGroups.CellSize = new System.Drawing.Size(155, 155);
                controlsGroups.Font = new System.Drawing.Font("Segoe UI Light", 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                controlsGroups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(172)))), ((int)(((byte)(255)))));
                //this.controlsGroups.Items.AddRange(new Telerik.WinControls.RadItem[] {
                //        this.movieLabTile,
                //this.photoAlbumTile
                //});
                controlsGroups.Margin = new System.Windows.Forms.Padding(0, 30, 65, 0);
                controlsGroups.Name = Group.Name;
                controlsGroups.RowsCount = 3;
                controlsGroups.Text = Group.Name;
                controlsGroups.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                int row = 0;
                int column = 0;

                foreach (GenericRuleTileButton RulegridTile in Group.Buttons)
                {
                    // 
                    // gridTile
                    // 
                    RulegridTile.AccessibleDescription = RulegridTile.Text;
                    RulegridTile.AccessibleName = "radTileElement1";
                    if (indexer == 0)
                    {
                        RulegridTile.BackgroundImage = Zamba._2Home.Resource.qsf_bg;
                        RulegridTile.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(129)))), ((int)(((byte)(190)))));
                        RulegridTile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(161)))), ((int)(((byte)(209)))));
                        RulegridTile.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(177)))), ((int)(((byte)(219)))));
                        RulegridTile.GradientStyle = Telerik.WinControls.GradientStyles.Linear;
                        indexer = 1;
                    }
                    else
                    {
                        RulegridTile.BackgroundImage = Zamba._2Home.Resource.magnifier_bg;
                        RulegridTile.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(64)))), ((int)(((byte)(172)))));
                        RulegridTile.BorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(64)))), ((int)(((byte)(172)))));
                        RulegridTile.BorderColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(128)))), ((int)(((byte)(197)))));
                        RulegridTile.BorderColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(128)))), ((int)(((byte)(197)))));
                        RulegridTile.BorderGradientStyle = Telerik.WinControls.GradientStyles.Linear;
                        indexer = 0;
                    }
                    RulegridTile.CellPadding = new System.Windows.Forms.Padding(5);
                    RulegridTile.DrawBorder = true;
                    RulegridTile.GradientAngle = 0F;
                    RulegridTile.Image = Zamba._2Home.Resource.GridView;
                    RulegridTile.ImageAlignment = System.Drawing.ContentAlignment.BottomLeft;
                    RulegridTile.ImageLayout = System.Windows.Forms.ImageLayout.None;
                    RulegridTile.Padding = new System.Windows.Forms.Padding(5, 5, 5, 10);
                    RulegridTile.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
                    RulegridTile.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
                    RulegridTile.TextWrap = true;
                    RulegridTile.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                    RulegridTile.Row = row;
                    RulegridTile.Column = column;

                    if (row < controlsGroups.RowsCount - 1)
                        row++;
                    else
                    {
                        row = 0;
                        column++;
                    }


                    controlsGroups.Items.Add(RulegridTile);
                    controlsGroups.UpdateLayout();
                }
            }
        }
        #endregion
    }


}
