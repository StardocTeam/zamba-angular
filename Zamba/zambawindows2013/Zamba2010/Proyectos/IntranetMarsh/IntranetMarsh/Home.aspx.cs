using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace IntranetMarsh
{
    public partial class Home : System.Web.UI.Page
    {
        private void Page_Init(object sender, EventArgs e)
        {
            btnCloseNews.Click += new EventHandler(btnCloseNews_Click);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Marsh Intranet - Novedades";
            if (!Page.IsPostBack)
            {
                System.Collections.Generic.List<string> Lista = new System.Collections.Generic.List<string>();
                Lista.Add("18-09-2008@Foro Seguro Ambiental@Es de nuestro interés que en este Foro podamos debatir, sugerir y actualizarnos de los temas que surjan referentes al medio Ambiente.");
                Lista.Add("03-07-2008@D&O, cobertura para directores y gerentes@La responsabilidad de quien conduce una empresa excede a menudo la de su ámbito de acción. No es infrecuente que hasta el patrimonio personal de los ejecutivos se vea comprometido por los resultados de una decisión que no ha dado los resultados esperados.");
                Lista.Add("14-12-2008@SEGURO AMBIENTAL@Marsh se encuentra trabajando en forma exclusiva con varios mercados aseguradores para obtener más ofertas de productos, capacidad y competencia a efectos de poder ofrecer a nuestros clientes la mayor cantidad posible de alternativas de aseguramiento siempre dentro del marco del cumplimiento de la nueva legislación que esta terminando de definirse.");
                CurrentNews = Lista;
            }
            if (CurrentNews.Count > 0)
                this.LoadNews(CurrentNews);
        }


        /// <summary>
        /// Metodo que recibe las noticias y las publica en una tabla que se genera dinamicamente.
        /// </summary>
        /// <param name="NewsList"></param>
        /// <history>[Ezequiel] Created 12/02/09</history>
        private void LoadNews(System.Collections.Generic.List<string> NewsList)
        {
            this.NewsTable.Controls.Clear();
            if (NewsList.Count > 0)
            {
                this.NewsTable.Controls.Clear();
                foreach (string x in NewsList)
                {
                    if (NewsList.IndexOf(x) != 0)
                    {
                        TableRow RowLine = new TableRow();
                        TableCell CellLine = new TableCell();

                        //CellLine.HorizontalAlign = HorizontalAlign.Center;
                        //CellLine.Height = new Unit(30);
                        //Image imgl = new Image();
                        //imgl.ImageUrl = "Images/line.JPG";
                        //CellLine.Controls.Add(imgl);
                        CellLine.BackColor = System.Drawing.Color.LightSeaGreen;
                        RowLine.Cells.Add(CellLine);
                        this.NewsTable.Rows.Add(RowLine);

                    }

                    TableCell Ncell = new TableCell();
                    TableRow Nrow = new TableRow();
                    Label Nlbl = new Label();
                    LinkButton Nhyp = new LinkButton();
                    Image ImgC = new Image();

                    ImgC.ImageUrl = "Images/NewsImg.png";
                    ImgC.Height = new Unit(20);
                    ImgC.Width = new Unit(20);
                    Ncell.Controls.Add(ImgC);
                    Nlbl.Text = x.Split('@')[0] + " | ";
                    Nlbl.Font.Size = new FontUnit(FontSize.Small);
                    Nlbl.ForeColor = System.Drawing.Color.FromName("#0000cc");
                    Nhyp.ID = (NewsList.IndexOf(x) + 1).ToString();
                    Nhyp.Text = x.Split('@')[1];
                    Nhyp.Font.Size = new FontUnit(FontSize.Small);
                    Nhyp.Click += new EventHandler(ViewNews);
                    Ncell.Controls.Add(Nlbl);
                    Ncell.Controls.Add(Nhyp);
                    Nrow.Cells.Add(Ncell);
                    this.NewsTable.Rows.Add(Nrow);

                    Nrow = new TableRow();
                    Ncell = new TableCell();
                    Nlbl = new Label();

                    Nlbl.Text = x.Split('@')[2].Substring(0,99) + "...";
                    Nlbl.Font.Size = new FontUnit(FontSize.Small);
                    Nlbl.ForeColor = System.Drawing.Color.Gray;
                    Ncell.Controls.Add(Nlbl);
                    Nrow.Controls.Add(Ncell);
                    this.NewsTable.Rows.Add(Nrow);
                }
            }
        }


        /// <summary>
        /// Metodo que muestra la noticia seleccionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [Ezequiel] 12/02/09 Created
        /// </history>
        private void ViewNews(object sender, EventArgs e)
        {
            if (CurrentNews.Count > 0)
            {
                int Id = Convert.ToInt32(((LinkButton)sender).ID) - 1;
                this.NewsTitle.Text = CurrentNews[Id].Split('@')[1];
                this.NewsMsg.Text = CurrentNews[Id].Split('@')[2];
                this.ShowNews.Visible = true;
            }
        }

        private System.Collections.Generic.List<string> CurrentNews
        {
            get
            {
                if (Session["CurrentNews"] != null)
                    return (System.Collections.Generic.List<string>)Session["CurrentNews"];
                else
                {
                    System.Collections.Generic.List<string> mNews = new System.Collections.Generic.List<string>();
                    Session["CurrentNews"] = mNews;
                    return mNews;
                }
            }
            set
            {
                Session["CurrentNews"] = value;
            }
        }

        void btnCloseNews_Click(object sender, EventArgs e)
        {
            if (ShowNews.Visible == true)
                ShowNews.Visible = false;
        }
    }
}
