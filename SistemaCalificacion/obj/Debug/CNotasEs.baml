   M S B A M L   `   `   `  ????N  JSistemaCalificacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:SistemaCalificacionSistemaCalificacion  5   	   "    SistemaCalificacion.CNotasEs   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:SistemaCalificacion     5   	     ? Title$  CNotasEs??5   	   $	??550??6   $
??1200??6'   .??5	      ? .+?5
   
   ?? $
??Left=?6   $	??521??6/   $??0,0,-0.4,-0.2q?6<   
???   ?$	??Top=?6b   $
??1194??6z    J? Opacity$ 0.755Q?6?   ??5      ?? 5       ?? ImageSource$ imagen/3.jpg??6   5      ?? 5   
   $#??CALIFICACIONES DEL ESTUDIANTE??6   $
??Left=?68   $??	297,9,0,0q?6S   $	??Top=?6f   $??36(?6~   $??Cooper Black)?6?   ???	#FFF7F6F66?   ?? 5   
   $
??Left=?6   $	??367??60   $??	22,66,0,0q?6=   $	??Top=?6P   $	??208??6h   ??5      ?? 5       ?? EndPoint$ 0.5,1&?6&    ?? 
StartPoint$ 0.5,0&?67   .??5      ?  ? Color$ Black??6#    ? Offset$ 0.074Q?61   ? 5      $ 	#FF060606??6#   $ 0.984Q?65   ? 5      $ 	#FF098B27??6#   $ 0.529Q?65   ? 5      $ 	#FFF1F1DF??6#   $ 0.785Q?65   ? 5      $ 	#FF82D8E0??6#   $ 0.883Q?65   ? 5      $ 	#FFBECA31??6#   $ 0.678Q?65   ? 5      $ 	#FF011706??6#   $ 0.423Q?65   ? 5      $ 	#FF060442??6#   $ 0.186Q?65   ? 5      $ 	#FF033574??6#   $ 0.303Q?65   5      .+?5      ?? $??15??6   ???	#FF01ECF96$   ?? 5      $??CONSULTA DE NOTAS??6   $??14(?60   $??Segoe UI Emoji)?6>   $	??150??6Z   ??5       ?? 5!      $ 0.5,1&?6*   $ 0.5,0&?6;   .??5"      ? $ 	#FFF70404??6'   $ 0Q?69   ? 5#      $ White??6'   $ 1Q?65   5$      5&      ?? 5'      $??15??6   ???	#FF01ECF96$   ?? 5(      -   5         ??Name$ txtCI??5(      $??Escriba su C.I.??6#   
???????6=   ?? 5)      $??15??6   ???	#FF01ECF96$   ?? 5*      $??50??6   $y?Hidden=?6$   ?? 5+      -   5         ??Name$ txtCi??5+      $	??150??6%   $
??29,0q?61   4  .System.Windows.Automation.AutomationProperties	   IsRequiredForForm	 . 6?   ?? 5,      $??50??6   $y?Hidden=?6$   ?? 5-      -   5        
 ??Name$
 btnCon??5-      $??	CONSULTAR??6%   ???	#FFF90000$??80??6P   $
??Bold"?6[   6m   50      ?? 51   
   $!??REGISTRO DE NOTAS POR CURSO??6   $
??Left=?66   $??272,115,0,0q?6Q   $	??Top=?6f   ???	#FFF7F902$
??Bold"?6?   &   System.Windows.Controls.DataGrid  52   
   -   5          Name$ consultaCal??52      $
??Left=?6(   $	??226??6C   $??272,175,0,0q?6P   $	??Top=?6e   $	??901??6}   ?? 53   
   -   5        $
 	btnCerrar??53      $??CERRAR??6$   $
??Left=?65   $??1098,150,0,0q?6P   $	??Top=?6f   $??75??6~   ???	#FFF60707$??25??6?   $
??Bold"?6?   55      56      