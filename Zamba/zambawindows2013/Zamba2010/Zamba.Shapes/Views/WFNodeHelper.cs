
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using Zamba.Core.Enumerators;
using Zamba.Core.Searchs;
using Zamba.AppBlock;
using Zamba.Core;

public class WFNodeHelper
{

	public static IntPtr CD { get; set; }

	public static Image GetnodeImage(NodeWFTypes NodeType)
	{
		Image currentImage = default(Image);
		switch (NodeType)
		{
			case NodeWFTypes.Busqueda:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_list_check.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.Comienzo:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.Etapa:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_list_add.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.FloatingRule:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.Inicio:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_axis_x.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.insercion:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.nodoBusqueda:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_page_search.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.nodoInsercion:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_add_multiple.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.Permiso:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.Regla:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.Tarea:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.TipoDeRegla:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.WorkFlow:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_list_add_above.GetThumbnailImage(32, 32, CB(), CD);
				break;
			case NodeWFTypes.Estado:
				currentImage = global::Zamba.Shapes.My.Resources.Resources.appbar_list_add_below.GetThumbnailImage(32, 32, CB(), CD);
				break;
		}

		return currentImage;
	}

	private static Image.GetThumbnailImageAbort CB()
	{
	}

}