﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="telefonos.aspx.cs" Inherits="telefonos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="500" height="28" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td width="494"><div class="titulo">
      <div align="left">Gu&iacute;a de contactos </div>
    </div></td>
  </tr>
</table>
<br />
<table width="500" height="100" border="0" align="left" cellpadding="0" cellspacing="0">
  <tr>
    <td width= "valign=&quot;top&quot;" valign="top"><div align="left"><span class="titulo3">Buscar contacto por letra: </span><br />
            <br />
    </div>
        <table width="500" height="15" border="0" cellpadding="4" cellspacing="1">
          <tr>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">A</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">B</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">C</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">D</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">E</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">F</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">G</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">H</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">I</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">J</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">K</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">L</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">M</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">N</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">&Ntilde;</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">O</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">P</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">Q</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">R</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">S</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">T</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">U</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">V</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">W</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">X</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">Y</a></td>
            <td bgcolor="#F3F3F3"><a href="#" class="linktitnoticia">Z</a></td>
          </tr>
        </table>
      <br />
        <form id="form2" name="form1" method="post" action="">
          <div align="left"><span class="titulo3">Buscar contacto</span>
              <input name="textfield2" type="text" class="texto" size="35" />
              <input name="Submit" type="submit" class="texto" value="Buscar" />
          </div>
          <label> </label>
        </form>
      <table width="500" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td bgcolor="#F0F0F0"><div align="left"><span class="titulo3"><strong>Resultado de busqueda</strong></span></div></td>
          </tr>
        </table>
      <table width="500" border="0" cellspacing="1" cellpadding="3">
          <tr>
            <td width="120" height="25" bgcolor="#F0F0F0" class="titulo2"><div align="left">Nombre</div></td>
            <td width="82" bgcolor="#F0F0F0" class="titulo2"><div align="left">Interno</div></td>
            <td width="148" bgcolor="#F0F0F0" class="titulo2"><div align="left">Sector</div></td>
            <td width="121" bgcolor="#F0F0F0" class="titulo2"><div align="left">Imagen de Contacto </div></td>
          </tr>
        </table>
      <table width="500" border="0" cellspacing="1" cellpadding="3">
          <tr>
            <td width="120" class="texto"><div align="left">Apellido Nombre </div></td>
            <td width="81" class="texto"><div align="left">5432</div></td>
            <td width="150" class="texto"><div align="left">Sector de pertenencia </div></td>
            <td width="120" class="texto"><div align="left"><img src="imgs/icono-tel.jpg" width="16" height="16" /></div></td>
          </tr>
      </table></td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>